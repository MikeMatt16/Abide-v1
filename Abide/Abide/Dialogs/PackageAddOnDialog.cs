using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Abide.Dialogs
{
    /// <summary>
    /// Prompts the user to package an AddOn.
    /// </summary>
    public partial class PackageAddOnDialog : Form
    {
        /// <summary>
        /// Gets or sets the name of the package.
        /// </summary>
        public string PackageName
        {
            get { return nameTextBox.Text; }
            set { nameTextBox.Text = value; }
        }
        /// <summary>
        /// Gets or sets the primary assembly of the package.
        /// </summary>
        public string PrimaryAssembly
        {
            get { return primaryAssembly; }
            set { primaryAssembly = value; }
        }

        private string primaryAssembly = string.Empty;
        private string root = string.Empty;

        /// <summary>
        /// Initializes a new <see cref="PackageAddOnDialog"/>.
        /// </summary>
        public PackageAddOnDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Loads a directory into the dialog, presenting the user with choices on which files to include.
        /// </summary>
        /// <param name="directory">The base directory.</param>
        public void LoadDirectory(string directory)
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
        private void createButton_Click(object sender, EventArgs e)
        {
            //Check
            if (!File.Exists(Path.Combine(root, primaryAssembly))) return;
            if (string.IsNullOrEmpty(nameTextBox.Text)) return;
            if (string.IsNullOrEmpty(root)) return;
            if (!Directory.Exists(root)) return;

            //Prepare
            string[] files = node_GetFiles(filesTreeView.Nodes);
            AddOnManifest manifest = new AddOnManifest();
            manifest.PrimaryAssemblyFile = primaryAssembly;
            manifest.Name = nameTextBox.Text;
            
            //Loop
            foreach (string file in files)
            {
                //Split...
                string[] parts = file.Replace(root, string.Empty).Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                manifest.Add(string.Join("\\", parts));
            }

            //Write
            manifest.SaveXml(Path.Combine(root, "Manifest.xml"));

            //OK
            DialogResult = DialogResult.OK;
        }

        private string[] node_GetFiles(TreeNodeCollection collection)
        {
            //Prepare
            List<string> files = new List<string>();

            //Loop
            foreach (TreeNode node in collection)
                if (node.Tag is string) files.Add((string)node.Tag);
                else if (node.Tag == null) files.AddRange(node_GetFiles(node.Nodes));

            //Return
            return files.ToArray();
        }
    }
}
