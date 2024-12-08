int L = _8.Resource.Input.IndexOf('\n');
char[,] map = new char[L, L];
Dictionary<char, List<(int x, int y)>> aerials = new();
for (int i = 0; i < L; i++)
	for (int j = 0; j < L; j++) {
		map[i, j] = _8.Resource.Input[i * (L + 1) + j];
		if (!Char.IsAsciiLetterOrDigit(map[i, j]))
			continue;
		if (!aerials.ContainsKey(map[i, j]))
			aerials[map[i, j]] = [];
		aerials[map[i, j]].Add((i, j));
	}
foreach (var (frequency, locationList) in aerials)
	for (int i = 0; i < locationList.Count - 1; i++)
		for (int j = i + 1; j < locationList.Count; j++) {
			var pJ = locationList[j];
			var pI = locationList[i];
			var dx = pJ.x - pI.x;
			var dy = pJ.y - pI.y;
			try {
				map[pJ.x + dx, pJ.y + dy] = '#';
			} catch { }
			try {
				map[pI.x - dx, pI.y - dy] = '#';
			} catch { }
		}
int sum1 = 0;
for (int i = 0; i < L; i++) {
	for (int j = 0; j < L; j++) {
		if (map[i, j] == '#')
			sum1++;
		map[i, j] = _8.Resource.Input[i * (L + 1) + j];
	}
}
Console.WriteLine(sum1);
foreach (var (frequency, locationList) in aerials)
	for (int i = 0; i < locationList.Count - 1; i++)
		for (int j = i + 1; j < locationList.Count; j++) {
			var pJ = locationList[j];
			var pI = locationList[i];
			var dx = pJ.x - pI.x;
			var dy = pJ.y - pI.y;
			bool cond = true;
			bool a = true;
			for (int k = 0; cond; k++) {
				a = true;
				try {
					map[pJ.x + k * dx, pJ.y + k * dy] = '#';
				} catch { a = false; }
				try {
					map[pI.x - k * dx, pI.y - k * dy] = '#';
				} catch { cond = a; }
			}
		}
int sum2 = 0;
for (int i = 0; i < L; i++)
	for (int j = 0; j < L; j++)
		if (map[i, j] == '#')
			sum2++;
Console.WriteLine(sum2);