public static class Day4
{
  public static void Execute()
  {
    var chars = new List<Character>()
    {
      new(1,1,'1'),
      new(1,2,'2'),
      new(1,3,'3'),
      new(2, 1, '1'),
      new(2, 2, '2'),
      new(2, 3, '3'),
      new(3,1,'1'),
      new(3,2,'2'),
      new(3,3,'3')
    };
    var ws = new WordSearch(chars);
    foreach(var row in ws.Rows())
    {
      Console.WriteLine(row);
    }

    foreach (var col in ws.Columns())
    {
      Console.WriteLine(col);
    }

    foreach (var d in ws.Diagonals())
    {
      Console.WriteLine(d);
    }
  }
}


public record Word(string Value);

public class WordSearch
{
  private readonly List<Character> _chars;

  public WordSearch(List<Character> chars)
  {
    _chars = chars;
  }

  public List<string> Rows()
  {
    var l = new List<string>();
    for(int x = 1; x <= _chars.Max(c => c.X); x++)
    {
      l.Add(string.Concat(_chars.Where(r => r.X == x).Select(c => c.Char).ToList()));
    }
    return l;
  }

  public List<string> Columns()
  {
    var columns = new List<string>();
    for(int y = 1; y <= _chars.Max(c => c.Y); y++)
    {
      columns.Add(string.Concat(_chars.Where(c => c.Y == y).Select(c => c.Char).ToList()));
    }
    return columns;
  }

  public List<string> Diagonals()
  {
    var diagonals = new List<string>();
    var maxX = _chars.Max(c => c.X);
    var maxY = _chars.Max(c => c.Y);

    for (var x = 1; x <= maxX; x++)
    {
      for (var y = 1; y <= maxY; y++)
      {
        var c = _chars.First(c => c.X == x && c.Y == y);
        foo.Add(c);
        var cc = _chars.First(ch => ch.X == x + 1 && ch.Y == y + 1);
      }
    }

    
    
    
    
    
    
    










    for(int x = 1; x <= maxX; x++)
    {
      var diagonal = new List<char>();
      for(int y = 1; y <= maxY; y++)
      {
        if(x + y - 1 <= maxX)
        {
          var c = _chars.FirstOrDefault(c => c.X == x + y - 1 && c.Y == y);
          if(c != null)
          {
            diagonal.Add(c.Char);
          }
        }
      }
      diagonals.Add(string.Concat(diagonal));
    }

    for(int y = 2; y <= maxY; y++)
    {
      var diagonal = new List<char>();
      for(int x = 1; x <= maxX; x++)
      {
        if(x + y - 1 <= maxY)
        {
          var c = _chars.FirstOrDefault(c => c.X == x && c.Y == x + y - 1);
          if(c != null)
          {
            diagonal.Add(c.Char);
          }
        }
      }
      diagonals.Add(string.Concat(diagonal));
    }

    return diagonals;
    
  }
}

public record Character(int X, int Y, char Char);

public class WordSearchLoader
{
}
