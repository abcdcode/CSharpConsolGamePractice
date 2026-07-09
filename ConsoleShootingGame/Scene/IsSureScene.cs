/// <summary>
/// 적정 화면 크기가 아닌데 게임 시작하려 할때 한번 더 확인 돌리는 씬
/// </summary>
public class IsSureScene : Scene
{
    public override void CheckInput(List<KeyAction> keyInputs)
    {
        if(coolTimer.IsCoolComp("Wait")) // 입력 딜레이. 두번 인식되서 바로 게임 시작되는거 방지
        {
            keyInputs.Add(new([ConsoleKey.D1,ConsoleKey.NumPad1],StartGame));
            keyInputs.Add(new([ConsoleKey.D9,ConsoleKey.NumPad9],ReturnToTitle));
        }
    }

    public override char[,] Render()
    {
        BufferClear();
        /*
        DrawString(0,0,$"Recommand Size X = {ScreenConfigScene.RecommandX} Y = {ScreenConfigScene.RecommandY}");
        DrawString(0,1,$"Current Size X = {Console.WindowWidth} Y = {Console.WindowHeight}");
        DrawString(0,2,"현재 사이즈가 권장 사이즈보다 크도록 화면 크기를 조정해주세요.");
        DrawString(0,3,"조정을 완료했다면 9번을 눌러 타이틀로 돌아가세요.");
        */
        DrawString(0,0,$"Recommand Size X = {ScreenConfigScene.RecommandX} Y = {ScreenConfigScene.RecommandY}");
        DrawString(0,1,$"Current Size X = {Console.WindowWidth} Y = {Console.WindowHeight}");
        DrawString(0,2,"현재 스크린 사이즈가 권장 사이즈보다 작습니다.");
        DrawString(0,3,"1.게임시작하기");
        DrawString(0,4,"9.타이틀로 돌아가기");
        return buffer;
    }

    public override void Update()
    {
        coolTimer.Update();
    }
    public override void OnChangeScene(Scene prevScene)
    {
        base.OnChangeScene(prevScene);
        coolTimer.SetCool("Wait",500,0,true,null);
    }
    public void StartGame()
    {
        GameManager.Instance.ChangeScene(SceneName.MainGame);
    }
    public void ReturnToTitle()
    {
        GameManager.Instance.ChangeScene(SceneName.Title);
    }
    public CoolTimer coolTimer = new CoolTimer();
}