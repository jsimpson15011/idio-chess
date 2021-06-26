namespace idiot_chess
{
    public class ChessBoard
    {
        public ChessBoard()
        {
            char[][] initBoard = new char[8][];
            for (int i = 0; i < initBoard.Length; i++)
            {
                initBoard[i] = new char[8];
                for (int j = 0; j < initBoard[i].Length; j++)
                {
                    initBoard[i][j] = 'X';
                }
            }

            initBoard[0][0] = 'r';
            initBoard[0][1] = 'k';
            initBoard[0][2] = 'b';
            initBoard[0][3] = 'q';
            initBoard[0][4] = 'K';

            Board = initBoard;

        }
        
        public ChessBoard(char[][] board)
        {
            Board = board;
        }
        
        public char[][] Board { get; set; }
    }
}