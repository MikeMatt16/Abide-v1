using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Represents a factory for creating and managing AddOn instances.
    /// This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class AddOnFactory : MarshalByRefObject
    {
        /// <summary>
        /// Gets or sets the AddOn base directory of this instance.
        /// </summary>
        public string AddOnDirectory
        {
            get { return addOnDirectory; }
            set { addOnDirectory = value; }
        }

        private string addOnDirectory = string.Empty;
        private readonly List<Type> addOns;
        private readonly Dictionary<string, Assembly> assemblyLookup;
        private readonly bool safeMode = false;
        
        /// <summary>
        /// Initializes a new <see cref="AddOnFactory"/> instance.
        /// </summary>
        public AddOnFactory()
        {
            //Initialize
            addOns = new List<Type>();
            assemblyLookup = new Dictionary<string, Assembly>();

            //Setup Events
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_Resolve;
            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_Resolve;
        }
        /// <summary>
        /// Initializes a new AddOnFactory instance using the specified safe-mode value.
        /// </summary>
        /// <param name="safeMode">True to load assemblies safely (loading into memory) or not. False will lock the source files.</param>
        public AddOnFactory(bool safeMode)
        {
            //Initialize
            addOns = new List<Type>();
            assemblyLookup = new Dictionary<string, Assembly>();
            this.safeMode = safeMode;

            //Setup Events
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_Resolve;
            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_Resolve;
        }
        /// <summary>
        /// Returns all found types that implement the <see cref="IAddOn"/> interface.
        /// </summary>
        /// <returns>An array of <see cref="Type"/> instances.</returns>
        public Type[] GetAddOnTypes()
        {
            return addOns.ToArray();
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

            //Loop
            foreach (Type type in assembly.GetExportedTypes())
                try
                {
                    if (type.GetInterface(typeof(IAddOn).Name) != null)
                    { addOns.Add(type); }
                }
                catch (AmbiguousMatchException) { }
        }
        /// <summary>
        /// Attempts to create an instance of a supplied type.
        /// </summary>
        /// <typeparam name="T">The <see cref="IAddOn"/> based type to retrieve.</typeparam>
        /// <param name="type">The type to instantiate.</param>
        /// <returns>Null if the type cannot be created, else returns an instance of <typeparamref name="T"/>.</returns>
        public T CreateInstance<T>(Type type) where T : class, IAddOn
        {
            //Prepare
            T instance = null;

            try { instance = AppDomain.CurrentDomain.CreateInstance(type.Assembly.GetName().Name, type.FullName) as T; }
            catch (Exception) { }

            //Return
            return instance;
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
            //Prepare
            Assembly assembly = null;
            
            //Get Assembly Locations...
            string libraryLocation = Path.Combine(addOnDirectory, $"{args.Name}.dll");
            string executableLocation = Path.Combine(addOnDirectory, $"{args.Name}.exe");

            //Check
            if (!assemblyLookup.ContainsKey(args.Name))
            {
                if (File.Exists(libraryLocation))
                {
                    if (safeMode) assemblyLookup.Add(args.Name, assembly_LoadFromSafe(libraryLocation));
                    else assemblyLookup.Add(args.Name, assembly_LoadFrom(libraryLocation));
                }
                else if (File.Exists(executableLocation))
                {
                    if (safeMode) assemblyLookup.Add(args.Name, assembly_LoadFromSafe(executableLocation));
                    else assemblyLookup.Add(args.Name, assembly_LoadFrom(executableLocation));
                }
            }

            //Get
            if (assemblyLookup.ContainsKey(args.Name))
                assembly = assemblyLookup[args.Name];

            //Return
            return assembly;
        }
        private Assembly assembly_LoadFromSafe(string filename)
        {
            //Prepare
            Assembly assembly = null;
            byte[] buffer = null;

            //Check
            if (File.Exists(filename))
                if (safeMode)
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

            //Return
            return assembly;
        }
        private Assembly assembly_LoadFrom(string filename)
        {
            return Assembly.LoadFile(filename);
        }
    }
}
