using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class GuerillaControl : UserControl
    {
        public event EventHandler FieldChanged;
        public virtual object Value { get; set; } = null;
        public Field Field
        {
            get { return m_Field; }
            set
            {
                bool changed = m_Field != value;
                m_Field = value;
                if (changed) OnFieldChanged(new EventArgs());
            }
        }
        
        protected ToolTip InformationTooltip
        {
            get { return informationToolTip; }
        }
        public string Title
        {
            get { return nameLabel.Text; }
            set { nameLabel.Text = value; }
        }

        private Field m_Field = null;

        public GuerillaControl()
        {
            InitializeComponent();
        }
        protected virtual void OnFieldChanged(EventArgs e)
        {
            nameLabel.Text = m_Field?.Name ?? string.Empty;
            FieldChanged?.Invoke(this, e);
        }
    }
}
