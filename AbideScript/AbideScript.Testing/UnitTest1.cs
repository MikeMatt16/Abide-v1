using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AbideScript.Testing
{
    public class Tests
    {
        public const string ScriptFile = @"C:\Users\mikem\Desktop\AbideScript test.as";
        private AbideScriptPage page = null;

        [SetUp]
        public void Setup()
        {
            page = new AbideScriptPage(ScriptFile);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
            Debugger.Break();
        }
    }
}