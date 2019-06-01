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
            get { return !string.IsNullOrEmpty(Explanation); }
        }
        /// <summary>
        /// Gets and returns the explanation.
        /// </summary>
        public string Explanation
        {
            get { if (TagField is ExplanationField explanationField) return explanationField.Explanation; return string.Empty; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplanationFieldModel"/> class.
        /// </summary>
        public ExplanationFieldModel() { }
    }
}
