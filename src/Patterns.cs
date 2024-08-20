namespace codecrafters_grep.src
{
    internal static class Patterns
    {
        private static readonly Dictionary<string, Func<string, string, bool>> _patterns = new() {
            {
                "contains", (inputLine, pattern) =>
                {
                    return inputLine.Contains(pattern);
                }
            },
            {
                "\\d", (inputLine, pattern) =>
                {
                    foreach(char c in inputLine)
                    {
                        if(char.IsAsciiDigit(c))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            },
            {
                "\\w", (inputLine, pattern) =>
                {
                    foreach(char c in inputLine)
                    {
                        if(char.IsAsciiDigit(c) || char.IsAsciiLetter(c) || c == '_')
                        {
                            return true;
                        }
                    }
                    
                    return false;
                }
            },
            {
                "[]", (inputLine, pattern) =>
                {
                    var patternChars = pattern[1..^1];

                    foreach(char c in patternChars)
                    {
                        return inputLine.Contains(c);
                    }

                    return false;
                }
            }
        };

        internal static bool MatchPattern(string inputLine, string inputPattern)
        {
            if (inputPattern.Length == 1)
            {
                return _patterns["contains"](inputLine, inputPattern);
            }
            else if(inputPattern.StartsWith('[') && inputPattern.EndsWith(']'))
            {
                return _patterns["[]"](inputLine, inputPattern);
            }
            else if (_patterns.TryGetValue(inputPattern, out Func<string, string, bool>? pattern))
            {
                return pattern(inputLine, inputPattern);
            }
            else
            {
                throw new ArgumentException($"Unhandled pattern: {inputPattern}");
            }
        }
    }
}
