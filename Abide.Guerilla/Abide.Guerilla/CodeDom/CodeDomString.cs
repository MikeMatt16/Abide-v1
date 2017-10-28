using System;
using System.Collections.Generic;
using System.Text;

namespace Abide.Guerilla.CodeDom
{
    /// <summary>
    /// Helper methods for strings for use with CodeDom.
    /// </summary>
    internal static class CodeDomString
    {
        private static readonly char[] NameSplitCharArray = new char[] { ' ', '_', '-', ',', '.' };

        /// <summary>
        /// Removes or replaces any troublesome characters from a given string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A filtered name string.</returns>
        public static string FilterName(this string name)
        {
            //Check
            if (string.IsNullOrWhiteSpace(name)) return "empty string";

            //Continue
            string formattedName = name;
            if (formattedName.Contains("^")) formattedName = formattedName.Substring(0, formattedName.IndexOf('^'));
            if (formattedName.Contains("#")) formattedName = formattedName.Substring(0, formattedName.IndexOf('#'));
            if (formattedName.Contains(":")) formattedName = formattedName.Substring(0, formattedName.IndexOf(':'));
            //if (formattedName.Contains("*")) formattedName = formattedName.Substring(0, formattedName.IndexOf('*'));
            formattedName = formattedName.Replace("*", string.Empty);       //*
            formattedName = formattedName.Replace("&", " and ");            //&
            formattedName = formattedName.Replace(">", " greater than ");   // >
            formattedName = formattedName.Replace("<", " less than ");      // <
            formattedName = formattedName.Replace("=", " equals ");         // =
            formattedName = formattedName.Replace("\"", string.Empty);      // "
            formattedName = formattedName.Replace("\'", string.Empty);      // '
            formattedName = formattedName.Replace(".", string.Empty);       // .
            formattedName = formattedName.Replace(",", string.Empty);       // ,            
            formattedName = formattedName.Replace("+", string.Empty);       // +
            formattedName = formattedName.Replace("*", string.Empty);       // *
            formattedName = formattedName.Replace("/", string.Empty);       // /
            formattedName = formattedName.Replace("\\", string.Empty);      // \
            formattedName = formattedName.Replace("|", string.Empty);       // |
            formattedName = formattedName.Replace("`", string.Empty);       // `
            formattedName = formattedName.Replace("~", string.Empty);       // ~
            formattedName = formattedName.Replace("(", string.Empty);       // (
            formattedName = formattedName.Replace(")", string.Empty);       // )
            formattedName = formattedName.Replace("[", string.Empty);       // [
            formattedName = formattedName.Replace("]", string.Empty);       // ]
            formattedName = formattedName.Replace("{", string.Empty);       // {
            formattedName = formattedName.Replace("}", string.Empty);       // }
            formattedName = formattedName.Replace("?", string.Empty);       // ?
            formattedName = formattedName.Replace("!", string.Empty);       // !
            return formattedName;
        }

        /// <summary>
        /// Converts the string's casing to PascalCasing.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A pascal-cased string.</returns>
        public static string ToPascalCase(this string name)
        {
            //Check
            if (string.IsNullOrEmpty(name)) return string.Empty;

            //Prepare
            StringBuilder builder = new StringBuilder();
            string[] parts = name.Split(NameSplitCharArray, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                StringBuilder partBuilder = new StringBuilder(part.ToLower());
                partBuilder[0] = part.ToUpper()[0];
                builder.Append(partBuilder.ToString());
            }

            //Return
            return builder.ToString();
        }

        /// <summary>
        /// Convers the string's casing to camelCasing.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A camel-cased string.</returns>
        public static string ToCamelCase(this string name)
        {
            //Check
            if (string.IsNullOrEmpty(name)) return string.Empty;

            //Prepare
            StringBuilder builder = new StringBuilder();
            string[] parts = name.Split(NameSplitCharArray, StringSplitOptions.RemoveEmptyEntries);

            builder.Append(parts[0].ToLower());
            for (int i = 1; i < parts.Length; i++)
            {
                StringBuilder partBuilder = new StringBuilder(parts[i].ToLower());
                partBuilder[0] = parts[i].ToUpper()[0];
                builder.Append(partBuilder.ToString());
            }

            //Return
            return builder.ToString();
        }

        /// <summary>
        /// Converts the string to a valid camel-cased member.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="members">The name of the members within the same code-space as <paramref name="name"/>.</param>
        /// <returns>A formatted name string.</returns>
        public static string ToCamelCasedMember(this string name, ICollection<string> members)
        {
            //Format name
            string formattedName = name.FilterName().ToCamelCase();
            if (string.IsNullOrEmpty(formattedName)) formattedName = "emptyString";

            //Check...
            StringBuilder builder = new StringBuilder(formattedName);
            if (char.IsNumber(builder[0])) builder.Insert(0, '_');
            formattedName = builder.ToString();

            //Loop
            int cloneCount = 0;
            while (members.Contains(formattedName))
            {
                cloneCount++;
                formattedName = builder.ToString() + cloneCount.ToString();
            }

            //Return
            return formattedName;
        }

        /// <summary>
        /// Converts the string to a valid pascal-cased member.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="members">The name of the members within the same code-space as <paramref name="name"/>.</param>
        /// <returns>A formatted name string.</returns>
        public static string ToPascalCasedMember(this string name, ICollection<string> members)
        {
            //Format name
            string formattedName = name.FilterName().ToPascalCase();
            if (string.IsNullOrEmpty(formattedName)) formattedName = "EmptyString";

            //Check...
            StringBuilder builder = new StringBuilder(formattedName);
            if (char.IsNumber(builder[0])) builder.Insert(0, '_');
            formattedName = builder.ToString();

            //Loop
            int cloneCount = 0;
            while (members.Contains(formattedName))
            {
                cloneCount++;
                formattedName = builder.ToString() + cloneCount.ToString();
            }

            //Return
            return formattedName;
        }
    }
}
