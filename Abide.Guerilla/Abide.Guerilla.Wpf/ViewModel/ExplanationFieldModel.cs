using Abide.Tag;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents an explanation field container.
    /// </summary>
    public class ExplanationFieldModel : FieldModel
    {
        /// <summary>
        /// Gets and returns <see langword="true"/> if the field has an explanation; otherwise, <see langword="false"/>.
        /// </summary>
        public bool HasExplanation
        {
            get { return hasExplanation; }
            set
            {
                if(hasExplanation!= value)
                {
                    hasExplanation = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets and returns the explanation.
        /// </summary>
        public string Explanation
        {
            get { return explanation; }
            set
            {
                if (explanation != value)
                {
                    explanation = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets or sets the explanation field this object wraps.
        /// </summary>
        public new ExplanationField TagField
        {
            get { return (ExplanationField)base.TagField; }
            set
            {
                if (base.TagField != value)
                {
                    base.TagField = value;
                    if (value != null)
                    {
                        HasExplanation = !string.IsNullOrEmpty(value.Explanation);
                        Explanation = value.Explanation;
                    }
                }
            }
        }

        private string explanation = string.Empty;
        private bool hasExplanation = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplanationFieldModel"/> class.
        /// </summary>
        public ExplanationFieldModel() { }
    }
}
