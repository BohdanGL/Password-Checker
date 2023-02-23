using System.Text.RegularExpressions;

const string filePath = @"C:\Users\b.hryhorenko\source\repos\PasswordChecker\PasswordChecker\passwords.txt";
Regex regex = new Regex(@"\S\s\d*-\d*:\s\S*");
string[] lines = File.ReadAllLines(filePath);
var rows = new List<Row>();

foreach (var line in lines)
{
    if (!regex.IsMatch(line))
        continue;

    var range = line.Split()[1];
    rows.Add(new Row
    {
        SerachedValue = line.First(),
        From = int.Parse(range.Split("-")[0]),
        To = int.Parse(range.Split("-")[1].Split(":")[0]),
        SearchingLine = line.Split()[2]
    });
}

var count = rows
    .Select(row => new { Count = row.SearchingLine.Count(x => x == row.SerachedValue), Row = row })
    .Where(x => x.Count >= x.Row.From && x.Count <= x.Row.To)
    .Count();

Console.WriteLine("Count of valid passwords in file = " + count);


class Row
{
    public char SerachedValue { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string SearchingLine { get; set; }
}