using System.Text.RegularExpressions;
using static System.Convert;
using static System.Math;

var regex = new Regex(@"(?<result>\d+): (?<numbers>.+)", RegexOptions.Compiled);
Dictionary<long, Stack<long>> equ = new(_7.Resource.Input.Split('\n')
	.AsParallel()
	.Select(line => regex.Match(line))
	.Select(m => KeyValuePair.Create(
		ToInt64(m.Groups["result"].Value),
		new Stack<long>(m.Groups["numbers"].Value.Split(' ').Select(x => ToInt64(x)))
	)));
long sum1 = 0;
long sum2 = 0;
foreach (var (res, stack) in equ) {
	Stack<Operation>[] opPermutations = Permutations1(stack.Count - 1);
	if (opPermutations
		.AsParallel()
		.Select(op => Calculation(new Stack<long>(stack), op, res))
		.Any(x => x))
		sum1 += res;
	opPermutations = Permutations2(stack.Count - 1);
	if (opPermutations
		.AsParallel()
		.Select(op => Calculation(new Stack<long>(stack), op, res))
		.Any(x => x))
		sum2 += res;
}
Console.WriteLine(sum1);
Console.WriteLine(sum2);
Stack<Operation>[] Permutations1(int n) {
	var res = new Stack<Operation>[(int) Pow(2, n)];
	Queue<Stack<Operation>> q = new([new Stack<Operation>([Operation.Add]), new Stack<Operation>([Operation.Multiply])]);
	int level = 1;
	while (level < n) {
		var c = q.Count;
		for (int i = 0; i < c; i++) {
			var cur = new Stack<Operation>(q.Dequeue());
			var dup = new Stack<Operation>(cur);
			cur.Push(Operation.Add);
			dup.Push(Operation.Multiply);
			q.Enqueue(cur);
			q.Enqueue(dup);
		}
		level++;
	}
	res = q.ToArray();
	return res;
}
Stack<Operation>[] Permutations2(int n) { // I will repeat myself all I want
	var res = new Stack<Operation>[(int) Pow(3, n)];
	Queue<Stack<Operation>> q = new([
		new Stack<Operation>([Operation.Add]),
		new Stack<Operation>([Operation.Multiply]),
		new Stack<Operation>([Operation.Concatenation])
	]);
	int level = 1;
	while (level < n) {
		var c = q.Count;
		for (int i = 0; i < c; i++) {
			var cur = new Stack<Operation>(q.Dequeue());
			var dup = new Stack<Operation>(cur);
			var dupl = new Stack<Operation>(cur);
			cur.Push(Operation.Add);
			dup.Push(Operation.Multiply);
			dupl.Push(Operation.Concatenation);
			q.Enqueue(cur);
			q.Enqueue(dup);
			q.Enqueue(dupl);
		}
		level++;
	}
	res = q.ToArray();
	return res;
}
bool Calculation(Stack<long> operands, Stack<Operation> operations, long expected) {
	while (operations.Count > 0) {
		var a = operands.Pop();
		var b = operands.Pop();
		operands.Push(operations.Pop() switch {
			Operation.Add => a + b,
			Operation.Multiply => a * b,
			Operation.Concatenation => a * ((long) Pow(10, (int) Log10(b) + 1)) + b
		});
	}
	return operands.Pop() == expected;
}
enum Operation {
	Add, Multiply, Concatenation
}