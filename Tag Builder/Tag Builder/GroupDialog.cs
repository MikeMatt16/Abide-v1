using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.TagBuilder
{
    public partial class GroupDialog : Form
    {
        private static class TagGroups
        {
            public static string[] GetTagGroups()
            {
                return (string[])tagGroups.Clone();
            }

            private static string[] tagGroups = new string[]
            {
                "$#!+",
                "*cen",
                "*eap",
                "*ehi",
                "*igh",
                "*ipd",
                "*qip",
                "*rea",
                "*sce",
                "<fx>",
                "BooM",
                "DECP",
                "DECR",
                "MGS2",
                "PRTM",
                "adlg",
                "ai**",
                "ant!",
                "bipd",
                "bitm",
                "bloc",
                "bsdt",
                "char",
                "cin*",
                "clu*",
                "clwd",
                "coll",
                "coln",
                "colo",
                "cont",
                "crea",
                "ctrl",
                "dc*s",
                "dec*",
                "deca",
                "devi",
                "devo",
                "dgr*",
                "dobc",
                "effe",
                "egor",
                "eqip",
                "fog ",
                "foot",
                "fpch",
                "garb",
                "gldf",
                "goof",
                "grhi",
                "hlmt",
                "hmt ",
                "hsc*",
                "hud#",
                "hudg",
                "item",
                "itmc",
                "jmad",
                "jpt!",
                "lens",
                "lifi",
                "ligh",
                "lsnd",
                "ltmp",
                "mach",
                "matg",
                "mdlg",
                "metr",
                "mode",
                "mpdt",
                "mply",
                "mulg",
                "nhdt",
                "obje",
                "phmo",
                "phys",
                "pmov",
                "pphy",
                "proj",
                "prt3",
                "sbsp",
                "scen",
                "scnr",
                "sfx+",
                "shad",
                "sily",
                "skin",
                "sky ",
                "slit",
                "sncl",
                "snd!",
                "snde",
                "snmx",
                "spas",
                "spk!",
                "ssce",
                "sslt",
                "stem",
                "styl",
                "tdtl",
                "trak",
                "trg*",
                "udlg",
                "ugh!",
                "unhi",
                "unic",
                "unit",
                "vehc",
                "vehi",
                "vrtx",
                "weap",
                "weat",
                "wgit",
                "wgtz",
                "whip",
                "wigl",
                "wind",
                "wphi",
            };
        }

        /// <summary>
        /// Gets or sets the selected tag group.
        /// </summary>
        public string SelectedGroup
        {
            get { return tagGroupComboBox.SelectedItem?.ToString() ?? string.Empty; }
            set { tagGroupComboBox.SelectedItem = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupDialog"/> class.
        /// </summary>
        public GroupDialog()
        {
            InitializeComponent();

            //Add
            foreach (string tagGroup in TagGroups.GetTagGroups())
                tagGroupComboBox.Items.Add(tagGroup);

            //Select
            tagGroupComboBox.SelectedIndex = 0;
        }
    }
}
