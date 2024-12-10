using System.Collections.Immutable;

int L = _10.Resource.Input.IndexOf('\n');
Direction[] AllDirections = [Direction.Left, Direction.Right, Direction.Up, Direction.Down];
int[,] map = new int[L, L];
HashSet<List<(int x, int y)>> paths = new();
for (int i = 0; i < L; i++)
	for (int j = 0; j < L; j++) {
		map[i, j] = Convert.ToInt32(_10.Resource.Input[i * (L + 1) + j]) - 48;
		if (map[i, j] == 0)
			paths.Add(new([(i, j)]));
	}

foreach (var path in paths.ToImmutableList()) {
	PathFind(path, AllDirections);
}
paths.RemoveWhere(l => l.Count != 10);
Console.WriteLine(paths.GroupBy(l => l.First()).Select(g => g.GroupBy(l => l.Last()).Count()).Sum());
Console.WriteLine(paths.GroupBy(l => l.First()).Select(g => g.Count()).Sum());
void PathFind(List<(int x, int y)> l, params Direction[] directions) {
	foreach (var direction in directions) {
		var cur = l.Last();
		if (map[cur.x, cur.y] == 9)
			return;
		var next = Next(cur, direction);
		if (!IsValid(next) || map[next.x, next.y] - map[cur.x, cur.y] != 1)
			continue;
		List<(int x, int y)> newl = new(l) {
			next
		};
		paths.Add(newl);
		PathFind(newl, AllDirections.Where(d => d != ~direction).ToArray());
	}
}
bool IsValid((int x, int y) p) => !(p.x < 0 || p.y < 0 || p.x >= L || p.y >= L);
(int x, int y) Next((int x, int y) p, Direction d) => d switch {
	Direction.Left => (p.x, p.y - 1),
	Direction.Right => (p.x, p.y + 1),
	Direction.Up => (p.x - 1, p.y),
	Direction.Down => (p.x + 1, p.y),
	_ => p
};
enum Direction {
	Left = 0b0101, Right = 0b1010, Up = 0b1100, Down = 0b0011
}