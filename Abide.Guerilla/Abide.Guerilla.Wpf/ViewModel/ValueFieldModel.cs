using Abide.HaloLibrary;
using Abide.Tag;
using Abide.Tag.Definition;
using System;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a value field container.
    /// </summary>
    public class ValueFieldModel : FieldModel
    {
        /// <summary>
        /// Gets or sets the width of the value input box.
        /// </summary>
        public int InputBoxWidth
        {
            get { return inputBoxWidth; }
            set
            {
                bool changed = inputBoxWidth != value;
                inputBoxWidth = value;
                if (changed) NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the value of the value field as a string.
        /// </summary>
        public new string Value
        {
            get
            {
                //Check
                if (TagField == null) return null;

                //Prepare
                string value = string.Empty;

                //Handle
                switch (TagField.Type)
                {
                    case FieldType.FieldString:
                        value = ((String32)base.Value).String;
                        break;
                    case FieldType.FieldLongString:
                        value = ((String256)base.Value).String;
                        break;
                    case FieldType.FieldTag:
                        value = ((TagFourCc)base.Value).FourCc;
                        break;
                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        value = (string)base.Value;
                        break;

                    case FieldType.FieldCharBlockIndex1:
                    case FieldType.FieldCharBlockIndex2:
                    case FieldType.FieldCharInteger:
                        value = Convert.ToString((byte)base.Value);
                        break;

                    case FieldType.FieldShortBlockIndex1:
                    case FieldType.FieldShortBlockIndex2:
                    case FieldType.FieldShortInteger:
                        value = Convert.ToString((short)base.Value);
                        break;

                    case FieldType.FieldLongBlockIndex1:
                    case FieldType.FieldLongBlockIndex2:
                    case FieldType.FieldLongInteger:
                        value = Convert.ToString((int)base.Value);
                        break;

                    case FieldType.FieldAngle:
                    case FieldType.FieldReal:
                    case FieldType.FieldRealFraction:
                        value = Convert.ToString((float)base.Value);
                        break;
                }

                //Return
                return value;
            }
            set
            {
                //Check
                if (TagField == null) return;

                //Handle
                switch (TagField.Type)
                {
                    case FieldType.FieldString:
                        base.Value = new String32() { String = value ?? string.Empty };
                        break;
                    case FieldType.FieldLongString:
                        base.Value = new String256() { String = value ?? string.Empty };
                        break;
                    case FieldType.FieldTag:
                        base.Value = new TagFourCc(value ?? string.Empty);
                        break;
                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        base.Value = value ?? string.Empty;
                        break;

                    case FieldType.FieldCharBlockIndex1:
                    case FieldType.FieldCharBlockIndex2:
                    case FieldType.FieldCharInteger:
                        if (byte.TryParse(value, out byte b))
                            base.Value = b;
                        break;

                    case FieldType.FieldShortBlockIndex1:
                    case FieldType.FieldShortBlockIndex2:
                    case FieldType.FieldShortInteger:
                        if (short.TryParse(value, out short s))
                            base.Value = s;
                        break;

                    case FieldType.FieldLongBlockIndex1:
                    case FieldType.FieldLongBlockIndex2:
                    case FieldType.FieldLongInteger:
                        if (int.TryParse(value, out int i))
                            base.Value = i;
                        break;

                    case FieldType.FieldAngle:
                    case FieldType.FieldReal:
                    case FieldType.FieldRealFraction:
                        if (float.TryParse(value, out float f))
                            base.Value = f;
                        break;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueFieldModel"/> class.
        /// </summary>
        public ValueFieldModel() { }

        private int inputBoxWidth = 80;
    }
}
