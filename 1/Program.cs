var sr = new StringReader(_1.Resource.Input);
int[] a = new int[1000];
int[] b = new int[1000];
int i = 0;
string? line;
while ((line = sr.ReadLine()) != null) {
	a[i] = Convert.ToInt32(line.Split("   ")[0]);
	b[i] = Convert.ToInt32(line.Split("   ")[1]);
	i++;
}
Console.WriteLine(a.Order().Zip(b.Order(), (A, B) => Math.Abs(A - B)).AsParallel().Sum());
i = 0;
foreach (var A in a) {
	i += A * b.AsParallel().Count(B => A == B);
}
Console.WriteLine(i);