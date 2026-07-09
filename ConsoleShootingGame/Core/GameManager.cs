using System.Dynamic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// 게임 전체 총괄 GOD 클래스
/// </summary>
public class GameManager
{
    //키 인풋을 위한 라이브러리.
    [DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int vKey);
    public static GameManager Instance{get;private set;}
    public GameManager()
    {
        Instance = this;
        Console.CursorVisible = false;
        State = new GameState();
        sceneDic = new Dictionary<SceneName, Scene>();
        sceneDic[SceneName.Title] = new TitleScene();
        sceneDic[SceneName.MainGame] = new MainGameScene();
        sceneDic[SceneName.Config] = new ScreenConfigScene();
        sceneDic[SceneName.ScreenSmall] = new IsSureScene();
        sceneDic[SceneName.Record] = new RecordScene();
        sceneDic[SceneName.JustBoard] = new JustRecordBoardScene();
        
    }
    public void Start()
    {
        ChangeScene(SceneName.Title);
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
        //화면 크기 이전 프레임과 다르면 지우개 찐빠나니까 한번 싹 밀고 다시 그리기
        if(lastScreenX != Console.WindowWidth || lastScreenY != Console.WindowHeight)
            {
                Console.Clear();
            }
            Console.SetCursorPosition(0, 0); //Write용 커서를 화면 좌상단으로 이동
            lastScreenX = Console.WindowWidth;
            lastScreenY = Console.WindowHeight;
            
            char[,] b = curScene.Render(); // 현재 씬에서 렌더링 데이터 받아오기
            StringBuilder sb = new StringBuilder();
            
            for(int y = 0 ; y < b.GetLength(0); y++)
            {
                int pointer = 0;
                for(int x = 0 ; x < b.GetLength(1) - pointer; x++)
                {
                    sb.Append(b[y,x]);
                    
                    if(GetWidth(b[y,x]) == 2)
                {
                    pointer += 1;
                }
                
                }
                sb.AppendLine();
            }
            Console.Write(sb.ToString()); //렌더링 데이터를 한줄한줄 출력
            Console.SetCursorPosition(0, 0);
    }
    //한글이면 버퍼 2칸 차지해야함. 챗지피티 작품
    public static int GetWidth(char c)
{
    // 한글 음절
    if (c >= 0xAC00 && c <= 0xD7A3)
        return 2;

    // 한글 자모
    if (c >= 0x1100 && c <= 0x11FF)
        return 2;

    return 1;
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
    Config,
    ScreenSmall,
    Record,
    JustBoard
}