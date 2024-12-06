using System.Text.RegularExpressions;

public static class Day4
{
    public static void Execute()
    {
        var ws = WordSearchLoader.Load("Day4/input.txt");
        var wf = new WordFinder(ws);

        Console.WriteLine($"Number of XMAS: {wf.CountWordInAllDirections("XMAS")}");

        Console.WriteLine($"Number of X-MAS: {wf.CountInXShape("MAS")}");
    }
}

public class WordFinder
{
    private readonly WordSearch _wordSearch;

    public WordFinder(WordSearch wordSearch)
    {
        _wordSearch = wordSearch;
    }

    public int CountWordInAllDirections(string word)
    {
        var counter = LookForWordAndBackwards(_wordSearch.Rows, word);
        counter += LookForWordAndBackwards(_wordSearch.Columns, word);
        counter += LookForWordAndBackwards(_wordSearch.Diagonals, word);
        return counter;
    }

    private static int LookForWordAndBackwards(IEnumerable<string> strings, string word)
    {
        var counter = 0;
        foreach (var s in strings)
        {
            s.Contains(word);
            counter += Regex.Matches(s, word).Count() +
           Regex.Matches(string.Concat(s.Reverse()), word).Count();
        }
        return counter;
    }

    public int CountInXShape(string word)
    {
        var counter = 0;
        foreach (var c in _wordSearch.GetAllCharacters('A'))
        {
            var topLeft = _wordSearch.FindRelative(c, -1, -1);
            var topRight = _wordSearch.FindRelative(c, -1, 1);
            var bottomLeft = _wordSearch.FindRelative(c, 1, -1);
            var bottomRight = _wordSearch.FindRelative(c, 1, 1);

            if (topLeft.IsCorrect(bottomRight) && topRight.IsCorrect(bottomLeft))
            {
              counter++;
            }

        }
        return counter;
    }
}

public class WordSearch
{
    private readonly List<Character> _chars;

    public WordSearch(List<Character> chars)
    {
        _chars = chars;
    }

    public List<string> Rows
    {
        get
        {
            var l = new List<string>();
            for (int x = 1; x <= _chars.Max(c => c.X); x++)
            {
                l.Add(string.Concat(_chars.Where(r => r.X == x).Select(c => c.Char).ToList()));
            }
            return l;
        }
    }

    public List<string> Columns
    {
        get
        {
            var columns = new List<string>();
            for (int y = 1; y <= _chars.Max(c => c.Y); y++)
            {
                columns.Add(string.Concat(_chars.Where(c => c.Y == y).Select(c => c.Char).ToList()));
            }
            return columns;
        }
    }

    public List<string> Diagonals
    {
        get
        {
            var diagonals = new List<string>();
            var maxX = _chars.Max(c => c.X);
            var maxY = _chars.Max(c => c.Y);

            foreach (var i in Enumerable.Range(0, maxX))
            {
                var rangeX = Enumerable.Range(1, maxX - i);
                var rangeY = Enumerable.Range(i + 1, maxY - i);
                var diagonal = rangeX.Zip(rangeY, (x, y) => _chars.First(c => c.X == x && c.Y == y).Char);
                diagonals.Add(string.Concat(diagonal));

                if (i != 0)
                {
                    diagonal = rangeX.Zip(rangeY, (x, y) => _chars.First(c => c.X == y && c.Y == x).Char);
                    diagonals.Add(string.Concat(diagonal));
                }
            }

            foreach (var i in Enumerable.Range(0, maxX))
            {
                var rangeX = Enumerable.Range(1, maxX - i);
                var rangeY = Enumerable.Range(1, maxY - i).Reverse();
                var diagonal = rangeX.Zip(rangeY, (x, y) => _chars.First(c => c.X == x && c.Y == y).Char);
                diagonals.Add(string.Concat(diagonal));

                if (i != 0)
                {
                    rangeX = Enumerable.Range(1 + i, maxX - i);
                    rangeY = Enumerable.Range(1 + i, maxY - i).Reverse();
                    diagonal = rangeX.Zip(rangeY, (x, y) => _chars.First(c => c.X == x && c.Y == y).Char);
                    diagonals.Add(string.Concat(diagonal));
                }
            }

            return diagonals;
        }
    }

    public IEnumerable<Character> GetAllCharacters(char character)
    {
        return _chars.Where(c => c.Char.Equals(character)).ToList();
    }

    public Character FindRelative(Character achor, int x, int y)
    {
        var relativeChar = _chars.FirstOrDefault(ch => ch.X == achor.X + x && ch.Y == achor.Y + y);
        if (relativeChar is null)
        {
            return Character.Null;
        }
        return relativeChar;
    }
}

public record Character(int X, int Y, char Char)
{
    public static Character Null => new NullCharacter();

    public bool IsCorrect(Character other) => (IsInXmas && other.IsInXmas) && Char != other.Char;

    private bool IsInXmas => Char == 'S' || Char == 'M';
}

public record NullCharacter() : Character(-1, -1, Char.MinValue);

public static class WordSearchLoader
{
    public static WordSearch Load(string fileName)
    {
        var chars = new List<Character>();
        var x = 1;
        foreach (var line in File.ReadLines(fileName))
        {
            chars.AddRange(line.ToCharArray().Select((c, index) => new Character(x, index + 1, c)));
            x++;
        }

        return new WordSearch(chars);
    }
}
