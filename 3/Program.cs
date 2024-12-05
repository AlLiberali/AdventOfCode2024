using System.Text.RegularExpressions;
var regex = new Regex(@"mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)", RegexOptions.Compiled);
Console.WriteLine(regex.Matches(_3.Resource.Input)
	.AsParallel()
	.Select(m => (m.Groups["a"].Value, m.Groups["b"].Value))
	.Select(t => Convert.ToDecimal(t.Item1) * Convert.ToDecimal(t.Item2))
	.Sum());
regex = new Regex(@"don't\(\)|do\(\)|mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)", RegexOptions.Compiled);
bool ignore = false;
long l = 0;
foreach (var m in regex.Matches(_3.Resource.Input).AsEnumerable()) {
	if (m.Value.Equals(@"do()")) {
		ignore = false;
		continue;
	}
	if (m.Value.Equals(@"don't()")) {
		ignore = true;
		continue;
	}
	if (ignore)
		continue;
	l += Convert.ToInt64(m.Groups["a"].Value) * Convert.ToInt64(m.Groups["b"].Value);
}
Console.WriteLine(l);
