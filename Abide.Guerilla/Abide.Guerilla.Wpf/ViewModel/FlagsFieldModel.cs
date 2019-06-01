using Abide.Tag;
using Abide.Tag.Definition;
using System;
using System.Collections.ObjectModel;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a flags field container.
    /// </summary>
    public class FlagsFieldModel : FieldModel
    {
        /// <summary>
        /// Gets or sets the flags collection.
        /// </summary>
        public ObservableCollection<FlagOptionModel> Flags { get; } = new ObservableCollection<FlagOptionModel>();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagsFieldModel"/> class.
        /// </summary>
        public FlagsFieldModel() { }
        /// <summary>
        /// Occurs when the tag field has been changed.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnTagFieldChanged(EventArgs e)
        {
            //Prepare
            FlagOptionModel flagOption = null;

            //Base procedures
            base.OnTagFieldChanged(e);

            //Check
            if (TagField != null && TagField is BaseFlagsField flagsField)
                foreach (var flag in flagsField.Options)
                {
                    flagOption = new FlagOptionModel(flag.Name) { Toggle = flagsField.HasFlag(flag) };
                    flagOption.FlagChanged += FlagOption_FlagChanged;
                    Flags.Add(flagOption);
                }
        }

        private void FlagOption_FlagChanged(object sender, FlagChangedEventArgs e)
        {
            //Prepare
            Option option = null;

            //Check
            if (TagField != null && TagField is BaseFlagsField flagsField && sender is FlagOptionModel flagOptionModel && Flags.Contains(flagOptionModel))
            {
                //Get index
                int flagIndex = Flags.IndexOf(flagOptionModel);
                option = flagsField.Options[flagIndex];

                //Check
                if(e.Toggle != flagsField.HasFlag(option))
                {
                    //Set
                    flagsField.SetFlag(option, e.Toggle);
                    Owner.IsDirty = true;
                }
            }
        }
    }

    /// <summary>
    /// Represents a basic flag option.
    /// </summary>
    public class FlagOptionModel : NotifyPropertyChangedViewModel
    {
        /// <summary>
        /// Occurs when the flag state has been changed.
        /// </summary>
        public event FlagChangedEventHandler FlagChanged;
        /// <summary>
        /// Gets or sets the toggle state of the flag.
        /// </summary>
        public bool Toggle
        {
            get { return toggle; }
            set
            {
                if (toggle != value)
                {
                    toggle = value;
                    NotifyPropertyChanged();

                    //Create event arguments
                    FlagChangedEventArgs e = new FlagChangedEventArgs(value);

                    //Call method and raise event
                    OnFlagChanged(e);
                    FlagChanged?.Invoke(this, e);
                }
            }
        }
        /// <summary>
        /// Gets or sets the name of the flag.
        /// </summary>
        public string Name
        {
            get { return name.Name; }
        }
        /// <summary>
        /// Gets or sets the flag tooltip.
        /// </summary>
        public string ToolTip
        {
            get { return name.Information; }
        }
        
        private readonly ObjectName name;
        private bool toggle = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagOptionModel"/> class.
        /// </summary>
        public FlagOptionModel(string name)
        {
            this.name = new ObjectName(name);
        }
        /// <summary>
        /// Occurs when the toggle state of the flag has been changed.
        /// </summary>
        /// <param name="e">The <see cref="FlagChangedEventArgs"/> that contains the event data.</param>
        protected virtual void OnFlagChanged(FlagChangedEventArgs e)
        {
            //Do nothing
        }
    }

    /// <summary>
    /// Represents a method that handles a flag changed event.
    /// </summary>
    /// <param name="sender">The object that caused the event.</param>
    /// <param name="e">The <see cref="FlagChangedEventArgs"/> that contains the event data.</param>
    public delegate void FlagChangedEventHandler(object sender, FlagChangedEventArgs e);

    /// <summary>
    /// Represents a class that contains event data for a flag changed event.
    /// </summary>
    public class FlagChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets and returns the new state of the flag.
        /// </summary>
        public bool Toggle { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagChangedEventArgs"/> class using the specified toggle value.
        /// </summary>
        /// <param name="flag">The flag that was was changed.</param>
        /// <param name="toggle">The new state of the flag.</param>
        public FlagChangedEventArgs(bool toggle)
        {
            Toggle = toggle;
        }
    }
}
