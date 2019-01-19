using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using HUD_Editor.Editors;
using HUD_Editor.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace HUD_Editor.Halo2
{
    public sealed class HaloHud : IDisposable
    {
        public BitmapWidgetProperties[] BitmapWidgets
        {
            get { return bitmapWidgets; }
        }

        private readonly BitmapWidgetProperties[] bitmapWidgets;
        private readonly IndexEntry entry;
        private readonly HudTag tag;

        public HaloHud(IndexEntry entry, MapFile map)
        {
            //Check
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            else if (entry.Root != HaloTags.nhdt) throw new ArgumentException("Index entry is not new hud.", nameof(entry));

            //Load
            this.entry = entry;
            tag = new HudTag(entry);

            //Prepare
            bitmapWidgets = new BitmapWidgetProperties[tag.Header.bitmapWidgets.Count];
            for (int i = 0; i < tag.Header.bitmapWidgets.Count; i++)
                bitmapWidgets[i] = new BitmapWidgetProperties(this, i, map);
        }
        /// <summary>
        /// Writes a this <see cref="HaloHud"/> instance to the tag's stream.
        /// </summary>
        /// <exception cref="IOException">An IO error occured.</exception>
        public void Write()
        {
            //Goto
            entry.TagData.Seek(entry.PostProcessedOffset, SeekOrigin.Begin);

            //Write Hud
            tag.Write(entry.TagData);
        }
        public void Dispose()
        {
        }

        public sealed class BitmapWidgetProperties : MapFileContainer
        {
            [Category("Widget Properties"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
            public StringProperty Name
            {
                get { return new StringProperty(hud.tag.BitmapWidgets[index].name, Map.Strings[hud.tag.BitmapWidgets[index].name.Index]); }
                set { hud.tag.BitmapWidgets[index].name = value.ID; }
            }
            [Category("HUD Imports")]
            public HudInput InputOne
            {
                get { return (HudInput)hud.tag.BitmapWidgets[index].input1; }
                set { hud.tag.BitmapWidgets[index].input1 = (byte)value; }
            }
            [Category("HUD Imports")]
            public HudInput InputTwo
            {
                get { return (HudInput)hud.tag.BitmapWidgets[index].input2; }
                set { hud.tag.BitmapWidgets[index].input2 = (byte)value; }
            }
            [Category("HUD Imports")]
            public HudInput InputThree
            {
                get { return (HudInput)hud.tag.BitmapWidgets[index].input3; }
                set { hud.tag.BitmapWidgets[index].input3 = (byte)value; }
            }
            [Category("HUD Imports")]
            public HudInput InputFour
            {
                get { return (HudInput)hud.tag.BitmapWidgets[index].input4; }
                set { hud.tag.BitmapWidgets[index].input4 = (byte)value; }
            }
            [Category("Draw Widget Options"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public UnitFlags DoDrawUnitFlags
            {
                get { return (UnitFlags)hud.tag.BitmapWidgets[index].y_UnitFlags; }
                set { hud.tag.BitmapWidgets[index].y_UnitFlags = (ushort)value; }
            }
            [Category("Draw Widget Options"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public AimFlags DoDrawAimFlags
            {
                get { return (AimFlags)hud.tag.BitmapWidgets[index].y_AimFlags; }
                set { hud.tag.BitmapWidgets[index].y_AimFlags = (ushort)value; }
            }
            [Category("Draw Widget Options"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public WeaponFlags DoDrawWeaponFlags
            {
                get { return (WeaponFlags)hud.tag.BitmapWidgets[index].y_WeaponFlags; }
                set { hud.tag.BitmapWidgets[index].y_WeaponFlags = (ushort)value; }
            }
            [Category("Draw Widget Options"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public EngineFlags DoDrawEngineFlags
            {
                get { return (EngineFlags)hud.tag.BitmapWidgets[index].y_EngineFlags; }
                set { hud.tag.BitmapWidgets[index].y_EngineFlags = (ushort)value; }
            }
            [Category("Do Not Draw Widget Options"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public UnitFlags DoNotDrawUnitFlags
            {
                get { return (UnitFlags)hud.tag.BitmapWidgets[index].n_UnitFlags; }
                set { hud.tag.BitmapWidgets[index].n_UnitFlags = (ushort)value; }
            }
            [Category("Do Not Draw Widget Options"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public AimFlags DoNotDrawAimFlags
            {
                get { return (AimFlags)hud.tag.BitmapWidgets[index].n_AimFlags; }
                set { hud.tag.BitmapWidgets[index].n_AimFlags = (ushort)value; }
            }
            [Category("Do Not Draw Widget Options"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public WeaponFlags DoNotDrawWeaponFlags
            {
                get { return (WeaponFlags)hud.tag.BitmapWidgets[index].n_WeaponFlags; }
                set { hud.tag.BitmapWidgets[index].n_WeaponFlags = (ushort)value; }
            }
            [Category("Do Not Draw Widget Options"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public EngineFlags DoNotDrawEngineFlags
            {
                get { return (EngineFlags)hud.tag.BitmapWidgets[index].n_EngineFlags; }
                set { hud.tag.BitmapWidgets[index].n_EngineFlags = (ushort)value; }
            }
            [Category("Widget Properties"), Editor(typeof(EffectEditor), typeof(UITypeEditor))]
            public HudEffects Effects
            {
                get { return effects; }
            }
            [Category("Widget Properties")]
            public byte AgeCutoff
            {
                get { return hud.tag.BitmapWidgets[index].ageCutoff; }
                set { hud.tag.BitmapWidgets[index].ageCutoff = value; }
            }
            [Category("Widget Properties")]
            public byte ClipCutoff
            {
                get { return hud.tag.BitmapWidgets[index].clipCutoff; }
                set { hud.tag.BitmapWidgets[index].clipCutoff = value; }
            }
            [Category("Widget Properties")]
            public byte TotalCutoff
            {
                get { return hud.tag.BitmapWidgets[index].totalCutoff; }
                set { hud.tag.BitmapWidgets[index].totalCutoff = value; }
            }
            [Category("Widget Properties")]
            public Anchor Anchor
            {
                get { return (Anchor)hud.tag.BitmapWidgets[index].anchor; }
                set { hud.tag.BitmapWidgets[index].anchor = (ushort)value; }
            }
            [Category("Widget Properties"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public DrawFlags Flags
            {
                get { return (DrawFlags)hud.tag.BitmapWidgets[index].flags; }
                set { hud.tag.BitmapWidgets[index].flags = (ushort)value; }
            }
            [Category("Widget Properties")]
            public SpecialHudType HudType
            {
                get { return (SpecialHudType)hud.tag.BitmapWidgets[index].specialHudType; }
                set { hud.tag.BitmapWidgets[index].specialHudType = (ushort)value; }
            }
            [Category("Tag References"), Editor(typeof(TagProperty.TagEditor), typeof(UITypeEditor))]
            public TagProperty BitmapTag
            {
                get { return new TagProperty(hud.tag.BitmapWidgets[index].bitmapId, Map.IndexEntries[hud.tag.BitmapWidgets[index].bitmapId]); }
                set { hud.tag.BitmapWidgets[index].bitmapId = value.ID; }
            }
            [Category("Tag References"), Editor(typeof(TagProperty.TagEditor), typeof(UITypeEditor))]
            public TagProperty ShaderTag
            {
                get { return new TagProperty(hud.tag.BitmapWidgets[index].shaderId, Map.IndexEntries[hud.tag.BitmapWidgets[index].shaderId]); }
                set { hud.tag.BitmapWidgets[index].shaderId = value.ID; }
            }
            [Category("Bitmap Properties")]
            public byte FullscreenSequenceIndex
            {
                get { return hud.tag.BitmapWidgets[index].fullscreenSequenceIndex; }
                set { hud.tag.BitmapWidgets[index].fullscreenSequenceIndex = value; }
            }
            [Category("Bitmap Properties")]
            public byte HalfscreenSequenceIndex
            {
                get { return hud.tag.BitmapWidgets[index].halfscreenSequenceIndex; }
                set { hud.tag.BitmapWidgets[index].halfscreenSequenceIndex = value; }
            }
            [Category("Bitmap Properties")]
            public byte QuarterscreenSequenceIndex
            {
                get { return hud.tag.BitmapWidgets[index].quarterscreenSequenceIndex; }
                set { hud.tag.BitmapWidgets[index].quarterscreenSequenceIndex = value; }
            }
            [Category("Bitmap Properties")]
            public Point FullscreenPositionOffset
            {
                get { return new Point(hud.tag.BitmapWidgets[index].fullscreenXOffset, hud.tag.BitmapWidgets[index].fullscreenYOffset); }
                set { hud.tag.BitmapWidgets[index].fullscreenXOffset = (short)value.X; hud.tag.BitmapWidgets[index].fullscreenYOffset = (short)value.Y; }
            }
            [Category("Bitmap Properties")]
            public Point HalfscreenPositionOffset
            {
                get { return new Point(hud.tag.BitmapWidgets[index].halfscreenXOffset, hud.tag.BitmapWidgets[index].halfscreenYOffset); }
                set { hud.tag.BitmapWidgets[index].halfscreenXOffset = (short)value.X; hud.tag.BitmapWidgets[index].halfscreenYOffset = (short)value.Y; }
            }
            [Category("Bitmap Properties")]
            public Point QuarterscreenPositionOffset
            {
                get { return new Point(hud.tag.BitmapWidgets[index].quarterscreenXOffset, hud.tag.BitmapWidgets[index].quarterscreenYOffset); }
                set { hud.tag.BitmapWidgets[index].quarterscreenXOffset = (short)value.X; hud.tag.BitmapWidgets[index].quarterscreenYOffset = (short)value.Y; }
            }
            [Category("Bitmap Properties")]
            public PointF FullscreenRegistrationPoint
            {
                get { return new PointF(hud.tag.BitmapWidgets[index].fullscreenRegistrationX, hud.tag.BitmapWidgets[index].fullscreenRegistrationY); }
                set { hud.tag.BitmapWidgets[index].fullscreenRegistrationX = value.X; hud.tag.BitmapWidgets[index].fullscreenRegistrationY = value.Y; }
            }
            [Category("Bitmap Properties")]
            public PointF HalfscreenRegistrationPoint
            {
                get { return new PointF(hud.tag.BitmapWidgets[index].halfscreenRegistrationX, hud.tag.BitmapWidgets[index].halfscreenRegistrationY); }
                set { hud.tag.BitmapWidgets[index].halfscreenRegistrationX = value.X; hud.tag.BitmapWidgets[index].halfscreenRegistrationY = value.Y; }
            }
            [Category("Bitmap Properties")]
            public PointF QuarterscreenRegistrationPoint
            {
                get { return new PointF(hud.tag.BitmapWidgets[index].quarterscreenRegistrationX, hud.tag.BitmapWidgets[index].quarterscreenRegistrationY); }
                set { hud.tag.BitmapWidgets[index].quarterscreenRegistrationX = value.X; hud.tag.BitmapWidgets[index].quarterscreenRegistrationY = value.Y; }
            }

            private readonly HudEffects effects;
            private readonly HaloHud hud;
            private readonly int index;

            public BitmapWidgetProperties(HaloHud hud, int index, MapFile map) : base(map)
            {
                this.hud = hud;
                this.index = index;
                effects = new HudEffects(hud, index, map);
            }

            public override string ToString()
            {
                return $"Bitmap: {Name}";
            }

            public sealed class HudEffects
            {
                [Browsable(false)]
                public HudEffect[] Effects
                {
                    get { return effects; }
                }

                private readonly HudEffect[] effects;
                private readonly int index;

                public HudEffects(HaloHud hud, int index, MapFile map)
                {
                    //Setup
                    this.index = index;
                    effects = new HudEffect[hud.tag.BitmapWidgets[index].effect.Count];

                    //Loop
                    for (int i = 0; i < hud.tag.BitmapWidgets[index].effect.Count; i++)
                        effects[i] = new HudEffect(hud, index, i, map);
                }

                public override string ToString()
                {
                    return $"Effects {effects.Length} count";
                }
            }

            public sealed class HudEffect : MapFileContainer
            {
                [Category("Effect Properties"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
                public EffectFlags Flags
                {
                    get { return (EffectFlags)hud.tag.BitmapEffects[bitmapIndex][index].flags; }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].flags = (ushort)value; }
                }

                [Category("Function 1"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty Function1Input
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].f1.inputName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].f1.inputName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f1.inputName = value.ID; }
                }
                [Category("Function 1"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty Function1Range
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].f1.rangeName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].f1.rangeName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f1.rangeName = value.ID; }
                }
                [Category("Function 1")]
                public float Function1Time
                {
                    get { return hud.tag.BitmapEffects[bitmapIndex][index].f1.timePeriod; }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f1.timePeriod = value; }
                }
                [Category("Function 1"), Editor(typeof(FunctionEditor), typeof(UITypeEditor))]
                public Function Function1
                {
                    get { return new Function(hud.tag.BitmapFunction1[bitmapIndex][index]); }
                    set { hud.tag.BitmapFunction1[bitmapIndex][index] = value.Data; }
                }

                [Category("Function 2"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty Function2Input
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].f2.inputName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].f2.inputName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f2.inputName = value.ID; }
                }
                [Category("Function 2"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty Function2Range
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].f2.rangeName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].f2.rangeName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f2.rangeName = value.ID; }
                }
                [Category("Function 2")]
                public float Function2Time
                {
                    get { return hud.tag.BitmapEffects[bitmapIndex][index].f2.timePeriod; }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f2.timePeriod = value; }
                }
                [Category("Function 2"), Editor(typeof(FunctionEditor), typeof(UITypeEditor))]
                public Function Function2
                {
                    get { return new Function(hud.tag.BitmapFunction2[bitmapIndex][index]); }
                    set { hud.tag.BitmapFunction2[bitmapIndex][index] = value.Data; }
                }

                [Category("Rotation"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty RotationInput
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].Rotation.inputName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].Rotation.inputName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].Rotation.inputName = value.ID; }
                }
                [Category("Rotation"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty RotationRange
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].Rotation.rangeName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].Rotation.rangeName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].Rotation.rangeName = value.ID; }
                }
                [Category("Rotation")]
                public float RotationTime
                {
                    get { return hud.tag.BitmapEffects[bitmapIndex][index].Rotation.timePeriod; }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].Rotation.timePeriod = value; }
                }
                [Category("Rotation"), Editor(typeof(FunctionEditor), typeof(UITypeEditor))]
                public Function Rotation
                {
                    get { return new Function(hud.tag.BitmapRotation[bitmapIndex][index]); }
                    set { hud.tag.BitmapRotation[bitmapIndex][index] = value.Data; }
                }

                [Category("Function 4"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty Function4Input
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].f4.inputName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].f4.inputName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f4.inputName = value.ID; }
                }
                [Category("Function 4"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty Function4Range
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].f4.rangeName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].f4.rangeName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f4.rangeName = value.ID; }
                }
                [Category("Function 4")]
                public float Function4Time
                {
                    get { return hud.tag.BitmapEffects[bitmapIndex][index].f4.timePeriod; }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f4.timePeriod = value; }
                }
                [Category("Function 4"), Editor(typeof(FunctionEditor), typeof(UITypeEditor))]
                public Function Function4
                {
                    get { return new Function(hud.tag.BitmapFunction4[bitmapIndex][index]); }
                    set { hud.tag.BitmapFunction4[bitmapIndex][index] = value.Data; }
                }

                [Category("Function 5"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty Function5Input
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].f5.inputName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].f5.inputName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f5.inputName = value.ID; }
                }
                [Category("Function 5"), Editor(typeof(StringProperty.StringIDEditor), typeof(UITypeEditor))]
                public StringProperty Function5Range
                {
                    get { return new StringProperty(hud.tag.BitmapEffects[bitmapIndex][index].f5.rangeName, Map.Strings[hud.tag.BitmapEffects[bitmapIndex][index].f5.rangeName.Index]); }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f5.rangeName = value.ID; }
                }
                [Category("Function 5")]
                public float Function5Time
                {
                    get { return hud.tag.BitmapEffects[bitmapIndex][index].f5.timePeriod; }
                    set { hud.tag.BitmapEffects[bitmapIndex][index].f5.timePeriod = value; }
                }
                [Category("Function 5"), Editor(typeof(FunctionEditor), typeof(UITypeEditor))]
                public Function Function5
                {
                    get { return new Function(hud.tag.BitmapFunction5[bitmapIndex][index]); }
                    set { hud.tag.BitmapFunction5[bitmapIndex][index] = value.Data; }
                }

                private readonly HaloHud hud;
                private readonly int bitmapIndex;
                private int index;

                public HudEffect(HaloHud hud, int bitmapIndex, int index, MapFile map) : base(map)
                {
                    this.hud = hud;
                    this.bitmapIndex = bitmapIndex;
                    this.index = index;
                }

                public override string ToString()
                {
                    return $"Effect {index}";
                }
            }

            public class EffectEditor : UITypeEditor
            {
                public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
                {
                    //Setup
                    HudEffects effects = null;

                    //Check
                    if (value != null && value is HudEffects)
                        using (HudBitmapWidgetEffectEditor editor = new HudBitmapWidgetEffectEditor((HudEffects)value))
                        { editor.ShowDialog(); effects = editor.Effects; }

                    //Return
                    return effects;
                }

                public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
                {
                    return UITypeEditorEditStyle.Modal;
                }
            }
        }
        
        private sealed class HudTag
        {
            private readonly bool HaloHudWriteTextWidgets = false;

            public NewHudTagGroup Header;
            public NewHudTagGroup.BitmapWidget[] BitmapWidgets;
            public NewHudTagGroup.Effect[][] BitmapEffects;
            public byte[][][] BitmapFunction1, BitmapFunction2, BitmapRotation, BitmapFunction4, BitmapFunction5;
            public NewHudTagGroup.TextWidget[] TextWidgets;
            public NewHudTagGroup.Effect[][] TextEffects;
            public byte[][][] TextFunction1, TextFunction2, TextRotation, TextFunction4, TextFunction5;
            public NewHudTagGroup.EffectWidget[] EffectWidgets;

            /// <summary>
            /// Initializes a new <see cref="HudTag"/> using the supplied object index entry.
            /// </summary>
            /// <param name="entry">The new hud object index entry.</param>
            public HudTag(IndexEntry entry)
            {
                //Initialize reader
                using (BinaryReader reader = entry.TagData.CreateReader())
                {
                    //Goto
                    entry.TagData.Seek(entry.Offset, SeekOrigin.Begin);

                    //Read
                    Header = reader.Read<NewHudTagGroup>();

                    //Prepare Structures and arrays
                    BitmapWidgets = new NewHudTagGroup.BitmapWidget[Header.bitmapWidgets.Count];
                    TextWidgets = new NewHudTagGroup.TextWidget[Header.textWidgets.Count];
                    EffectWidgets = new NewHudTagGroup.EffectWidget[Header.effectWidgets.Count];
                    BitmapEffects = new NewHudTagGroup.Effect[Header.bitmapWidgets.Count][];
                    TextEffects = new NewHudTagGroup.Effect[Header.textWidgets.Count][];
                    BitmapFunction2 = new byte[Header.bitmapWidgets.Count][][];
                    BitmapRotation = new byte[Header.bitmapWidgets.Count][][];
                    BitmapFunction1 = new byte[Header.bitmapWidgets.Count][][];
                    BitmapFunction4 = new byte[Header.bitmapWidgets.Count][][];
                    BitmapFunction5 = new byte[Header.bitmapWidgets.Count][][];
                    TextFunction2 = new byte[Header.textWidgets.Count][][];
                    TextRotation = new byte[Header.textWidgets.Count][][];
                    TextFunction1 = new byte[Header.textWidgets.Count][][];
                    TextFunction4 = new byte[Header.textWidgets.Count][][];
                    TextFunction5 = new byte[Header.textWidgets.Count][][];

                    //Read Bitmap Widgets
                    if (Header.bitmapWidgets.Count > 0)
                    {
                        entry.TagData.Seek(Header.bitmapWidgets.Offset, SeekOrigin.Begin);
                        for (int i = 0; i < Header.bitmapWidgets.Count; i++)
                            BitmapWidgets[i] = reader.Read<NewHudTagGroup.BitmapWidget>();
                    }

                    //Read Text Widgets
                    if (Header.textWidgets.Count > 0)
                    {
                        entry.TagData.Seek(Header.textWidgets.Offset, SeekOrigin.Begin);
                        for (int i = 0; i < Header.textWidgets.Count; i++)
                            TextWidgets[i] = reader.Read<NewHudTagGroup.TextWidget>();
                    }

                    //Read Effect Widgets
                    if (Header.effectWidgets.Count > 0)
                    {
                        entry.TagData.Seek(Header.effectWidgets.Offset, SeekOrigin.Begin);
                        for (int i = 0; i < Header.effectWidgets.Count; i++)
                            EffectWidgets[i] = reader.Read<NewHudTagGroup.EffectWidget>();
                    }

                    //Prepare Bitmap Structures
                    for (int i = 0; i < Header.bitmapWidgets.Count; i++)
                    {
                        //Read Bitmap Effects
                        BitmapEffects[i] = new NewHudTagGroup.Effect[BitmapWidgets[i].effect.Count];
                        if (BitmapWidgets[i].effect.Count > 0)
                        {
                            entry.TagData.Seek(BitmapWidgets[i].effect.Offset, SeekOrigin.Begin);
                            for (int j = 0; j < BitmapWidgets[i].effect.Count; j++)
                                BitmapEffects[i][j] = reader.Read<NewHudTagGroup.Effect>();
                        }

                        //Setup Arrays
                        BitmapFunction1[i] = new byte[BitmapWidgets[i].effect.Count][];
                        BitmapFunction2[i] = new byte[BitmapWidgets[i].effect.Count][];
                        BitmapRotation[i] = new byte[BitmapWidgets[i].effect.Count][];
                        BitmapFunction4[i] = new byte[BitmapWidgets[i].effect.Count][];
                        BitmapFunction5[i] = new byte[BitmapWidgets[i].effect.Count][];

                        //Prepare Bitmap Functions
                        for (int j = 0; j < BitmapWidgets[i].effect.Count; j++)
                        {
                            //Setup Arrays
                            BitmapFunction1[i][j] = new byte[BitmapEffects[i][j].f1.data.Count];
                            BitmapFunction2[i][j] = new byte[BitmapEffects[i][j].f2.data.Count];
                            BitmapRotation[i][j] = new byte[BitmapEffects[i][j].Rotation.data.Count];
                            BitmapFunction4[i][j] = new byte[BitmapEffects[i][j].f4.data.Count];
                            BitmapFunction5[i][j] = new byte[BitmapEffects[i][j].f5.data.Count];

                            //Read Scale
                            entry.TagData.Seek(BitmapEffects[i][j].f1.data.Offset);
                            for (int k = 0; k < BitmapEffects[i][j].f1.data.Count; k++)
                                BitmapFunction1[i][j][k] = reader.ReadByte();

                            //Read Scale
                            entry.TagData.Seek(BitmapEffects[i][j].f2.data.Offset);
                            for (int k = 0; k < BitmapEffects[i][j].f2.data.Count; k++)
                                BitmapFunction2[i][j][k] = reader.ReadByte();

                            //Read Scale
                            entry.TagData.Seek(BitmapEffects[i][j].Rotation.data.Offset);
                            for (int k = 0; k < BitmapEffects[i][j].Rotation.data.Count; k++)
                                BitmapRotation[i][j][k] = reader.ReadByte();

                            //Read Scale
                            entry.TagData.Seek(BitmapEffects[i][j].f4.data.Offset);
                            for (int k = 0; k < BitmapEffects[i][j].f4.data.Count; k++)
                                BitmapFunction4[i][j][k] = reader.ReadByte();

                            //Read Scale
                            entry.TagData.Seek(BitmapEffects[i][j].f5.data.Offset);
                            for (int k = 0; k < BitmapEffects[i][j].f5.data.Count; k++)
                                BitmapFunction5[i][j][k] = reader.ReadByte();
                        }
                    }

                    //Prepare Text Structures
                    for (int i = 0; i < Header.textWidgets.Count; i++)
                    {
                        //Read Text Effects
                        TextEffects[i] = new NewHudTagGroup.Effect[TextWidgets[i].effect.Count];
                        if (TextWidgets[i].effect.Count > 0)
                        {
                            entry.TagData.Seek(TextWidgets[i].effect.Offset, SeekOrigin.Begin);
                            for (int j = 0; j < TextWidgets[i].effect.Count; j++)
                                TextEffects[i][j] = reader.Read<NewHudTagGroup.Effect>();
                        }

                        //Setup Arrays
                        TextFunction1[i] = new byte[TextWidgets[i].effect.Count][];
                        TextFunction2[i] = new byte[TextWidgets[i].effect.Count][];
                        TextRotation[i] = new byte[TextWidgets[i].effect.Count][];
                        TextFunction4[i] = new byte[TextWidgets[i].effect.Count][];
                        TextFunction5[i] = new byte[TextWidgets[i].effect.Count][];

                        //Prepare Text Functions
                        for (int j = 0; j < TextWidgets[i].effect.Count; j++)
                        {
                            //Setup Arrays
                            TextFunction1[i][j] = new byte[TextEffects[i][j].f1.data.Count];
                            TextFunction2[i][j] = new byte[TextEffects[i][j].f2.data.Count];
                            TextRotation[i][j] = new byte[TextEffects[i][j].Rotation.data.Count];
                            TextFunction4[i][j] = new byte[TextEffects[i][j].f4.data.Count];
                            TextFunction5[i][j] = new byte[TextEffects[i][j].f5.data.Count];

                            //Read Scale
                            entry.TagData.Seek(TextEffects[i][j].f1.data.Offset);
                            for (int k = 0; k < TextEffects[i][j].f1.data.Count; k++)
                                TextFunction1[i][j][k] = reader.ReadByte();

                            //Read Scale
                            entry.TagData.Seek(TextEffects[i][j].f2.data.Offset);
                            for (int k = 0; k < TextEffects[i][j].f2.data.Count; k++)
                                TextFunction2[i][j][k] = reader.ReadByte();

                            //Read Scale
                            entry.TagData.Seek(TextEffects[i][j].Rotation.data.Offset);
                            for (int k = 0; k < TextEffects[i][j].Rotation.data.Count; k++)
                                TextRotation[i][j][k] = reader.ReadByte();

                            //Read Scale
                            entry.TagData.Seek(TextEffects[i][j].f4.data.Offset);
                            for (int k = 0; k < TextEffects[i][j].f4.data.Count; k++)
                                TextFunction4[i][j][k] = reader.ReadByte();

                            //Read Scale
                            entry.TagData.Seek(TextEffects[i][j].f5.data.Offset);
                            for (int k = 0; k < TextEffects[i][j].f5.data.Count; k++)
                                TextFunction5[i][j][k] = reader.ReadByte();
                        }
                    }
                }
            }
            /// <summary>
            /// Writes the <see cref="HudTag"/> to the supplied stream.
            /// </summary>
            /// <param name="outStream">The stream to write the new hud tag group to.</param>
            /// <exception cref="ArgumentNullException"><paramref name="outStream"/> is null.</exception>
            /// <exception cref="ArgumentException"><paramref name="outStream"/> does not support seeking.</exception>
            /// <exception cref="IOException">An IO error occured.</exception>
            public void Write(Stream outStream)
            {
                //Check
                if (outStream == null) throw new ArgumentNullException(nameof(outStream));
                if (!outStream.CanWrite) throw new ArgumentException("Stream does not support writing.", nameof(outStream));

                //Create Writer
                using (BinaryWriter writer = new BinaryWriter(outStream))
                {
                    //Write Header
                    writer.Write(Header);

                    //Write Bitmap Widgets
                    outStream.Seek(Header.bitmapWidgets.Offset, SeekOrigin.Begin);
                    for (int i = 0; i < BitmapWidgets.Length; i++)
                        writer.Write(BitmapWidgets[i]);

                    //Write Bitmap Widget Effects
                    for (int i = 0; i < BitmapWidgets.Length; i++)
                    {
                        outStream.Seek(BitmapWidgets[i].effect.Offset, SeekOrigin.Begin);
                        for (int j = 0; j < BitmapEffects[i].Length; j++)
                            writer.Write(BitmapEffects[i][j]);
                    }

                    //Write Bitmap Widget Function 1 Data
                    for (int i = 0; i < BitmapWidgets.Length; i++)
                        for (int j = 0; j < BitmapEffects[i].Length; j++)
                        {
                            outStream.Seek(BitmapEffects[i][j].f1.data.Offset, SeekOrigin.Begin);
                            for (int k = 0; k < BitmapFunction1[i][j].Length; k++)
                                writer.Write(BitmapFunction1[i][j][k]);
                        }

                    //Write Bitmap Widget Function 2 Data
                    for (int i = 0; i < BitmapWidgets.Length; i++)
                        for (int j = 0; j < BitmapEffects[i].Length; j++)
                        {
                            outStream.Seek(BitmapEffects[i][j].f2.data.Offset, SeekOrigin.Begin);
                            for (int k = 0; k < BitmapFunction2[i][j].Length; k++)
                                writer.Write(BitmapFunction2[i][j][k]);
                        }

                    //Write Bitmap Rotation Data
                    for (int i = 0; i < BitmapWidgets.Length; i++)
                        for (int j = 0; j < BitmapEffects[i].Length; j++)
                        {
                            outStream.Seek(BitmapEffects[i][j].Rotation.data.Offset, SeekOrigin.Begin);
                            for (int k = 0; k < BitmapRotation[i][j].Length; k++)
                                writer.Write(BitmapRotation[i][j][k]);
                        }

                    //Write Bitmap Widget Function 4 Data
                    for (int i = 0; i < BitmapWidgets.Length; i++)
                        for (int j = 0; j < BitmapEffects[i].Length; j++)
                        {
                            outStream.Seek(BitmapEffects[i][j].f4.data.Offset, SeekOrigin.Begin);
                            for (int k = 0; k < BitmapFunction4[i][j].Length; k++)
                                writer.Write(BitmapFunction4[i][j][k]);
                        }

                    //Write Bitmap Widget Function 5 Data
                    for (int i = 0; i < BitmapWidgets.Length; i++)
                        for (int j = 0; j < BitmapEffects[i].Length; j++)
                        {
                            outStream.Seek(BitmapEffects[i][j].f5.data.Offset, SeekOrigin.Begin);
                            for (int k = 0; k < BitmapFunction5[i][j].Length; k++)
                                writer.Write(BitmapFunction5[i][j][k]);
                        }

                    if (HaloHudWriteTextWidgets)
                    {
                        //Write Text Widgets
                        for (int i = 0; i < TextWidgets.Length; i++)
                            writer.Write(TextWidgets[i]);

                        //Write Text Widget Effects
                        for (int i = 0; i < TextWidgets.Length; i++)
                            for (int j = 0; j < TextEffects[i].Length; j++)
                                writer.Write(TextEffects[i][j]);

                        //Write Text Widget Scale Data
                        for (int i = 0; i < TextWidgets.Length; i++)
                            for (int j = 0; j < TextEffects[i].Length; j++)
                                for (int k = 0; k < TextFunction1[i][j].Length; k++)
                                    writer.Write(TextFunction1[i][j][k]);

                        //Write Text Widget Clockwise Data
                        for (int i = 0; i < TextWidgets.Length; i++)
                            for (int j = 0; j < TextEffects[i].Length; j++)
                                for (int k = 0; k < TextFunction2[i][j].Length; k++)
                                    writer.Write(TextFunction2[i][j][k]);

                        //Write Text Widget Anticlockwise Data
                        for (int i = 0; i < TextWidgets.Length; i++)
                            for (int j = 0; j < TextEffects[i].Length; j++)
                                for (int k = 0; k < TextRotation[i][j].Length; k++)
                                    writer.Write(TextRotation[i][j][k]);

                        //Write Text Widget Offset X Data
                        for (int i = 0; i < TextWidgets.Length; i++)
                            for (int j = 0; j < TextEffects[i].Length; j++)
                                for (int k = 0; k < TextFunction4[i][j].Length; k++)
                                    writer.Write(TextFunction4[i][j][k]);

                        //Write Text Widget Offset Y Data
                        for (int i = 0; i < TextWidgets.Length; i++)
                            for (int j = 0; j < TextEffects[i].Length; j++)
                                for (int k = 0; k < TextFunction5[i][j].Length; k++)
                                    writer.Write(TextFunction5[i][j][k]);

                        //Write Effect Widgets
                        for (int i = 0; i < EffectWidgets.Length; i++)
                            writer.Write(EffectWidgets[i]);
                    }
                }
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        private struct NewHudTagGroup
        {
            public static readonly int Size = Marshal.SizeOf(typeof(NewHudTagGroup));

            public TagFourCc donotusetag;
            public TagId donotuseid;
            public TagBlock bitmapWidgets;
            public TagBlock textWidgets;
            public short lowClipCutoff;
            public short lowAmmoCutoff;
            public float ageCutoff;
            public TagBlock effectWidgets;

            [StructLayout(LayoutKind.Sequential)]
            public struct BitmapWidget
            {
                public static readonly int Size = Marshal.SizeOf(typeof(BitmapWidget));

                public StringId name;
                public byte input1;
                public byte input2;
                public byte input3;
                public byte input4;
                public ushort y_UnitFlags;
                public ushort y_AimFlags;
                public ushort y_WeaponFlags;
                public ushort y_EngineFlags;
                public ushort n_UnitFlags;
                public ushort n_AimFlags;
                public ushort n_WeaponFlags;
                public ushort n_EngineFlags;
                public byte ageCutoff;
                public byte clipCutoff;
                public byte totalCutoff;
                public byte unused1;
                public ushort anchor;
                public ushort flags;
                public TagFourCc bitmapTag;
                public TagId bitmapId;
                public TagFourCc shaderTag;
                public TagId shaderId;
                public byte fullscreenSequenceIndex;
                public byte halfscreenSequenceIndex;
                public byte quarterscreenSequenceIndex;
                public byte unused2;
                public short fullscreenXOffset;
                public short fullscreenYOffset;
                public short halfscreenXOffset;
                public short halfscreenYOffset;
                public short quarterscreenXOffset;
                public short quarterscreenYOffset;
                public float fullscreenRegistrationX;
                public float fullscreenRegistrationY;
                public float halfscreenRegistrationX;
                public float halfscreenRegistrationY;
                public float quarterscreenRegistrationX;
                public float quarterscreenRegistrationY;
                public TagBlock effect;
                public ushort specialHudType;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
                public byte[] unused3;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct TextWidget
            {
                public static readonly int Size = Marshal.SizeOf(typeof(TextWidget));

                public StringId name;
                public byte input1;
                public byte input2;
                public byte input3;
                public byte input4;
                public ushort y_UnitFlags;
                public ushort y_AimFlags;
                public ushort y_WeaponFlags;
                public ushort y_EngineFlags;
                public ushort n_UnitFlags;
                public ushort n_AimFlags;
                public ushort n_WeaponFlags;
                public ushort n_EngineFlags;
                public byte ageCutoff;
                public byte clipCutoff;
                public byte totalCutoff;
                public byte unused1;
                public ushort anchor;
                public ushort flags;
                public TagFourCc shaderTag;
                public TagId shaderId;
                public StringId stringName;
                public ushort justification;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
                public byte[] unused2;
                public byte fullscreenFont;
                public byte halfscreenFont;
                public byte quarterscreenFont;
                public byte unused3;
                public float fullscreenScale;
                public float halfscreenScale;
                public float quarterscreenScale;
                public short fullscreenXOffset;
                public short fullscreenYOffset;
                public short halfscreenXOffset;
                public short halfscreenYOffset;
                public short quarterscreenXOffset;
                public short quarterscreenYOffset;
                public TagBlock effect;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct EffectWidget
            {
                public StringId name;
                public byte input1;
                public byte input2;
                public byte input3;
                public byte input4;
                public ushort y_UnitFlags;
                public ushort y_AimFlags;
                public ushort y_WeaponFlags;
                public ushort y_EngineFlags;
                public ushort n_UnitFlags;
                public ushort n_AimFlags;
                public ushort n_WeaponFlags;
                public ushort n_EngineFlags;
                public byte ageCutoff;
                public byte clipCutoff;
                public byte totalCutoff;
                public byte unused1;
                public ushort anchor;
                public ushort flags;
                public TagFourCc bitmapTag;
                public TagId bitmapId;
                public TagFourCc fullscreenEffectTag;
                public TagId fullscreenEffectId;
                public TagFourCc halfcreenEffectTag;
                public TagId halfscreenEffectId;
                public TagFourCc quarterscreenEffectTag;
                public TagId quarterscreenEffectId;
                public byte fullscreenSequenceIndex;
                public byte halfscreenSequenceIndex;
                public byte quarterscreenSequenceIndex;
                public byte unused2;
                public short fullscreenXOffset;
                public short fullscreenYOffset;
                public short halfscreenXOffset;
                public short halfscreenYOffset;
                public short quarterscreenXOffset;
                public short quarterscreenYOffset;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct Effect
            {
                public static readonly int Size = Marshal.SizeOf(typeof(Effect));

                public ushort flags;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
                public byte[] unused1;
                public EffectFunction f1;
                public EffectFunction f2;
                public EffectFunction Rotation;
                public EffectFunction f4;
                public EffectFunction f5;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct EffectFunction
            {
                public static readonly int Size = Marshal.SizeOf(typeof(EffectFunction));

                public StringId inputName;
                public StringId rangeName;
                public float timePeriod;
                public TagBlock data;
            }
        }
    }

    public sealed class Function
    {
        public byte[] Data
        {
            get { return data; }
        }

        private readonly byte[] data;

        public Function(byte[] data)
        {
            this.data = data;
        }

        public override string ToString()
        {
            if (data != null) return $"Length: {data.Length}";
            else return base.ToString();
        }
    }

    public enum HudInput : byte
    {
        Zero = 0,
        One = 1,
        Time = 2,
        GlobalHUDFade = 3,
        PlayerSheild = 16,
        PlayerBody = 17,
        PlayerAutoAimed = 18,
        PlayerHasNoGrenades = 19,
        PlayerFragGrenadeCount = 20,
        PlayerPlasmaGrenadeCount = 21,
        PlayerTimeOnDPLSheild = 22,
        PlayerZoomFraction = 23,
        PlayerCamoValue = 24,
        ParentSheild = 33,
        ParentBody = 34,
        WeaponClipAmmo = 49,
        WeaponHeat = 40,
        WeaponBattery = 51,
        WeaponTotalAmmo = 52,
        WeaponBarrelSpin = 53,
        WeaponOverheated = 54,
        WeaponClipAmmoFraction = 55,
        WeaponTimeOnOverheat = 56,
        WeaponBatteryFraction = 57,
        WeaponLockingFraction = 58,
        UserScoreFraction = 66,
        OtherUserScoreFraction = 67,
        UserWinning = 68,
        BombArmingAmount = 69
    };

    [Flags]
    public enum UnitFlags : ushort
    {
        Default = 0x1,
        GrenadeTypeNone = 0x2,
        GrenadeTypeFrag = 0x4,
        GrenadeTypePlasma = 0x8,
        UnitIsSingleWielding = 0x10,
        UnitIsDualWielding = 0x20,
        UnitIsUnzoomed = 0x40,
        UnitIsZoomLevelOne = 0x80,
        UnitIsZoomLevelTwo = 0x100,
        GrenadesDisabled = 0x200,
        BinocularsEnabled = 0x400,
        MotionSensorEnabled = 0x800,
        Dervish = 0x1000,
    };

    [Flags]
    public enum AimFlags : ushort
    {
        Friendly = 0x1,
        Plasma = 0x2,
        Headshot = 0x4,
        Vulnerable = 0x8,
        Invincible = 0x10,
    };

    [Flags]
    public enum WeaponFlags : ushort
    {
        PrimaryWeapon = 0x1,
        SecondaryWeapon = 0x2,
        BackpackWeapon = 0x4,
        AgeBelowCutoff = 0x8,
        ClipBelowCutoff = 0x10,
        TotalBelowCutoff = 0x20,
        Overheated = 0x40,
        OutOfAmmo = 0x80,
        LockTargetAvailable = 0x100,
        Locking = 0x200,
        Locked = 0x400,
    };

    [Flags]
    public enum EngineFlags : ushort
    {
        CampaignSolo = 0x1,
        CampaignCoop = 0x2,
        FreeForAll = 0x4,
        TeamGame = 0x8,
        UserLeading = 0x10,
        UserNotLeading = 0x20,
        TimedGame = 0x40,
        UntimedGame = 0x80,
        OtherScoreValid = 0x100,
        OtherScoreInvalid = 0x200,
        PlayerArmingBomb = 0x400,
        PlayerTalking = 0x800,
    };

    public enum Anchor : ushort
    {
        HealthAndShield = 0,
        WeaponHud = 1,
        MotionSensor = 2,
        Scoreboard = 3,
        Crosshair = 4,
        LockOnTarget = 5,
    };

    [Flags]
    public enum DrawFlags : ushort
    {
        FlipX = 0x1,
        FlipY = 0x2,
        MirrorX = 0x4,
        MirrorY = 0x8,
        Fill = 0x10,
    };

    [Flags]
    public enum EffectFlags : ushort
    {
        Scale = 0x1,
        Theta = 0x2,
        Offset = 0x4,
    };

    public enum SpecialHudType : ushort
    {
        Unspecial = 0,
        PlayerEmblem = 1,
        OtherPlayerEmblem = 2,
        PlayerScore = 3,
        OtherPlayerScore = 4,
        UnitShieldMeter = 5,
        MotionSensor = 6,
        TerritoryMeter = 7,
    };

    [Flags]
    public enum StringFlags : ushort
    {
        StringIsANumber = 0x1,
        ForceTwoDigits = 0x2,
        ForceThreeDigits = 0x4,
        TalkingPlayerHack = 0x8,
    };

    public enum TextJustification : ushort
    {
        Left = 0,
        Right = 1,
        Center = 2
    };

    public enum StringFont : byte
    {
        Default = 0,
        NumberFont = 1
    };
}
