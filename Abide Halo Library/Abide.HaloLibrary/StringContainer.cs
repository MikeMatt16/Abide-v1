using System;
using System.Collections;
using System.Collections.Generic;

namespace Abide.HaloLibrary
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
    public sealed class StringEntry : IEquatable<StringEntry>, ICloneable
    {
        /// <summary>
        /// The string entry's text.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// The identifier of this string entry.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Creates a new <see cref="StringEntry"/> instance using the supplied text string, and string identifier.
        /// </summary>
        /// <param name="value">The text of the string entry</param>
        /// <param name="id">The string identifier of this string entry.</param>
        public StringEntry(string value, string id)
        {
            Value = value;
            ID = id;
        }
        /// <summary>
        /// Creates a blank <see cref="StringEntry"/> instance.
        /// </summary>
        public StringEntry()
        {
            Value = string.Empty;
            ID = string.Empty;
        }
        /// <summary>
        /// Gets a string representation of this <see cref="StringEntry"/> instance.
        /// </summary>
        /// <returns>A string containing the string identifier and text.</returns>
        public override string ToString()
        {
            return string.Format("{0}: \"{1}\"", ID, Value);
        }
        /// <summary>
        /// Compares this instance and another <see cref="StringEntry"/> are equal. 
        /// </summary>
        /// <param name="other">The <see cref="StringEntry"/> to compare this to.</param>
        /// <returns>True if they are equal, false if not.</returns>
        public bool Equals(StringEntry other)
        {
            return Value.Equals(other.Value) && ID.Equals(other.ID);
        }
        /// <summary>
        /// Creates and returns a copy of this <see cref="StringEntry"/> object.
        /// </summary>
        /// <returns>A copy of this instance.</returns>
        public object Clone()
        {
            return new StringEntry(Value, ID);
        }
    }

    /// <summary>
    /// Represents a Halo 2 unicode string container.
    /// </summary>
    [Serializable]
    public sealed class StringContainer : IEnumerable<List<StringEntry>>
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
                    case StringLocale.English: return English;
                    case StringLocale.Japanese: return Japanese;
                    case StringLocale.German: return German;
                    case StringLocale.French: return French;
                    case StringLocale.Spanish: return Spanish;
                    case StringLocale.Italian: return Italian;
                    case StringLocale.Korean: return Korean;
                    case StringLocale.Chinese: return Chinese;
                    case StringLocale.Portuguese: return Portuguese;
                    default: throw new ArgumentException("Locale does not exist.", "locale");
                }
            }
        }
        /// <summary>
        /// Gets and returns the English localized string entry list.
        /// </summary>
        public List<StringEntry> English { get; private set; }
        /// <summary>
        /// Gets and returns the Japanese localized string entry list.
        /// </summary>
        public List<StringEntry> Japanese { get; private set; }
        /// <summary>
        /// Gets and returns the German localized string entry list.
        /// </summary>
        public List<StringEntry> German { get; private set; }
        /// <summary>
        /// Gets and returns the French localized string entry list.
        /// </summary>
        public List<StringEntry> French { get; private set; }
        /// <summary>
        /// Gets and returns the Spanish localized string entry list.
        /// </summary>
        public List<StringEntry> Spanish { get; private set; }
        /// <summary>
        /// Gets and returns the Italian localized string entry list.
        /// </summary>
        public List<StringEntry> Italian { get; private set; }
        /// <summary>
        /// Gets and returns the Korean localized string entry list.
        /// </summary>
        public List<StringEntry> Korean { get; private set; }
        /// <summary>
        /// Gets and returns the Chinese localized string entry list.
        /// </summary>
        public List<StringEntry> Chinese { get; private set; }
        /// <summary>
        /// Gets and returns the Portuguese localized string entry list.
        /// </summary>
        public List<StringEntry> Portuguese { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="StringContainer"/>.
        /// </summary>
        public StringContainer()
        {
            //Setup
            English = new List<StringEntry>();
            Japanese = new List<StringEntry>();
            German = new List<StringEntry>();
            French = new List<StringEntry>();
            Spanish = new List<StringEntry>();
            Italian = new List<StringEntry>();
            Korean = new List<StringEntry>();
            Chinese = new List<StringEntry>();
            Portuguese = new List<StringEntry>();
        }
        /// <summary>
        /// Copies the strings contained within this instance to another container.
        /// </summary>
        /// <param name="container">The container to copy the strings in this instance to.</param>
        public void CopyTo(StringContainer container)
        {
            //Check
            if (container == null) throw new ArgumentNullException(nameof(container));

            //Clone and add
            foreach (var entry in English) container.English.Add((StringEntry)entry.Clone());
            foreach (var entry in Japanese) container.Japanese.Add((StringEntry)entry.Clone());
            foreach (var entry in German) container.German.Add((StringEntry)entry.Clone());
            foreach (var entry in French) container.French.Add((StringEntry)entry.Clone());
            foreach (var entry in Spanish) container.Spanish.Add((StringEntry)entry.Clone());
            foreach (var entry in Italian) container.Italian.Add((StringEntry)entry.Clone());
            foreach (var entry in Korean) container.Korean.Add((StringEntry)entry.Clone());
            foreach (var entry in Chinese) container.Chinese.Add((StringEntry)entry.Clone());
            foreach (var entry in Portuguese) container.Portuguese.Add((StringEntry)entry.Clone());
        }
        /// <summary>
        /// Clears all string lists within this container.
        /// </summary>
        public void Clear()
        {
            English.Clear();
            Japanese.Clear();
            German.Clear();
            French.Clear();
            Spanish.Clear();
            Italian.Clear();
            Korean.Clear();
            Chinese.Clear();
            Portuguese.Clear();
        }
        /// <summary>
        /// Gets an enumerator that iterates the string container.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<List<StringEntry>> GetEnumerator()
        {
            yield return English;
            yield return Japanese;
            yield return German;
            yield return French;
            yield return Spanish;
            yield return Italian;
            yield return Korean;
            yield return Chinese;
            yield return Portuguese;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
