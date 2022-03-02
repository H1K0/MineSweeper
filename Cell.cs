namespace MineSweeper
{
    internal class Cell
    {
        public string value = " ";
        public bool isMine = false;
        public bool isMarked = false;

        public Cell(bool mine)
        {
            if (mine)
            {
                isMine = true;
                value = "¤";
            }
        }
        public string show()
        {
            if (isMarked)
                return "X";
            else
                return value;
        }
        public void setMine() { isMine = true; }
        public void mark() { isMarked = true; }
        public void unmark() { isMarked = false; }
        public bool isEmpty() { return value == " "; }
    }
}
