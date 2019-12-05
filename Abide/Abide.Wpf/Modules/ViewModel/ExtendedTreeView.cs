using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.ViewModel
{
    /// <summary>
    /// Provides some attached properties for <see cref="TreeView"/> elements.
    /// </summary>
    public static class ExtendedTreeView
    {
        private static Dictionary<DependencyObject, TreeViewBehavior> behaviors = new Dictionary<DependencyObject, TreeViewBehavior>();

        /// <summary>
        /// Identifies the <see cref="SelectedItem"/> attached property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(ExtendedTreeView), new PropertyMetadata(SelectedItemPropertyChanged));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static object GetSelectedItem(DependencyObject d)
        {
            //Return
            return d.GetValue(SelectedItemProperty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        public static void SetSelectedItem(DependencyObject d, object value)
        {
            //Set
            d.SetValue(SelectedItemProperty, value);
        }
        private static void SelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is TreeView treeView)
            {
                //Create behavior?
                if (!behaviors.ContainsKey(d))
                    behaviors.Add(d, new TreeViewBehavior(treeView));

                //Get behavior
                TreeViewBehavior behavior = behaviors[d];
                behavior.ChangeSelectedItem(e.NewValue);
            }
        }

        private sealed class TreeViewBehavior
        {
            public TreeView TreeView { get; }
            public TreeViewBehavior(TreeView treeView)
            {
                //Set tree view
                TreeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
                TreeView.SelectedItemChanged += TreeView_SelectedItemChanged;
            }
            private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
            {
                //Set selected item
                SetSelectedItem(TreeView, e.NewValue);
            }
            public void ChangeSelectedItem(object i)
            {
                TreeViewItem item = TreeView.ItemContainerGenerator.ContainerFromItem(i) as TreeViewItem;
                if (item != null) item.IsSelected = true;
            }
        }
    }
}
