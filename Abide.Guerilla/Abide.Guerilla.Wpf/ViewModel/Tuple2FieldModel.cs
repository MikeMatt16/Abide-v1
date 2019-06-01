using Abide.Tag;
using Abide.Tag.Definition;

namespace Abide.Guerilla.Wpf.ViewModel
{
    public class Tuple2FieldModel : FieldModel
    {
        /// <summary>
        /// Gets and returns the text for the first input.
        /// </summary>
        public string Text1
        {
            get
            {
                //Check
                if (TagField == null) return null;

                //Handle
                switch (TagField.Type)
                {
                    case FieldType.FieldPoint2D:
                    case FieldType.FieldRealPoint2D:
                        return "x";
                }

                //Return
                return "a";
            }
        }
        /// <summary>
        /// Gets and returns the text for the second input.
        /// </summary>
        public string Text2
        {
            get
            {
                //Check
                if (TagField == null) return null;

                //Handle
                switch (TagField.Type)
                {
                    case FieldType.FieldPoint2D:
                    case FieldType.FieldRealPoint2D:
                        return "y";

                }

                //Return
                return "b";
            }
        }
        /// <summary>
        /// Gets or sets the first value as a string.
        /// </summary>
        public string Value1
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
                    case FieldType.FieldPoint2D:
                        Point2 point2 = (Point2)Value;
                        value = point2.X.ToString();
                        break;
                    case FieldType.FieldRealPoint2D:
                        Point2F point2F = (Point2F)Value;
                        value = point2F.X.ToString();
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
                    case FieldType.FieldPoint2D:
                        Point2 point2 = (Point2)Value;
                        if (short.TryParse(value, out short s))
                            point2.X = s;
                        Value = point2;
                        break;
                    case FieldType.FieldRealPoint2D:
                        Point2F point2F = (Point2F)Value;
                        if (float.TryParse(value, out float f))
                            point2F.X = f;
                        Value = point2F;
                        break;
                }
            }
        }
        /// <summary>
        /// Gets or sets the second value as a string.
        /// </summary>
        public string Value2
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
                    case FieldType.FieldPoint2D:
                        Point2 point2 = (Point2)Value;
                        value = point2.Y.ToString();
                        break;
                    case FieldType.FieldRealPoint2D:
                        Point2F point2F = (Point2F)Value;
                        value = point2F.Y.ToString();
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
                    case FieldType.FieldPoint2D:
                        Point2 point2 = (Point2)Value;
                        if (short.TryParse(value, out short s))
                            point2.Y = s;
                        Value = point2;
                        break;
                    case FieldType.FieldRealPoint2D:
                        Point2F point2F = (Point2F)Value;
                        if (float.TryParse(value, out float f))
                            point2F.Y = f;
                        Value = point2F;
                        break;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple2FieldModel"/> class.
        /// </summary>
        public Tuple2FieldModel() { }
    }
}
