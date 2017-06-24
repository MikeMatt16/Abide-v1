using Abide.Compression;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Forms
{
    /// <summary>
    /// Represents an AddOn installer Windows form.
    /// </summary>
    public partial class AddOnInstaller : Form
    {
        /// <summary>
        /// Gets and returns the number of packages in the installer.
        /// </summary>
        public int PackageCount
        {
            get
            {
                //Prepare
                int count = 0;

                //Loop
                foreach (object item in addOnsListBox.Items)
                    if (item is AddOn) count++;

                //Return
                return count;
            }
        }
        /// <summary>
        /// Initializes a new <see cref="AddOnInstaller"/> form.
        /// </summary>
        public AddOnInstaller()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Adds a package to the AddOn package list.
        /// </summary>
        /// <param name="package">The AddOn package file.</param>
        /// <param name="manifest">The AddOn maniest.</param>
        public void AddPackage(AddOnPackageFile package, AddOnManifest manifest)
        {
            //Create
            AddOn addOn = new AddOn(package, manifest);

            //Add to list...
            addOnsListBox.SetItemChecked(addOnsListBox.Items.Add(addOn), true);
        }
        /// <summary>
        /// Adds a package to the AddOn package list.
        /// </summary>
        /// <param name="packageFileName">The file path of the AddOn package file.</param>
        public void AddPackage(string packageFileName)
        {
            //Prepare...
            AddOnPackageFile package = new AddOnPackageFile();
            package.DecompressData += Program.Package_DecompressData;

            //Load
            try { package.Load(packageFileName); } catch { }

            //Extract
            try
            {
                if (package.Entries.Count > 0 && package.Entries.ContainsFilename("Manifest.xml"))
                {
                    //Load Manifest
                    AddOnManifest manifest = new AddOnManifest();
                    manifest.LoadXml(package.LoadFile("Manifest.xml"));

                    //Create
                    AddOn addOn = new AddOn(package, manifest);

                    //Add to list...
                    addOnsListBox.SetItemChecked(addOnsListBox.Items.Add(addOn), true);
                }
            }
            catch (Exception) { }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            //Close
            Close();
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            //Disable
            installButton.Enabled = false;
            addPackageButton.Enabled = false;

            //Prepare
            AddOn addOn = null;
            string root = string.Empty;

            //Loop
            foreach (int i in addOnsListBox.CheckedIndices)
                if (addOnsListBox.Items[i] is AddOn)
                {
                    //Get AddOn instance from list...
                    addOn = (AddOn)addOnsListBox.Items[i];

                    //Install...
                    root = Path.Combine(AbideRegistry.AddOnsDirectory, addOn.Manifest.Name);
                    Log("Installing {0} at {1}", addOn.Manifest.Name, root);

                    //Create Directories
                    string directory = string.Empty;
                    foreach (string file in addOn.Manifest)
                    {
                        //Get Directory...
                        directory = Path.GetDirectoryName(Path.Combine(root, file));

                        //Create?
                        if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); Log("Creating Directory {0}...", directory); }
                    }

                    //Extract Files
                    foreach (string file in addOn.Manifest)
                    {
                        //Check
                        if (addOn.Package.Entries.ContainsFilename(file))
                        {
                            //Prepare
                            bool failed = false;

                            //Copy to file stream...
                            using (Stream packageFileStream = addOn.Package.LoadFile(file))
                                try
                                {
                                    using (FileStream fs = new FileStream(Path.Combine(root, file), FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                                        packageFileStream.CopyTo(fs);
                                }
                                catch (Exception ex) { failed = true; Log("Failed to write file {0}. \r\n\t{1}", file, ex.Message); }

                            //Log
                            if (!failed) Log("Extracting file {0}", file);
                        }
                    }

                    //Extract Manifest
                    if(addOn.Package.Entries.ContainsFilename("Manifest.xml"))
                    {
                        //Prepare
                        bool failed = false;

                        //Copy to file stream...
                        using (Stream packageFileStream = addOn.Package.LoadFile("Manifest.xml"))
                            try
                            {
                                using (FileStream fs = new FileStream(Path.Combine(root, "Manifest.xml"), FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                                    packageFileStream.CopyTo(fs);
                            }
                            catch (Exception ex) { failed = true; Log("Failed to write AddOn Manifest. \r\n\t{0}", ex.Message); }

                        //Log
                        if (!failed) Log("Extracting AddOn Manifest...");
                    }
                }

            //Done
            Log($"Complete. Click \'Close\' to exit.");
        }

        private void addPackageButton_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Abide AddOn Packages (*.aao)|*.aao";
                openDlg.Title = "Add AddOn Package...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open)
            {
                //Prepare...
                AddOnPackageFile package = new AddOnPackageFile();
                package.DecompressData += Program.Package_DecompressData;

                //Load
                try { package.Load(filename); } catch { }

                //Extract
                try
                {
                    if (package.Entries.Count > 0 && package.Entries.ContainsFilename("Manifest.xml"))
                    {
                        //Load Manifest
                        AddOnManifest manifest = new AddOnManifest();
                        manifest.LoadXml(package.LoadFile("Manifest.xml"));

                        //Create
                        AddOn addOn = new AddOn(package, manifest);

                        //Add to list...
                        addOnsListBox.SetItemChecked(addOnsListBox.Items.Add(addOn), true);
                    }
                }
                catch (Exception) { }
            }
        }

        private void Log(string message, params object[] args)
        {
            //Append Line
            string line = $"[{DateTime.Now.ToShortTimeString()}] {{0}}{Environment.NewLine}";
            if (args != null) installLogRichTextBox.AppendText(string.Format(line, string.Format(message, args)));
            else installLogRichTextBox.AppendText(string.Format(line, message));

            //Goto new line
            installLogRichTextBox.Select(installLogRichTextBox.TextLength, 0);
            installLogRichTextBox.ScrollToCaret();
        }

        /// <summary>
        /// Represents a basic AddOn package instance.
        /// </summary>
        private class AddOn
        {
            /// <summary>
            /// Gets and returns the AddOn package file.
            /// </summary>
            public AddOnPackageFile Package
            {
                get { return package; }
            }
            /// <summary>
            /// Gets and returns the AddOn manifest.
            /// </summary>
            public AddOnManifest Manifest
            {
                get { return manifest; }
            }

            private readonly AddOnPackageFile package;
            private readonly AddOnManifest manifest;

            /// <summary>
            /// Initializes a new <see cref="AddOn"/> instance using the supplied package and AddOn manifest instances. 
            /// </summary>
            /// <param name="package">The AddOn package file.</param>
            /// <param name="manifest">The AddOn manifest.</param>
            public AddOn(AddOnPackageFile package, AddOnManifest manifest)
            {
                this.package = package;
                this.manifest = manifest;
            }
            /// <summary>
            /// Gets the name of the AddOn.
            /// </summary>
            /// <returns>A name of the AddOn.</returns>
            public override string ToString()
            {
                //Get Name
                string name = base.ToString();
                if (manifest != null && manifest.Name != null) name = manifest.Name;

                //Return
                return name;
            }
        }
    }
}
