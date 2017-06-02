using Abide.AddOnApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Abide.Classes
{
    /// <summary>
    /// Represents an AddOn factory manager.
    /// </summary>
    public sealed class AddOnManager
    {
        private readonly Dictionary<string, AddOnFactory> factories;

        /// <summary>
        /// Initializes a new <see cref="AddOnManager"/>
        /// </summary>
        public AddOnManager()
        {
            //Initialize
            factories = new Dictionary<string, AddOnFactory>();
        }
        /// <summary>
        /// Retrieves all <see cref="AddOnFactory"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="AddOnFactory"/> loaded by the container.</returns>
        public AddOnFactory[] GetFactories()
        {
            //Get Factories
            AddOnFactory[] factories = new AddOnFactory[this.factories.Count];
            this.factories.Values.CopyTo(factories, 0);
            return factories;
        }
        /// <summary>
        /// Adds an assembly to the instance without locking the source assembly file.
        /// </summary>
        /// <param name="filename">The file path to the assembly.</param>
        public void AddAssemblySafe(string filename)
        {
            //Prepare
            AddOnFactory factory = null;
            string directory = Path.GetDirectoryName(filename);
            if (Directory.Exists(directory))
            {
                //Create or get factory...
                if (!factories.ContainsKey(directory))
                {
                    //Create
                    factory = new AddOnFactory() { AddOnDirectory = directory };
                    factories.Add(directory, factory);
                }
                else factory = factories[directory];

                //Load Assembly
                try { factory.LoadAssemblySafe(filename); }
                catch (Exception ex) { Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace); }
            }
        }
        /// <summary>
        /// Adds an assembly to the instance.
        /// </summary>
        /// <param name="filename">The file path to the assembly.</param>
        public void AddAssembly(string filename)
        {
            //Prepare
            AddOnFactory factory = null;
            string directory = Path.GetDirectoryName(filename);

            //Create or get factory...
            if (!factories.ContainsKey(directory))
            {
                //Create
                factory = new AddOnFactory() { AddOnDirectory = directory };
                factories.Add(directory, factory);
            }
            else factory = factories[directory];

            //Load Assembly
            try { factory.LoadAssembly(filename); }
            catch (Exception ex) { Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace); }
        }
        /// <summary>
        /// Adds an assembly to the instance.
        /// </summary>
        /// <param name="assembly">The assembly to add.</param>
        /// <param name="directory">The directory of the assembly.</param>
        public void AddAssembly(Assembly assembly, string directory)
        {
            //Prepare
            AddOnFactory factory = null;

            //Create or get factory...
            if (!factories.ContainsKey(directory))
            {
                //Create
                factory = new AddOnFactory() { AddOnDirectory = directory };
                factories.Add(directory, factory);
            }
            else factory = factories[directory];

            //Load Assembly
            try { factory.LoadAssembly(assembly); }
            catch (Exception ex) { Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace); }
        }
    }
}
