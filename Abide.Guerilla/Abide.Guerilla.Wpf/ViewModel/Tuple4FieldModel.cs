using Abide.Tag;
using Abide.Tag.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Tuple4FieldModel : FieldModel
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
                    case FieldType.FieldRealPlane3D: return "i";
                    case FieldType.FieldRectangle2D: return "t";
                    case FieldType.FieldQuaternion: return "w";
                    case FieldType.FieldArgbColor:
                    case FieldType.FieldRealArgbColor:
                        return "a";
                    case FieldType.FieldRealAhsvColor:
                        return "a";
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
                    case FieldType.FieldRealPlane3D: return "j";
                    case FieldType.FieldRectangle2D: return "l";
                    case FieldType.FieldQuaternion: return "i";
                    case FieldType.FieldArgbColor:
                    case FieldType.FieldRealArgbColor:
                        return "r";
                    case FieldType.FieldRealAhsvColor:
                        return "h";
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
                    case FieldType.FieldRealPlane3D: return "k";
                    case FieldType.FieldRectangle2D: return "r";
                    case FieldType.FieldQuaternion: return "j";
                    case FieldType.FieldArgbColor:
                    case FieldType.FieldRealArgbColor:
                        return "g";
                    case FieldType.FieldRealAhsvColor:
                        return "s";
                }

                //Return
                return "c";
            }
        }
        /// <summary>
        /// Gets and returns the text for the fourth input.
        /// </summary>
        public string Text4
        {
            get
            {
                //Check
                if (TagField == null) return null;

                //Handle
                switch (TagField.Type)
                {
                    case FieldType.FieldRealPlane3D: return "d";
                    case FieldType.FieldRectangle2D: return "b";
                    case FieldType.FieldQuaternion: return "k";
                    case FieldType.FieldArgbColor:
                    case FieldType.FieldRealArgbColor:
                        return "b";
                    case FieldType.FieldRealAhsvColor:
                        return "v";
                }

                //Return
                return "d";
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
                    case FieldType.FieldRectangle2D:
                        Rectangle2 rectangle2 = (Rectangle2)Value;
                        value = rectangle2.Top.ToString();
                        break;
                    case FieldType.FieldQuaternion:
                        Quaternion quaternion = (Quaternion)Value;
                        value = quaternion.W.ToString();
                        break;
                    case FieldType.FieldRealPlane3D:
                        Vector4 vector4 = (Vector4)Value;
                        value = vector4.I.ToString();
                        break;
                    case FieldType.FieldArgbColor:
                        ColorArgb colorArgb = (ColorArgb)Value;
                        value = colorArgb.Alpha.ToString();
                        break;
                    case FieldType.FieldRealArgbColor:
                        ColorArgbF colorArgbF = (ColorArgbF)Value;
                        value = colorArgbF.Alpha.ToString();
                        break;
                    case FieldType.FieldRealAhsvColor:
                        ColorAhsv colorAhsv = (ColorAhsv)Value;
                        value = colorAhsv.Alpha.ToString();
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
                    case FieldType.FieldRectangle2D:
                        Rectangle2 rectangle2 = (Rectangle2)Value;
                        if (short.TryParse(value, out short r))
                            rectangle2.Top = r;
                        Value = rectangle2;
                        break;
                    case FieldType.FieldQuaternion:
                        Quaternion quaternion = (Quaternion)Value;
                        if (float.TryParse(value, out float q))
                            quaternion.W = q;
                        Value = quaternion;
                        break;
                    case FieldType.FieldRealPlane3D:
                        Vector4 vector4 = (Vector4)Value;
                        if (float.TryParse(value, out float v))
                            vector4.I = v;
                        Value = vector4;
                        break;
                    case FieldType.FieldArgbColor:
                        ColorArgb colorArgb = (ColorArgb)Value;
                        if (byte.TryParse(value, out byte cb))
                            colorArgb.Alpha = cb;
                        Value = colorArgb;
                        break;
                    case FieldType.FieldRealArgbColor:
                        ColorArgbF colorArgbF = (ColorArgbF)Value;
                        if (float.TryParse(value, out float cf))
                            colorArgbF.Alpha = cf;
                        Value = colorArgbF;
                        break;
                    case FieldType.FieldRealAhsvColor:
                        ColorAhsv colorAhsv = (ColorAhsv)Value;
                        if (float.TryParse(value, out float hf))
                            colorAhsv.Alpha = hf;
                        Value = colorAhsv;
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
                    case FieldType.FieldRectangle2D:
                        Rectangle2 rectangle2 = (Rectangle2)Value;
                        value = rectangle2.Left.ToString();
                        break;
                    case FieldType.FieldQuaternion:
                        Quaternion quaternion = (Quaternion)Value;
                        value = quaternion.I.ToString();
                        break;
                    case FieldType.FieldRealPlane3D:
                        Vector4 vector4 = (Vector4)Value;
                        value = vector4.J.ToString();
                        break;
                    case FieldType.FieldArgbColor:
                        ColorArgb colorArgb = (ColorArgb)Value;
                        value = colorArgb.Red.ToString();
                        break;
                    case FieldType.FieldRealArgbColor:
                        ColorArgbF colorArgbF = (ColorArgbF)Value;
                        value = colorArgbF.Red.ToString();
                        break;
                    case FieldType.FieldRealAhsvColor:
                        ColorAhsv colorAhsv = (ColorAhsv)Value;
                        value = colorAhsv.Hue.ToString();
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
                    case FieldType.FieldRectangle2D:
                        Rectangle2 rectangle2 = (Rectangle2)Value;
                        if (short.TryParse(value, out short r))
                            rectangle2.Left = r;
                        Value = rectangle2;
                        break;
                    case FieldType.FieldQuaternion:
                        Quaternion quaternion = (Quaternion)Value;
                        if (float.TryParse(value, out float q))
                            quaternion.I = q;
                        Value = quaternion;
                        break;
                    case FieldType.FieldRealPlane3D:
                        Vector4 vector4 = (Vector4)Value;
                        if (float.TryParse(value, out float v))
                            vector4.J = v;
                        Value = vector4;
                        break;
                    case FieldType.FieldArgbColor:
                        ColorArgb colorArgb = (ColorArgb)Value;
                        if (byte.TryParse(value, out byte cb))
                            colorArgb.Red = cb;
                        Value = colorArgb;
                        break;
                    case FieldType.FieldRealArgbColor:
                        ColorArgbF colorArgbF = (ColorArgbF)Value;
                        if (float.TryParse(value, out float cf))
                            colorArgbF.Red = cf;
                        Value = colorArgbF;
                        break;
                    case FieldType.FieldRealAhsvColor:
                        ColorAhsv colorAhsv = (ColorAhsv)Value;
                        if (float.TryParse(value, out float hf))
                            colorAhsv.Hue = hf;
                        Value = colorAhsv;
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
                    case FieldType.FieldRectangle2D:
                        Rectangle2 rectangle2 = (Rectangle2)Value;
                        value = rectangle2.Bottom.ToString();
                        break;
                    case FieldType.FieldQuaternion:
                        Quaternion quaternion = (Quaternion)Value;
                        value = quaternion.J.ToString();
                        break;
                    case FieldType.FieldRealPlane3D:
                        Vector4 vector4 = (Vector4)Value;
                        value = vector4.K.ToString();
                        break;
                    case FieldType.FieldArgbColor:
                        ColorArgb colorArgb = (ColorArgb)Value;
                        value = colorArgb.Green.ToString();
                        break;
                    case FieldType.FieldRealArgbColor:
                        ColorArgbF colorArgbF = (ColorArgbF)Value;
                        value = colorArgbF.Green.ToString();
                        break;
                    case FieldType.FieldRealAhsvColor:
                        ColorAhsv colorAhsv = (ColorAhsv)Value;
                        value = colorAhsv.Saturation.ToString();
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
                    case FieldType.FieldRectangle2D:
                        Rectangle2 rectangle2 = (Rectangle2)Value;
                        if (short.TryParse(value, out short r))
                            rectangle2.Bottom = r;
                        Value = rectangle2;
                        break;
                    case FieldType.FieldQuaternion:
                        Quaternion quaternion = (Quaternion)Value;
                        if (float.TryParse(value, out float q))
                            quaternion.J = q;
                        Value = quaternion;
                        break;
                    case FieldType.FieldRealPlane3D:
                        Vector4 vector4 = (Vector4)Value;
                        if (float.TryParse(value, out float v))
                            vector4.K = v;
                        Value = vector4;
                        break;
                    case FieldType.FieldArgbColor:
                        ColorArgb colorArgb = (ColorArgb)Value;
                        if (byte.TryParse(value, out byte cb))
                            colorArgb.Green = cb;
                        Value = colorArgb;
                        break;
                    case FieldType.FieldRealArgbColor:
                        ColorArgbF colorArgbF = (ColorArgbF)Value;
                        if (float.TryParse(value, out float cf))
                            colorArgbF.Green = cf;
                        Value = colorArgbF;
                        break;
                    case FieldType.FieldRealAhsvColor:
                        ColorAhsv colorAhsv = (ColorAhsv)Value;
                        if (float.TryParse(value, out float hf))
                            colorAhsv.Saturation = hf;
                        Value = colorAhsv;
                        break;
                }
            }
        }
        /// <summary>
        /// Gets or sets the fourth value as a string.
        /// </summary>
        public string Value4
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
                    case FieldType.FieldRectangle2D:
                        Rectangle2 rectangle2 = (Rectangle2)Value;
                        value = rectangle2.Right.ToString();
                        break;
                    case FieldType.FieldQuaternion:
                        Quaternion quaternion = (Quaternion)Value;
                        value = quaternion.K.ToString();
                        break;
                    case FieldType.FieldRealPlane3D:
                        Vector4 vector4 = (Vector4)Value;
                        value = vector4.W.ToString();
                        break;
                    case FieldType.FieldArgbColor:
                        ColorArgb colorArgb = (ColorArgb)Value;
                        value = colorArgb.Blue.ToString();
                        break;
                    case FieldType.FieldRealArgbColor:
                        ColorArgbF colorArgbF = (ColorArgbF)Value;
                        value = colorArgbF.Blue.ToString();
                        break;
                    case FieldType.FieldRealAhsvColor:
                        ColorAhsv colorAhsv = (ColorAhsv)Value;
                        value = colorAhsv.Value.ToString();
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
                    case FieldType.FieldRectangle2D:
                        Rectangle2 rectangle2 = (Rectangle2)Value;
                        if (short.TryParse(value, out short r))
                            rectangle2.Right = r;
                        Value = rectangle2;
                        break;
                    case FieldType.FieldQuaternion:
                        Quaternion quaternion = (Quaternion)Value;
                        if (float.TryParse(value, out float q))
                            quaternion.K = q;
                        Value = quaternion;
                        break;
                    case FieldType.FieldRealPlane3D:
                        Vector4 vector4 = (Vector4)Value;
                        if (float.TryParse(value, out float v))
                            vector4.W = v;
                        Value = vector4;
                        break;
                    case FieldType.FieldArgbColor:
                        ColorArgb colorArgb = (ColorArgb)Value;
                        if (byte.TryParse(value, out byte cb))
                            colorArgb.Blue = cb;
                        Value = colorArgb;
                        break;
                    case FieldType.FieldRealArgbColor:
                        ColorArgbF colorArgbF = (ColorArgbF)Value;
                        if (float.TryParse(value, out float cf))
                            colorArgbF.Blue = cf;
                        Value = colorArgbF;
                        break;
                    case FieldType.FieldRealAhsvColor:
                        ColorAhsv colorAhsv = (ColorAhsv)Value;
                        if (float.TryParse(value, out float hf))
                            colorAhsv.Value = hf;
                        Value = colorAhsv;
                        break;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple4FieldModel"/> class.
        /// </summary>
        public Tuple4FieldModel() { }

    }
}
