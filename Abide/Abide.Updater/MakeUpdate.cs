using Abide.Updater.Compression;
using Abide.Updater.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Abide.Updater
{
    public partial class MakeUpdate : Form
    {
        private readonly string root;

        private MakeUpdate()
        {
            //Upgrade?
            if(Settings.Default.RequiresUpgrade)
            {
                Settings.Default.Upgrade();
                Settings.Default.Save();
            }

            //Init
            InitializeComponent();
            releaseNotesRichTextBox.Text = Settings.Default.ReleaseNotes;
            updatePackageUrlTextBox.Text = Settings.Default.PackageUrl;
        }
        public MakeUpdate(string directory) : this()
        {
            //Check
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            if (!Directory.Exists(directory)) throw new DirectoryNotFoundException();

            //Setup
            root = directory;

            //Prepare
            List<string> files = new List<string>();

            //Loop
            files.AddRange(Directory.GetFiles(directory));

            //Loop Directories
            foreach (string childDirectory in Directory.EnumerateDirectories(directory))
                files.AddRange(directory_GetFiles(childDirectory));

            //Begin
            filesTreeView.BeginUpdate();
            filesTreeView.Nodes.Clear();

            //Loop
            foreach (string file in files)
            {
                //Split...
                string[] parts = file.Replace(directory, string.Empty).Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

                //Prepare
                TreeNodeCollection collection = filesTreeView.Nodes;
                for (int i = 0; i < parts.Length - 1; i++)
                    if (collection.ContainsKey(parts[i])) collection = collection[parts[i]].Nodes;
                    else collection = collection.Add(parts[i], parts[i]).Nodes;

                //Add
                string filename = parts[parts.Length - 1];
                TreeNode node = collection.Add(filename, filename);
                node.Checked = true;
                node.Tag = file;
            }

            //End
            filesTreeView.EndUpdate();
        }
        private void createPackageButton_Click(object sender, EventArgs e)
        {
            //Prepare
            string[] parts = null;
            string localName = null;
            AssemblyName name = null;

            //Create
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                //Setup
                saveDlg.FileName = "Update";
                saveDlg.Filter = "Abide Update Package Files (*.aup)|*.aup;";

                //Show
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    //Save
                    Settings.Default.ReleaseNotes = releaseNotesRichTextBox.Text;
                    Settings.Default.PackageUrl = updatePackageUrlTextBox.Text;
                    Settings.Default.Save();

                    //Get Save Root
                    string saveRoot = Path.GetDirectoryName(saveDlg.FileName);

                    //Make Package
                    UpdatePackageFile package = new UpdatePackageFile();
                    UpdateManifest manifest = new UpdateManifest();
                    string[] files = node_GetFiles(filesTreeView.Nodes);

                    //Setup
                    manifest.Release = DateTime.UtcNow;
                    manifest.ReleaseNotes = releaseNotesRichTextBox.Text;
                    manifest.PackageUrl = updatePackageUrlTextBox.Text;

                    //Loop
                    foreach (string file in files)
                    {
                        //Setup
                        parts = file.Replace(root, string.Empty).Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                        try { name = AssemblyName.GetAssemblyName(file); } catch { name = null; }
                        localName = string.Join("\\", parts);

                        //Add to manifest
                        manifest.Add(localName, name);

                        //Add to package
                        package.AddFile(file, localName);
                    }

                    //Save Manifest
                    using (FileStream fs = new FileStream(Path.Combine(saveRoot, "Update.xml"), FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        //Create Writer
                        XmlWriter writer = XmlWriter.Create(fs, new XmlWriterSettings() { Indent = true });

                        //Write Manifest
                        manifest.SaveXml(writer);

                        //Close
                        writer.Close();
                    }

                    //Save Package
                    using (FileStream fs = new FileStream(saveDlg.FileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                        package.Save(fs);
                }
            }
        }
        private string[] directory_GetFiles(string directory)
        {
            //Check
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            if (!Directory.Exists(directory)) throw new DirectoryNotFoundException();

            //Prepare
            List<string> files = new List<string>();

            //Loop
            files.AddRange(Directory.GetFiles(directory));

            //Loop Directories
            foreach (string childDirectory in Directory.EnumerateDirectories(directory))
                files.AddRange(directory_GetFiles(childDirectory));

            //Return
            return files.ToArray();
        }
        private string[] node_GetFiles(TreeNodeCollection collection)
        {
            //Prepare
            List<string> files = new List<string>();

            //Loop
            foreach (TreeNode node in collection)
                if (node.Tag is string && node.Checked) files.Add((string)node.Tag);
                else if (node.Tag == null) files.AddRange(node_GetFiles(node.Nodes));

            //Return
            return files.ToArray();
        }
    }
}
