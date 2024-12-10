int[] files = new int[_9.Resource.Input.Length / 2 + 1];
int[] free = new int[_9.Resource.Input.Length / 2];
List<Chunk> chunks = new(_9.Resource.Input.Length);
for (int i = 0; i < _9.Resource.Input.Length; i++) {
	if ((i & 1) == 1) {
		free[i / 2] = Convert.ToInt32(_9.Resource.Input[i]) - 48;
		chunks.Add(new() { Id = -1, Size = free[i / 2] });
	} else {
		files[i / 2] = Convert.ToInt32(_9.Resource.Input[i]) - 48;
		chunks.Add(new() { Id = i / 2, Size = files[i / 2] });
	}
}
int forwardIndexFiles = 0;
int reverseIndexFiles = files.Length - 1;
int forwardIndexFree = 0;
long checksum1 = 0;
for (int position = 0; files.AsParallel().Any(x => x != 0); position++) {
	if (files[forwardIndexFiles] != 0) {
		checksum1 += position * forwardIndexFiles;
		files[forwardIndexFiles]--;
		continue;
	}
	if (free[forwardIndexFree] != 0) {
		if (files[reverseIndexFiles] == 0)
			reverseIndexFiles--;
		checksum1 += position * reverseIndexFiles;
		files[reverseIndexFiles]--;
		free[forwardIndexFree]--;
		continue;
	}
	forwardIndexFree++;
	forwardIndexFiles++;
	position--;
}
Console.WriteLine(checksum1);
for (int i = chunks.Count - 1; i >= 0; i--) {
	if (chunks[i].Id == -1 || chunks[i].Moved)
		continue;
	int j;
	for (j = 0; j < i; j++)
		if (chunks[j].Id == -1 && chunks[j].Size >= chunks[i].Size)
			break;
	if (j == i)
		continue;
	var freeSpace = chunks[j].Size;
	chunks[j] = chunks[i] with { Moved = true };
	chunks[i] = chunks[i] with { Id = -1 };
	if (freeSpace != chunks[j].Size)
		chunks.Insert(j + 1, new() { Id = -1, Size = freeSpace - chunks[j].Size });
	i++;
}
long checksum2 = 0;
int pos = 0;
foreach (var chunk in chunks) {
	for (int i = 0; i < chunk.Size; i++) {
		if (chunk.Id != -1)
			checksum2 += pos * chunk.Id;
		pos++;
	}
}
Console.WriteLine(checksum2);
struct Chunk {
	public int Id;
	public int Size;
	public bool Moved;
}