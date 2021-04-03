using Abide.Tag;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Abide.Wpf.Modules.Tag
{
    public sealed class AbideTagBlock : AbideTagObject
    {
        private readonly Block block;
        private string displayName = string.Empty;

        public ObservableCollection<AbideTagField> Fields { get; } = new ObservableCollection<AbideTagField>();
        public Block TagBlock => block;
        public override string Name => block.Name ?? base.Name;
        public string DisplayName
        {
            get => displayName;
            set
            {
                if (displayName != value)
                {
                    displayName = value ?? string.Empty;
                    NotifyPropertyChanged();
                }
            }
        }

        public AbideTagBlock(TagContext context, AbideTagObject owner, Block block) : base(context, owner)
        {
            this.block = block;
            foreach (var field in block.Fields)
            {
                var f = AbideTagField.CreateFromField(context, this, field);
                if (f != null)
                {
                    if (field.IsBlockName)
                    {
                        f.PropertyChanged += BlockNameField_PropertyChanged;
                    }

                    Fields.Add(f);
                }
            }

            UpdateDisplayName();
        }
        public void PostprocessTagBlock()
        {
            switch (block.Name)
            {
                case "scenario_scenery_block":
                    ((BlockIndexValueField)Fields[0]).Instructions = "U U U F 13";
                    ((BlockIndexValueField)Fields[1]).Instructions = "U U U F 11";
                    break;

                case "scenario_biped_block":
                    ((BlockIndexValueField)Fields[0]).Instructions = "U U U F 15";
                    ((BlockIndexValueField)Fields[1]).Instructions = "U U U F 11";
                    break;

                case "scenario_vehicle_block":
                    ((BlockIndexValueField)Fields[0]).Instructions = "U U U F 17";
                    ((BlockIndexValueField)Fields[1]).Instructions = "U U U F 11";
                    break;
            }

            foreach (var field in Fields)
            {
                field.PostProcessField();
            }
        }

        private void BlockNameField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateDisplayName();
        }
        private void UpdateDisplayName()
        {
            if (Fields.Any(f => f.IsTagBlockName))
            {
                DisplayName = string.Join(", ", Fields
                    .Where(f => f.IsTagBlockName)
                    .Select(f => f.GetValueString()));
            }
            else
            {
                DisplayName = Name;
            }
        }
    }
}
