namespace Donek.Core.JSON
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JsonString : JsonValueContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public new string Value
        {
            get { return (string)base.Value; }
            set { base.Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public JsonString() { Value = string.Empty; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Stringify()
        {
            return $"\"{Value.ToString()}\"";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public static implicit operator string(JsonString json)
        {
            return json.Value;
        }
    }
}
