int L = _15.Resource.Input.IndexOf('\n');
char[,] map = new char[L, L];
(int x, int y) robot = (0, 0);
for (int i = 0; i < L; i++)
	for (int j = 0; j < L; j++) {
		if (map[i, j] == '@')
			robot = (i, j);
		map[i, j] = _15.Resource.Input[i * (L + 1) + j];
	}
