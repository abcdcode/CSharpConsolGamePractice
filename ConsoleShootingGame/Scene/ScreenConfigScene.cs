/// <summary>
/// 화면 체크 씬. 스크린 크기가 일정 크기 이상이 안되면 정상적으로 맵이 렌더링되지 않으므로 여기서 맞추도록 함
/// </summary>
public class ScreenConfigScene : Scene
{
    public override char[,] Render()
    {
        BufferClear();
        DrawString(0,0,$"Recommand Size X = {RecommandX} Y = {RecommandY}");
        DrawString(0,1,$"Current Size X = {Console.WindowWidth} Y = {Console.WindowHeight}");
        DrawString(0,2,"현재 사이즈가 권장 사이즈보다 크도록 화면 크기를 조정해주세요.");
        DrawString(0,3,"조정을 완료했다면 9번을 눌러 타이틀로 돌아가세요.");
        return buffer;
    }

    public override void Update()
    {
        
    }
    public override void CheckInput(List<KeyAction> keyInputs)
    {
        keyInputs.Add(new([ConsoleKey.D9,ConsoleKey.NumPad9],ReturnToTitle));
    }
    public void ReturnToTitle()
    {
        GameManager.Instance.ChangeScene(SceneName.Title);
    }
    public static int RecommandX => GameState.MapSizeX+10;
    public static int RecommandY => GameState.MapSizeY+15;
}