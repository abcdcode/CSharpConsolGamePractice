public class WaveManager
{
    public static WaveManager Instance{get;private set;}
    public WaveManager()
    {
        Instance = this;
    }
    public void DataInit()
    {
        waveDic = new Dictionary<int, List<WaveData>>();
        waveDic[1] = WavePreset.Stage1();
        waveDic[2] = WavePreset.Stage2();
        waveDic[3] = WavePreset.Stage3();
        curTime = 0;
        curStage = 3;
    }
    public void Update()
    {
        if(!waveDic.ContainsKey(curStage)) // 웨이브 전체 클리어한 상태
        {
            return;
        }
        curTime += GameManager.FrameTime;
        var list = waveDic[curStage];
        foreach(var w in list.ToList())
        {
            if(w.activeTime <= curTime)
            {
                foreach(var s in w.summonDatas)
                {
                    s.model.Position = s.pos;
                    s.model.Init(s.enemyAI);
                    GameState.Instance.AddEnemy(s.model);
                }
                list.Remove(w);
            }
        }
        if(list.Count == 0 && GameState.Instance.GetEnemyList().Count == 0)
        {
            curStage += 1;
            curTime = 0;
        }
    }
    public int curTime;
    public Dictionary<int,List<WaveData>> waveDic;
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
/// <summary>
/// WaveData 프리셋. 메소드로 불러와서 DataInit시 알아서 새 개체로 초기화
/// </summary>
public static class WavePreset
{
    public static List<WaveData> Stage1()
    {
        List<WaveData> data = new()
        {
            new(1000,[
                new(new(100,0),new Jaco(),new MoveAndAttack(8,1)),
                new(new(100,14),new Jaco(),new MoveAndAttack(8,1))
            ]),
            new(2500,[
                new(new(100,1),new Jaco(),new MoveAndAttack(8,1)),
                new(new(100,13),new Jaco(),new MoveAndAttack(8,1))
                ]),
            new(4000,[
                new(new(100,2),new Jaco(),new MoveAndAttack(8,1)),
                new(new(100,12),new Jaco(),new MoveAndAttack(8,1))
                ]),
            new(5500,[
                new(new(100,3),new Jaco(),new MoveAndAttack(8,1)),
                new(new(100,11),new Jaco(),new MoveAndAttack(8,1))
                ]),
            new(7000,[
                new(new(100,4),new Jaco(),new MoveAndAttack(8,1)),
                new(new(100,10),new Jaco(),new MoveAndAttack(8,1))
                ]),
            new(8500,[
                new(new(100,5),new Jaco(),new MoveAndAttack(8,1)),
                new(new(100,9),new Jaco(),new MoveAndAttack(8,1))
                ]),
            new(10000,[
                new(new(100,6),new Jaco(),new MoveAndAttack(8,1)),
                new(new(100,8),new Jaco(),new MoveAndAttack(8,1))
                ]),
            new(11500,[
                new(new(100,7),new Jaco(),new MoveAndAttack(8,1))
                ]),
        };
        return data;
    }
    public static List<WaveData> Stage2()
    {
        List<WaveData> data = new()
        {
            new(1000,[
                new(new(100,2),new Shielder(),new JustMove(6))
            ]),
            new(1500,[
                new(new(100,3),new Jaco(),new MoveAndAttack(6,1))
            ]),
            new(1700,[
                new(new(100,2),new Jaco(),new MoveAndAttack(6,1)),
                new(new(100,4),new Jaco(),new MoveAndAttack(6,1))
            ]),
            new(1900,[
                new(new(100,1),new Jaco(),new MoveAndAttack(6,1)),
                new(new(100,5),new Jaco(),new MoveAndAttack(6,1))
            ]),

            new(2000,[
                new(new(100,8),new Shielder(),new JustMove(6))
            ]),
            new(2500,[
                new(new(100,9),new Jaco(),new MoveAndAttack(6,1))
            ]),
            new(2700,[
                new(new(100,8),new Jaco(),new MoveAndAttack(6,1)),
                new(new(100,10),new Jaco(),new MoveAndAttack(6,1))
            ]),
            new(2900,[
                new(new(100,5),new Jaco(),new MoveAndAttack(6,1)),
                new(new(100,11),new Jaco(),new MoveAndAttack(6,1))
            ]),
        };
        return data;
    } 
    public static List<WaveData> Stage3()
    {
        List<WaveData> data = new()
        {
            new(1000,[
                new(new(100,7),new Elite(),new EliteAI(new(80,7),9,1.5f))
            ]),
        };
        return data;
    } 
}