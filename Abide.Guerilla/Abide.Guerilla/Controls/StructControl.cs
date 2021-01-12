using Abide.Guerilla;
using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class StructControl : UserControl
    {
        public StructField Field
        {
            get { return m_Field; }
            set
            {
                bool changed = m_Field != value;
                m_Field = value;
                if (changed) OnFieldChanged(new EventArgs());
            }
        }

        private StructField m_Field;

        public StructControl(StructField field) : this()
        {
            Tags.GenerateControls(controlsFlowLayoutPanel, field.Create());
            Field = field;
        }
        private StructControl()
        {
            InitializeComponent();
        }
        protected virtual void OnFieldChanged(EventArgs e)
        {
            ITagBlock tagBlock = (ITagBlock)m_Field.Value;
            for (int i = 0; i < tagBlock.FieldCount; i++)
            {
                switch (tagBlock[i].Type)
                {
                    case FieldType.FieldBlock:
                        ((BlockControl)controlsFlowLayoutPanel.Controls[i]).Field = (BlockField)tagBlock[i];
                        break;
                    case FieldType.FieldStruct:
                        ((StructControl)controlsFlowLayoutPanel.Controls[i]).Field = (StructField)tagBlock[i];
                        break;
                    case FieldType.FieldExplanation:
                        continue;
                    default:
                        ((GuerillaControl)controlsFlowLayoutPanel.Controls[i]).Field = tagBlock[i];
                        break;
                }
            }
        }
    }
}
