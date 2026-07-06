public class TitleScene : Scene
{
    public override void Update()
    {
        KeyAction[] actions =
        {
            new('1',StartGame),
            new('0',QuitGame)
        };
        IInputable.DefaultInputCheck(actions);
    }

    public override char[,] Render()
    {
        BufferClear();
        DrawString(0,0,"=============================");
        DrawString(0,1,"       Shooting Game         ");
        DrawString(0,2,"=============================");
        DrawString(0,3,"1.Start Game");
        DrawString(0,4,"0.Exit Game");
        return buffer;
    }
    public override void OnChangeScene(Scene prevScene)
    {
        base.OnChangeScene(prevScene);
        GameManager.Instance.IsPlaying = false;
    }
    public void StartGame()
    {
        GameManager.Instance.ChangeScene(SceneName.MainGame);
    }
    public void QuitGame()
    {
        GameManager.Instance.IsRunning = false;
    }
    public string Input = "no";
}