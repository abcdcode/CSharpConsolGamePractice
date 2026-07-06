public class TitleScene : Scene
{
    public override void CheckInput(List<KeyAction> keyInputs)
    {
        keyInputs.AddRange(
        [
            new([ConsoleKey.D1,ConsoleKey.NumPad1],StartGame),
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