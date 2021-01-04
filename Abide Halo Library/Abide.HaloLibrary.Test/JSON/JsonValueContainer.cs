namespace Donek.Core.JSON
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class JsonValueContainer : JsonObject
    {
        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public JsonValueContainer() { }
    }
}
