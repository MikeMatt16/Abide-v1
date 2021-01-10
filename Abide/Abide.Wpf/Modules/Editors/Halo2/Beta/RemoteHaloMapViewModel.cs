using Abide.DebugXbox;
using Abide.HaloLibrary;
using Abide.HaloLibrary.IO;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Abide.Wpf.Modules.Editors.Halo2.Beta
{
    public sealed class RemoteHaloMapViewModel : BaseViewModel
    {
        List<Tag> Tags { get; } = new List<Tag>();
        public Xbox Xbox { get; private set; }
        private BinaryReader Reader { get; set; }
        private BinaryWriter Writer { get; set; }

        public RemoteHaloMapViewModel()
        {
            var xboxes = NameAnsweringProtocol.Discover(10);
            if (xboxes.Any())
            {
                Xbox = xboxes.First();
                Xbox.ConnectionStateChanged += Xbox_ConnectionStateChanged;
                Xbox.Connect();
            }
        }
        private void Xbox_ConnectionStateChanged(object sender, EventArgs e)
        {
            if (Xbox.Connected)
            {
                _ = Xbox.MemoryStream.Seek(0x80061000, SeekOrigin.Begin);
                Reader = new BinaryReader(Xbox.MemoryStream);
                Writer = new BinaryWriter(Xbox.MemoryStream);
                uint objectsOffset = Reader.ReadUInt32();
                TagId scenarioId = Reader.ReadTagId();
                uint unknown = Reader.ReadUInt32();
                int objectCount = Reader.ReadInt32();
                TagFourCc tags = Reader.ReadTag();

                Tags.Clear();
                _ = Xbox.MemoryStream.Seek(objectsOffset, SeekOrigin.Begin);
                using (VirtualStream stream = new VirtualStream(objectsOffset, Reader.ReadBytes(32 * objectCount)))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    for (int i = 0; i < objectCount; i++)
                    {
                        TagFourCc root = reader.ReadTag();
                        TagFourCc parent = reader.ReadTag();
                        TagFourCc @class = reader.ReadTag();
                        TagId id = reader.ReadTagId();
                        uint tagNameAddress = reader.ReadUInt32();
                        uint tagDataAddress = reader.ReadUInt32();
                        uint size = reader.ReadUInt32();

                        Tags.Add(new Tag()
                        {
                            Root = root,
                            Parent = parent,
                            Class = @class,
                            Id = id,
                            TagNameAddress = tagNameAddress,
                            TagDataAddress = tagDataAddress,
                            Length = (int)size
                        });
                    }
                }

                Tags.ForEach(t =>
                {
                    Xbox.MemoryStream.Position = t.TagNameAddress;
                    t.TagName = Reader.ReadUTF8NullTerminated();
                });
            }
            else
            {
                Reader = null;
                Writer = null;
            }
        }
    }

    public sealed class Tag
    {
        public TagFourCc Root { get; set; }
        public TagFourCc Parent { get; set; }
        public TagFourCc Class { get; set; }
        public TagId Id { get; set; }
        public long TagNameAddress { get; set; }
        public long TagDataAddress { get; set; }
        public int Length { get; set; }
        public string TagName { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(TagName))
            {
                return base.ToString();
            }

            return $"{Root} {TagName} 0x{Id} Address: {TagDataAddress:X8} Length: {Length}";
        }
    }
}
