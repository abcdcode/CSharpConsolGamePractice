public class GameState
{
    public GameState()
    {
        GameBoard = new int[20,20];
        player = new Player();
    }
    public int[,] GameBoard;
    public Player player;
}