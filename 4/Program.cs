using System.Text.RegularExpressions;
var regex = new Regex(@"(?=(XMAS|SAMX))", RegexOptions.Compiled);
int sum1 = 0;
int sum2 = 0;
sum1 += _4.Resource.Input
	.Split('\n')
	.Select(line => regex.Matches(line).Count)
	.Sum();
int L = _4.Resource.Input.IndexOf('\n');
char[,] memory = new char[L, L];
int i = 0;
int j = 0;
foreach (var c in _4.Resource.Input.ToCharArray().Where(c => c != '\n')) {
	memory[i, j] = c;
	j++;
	if (j == L) {
		j = 0;
		i++;
	}
}
for (i = 1; i < L - 1; i++)
	for (j = 1; j < L - 1; j++) {
		if (memory[i, j] != 'A')
			continue;
		if (memory[i - 1, j - 1] * memory[i + 1, j + 1] == 6391 && memory[i - 1, j + 1] * memory[i + 1, j - 1] == 6391)
			sum2++;
	}
string[] strings = new string[L];
char[] chars = new char[L];
for (i = 0; i < L; i++) {
	for (j = 0; j < L; j++) {
		chars[j] = memory[j, i];
	}
	strings[i] = new string(chars);
}
sum1 += strings
	.Select(line => regex.Matches(line).Count)
	.Sum();
Array.Fill(chars, '\0');
strings = new string[2 * L];
int k = 0;
for (int s = 3; s < 2 * L - 4; s++) {
	for (i = 0; i < L; i++)
		for (j = 0; j < L; j++)
			if (i + j == s)
				chars[k++] = memory[i, j];
	strings[s - 3] = new string(chars).TrimEnd('\0');
	k = 0;
	Array.Fill(chars, '\0');
}
sum1 += strings
	.Where(line => line != null)
	.Select(line => regex.Matches(line).Count)
	.Sum();
char ch;
for (i = 0; i < L; i++)
	for (j = 0; j < L / 2; j++) {
		ch = memory[i, j];
		memory[i, j] = memory[i, L - 1 - j];
		memory[i, L - 1 - j] = ch;
	}
Array.Fill(chars, '\0');
strings = new string[273];
for (int s = 3; s < 2 * L - 4; s++) {
	for (i = 0; i < L; i++)
		for (j = 0; j < L; j++)
			if (i + j == s)
				chars[k++] = memory[i, j];
	strings[s - 3] = new string(chars).TrimEnd('\0');
	k = 0;
	Array.Fill(chars, '\0');
}
sum1 += strings
	.Where(line => line != null)
	.Select(line => regex.Matches(line).Count)
	.Sum();
Console.WriteLine(sum1);
Console.Write(sum2);
