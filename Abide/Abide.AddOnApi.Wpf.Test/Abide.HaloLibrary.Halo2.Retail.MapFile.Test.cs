using Abide.HaloLibrary.Halo2.Retail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.AddOnApi.Wpf.Test
{
    [TestClass]
    public class MapFileTest
    {
        [TestMethod]
        public void LoadTest()
        {
            using (MapFile file = new MapFile(@"F:\XBox\Original\Games\Halo 2\Clean Maps\ascension.map"))
            {
                // TODO: Add tests
            }
        }
    }
}
