using System.Windows.Forms;

namespace Abide.Dialogs
{
    public partial class FileTypeRegistrationDialog : Form
    {
        /// <summary>
        /// Initializes a new <see cref="FileTypeRegistrationDialog"/> instance.
        /// </summary>
        public FileTypeRegistrationDialog()
        {
            InitializeComponent();
            registerAaoCheckBox.Checked = AbideRegistry.IsAaoRegistered;
            registerMapCheckBox.Checked = AbideRegistry.IsMapRegistered;
            registerAtagCheckBox.Checked = AbideRegistry.IsAtagRegistered;
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
            if (registerAtagCheckBox.Checked && !AbideRegistry.IsAtagRegistered) { AbideRegistry.RegisterAtag(Application.ExecutablePath); result = DialogResult.Yes; }
            else if (!registerAtagCheckBox.Checked && AbideRegistry.IsAtagRegistered) { AbideRegistry.UnregisterAtag(); result = DialogResult.Yes; }

            //Set
            DialogResult = result;
        }
    }
}
