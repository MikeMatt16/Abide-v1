using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Provides an empty <see cref="SettingsPage"/> AddOn control which implements the <see cref="ISettingsPage"/> interface.
    /// </summary>
    public class SettingsPage : UserControl, ISettingsPage
    {
        /// <summary>
        /// Occurs when the AddOn instance is initialized.
        /// </summary>
        [Category("Abide"), Description("Occurs when the AddOn instance is initialized.")]
        public event EventHandler<AddOnHostEventArgs> Initialize
        {
            add { initialize += value; }
            remove { initialize -= value; }
        }
        /// <summary>
        /// Gets or sets the description of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The description of the AddOn.")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        /// <summary>
        /// Gets or sets the author of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The author of the AddOn.")]
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        /// <summary>
        /// Gets or sets the name of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The name of the AddOn."), Browsable(true)]
        public string SettingsName
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets and returns the AddOn host.
        /// </summary>
        [Browsable(false)]
        public IHost Host
        {
            get { return host; }
        }

        private event EventHandler<AddOnHostEventArgs> initialize;
        private string author = string.Empty;
        private string description = string.Empty;
        private string name = string.Empty;
        private IHost host;

        /// <summary>
        /// Initializes a new <see cref="SettingsPage"/> instance.
        /// </summary>
        public SettingsPage()
        {
            name = Name;
            BackColor = SystemColors.ButtonHighlight;
        }
        /// <summary>
        /// Occurs when the AddOn instance is being initialized.
        /// </summary>
        /// <param name="e">The AddOn host event arguments.</param>
        protected virtual void OnInitialize(AddOnHostEventArgs e) { }

        string IAddOn.Author
        {
            get { return author; }
        }
        string IAddOn.Description
        {
            get { return description; }
        }
        string IAddOn.Name
        {
            get { return name; }
        }
        UserControl ISettingsPage.UserInterface
        {
            get { return this; }
        }
        void IAddOn.Initialize(IHost host)
        {
            //Set
            this.host = host;

            //Create Arguments
            AddOnHostEventArgs e = new AddOnHostEventArgs(host);

            //Initialize
            OnInitialize(e);

            //Invoke
            initialize?.Invoke(this, e);
        }
    }
}
