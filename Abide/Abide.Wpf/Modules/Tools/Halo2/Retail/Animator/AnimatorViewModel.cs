using Abide.HaloLibrary.Halo2;
using Abide.HaloLibrary.Halo2.Retail.Tag;
using Abide.HaloLibrary.Halo2.Retail.Tag.Generated;
using System.IO;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.Animator
{
    public sealed class AnimatorViewModel : BaseAddOnViewModel
    {
        private ModelAnimationGraph modelAnimationGraph = new ModelAnimationGraph();
        protected override void OnSelectedEntryChanged()
        {
            if (SelectedEntry?.Root == HaloTags.jmad)
            {
                var stream = SelectedEntry.Data.GetVirtualStream();
                stream.Seek(SelectedEntry.Address, SeekOrigin.Begin);

                using (var reader = stream.CreateReader())
                    modelAnimationGraph.Read(reader);

                ModelAnimationGraphBlock block = (ModelAnimationGraphBlock)modelAnimationGraph.TagBlocks[0];
                BlockList animationsList = ((BlockField)block.Fields[6]).BlockList;
            }
        }
    }
}
