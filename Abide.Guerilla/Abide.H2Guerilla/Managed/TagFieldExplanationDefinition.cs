using Abide.H2Guerilla.H2Guerilla;

namespace Abide.H2Guerilla.Managed
{
    /// <summary>
    /// Represents an explaination field definition.
    /// </summary>
    public sealed class TagFieldExplanationDefinition : TagFieldDefinition
    {
        /// <summary>
        /// Gets and returns this field's explanation.
        /// </summary>
        public string Explanation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagFieldExplanationDefinition"/> class.
        /// </summary>
        public TagFieldExplanationDefinition() : base() { }
        /// <summary>
        /// Reads the field using the supplied guerilla reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal override void Read(GuerillaBinaryReader reader)
        {
            //Read
            base.Read(reader);

            //Read string
            Explanation = reader.ReadLocalizedString(DefinitionAddress);
        }
        /// <summary>
        /// Returns a string that represents this explanation field definition.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{Name}: {Explanation}";
        }
    }
}
