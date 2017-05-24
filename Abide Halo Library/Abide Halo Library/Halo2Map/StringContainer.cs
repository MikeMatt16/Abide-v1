using System;
using System.Collections.Generic;

namespace AbideHaloLibrary.Halo2Map
{
    /// <summary>
    /// Contains an enumeration of the Halo 2 string localizations.
    /// </summary>
    public enum StringLocale
    {
        /// <summary>
        /// English Localization. 
        /// </summary>
        English,
        /// <summary>
        /// Japanese Localization.
        /// </summary>
        Japanese,
        /// <summary>
        /// German Localization.
        /// </summary>
        German,
        /// <summary>
        /// French Localization.
        /// </summary>
        French,
        /// <summary>
        /// Spanish Localization.
        /// </summary>
        Spanish,
        /// <summary>
        /// Italian Localization.
        /// </summary>
        Italian,
        /// <summary>
        /// Korean Localization.
        /// </summary>
        Korean,
        /// <summary>
        /// Chinese Localization.
        /// </summary>
        Chinese,
        /// <summary>
        /// Portuguese Localiaztion.
        /// </summary>
        Portuguese
    }

    /// <summary>
    /// Represents a Halo 2 unicode string entry.
    /// </summary>
    [Serializable]
    public sealed class StringEntry : IEquatable<StringEntry>
    {
        /// <summary>
        /// The string entry's text.
        /// </summary>
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
        /// <summary>
        /// The identifier of this string entry.
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string value;
        private string id;

        /// <summary>
        /// Creates a new <see cref="StringEntry"/> instance using the supplied text string, and string identifier.
        /// </summary>
        /// <param name="value">The text of the string entry</param>
        /// <param name="id">The string identifier of this string entry.</param>
        public StringEntry(string value, string id)
        {
            this.value = value;
            this.id = id;
        }
        /// <summary>
        /// Creates a blank <see cref="StringEntry"/> instance.
        /// </summary>
        public StringEntry()
        {
            value = string.Empty;
            id = string.Empty;
        }
        /// <summary>
        /// Gets a string representation of this <see cref="StringEntry"/> instance.
        /// </summary>
        /// <returns>A string containing the string identifier and text.</returns>
        public override string ToString()
        {
            return string.Format("{0}: \"{1}\"", id, value);
        }
        /// <summary>
        /// Compares this instance and another <see cref="StringEntry"/> are equal. 
        /// </summary>
        /// <param name="other">The <see cref="StringEntry"/> to compare this to.</param>
        /// <returns>True if they are equal, false if not.</returns>
        public bool Equals(StringEntry other)
        {
            return value.Equals(other.value) && id.Equals(other.id);
        }
    }

    /// <summary>
    /// Represents a Halo 2 unicode string container.
    /// </summary>
    [Serializable]
    public sealed class StringContainer : IDisposable
    {
        /// <summary>
        /// Gets and returns the string entry list for a given localiztion.
        /// </summary>
        /// <param name="locale">The string localization.</param>
        /// <returns>A list of <see cref="StringEntry"/> elements containing the strings of the given locale.</returns>
        public List<StringEntry> this[StringLocale locale]
        {
            get
            {
                switch (locale)
                {
                    case StringLocale.English: return en;
                    case StringLocale.Japanese: return jp;
                    case StringLocale.German: return nl;
                    case StringLocale.French: return fr;
                    case StringLocale.Spanish: return es;
                    case StringLocale.Italian: return it;
                    case StringLocale.Korean: return kr;
                    case StringLocale.Chinese: return zh;
                    case StringLocale.Portuguese: return pr;
                    default: throw new ArgumentException("Locale does not exist.", "locale");
                }
            }
        }
        /// <summary>
        /// Gets and returns the English localized string entry list.
        /// </summary>
        public List<StringEntry> English
        {
            get { return en; }
        }
        /// <summary>
        /// Gets and returns the Japanese localized string entry list.
        /// </summary>
        public List<StringEntry> Japanese
        {
            get { return jp; }
        }
        /// <summary>
        /// Gets and returns the German localized string entry list.
        /// </summary>
        public List<StringEntry> German
        {
            get { return nl; }
        }
        /// <summary>
        /// Gets and returns the French localized string entry list.
        /// </summary>
        public List<StringEntry> French
        {
            get { return fr; }
        }
        /// <summary>
        /// Gets and returns the Spanish localized string entry list.
        /// </summary>
        public List<StringEntry> Spanish
        {
            get { return es; }
        }
        /// <summary>
        /// Gets and returns the Italian localized string entry list.
        /// </summary>
        public List<StringEntry> Italian
        {
            get { return it; }
        }
        /// <summary>
        /// Gets and returns the Korean localized string entry list.
        /// </summary>
        public List<StringEntry> Korean
        {
            get { return kr; }
        }
        /// <summary>
        /// Gets and returns the Chinese localized string entry list.
        /// </summary>
        public List<StringEntry> Chinese
        {
            get { return zh; }
        }
        /// <summary>
        /// Gets and returns the Portuguese localized string entry list.
        /// </summary>
        public List<StringEntry> Portuguese
        {
            get { return pr; }
        }

        private List<StringEntry> en;
        private List<StringEntry> jp;
        private List<StringEntry> nl;
        private List<StringEntry> fr;
        private List<StringEntry> es;
        private List<StringEntry> it;
        private List<StringEntry> kr;
        private List<StringEntry> zh;
        private List<StringEntry> pr;

        /// <summary>
        /// Initializes a new <see cref="StringContainer"/>.
        /// </summary>
        public StringContainer()
        {
            //Setup
            en = new List<StringEntry>();
            jp = new List<StringEntry>();
            nl = new List<StringEntry>();
            fr = new List<StringEntry>();
            es = new List<StringEntry>();
            it = new List<StringEntry>();
            kr = new List<StringEntry>();
            zh = new List<StringEntry>();
            pr = new List<StringEntry>();
        }

        /// <summary>
        /// Releases any resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Clear
            en.Clear();
            jp.Clear();
            nl.Clear();
            fr.Clear();
            es.Clear();
            it.Clear();
            kr.Clear();
            zh.Clear();
            pr.Clear();

            //Null
            en = null;
            jp = null;
            nl = null;
            fr = null;
            es = null;
            it = null;
            kr = null;
            zh = null;
            pr = null;
        }
    }
}
