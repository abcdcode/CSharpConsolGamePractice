public class WaveManager
{
    public WaveManager Instance{get;private set;}
    public WaveManager()
    {
        Instance = this;
    }
    public void DataInit()
    {
        waveDic = new Dictionary<int, WaveData>();
    }
    public Dictionary<int,WaveData> waveDic;
    public int curStage;
}
public class WaveData
{
    public WaveData(int t, SummonData[] s)
    {
        activeTime = t;
        summonDatas = s;
    }
    public int activeTime;
    public SummonData[] summonDatas;
}
public class SummonData
{
    public SummonData(Vector2 p, Enemy m, EnemyAI ai)
    {
        pos = p;
        model = m;
        enemyAI = ai;
    }
    public Vector2 pos;
    public Enemy model;
    public EnemyAI enemyAI;
}