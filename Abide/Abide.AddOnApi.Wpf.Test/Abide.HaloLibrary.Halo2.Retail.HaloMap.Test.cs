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
    public class HaloMapTest
    {
        [TestMethod]
        public void LoadTest()
        {
            using (HaloMap map = new HaloMap(@"C:\Users\mikem\Desktop\Shadow07 Help\coagulation - Original.map"))
            {
                map.Save(@"C:\Users\mikem\Desktop\Shadow07 Help\coagulation.map");
            }
        }
    }
}
