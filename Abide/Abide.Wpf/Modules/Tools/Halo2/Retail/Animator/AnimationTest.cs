using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using Abide.DebugXbox;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2;
using Abide.HaloLibrary.Halo2.Retail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.Animator
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AnimationTest : ITool
    {
        public IHost Host { get; private set; }

        public MapVersion Version => MapVersion.Halo2;

        public HaloTag SelectedEntry => (HaloTag)Host?.Request(this, "GetSelectedTag") ?? null;

        public HaloMapFile Map => (HaloMapFile)Host?.Request(this, "GetMap") ?? null;

        public Xbox Xbox => (Xbox)Host?.Request(this, "GetXbox") ?? null;

        public string Name => "Animation Test";

        public string Description => "Animation Test";

        public string Author => "Click16";

        public FrameworkElement Element => null;

        public void DebugXboxChanged()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(IHost host)
        {
            if (host != null)
            {
                Host = host;
            }
        }

        public void OnMapLoad()
        {
        }

        public void OnSelectedEntryChanged()
        {
            //if (SelectedEntry != null && SelectedEntry.GroupTag == HaloTags.jmad)
            //    if (SelectedEntry.Resources.TryGetResource(17322496, out HaloMapDataContainer resource))
            //    {
            //        List<AnimationVertexBuffer> buffer = new List<AnimationVertexBuffer>();
            //
            //        using (var stream = resource.GetVirtualStream())
            //        using (BinaryReader reader = new BinaryReader(stream))
            //        {
            //            //Read animation codec
            //            AnimationCodec codec = (AnimationCodec)reader.PeekChar();
            //
            //            StaticHeader staticHeader = new StaticHeader();
            //            AnimatedHeader animatedHeader = new AnimatedHeader();
            //            
            //            switch (codec)
            //            {
            //                case AnimationCodec.UncompressedStaticData:
            //                case AnimationCodec.UncompressedAnimatedData:
            //                case AnimationCodec.QuantizedRotationOnly:
            //                case AnimationCodec.BlendScreen:
            //                    staticHeader = reader.Read<StaticHeader>();
            //                    break;
            //                case AnimationCodec.ByteKeyframeLightlyQuantized:
            //                case AnimationCodec.WordKeyframeLightlyQuantized:
            //                case AnimationCodec.ReverseByteKeyframeLightlyQuantized:
            //                case AnimationCodec.ReverseWordKeyframeLightlyQuantized:
            //                    animatedHeader = reader.Read<AnimatedHeader>();
            //                    break;
            //            }
            //
            //            if (staticHeader.CompressionType != 0)
            //            {
            //                for (int n = 0; n < staticHeader.RotationFrameSize / 8; n++)
            //                {
            //                    float i = reader.ReadInt16() * 0.000030518509f;
            //                    float j = reader.ReadInt16() * 0.000030518509f;
            //                    float k = reader.ReadInt16() * 0.000030518509f;
            //                    float w = reader.ReadInt16() * 0.000030518509f;
            //
            //                    buffer.Add(new AnimationVertexBuffer()
            //                    {
            //                        RotationI = i,
            //                        RotationJ = j,
            //                        RotationK = k,
            //                        RotationW = w
            //                    });
            //                }
            //            }
            //            else if (animatedHeader.CompressionType != 0)
            //            {
            //
            //            }
            //            else throw new NotImplementedException();
            //        }
            //    }
        }
    }

    public enum AnimationCodec : byte
    {
        NoCompression = 0,
        UncompressedStaticData,
        UncompressedAnimatedData,
        QuantizedRotationOnly,
        ByteKeyframeLightlyQuantized,
        WordKeyframeLightlyQuantized,
        ReverseByteKeyframeLightlyQuantized,
        ReverseWordKeyframeLightlyQuantized,
        BlendScreen,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct StaticHeader
    {
        public AnimationCodec CompressionType;
        public byte TotalRotatedNodes;
        public byte TotalTranslatedNodes;
        public byte TotalScaledNodes;
        public float ErrorPercent;
        public float PlaybackRate;
        public int TranslationDataOffset;
        public int ScaleDataOffset;
        public int RotationFrameSize;
        public int TranslationFrameSize;
        public int ScaleFrameSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AnimatedHeader
    {
        public AnimationCodec CompressionType;
        public byte TotalRotatedNodes;
        public byte TotalTranslatedNodes;
        public byte TotalScaledNodes;
        public float ErrorPercent;
        public float PlaybackRate;
        public int TranslationFrameInfoOffset;
        public int ScaleFrameInfoOffset;
        public int RotationKeyFrameDataOffset;
        public int TranslationKeyFrameDataOffset;
        public int ScaleKeyFrameDataOffset;
        public int RotationDataOffset;
        public int TranslationDataOffset;
        public int ScaleDataOffset;
    }

    public struct AnimationOutInfo
    {
        public int RawDataAddress;
        public int DataSizesAddress;
        public byte NodeCount;
        public byte FrameInfoType;
        public byte FrameCount;
    }

    public struct AnimationVertexBuffer
    {
        public float RotationI, RotationJ, RotationK, RotationW;
        public float TranslationX, TranslationY, TranslationZ;
        public float Scale;
    }
}
