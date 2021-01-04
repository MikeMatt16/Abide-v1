namespace Donek.Core.JSON
{
    /// <summary>
    /// Represents a JSON element.
    /// </summary>
    public class JsonElement : JsonObject
    {
        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the value of the element.
        /// </summary>
        public JsonObject Value { get; set; } = null;
        /// <summary>
        /// Converts this JSON element to a string.
        /// </summary>
        /// <returns>A JSON-formatted string representing this element.</returns>
        public override string Stringify()
        {
            return $"\"{Name}\": {Value?.Stringify() ?? "null"}";
        }
    }
}
