using Abide.Tag.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla
{
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
                        control = new Controls.Vector3Control()
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
