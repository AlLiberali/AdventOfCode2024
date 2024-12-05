var sr = new StringReader(_2.Resource.Input);
string? line;
int unsafe1 = 0;
int unsafe2 = 0;
while ((line = sr.ReadLine()) != null) {
	var report = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
	for (int i = 0; i < report.Length - 1; i++) {
		if (isUnsafe(report)) {
			unsafe1++;
			unsafe2++;
			if (dampenerDampens(report))
				unsafe2--;
			break;
		}
	}
}
Console.WriteLine(1000 - unsafe1);
Console.WriteLine(1000 - unsafe2);
bool isUnsafe(int[] report) {
	bool isInc = report.Last() > report[0];
	for (int i = 0; i < report.Length - 1; i++)
		if (Math.Abs(report[i] - report[i + 1]) > 3 || isInc && report[i] >= report[i + 1] || !isInc && report[i] <= report[i + 1])
			return true;
	return false;
}
bool dampenerDampens(int[] report) {
	return Enumerable.Repeat(0, report.Length)
		.AsParallel()
		.Select(_ => report.Clone())
		.Cast<int[]>()
		.Select((arr, i) => arr.Take(i).Concat(arr.Skip(i + 1)).ToArray())
		.Select(isUnsafe)
		.Any(x => !x);
}