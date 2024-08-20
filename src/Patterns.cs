namespace codecrafters_grep.src
{
    internal static class Patterns
    {
        private static readonly Dictionary<string, Func<string, bool>> _patterns = new() {
            { "\\d", (inputLine) =>
                {
                    foreach(char c in inputLine)
                    {
                        if(IsASCIIDigit(c))
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

        private static bool IsASCIIDigit(char c)
        {
            return c > 47 && c < 58;
        }
    }
}
