int L = _12.Resource.Input.IndexOf('\n');
char[,] map = new char[L, L];
(int x, int y)[] idk = [(-1, 0), (1, 0), (0, -1), (0, 1)];
for (int i = 0; i < L; i++)
	for (int j = 0; j < L; j++)
		map[i, j] = _12.Resource.Input[i * (L + 1) + j];
List<HashSet<(int x, int y)>> regions = new();
for (int i = 0; i < L; i++)
	for (int j = 0; j < L; j++) {
		if (InRegion(i, j))
			continue;
		HashSet<(int x, int y)> region = new();
		Neighbours((-1, -1), (i, j), map[i, j], region);
		regions.Add(region);
	}
Console.WriteLine(regions.AsParallel()
	.Select(r => r.Count * Perimeter(r))
	.Sum()
);
Console.WriteLine(regions.AsParallel()
	.Select(r => r.Count * Sides(r))
	.Sum()
);
bool InRegion(int i, int j) {
	return regions.AsParallel().SelectMany(x => x).Contains((i, j));
}
void Neighbours((int x, int y) caller, (int x, int y) self, char plant, HashSet<(int x, int y)> region) {
	region.Add(self);
	var heapHelp = Enumerable.Repeat(self, 4)
		.Zip(idk, (a, b) => ((int x, int y)) (a.x + b.x, a.y + b.y))
		.Where(w => w.x >= 0 && w.y >= 0 && w.x < L && w.y < L)
		.Where(w => map[w.x, w.y] == plant)
		.Where(w => !region.Contains(w));
	foreach (var a in heapHelp) {
		region.Add(a);
		Neighbours(self, a, plant, region);
	}
}
int Perimeter(HashSet<(int x, int y)> region) {
	return region
		.SelectMany(w => idk.Select(z => (w.x + z.x, w.y + z.y)))
		.Where(w => !region.Contains(w))
		.Count();
}
int lineSegments(IEnumerable<int> e) {
		var ranges = new List<(int Min, int Max)>();
		var currentRange = (Min: e.First(), Max: e.First());
		foreach (var number in e.Skip(1)) {
			if (number == currentRange.Max + 1) {
				currentRange.Max = number;
			} else {
				ranges.Add(currentRange);
				currentRange = (Min: number, Max: number);
			}
		}
		ranges.Add(currentRange);
		return ranges.Count;
	}
int Sides(HashSet<(int x, int y)> region) {
	var sum = region
		.Select(w => (w.x, w.y - 1))
		.Where(w => !region.Contains(w))
		.OrderBy(w => w.x)
		.GroupBy(w => w.Item2)
		.Select(g => lineSegments(g.Select(w => w.x)))
		.Sum();
	sum += region
		.Select(w => (w.x, w.y + 1))
		.Where(w => !region.Contains(w))
		.OrderBy(w => w.x)
		.GroupBy(w => w.Item2)
		.Select(g => lineSegments(g.Select(w => w.x)))
		.Sum();
	sum += region
		.Select(w => (w.x - 1, w.y))
		.Where(w => !region.Contains(w))
		.OrderBy(w => w.y)
		.GroupBy(w => w.Item1)
		.Select(g => lineSegments(g.Select(w => w.y)))
		.Sum();
	sum += region
		.Select(w => (w.x + 1, w.y))
		.Where(w => !region.Contains(w))
		.OrderBy(w => w.y)
		.GroupBy(w => w.Item1)
		.Select(g => lineSegments(g.Select(w => w.y)))
		.Sum();
	return sum;
}