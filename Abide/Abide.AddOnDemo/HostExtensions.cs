using Abide.AddOnApi;
using Abide.HaloLibrary;

namespace Abide.AddOnDemo
{
    internal static class HostExtensions
    {
        public static TagId BrowseTag(this IHost host, IAddOn sender, TagId id)
        {
            //Prepare
            TagId selectedId = id;

            //Check
            if (host.InvokeRequired) host.Invoke(new TagIdInvoker(BrowseTag), new object[] { host, sender, id });
            else
            {
                object response = host.Request(sender, "TagBrowserDialog", selectedId);
                if (response != null && response is TagId)
                    selectedId = (TagId)response;
            }

            //Return
            return selectedId;
        }
        public static TagId SelectEntry(this IHost host, IAddOn sender, TagId id)
        {
            //Prepare
            TagId selectedId = id;

            //Check
            if (host.InvokeRequired) host.Invoke(new TagIdInvoker(SelectEntry), new object[] { host, sender, id });
            else
            {
                object response = host.Request(sender, "SelectEntry", selectedId);
                if (response != null && response is TagId)
                    selectedId = (TagId)response;
            }

            //Return
            return selectedId;
        }

        private delegate TagId TagIdInvoker(IHost host, IAddOn sender, TagId id);
    }
}
