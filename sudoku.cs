using System;

class sudoku {
    static int[,] grid = new int[9,9];

    static List<int> checkPossibilities(int i, int j, int n) {
        var pos = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8};
        for (int x = 0, y = 0; y <= 2; y += x/2, x = (x+1)%3) {
            if (grid[i+y,j+x] != 0) {
                pos.Remove(y*3 + x);
            }
            for (int ind = 0; ind < i || ind < j; ind++) {
                if (x == 0 && ind < j && grid[i+y,ind] == n) {
                    pos.Remove(y*3);
                    pos.Remove(y*3 +1);
                    pos.Remove(y*3 +2);
                }
                if (y == 0 && ind < i && grid[ind,j+x] == n) {
                    pos.Remove(x);
                    pos.Remove(3 + x);
                    pos.Remove(6 + x);
                }
            }
        }
        return (pos);
    }

    static bool initGrid(int n, int bl, int bc, Random rng) {
        var pos = checkPossibilities(bl*3, bc*3, n);
        do
        {
            if (pos.Count == 0) {
                return false;
            }
            int r = rng.Next(pos.Count);
            int p = pos[r];
            int x = bl*3 + p/3, y = bc*3 + p%3;
            grid[x,y] = n;
            if (n == 9) {
                if (bl == 2 && bc == 2) {
                    return true;
                }
                return (initGrid(9, (bl + bc/2)%3, (bc+1)%3, rng));
            }
            if (initGrid(n + (bl*3 + bc)/8, (bl + bc/2)%3, (bc+1)%3, rng)) {
                return true;
            }
            grid[x,y] = 0;
            pos.RemoveAt(r);
        } while (true);
    }

    static void printGrid() {
        string border = " ------- ------- ------- ";
        for (int i = 0; i < 9; i++) {
            if (i % 3 == 0)
                Console.WriteLine(border);
            Console.Write("|");
            for (int j = 0; j < 9; j++) {
                Console.Write(" " + grid[i,j]);
                if (j % 3 == 2)
                    Console.Write(" |");
            }
            Console.WriteLine("");
        }
        Console.WriteLine(border);
    }

    static int Main(string[] arg) {
        Random rng = new Random(DateTime.Now.Millisecond);
        rng = new Random(rng.Next());
        rng = new Random(rng.Next());
        initGrid(1, 0, 0, rng);
        printGrid();
        // do
        // {
        //     ConsoleKeyInfo k = Console.ReadKey();
        //     val = k.Key.ToString();
        //     continue;
        // } while (val != "Q");
        return 0;
    }
}
