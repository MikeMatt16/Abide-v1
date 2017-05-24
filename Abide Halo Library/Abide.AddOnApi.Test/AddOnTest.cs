namespace Abide.AddOnApi.Test
{
    public sealed class AddOnTest : IAddOn
    {
        public void Initialize(IHost host)
        {
            System.Windows.Forms.MessageBox.Show((string)host.Request("test"));
        }
    }
}
