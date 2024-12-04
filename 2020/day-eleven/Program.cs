using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt");

var seatMap = input.Select(line => line.ToList()).ToList();

var currentSeatMap = seatMap;
List<List<char>> nextSeatMap = new List<List<char>>();
while (true)
{
    nextSeatMap = PeopleArriving(currentSeatMap);
    if (CompareSeatMaps(nextSeatMap, currentSeatMap))
    {
        break;
    }
    currentSeatMap = nextSeatMap;
}

Console.WriteLine($"Total occupied seats: {nextSeatMap.SelectMany(x => x).Count(y => y == '#')}");

List<List<char>> PeopleArriving(List<List<char>> seatMap)
{
    var newSeatMap = new List<List<char>>();
    for (var x = 0; x < seatMap.Count; x++)
    {
        var newRow = new List<char>();
        for (var y = 0; y < seatMap[x].Count; y++)
        {
            var seat = seatMap[x][y];
            var seats = GetSeatsAround(seatMap, x, y);
            var newSeat = seat;
            if (seat == 'L' && seats.All(s => s != '#'))
            {
                // no occupied seats around then becomes occupied
                newSeat = '#';
            }
            else if (seat == '#' && seats.Count(s => s == '#') >= 4)
            {
                // if four or more around then become unoccupied
                newSeat = 'L';
            }
            newRow.Add(newSeat);
        }
        newSeatMap.Add(newRow);
    }
    return newSeatMap;
}

List<char> GetSeatsAround(List<List<char>> seatMap, int x, int y)
{
    return new List<char>
    {
        GetSeatAt(seatMap, x - 1, y - 1),
        GetSeatAt(seatMap, x - 1, y),
        GetSeatAt(seatMap, x - 1, y + 1),
        GetSeatAt(seatMap, x, y - 1),
        GetSeatAt(seatMap, x, y + 1),
        GetSeatAt(seatMap, x + 1, y - 1),
        GetSeatAt(seatMap, x + 1, y),
        GetSeatAt(seatMap, x + 1, y + 1),
    }
    .Where(s => s != '*')
    .ToList();
}

char GetSeatAt(List<List<char>> map, int x, int y){
    if (x < 0 || y < 0 || x >= map.Count() || y >= map[0].Count())
    {
        return '*';
    }
    return map[x][y];
};

bool CompareSeatMaps(List<List<char>> a, List<List<char>> b) => Enumerable.Range(0, a.Count()).All(index => a[index].SequenceEqual(b[index]));