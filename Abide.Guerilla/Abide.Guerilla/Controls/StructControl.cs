using Abide.Guerilla;
using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class StructControl : UserControl
    {
        public BaseStructField Field
        {
            get { return m_Field; }
            set
            {
                bool changed = m_Field != value;
                m_Field = value;
                if (changed) OnFieldChanged(new EventArgs());
            }
        }

        private BaseStructField m_Field;

        public StructControl(BaseStructField field) : this()
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
            for (int i = 0; i < tagBlock.Fields.Count; i++)
            {
                switch (tagBlock.Fields[i].Type)
                {
                    case Definition.FieldType.FieldBlock:
                        ((BlockControl)controlsFlowLayoutPanel.Controls[i]).Field = (BaseBlockField)tagBlock.Fields[i];
                        break;
                    case Definition.FieldType.FieldStruct:
                        ((StructControl)controlsFlowLayoutPanel.Controls[i]).Field = (BaseStructField)tagBlock.Fields[i];
                        break;
                    case Definition.FieldType.FieldExplanation:
                        continue;
                    default:
                        ((GuerillaControl)controlsFlowLayoutPanel.Controls[i]).Field = tagBlock.Fields[i];
                        break;
                }
            }
        }
    }
}
