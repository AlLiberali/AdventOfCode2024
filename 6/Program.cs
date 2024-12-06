using static System.Net.Mime.MediaTypeNames;

int L = _6.Resource.Input.IndexOf('\n');
char[,] map = new char[L, L];
int i = 0;
int j = 0;
(int, int) start = (0, 0);
List<(int, int)> blank = new();
for (i = 0; i < L; i++)
	for (j = 0; j < L; j++) {
		map[i, j] = _6.Resource.Input[i * (L + 1) + j];
		if (map[i, j] == '^')
			start = (i, j);
		if (map[i, j] == '.')
			blank.Add((i, j));
	}

List<(int, int)> steps = [start];
(i, j) = start;
Direction d = Direction.Up;
while (i >= 0 && j >= 0 && i < L && j < L) {
	var next = d switch {
		Direction.Up => (i - 1,  j),
		Direction.Down => (i + 1, j),
		Direction.Left => (i, j - 1),
		Direction.Right => (i, j + 1),
		_ => (i, j) // Shut up C#
	};
	try { // Cardinal sin
		if (map[next.Item1, next.Item2] == '#') {
			d = d + 1 == (Direction) 4 ? Direction.Up : d + 1;
			continue;
		}
	} catch {
		break;
	}
	steps.Add(next);
	(i, j) = next;
}
Console.WriteLine(steps.Distinct().Count());
Console.WriteLine(blank.AsParallel()
	.Select(tu => {
		var clone = (char[,])map.Clone();
		clone[tu.Item1, tu.Item2] = '#';
		return clone;
	})
	.Select(PartTwo)
	.Count(x => x));
bool PartTwo(char[,] MAP) { // YELLING LIKE IT'S COBOL
	int I = start.Item1;
	int J = start.Item2;
	Direction D = Direction.Up;
	(int, int, Direction) START = (I, J, D);
	HashSet<(int, int, Direction)> STEPS = [START];
	while (I >= 0 && J >= 0 && I < L && J < L) {
		var NEXT = D switch {
			Direction.Up => (I - 1, J),
			Direction.Down => (I + 1, J),
			Direction.Left => (I, J - 1),
			Direction.Right => (I, J + 1),
			_ => (I, J)
		};
		try { // Cardinal sin
			if (MAP[NEXT.Item1, NEXT.Item2] == '#') {
				D = D + 1 == (Direction) 4 ? Direction.Up : D + 1;
				continue;
			}
		} catch {
			return false;
		}
		if (!STEPS.Add((NEXT.Item1, NEXT.Item2, D)))
			return true;
		(I, J) = NEXT;
	}
	return false;
}
enum Direction {
	Up, Right, Down, Left
}