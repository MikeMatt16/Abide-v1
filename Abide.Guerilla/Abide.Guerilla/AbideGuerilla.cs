using Abide.Decompiler;
using Abide.Guerilla.Dialogs;
using Abide.Guerilla.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Abide.Guerilla
{
    public partial class AbideGuerilla : Form
    {
        private Dictionary<string, TagGroupFileEditor> openEditors = new Dictionary<string, TagGroupFileEditor>();

        public AbideGuerilla()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }

        private void decompilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize CacheDecompiler instance...
            using (CacheDecompiler decompiler = new CacheDecompiler() { Icon = Properties.Resources.abide_icon, StartPosition = FormStartPosition.CenterParent })
                decompiler.ShowDialog();    //Show
        }

        private void compilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize CaceCompiler instance...
            using (CacheCompilerDialog compiler = new CacheCompilerDialog())
                compiler.ShowDialog();  //Show
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            TagGroupFile file = null;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "All files (*.*)|*.*";
                openDlg.CustomPlaces.Add(new FileDialogCustomPlace(RegistrySettings.WorkspaceDirectory));

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                {

                    //Check
                    if (openEditors.ContainsKey(openDlg.FileName))
                        openEditors[openDlg.FileName].Show();
#if DEBUG
                    //Load file
                    file = new TagGroupFile();
                    file.Load(openDlg.FileName);
#else
                    try
                    {
                        using (Stream stream = openDlg.OpenFile())
                        {
                            //Load file
                            file = new TagGroupFile();
                            file.Load(stream);
                        }
                    }
                    catch { file = null; MessageBox.Show($"An error occured while opening {openDlg.FileName}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } 
#endif

                    //Check
                    if (file != null)
                    {
                        //Create editor
                        TagGroupFileEditor editor = new TagGroupFileEditor(openDlg.FileName, file) { MdiParent = this };
                        editor.FileNameChanged += Editor_FileNameChanged;
                        editor.FormClosed += Editor_FormClosed;
                        
                        //Show
                        editor.Show();
                    }
                }
            }
        }
        
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create dialog
            using (NewTagGroupDialog groupDialog = new NewTagGroupDialog())
            {
                //Show
                if(groupDialog.ShowDialog() == DialogResult.OK)
                {
                    //Create file
                    TagGroupFile file = new TagGroupFile() { TagGroup = groupDialog.SelectedGroup };

                    //Get name
                    string tagName = "tag"; int tagIndex = 1;
                    while (openEditors.ContainsKey($"{tagName}{tagIndex}")) tagIndex++;
                    
                    //Create editor
                    TagGroupFileEditor editor = new TagGroupFileEditor($"{tagName}{tagIndex}.{file.TagGroup.Name}", file) { MdiParent = this };
                    editor.FileNameChanged += Editor_FileNameChanged;
                    editor.FormClosed += Editor_FormClosed;
                    
                    //Show
                    editor.Show();
                }
            }
        }

        private void Editor_FileNameChanged(object sender, EventArgs e)
        {
            //Clear
            openEditors.Clear();

            //Loop
            foreach (Form child in MdiChildren)
                if (child is TagGroupFileEditor editor)
                    openEditors.Add(editor.FileName, editor);
        }

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Prepare
            TagGroupFileEditor editor = null;

            //Get form
            if (sender is TagGroupFileEditor)
            {
                editor = (TagGroupFileEditor)sender;
                openEditors.Remove(editor.FileName);
            }
            else
            {
                //Clear
                openEditors.Clear();

                //Loop
                foreach (Form child in MdiChildren)
                    if (child is TagGroupFileEditor)
                    {
                        editor = (TagGroupFileEditor)child;
                        openEditors.Add(editor.FileName, editor);
                    }
            }
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize StartupScreen instance...
            using (StartupScreen startup = new StartupScreen())
            startup.ShowDialog();   //Show
        }
    }
}
