using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace HUD_Editor.Editors
{
    public sealed class PointFEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return base.GetEditStyle(context);
        }
    }
}
