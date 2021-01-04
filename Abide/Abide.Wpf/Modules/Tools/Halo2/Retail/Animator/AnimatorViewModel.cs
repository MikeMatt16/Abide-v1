using Abide.HaloLibrary.Halo2;
using Abide.HaloLibrary.Halo2.Retail.Tag;
using Abide.HaloLibrary.Halo2.Retail.Tag.Generated;
using Abide.Wpf.Modules.ViewModel;
using System.IO;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.Animator
{
    public sealed class AnimatorViewModel : BaseAddOnViewModel
    {
        private ModelAnimationGraph modelAnimationGraph = new ModelAnimationGraph();
        protected override void OnSelectedTagChanged()
        {
            //if (SelectedTag?.GroupTag == HaloTags.jmad)
            //{
            //    var stream = SelectedTag.Data.GetVirtualStream();
            //    stream.Seek(SelectedTag.Address, SeekOrigin.Begin);
            //
            //    using (var reader = stream.CreateReader())
            //        modelAnimationGraph.Read(reader);
            //
            //    ModelAnimationGraphBlock block = (ModelAnimationGraphBlock)modelAnimationGraph.TagBlocks[0];
            //    BlockList animationsList = ((BlockField)block.Fields[6]).BlockList;
            //}
        }
    }
}
