using System;
using System.Windows.Forms;
using static HUD_Editor.Halo2.HaloHud.BitmapWidgetProperties;

namespace HUD_Editor.Forms
{
    public partial class HudBitmapWidgetEffectEditor : Form
    {
        public HudEffects Effects
        {
            get { return effects; }
        }
        private readonly HudEffects effects;

        private HudBitmapWidgetEffectEditor()
        {
            InitializeComponent();
        }

        public HudBitmapWidgetEffectEditor(HudEffects effects)
        {
            //Setup Effect
            this.effects = effects;

            //Initialize
            InitializeComponent();

            //Setup
            effectComboBox.BeginUpdate();
            foreach (HudEffect effect in effects.Effects)
                effectComboBox.Items.Add(effect);
            effectComboBox.EndUpdate();

            //Check
            if (effects.Effects.Length > 0) effectComboBox.SelectedIndex = 0;
        }

        private void effectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check
            if (effectComboBox.SelectedItem is HudEffect)
                effectProperties.SelectedObject = (HudEffect)effectComboBox.SelectedItem;
        }
    }
}
