using System.Net.Http.Headers;
using System.Text;

public class GameManager
{
    public static GameManager Instance{get;private set;}
    public GameManager()
    {
        Instance = this;
        Console.CursorVisible = false;
    }
    public void Start()
    {
        curScene = new TestScene();
        MainLoop();
    }
    public async void MainLoop()
    {
        while(true)
        {
            //Console.SetWindowSize(curScene.CurrentWidth, curScene.CurrentHeight);
            if(lastScreenX != Console.WindowWidth || lastScreenY != Console.WindowHeight)
            {
                Console.Clear();
            }
            Console.SetCursorPosition(0, 0);
            lastScreenX = Console.WindowWidth;
            lastScreenY = Console.WindowHeight;
            char[,] b = curScene.Render();
            StringBuilder sb = new StringBuilder();
            
            for(int y = 0 ; y < b.GetLength(0); y++)
            {
                for(int x = 0 ; x < b.GetLength(1); x++)
                {
                    sb.Append(b[y,x]);
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
            Console.SetCursorPosition(0, 0);
            await Task.Delay(100);
        }
    }
    public int lastScreenX;
    public int lastScreenY;
    public Scene curScene;
}