namespace codecrafters_grep.src
{
    internal static class Patterns
    {
        private static readonly Dictionary<string, Func<string, bool>> _patterns = new() {
            { 
                "\\d", (inputLine) =>
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
                "\\w", (inputLine) =>
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
            }
        };

        internal static bool MatchPattern(string inputLine, string inputPattern)
        {
            if (inputPattern.Length == 1)
            {
                return inputLine.Contains(inputPattern);
            }
            else if (_patterns.TryGetValue(inputPattern, out Func<string, bool>? pattern))
            {
                return pattern(inputLine);
            }
            else
            {
                throw new ArgumentException($"Unhandled pattern: {inputPattern}");
            }
        }
    }
}
