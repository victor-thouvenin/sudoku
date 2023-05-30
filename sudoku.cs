using System;

class sudoku {
    static int[][] filledGrid = new int[9][];
    static int[][] grid = new int[9][];

    static List<int> checkPossibilities(int i, int j, int n) {
        var pos = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8};
        for (int x = 0, y = 0; y <= 2; y += x/2, x = (x+1)%3) {
            if (filledGrid[i+y][j+x] != 0) {
                pos.Remove(y*3 + x);
            }
            for (int ind = 0; ind < i || ind < j; ind++) {
                if (x == 0 && ind < j && filledGrid[i+y][ind] == n) {
                    pos.Remove(y*3);
                    pos.Remove(y*3 +1);
                    pos.Remove(y*3 +2);
                }
                if (y == 0 && ind < i && filledGrid[ind][j+x] == n) {
                    pos.Remove(x);
                    pos.Remove(3 + x);
                    pos.Remove(6 + x);
                }
            }
        }
        return (pos);
    }

    static bool fillGrid(int n, int bl, int bc, Random rng) {
        var pos = checkPossibilities(bl*3, bc*3, n);
        do
        {
            if (pos.Count == 0) {
                return false;
            }
            int r = rng.Next(pos.Count);
            int p = pos[r];
            int y = bl*3 + p/3, x = bc*3 + p%3;
            filledGrid[y][x] = n;
            if (n == 9) {
                if (bl == 2 && bc == 2) {
                    return true;
                }
                return (fillGrid(9, (bl + bc/2)%3, (bc+1)%3, rng));
            }
            if (fillGrid(n + (bl*3 + bc)/8, (bl + bc/2)%3, (bc+1)%3, rng)) {
                return true;
            }
            filledGrid[y][x] = 0;
            pos.RemoveAt(r);
        } while (true);
    }

    static void emptyGrid(Random rng) {
        grid[0] = new int[9];
        filledGrid[0].CopyTo(grid[0], 0);
        grid[1] = new int[9];
        filledGrid[1].CopyTo(grid[1], 0);
        grid[2] = new int[9];
        filledGrid[2].CopyTo(grid[2], 0);
        grid[3] = new int[9];
        filledGrid[3].CopyTo(grid[3], 0);
        grid[4] = new int[9];
        filledGrid[4].CopyTo(grid[4], 0);
        grid[5] = new int[9];
        filledGrid[5].CopyTo(grid[5], 0);
        grid[6] = new int[9];
        filledGrid[6].CopyTo(grid[6], 0);
        grid[7] = new int[9];
        filledGrid[7].CopyTo(grid[7], 0);
        grid[8] = new int[9];
        filledGrid[8].CopyTo(grid[8], 0);
        for (int ind = rng.Next(40, 60); ind > 0; ind--) {
            int i = rng.Next(9);
            int j = rng.Next(9);
            grid[i][j] = 0;
        }
    }
    
    static bool checkGrid() {
        return true;
    }

    static bool checkWin() {
        int bnum, lnum, cnum;
        for (int ind = 0; ind < 9; ind++) {
            var block = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};
            var line = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};
            var col = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};
            for (int ind2 = 0; ind2 < 9; ind2++) {
                bnum = grid[(ind/3)*3 + ind2/3][(ind%3)*3 + ind2%3];
                lnum = grid[ind][ind2];
                cnum = grid[ind2][ind];
                block.Remove(bnum < 10 ? bnum : bnum-10);
                line.Remove(lnum < 10 ? lnum : lnum-10);
                col.Remove(cnum < 10 ? cnum : cnum-10);
            }
            if (block.Count != 0 || line.Count != 0 || col.Count != 0) {
                return false;
            }
        }
        return true;
    }

    static void initGrid() {
        filledGrid[0] = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
        filledGrid[1] = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
        filledGrid[2] = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
        filledGrid[3] = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
        filledGrid[4] = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
        filledGrid[5] = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
        filledGrid[6] = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
        filledGrid[7] = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
        filledGrid[8] = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
        Random rng = new Random(DateTime.Now.Millisecond);
        rng = new Random(rng.Next());
        rng = new Random(rng.Next());
        fillGrid(1, 0, 0, rng);
        do {
            rng = new Random(rng.Next());
            emptyGrid(rng);
            if (checkGrid()) {
                return;
            }
        } while (true);
    }

    static void printGrid(int[][] gridToPrint) {
        string border = " ------- ------- ------- ";
        int num;
        for (int i = 0; i < 9; i++) {
            if (i % 3 == 0)
                Console.WriteLine(border);
            Console.Write("|");
            for (int j = 0; j < 9; j++) {
                num = gridToPrint[i][j];
                Console.Write(" " + (num == 0 ? "_" : (num < 10 ? num : num-10).ToString()));
                if (j % 3 == 2)
                    Console.Write(" |");
            }
            Console.WriteLine("");
        }
        Console.WriteLine(border);
    }

    static int Main(string[] arg) {
        initGrid();
        printGrid(grid);
        string val;
        int i = 0, j = 0, num = 0;
        do
        {
            ConsoleKeyInfo k = Console.ReadKey();
            val = k.Key.ToString();
            if (val.Count() == 2 && val[1] > '0' && val[1] <= '9') {
                int tmp = int.Parse("" + val[1]);
                if (i == 0) {
                    i = tmp;
                    Console.Write(" ");
                    continue;
                } else if (j == 0) {
                    j = tmp;
                    Console.Write(" ");
                    continue;
                } else {
                    num = tmp;
                    Console.Write("\n");
                }
            } else if (val == "Backspace") {
                Console.Write("\b\b \b");
                if (j != 0) {
                    j = 0;
                } else if (i != 0) {
                    i = 0;
                }
                continue;
            } else if (val == "Q") {
                Console.Write("\bsolution\n");
                printGrid(filledGrid);
                break;
            } else {
                Console.Write("\b \b");
                continue;
            }
            if (grid[i-1][j-1] != 0 && grid[i-1][j-1] < 10) {
                Console.WriteLine("nope, that's a predefined number.");
                i = j = num = 0;
                continue;
            }
            grid[i-1][j-1] = num+10;
            printGrid(grid);
            i = j = num = 0;
            if (checkWin()) {
                Console.WriteLine("You Win, perfect.");
                break;
            }
        } while (true);
        return 0;
    }
}
