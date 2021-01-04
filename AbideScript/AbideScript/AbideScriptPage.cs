using AbideScript.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AbideScript
{
    public class AbideScriptPage : IDisposable
    {
        private AbideScriptElement[] members = new AbideScriptElement[0];
        private AbideScriptReader reader = null;

        public AbideScriptPage(string fileName)
        {
            using var stream = File.OpenText(fileName);
            string script = stream.ReadToEnd();
            reader = AbideScriptReader.Create(new StringReader(script));

            List<AbideScriptStatement> statements = new List<AbideScriptStatement>();
            while (!reader.EndOfStream)
            {
                statements.Add(reader.ReadStatement());
            }
        }
        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
