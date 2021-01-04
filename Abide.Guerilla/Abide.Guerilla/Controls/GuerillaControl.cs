using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class GuerillaControl : UserControl
    {
        public event EventHandler FieldChanged;
        public virtual object Value { get; set; } = null;
        public ITagField Field
        {
            get { return field; }
            set
            {
                bool changed = field != value;
                field = value;
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

        private ITagField field = null;

        public GuerillaControl()
        {
            InitializeComponent();
        }
        protected virtual void OnFieldChanged(EventArgs e)
        {
            nameLabel.Text = field?.Name ?? string.Empty;
            FieldChanged?.Invoke(this, e);
        }
    }
}
