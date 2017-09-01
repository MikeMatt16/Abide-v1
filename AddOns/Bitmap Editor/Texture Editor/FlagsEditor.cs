using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Texture_Editor
{
    /// <summary>
    /// Provides an editor for flag enumerators.
    /// </summary>
    internal sealed class FlagsEditor : UITypeEditor
    {
        public override bool IsDropDownResizable
        {
            get { return false; }
        }

        private CheckedListBox flagListBox;

        public FlagsEditor() : base()
        {
            flagListBox = new CheckedListBox();
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //Prepare
            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider?.GetService(typeof(IWindowsFormsEditorService));
            Type propertyType = context?.PropertyDescriptor.PropertyType;

            //Check
            if (editorService != null && propertyType != null)
            {
                //Get Enum
                Enum e = (Enum)value;

                //Clear
                flagListBox.Items.Clear();

                //Loop
                foreach (Enum flagValue in Enum.GetValues(propertyType))
                    flagListBox.Items.Add(flagValue);

                //Loop
                for (int i = 0; i < flagListBox.Items.Count; i++)
                    if (e.HasFlag((Enum)flagListBox.Items[i]))
                        flagListBox.SetItemChecked(i, true);
                    else flagListBox.SetItemChecked(i, false);

                //Drop Down
                editorService.DropDownControl(flagListBox);

                //Get Value
                uint enumValue = 0;
                for (int i = 0; i < flagListBox.Items.Count; i++)
                    if (flagListBox.GetItemChecked(i))
                        enumValue |= (uint)Convert.ChangeType(flagListBox.Items[i], typeof(uint));

                //Return
                return Convert.ChangeType(enumValue, Enum.GetUnderlyingType(propertyType));
            }

            return base.EditValue(context, provider, value);
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }
}
