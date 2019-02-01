using Abide.Tag;
using Abide.Tag.Definition;
using Abide.Tag.Guerilla;
using Abide.Tag.Ui.Guerilla.Controls;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Abide.Guerilla
{
    public static partial class Tags
    {
        public static void GenerateControls(FlowLayoutPanel panel, ITagBlock block)
        {
            //Suspend
            panel.SuspendLayout();

            //Loop
            foreach (Field field in block.Fields)
            {
                //Prepare
                Control control = null;

                //Handle
                switch (field.Type)
                {
                    case FieldType.FieldExplanation: control = new ExplanationControl((ExplanationField)field); break;
                    case FieldType.FieldBlock: control = new BlockControl((BaseBlockField)field); break;
                    case FieldType.FieldStruct: control = new StructControl((BaseStructField)field); break;

                    case FieldType.FieldString:
                    case FieldType.FieldLongString:
                        control = new StringControl(field);
                        break;

                    case FieldType.FieldTagReference:
                        control = new TagReferenceControl(field);
                        break;

                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        control = new StringControl(field);
                        break;

                    case FieldType.FieldCharInteger:
                    case FieldType.FieldShortInteger:
                    case FieldType.FieldLongInteger:
                    case FieldType.FieldAngle:
                    case FieldType.FieldTag:
                    case FieldType.FieldReal:
                    case FieldType.FieldRealFraction:
                        control = new ValueControl(field);
                        break;

                    case FieldType.FieldCharEnum:
                        control = new EnumControl()
                        {
                            Field = field,
                            Title = field.Name,
                            Information = field.Information,
                            Details = field.Details,
                            Options = ((CharEnumField)field).Options.Select(o => o.Name).ToArray(),
                            Value = field.Value,
                            ValueChanged = EnumControl_ValueChanged
                        };
                        break;
                    case FieldType.FieldEnum:
                        control = new EnumControl()
                        {
                            Field = field,
                            Title = field.Name,
                            Information = field.Information,
                            Details = field.Details,
                            Options = ((EnumField)field).Options.Select(o => o.Name).ToArray(),
                            Value = field.Value,
                            ValueChanged = EnumControl_ValueChanged
                        };
                        break;
                    case FieldType.FieldLongEnum:
                        control = new EnumControl()
                        {
                            Field = field,
                            Title = field.Name,
                            Information = field.Information,
                            Details = field.Details,
                            Options = ((LongEnumField)field).Options.Select(o => o.Name).ToArray(),
                            Value = field.Value,
                            ValueChanged = EnumControl_ValueChanged
                        };
                        break;

                    case FieldType.FieldLongFlags:
                        control = new FlagsControl()
                        {
                            Field = field,
                            Title = field.Name,
                            Information = field.Information,
                            Options = ((LongFlagsField)field).Options.Select(o => o.Name).ToArray(),
                            Details = field.Details,
                            Value = field.Value,
                            ValueChanged = FlagsControl_ValueChanged
                        };
                        break;
                    case FieldType.FieldWordFlags:
                        control = new FlagsControl()
                        {
                            Field = field,
                            Title = field.Name,
                            Information = field.Information,
                            Options = ((WordFlagsField)field).Options.Select(o => o.Name).ToArray(),
                            Details = field.Details,
                            Value = field.Value,
                            ValueChanged = FlagsControl_ValueChanged
                        };
                        break;
                    case FieldType.FieldByteFlags:
                        control = new FlagsControl()
                        {
                            Field = field,
                            Title = field.Name,
                            Information = field.Information,
                            Options = ((ByteFlagsField)field).Options.Select(o => o.Name).ToArray(),
                            Details = field.Details,
                            Value = field.Value,
                            ValueChanged = FlagsControl_ValueChanged
                        };
                        break;

                    case FieldType.FieldShortBounds:
                        control = new RangeControl()
                        {
                            Field = field,
                            Title = field.Name,
                            RangeValue = new string[] { ((ShortBounds)field.Value).Min.ToString(), ((ShortBounds)field.Value).Max.ToString() }
                        };
                        break;
                    case FieldType.FieldAngleBounds:
                    case FieldType.FieldRealBounds:
                    case FieldType.FieldRealFractionBounds:
                        control = new RangeControl()
                        {
                            Field = field,
                            Title = field.Name,
                            RangeValue = new string[] { ((FloatBounds)field.Value).Min.ToString(), ((FloatBounds)field.Value).Max.ToString() }
                        };
                        break;

                    case FieldType.FieldPoint2D:
                        control = new TwoTupleControl()
                        {
                            LabelA = "x",
                            LabelB = "y",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Point2)field.Value).X.ToString(), ((Point2)field.Value).Y.ToString() }
                        };
                        break;
                    case FieldType.FieldRealPoint2D:
                        control = new TwoTupleControl()
                        {
                            LabelA = "x",
                            LabelB = "y",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Point2F)field.Value).X.ToString(), ((Point2F)field.Value).Y.ToString() }
                        };
                        break;
                    case FieldType.FieldRealPoint3D:
                        control = new ThreeTupleControl()
                        {
                            LabelA = "x",
                            LabelB = "y",
                            LabelC = "z",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Point3F)field.Value).X.ToString(), ((Point3F)field.Value).Y.ToString(), ((Point3F)field.Value).Z.ToString() }
                        };
                        break;
                    case FieldType.FieldRealVector2D:
                        control = new TwoTupleControl()
                        {
                            LabelA = "i",
                            LabelB = "j",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Vector2)field.Value).I.ToString(), ((Vector2)field.Value).J.ToString() }
                        };
                        break;
                    case FieldType.FieldRealVector3D:
                        control = new ThreeTupleControl()
                        {
                            LabelA = "i",
                            LabelB = "j",
                            LabelC = "k",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Vector3)field.Value).I.ToString(), ((Vector3)field.Value).J.ToString(), ((Vector3)field.Value).K.ToString() }
                        };
                        break;


                    case FieldType.FieldRectangle2D:
                        control = new FourTupleControl()
                        {
                            LabelA = "t",
                            LabelB = "l",
                            LabelC = "r",
                            LabelD = "b",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Rectangle2)field.Value).Top.ToString(), ((Rectangle2)field.Value).Left.ToString(),
                                ((Rectangle2)field.Value).Right.ToString(), ((Rectangle2)field.Value).Bottom.ToString() }
                        };
                        break;

                    case FieldType.FieldRgbColor:
                        control = new ThreeTupleControl()
                        {
                            LabelA = "r",
                            LabelB = "g",
                            LabelC = "b",
                            Field = field,
                            Title = field.Name,
                        };
                        break;
                    case FieldType.FieldArgbColor:
                        control = new FourTupleControl()
                        {
                            LabelA = "a",
                            LabelB = "r",
                            LabelC = "g",
                            LabelD = "b",
                            Field = field,
                            Title = field.Name,
                        };
                        break;
                    case FieldType.FieldRealRgbColor:
                        control = new ThreeTupleControl()
                        {
                            LabelA = "r",
                            LabelB = "g",
                            LabelC = "b",
                            Field = field,
                            Title = field.Name,
                        };
                        break;
                    case FieldType.FieldRealArgbColor:
                        control = new FourTupleControl()
                        {
                            LabelA = "a",
                            LabelB = "r",
                            LabelC = "g",
                            LabelD = "b",
                            Field = field,
                            Title = field.Name,
                        };
                        break;
                    case FieldType.FieldRealHsvColor:
                        control = new ThreeTupleControl()
                        {
                            LabelA = "h",
                            LabelB = "s",
                            LabelC = "v",
                            Field = field,
                            Title = field.Name,
                        };
                        break;
                    case FieldType.FieldRealAhsvColor:
                        control = new FourTupleControl()
                        {
                            LabelA = "a",
                            LabelB = "h",
                            LabelC = "s",
                            LabelD = "v",
                            Field = field,
                            Title = field.Name,
                        };
                        break;

                    case FieldType.FieldQuaternion:
                        control = new FourTupleControl()
                        {
                            LabelA = "w",
                            LabelB = "i",
                            LabelC = "j",
                            LabelD = "k",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Quaternion)field.Value).W.ToString(), ((Quaternion)field.Value).I.ToString(),
                                ((Quaternion)field.Value).J.ToString(), ((Quaternion)field.Value).K.ToString() }
                        };
                        break;

                    case FieldType.FieldEulerAngles2D:
                        control = new TwoTupleControl()
                        {
                            LabelA = "i",
                            LabelB = "j",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Vector2)field.Value).I.ToString(), ((Vector2)field.Value).J.ToString() }
                        };
                        break;
                    case FieldType.FieldEulerAngles3D:
                        control = new ThreeTupleControl()
                        {
                            LabelA = "i",
                            LabelB = "j",
                            LabelC = "k",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Vector3)field.Value).I.ToString(), ((Vector3)field.Value).J.ToString(), ((Vector3)field.Value).K.ToString() }
                        };
                        break;

                    case FieldType.FieldRealPlane2D:
                        control = new ThreeTupleControl()
                        {
                            LabelA = "i",
                            LabelB = "j",
                            LabelC = "d",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Vector3)field.Value).I.ToString(), ((Vector3)field.Value).J.ToString(), ((Vector3)field.Value).K.ToString() }
                        };
                        break;

                    case FieldType.FieldRealPlane3D:
                        control = new FourTupleControl()
                        {
                            LabelA = "i",
                            LabelB = "j",
                            LabelC = "k",
                            LabelD = "d",
                            Field = field,
                            Title = field.Name,
                            TupleValue = new string[] { ((Vector4)field.Value).I.ToString(), ((Vector4)field.Value).J.ToString(), ((Vector4)field.Value).K.ToString(), ((Vector4)field.Value).W.ToString() }
                        };
                        break;


                    default: control = new GuerillaControl() { Visible = false }; break;
                }

                //Check
                if (control != null) panel.Controls.Add(control);
            }

            //Resume
            panel.ResumeLayout();
        }
        
        private static void FlagsControl_ValueChanged(object sender, EventArgs e)
        {
            return;
            throw new NotImplementedException();
        }

        private static void EnumControl_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
