using Abide.AddOnApi.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Abide.Wpf.Modules.ViewModel
{
    public class SolutionViewModel : DependencyObject
    {
        private static readonly DependencyPropertyKey SolutionItemsPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(SolutionItems), typeof(ObservableCollection<SolutionViewModel>), typeof(SolutionViewModel),
                new PropertyMetadata(new ObservableCollection<SolutionViewModel>()));

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(nameof(Name), typeof(string), typeof(SolutionViewModel), new PropertyMetadata("Untitled"));
        public static readonly DependencyProperty SolutionItemsProperty =
            SolutionItemsPropertyKey.DependencyProperty;

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public ObservableCollection<SolutionItemViewModel> SolutionItems
        {
            get => (ObservableCollection<SolutionItemViewModel>)GetValue(SolutionItemsProperty);
            private set => SetValue(SolutionItemsPropertyKey, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionViewModel"/> class.
        /// </summary>
        /// <param name="name">The name of the solution.</param>
        public SolutionViewModel(string name = "Untitled")
        {
            Name = name ?? string.Empty;
        }

        public static SolutionViewModel LoadFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Invalid solution file path.", nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open specified solution file.", path);
            }

            SolutionViewModel model = new SolutionViewModel(Path.GetFileNameWithoutExtension(path));

            using (SolutionReader reader = new SolutionReader(File.OpenText(path)))
            {
                reader.ReadSolution();
            }

            return model;
        }

        private sealed class SolutionReader : IDisposable
        {
            private const string Header = "Abide Solution File, Format Version 1.0";

            public bool IsDisposed { get; private set; } = false;
            public TextReader Reader { get; }

            public SolutionReader(string path)
            {
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentException("Invalid path.", nameof(path));
                }

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Specified file does not exist.", path);
                }

                Reader = File.OpenText(path);
            }
            public SolutionReader(TextReader reader)
            {
                Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            }
            public void ReadSolution()
            {
                if (IsDisposed)
                {
                    return;
                }

                SkipWhitespace();
                string line = Reader.ReadLine();

                // Check
                if (line == Header)
                {
                    while (Reader.Peek() != -1)
                    {
                        SkipToNextToken();
                        line = Reader.ReadLine();

                        List<string> tokens = new List<string>();
                        using (var reader = new SolutionReader(new StringReader(line.Trim())))
                        {
                            string token = reader.ReadToken();
                            while (token != null)
                            {
                                tokens.Add(token);
                                reader.SkipToNextToken();
                                token = reader.ReadToken();
                            }
                        }
                    }
                }
            }
            private string ReadToken()
            {
                if (IsDisposed)
                {
                    return null;
                }

                if (Reader.Peek() == -1)
                {
                    return null;
                }

                if (char.IsWhiteSpace((char)Reader.Peek()))
                {
                    return null;
                }

                char peekChar = (char)Reader.Peek();

                StringBuilder tokenBuilder = new StringBuilder();

                if (peekChar == '#')
                {
                    _ = tokenBuilder.Append(Reader.ReadLine().Trim());
                }
                else if (peekChar == '=')
                {
                    _ = tokenBuilder.Append((char)Reader.Read());
                }
                else
                {
                    while (Reader.Peek() != -1 && !char.IsWhiteSpace((char)Reader.Peek()))
                    {
                        _ = tokenBuilder.Append((char)Reader.Read());
                    }
                }

                // TODO: append token string builder character by character until reaching an invalid character.
                //  Token cannot start with a number and have appended characters such as '4Head' but can end with a number such as 'Placeholder3'
                //  Token assignment is separated by an equals sign (=) where the token is on the right side and the value is on the left.
                //  The equals sign should be read as its own token. (Calling ReadToken() when the reader is peeking an equals sign
                //      should just return a single equals sign.

                return tokenBuilder.ToString();
            }
            private string ReadNameToken()
            {
                if (Reader.Peek() == -1)
                {
                    return null;
                }

                return string.Empty;
            }
            private void SkipWhitespace()
            {
                if (IsDisposed)
                {
                    return;
                }

                while (Reader.Peek() != -1 && char.IsWhiteSpace((char)Reader.Peek()))
                {
                    _ = Reader.Read();
                }
            }
            private void SkipToNextToken()
            {
                if (IsDisposed)
                {
                    return;
                }

                if (Reader.Peek() != -1)
                {
                    SkipWhitespace();

                    if (Reader.Peek() == '#')
                    {
                        _ = Reader.ReadLine();
                    }
                }
            }
            public void Dispose()
            {
                if (IsDisposed)
                {
                    return;
                }

                IsDisposed = true;

                Reader.Dispose();
            }
        }
    }
}
