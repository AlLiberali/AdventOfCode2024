using System.Text.RegularExpressions;
using static System.Convert;

var regex = new Regex(@"p=(?<rx>\d+),(?<ry>\d+) v=(?<vx>-?\d+),(?<vy>-?\d+)", RegexOptions.Compiled);
Console.WriteLine(regex.Matches(_14.Resource.Input)
	.AsParallel()
	.Select(m => m.Groups)
	.Select(Robot.Of)
	.Select(r => {
		for (int i = 0; i < 100; i++)
			r.Tick();
		return r;
	})
	.GroupBy(r => (r.X, r.Y) switch {
		(< 50, < 51) => Quadrant.TopLeft,
		(> 50, < 51) => Quadrant.TopRight,
		(< 50, > 51) => Quadrant.BottomLeft,
		(> 50, > 51) => Quadrant.BottonRight,
		_ => Quadrant.Middle
	})
	.Where(g => g.Key != Quadrant.Middle)
	.Select(g => g.Count())
	.Aggregate((a, b) => a * b));
// I won't be solving part 2 because I have no idea what a Christmas tree would look like
// Especially so in a 101x103 grid

class Robot((int x, int y) d, (int x, int y) v) {
	public int X = d.x;
	public int Y = d.y;
	public readonly (int x, int y) V = v;
	public void Tick() {
		X = ((X + V.x) % 101 + 101) % 101;
		Y = ((Y + V.y) % 103 + 103) % 103;
	}
	public static Robot Of(GroupCollection gc) => new(
		(ToInt32(gc["rx"].Value), ToInt32(gc["ry"].Value)),
		(ToInt32(gc["vx"].Value), ToInt32(gc["vy"].Value))
	);
}
enum Quadrant {
	Middle, TopLeft, TopRight, BottomLeft, BottonRight
}