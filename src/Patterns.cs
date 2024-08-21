using System.Text;

namespace codecrafters_grep.src
{
    internal static class Patterns
    {
        private const string _containsPattern = "contains";
        private const string _characterGroupsPattern = "[]";

        private static List<string> _patternChunks = [];

        private static readonly Dictionary<string, Func<char, string, bool>> _patterns = new() {
            {
                _containsPattern, (character, pattern) =>
                {
                    return pattern.Contains(character);
                }
            },
            {
                "\\d", (character, pattern) =>
                {
                    return char.IsAsciiDigit(character);
                }
            },
            {
                "\\w", (character, pattern) =>
                {
                    return (char.IsAsciiDigit(character) || char.IsAsciiLetter(character) || character == '_');
                }
            },
            {
                _characterGroupsPattern, (character, pattern) =>
                {
                    string patternChars = string.Empty;
                    bool isNegative = pattern[1] == '^';

                    patternChars = isNegative ? pattern[2..^1] : pattern[1..^1];

                    foreach(char c in patternChars)
                    {
                        if(isNegative)
                        {
                            if(character.Equals(c))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if(character.Equals(c))
                            {
                                return true;
                            }
                        }
                    }

                    return isNegative;
                }
            }
        };

        internal static void BuildPatternChunks(string pattern)
        {
            StringBuilder characterGroup = new();

            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '\\')
                {
                    _patternChunks.Add(pattern.Substring(i, 2));
                    i++;
                }
                else if (pattern[i] == '[')
                {
                    characterGroup.Append(pattern[i]);
                }
                else if (pattern[i] == ']')
                {
                    characterGroup.Append(pattern[i]);
                    _patternChunks.Add(characterGroup.ToString());
                    characterGroup.Clear();
                }
                else
                {
                    if (characterGroup.Length > 0)
                    {
                        characterGroup.Append(pattern[i]);
                    }
                    else
                    {
                        _patternChunks.Add(pattern[i].ToString());
                    }
                }
            }
        }

        internal static bool MatchPattern(string inputLine)
        {
            int patternChunksIndex = 0;
            bool matches = false;

            foreach (char c in inputLine)
            {
                var inputPattern = _patternChunks[patternChunksIndex];

                if (inputPattern.Length == 1)
                {
                    matches = _patterns[_containsPattern](c, inputPattern);
                }
                else if (inputPattern.StartsWith('[') && inputPattern.EndsWith(']'))
                {
                    matches = _patterns[_characterGroupsPattern](c, inputPattern);
                }
                else if (_patterns.TryGetValue(inputPattern, out Func<char, string, bool>? pattern))
                {
                    matches = pattern(c, inputPattern);
                }
                else
                {
                    throw new ArgumentException($"Unhandled pattern: {inputPattern}");
                }

                if(matches)
                {
                    patternChunksIndex++;
                }
                else
                {
                    patternChunksIndex = 0;
                    matches = false;
                }
            }

            return patternChunksIndex == _patternChunks.Count;
        }
    }
}
