using Abide.Guerilla.H2Guerilla;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents an explaination definition.
    /// </summary>
    public sealed class ExplanationDefinition : TagFieldDefinition
    {
        /// <summary>
        /// Gets and returns this field's explanation.
        /// </summary>
        public string Explanation
        {
            get { return explanation; }
        }

        private string explanation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplanationDefinition"/> class.
        /// </summary>
        public ExplanationDefinition() : base() { }
        /// <summary>
        /// Reads the field using the supplied guerilla reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal override void Read(GuerillaBinaryReader reader)
        {
            //Read
            base.Read(reader);

            //Read string
            explanation = reader.ReadLocalizedString(DefinitionAddress);
        }
        /// <summary>
        /// Returns a string that represents this explanation field definition.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{Name}: {explanation}";
        }
    }
}
