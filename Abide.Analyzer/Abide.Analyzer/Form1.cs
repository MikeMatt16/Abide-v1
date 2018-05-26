using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Analyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Prepare
            MapFile map1 = new MapFile(), map2 = new MapFile();
            bool compare = true;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Prepare
                openDlg.Filter = "Halo 2 Map Files (*.map)|*.map";

                //Open map 1
                openDlg.Title = "Open source map";
                if (openDlg.ShowDialog() == DialogResult.OK)
                    using (var mapStream = openDlg.OpenFile())
                    { map1.Load(mapStream); compare &= true; }
                else compare = false;

                //Open map 1
                openDlg.Title = "Open modified map";
                if (openDlg.ShowDialog() == DialogResult.OK)
                    using (var mapStream = openDlg.OpenFile())
                    { map2.Load(mapStream); compare &= true; }
                else compare = false;
            }

            //Check
            CompareTest[] results = null;
            if (compare)
                results = Maps_Compare(map1, map2);
        }

        private CompareTest[] Maps_Compare(MapFile sourceMap, MapFile moddedMap)
        {
            //Prepare
            IndexEntry sourceEntry, moddedEntry;
            byte[] sourceBuffer, moddedBuffer;
            List<CompareTest> testResults = new List<CompareTest>();

            //Loop
            for (int i = 0; i < Math.Min(sourceMap.IndexEntries.Count, moddedMap.IndexEntries.Count); i++)
            {
                //Get
                sourceEntry = sourceMap.IndexEntries[i];
                moddedEntry = moddedMap.IndexEntries[i];

                //Sanity Check
                if (sourceEntry.PostProcessedSize <= 0 || moddedEntry.PostProcessedSize <= 0)
                    continue;

                //Goto
                sourceBuffer = new byte[sourceEntry.PostProcessedSize];
                sourceEntry.TagData.Seek(sourceEntry.PostProcessedOffset, SeekOrigin.Begin);
                sourceEntry.TagData.Read(sourceBuffer, 0, sourceEntry.PostProcessedSize);

                //Goto
                moddedBuffer = new byte[moddedEntry.PostProcessedSize];
                moddedEntry.TagData.Seek(moddedEntry.PostProcessedOffset, SeekOrigin.Begin);
                moddedEntry.TagData.Read(moddedBuffer, 0, moddedEntry.PostProcessedSize);

                //Compare
                CompareTest test = new CompareTest(sourceEntry, moddedEntry);
                Buffer_Compare(sourceBuffer, moddedBuffer, test);

                //Add
                testResults.Add(test);
            }

            //Return
            return testResults.ToArray();
        }

        private void Buffer_Compare(byte[] sourceBuffer, byte[] moddedBuffer, CompareTest test)
        {
            //Get Size difference
            int sizeDifference = moddedBuffer.Length - sourceBuffer.Length;
            int min = Math.Min(sourceBuffer.Length, moddedBuffer.Length);

            //Loop
            for (int i = 0; i < min; i++)
            {
                if (sourceBuffer[i] != moddedBuffer[i])
                    test.Differences.Add(i, new byte[] { sourceBuffer[i], moddedBuffer[i] });
            }
        }

        private sealed class CompareTest
        {
            public IndexEntry SourceEntry
            {
                get { return source; }
            }
            public IndexEntry ModdedEntry
            {
                get { return modded; }
            }
            public Dictionary<int, byte[]> Differences
            {
                get { return differences; }
            }

            private readonly Dictionary<int, byte[]> differences = new Dictionary<int, byte[]>();
            private readonly IndexEntry source;
            private readonly IndexEntry modded;
            
            public CompareTest(IndexEntry source, IndexEntry modded)
            {
                this.source = source;
                this.modded = modded;
            }
        }
    }
}
