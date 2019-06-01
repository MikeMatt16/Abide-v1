using Abide.Tag;
using Abide.Tag.Definition;
using System;
using System.Collections.ObjectModel;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents an enum field container.
    /// </summary>
    public class EnumFieldModel : FieldModel
    {
        /// <summary>
        /// Gets or sets the zero-based index of the selected option.
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    NotifyPropertyChanged();

                    switch (TagField.Type)
                    {
                        case FieldType.FieldCharEnum:
                            Value = (byte)value;
                            break;
                        case FieldType.FieldEnum:
                            Value = (short)value;
                            break;
                        case FieldType.FieldLongEnum:
                            Value = (int)value;
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Gets and returns the list of options.
        /// </summary>
        public ObservableCollection<string> Options
        {
            get { return options; }
            private set
            {
                if (options != value)
                {
                    options = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> options = new ObservableCollection<string>();
        private int selectedIndex = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumFieldModel"/> class.
        /// </summary>
        public EnumFieldModel() { }
        /// <summary>
        /// Occurs when the tag field has been changed.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnTagFieldChanged(EventArgs e)
        {
            //Base procedures
            base.OnTagFieldChanged(e);

            //Check
            if (TagField == null) return;
            if (TagField is OptionField optionField)
            {
                //Create collection
                Options = new ObservableCollection<string>(optionField.GetOptions());

                //Handle
                switch (TagField.Type)
                {
                    case FieldType.FieldCharEnum: SelectedIndex = (byte)Value; break;
                    case FieldType.FieldEnum: SelectedIndex = (short)Value; break;
                    case FieldType.FieldLongEnum: SelectedIndex = (int)Value; break;
                }
            }
        }
    }
}
