using System;

namespace Abide.AddOnApi.AddOnTest
{
    [AddOn]
    public class TestAddOn : IAddOn
    {
        public string Name => "Test AddOn";
        public string Description => "AddOn test.";
        public string Author => "Click16";

        public TestAddOn() { }
        public void Dispose()
        {
            //Do nothing
        }
        public void Initialize(IHost host)
        {
            //Do nothing
            Console.WriteLine("Hello from AddOn!");
        }
    }
}
