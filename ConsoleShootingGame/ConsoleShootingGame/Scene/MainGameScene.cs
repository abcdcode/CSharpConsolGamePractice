using System.Text;

public class MainGameScene : Scene
{
    public override char[,] Render()
    {
        BufferClear();
        var stage = $"Stage : {WaveManager.Instance.curStage}";
        var score = $"Score : {GameState.Instance.Score}";
        var space = GameState.MapSizeX-stage.Length-score.Length;
        DrawString(0,0,$"{stage}{new string(' ',space)}{score}"); //현재 스테이지와 스코어 값을 최상단에 배치
        DrawMap(0,1); // 맵 그리기
        DrawObjects(); // 맵 위 오브젝트들 그리기
        int drawY = GameState.MapSizeY+1;

        //플레이어 스탯들 그리기
        DrawString(0,drawY,$"Atk:[{new string('□',GameState.Instance.PStat.Atk)}]");
        DrawString(0,drawY+1,$"Atk Speed:[{new string('□',GameState.Instance.PStat.ShotSpeed/2)}]");
        DrawString(0,drawY+2,$"Move Speed:[{new string('□',GameState.Instance.PStat.Speed/5)}]");
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
        var iList = GameState.Instance.GetItemList();
        foreach(var i in iList)
        {
            DrawObject(i);
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