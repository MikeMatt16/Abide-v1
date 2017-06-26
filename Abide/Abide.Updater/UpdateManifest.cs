using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Abide.Updater
{
    public sealed class UpdateManifest : IEnumerable<AssemblyInformation>
    {
        private const string AbideUpdaterUrl = @"http://zaidware.com/michael.mattera/PotentialSoftware/Abide2/Updater.exe";

        public int Count
        {
            get { return informationList.Count; }
        }
        public DateTime Release
        {
            get { return release; }
            set { release = value; }
        }
        public string ReleaseNotes
        {
            get { return releaseNotes; }
            set { releaseNotes = value ?? string.Empty; }
        }
        public string PackageUrl
        {
            get { return packageUrl; }
            set { packageUrl = value; }
        }
        public string UpdaterUrl
        {
            get { return updaterUrl; }
        }
        public AssemblyInformation this[int index]
        {
            get { return informationList[index]; }
            set { informationList[index] = value; }
        }
        
        private DateTime release;
        private string releaseNotes;
        private string packageUrl;
        private string updaterUrl;
        private readonly List<AssemblyInformation> informationList;

        public UpdateManifest()
        {
            //Prepare
            release = DateTime.Now;
            informationList = new List<AssemblyInformation>();
            updaterUrl = AbideUpdaterUrl;
            releaseNotes = string.Empty;
            packageUrl = string.Empty;
        }
        internal UpdateManifest(XmlDocument document) : this()
        {
            //Get Manifest Node
            XmlNode manifest = document["UpdateManifest"];

            //Check...
            if (manifest != null)
            {
                //Get Release
                DateTime.TryParse(manifest["Release"]?.InnerText, out release);

                //Get Notes
                releaseNotes = manifest["Notes"]?.InnerText ?? string.Empty;

                //Get Url
                packageUrl = manifest["PackageUrl"]?.InnerText ?? string.Empty;

                //Get Updater
                updaterUrl = manifest["UpdaterUrl"]?.InnerText ?? string.Empty;

                //Get List
                informationList.Clear();
                foreach (XmlNode infoNode in manifest["AssemblyList"]?.ChildNodes)
                    informationList.Add(new AssemblyInformation(infoNode));
            }
        }
        public void Add(string filename, AssemblyName name)
        {
            informationList.Add(new AssemblyInformation() { Filename = filename, AssemblyName = name });
        }
        internal void SaveXml(XmlWriter writer)
        {
            //Start Document
            writer.WriteStartDocument();

            //Write Update Manifest
            writer.WriteStartElement("UpdateManifest");

            //Write Release
            writer.WriteStartElement("Release"); writer.WriteValue(release); writer.WriteEndElement();

            //Write Notes
            writer.WriteStartElement("Notes"); writer.WriteValue(releaseNotes); writer.WriteEndElement();

            //Write Updater Url
            writer.WriteStartElement("UpdaterUrl"); writer.WriteValue(UpdaterUrl); writer.WriteEndElement();

            //Write Package Url
            writer.WriteStartElement("PackageUrl"); writer.WriteValue(packageUrl); writer.WriteEndElement();

            //Write Assembly List
            writer.WriteStartElement("AssemblyList"); foreach (var info in informationList) info.SaveXml(writer); writer.WriteEndElement();

            //End Update Manifest
            writer.WriteEndElement();

            //End Document
            writer.WriteEndDocument();

            //Flush
            writer.Flush();
        }
        public override string ToString()
        {
            string value = null;
            try { value = $"{release} Count: {informationList.Count}"; }
            catch { value = base.ToString(); }
            return value;
        }
        public IEnumerator<AssemblyInformation> GetEnumerator()
        {
            return ((IEnumerable<AssemblyInformation>)informationList).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<AssemblyInformation>)informationList).GetEnumerator();
        }
    }

    public struct AssemblyInformation
    {
        public string Filename
        {
            get { return originalFilename; }
            set { originalFilename = value; }
        }
        public AssemblyName AssemblyName
        {
            get { if (string.IsNullOrEmpty(assemblyName)) return null; return new AssemblyName(assemblyName); }
            set { assemblyName = value?.FullName ?? string.Empty; }
        }

        private string originalFilename;
        private string assemblyName;
        
        internal AssemblyInformation(XmlNode node) : this()
        {
            originalFilename = node["FileName"]?.InnerText ?? string.Empty;
            assemblyName = node["AssemblyName"]?.InnerText ?? string.Empty;
        }
        internal void SaveXml(XmlWriter writer)
        {
            writer.WriteStartElement("File");
            writer.WriteStartElement("FileName"); writer.WriteValue(originalFilename); writer.WriteEndElement();
            writer.WriteStartElement("AssemblyName"); writer.WriteValue(assemblyName); writer.WriteEndElement();
            writer.WriteEndElement();
        }
        public override string ToString()
        {
            string value = null;
            try { value = $"{originalFilename} {assemblyName ?? "null"}"; }
            catch { value = base.ToString(); }
            return value;
        }
    }
}
