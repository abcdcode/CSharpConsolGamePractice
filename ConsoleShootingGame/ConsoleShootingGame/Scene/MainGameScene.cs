using System.Text;

public class MainGameScene : Scene
{
    public override char[,] Render()
    {
        BufferClear();
        DrawMap(0,1);
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
        var eList = GameState.Instance.GetEnemyList();
        foreach(var e in eList)
        {
            DrawObject(e);
        }
        var p = GameState.Instance.player;
        DrawObject(p);
    }
    public override void Update()
    {
    }
    public override void OnChangeScene(Scene prevScene)
    {
        base.OnChangeScene(prevScene);
        GameManager.Instance.IsPlaying = true;
        GameState.Instance.Init();
    }
    public override void CheckInput(List<KeyAction> keyInputs)
    {
    }
}