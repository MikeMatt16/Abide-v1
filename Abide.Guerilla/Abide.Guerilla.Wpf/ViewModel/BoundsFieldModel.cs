using Abide.Tag;
using Abide.Tag.Definition;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a bounds field container.
    /// </summary>
    public class BoundsFieldModel : FieldModel
    {
        /// <summary>
        /// Gets or sets the minimum value of the bounds as a string.
        /// </summary>
        public string Min
        {
            get
            {
                //Check
                if (TagField == null) return null;

                //Prepare
                string value = string.Empty;

                //Handle
                switch(TagField.Type)
                {
                    case FieldType.FieldShortBounds:
                        ShortBounds shortBounds = (ShortBounds)Value;
                        value = shortBounds.Min.ToString();
                        break;
                    case FieldType.FieldRealFractionBounds:
                    case FieldType.FieldAngleBounds:
                    case FieldType.FieldRealBounds:
                        FloatBounds floatBounds = (FloatBounds)Value;
                        value = floatBounds.Min.ToString();
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
                    case FieldType.FieldShortBounds:
                        ShortBounds shortBounds = (ShortBounds)Value;
                        if (short.TryParse(value, out short s))
                            Value = new ShortBounds() { Min = s, Max = shortBounds.Max };
                        break;
                    case FieldType.FieldRealFractionBounds:
                    case FieldType.FieldAngleBounds:
                    case FieldType.FieldRealBounds:
                        FloatBounds floatBounds = (FloatBounds)Value;
                        if (float.TryParse(value, out float f))
                            Value = new FloatBounds() { Min = f, Max = floatBounds.Max };
                        break;
                }
            }
        }
        /// <summary>
        /// Gets or sets the maximum value of the bounds as a string.
        /// </summary>
        public string Max
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
                    case FieldType.FieldShortBounds:
                        ShortBounds shortBounds = (ShortBounds)Value;
                        value = shortBounds.Max.ToString();
                        break;
                    case FieldType.FieldRealFractionBounds:
                    case FieldType.FieldAngleBounds:
                    case FieldType.FieldRealBounds:
                        FloatBounds floatBounds = (FloatBounds)Value;
                        value = floatBounds.Max.ToString();
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
                    case FieldType.FieldShortBounds:
                        ShortBounds shortBounds = (ShortBounds)Value;
                        if (short.TryParse(value, out short s))
                            Value = new ShortBounds() { Min = shortBounds.Min, Max = s };
                        break;
                    case FieldType.FieldRealFractionBounds:
                    case FieldType.FieldAngleBounds:
                    case FieldType.FieldRealBounds:
                        FloatBounds floatBounds = (FloatBounds)Value;
                        if (float.TryParse(value, out float f))
                            Value = new FloatBounds() { Min = floatBounds.Min, Max = f };
                        break;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundsFieldModel"/> class.
        /// </summary>
        public BoundsFieldModel() { }
    }
}
