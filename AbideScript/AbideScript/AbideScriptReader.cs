using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AbideScript
{
    public class AbideScriptReader : IDisposable
    {
        private readonly TextReader reader;

        private bool isDisposed = false;

        public int LinePosition { get; private set; } = 0;

        public int LineNumber { get; private set; } = 0;

        public bool EndOfStream
        {
            get
            {
                if (isDisposed) throw new ObjectDisposedException(nameof(AbideScriptReader));
                return reader.Peek() == -1;
            }
        }

        private AbideScriptReader(TextReader reader)
        {
            this.reader = reader;
        }

        public static AbideScriptReader Create(string fileName)
        {
            //Check
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName)) throw new FileNotFoundException("Specifie file does not exist.", fileName);

            //Open
            return new AbideScriptReader(File.OpenText(fileName));
        }

        public static AbideScriptReader Create(Stream inStream)
        {
            //Check
            if (inStream == null) throw new ArgumentNullException(nameof(inStream));
            if (!inStream.CanRead) throw new ArgumentException("Stream not readable.", nameof(inStream));

            //Return reader
            return Create(new StreamReader(inStream));
        }

        public static AbideScriptReader Create(TextReader reader)
        {
            //Check
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            //Open
            return new AbideScriptReader(reader);
        }

        public void Dispose()
        {
            //Check
            if (isDisposed) throw new ObjectDisposedException(nameof(AbideScriptReader));
            isDisposed = true;

            //Reader
            reader.Dispose();
        }

        public void SkipWhitespace()
        {
            //Check and read
            while (reader.Peek() != -1 && char.IsWhiteSpace((char)reader.Peek()))
            {
                if (reader.Peek() == '\n')
                {
                    LineNumber++;
                    LinePosition = 0;
                }

                ReadCharacter();
            }
        }

        public char ReadCharacter()
        {
            if (reader.Peek() == -1) throw new EndOfStreamException();
            char c = (char)reader.Read();
            LinePosition++;
            return c;
        }

        public AbideScriptStatement ReadStatement()
        {
            List<AbideScriptToken> tokens = new List<AbideScriptToken>();

            if (SkipThenPeek() != -1)
            {
                var token = ReadToken();
                tokens.Add(token);

                switch (token.Token)
                {
                    case "global":
                    case "internal":
                    case "familial":
                    case "local":
                        if (reader.Peek() != -1)
                        {
                            var followingToken = ReadToken();
                            SkipWhitespace();

                            if (IsTypeDeclaration(followingToken))
                            {

                            }
                            else
                            {
                                if (followingToken.Token == "const")
                                {
                                    tokens.Add(followingToken);
                                    followingToken = ReadToken();
                                }

                                var firstToken = followingToken;
                                StringBuilder typeTokenBuilder = new StringBuilder();
                                switch (followingToken.Token)
                                {
                                    case "string":
                                    case "char":
                                    case "sbyte":
                                    case "byte":
                                    case "short":
                                    case "ushort":
                                    case "int":
                                    case "uint":
                                    case "long":
                                    case "ulong":
                                    case "float":
                                    case "double":
                                    case "bool":
                                        typeTokenBuilder.Append(followingToken.Token);
                                        break;

                                    case "function":
                                        typeTokenBuilder.Append(followingToken.Token);
                                        if (SkipThenPeek() == '(')
                                            typeTokenBuilder.Append(ReadToken().Token);
                                        break;

                                    default:
                                        while (SkipThenPeek() == '.')
                                        {
                                            typeTokenBuilder.Append(ReadToken().Token);
                                            if (SkipThenPeek() != -1)
                                                typeTokenBuilder.Append(ReadToken().Token);
                                        }
                                        break;
                                }

                                var typeToken = new AbideScriptToken(typeTokenBuilder.ToString(), firstToken.LineNumber, firstToken.LinePosition);
                                tokens.Add(typeToken);

                                if (IsValidObjectNameCharacter((char)SkipThenPeek()))
                                {
                                    var nameToken = ReadToken();
                                    tokens.Add(nameToken);
                                }

                                if (SkipThenPeek() == ';')
                                    tokens.Add(ReadToken());

                                else if (SkipThenPeek() == '=')
                                {
                                    tokens.Add(ReadToken());

                                    var valueToken = ReadToken();
                                    tokens.Add(valueToken);

                                    if (valueToken.Token.StartsWith('('))
                                    {
                                        if (SkipThenPeek() == '=')
                                        {
                                            var lambdaToken = ReadToken();

                                            if (lambdaToken.Token == "=>")
                                            {
                                                tokens.Add(ReadToken());
                                            }
                                        }
                                    }

                                    if (SkipThenPeek() == ';')
                                        tokens.Add(ReadToken());
                                }
                            }
                        }
                        break;

                    case "namespace":
                        if (reader.Peek() != -1)
                            tokens.Add(ReadToken());
                        if (reader.Peek() != -1)
                            tokens.Add(ReadToken());
                        break;

                    case "include":
                        if (reader.Peek() != -1)
                            tokens.Add(ReadToken());
                        if (reader.Peek() == ';')
                            tokens.Add(ReadToken());
                        break;
                }
            }

            return new AbideScriptStatement(tokens);
        }

        public AbideScriptToken ReadToken()
        {
            int tokenLine = 0;
            int tokenStart = 0;
            StringBuilder tokenBuilder = new StringBuilder();
            Func<StringBuilder, AbideScriptToken> createToken = (sb) =>
            {
                return new AbideScriptToken(sb.ToString(), tokenLine, tokenStart);
            };

            SkipWhitespace();
            while (reader.Peek() != -1)
            {
                char peekChar = (char)reader.Peek();
                tokenLine = LineNumber;
                tokenStart = LinePosition;

                if (IsValidObjectNameCharacter(peekChar))
                {
                    ReadObjectNameToken(tokenBuilder);
                    return createToken.Invoke(tokenBuilder);
                }

                switch (peekChar)
                {
                    case '.':
                    case ';':
                    case ',':
                    case '+':
                    case '-':
                    case '^':
                    case '!':
                        tokenBuilder.Append(ReadCharacter());
                        switch (SkipThenPeek())
                        {
                            case '=':
                                tokenBuilder.Append(ReadCharacter());
                                break;
                        }
                        return createToken.Invoke(tokenBuilder);

                    case '>':
                        if (reader.Peek() == '>') tokenBuilder.Append(ReadCharacter());
                        switch (SkipThenPeek())
                        {
                            case '>':
                            case '=':
                                tokenBuilder.Append(ReadCharacter());
                                break;
                        }
                        return createToken.Invoke(tokenBuilder);
                    case '<':
                        if (reader.Peek() == '<') tokenBuilder.Append(ReadCharacter());
                        switch (SkipThenPeek())
                        {
                            case '<':
                            case '=':
                                tokenBuilder.Append(ReadCharacter());
                                break;
                        }
                        return createToken.Invoke(tokenBuilder);
                    case '=':
                        if (reader.Peek() == '=') tokenBuilder.Append(ReadCharacter());
                        switch (SkipThenPeek())
                        {
                            case '>':
                            case '=':
                                tokenBuilder.Append(ReadCharacter());
                                break;
                        }
                        return createToken.Invoke(tokenBuilder);
                    case '&':
                        if (reader.Peek() == '&') tokenBuilder.Append(ReadCharacter());
                        switch (SkipThenPeek())
                        {
                            case '&':
                                tokenBuilder.Append(ReadCharacter());
                                break;
                        }
                        return createToken.Invoke(tokenBuilder);
                    case '|':
                        if (reader.Peek() == '|') tokenBuilder.Append(ReadCharacter());
                        switch (SkipThenPeek())
                        {
                            case '|':
                                tokenBuilder.Append(ReadCharacter());
                                break;
                        }
                        return createToken.Invoke(tokenBuilder);

                    case '{':
                        ReadTokenBoundedByCurlyBraces(tokenBuilder);
                        return createToken.Invoke(tokenBuilder);
                    case '(':
                        ReadTokenBoundedByParenthesis(tokenBuilder);
                        return createToken.Invoke(tokenBuilder);
                    case '"':
                        ReadTokenBoundedByDoubleQuotationMarks(tokenBuilder);
                        return createToken.Invoke(tokenBuilder);
                    case '\'':
                        ReadTokenBoundedBySingleQuotationMarks(tokenBuilder);
                        return createToken.Invoke(tokenBuilder);

                    case '/':
                        tokenBuilder.Append(ReadCharacter());
                        if(reader.Peek() == '/' || reader.Peek() == '*')
                        {
                            ReadCommentToken(tokenBuilder);
                            return createToken.Invoke(tokenBuilder);
                        }
                        return createToken.Invoke(tokenBuilder);
                }

                if (IsPossibleNumberCharacter(peekChar))
                {
                    ReadNumberToken(tokenBuilder);
                    return createToken.Invoke(tokenBuilder);
                }

                tokenBuilder.Append(ReadCharacter());
                return createToken.Invoke(tokenBuilder);
            }

            return null;
        }
        
        private void ReadObjectNameToken(StringBuilder stringBuilder)
        {
            SkipWhitespace();

            while (reader.Peek() != -1)
            {
                char peekChar = (char)reader.Peek();
                if (IsValidObjectNameCharacter(peekChar))
                    stringBuilder.Append(ReadCharacter());
                else break;
            }
        }

        private void ReadTokenBoundedByDoubleQuotationMarks(StringBuilder tokenBuilder)
        {
            SkipWhitespace();

            if (reader.Peek() == '"') tokenBuilder.Append(ReadCharacter());
            while (reader.Peek() != -1)
            {
                char currentChar = ReadCharacter();
                tokenBuilder.Append(currentChar);
                if (currentChar == '\\')
                    tokenBuilder.Append(ReadCharacter());
                char peekCharacter = (char)reader.Peek();
                if (peekCharacter == '"' || peekCharacter == '\r' || peekCharacter == '\n') break;
            }

            if (reader.Peek() == '"') tokenBuilder.Append(ReadCharacter());
        }

        private void ReadTokenBoundedBySingleQuotationMarks(StringBuilder tokenBuilder)
        {
            SkipWhitespace();

            if (reader.Peek() == '\'') tokenBuilder.Append(ReadCharacter());
            while (reader.Peek() != -1)
            {
                char currentChar = ReadCharacter();
                tokenBuilder.Append(currentChar);
                if (currentChar == '\\')
                    tokenBuilder.Append(ReadCharacter());
                char peekCharacter = (char)reader.Peek();
                if (peekCharacter == '\'' || peekCharacter == '\r' || peekCharacter == '\n') break;
            }

            if (reader.Peek() == '\'') tokenBuilder.Append(ReadCharacter());
        }

        private void ReadTokenBoundedByCurlyBraces(StringBuilder tokenBuilder)
        {
            SkipWhitespace();

            int indent = 0;
            if (reader.Peek() == '{') indent++;
            while (reader.Peek() != -1 && indent > 0)
            {
                tokenBuilder.Append(ReadCharacter());
                char peekCharacter = (char)reader.Peek();
                if (peekCharacter == '{') indent++;
                if (peekCharacter == '}') indent--;
            }

            if (reader.Peek() == '}') tokenBuilder.Append(ReadCharacter());
        }

        private void ReadTokenBoundedByParenthesis(StringBuilder tokenBuilder)
        {
            SkipWhitespace();

            int indent = 0;
            if (reader.Peek() == '(') indent++;
            while (reader.Peek() != -1 && indent > 0)
            {
                tokenBuilder.Append(ReadCharacter());
                char peekCharacter = (char)reader.Peek();
                if (peekCharacter == '(') indent++;
                if (peekCharacter == ')') indent--;
            }

            if (reader.Peek() == ')') tokenBuilder.Append(ReadCharacter());
        }

        private void ReadNumberToken(StringBuilder tokenBuilder)
        {
            SkipWhitespace();
            int decimalPointCount = 0;
            int signCount = 0;

            if (reader.Peek() == '.') tokenBuilder.Append('0');
            while (reader.Peek() != -1 && IsPossibleNumberCharacter((char)reader.Peek()))
            {
                char peekCharacter = (char)reader.Peek();
                if (peekCharacter == '.') decimalPointCount++;
                if (peekCharacter == '-') signCount++;
                if (decimalPointCount > 1 || signCount > 1) break;
                tokenBuilder.Append(ReadCharacter());
            }
        }

        private void ReadCommentToken(StringBuilder tokenBuilder)
        {
            bool inline = reader.Peek() == '*';
            if (reader.Peek() == '/') tokenBuilder.Append(ReadCharacter());
            else if (reader.Peek() == '*') tokenBuilder.Append(ReadCharacter());

            if (inline)
                while (reader.Peek() != -1)
                {
                    char c = ReadCharacter();
                    tokenBuilder.Append(c);
                    if (c == '*' && reader.Peek() == '/')
                    {
                        tokenBuilder.Append(ReadCharacter());
                        return;
                    }
                }
            else
                while (reader.Peek() != -1 && reader.Peek() != '\n')
                    tokenBuilder.Append(ReadCharacter());
        }
        
        [DebuggerStepThrough]
        private int SkipThenPeek()
        {
            SkipWhitespace();
            return reader.Peek();
        }

        [DebuggerStepThrough]
        private static bool IsTypeDeclaration(AbideScriptToken token)
        {
            if (token == null) return false;

            switch (token.Token)
            {
                case "struct":
                case "class":
                case "enum":
                    return true;
            }

            return false;
        }

        [DebuggerStepThrough]
        private static bool IsPossibleNumberCharacter(char c)
        {
            if (char.IsNumber(c)) return true;
            if (c == '.') return true;
            if (c == '-') return true;
            return false;
        }
        
        [DebuggerStepThrough]
        private static bool IsValidObjectNameCharacter(char c)
        {
            if (char.IsLetter(c)) return true;
            if (c == '_') return true;
            return false;
        }
        
        [DebuggerStepThrough]
        private static bool IsValidObjectName(string token)
        {
            if (token == null) return false;
            if (token.Length == 0) return false;

            if (char.IsLetter(token[0]) || token[0] == '_')
            {
                for (int i = 1; i < token.Length; i++)
                    if (!char.IsLetterOrDigit(token[i]) && token[i] != '_')
                        return false;
            }

            return true;
        }
    }
}
