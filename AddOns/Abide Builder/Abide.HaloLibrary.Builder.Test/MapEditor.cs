using Abide.HaloLibrary.Halo2Map;
using System;

namespace Abide.HaloLibrary.Builder.Test
{
    /// <summary>
    /// Represents a map editor.
    /// </summary>
    public class MapEditor : IProgram
    {
        private readonly MapFile mapFile;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MapEditor"/> class.
        /// </summary>
        /// <param name="mapFile">The map file.</param>
        public MapEditor(MapFile mapFile)
        {
            //Setup
            this.mapFile = mapFile ?? throw new ArgumentNullException(nameof(mapFile));
        }

        public void Exit()
        {
        }

        public void Start()
        {
            //Clear
            Console.Clear();

            //Write
            Console.WriteLine("Welcome to Map Editor.");
        }

        public void OnInput(string input, params string[] args)
        {
            //Check
            switch (input)
            {
                default: Console.WriteLine($"Unknown command {input}."); break;
            }
        }
    }
}
