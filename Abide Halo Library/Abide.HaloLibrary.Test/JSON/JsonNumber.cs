namespace Donek.Core.JSON
{
    /// <summary>
    /// Represents a numerical value in JSON.
    /// </summary>
    public sealed class JsonNumber : JsonValueContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public new decimal Value
        {
            get { return (decimal)base.Value; }
            set { base.Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public JsonNumber() { Value = 0; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Stringify()
        {
            return Value.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public static implicit operator decimal(JsonNumber json)
        {
            return json.Value;
        }
    }
}
