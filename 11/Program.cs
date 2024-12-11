using System.Collections.Concurrent;
using static System.Math;
List<long> m = _11.Resource.Input.Split(' ').AsParallel().Select(s => Convert.ToInt64(s)).ToList();
// But I hate recursion...
ConcurrentDictionary<(long c, int r), long> cache = new();
Console.WriteLine(m.AsParallel().Select(x => RecusiveFunctionOfDoom(x, 25)).Sum());
Console.WriteLine(m.AsParallel().Select(x => RecusiveFunctionOfDoom(x, 75)).Sum());
long RecusiveFunctionOfDoom(long current, int stepsRemaining) {
	if (stepsRemaining == 0)
		return 1;
	long res = 0;
	if (cache.TryGetValue((current, stepsRemaining), out res))
		return res;
	if (current == 0)
		return RecusiveFunctionOfDoom(1, stepsRemaining - 1);
	var len = (int) Log10(current) + 1;
	var a = (long) (current / Pow(10, len / 2));
	var b = (long) (current - (a * Pow(10, len / 2)));
	var ra = RecusiveFunctionOfDoom(a, stepsRemaining - 1);
	var rb = RecusiveFunctionOfDoom(b, stepsRemaining - 1);
	if ((len & 1) != 1) {
		cache.TryAdd((a, stepsRemaining - 1), ra);
		cache.TryAdd((b, stepsRemaining - 1), rb);
		cache.TryAdd((current, stepsRemaining), ra + rb);
		return ra + rb;
	}
	res = RecusiveFunctionOfDoom(current * 2024, stepsRemaining - 1);
	cache.TryAdd((current, stepsRemaining), res);
	return res;
}