decimal ExecuteDay1Part1(string text)
{
    return text.Split("\n\n").Select(x => x.Split("\n").Sum(decimal.Parse)).Max();
}

decimal ExecuteDay1Part2(string text)
{
    var caloriesPerReindeer = text.Split("\n\n");
    return caloriesPerReindeer
        .Select(x => x.Split("\n").Sum(decimal.Parse))
        .OrderByDescending(x => x).Take(3).Sum();
}

long ExecuteDay2Part1(string[] lines)
{
    var encryptedDifference = 'X' - 'A';
    var getTypeScore = (char chosen) => chosen - 'A' + 1;

    const int winScore = 6;
    const int drawScore = 3;

    long total = 0;
    foreach (var split in lines.Select(x => x.Split(" ")))
    {
        var opponent = char.Parse(split[0]);
        var own = (char)(char.Parse(split[1]) - encryptedDifference);
        total += getTypeScore(own);

        if (opponent == own) total += drawScore;
        if (opponent == (own == 'A' ? own + 2 : own - 1)) total += winScore;
    }

    return total;
}

long ExecuteDay2Part2(string[] lines)
{
    const int winScore = 6;
    const int drawScore = 3;

    var getTypeScore = (char chosen) => chosen - 'A' + 1;
    var getWinningType = (char chosen) => (char)(chosen == 'C' ? 'A' : chosen + 1);
    var getLosingType = (char chosen) => (char)(chosen == 'A' ? 'C' : chosen - 1);
    long total = 0;
    foreach (var split in lines.Select(x => x.Split(" ")))
    {
        var opponent = char.Parse(split[0]);
        var own = char.Parse(split[1]);

        total += own switch
        {
            'X' => getTypeScore(getLosingType(opponent)),
            'Y' => getTypeScore(opponent) + drawScore,
            _ => getTypeScore(getWinningType(opponent)) + winScore
        };
    }

    return total;
}

decimal ExecuteDay3Part1(string[] lines)
{
    var getPriority = (char c) => char.IsUpper(c) ? c - 38 : c - 96;
    var getHalf = (int length) => length / 2;
    return lines.Sum(line => getPriority(line[..getHalf(line.Length)].FirstOrDefault(c => line[getHalf(line.Length)..].Contains(c))));
}

decimal ExecuteDay3Part2(string[] lines)
{
    var getPriority = (char c) => char.IsUpper(c) ? c - 38 : c - 96;
    var findCommon = (string[] lines) => lines[0].FirstOrDefault(c => lines.All(line => line.Contains(c)));

    return lines
        .Select((value, index) => new { value, index })
        .GroupBy(x => x.index / 3)
        .Sum(group => getPriority(findCommon(group.Select(x => x.value).ToArray())));
}

decimal ExecuteDay4Part1(string[] lines)
{
    var isFullyWithinRange = (string line) =>
    {
        var split = line.Split(",");
        var firstRange = split[0].Split("-");
        var secondRange = split[1].Split("-");

        var firstMin = int.Parse(firstRange[0]);
        var firstMax = int.Parse(firstRange[1]);
        var secondMin = int.Parse(secondRange[0]);
        var secondMax = int.Parse(secondRange[1]);

        return (firstMin <= secondMin && firstMax >= secondMax) || (secondMin <= firstMin && secondMax >= firstMax);
    };

    return lines.Where(isFullyWithinRange).Count();
}

decimal ExecuteDay4Part2(string[] lines)
{
    var sharesRange = (string line) =>
    {
        var split = line.Split(",");
        var firstRange = split[0].Split("-");
        var secondRange = split[1].Split("-");

        var firstMin = int.Parse(firstRange[0]);
        var firstMax = int.Parse(firstRange[1]);
        var secondMin = int.Parse(secondRange[0]);
        var secondMax = int.Parse(secondRange[1]);

        return (firstMin >= secondMin && firstMin < secondMax) || (firstMax >= secondMin && firstMax <= secondMax)
            || (secondMin >= firstMin && secondMin < firstMax) || secondMax >= firstMin && secondMax <= firstMax;
    };

    return lines.Where(sharesRange).Count();
}




// Sample execution
var lines = await File.ReadAllLinesAsync("C:/dev/input.txt");
Console.WriteLine(ExecuteDay4Part2(lines));