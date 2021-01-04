using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Abide.Wpf.Modules.Windows
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public ObservableCollection<TestNode> Nodes { get; } = new ObservableCollection<TestNode>();

        public Test()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                TestNode node1 = new TestNode($"Level 1 Node {i}");
                for (int j = 0; j < 4; j++)
                {
                    TestNode node2 = new TestNode($"Level 2 Node {i}");
                    for (int k = 0; k < 3; k++)
                    {
                        TestNode node3 = new TestNode($"Level 3 Node {i}");
                        for (int l = 0; l < 2; l++)
                        {
                            TestNode node4 = new TestNode($"Level 4 Node {i}");
                            node3.Nodes.Add(node4);
                        }

                        node2.Nodes.Add(node3);
                    }

                    node1.Nodes.Add(node2);
                }

                Nodes.Add(node1);
            }

            DataContext = Nodes;
        }
    }

    public class TestNode : INotifyPropertyChanged
    {
        private string name = string.Empty;

        public ObservableCollection<TestNode> Nodes { get; } = new ObservableCollection<TestNode>();
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TestNode() { }
        public TestNode(string name)
        {
            Name = name;
        }
    }
}
