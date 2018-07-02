using Abide.Tag.Cache;
using Abide.Tag.Definition;
using Abide.Tag.Ui.Guerilla.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla
{
    public static class Tags
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
                    case FieldType.FieldExplanation:
                        control = new ExplanationControl()
                        {
                            Title = field.Name,
                            Explanation = ((ExplanationField)field).Explanation
                        };
                        break;

                    case FieldType.FieldBlock:
                        control = new BlockControl()
                        {
                            Title = field.Name,
                            List = ((BaseBlockField)field).BlockList
                        };
                        break;

                    case FieldType.FieldStruct:
                        if (field.Value is Block child)
                            GenerateControls(panel, child);
                        break;

                    case FieldType.FieldString:
                    case FieldType.FieldLongString:
                        control = new StringControl()
                        {
                            Field = field,
                            Title = field.Name,
                            IsReadOnly = field.IsReadOnly,
                            StringValue = field.Value.ToString(),
                            ValueChanged = StringControl_ValueChanged
                        };
                        break;

                    case FieldType.FieldTagReference:
                        control = new TagReferenceControl()
                        {
                            Field = field,
                            Title = field.Name,
                            IsReadOnly = field.IsReadOnly,
                            RefernceValue = (TagReference)field.Value,
                            ValueChanged = TagReference_ValueChanged
                        };
                        break;

                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        break;

                    case FieldType.FieldCharInteger:
                    case FieldType.FieldShortInteger:
                    case FieldType.FieldLongInteger:
                    case FieldType.FieldAngle:
                    case FieldType.FieldTag:
                    case FieldType.FieldReal:
                    case FieldType.FieldRealFraction:
                        control = new ValueControl()
                        {
                            Field = field,
                            Title = field.Name,
                            IsReadOnly = field.IsReadOnly,
                            Information = field.Information,
                            Details = field.Details,
                            Value = field.Value.ToString(),
                            ValueChanged = ValueControl_ValueChanged
                        };
                        break;

                    case FieldType.FieldCharEnum:
                        control = new EnumControl()
                        {
                            Field = field,
                            Title = field.Name,
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
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
                            IsReadOnly = field.IsReadOnly,
                            TupleValue = new string[] { ((Vector4)field.Value).I.ToString(), ((Vector4)field.Value).J.ToString(), ((Vector4)field.Value).K.ToString(), ((Vector4)field.Value).W.ToString() }
                        };
                        break;


                    case FieldType.FieldLongBlockFlags:
                        break;
                    case FieldType.FieldWordBlockFlags:
                        break;
                    case FieldType.FieldByteBlockFlags:
                        break;
                    case FieldType.FieldCharBlockIndex1:
                        break;
                    case FieldType.FieldCharBlockIndex2:
                        break;
                    case FieldType.FieldShortBlockIndex1:
                        break;
                    case FieldType.FieldShortBlockIndex2:
                        break;
                    case FieldType.FieldLongBlockIndex1:
                        break;
                    case FieldType.FieldLongBlockIndex2:
                        break;
                    case FieldType.FieldData:
                        break;

                    case FieldType.FieldVertexBuffer:
                        break;
                }

                //Check
                if (control != null) panel.Controls.Add(control);
            }
            //Resume
            panel.ResumeLayout();
        }

        private static void TagReference_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void FlagsControl_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void EnumControl_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void ValueControl_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void StringControl_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
    public static class Guerilla
    {
        public static void GenerateControls(AbideTagBlock tagBlock, FlowLayoutPanel container)
        {
            //Begin
            container.SuspendLayout();

            //Loop
            Control control = null;
            foreach (AbideTagField field in tagBlock.FieldSet)
            {
                switch (field.FieldType)
                {
                    case FieldType.FieldStruct:
                        if (field.ReferencedBlock != null)
                            GenerateControls(field.ReferencedBlock, container);
                        break;

                    case FieldType.FieldBlock:
                        control = new Controls.BlockControl()
                        {
                            Title = field.Name
                        };
                        if (field.ReferencedBlock != null) GenerateControls(field.ReferencedBlock, ((Controls.BlockControl)control).Contents);
                        container.Controls.Add(control);
                        break;

                    case FieldType.FieldExplanation:
                        control = new Controls.ExplanationControl()
                        {
                            Title = field.Name,
                            Explanation = field.Explanation
                        };
                        container.Controls.Add(control);
                        break;
                    case FieldType.FieldString:
                        control = new Controls.StringControl()
                        {
                            Title = field.Name,
                            IsReadOnly = field.IsReadOnly,
                        };
                        container.Controls.Add(control);
                        break;
                    case FieldType.FieldLongString:
                        break;

                    case FieldType.FieldStringId:
                        control = new Controls.ValueControl()
                        {
                            Title = field.Name,
                            Information = field.Information,
                            Details = field.Details,
                            IsReadOnly = field.IsReadOnly
                        };
                        container.Controls.Add(control);
                        break;
                    case FieldType.FieldOldStringId:
                    case FieldType.FieldCharInteger:
                    case FieldType.FieldShortInteger:
                    case FieldType.FieldLongInteger:
                    case FieldType.FieldAngle:
                    case FieldType.FieldTag:
                    case FieldType.FieldReal:
                        control = new Controls.ValueControl()
                        {
                            Title = field.Name,
                            Details = field.Details,
                            Information = field.Information,
                            IsReadOnly = field.IsReadOnly
                        };
                        container.Controls.Add(control);
                        break;

                    case FieldType.FieldCharEnum:
                    case FieldType.FieldEnum:
                    case FieldType.FieldLongEnum:
                        control = new Controls.EnumControl()
                        {
                            Title = field.Name,
                            Details = field.Details,
                            Information = field.Information,
                            IsReadOnly = field.IsReadOnly
                        };
                        ((Controls.EnumControl)control).Options = field.Options.Select(o => o.Name).ToArray();
                        container.Controls.Add(control);
                        break;

                    case FieldType.FieldShortBounds:
                    case FieldType.FieldAngleBounds:
                    case FieldType.FieldRealBounds:
                        control = new Controls.RangeControl()
                        {
                            Title = field.Name,
                        };
                        container.Controls.Add(control);
                        break;

                    case FieldType.FieldLongBlockFlags:
                    case FieldType.FieldWordBlockFlags:
                    case FieldType.FieldByteBlockFlags:
                    case FieldType.FieldLongFlags:
                    case FieldType.FieldWordFlags:
                    case FieldType.FieldByteFlags:
                        control = new Controls.FlagsControl()
                        {
                            Title = field.Name,
                            Details = field.Details,
                            Information = field.Information,
                            IsReadOnly = field.IsReadOnly
                        };
                        ((Controls.FlagsControl)control).Options = field.Options.Select(o => o.Name).ToArray();
                        container.Controls.Add(control);
                        break;

                    case FieldType.FieldPoint2D:
                        control = new Controls.Point2Control()
                        {
                            Title = field.Name,
                            IsReadOnly = field.IsReadOnly
                        };
                        container.Controls.Add(control);
                        break;
                    case FieldType.FieldRectangle2D:
                        break;
                    case FieldType.FieldRgbColor:
                        break;
                    case FieldType.FieldArgbColor:
                        break;
                    case FieldType.FieldRealFraction:
                        break;
                    case FieldType.FieldRealPoint2D:
                        break;
                    case FieldType.FieldRealPoint3D:
                        break;
                    case FieldType.FieldRealVector2D:
                        break;
                    case FieldType.FieldRealVector3D:
                        control = new Controls.ThreeTupleControl()
                        {
                            Title = field.Name,
                            IsReadOnly = field.IsReadOnly
                        };
                        container.Controls.Add(control);
                        break;
                    case FieldType.FieldQuaternion:
                        break;
                    case FieldType.FieldEulerAngles2D:
                        break;
                    case FieldType.FieldEulerAngles3D:
                        break;
                    case FieldType.FieldRealPlane2D:
                        break;
                    case FieldType.FieldRealPlane3D:
                        break;
                    case FieldType.FieldRealRgbColor:
                        break;
                    case FieldType.FieldRealArgbColor:
                        break;
                    case FieldType.FieldRealHsvColor:
                        break;
                    case FieldType.FieldRealAhsvColor:
                        break;
                    case FieldType.FieldRealFractionBounds:
                        break;
                    case FieldType.FieldTagReference:
                        break;
                    case FieldType.FieldCharBlockIndex1:
                        break;
                    case FieldType.FieldCharBlockIndex2:
                        break;
                    case FieldType.FieldShortBlockIndex1:
                        break;
                    case FieldType.FieldShortBlockIndex2:
                        break;
                    case FieldType.FieldLongBlockIndex1:
                        break;
                    case FieldType.FieldLongBlockIndex2:
                        break;
                    case FieldType.FieldData:
                        break;
                    case FieldType.FieldVertexBuffer:
                        break;
                    case FieldType.FieldArrayStart:
                        break;
                    case FieldType.FieldArrayEnd:
                        break;
                }
            }

            //End
            container.ResumeLayout();
        }
    }
}
