using Abide.Guerilla.Library;
using Abide.Tag;
using Abide.Wpf.Modules.UI;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Abide.Wpf.Modules.Editors.Halo2
{
    public sealed class TagGroupViewModel : BaseViewModel
    {
        public static readonly DependencyProperty FilePathProperty = 
            DependencyProperty.Register(nameof(FilePath), typeof(string), typeof(TagGroupViewModel), new PropertyMetadata(FilePathPropertyChanged));

        private AbideTagGroupFile file = null;
        private Group tagGroup = null;

        public string FilePath
        {
            get => (string)GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }
        public Group TagGroup
        {
            get => tagGroup;
            private set
            {
                if (tagGroup != value)
                {
                    tagGroup = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TagGroupViewModel() { }

        private static void ConvertGroup(Group tagGroup)
        {
            for (int i = 0; i < tagGroup.TagBlockCount; i++)
            {
                ConvertBlock(tagGroup.TagBlocks[i]);
            }
        }
        private static void ConvertBlock(Block block)
        {
            switch (block.Name)
            {
                case "scenario_object_names_block":
                    break;

                case "scenario_scenery_block":
                    if (block.Fields[0] is BaseBlockIndexField scenarioSceneryTypeField)
                    {
                        scenarioSceneryTypeField.Instructions = "U U U F 13 BL";
                    }

                    if (block.Fields[1] is BaseBlockIndexField scenarioSceneryNameField)
                    {
                        scenarioSceneryNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_biped_block":
                    if (block.Fields[0] is BaseBlockIndexField scenarioBipedTypeField)
                    {
                        scenarioBipedTypeField.Instructions = "U U U F 15 BL";
                    }

                    if (block.Fields[1] is BaseBlockIndexField scenarioBipedNameField)
                    {
                        scenarioBipedNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_vehicle_block":
                    if (block.Fields[0] is BaseBlockIndexField scenarioVehicleTypeField)
                    {
                        scenarioVehicleTypeField.Instructions = "U U U F 17 BL";
                    }

                    if (block.Fields[1] is BaseBlockIndexField scenarioVehicleNameField)
                    {
                        scenarioVehicleNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_equipment_block":
                    if (block.Fields[0] is BaseBlockIndexField scenarioEquipmentTypeField)
                    {
                        scenarioEquipmentTypeField.Instructions = "U U U F 19 BL";
                    }

                    if (block.Fields[1] is BaseBlockIndexField scenarioEquipmentNameField)
                    {
                        scenarioEquipmentNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_weapons_block":
                    if (block.Fields[0] is BaseBlockIndexField scenarioWeaponsTypeField)
                    {
                        scenarioWeaponsTypeField.Instructions = "U U U F 21 BL";
                    }

                    if (block.Fields[1] is BaseBlockIndexField scenarioWeaponsNameField)
                    {
                        scenarioWeaponsNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "device_group_block":
                    if (block.Fields[0] is BaseBlockIndexField deviceGroupNameField)
                    {
                        deviceGroupNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_machine_block":
                    if (block.Fields[0] is BaseBlockIndexField scenarioMachineTypeField)
                    {
                        scenarioMachineTypeField.Instructions = "U U U F 24 BL";
                    }

                    if (block.Fields[1] is BaseBlockIndexField scenarioMachineNameField)
                    {
                        scenarioMachineNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_control_block":
                    if (block.Fields[0] is BaseBlockIndexField scenarioControlTypeField)
                    {
                        scenarioControlTypeField.Instructions = "U U U F 26 BL";
                    }

                    if (block.Fields[1] is BaseBlockIndexField scenarioControlNameField)
                    {
                        scenarioControlNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_light_fixture_block":
                    if (block.Fields[0] is BaseBlockIndexField scenarioLightFixtureTypeField)
                    {
                        scenarioLightFixtureTypeField.Instructions = "U U U F 28 BL";
                    }

                    if (block.Fields[1] is BaseBlockIndexField scenarioLightFixtureNameField)
                    {
                        scenarioLightFixtureNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_sound_scenery_block":
                    if (block.Fields[0] is BaseBlockIndexField scenarioSoundSceneryTypeField)
                    {
                        scenarioSoundSceneryTypeField.Instructions = "U U U F 30 BL";
                    }

                    if (block.Fields[1] is BaseBlockIndexField scenarioSoundSceneryNameField)
                    {
                        scenarioSoundSceneryNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_light_block":
                    if (block.Fields[1] is BaseBlockIndexField scenarioLightTypeField)
                    {
                        scenarioLightTypeField.Instructions = "U U U F 32 BL";
                    }

                    if (block.Fields[2] is BaseBlockIndexField scenarioLightNameField)
                    {
                        scenarioLightNameField.Instructions = "U U U F 11 BL";
                    }
                    break;

                case "scenario_trigger_volume_block":

                    break;

                case "weapon_barrels":
                    if (block.Fields[10] is BaseBlockIndexField weaponBarrelsMagazineField)
                    {
                        weaponBarrelsMagazineField.Instructions = "U U U F 66 BL";
                    }
                    break;

                case "weapon_triggers":
                    if (block.Fields[3] is BaseBlockIndexField weaponTriggersPrimaryBarrelField)
                    {
                        weaponTriggersPrimaryBarrelField.Instructions = "U U U F 68 BL";
                    }

                    if (block.Fields[4] is BaseBlockIndexField weaponTriggersSecondaryBarrelField)
                    {
                        weaponTriggersSecondaryBarrelField.Instructions = "U U U F 68 BL";
                    }
                    break;
            }

            for (int i = 0; i < block.FieldCount; i++)
            {
                if (block.Fields[i] is BlockField blockField)
                {
                    foreach (var tagBlock in blockField.BlockList)
                    {
                        ConvertBlock(tagBlock);
                    }

                    block.Fields[i] = new SelectableBlockField(blockField);
                }

                block.Fields[i].PropertyChanged += Field_PropertyChanged;
            }
        }
        private static void FilePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagGroupViewModel model)
            {
                if (e.NewValue is string filePath && File.Exists(filePath))
                {
                    model.file = new AbideTagGroupFile();
                    model.file.Load(filePath);
                    ConvertGroup(model.file.TagGroup);
                    model.TagGroup = model.file.TagGroup;
                }
            }
        }
        private static void Field_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Field field && e.PropertyName == nameof(Field.Value))
            {

            }
        }
    }
}
