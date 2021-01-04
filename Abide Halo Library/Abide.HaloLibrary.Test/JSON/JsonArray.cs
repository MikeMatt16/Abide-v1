namespace Donek.Core.JSON
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonArray : JsonObject
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonObjectCollection Elements { get; } = new JsonObjectCollection();
        /// <summary>
        /// 
        /// </summary>
        public JsonArray() { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Stringify()
        {
            return $"[ {Elements.Stringify()} ]";
        }
    }
}
