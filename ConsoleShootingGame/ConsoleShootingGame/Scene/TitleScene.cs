/// <summary>
/// 타이틀 화면 씬
/// </summary>
public class TitleScene : Scene
{
    public TitleScene()
    {
        //coolTimer.SetCool("InputDelay",500,0,true,null);
    }
    public override void CheckInput(List<KeyAction> keyInputs)
    {
        if(!coolTimer.IsCoolComp("InputDelay")) return;
        keyInputs.AddRange(
        [
            new([ConsoleKey.D1,ConsoleKey.NumPad1],StartGame),
            new([ConsoleKey.D2,ConsoleKey.NumPad1],ConfigStart),
            new([ConsoleKey.D0,ConsoleKey.NumPad0],QuitGame)
        ]);
    }
    public override void Update()
    {
        
    }

    public override char[,] Render()
    {
        BufferClear();
        DrawString(0,0,"=============================");
        DrawString(0,1,"       Shooting Game         ");
        DrawString(0,2,"=============================");
        DrawString(0,3,"1.Start Game");
        DrawString(0,4,"2.Screen Config");
        DrawString(0,5,"0.Exit Game");
        return buffer;
    }
    public override void OnChangeScene(Scene prevScene)
    {
        base.OnChangeScene(prevScene);
        GameManager.Instance.IsPlaying = false;
    }
    //게임 시작
    public void StartGame()
    {
        if(Console.WindowWidth < ScreenConfigScene.RecommandX || Console.WindowHeight < ScreenConfigScene.RecommandY)
        {
            GameManager.Instance.ChangeScene(SceneName.ScreenSmall); // 화면 크기 안맞으면 확인 씬 띄우기
        }else
        {
            GameManager.Instance.ChangeScene(SceneName.MainGame);
        }
    }
    public void ConfigStart()
    {
        GameManager.Instance.ChangeScene(SceneName.Config);
    }
    public void QuitGame()
    {
        GameManager.Instance.IsRunning = false;
    }
    public CoolTimer coolTimer = new CoolTimer();
    public string Input = "no";
}