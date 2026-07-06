using System.Dynamic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;

public class GameManager
{
    [DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int vKey);
    public static GameManager Instance{get;private set;}
    public GameManager()
    {
        Instance = this;
        Console.CursorVisible = false;
        sceneDic = new Dictionary<SceneName, Scene>();
        sceneDic[SceneName.Title] = new TitleScene();
        sceneDic[SceneName.MainGame] = new MainGameScene();
        sceneDic[SceneName.Config] = new ScreenConfigScene();
        
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
    public bool IsPressed(ConsoleKey key)
    {
        return (GetAsyncKeyState((int)key) & 0x8000) != 0;
    }
    private void InputProcessing(List<KeyAction> actions)
    {
        foreach(var ac in actions)
        {
            foreach(var k in ac.key)
            {
                if(IsPressed(k))
                {
                    ac.action();
                    break;
                }
            }
        }
    }
    private async void MainLoop()
    {
        
        while(true)
        {
            //키 입력 액션 집합
            List<KeyAction> actions = new List<KeyAction>();
            GameState.Instance.CheckInput(actions);
            curScene.CheckInput(actions);
            InputProcessing(actions);

            //업데이트
            State.Update();
            curScene.Update();

            //화면 렌더링
            DrawScreen();

            //프레임 시간만큼 대기
            await Task.Delay(FrameTime);
        }
    }
    private void DrawScreen()
    {
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
    MainGame,
    Config
}