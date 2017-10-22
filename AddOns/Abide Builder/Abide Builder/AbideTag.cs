using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Abide.Builder
{
    /// <summary>
    /// Represents an Abide tag.
    /// </summary>
    public sealed class AbideTag : IDisposable
    {
        /// <summary>
        /// Gets and returns the map file that owns this tag.
        /// </summary>
        public MapFile Map
        {
            get { return mapFile; }
        }
        /// <summary>
        /// Gets and returns the tag data instance.
        /// </summary>
        public Instance Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// Gets and returns an array of tag blocks within the tag.
        /// </summary>
        public AbideTagBlock[] TagBlocks
        {
            get { return tagBlocks; }
        }
        /// <summary>
        /// Gets and returns an array of all string ID references within the tag.
        /// </summary>
        public StringIdReference[] StringIdReferences
        {
            get { return stringIdReferences; }
        }
        /// <summary>
        /// Gets and returns an array of all tag ID references within the tag.
        /// </summary>
        public TagIdReference[] TagIdReferences
        {
            get { return tagIdReferences; }
        }
        /// <summary>
        /// Gets and returns the raw container of this tag.
        /// </summary>
        public RawContainer Raws
        {
            get { return rawContainer; }
        }

        private readonly MapFile mapFile;
        private readonly AbideTagDefinition tagDefinition;
        private readonly Instance instance;
        private readonly AbideTagBlock[] tagBlocks;
        private readonly StringIdReference[] stringIdReferences;
        private readonly TagIdReference[] tagIdReferences;
        private readonly RawContainer rawContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTag"/> class instance using the supplied map file and index entry.
        /// </summary>
        /// <param name="mapFile">The map file.</param>
        /// <param name="indexEntry">The index entry which belongs to the specified map file.</param>
        /// <exception cref="NullReferenceException"><paramref name="mapFile"/> or <paramref name="indexEntry"/> is null.</exception>
        public AbideTag(MapFile mapFile, IndexEntry indexEntry)
        {
            //Check
            if (mapFile == null) throw new ArgumentNullException(nameof(mapFile));
            if (indexEntry == null) throw new ArgumentNullException(nameof(indexEntry));

            //Prepare
            List<AbideTagBlock> tagBlocks = new List<AbideTagBlock>();
            List<TagIdReference> tagIdReferences = new List<TagIdReference>();
            List<StringIdReference> stringIdReferences = new List<StringIdReference>();

            //Get Definition
            tagDefinition = new AbideTagDefinition(indexEntry.Root);

            //Goto
            indexEntry.TagData.Seek(indexEntry.PostProcessedOffset, SeekOrigin.Begin);
            using(BinaryReader tagReader = new BinaryReader(indexEntry.TagData))
            {
                //Initialize tag
                instance = Instance.CreateInstance(tagReader.ReadBytes((int)tagDefinition.Size));
                using (BinaryReader dataReader = new BinaryReader(instance.Data))
                    foreach (var member in tagDefinition.GetTagMembers())
                    {
                        //Goto
                        instance.Data.Seek(member.Offset, SeekOrigin.Begin);
                        switch (member.MemberType)
                        {
                            case TagMemberTypes.TagId: tagIdReferences.Add(new TagIdReference(dataReader.ReadInt32(), member)); break;
                            case TagMemberTypes.StringId: stringIdReferences.Add(new StringIdReference(dataReader.ReadInt32(), member)); break;
                            case TagMemberTypes.TagBlock: tagBlocks.Add(new AbideTagBlock(member, tagReader, dataReader.ReadInt64())); break;
                        }
                    }

                //Close instance
                instance.Close();
            }

            //Set
            rawContainer = (RawContainer)indexEntry.Raws.Clone();
            this.tagBlocks = tagBlocks.ToArray();
            this.stringIdReferences = stringIdReferences.ToArray();
            this.tagIdReferences = tagIdReferences.ToArray();
            this.mapFile = mapFile;
        }
        /// <summary>
        /// Post-processes the Abide tag.
        /// This nullifies all tag block, tag ID, and string ID instances within this tag and its children.
        /// </summary>
        public void PostProcess()
        {
            //Open instance
            instance.Open();

            //Create Writer
            using (BinaryWriter writer = new BinaryWriter(instance.Data))
            {
                //Loop
                foreach (AbideTagDefinitionMember member in tagDefinition.GetTagMembers())
                {
                    //Goto
                    instance.Data.Seek(member.Offset, SeekOrigin.Begin);
                    switch (member.MemberType)  //Handle type
                    {
                        case TagMemberTypes.TagId: writer.Write(TagId.Null); break;
                        case TagMemberTypes.StringId: writer.Write(StringId.Zero); break;
                        case TagMemberTypes.TagBlock: writer.Write(TagBlock.Zero); break;
                    }
                }
            }

            //Close Instance
            instance.Close();

            //Loop
            foreach (AbideTagBlock tagBlock in tagBlocks)
                tagBlock.PostProcess();
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Dispose
            rawContainer.Dispose();
            foreach (AbideTagBlock tagBlock in tagBlocks)
                tagBlock.Dispose();
        }
        /// <summary>
        /// Compiles the Abide tag into a single tag byte array.
        /// </summary>
        /// <param name="address">The base address of the tag.</param>
        /// <returns>A byte array.</returns>
        public byte[] Compile(long address)
        {
            //Prepare
            byte[] tagBuffer = new byte[GetCompiledLength(address)];
            long blockAddress = address;
            long position = 0;

            //Create buffer
            using (MemoryStream ms = new MemoryStream(tagBuffer))
            using (BinaryReader reader = new BinaryReader(ms))
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Write header
                writer.Write(GetInstancedBuffer(instance));

                //Get addresses
                blockAddress = address + ms.Position;
                position = ms.Position;
                
                //Write Blocks
                foreach (AbideTagBlock tagBlock in tagBlocks)
                {
                    //Pad
                    blockAddress += tagBlock.Pad(blockAddress);
                    position += tagBlock.Pad(blockAddress);

                    //Create tag block
                    TagBlock block = tagBlock.CreateTagBlock(blockAddress);

                    //Goto member and write block
                    ms.Seek(tagBlock.Member.Offset, SeekOrigin.Begin);
                    writer.Write(block);

                    //Write block array
                    ms.Seek(position, SeekOrigin.Begin);
                    writer.Write(tagBlock.Compile(blockAddress));

                    //Update addresses
                    blockAddress = address + ms.Position;
                    position = ms.Position;
                }
            }

            //Return
            return tagBuffer;
        }
        /// <summary>
        /// Retrieves the length of the compiled tag.
        /// </summary>
        /// <param name="address">The base address of the tag.</param>
        /// <returns>A signed 64-bit integer containing the length of the compiled data.</returns>
        public long GetCompiledLength(long address)
        {
            //Setup
            long length = instance.Length;
            long newAddress = address + length;

            //Loop
            foreach (AbideTagBlock tagBlock in tagBlocks)
            {
                newAddress += tagBlock.Pad(newAddress);
                length += tagBlock.GetCompiledLength(newAddress);
            }

            //Return
            return length;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        private byte[] GetInstancedBuffer(Instance instance)
        {
            //Prepare
            byte[] buffer;

            //Clone Instance
            using (Instance newInstance = (Instance)instance.Clone())
            {
                //Open
                newInstance.Open();

                //Write references
                using (BinaryWriter writer = new BinaryWriter(newInstance.Data))
                {
                    //Loop through tag references
                    foreach (TagIdReference idReference in tagIdReferences)
                    {
                        writer.BaseStream.Seek(idReference.Member.Offset, SeekOrigin.Begin);
                        writer.Write(idReference.Id);
                    }

                    //Loop through string references
                    foreach (StringIdReference idReference in stringIdReferences)
                    {
                        writer.BaseStream.Seek(idReference.Member.Offset, SeekOrigin.Begin);
                        writer.Write(idReference.Id);
                    }
                }

                //Close
                newInstance.Close();

                //Get Buffer
                buffer = newInstance.GetBuffer();
            }

            //Return
            return buffer;
        }
    }

    /// <summary>
    /// Represents a tag block instance within an Abide tag.
    /// </summary>
    public sealed class AbideTagBlock : IDisposable, IEnumerable<AbideBlock>
    {
        /// <summary>
        /// Gets and returns a block at a given index.
        /// </summary>
        /// <param name="index">The zero-based index of the block.</param>
        /// <returns>A block instance.</returns>
        public AbideBlock this[int index]
        {
            get { return blocks[index]; }
        }
        /// <summary>
        /// Gets and returns the number of blocks within the tag block.
        /// </summary>
        public int Count
        {
            get { return blocks.Count; }
        }
        /// <summary>
        /// Gets and returns the tag block member.
        /// </summary>
        public AbideTagDefinitionMember Member
        {
            get { return tagBlockMember; }
        }

        private readonly List<AbideBlock> blocks;
        private readonly AbideTagDefinitionMember tagBlockMember;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagBlock"/> class using the supplied Abide tag definition member.
        /// </summary>
        /// <param name="member">The tag block Abide tag definition member.</param>
        public AbideTagBlock(AbideTagDefinitionMember member)
        {
            //Setup
            this.tagBlockMember = member;
            blocks = new List<AbideBlock>((int)member.MaxBlockCount);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagBlock"/> class using the supplied Abide tag definition member, map tag reader, and tag block instance.
        /// </summary>
        /// <param name="member">The tag block Abide tag definition member.</param>
        /// <param name="tagReader">The map file tag reader.</param>
        /// <param name="block">The tag block read from the map.</param>
        public AbideTagBlock(AbideTagDefinitionMember member, BinaryReader tagReader, TagBlock block) : this(member)
        {
            //Loop
            for (int i = 0; i < block.Count; i++)
            {
                //Goto
                tagReader.BaseStream.Seek(block.Offset + (member.Size * i), SeekOrigin.Begin);
                blocks.Add(new AbideBlock(tagReader.ReadBytes((int)member.Size), tagReader, member));
            }
        }
        /// <summary>
        /// Post-processes the Abide tag block.
        /// This nullifies all tag block, tag ID, and string ID instances within this tag block and its children.
        /// </summary>
        public void PostProcess()
        {
            //Loop through blocks
            foreach (AbideBlock block in blocks)
            {
                //Open instance
                block.Instance.Open();

                //Create Writer
                using (BinaryWriter writer = new BinaryWriter(block.Instance.Data))
                {
                    //Loop
                    foreach (AbideTagDefinitionMember member in tagBlockMember.GetTagMembers())
                    {
                        //Goto
                        block.Instance.Data.Seek(member.Offset, SeekOrigin.Begin);
                        switch (member.MemberType)  //Handle type
                        {
                            case TagMemberTypes.TagId: writer.Write(TagId.Null); break;
                            case TagMemberTypes.StringId: writer.Write(StringId.Zero); break;
                            case TagMemberTypes.TagBlock: writer.Write(TagBlock.Zero); break;
                        }
                    }
                }

                //Close instance
                block.Instance.Close();

                //Loop
                foreach (AbideTagBlock tagBlock in block.TagBlocks)
                    tagBlock.PostProcess();
            }
        }
        /// <summary>
        /// Attempts to add a new block instance to this tag block.
        /// </summary>
        /// <param name="newBlock">The block that was created if the attempt was successfull; otherwise null.</param>
        /// <returns>True if a block was successfully added; otherwise false.</returns>
        public bool Add(out AbideBlock newBlock)
        {
            //Prepare
            newBlock = null;

            //Check
            if (blocks.Count < tagBlockMember.MaxBlockCount)
            {
                //Create
                newBlock = new AbideBlock(tagBlockMember);

                //Return
                return true;
            }

            //Return
            return false;
        }
        /// <summary>
        /// Attempts to add a given block to the tag block.
        /// </summary>
        /// <param name="block">The block to add.</param>
        /// <returns>True if the block was successfully added; otherwise false.</returns>
        public bool Add(AbideBlock block)
        {
            //Check
            if (blocks.Count < tagBlockMember.MaxBlockCount)
            {
                //Add
                blocks.Add(block);

                //Return
                return true;
            }

            //Return
            return false;
        }
        /// <summary>
        /// Removes a block from the tag block at a given index.
        /// </summary>
        /// <param name="index">The zero-based index of the block to remove.</param>
        public void Remove(int index)
        {
            //Remove
            if (blocks.Count > index) blocks.RemoveAt(index);
        }
        /// <summary>
        /// Removes all blocks from the tag block.
        /// </summary>
        public void Clear()
        {
            //Clear
            blocks.Clear();
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Loop
            blocks.ForEach(b => b.Dispose());
        }
        /// <summary>
        /// Compiles the Abide tag block into a single tag block byte array.
        /// </summary>
        /// <param name="address">The base address of the tag block.</param>
        /// <returns>A byte array.</returns>
        public byte[] Compile(long address)
        {
            //Prepare
            byte[] tagBuffer = new byte[GetCompiledLength(address)];
            long blockAddress = address;
            long position = 0, blockPosition = 0;

            //Create buffer
            using (MemoryStream ms = new MemoryStream(tagBuffer))
            using (BinaryReader reader = new BinaryReader(ms))
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Write array
                foreach (AbideBlock block in blocks)
                    writer.Write(GetInstancedBuffer(block.Instance, block));

                //Get addresses
                blockAddress = address + ms.Position;
                position = ms.Position;

                //Write Children
                for (int i = 0; i < blocks.Count; i++)
                    foreach (AbideTagBlock tagBlock in blocks[i].TagBlocks)
                    {
                        //Get block position
                        blockPosition = i * Member.Size;

                        //Pad
                        blockAddress += tagBlock.Pad(blockAddress);
                        position += tagBlock.Pad(blockAddress);

                        //Create tag block
                        TagBlock block = tagBlock.CreateTagBlock(blockAddress);

                        //Goto member and write block
                        ms.Seek(blockPosition + tagBlock.Member.Offset, SeekOrigin.Begin);
                        writer.Write(block);

                        //Write block array
                        ms.Seek(position, SeekOrigin.Begin);
                        writer.Write(tagBlock.Compile(blockAddress));

                        //Update addresses
                        blockAddress = address + ms.Position;
                        position = ms.Position;
                    }
            }

            //Return
            return tagBuffer;
        }
        /// <summary>
        /// Retrieves the length of the compiled tag block.
        /// </summary>
        /// <param name="address">The base address of the tag.</param>
        /// <returns>A signed 64-bit integer containing the length of the compiled data.</returns>
        public long GetCompiledLength(long address)
        {
            //Setup
            long length = blocks.Count * tagBlockMember.Size;
            long newAddress = address + length;

            //Loop
            for (int i = 0; i < Count; i++)
                foreach (AbideTagBlock tagBlock in blocks[i].TagBlocks)
                {
                    newAddress += tagBlock.Pad(newAddress);
                    length += tagBlock.GetCompiledLength(newAddress);
                }

            //Return
            return length;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        private byte[] GetInstancedBuffer(Instance instance, AbideBlock block)
        {
            //Prepare
            byte[] buffer;

            //Clone Instance
            using (Instance newInstance = (Instance)instance.Clone())
            {
                //Open
                newInstance.Open();

                //Write references
                using (BinaryWriter writer = new BinaryWriter(newInstance.Data))
                {
                    //Loop through tag references
                    foreach (TagIdReference idReference in block.TagIdReferences)
                    {
                        writer.BaseStream.Seek(idReference.Member.Offset, SeekOrigin.Begin);
                        writer.Write(idReference.Id);
                    }

                    //Loop through string references
                    foreach (StringIdReference idReference in block.StringIdReferences)
                    {
                        writer.BaseStream.Seek(idReference.Member.Offset, SeekOrigin.Begin);
                        writer.Write(idReference.Id);
                    }
                }

                //Close
                newInstance.Close();

                //Get Buffer
                buffer = newInstance.GetBuffer();
            }

            //Return
            return buffer;
        }
        /// <summary>
        /// Returns an enumerator that iterates this instance.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<AbideBlock> GetEnumerator()
        {
            return blocks.GetEnumerator();
        }
        /// <summary>
        /// Retrieves the number of bytes to pad the address per the tag block's alignment.
        /// </summary>
        /// <param name="address">The base address.</param>
        /// <returns>The number of padded bytes.</returns>
        public long Pad(long address)
        {
            if (Count == 0) return 0;
            if (address % Member.Alignment == 0) return 0;
            else return Member.Alignment - (address % Member.Alignment);
        }
        /// <summary>
        /// Creates a tag block using the given block address.
        /// </summary>
        /// <param name="address">The address of the tag block array.</param>
        /// <returns>A tag block structure.</returns>
        public TagBlock CreateTagBlock(long address)
        {
            if (Count == 0) return TagBlock.Zero;
            return new TagBlock((uint)blocks.Count, (uint)address);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return blocks.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a single block element within an Abide tag block.
    /// </summary>
    public sealed class AbideBlock : IDisposable
    {
        /// <summary>
        /// Gets and returns the tag block member.
        /// </summary>
        public AbideTagDefinitionMember Member
        {
            get { return tagBlockMember; }
        }
        /// <summary>
        /// Gets and returns this block's instance.
        /// </summary>
        public Instance Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// Gets and returns an array of all child tag blocks within this block.
        /// </summary>
        public AbideTagBlock[] TagBlocks
        {
            get { return tagBlocks; }
        }
        /// <summary>
        /// Gets and returns an array of all string ID references within this block.
        /// </summary>
        public StringIdReference[] StringIdReferences
        {
            get { return stringIdReferences; }
        }
        /// <summary>
        /// Gets and returns an array of all tag ID references within this block.
        /// </summary>
        public TagIdReference[] TagIdReferences
        {
            get { return tagIdReferences; }
        }

        private readonly Instance instance;
        private readonly AbideTagBlock[] tagBlocks;
        private readonly StringIdReference[] stringIdReferences;
        private readonly TagIdReference[] tagIdReferences;
        private readonly AbideTagDefinitionMember tagBlockMember;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideBlock"/> class using the supplied abide tag definition member.
        /// </summary>
        /// <param name="tagBlockMember">The Abide tag definition member representing a tag block.</param>
        public AbideBlock(AbideTagDefinitionMember tagBlockMember)
        {
            //Prepare
            List<AbideTagBlock> tagBlocks = new List<AbideTagBlock>();

            //Initialize
            instance = Instance.CreateInstance(tagBlockMember.Size);
            foreach (var member in tagBlockMember.GetTagMembers().Where(m => m.MemberType == TagMemberTypes.TagBlock))
                tagBlocks.Add(new AbideTagBlock(member));

            //Close instance
            instance.Close();

            //Set
            this.tagBlockMember = tagBlockMember;
            this.tagBlocks = tagBlocks.ToArray();
            tagIdReferences = new TagIdReference[0];
            stringIdReferences = new StringIdReference[0];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AbideBlock"/> class using the supplied data buffer, map tag reader, and abide tag definition member.
        /// </summary>
        /// <param name="buffer">The data buffer containing the block's data.</param>
        /// <param name="tagReader">The map's tag data reader.</param>
        /// <param name="tagBlockMember">The Abide tag definition member representing a tag block.</param>
        public AbideBlock(byte[] buffer, BinaryReader tagReader, AbideTagDefinitionMember tagBlockMember)
        {
            //Prepare
            List<AbideTagBlock> tagBlocks = new List<AbideTagBlock>();
            List<TagIdReference> tagIdReferences = new List<TagIdReference>();
            List<StringIdReference> stringIdReferences = new List<StringIdReference>();

            //Initialize
            instance = Instance.CreateInstance(buffer);
            using (BinaryReader dataReader = new BinaryReader(instance.Data))
                foreach (var member in tagBlockMember.GetTagMembers())
                {
                    //Goto
                    instance.Data.Seek(member.Offset, SeekOrigin.Begin);
                    switch (member.MemberType)
                    {
                        case TagMemberTypes.TagId: tagIdReferences.Add(new TagIdReference(dataReader.ReadInt32(), member)); break;
                        case TagMemberTypes.StringId: stringIdReferences.Add(new StringIdReference(dataReader.ReadInt32(), member)); break;
                        case TagMemberTypes.TagBlock: tagBlocks.Add(new AbideTagBlock(member, tagReader, dataReader.ReadInt64())); break;
                    }
                }

            //Close instance
            instance.Close();

            //Set
            this.tagBlockMember = tagBlockMember;
            this.tagBlocks = tagBlocks.ToArray();
            this.stringIdReferences = stringIdReferences.ToArray();
            this.tagIdReferences = tagIdReferences.ToArray();
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Dispose of the instance.
            instance.Dispose();
        }
    }

    /// <summary>
    /// Represents a data instance.
    /// </summary>
    public sealed class Instance : IDisposable, ICloneable
    {
        /// <summary>
        /// Gets and returns this instance's data stream.
        /// </summary>
        public MemoryStream Data
        {
            get { return ms; }
        }
        /// <summary>
        /// Gets and returns the length of the instance's data.
        /// </summary>
        public long Length
        {
            get { return buffer.LongLength; }
        }

        private bool isDisposed = false;
        private byte[] buffer;
        private MemoryStream ms;

        /// <summary>
        /// Initializes a new instance of the <see cref="Instance"/> class using the supplied data buffer. 
        /// </summary>
        /// <param name="buffer">The data buffer.</param>
        public Instance(byte[] buffer)
        {
            //Initialize
            this.buffer = buffer;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Instance"/> class using the supplied data length. 
        /// </summary>
        /// <param name="length">The length of the data.</param>
        public Instance(long length)
        {
            //Initialize
            buffer = new byte[length];
        }
        /// <summary>
        /// Returns a byte array containing the instance's buffered data.
        /// This data is a shallow copy of the instance's data buffer.
        /// </summary>
        /// <returns>A byte array.</returns>
        public byte[] GetBuffer()
        {
            return (byte[])buffer.Clone();
        }
        /// <summary>
        /// Opens the stream of this instance.
        /// </summary>
        public void Open()
        {
            ms = new MemoryStream(buffer);
        }
        /// <summary>
        /// Closes the stream of this instance.
        /// </summary>
        public void Close()
        {
            ms.Dispose();
            ms = null;
        }
        /// <summary>
        /// Creates a copy of this instance.
        /// </summary>
        /// <returns>A new instance of the <see cref="Instance"/> class.</returns>
        public object Clone()
        {
            //Clone buffer
            byte[] buffer = (byte[])this.buffer.Clone();
            return new Instance(buffer);
        }
        /// <summary>
        /// Releases all resources used by this <see cref="Instance"/>.
        /// </summary>
        public void Dispose()
        {
            //Check
            if (isDisposed) return;

            //Check
            isDisposed = true;
            if (ms != null) { ms.Dispose(); ms = null; }
            buffer = null;
        }

        /// <summary>
        /// Creates and opens a new instance using the supplied data buffer.
        /// </summary>
        /// <param name="buffer">The data buffer.</param>
        /// <returns>A new <see cref="Instance"/>.</returns>
        public static Instance CreateInstance(byte[] buffer)
        {
            //Create
            Instance instance = new Instance(buffer);
            instance.Open();

            //Return
            return instance;
        }
        /// <summary>
        /// Creates and opens a new instance using the supplied length.
        /// </summary>
        /// <param name="length">The data length.</param>
        /// <returns>A new <see cref="Instance"/>.</returns>
        public static Instance CreateInstance(long length)
        {
            //Create
            Instance instance = new Instance(length);
            instance.Open();

            //Return
            return instance;
        }
    }

    /// <summary>
    /// Represents a string ID reference.
    /// </summary>
    public sealed class StringIdReference
    {
        /// <summary>
        /// Gets or sets the referenced string ID.
        /// </summary>
        public StringId Id { get => id; set => id = value; }
        /// <summary>
        /// Gets and returns the Abide tag definition member.
        /// </summary>
        public AbideTagDefinitionMember Member => member;

        private readonly AbideTagDefinitionMember member;
        private StringId id;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringIdReference"/> class using the supplied string ID and Abide tag definition member.
        /// </summary>
        /// <param name="id">The referenced string ID.</param>
        /// <param name="member">The Abide tag definition string ID member.</param>
        /// <exception cref="ArgumentNullException"><paramref name="member"/> is null.</exception>
        /// <exception cref="ArgumentException">The member type of <paramref name="member"/> is not <see cref="TagMemberTypes.StringId"/></exception>
        public StringIdReference(StringId id, AbideTagDefinitionMember member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (member.MemberType != TagMemberTypes.StringId) throw new ArgumentException("Member must be a string ID.", nameof(member));

            this.id = id;
            this.member = member;
        }
        /// <summary>
        /// Returns a string representation of this reference.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"String ID {id} @ {member.Offset}";
        }
    }

    /// <summary>
    /// Represents a Tag ID reference.
    /// </summary>
    public sealed class TagIdReference
    {
        /// <summary>
        /// Gets or sets the referenced tag ID.
        /// </summary>
        public TagId Id { get => id; set => id = value; }
        /// <summary>
        /// Gets and returns the Abide tag definition member.
        /// </summary>
        public AbideTagDefinitionMember Member => member;

        private readonly AbideTagDefinitionMember member;
        private TagId id;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TagIdReference"/> class using the supplied tag ID and Abide tag definition member.
        /// </summary>
        /// <param name="id">The referenced tag ID.</param>
        /// <param name="member">The Abide tag definition tag ID member.</param>
        /// <exception cref="ArgumentNullException"><paramref name="member"/> is null.</exception>
        /// <exception cref="ArgumentException">The member type of <paramref name="member"/> is not <see cref="TagMemberTypes.TagId"/></exception>
        public TagIdReference(TagId id, AbideTagDefinitionMember member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (member.MemberType != TagMemberTypes.TagId) throw new ArgumentException("Member must be a tag ID.", nameof(member));

            this.id = id;
            this.member = member;
        }
        /// <summary>
        /// Returns a string representation of this reference.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"Tag ID {id} @ {member.Offset}";
        }
    }
}
