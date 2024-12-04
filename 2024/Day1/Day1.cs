public static class Day1
{
    public static void Execute()
    {
        var (leftList, rightList) = NotesFinder.FindNotes("Day1/input.txt");

        Console.WriteLine($"Total Distance: {leftList.TotalDistanceFrom(rightList)}");
        Console.WriteLine($"Similarity Score: {leftList.CalculateTheSimilarityScore(rightList)}");
    }
}

public class NotesFinder
{
    public static (LocationList LeftList, LocationList RightList) FindNotes(string fileName)
    {
        var leftList = new LocationList();
        var rightList = new LocationList();

        foreach (var line in File.ReadLines(fileName, System.Text.Encoding.UTF8))
        {
            var split = line.Split("   ");
            leftList.Add(new Location(int.Parse(split[0])));
            rightList.Add(new Location(int.Parse(split[1])));
        }

        return (leftList, rightList);
    }
}

public class LocationComparer : Comparer<Location>
{
    public override int Compare(Location? x, Location? y)
    {
        if (x is null && y is not null) return -1;
        if (x is not null && y is null) return 1;
        if (x is null && y is null) return 0;
        return x!.Id.CompareTo(y!.Id);
    }
}

public record Location(int Id)
{
    public int DistanceFrom(Location location)
    {
        return Math.Abs(Id - location.Id);
    }
};

public class LocationList : List<Location>
{
    public int TotalDistanceFrom(LocationList list)
    {
        var thisList = this
            .Order(new LocationComparer())
            .ToList();
        var otherList = list
            .Order(new LocationComparer())
            .ToList();

        int total = 0;
        foreach (var location in thisList)
        {
            total += location.DistanceFrom(otherList.ElementAt(thisList.IndexOf(location)));
        }

        return total;
    }

    public int CalculateTheSimilarityScore(LocationList list)
    {
        int total = 0;
        foreach (var location in this)
        {
            var times = list.Count(x => x.Id == location.Id);
            total += location.Id * times;
        }

        return total;
    }
}
