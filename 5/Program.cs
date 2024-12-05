using System.Text.RegularExpressions;
Dictionary<int, List<int>> rules = new();
foreach (var (k, v) in new Regex(@"(?<a>\d\d)\|(?<b>\d\d)", RegexOptions.Compiled)
	.Matches(_5.Resource.Input)
	.Select(m => (Convert.ToInt32(m.Groups["a"].Value), Convert.ToInt32(m.Groups["b"].Value)))) {
	if (!rules.ContainsKey(k))
		rules.Add(k, new List<int>());
	rules[k].Add(v);
}
Func<int, IEnumerable<int>> tryGet = i => {
	if (rules.ContainsKey(i))
		return rules[i];
	return [];
};
int sum1 = 0;
int sum2 = 0;
int swap = 0;
List<string> invalid = new();
foreach (var update in _5.Resource.Input.Split("\n\n")[1].Split('\n')) {
	var numbers = update.Split(',').Select(x => Convert.ToInt32(x)).ToList();
	if (!numbers
		.Select((n, i) => numbers.Take(i).Intersect(tryGet(numbers[i])).Any())
		.All(x => !x)) {
		do {
			swap = 0;
			for (int i = 1; i < numbers.Count; i++) {
				var intersect = numbers.Take(i).Intersect(tryGet(numbers[i]));
				if (!intersect.Any())
					continue;
				var j = numbers.IndexOf(intersect.First());
				(numbers[i], numbers[j]) = (numbers[j], numbers[i]);
				swap++;
			}
		} while (swap > 0);
		sum2 += numbers[numbers.Count / 2];
		continue;
	}
	sum1 += numbers[numbers.Count / 2];
}
Console.WriteLine(sum1);
Console.WriteLine(sum2);
