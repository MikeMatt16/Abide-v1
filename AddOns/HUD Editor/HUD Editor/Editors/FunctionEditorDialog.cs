using HUD_Editor.Controls;
using HUD_Editor.Halo2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HUD_Editor.Editors
{
    public partial class FunctionEditorDialog : Form
    {
        private Dictionary<int, MetaControl> controls = new Dictionary<int, MetaControl>();

        public byte[] Data
        {
            get { return data; }
        }

        private byte[] data;
        private DataType dataType;

        private FunctionEditorDialog() : this(new byte[0]) { }
        public FunctionEditorDialog(byte[] data)
        {
            //Initialize
            InitializeComponent();

            //Add...
            dataTypeComboBox.Items.Add(new DataType(typeof(double)));
            dataTypeComboBox.Items.Add(new DataType(typeof(float)));
            dataTypeComboBox.Items.Add(new DataType(typeof(int)));
            dataTypeComboBox.Items.Add(new DataType(typeof(short)));
            dataTypeComboBox.Items.Add(new DataType(typeof(sbyte)));
            dataTypeComboBox.Items.Add(new DataType(typeof(long)));
            dataTypeComboBox.Items.Add(new DataType(typeof(uint)));
            dataTypeComboBox.Items.Add(new DataType(typeof(ushort)));
            dataTypeComboBox.Items.Add(new DataType(typeof(byte)));
            dataTypeComboBox.Items.Add(new DataType(typeof(ulong)));

            //Set Data
            this.data = data;

            //Set Element Count Label
            elementCountLabel.Text = $"Length: {data.Length} ({data.Length * 8} bits)";
        }
        
        private void dataTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check
            if (dataTypeComboBox.SelectedItem is DataType)
            {
                //Get Data Type
                dataType = (DataType)dataTypeComboBox.SelectedItem;

                //Get Bit Count
                int bitCount = data.Length * 8;
                int dataLength = dataType.Length * 8;

                //Begin
                containerPanel.SuspendLayout();

                //Clear Panel
                containerPanel.Controls.Clear();

                //Clear Dictionary
                foreach (var control in controls)
                    control.Value.Dispose();
                controls.Clear();

                //Create
                using (MemoryStream ms = new MemoryStream(data))
                using (BinaryReader reader = new BinaryReader(ms))
                    for (int i = 0; i < bitCount; i += dataLength)
                    {
                        //Get Offset
                        int offset = i / 8;

                        //Prepare
                        ValueControl valueControl = new ValueControl();
                        valueControl.Address = offset;
                        valueControl.Type = dataType.Name;
                        valueControl.ControlName = $"{dataType.Name} 0x{offset:X}";
                        valueControl.Value = dataType.Read(reader).ToString();
                        valueControl.ValueChanged = valueControl_ValueChanged;

                        //Add
                        controls.Add(offset, valueControl);
                        containerPanel.Controls.Add(valueControl);
                    }

                //End
                containerPanel.ResumeLayout();
            }
        }

        private void valueControl_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            using (MemoryStream ms = new MemoryStream(data))
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Goto
                ms.Seek(((MetaControl)sender).Address, SeekOrigin.Begin);
                dataType.Write(writer, e.Value);
            }
        }

        private sealed class DataType
        {
            public string Name
            {
                get { return dataType.Name; }
            }
            public Type Type
            {
                get { return dataType; }
            }
            public int Length
            {
                get { return dataLength; }
            }
            
            private readonly Type dataType;
            private readonly int dataLength;

            public DataType(Type dataType)
            {
                //Check
                if (dataType == null) throw new ArgumentNullException(nameof(dataType));

                //Try
                try
                {
                    this.dataType = dataType;
                    dataLength = Marshal.SizeOf(dataType);
                }
                catch (ArgumentException) { throw new ArgumentException("Unable to marshal supplied type.", nameof(dataType)); }
            }
            public object Read(BinaryReader reader)
            {
                byte[] buffer = reader.ReadBytes(dataLength);
                switch (Type.GetTypeCode(dataType))
                {
                    case TypeCode.Double: return BitConverter.ToDouble(buffer, 0);
                    case TypeCode.Single: return BitConverter.ToSingle(buffer, 0);
                    case TypeCode.Int64: return BitConverter.ToInt64(buffer, 0);
                    case TypeCode.Int32: return BitConverter.ToInt32(buffer, 0);
                    case TypeCode.Int16: return BitConverter.ToInt16(buffer, 0);
                    case TypeCode.SByte: return (sbyte)buffer[0];
                    case TypeCode.UInt64: return BitConverter.ToUInt64(buffer, 0);
                    case TypeCode.UInt32: return BitConverter.ToUInt32(buffer, 0);
                    case TypeCode.UInt16: return BitConverter.ToUInt16(buffer, 0);
                    case TypeCode.Byte: return buffer[0];
                    default: return null;
                }
            }
            public void Write(BinaryWriter writer, string value)
            {
                byte[] buffer = new byte[0];
                object dataValue = Convert.ChangeType(value, dataType);
                switch (Type.GetTypeCode(dataType))
                {
                    case TypeCode.Double: buffer = BitConverter.GetBytes((double)dataValue); break;
                    case TypeCode.Single: buffer = BitConverter.GetBytes((float)dataValue); break;
                    case TypeCode.Int64: buffer = BitConverter.GetBytes((long)dataValue); break;
                    case TypeCode.Int32: buffer = BitConverter.GetBytes((int)dataValue); break;
                    case TypeCode.Int16: buffer = BitConverter.GetBytes((short)dataValue); break;
                    case TypeCode.UInt64: buffer = BitConverter.GetBytes((ulong)dataValue); break;
                    case TypeCode.UInt32: buffer = BitConverter.GetBytes((uint)dataValue); break;
                    case TypeCode.UInt16: buffer = BitConverter.GetBytes((ushort)dataValue); break;

                    case TypeCode.SByte:
                    case TypeCode.Byte: buffer = new byte[] { (byte)dataValue }; break;
                }

                //Write
                writer.Write(buffer);
            }
            public override string ToString()
            {
                return dataType.Name;
            }
        }
    }

    public sealed class FunctionEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //Prepare
            Function function = null;

            //Check
            if(value != null && value is Function)
            {
                //Setup
                function = (Function)value;

                //Prepare
                using (FunctionEditorDialog funcDlg = new FunctionEditorDialog(function.Data))
                {
                    if (funcDlg.ShowDialog() == DialogResult.OK)
                        function = new Function(funcDlg.Data);
                }

                //Setup
                return function;
            }

            //Return
            return base.EditValue(context, provider, value);
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
