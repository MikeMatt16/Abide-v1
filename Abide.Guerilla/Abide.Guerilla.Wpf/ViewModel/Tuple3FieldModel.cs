using Abide.Tag;
using Abide.Tag.Definition;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a 3-tuple field container.
    /// </summary>
    public class Tuple3FieldModel : FieldModel
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
                    case FieldType.FieldRealPoint3D:
                    case FieldType.FieldEulerAngles3D:
                        return "x";

                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldRealPlane2D: return "i";

                    case FieldType.FieldRealRgbColor:
                    case FieldType.FieldRgbColor:
                        return "r";

                    case FieldType.FieldRealHsvColor:
                        return "h";
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
                    case FieldType.FieldRealPoint3D:
                    case FieldType.FieldEulerAngles3D:
                        return "y";

                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldRealPlane2D: return "j";

                    case FieldType.FieldRealRgbColor:
                    case FieldType.FieldRgbColor:
                        return "g";

                    case FieldType.FieldRealHsvColor:
                        return "v";
                }

                //Return
                return "b";
            }
        }
        /// <summary>
        /// Gets and returns the text for the third input.
        /// </summary>
        public string Text3
        {
            get
            {
                //Check
                if (TagField == null) return null;

                //Handle
                switch (TagField.Type)
                {
                    case FieldType.FieldRealPoint3D:
                    case FieldType.FieldEulerAngles3D:
                        return "z";

                    case FieldType.FieldRealVector3D: return "k";
                    case FieldType.FieldRealPlane2D: return "d";

                    case FieldType.FieldRealRgbColor:
                    case FieldType.FieldRgbColor:
                        return "b";

                    case FieldType.FieldRealHsvColor:
                        return "v";
                }

                //Return
                return "c";
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
                switch(TagField.Type)
                {
                    case FieldType.FieldEulerAngles3D:
                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldRealPlane2D:
                        Vector3 vector3 = (Vector3)Value;
                        value = vector3.I.ToString();
                        break;

                    case FieldType.FieldRealPoint3D:
                        Point3F point3 = (Point3F)Value;
                        value = point3.X.ToString();
                        break;

                    case FieldType.FieldRgbColor:
                        ColorRgb colorRgb = (ColorRgb)Value;
                        value = colorRgb.Red.ToString();
                        break;

                    case FieldType.FieldRealRgbColor:
                        ColorRgbF colorRgbF = (ColorRgbF)Value;
                        value = colorRgbF.Red.ToString();
                        break;

                    case FieldType.FieldRealHsvColor:
                        ColorHsv colorHsv = (ColorHsv)Value;
                        value = colorHsv.Hue.ToString();
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
                    case FieldType.FieldEulerAngles3D:
                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldRealPlane2D:
                        Vector3 vector3 = (Vector3)Value;
                        if (float.TryParse(value, out float vf))
                            Value = new Vector3(vf, vector3.J, vector3.K);
                        break;

                    case FieldType.FieldRealPoint3D:
                        Point3F point3 = (Point3F)Value;
                        if (float.TryParse(value, out float pf))
                            Value = new Point3F(pf, point3.Y, point3.Z);
                        break;

                    case FieldType.FieldRgbColor:
                        ColorRgb colorRgb = (ColorRgb)Value;
                        if (byte.TryParse(value, out byte b))
                            Value = new ColorRgb(b, colorRgb.Green, colorRgb.Blue);
                        break;

                    case FieldType.FieldRealRgbColor:
                        ColorRgbF colorRgbF = (ColorRgbF)Value;
                        if (float.TryParse(value, out float cf))
                            Value = new ColorRgbF(cf, colorRgbF.Green, colorRgbF.Blue);
                        break;

                    case FieldType.FieldRealHsvColor:
                        ColorHsv colorHsv = (ColorHsv)Value;
                        if (float.TryParse(value, out float hf))
                            Value = new ColorHsv(hf, colorHsv.Saturation, colorHsv.Value);
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
                    case FieldType.FieldEulerAngles3D:
                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldRealPlane2D:
                        Vector3 vector3 = (Vector3)Value;
                        value = vector3.J.ToString();
                        break;

                    case FieldType.FieldRealPoint3D:
                        Point3F point3 = (Point3F)Value;
                        value = point3.Y.ToString();
                        break;

                    case FieldType.FieldRgbColor:
                        ColorRgb colorRgb = (ColorRgb)Value;
                        value = colorRgb.Green.ToString();
                        break;

                    case FieldType.FieldRealRgbColor:
                        ColorRgbF colorRgbF = (ColorRgbF)Value;
                        value = colorRgbF.Green.ToString();
                        break;

                    case FieldType.FieldRealHsvColor:
                        ColorHsv colorHsv = (ColorHsv)Value;
                        value = colorHsv.Saturation.ToString();
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
                    case FieldType.FieldEulerAngles3D:
                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldRealPlane2D:
                        Vector3 vector3 = (Vector3)Value;
                        if (float.TryParse(value, out float vf))
                            Value = new Vector3(vector3.I, vf, vector3.K);
                        break;

                    case FieldType.FieldRealPoint3D:
                        Point3F point3 = (Point3F)Value;
                        if (float.TryParse(value, out float pf))
                            Value = new Point3F(point3.X, pf, point3.Z);
                        break;

                    case FieldType.FieldRgbColor:
                        ColorRgb colorRgb = (ColorRgb)Value;
                        if (byte.TryParse(value, out byte b))
                            Value = new ColorRgb(colorRgb.Red, b, colorRgb.Blue);
                        break;

                    case FieldType.FieldRealRgbColor:
                        ColorRgbF colorRgbF = (ColorRgbF)Value;
                        if (float.TryParse(value, out float cf))
                            Value = new ColorRgbF(colorRgbF.Red, cf, colorRgbF.Blue);
                        break;

                    case FieldType.FieldRealHsvColor:
                        ColorHsv colorHsv = (ColorHsv)Value;
                        if (float.TryParse(value, out float hf))
                            Value = new ColorHsv(colorHsv.Hue, hf, colorHsv.Value);
                        break;
                }
            }
        }
        /// <summary>
        /// Gets or sets the third value as a string.
        /// </summary>
        public string Value3
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
                    case FieldType.FieldEulerAngles3D:
                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldRealPlane2D:
                        Vector3 vector3 = (Vector3)Value;
                        value = vector3.K.ToString();
                        break;

                    case FieldType.FieldRealPoint3D:
                        Point3F point3 = (Point3F)Value;
                        value = point3.Z.ToString();
                        break;

                    case FieldType.FieldRgbColor:
                        ColorRgb colorRgb = (ColorRgb)Value;
                        value = colorRgb.Blue.ToString();
                        break;

                    case FieldType.FieldRealRgbColor:
                        ColorRgbF colorRgbF = (ColorRgbF)Value;
                        value = colorRgbF.Blue.ToString();
                        break;

                    case FieldType.FieldRealHsvColor:
                        ColorHsv colorHsv = (ColorHsv)Value;
                        value = colorHsv.Value.ToString();
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
                    case FieldType.FieldEulerAngles3D:
                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldRealPlane2D:
                        Vector3 vector3 = (Vector3)Value;
                        if (float.TryParse(value, out float vf))
                            Value = new Vector3(vector3.I, vector3.J, vf);
                        break;

                    case FieldType.FieldRealPoint3D:
                        Point3F point3 = (Point3F)Value;
                        if (float.TryParse(value, out float pf))
                            Value = new Point3F(point3.X, point3.Y, pf);
                        break;

                    case FieldType.FieldRgbColor:
                        ColorRgb colorRgb = (ColorRgb)Value;
                        if (byte.TryParse(value, out byte b))
                            Value = new ColorRgb(colorRgb.Red, colorRgb.Green, b);
                        break;

                    case FieldType.FieldRealRgbColor:
                        ColorRgbF colorRgbF = (ColorRgbF)Value;
                        if (float.TryParse(value, out float cf))
                            Value = new ColorRgbF(colorRgbF.Red, colorRgbF.Green, cf);
                        break;

                    case FieldType.FieldRealHsvColor:
                        ColorHsv colorHsv = (ColorHsv)Value;
                        if (float.TryParse(value, out float hf))
                            Value = new ColorHsv(colorHsv.Hue, colorHsv.Saturation, hf);
                        break;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple3FieldModel"/> class.
        /// </summary>
        public Tuple3FieldModel() { }
    }
}
