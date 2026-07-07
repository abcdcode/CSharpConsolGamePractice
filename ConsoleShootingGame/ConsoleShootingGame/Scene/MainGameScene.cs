using System.Text;

public class MainGameScene : Scene
{
    public override char[,] Render()
    {
        BufferClear();
        var stage = $"Stage : {WaveManager.Instance.curStage}";
        var score = $"Score : {GameState.Instance.Score}";
        var space = GameState.MapSizeX-stage.Length-score.Length;
        DrawString(0,0,$"{stage}{new string(' ',space)}{score}");
        DrawMap(0,1);
        int drawY = GameState.MapSizeY+1;
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