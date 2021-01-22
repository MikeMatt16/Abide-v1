using System.Windows.Input;

namespace Abide.Wpf.Modules.ViewModel
{
    public class HistoryModel : BaseViewModel
    {
        private ICommand undo, redo;
        public object State { get; set; }
        public string Name { get; set; }
        public ICommand UndoAction
        {
            get => undo;
            set
            {
                if (undo != value)
                {
                    undo = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ICommand RedoAction
        {
            get => redo;
            set
            {
                if (redo != value)
                {
                    redo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public HistoryModel(string name, object state)
        {
            Name = name;
            State = state;
        }
    }
}
