using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;

namespace XbExplorer.Wpf.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private static readonly string DefaultSettingsFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XbExplorer", "settings.xml");
        public static SettingsViewModel Default { get; } = new SettingsViewModel(DefaultSettingsFileLocation);

        private static readonly DependencyPropertyKey XboxNamesPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(XboxNames), typeof(ObservableCollection<string>), typeof(SettingsViewModel), new PropertyMetadata(new ObservableCollection<string>()));
        public static readonly DependencyProperty RequiresUpgradeProperty =
            DependencyProperty.Register(nameof(RequiresUpgrade), typeof(bool), typeof(SettingsViewModel), new PropertyMetadata(true));
        public static readonly DependencyProperty XboxNamesProperty =
            XboxNamesPropertyKey.DependencyProperty;
        
        public string FileName { get; }
        public bool RequiresUpgrade
        {
            get { return (bool)GetValue(RequiresUpgradeProperty); }
            set { SetValue(RequiresUpgradeProperty, value); }
        }
        public ObservableCollection<string> XboxNames
        {
            get { return (ObservableCollection<string>)GetValue(XboxNamesProperty); }
            private set { SetValue(XboxNamesPropertyKey, value); }
        }

        public SettingsViewModel(string fileName)
        {
            FileName = fileName;
            Load();
        }
        public void Load()
        {
            if (File.Exists(FileName))
            {
                using (Stream fs = File.OpenRead(FileName))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(fs);

                    var settingsNode = document["Settings"];
                    if (settingsNode != null)
                    {
                        // Check assembly name
                        var assemblyNameAttribute = settingsNode.Attributes["AssemblyName"];
                        if (assemblyNameAttribute != null)
                        {
                            AssemblyName name = new AssemblyName(assemblyNameAttribute.InnerText);
                            if (name.Version == typeof(SettingsViewModel).Assembly.GetName().Version)
                            {
                                RequiresUpgrade = false;
                            }
                        }

                        // Get Xbox Names
                        var xboxNamesNode = settingsNode["XboxNames"];
                        if(xboxNamesNode != null)
                            foreach (XmlNode nameNode in xboxNamesNode.ChildNodes)
                            {
                                if (nameNode.Name == "Name")
                                {
                                    XboxNames.Add(nameNode.InnerText);
                                }
                            }
                    }
                }
            }
        }
        public void Upgrade()
        {
            //TODO: Implement upgrade procedure.

            RequiresUpgrade = false;
        }
        public void Save()
        {
            if (!Directory.Exists(Path.GetDirectoryName(FileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(FileName));

            XmlWriterSettings writerSettings = new XmlWriterSettings
            {
                Indent = true,
            };

            using (Stream fs = File.Create(FileName))
            using (XmlWriter writer = XmlWriter.Create(fs, writerSettings))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("Settings");
                writer.WriteStartAttribute("AssemblyName");
                writer.WriteValue(typeof(SettingsViewModel).Assembly.FullName);
                writer.WriteEndAttribute();

                writer.WriteStartElement("XboxNames");
                foreach (var name in XboxNames)
                {
                    writer.WriteStartElement("Name");
                    writer.WriteValue(name);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
        }
    }
}
