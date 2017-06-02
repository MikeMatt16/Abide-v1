using Abide.AddOnApi;
using Abide.HaloLibrary;

namespace Abide.AddOnDemo
{
    internal static class HostExtensions
    {
        public static TAGID BrowseTag(this IHost host, IAddOn sender, TAGID id)
        {
            //Prepare
            TAGID selectedId = id;

            //Check
            if (host.InvokeRequired) host.Invoke(new BrowseTagInvoker(BrowseTag), new object[] { host, sender, id });
            else
            {
                object response = host.Request(sender, "TagBrowserDialog", selectedId);
                if (response != null && response is TAGID)
                    selectedId = (TAGID)response;
            }

            //Return
            return selectedId;
        }

        private delegate TAGID BrowseTagInvoker(IHost host, IAddOn sender, TAGID id);
    }
}
