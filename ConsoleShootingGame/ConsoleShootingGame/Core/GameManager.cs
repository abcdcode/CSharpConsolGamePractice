using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;

public class GameManager
{
    public static GameManager Instance{get;private set;}
    public GameManager()
    {
        Instance = this;
        Console.CursorVisible = false;
        sceneDic = new Dictionary<SceneName, Scene>();
        sceneDic[SceneName.Title] = new TitleScene();
        sceneDic[SceneName.MainGame] = new MainGameScene();
    }
    public void Start()
    {
        ChangeScene(SceneName.Title);
        State = new GameState();
        State.player.Position = new Vector2(0,0);
        MainLoop();
    }
    public void ChangeScene(SceneName sceneName)
    {
        var scene = sceneDic[sceneName];
        scene.OnChangeScene(curScene);
        curScene = scene;
    }
    public async void MainLoop()
    {
        
        while(true)
        {
            State.Update();
            curScene.Update();
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
            await Task.Delay(FrameTime);
        }
    }
    public bool IsPlaying{get;set;}
    public GameState State{get;private set;}
    public const int FrameTime = 10;
    public int lastScreenX;
    public int lastScreenY;
    public Scene curScene;
    public Dictionary<SceneName,Scene> sceneDic;
    public bool IsRunning = true;
}
public enum SceneName
{
    Title,
    MainGame
}