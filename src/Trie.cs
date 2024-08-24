namespace codecrafters_grep.src
{
    internal class Trie
    {
        internal readonly Dictionary<char, TrieNode> Nodes = [];

        public Trie(string inputLine)
        {
            BuildTrie(inputLine);
        }

        private void BuildTrie(string inputLine)
        {
            if(inputLine == string.Empty)
            {
                return;
            }

            for(int i = 0; i < inputLine.Length; i++)
            {
                var character = inputLine[i];

                TrieNode currentNode;

                if (Nodes.ContainsKey(character))
                {
                    currentNode = Nodes[character];
                }
                else
                {
                    currentNode = new TrieNode(character);
                    Nodes.Add(character, currentNode);
                }

                for (int j = i + 1; j < inputLine.Length; j++)
                {
                    var newCharacter = inputLine[j];
                    var newNode = new TrieNode(newCharacter);

                    if (currentNode.TryAddNext(newNode))
                    {
                        currentNode = newNode;
                    }
                    else
                    {
                        currentNode = currentNode.NextNodes[newCharacter];
                    }
                }
            }
        }
    
        internal class TrieNode
        {
            internal char Character;
            internal readonly Dictionary<char, TrieNode> NextNodes = [];

            public TrieNode(char character)
            {
                Character = character;
            }

            internal bool TryAddNext(TrieNode node)
            {
                return NextNodes.TryAdd(node.Character, node);
            }
        }
    }
}
