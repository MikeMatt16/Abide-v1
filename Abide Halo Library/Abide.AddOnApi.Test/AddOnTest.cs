using System;

namespace Abide.AddOnApi.Test
{
    public sealed class AddOnTest : IAddOn
    {
        public string Author
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Initialize(IHost host)
        {
            System.Windows.Forms.MessageBox.Show((string)host.Request(this, "test"));
        }
    }
}
