using codecrafters_grep.src;

if (args[0] != "-E")
{
    Console.WriteLine("Expected first argument to be '-E'");
    Environment.Exit(2);
}

string pattern = args[1];
string inputLine = Console.In.ReadToEnd();

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

Patterns.BuildPatternChunks(pattern);

if (Patterns.MatchPattern(inputLine))
{
    Environment.Exit(0);
}
else
{
    Environment.Exit(1);
}
