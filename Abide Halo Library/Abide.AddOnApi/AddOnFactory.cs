using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Abide.AddOnApi
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class AddOnFactory : MarshalByRefObject
    {
        /// <summary>
        /// Gets or sets the directory of the AddOn assemblies.
        /// </summary>
        public string AddOnDirectory
        {
            get { return addOnDirectory; }
            set { addOnDirectory = value; }
        }
        /// <summary>
        /// Gets and returns a dictionary whose Key is the assembly name, and value array contains the found <see cref="IAddOn"/>-containing types.
        /// </summary>
        public Dictionary<string, string[]> AddOns
        {
            get { return addOns; }
        }

        private readonly Dictionary<string, string[]> addOns;
        private string addOnDirectory;

        /// <summary>
        /// Initializes a new <see cref="AddOnFactory"/> instance.
        /// </summary>
        public AddOnFactory()
        {
            //Initialize
            addOns = new Dictionary<string, string[]>();

            //Setup Events
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_Resolve;
            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_Resolve;
        }
        /// <summary>
        /// Attempts to load any <see cref="IAddOn"/> exported types from the specified assembly file name.
        /// This method loads the assembly into memory, and does not lock the file.
        /// </summary>
        /// <param name="filename">The file name of the assembly.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filename"/> is null.</exception>
        /// <exception cref="FileNotFoundException"><paramref name="filename"/> is not a valid file name.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="filename"/> is not a valid assembly.</exception>
        public void LoadAssemblySafe(string filename)
        {
            //Check
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (!File.Exists(filename)) throw new FileNotFoundException("Cannot find the assembly file name specified.", filename);

            //Prepare
            byte[] buffer = null;
            Assembly assembly = null;

            try
            {
                //Open File
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    //Setup Buffer...
                    buffer = new byte[fs.Length];

                    //Read
                    fs.Read(buffer, 0, buffer.Length);
                }

                //Create Assembly
                assembly = Assembly.Load(buffer);
            }
            catch(Exception ex) { throw new InvalidOperationException("Unable to load assembly file.", ex); }

            //Check
            if (assembly == null) throw new InvalidOperationException("Invalid assembly file.", new ArgumentNullException(nameof(assembly)));

            //Load
            LoadAssembly(assembly);
        }
        /// <summary>
        /// Attempts to load any <see cref="IAddOn"/> exported types from the specified assembly file name.
        /// </summary>
        /// <param name="filename">The file path of the assembly.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filename"/> is null.</exception>
        /// <exception cref="FileNotFoundException"><paramref name="filename"/> is not a valid file name.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="filename"/> is not a file path to a valid assembly.</exception>
        public void LoadAssembly(string filename)
        {
            //Check
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (!File.Exists(filename)) throw new FileNotFoundException("Cannot find the assembly file name specified.", filename);

            //Prepare
            Assembly assembly = null;

            try { assembly = Assembly.LoadFile(filename); }
            catch (Exception ex) { throw new InvalidOperationException("Unable to load assembly file.", ex); }

            //Check
            if (assembly == null) throw new InvalidOperationException("Invalid assembly file.", new ArgumentNullException(nameof(assembly)));

            //Load
            LoadAssembly(assembly);
        }
        /// <summary>
        /// Attempts to load any <see cref="IAddOn"/> exported types from the supplied assembly.
        /// </summary>
        /// <param name="assembly">The assembly to retrieve <see cref="IAddOn"/> types from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is null.</exception>
        public void LoadAssembly(Assembly assembly)
        {
            //Check
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            //Prepare
            List<string> addOnTypes = new List<string>();
            string assemblyName = assembly.GetName().Name;

            //Loop
            foreach (Type type in assembly.GetExportedTypes())
                try
                {
                    if (type.GetInterface(typeof(IAddOn).Name) != null)
                        addOnTypes.Add(type.FullName);
                }
                catch (AmbiguousMatchException) { }

            //Check
            if (addOnTypes.Count > 0 && !addOns.ContainsKey(assemblyName))
                addOns.Add(assemblyName, addOnTypes.ToArray());
        }
        /// <summary>
        /// Attempts to create an instance of a supplied type from the given assembly name and type.
        /// </summary>
        /// <typeparam name="T">The <see cref="IAddOn"/> based type to retrieve.</typeparam>
        /// <param name="assembly">The assembly name that the type resides in.</param>
        /// <param name="type">The type's full name.</param>
        /// <returns>Null if the type cannot be created, else returns an instance of <typeparamref name="T"/>.</returns>
        public T CreateInstance<T>(string assembly, string type) where T : class, IAddOn
        {
            //Prepare
            T instance = null;

            try { instance = AppDomain.CurrentDomain.CreateInstance(assembly, type).Unwrap() as T; }
            catch (Exception) { }

            //Return
            return instance;
        }

        private Assembly CurrentDomain_Resolve(object sender, ResolveEventArgs args)
        {
            //Check
            if (!Directory.Exists(addOnDirectory)) return null;

            //Get Assembly Locations...
            string libraryLocation = Path.Combine(addOnDirectory, $"{args.Name}.dll");
            string executableLocation = Path.Combine(addOnDirectory, $"{args.Name}.exe");

            //Check
            if (File.Exists(libraryLocation)) return Assembly.LoadFrom(libraryLocation);
            else if (File.Exists(executableLocation)) return Assembly.LoadFrom(executableLocation);
            else return null;
        }
    }
}
