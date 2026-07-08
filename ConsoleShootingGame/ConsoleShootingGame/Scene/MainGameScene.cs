using System.Reflection.Metadata.Ecma335;
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
        DrawString(0,drawY+3,$"Bomb:[{new string('□',GameState.Instance.PStat.BombCount)}]");

        //플레이어 사망 시 게임오버
        if(!GameManager.Instance.IsPlaying)
        {
            DrawSString(0,5,GameOver());
        }
        return buffer;
    }
    //GameState에서 오브젝트들 가져와서 맵에 그리기
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
        var oList = GameState.Instance.GetObjectList();
        foreach(var o in oList)
        {
            DrawObject(o);
        }
        var p = GameState.Instance.player;
        DrawObject(p);
    }
    public override void Update()
    {
    }
    //씬 전환 시 GameState 초기화
    public override void OnChangeScene(Scene prevScene)
    {
        base.OnChangeScene(prevScene);
        GameManager.Instance.IsPlaying = true;
        GameState.Instance.Init();
    }
    public override void CheckInput(List<KeyAction> keyInputs)
    {
    }
    //게임오버 아스키 아트
    public string[] GameOver() 
    {
        return [
            """  _______      ___       ___  ___   _______      ______   ____    ____  _______ .______      """,
            """ /  _____|    /   \     |   \/   | |   ____|    /  __  \  \   \  /   / |   ____||   _  \     """,
            """|  |  __     /  ^  \    |  \  /  | |  |__      |  |  |  |  \   \/   /  |  |__   |  |_)  |    """,
            """|  | |_ |   /  /_\  \   |  |\/|  | |   __|     |  |  |  |   \      /   |   __|  |      /     """,
            """|  |__| |  /  _____  \  |  |  |  | |  |____    |  `--'  |    \    /    |  |____ |  |\  \     """,
            """ \______| /__/     \__\ |__|  |__| |_______|    \______/      \__/     |_______|| _| `__|    """];
    }
}