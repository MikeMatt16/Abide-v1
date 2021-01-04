namespace Donek.Core.JSON
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JsonBoolean : JsonValueContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public new bool Value
        {
            get { return (bool)base.Value; }
            set { base.Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public JsonBoolean() { Value = false; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Stringify()
        {
            return Value ? "true" : "false";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public static implicit operator bool(JsonBoolean json)
        {
            return json.Value;
        }
    }
}
