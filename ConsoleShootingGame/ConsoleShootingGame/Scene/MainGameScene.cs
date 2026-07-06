using System.Text;

public class MainGameScene : Scene
{
    public override char[,] Render()
    {
        BufferClear();
        //DrawString(0,0,$"현재 시각 : {DateTime.Now}");
        //DrawString(0,1,$"현재 화면 크기 : {Console.WindowWidth} , {Console.WindowHeight}");
        //DrawString(0,2,Input);
        DrawMap(GameState.MapSizeX,GameState.MapSizeY);
        DrawObjects();
        return buffer;
    }
    public void DrawObjects()
    {
        var bList = GameState.Instance.GetBulletList();
        foreach(var b in bList)
        {
            DrawObject(b);
        }
        var p = GameState.Instance.player;
        DrawObject(p);
    }
    public override void Update()
    {
        GameManager.Instance.State.player.CheckInput();
    }
    public override void OnChangeScene(Scene prevScene)
    {
        base.OnChangeScene(prevScene);
        GameManager.Instance.IsPlaying = true;
        GameState.Instance.Init();
    }
}