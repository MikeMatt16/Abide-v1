using Abide.HaloLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Abide.Dialogs
{
    internal partial class StringSelectDialog : Form
    {
        public StringId SelectedString
        {
            get { return selectedString; }
            set { OnSelectedStringChanged(value); }
        }

        private readonly StringID[] strings;
        private readonly Dictionary<StringId, int> stringIndex;
        private StringId selectedString = StringId.Zero;

        private StringSelectDialog()
        {
            InitializeComponent();

            //Setup
            strings = new StringID[0];
            stringIndex = new Dictionary<StringId, int>();
        }
        
        public StringSelectDialog(List<string> strings)
        {
            //Init
            InitializeComponent();
            this.strings = new StringID[strings.Count];
            stringIndex = new Dictionary<StringId, int>();

            //Loop
            for (int i = 0; i < strings.Count; i++)
            { this.strings[i] = new StringID(strings[i], i); stringIndex.Add(StringId.FromString(strings[i], i), i); }
            
            //Add to list
            stringList.BeginUpdate();
            foreach (StringID sid in this.strings)
                stringList.Items.Add(sid);
            stringList.EndUpdate();
        }

        private volatile bool isWorking = false;

        private void filterBox_TextChanged(object sender, EventArgs e)
        {
            //Check
            if (string.IsNullOrEmpty(filterBox.Text) && !isWorking)
            {
                //Add to list
                stringList.BeginUpdate();
                stringList.Items.Clear();
                foreach (StringID sid in strings)
                    stringList.Items.Add(sid);
                stringList.EndUpdate();
            }
            else if (!isWorking)
                ThreadPool.QueueUserWorkItem(state =>
                {
                    //Get Pattern
                    string pattern = (string)state;

                    //Working...
                    isWorking = true;

                    //Match
                    StringID[] matchedIds = strings.Where(sid => Regex.IsMatch(sid.Text, pattern)).ToArray();
                    Invoke(new MethodInvoker(delegate
                    {
                        //Add to list
                        stringList.BeginUpdate();
                        stringList.Items.Clear();
                        foreach (StringID sid in matchedIds)
                            stringList.Items.Add(sid);
                        stringList.EndUpdate();
                    }));

                    //Done
                    isWorking = false;
                }, filterBox.Text);
        }

        private void stringList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get
            bool isSelected = stringList.SelectedItem is StringID;
            okButton.Enabled = isSelected;
            if (isSelected) selectedString = ((StringID)stringList.SelectedItem).ToSID();
            else selectedString = StringId.Zero;
        }

        protected virtual void OnSelectedStringChanged(StringId stringId)
        {
            //Check
            if (!stringIndex.ContainsKey(stringId)) return;

            //Begin Update
            stringList.BeginUpdate();

            //Select
            stringList.SelectedItem = strings[stringIndex[stringId]];

            //End Update
            stringList.EndUpdate();
        }

        private class StringID
        {
            public string Text
            {
                get { return text; }
                set { text = value; }
            }
            public int Index
            {
                get { return index; }
                set { index = value; }
            }

            private string text;
            private int index;

            public StringID(string text, int index)
            {
                this.text = text;
                this.index = index;
            }
            public override string ToString()
            {
                if (!string.IsNullOrEmpty(text)) return text;
                else return "null";
            }
            public StringId ToSID()
            {
                return StringId.FromString(text, index);
            }
        }
    }
}
