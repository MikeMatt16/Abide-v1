using System.IO;
using System.Text;

namespace Donek.Core.JSON
{
    /// <summary>
    /// Represents a base JSON object.
    /// </summary>
    public abstract class JsonObject
    {
        /// <summary>
        /// Converts this JSON object to a string.
        /// </summary>
        /// <returns>A JSON-formatted string.</returns>
        public abstract string Stringify();
        /// <summary>
        /// Converts the specified string to a JSON object.
        /// </summary>
        /// <param name="json">The JSON string. This value can be null.</param>
        /// <returns>A JSON object representing the specified string.</returns>
        public static JsonObject Parse(string json)
        {
            //Check
            if (string.IsNullOrEmpty(json)) return new JsonNull();

            //Create reader
            using (JsonStringReader reader = new JsonStringReader(json))
            {
                //Read first token
                if (reader.PeekToken() == -1) return new JsonNull();
                string token = reader.ReadToken();

                //Check peek char
                switch (GetTokenType(token))
                {
                    case JsonTokenType.Number:
                        return new JsonNumber() { Value = decimal.Parse(token) };

                    case JsonTokenType.String:
                        return new JsonString() { Value = FormatString(token) };

                    case JsonTokenType.Collection:
                        JsonElementCollection collection = new JsonElementCollection();
                        using (JsonStringReader collectionReader = new JsonStringReader(token))
                        {
                            //Read open brace
                            collectionReader.Read();

                            //Loop
                            while (collectionReader.PeekToken() != '}')
                            {
                                //Read key-value pair
                                string nameToken = collectionReader.ReadString();
                                collectionReader.ReadToken();
                                string valueToken = collectionReader.ReadToken();

                                //Check
                                if (collectionReader.PeekToken() == ',') collectionReader.ReadToken();
                                collection.Add(new JsonElement() { Name = FormatString(nameToken), Value = Parse(valueToken) });
                            }

                            //Read close brace
                            collectionReader.Read();
                        }
                        return collection;

                    case JsonTokenType.Array:
                        JsonObjectCollection array = new JsonObjectCollection();
                        using (JsonStringReader arrayReader = new JsonStringReader(token))
                        {
                            //Read open bracket
                            arrayReader.Read();

                            //Loop
                            while (arrayReader.PeekToken() != ']')
                            {
                                //Read object
                                string elementToken = arrayReader.ReadToken();

                                //Check
                                if (arrayReader.PeekToken() == ',') arrayReader.ReadToken();
                                array.Add(Parse(elementToken));
                            }

                            //Read close bracket
                            arrayReader.Read();
                        }
                        return array;

                    case JsonTokenType.Literal:
                        if (token == "true" || token == "false") return new JsonBoolean() { Value = bool.Parse(token) };
                        else if (token == "null") return new JsonNull();
                        throw new InvalidDataException("Only the 'true', 'false', and 'null' literals are supported.");
                }
            }

            //Return
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="object"></param>
        /// <returns></returns>
        public static bool TryParse(string json, out JsonObject @object)
        {
            //Prepare
            bool succeeded = true;
            JsonObject newObject = null;

            //Parse
            try { newObject = Parse(json); }
            catch { succeeded = false; }

            //Set
            @object = newObject;
            return succeeded;
        }
        /// <summary>
        /// Returns a string representation of this JSON object.
        /// </summary>
        /// <returns>A JSON string.</returns>
        public override string ToString()
        {
            return Stringify();
        }

        private static string FormatString(string str)
        {
            //Prepare
            StringBuilder sb = new StringBuilder();

            //Create reader
            using (StringReader reader = new StringReader(str ?? string.Empty))
            {
                //Check
                if (reader.Peek() == '"')
                {
                    //Read opening quote   
                    reader.Read();

                    //Peek
                    while (reader.Peek() != -1 && reader.Peek() != '"')
                    {
                        //Check
                        if (reader.Peek() != '\\') sb.Append((char)reader.Read());
                        else
                        {
                            System.Diagnostics.Debugger.Break();
                        }
                    }

                    //Read closeing quote
                    reader.Read();
                }
            }

            //Return
            return sb.ToString();
        }
        private static JsonTokenType GetTokenType(string token)
        {
            //Prepare
            JsonTokenType type = JsonTokenType.Literal;
            using (JsonStringReader reader = new JsonStringReader(token))
            {
                switch (reader.PeekToken())
                {
                    case '{': type = JsonTokenType.Collection; break;
                    case '[': type = JsonTokenType.Array; break;
                    case '"': type = JsonTokenType.String; break;
                    case '-':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9': type = JsonTokenType.Number; break;
                }
            }

            //Return
            return type;
        }
        private enum JsonTokenType
        {
            Number,
            String,
            Literal,
            Collection,
            Array,
        }
    }
}
