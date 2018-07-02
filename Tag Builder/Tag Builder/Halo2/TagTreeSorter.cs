using Abide.HaloLibrary.Halo2Map;
using System.Collections;
using System.Windows.Forms;

namespace Abide.TagBuilder.Halo2
{
    internal class TagTreeViewSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is TreeNode a && y is TreeNode b)
                return CompareNodes(a, b);
            else return 0;
        }
        public int CompareNodes(TreeNode a, TreeNode b)
        {
            if (a.Tag is IndexEntry && b.Tag == null)
                return 1;
            else if (a.Tag == null && b.Tag is IndexEntry)
                return -1;
            else return a.Text.CompareTo(b.Text);
        }
    }
}
