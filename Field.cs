namespace MineSweeper
{
    internal class Field
    {
        private int width = 0;
        private int height = 0;
        private int size = 0;
        private List<Cell> cells = new List<Cell>();
        private List<Cell> opened = new List<Cell>();
        public Field(int input_width, int input_height, int nmines)
        {
            if (input_width < 0 || input_height < 0)
                throw new ArgumentException("Field dimensions must be natural numbers.");
            if (input_width < 2 || input_height < 2)
                throw new ArgumentException("Too small field.");
            if (input_width > 10 || input_height > 26)
                throw new ArgumentException("Too large field.");
            if (nmines < 0)
                throw new ArgumentException("The number of mines can not be negative.");
            if (nmines > input_width * input_height)
                throw new ArgumentException("The number of mines can not be greater than the number of all cells.");
            width = input_width;
            height = input_height;
            size = width * height;
            cells = new List<Cell>();
            for (int i = 0; i < size; i++)  // initializing field
            {
                cells.Add(new Cell(nmines-- > 0));
            }
            Random rnd = new Random();
            for (int i = 0; i < size; i++)  // shuffling field
            {
                int dest = rnd.Next(size);
                Cell tmp = cells[i];
                cells[i] = cells[dest];
                cells[dest] = tmp;
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (cells[y * width + x].isMine)
                    {
                        for (int y1 = y - 1; y1 <= y + 1; y1++)
                        {
                            if (y1 < 0 || y1 >= height)
                                continue;
                            for (int x1 = x - 1; x1 <= x + 1; x1++)
                            {
                                if (x1 < 0 || x1 >= width)
                                    continue;
                                Cell current = cells[y1 * width + x1];
                                if (current.isMine)
                                    continue;
                                if (current.value == " ")
                                    current.value = "1";
                                else
                                    current.value = Convert.ToString(Convert.ToInt16(current.value) + 1);
                            }
                        }
                    }
                }
            }
        }
        public void draw()
        {
            Console.Write("   ");
            for (int i = 0; i < width;)
            {
                Console.Write(" " + ++i);
            }
            Console.Write("\n  ╔");
            for (int i = 0; i < 2 * width + 1; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("╗");
            for (int y = 0; y < height; y++)
            {
                Console.Write((char)('A' + y) + " ║");
                for (int x = 0; x < width; x++)
                {
                    Console.Write(" " + ((opened.Contains(cells[y * width + x]) || cells[y * width + x].isMarked) ? cells[y * width + x].show() : "■"));
                }
                Console.WriteLine(" ║");
            }
            Console.Write("  ╚");
            for (int i = 0; i < 2 * width + 1; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("╝");
        }
        public bool open(int y, int x)
        {
            if (y < 0 || y >= height || x < 0 || x >= width)
                throw new Exception("Coordinates out of the field!");
            if (cells[y * width + x].isMine)
                return true;
            if (cells[y * width + x].isMarked)
                return false;
            if (!opened.Contains(cells[y * width + x]))
            {
                opened.Add(cells[y * width + x]);
                if (cells[y * width + x].isEmpty())
                {
                    if (x < width - 1)
                        this.open(y, x + 1);
                    if (x > 0)
                        this.open(y, x - 1);
                    if (y < height - 1)
                        this.open(y + 1, x);
                    if (y > 0)
                        this.open(y - 1, x);
                    if (x < width - 1 && y < height - 1)
                        this.open(y + 1, x + 1);
                    if (x > 0 && y < height - 1)
                        this.open(y + 1, x - 1);
                    if (x < width - 1 && y > 0)
                        this.open(y - 1, x + 1);
                    if (x > 0 && y > 0)
                        this.open(y - 1, x - 1);
                }
            }
            else if (!cells[y * width + x].isEmpty())
            {
                int marked = 0;
                for (int y1 = y - 1; y1 < y + 2; y1++)
                {
                    if (y1 < 0 || y1 >= height)
                        continue;
                    for (int x1 = x - 1; x1 < x + 2; x1++)
                    {
                        if (x1 < 0 || x1 >= width || y1 == y && x1 == x)
                            continue;
                        if (cells[y1 * width + x1].isMarked)
                            marked++;
                    }
                }
                if (marked == Convert.ToInt16(cells[y * width + x].value))
                    for (int y1 = y - 1; y1 < y + 2; y1++)
                    {
                        if (y1 < 0 || y1 >= height)
                            continue;
                        for (int x1 = x - 1; x1 < x + 2; x1++)
                        {
                            if (x1 < 0 || x1 >= width || y1 == y && x1 == x)
                                continue;
                            if (!cells[y1 * width + x1].isMarked && !opened.Contains(cells[y1 * width + x1]))
                                if (this.open(y1, x1))
                                    return true;
                        }
                    }
            }
            return false;
        }
        public void openall()
        {
            for (int i = 0; i < size; i++)
            {
                if (!opened.Contains(cells[i]))
                    opened.Add(cells[i]);
            }
        }
        public void mark(int y, int x) { cells[y * width + x].mark(); }
        public void unmark(int y, int x) { cells[y * width + x].unmark(); }
        public bool check()
        {
            for (int i = 0; i < size; i++)
                if (!cells[i].isMine && !opened.Contains(cells[i]))
                    return false;
            return true;
        }
    }
}
