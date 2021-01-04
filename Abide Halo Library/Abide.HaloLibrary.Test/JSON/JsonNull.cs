namespace Donek.Core.JSON
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JsonNull : JsonObject
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonNull() { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Stringify()
        {
            return "null";
        }
    }
}
