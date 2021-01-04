using System.IO;
using System.Text;

namespace Donek.Core.JSON
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JsonStringReader : StringReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public JsonStringReader(string json) : base(json) { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReadToken()
        {
            //Skip whitespace
            ReadWhitespace();

            //Peek
            int peek = Peek();
            if (peek == -1) return null;

            //Check peek character
            switch (peek)
            {
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
                case '9': return ReadNumber();

                case '.':
                case ',':
                case ':': return $"{(char)Read()}";

                case '{':
                case '}': return ReadCollection();

                case '[':
                case ']': return ReadArray();

                case '\"': return ReadString();

                default: return ReadLiteral();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PeekToken()
        {
            //Skip
            ReadWhitespace();

            //Return
            return Peek();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReadCollection()
        {
            //Prepare
            StringBuilder sb = new StringBuilder();

            //Check
            if (PeekToken() == '{')
            {
                //Read open brace
                sb.Append((char)Read());

                //Read elements
                while (PeekToken() != '}' && PeekToken() != -1)
                {
                    //Read Element
                    string element = ReadElement();
                    if (element == null) throw new InvalidDataException("End of file reached, was expecting '}'."); 
                    sb.Append(element); //Append

                    //Check next token
                    char peekChar = (char)PeekToken();
                    if (PeekToken() != ',' && PeekToken() != '}') throw new InvalidDataException("Invalid token. Expecting ',' or '}'.");
                    sb.Append(ReadToken()); //Append token
                }

                //Read close brace
                sb.Append((char)Read());
            }

            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReadArray()
        {
            //Skip whitespace
            ReadWhitespace();

            //Prepare
            StringBuilder sb = new StringBuilder();

            //Check
            if (PeekToken() == '[')
            {
                //Read open bracket
                sb.Append((char)Read());

                //Read elements
                while (PeekToken() != ']' && PeekToken() != -1)
                {
                    //Peek
                    char peekChar = (char)PeekToken();

                    //Read Element
                    string element = ReadToken();
                    if (element == null) throw new InvalidDataException("End of file reached, was expecting '}'.");

                    //Append
                    sb.Append(element);

                    //Check next token
                    peekChar = (char)PeekToken();
                    if (PeekToken() != ',' && PeekToken() != ']') throw new InvalidDataException("Invalid token. Expecting ',' or ']'.");
                    sb.Append(ReadToken()); //Append token
                }

                //Read close bracket
                sb.Append((char)Read());
            }

            //Return
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReadElement()
        {
            //Read name and value
            string name = ReadString();
            if (string.IsNullOrEmpty(name)) return null;
            if (PeekToken() != ':') throw new InvalidDataException("Invalid token. Expecting ':'.");
            else ReadToken();
            char peekChar = (char)PeekToken();
            string value = ReadToken();

            //Return
            return $"{name}:{value}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReadString()
        {
            //Check
            if (Peek() == -1) return null;
            ReadWhitespace();

            //Return
            StringBuilder sb = new StringBuilder();

            //Check
            if (Peek() == '\"')
            {
                //Append
                sb.Append((char)Read());

                //Loop
                while (Peek() != -1 && Peek() != '\"')
                    if (Peek() == '\\')
                    {
                        StringBuilder sequence = new StringBuilder();
                        sequence.Append((char)Read());
                        sequence.Append((char)Read());
                    }
                    else sb.Append((char)Read());

                //Check
                if (Peek() == '\"') sb.Append((char)Read());
                else if (Peek() == -1) throw new InvalidDataException("Invalid token. Was expecting '\"'.");
            }

            //Return
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReadNumber()
        {
            //Check
            if (Peek() == -1) return null;
            ReadWhitespace();

            //Prepare
            StringBuilder sb = new StringBuilder();
            int decimalCount = 0;

            //Check first character
            if (PeekToken() == '-') sb.Append((char)Read());

            //Check if next character is digit
            if (!char.IsDigit((char)PeekToken()))
                throw new InvalidDataException("Invalid number.");

            //Loop
            while (char.IsDigit((char)PeekToken()) || PeekToken() == '.' && decimalCount < 2)
            {
                //Check
                if (Peek() == '.') decimalCount++;
                if (decimalCount > 1) break;

                //Append
                sb.Append((char)Read());
            }

            //Return
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReadLiteral()
        {
            //Check
            if (Peek() == -1) return null;
            ReadWhitespace();

            //Prepare
            StringBuilder sb = new StringBuilder();
            int peek;

            do
            {
                //Peek
                peek = Peek();
                char peekChar = (char)peek;

                //Check
                if (!char.IsLetterOrDigit(peekChar)) break;

                //Append
                sb.Append((char)Read());
            }
            while (peek != -1);

            //Return
            return sb.ToString();
        }
        private string ReadWhitespace()
        {
            //Prepare
            StringBuilder sb = new StringBuilder();

            //Loop
            while (char.IsWhiteSpace((char)Peek()) && Peek() != -1)
                sb.Append((char)Read());

            //Return
            return sb.ToString();
        }
    }
}
