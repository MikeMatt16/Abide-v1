using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Guerilla.Library
{
    public sealed class GuerillaTagFile : IDisposable
    {
        public GuerillaTagFile() { }

        public void Load(string fileName)
        {
            //Prepare
            FileStream fileStream = null;

            //Create stream
            try { fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); }
            catch (Exception ex) { throw new GuerillaException("Unable to load guerilla tag file.", ex); }

            //Load
            Load(fileStream);
        }

        public void Load(Stream inStream)
        {
            //Check
            if (inStream == null) throw new ArgumentNullException(nameof(inStream));
            if (!inStream.CanRead) throw new ArgumentException("Stream is not readable.", nameof(inStream));
            if (!inStream.CanSeek) throw new ArgumentException("Stream is not seekable.", nameof(inStream));
            if (inStream.Length < GuerillaTagHeader.Size) throw new GuerillaException("Invalid guerilla tag file.");

            //Try
            try
            {
                //Create reader
                using (BinaryReader reader = new BinaryReader(inStream))
                {
                    //Read header
                    GuerillaTagHeader header = reader.Read<GuerillaTagHeader>();

                    //stop hammer time
                    System.Diagnostics.Debugger.Break();
                }
            }
            finally
            {

            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
