using System.Windows.Forms;

namespace Abide.Dialogs
{
    public partial class FileTypeRegistrationDialog : Form
    {
        public FileTypeRegistrationDialog()
        {
            InitializeComponent();
            registerAaoCheckBox.Checked = AbideRegistry.IsAaoRegistered;
            registerMapCheckBox.Checked = AbideRegistry.IsMapRegistered;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            //Prepare
            DialogResult result = DialogResult.OK;

            //Check
            if (registerMapCheckBox.Checked && !AbideRegistry.IsMapRegistered) { AbideRegistry.RegisterMap(Application.ExecutablePath); result = DialogResult.Yes; }
            else if (!registerMapCheckBox.Checked && AbideRegistry.IsMapRegistered) { AbideRegistry.UnregisterMap(); result = DialogResult.Yes; }
            if (registerAaoCheckBox.Checked && !AbideRegistry.IsAaoRegistered) { AbideRegistry.RegisterAao(Application.ExecutablePath); result = DialogResult.Yes; }
            else if (!registerAaoCheckBox.Checked && AbideRegistry.IsAaoRegistered) { AbideRegistry.UnregisterAao(); result = DialogResult.Yes; }

            //Set
            DialogResult = result;
        }
    }
}
