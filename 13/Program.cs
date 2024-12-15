using System.Text.RegularExpressions;
using static System.Convert;
var regex = new Regex(@"Button A: X\+(?<xA>\d+), Y\+(?<yA>\d+)\nButton B: X\+(?<xB>\d+), Y\+(?<yB>\d+)\nPrize: X=(?<x>\d+), Y=(?<y>\d+)", RegexOptions.Compiled);
var res = regex.Matches(_13.Resource.Input)
	.AsParallel()
	.Select(m => Machine.Of(m.Groups))
	.Select(m => (NumeroUno(m), NumeroDos(m)));
Console.WriteLine(res.Sum(t => t.Item1));
Console.WriteLine(res.Sum(t => t.Item2));
long NumeroUno(Machine m) {
	long i = 0;
	long j = 0;
	for (i = 0; i < 101; i++)
		for (j = 0; j < 101; j++)
			if (((m.a.x * i + m.b.x * j) == m.goal.x) && ((m.a.y * i + m.b.y * j) == m.goal.y))
				return 3 * i + j;
	return 0;
}
long NumeroDos(Machine m) {
	long d = m.a.x * m.b.y - m.a.y * m.b.x;
	if (d == 0)
		return 0;
	long dA = (m.goal.x + 10000000000000) * m.b.y - (m.goal.y + 10000000000000) * m.b.x;
	long dB = (m.goal.y + 10000000000000) * m.a.x - (m.goal.x + 10000000000000) * m.a.y;
	if (dA % d != 0 || dB % d != 0)
		return 0;
	long a = dA / d;
	long b = dB / d;
	if (a < 0 || b < 0)
		return 0;
	return 3 * a + b;
}
struct Machine((long x, long y) A, (long x, long y) B, (long x, long y) Goal) {
	public readonly (Int64 x, Int64 y) a = A;
	public readonly (Int64 x, Int64 y) b = B;
	public readonly (Int64 x, Int64 y) goal = Goal;
	public static Machine Of(GroupCollection gc) => new(
		(ToInt64(gc["xA"].Value), ToInt64(gc["yA"].Value)),
		(ToInt64(gc["xB"].Value), ToInt64(gc["yB"].Value)),
		(ToInt64(gc["x"].Value), ToInt64(gc["y"].Value))
	);
}