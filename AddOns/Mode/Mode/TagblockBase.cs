using System;
using System.ComponentModel;

namespace Mode
{
    /// <summary>
    /// Represents a base tag block properties container.
    /// </summary>
    public abstract class TagblockProperties<TTag>
    {
        [Browsable(false)]
        public abstract uint Address
        {
            get;
        }

        protected readonly TTag tagBlock;

        public TagblockProperties(TTag tagBlock)
        {
            //Check
            if (tagBlock == null) throw new ArgumentNullException(nameof(tagBlock));

            //Set
            this.tagBlock = tagBlock;
        }
    }
}
