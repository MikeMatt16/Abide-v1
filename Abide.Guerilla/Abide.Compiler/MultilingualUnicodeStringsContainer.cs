using Abide.HaloLibrary.Halo2Map;
using System.Collections.Generic;

namespace Abide.Compiler
{
    internal sealed class MultilingualUnicodeStringsContainer
    {
        public int EnSize { get; set; } = 0;
        public int JpSize { get; set; } = 0;
        public int NlSize { get; set; } = 0;
        public int FrSize { get; set; } = 0;
        public int EsSize { get; set; } = 0;
        public int ItSize { get; set; } = 0;
        public int KrSize { get; set; } = 0;
        public int ZhSize { get; set; } = 0;
        public int PrSize { get; set; } = 0;
        public List<StringEntry> En { get; set; } = new List<StringEntry>();
        public List<StringEntry> Jp { get; set; } = new List<StringEntry>();
        public List<StringEntry> Nl { get; set; } = new List<StringEntry>();
        public List<StringEntry> Fr { get; set; } = new List<StringEntry>();
        public List<StringEntry> Es { get; set; } = new List<StringEntry>();
        public List<StringEntry> It { get; set; } = new List<StringEntry>();
        public List<StringEntry> Kr { get; set; } = new List<StringEntry>();
        public List<StringEntry> Zh { get; set; } = new List<StringEntry>();
        public List<StringEntry> Pr { get; set; } = new List<StringEntry>();

        public void Clear()
        {
            //Reset
            EnSize = 0;
            JpSize = 0;
            NlSize = 0;
            FrSize = 0;
            EsSize = 0;
            ItSize = 0;
            KrSize = 0;
            ZhSize = 0;
            PrSize = 0;
            En.Clear();
            Jp.Clear();
            Nl.Clear();
            Fr.Clear();
            Es.Clear();
            It.Clear();
            Kr.Clear();
            Zh.Clear();
            Pr.Clear();
        }
    }
}
