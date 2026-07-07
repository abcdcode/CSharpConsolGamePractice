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
        waveDic[4] = WavePreset.Stage4();
        curTime = 0;
        curStage = 4;
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
                    s.model.Init(s.enemyAI,s.dropItem);
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
    public WaveData(int t, SummonData[] s) : this(t,s,null)
    {
    }
    public WaveData(int t, SummonData[] s,WaveRepeat r)
    {
        activeTime = t;
        summonDatas = s;
        waveRepeat = r;
    }
    public int activeTime;
    public SummonData[] summonDatas;
    public WaveRepeat waveRepeat;
    public class WaveRepeat
    {
        public WaveRepeat(int r, int c)
        {
            repeatTime =r;
            cycle = c;
        }
        public int repeatTime;
        public int cycle;
    }
}
public class SummonData
{
    public SummonData(Vector2 p, Enemy m, EnemyAI ai):this(p,m,ai,null)
    {
    }
    public SummonData(Vector2 p, Enemy m, EnemyAI ai,Item d)
    {
        pos = p;
        model = m;
        enemyAI = ai;
        dropItem = d;
    }
    public Vector2 pos;
    public Enemy model;
    public EnemyAI enemyAI;
    public Item dropItem;
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
                new(new(100,0),new Jaco(),new MoveAndAttack(10,1)),
                new(new(100,14),new Jaco(),new MoveAndAttack(10,1))
            ]),
            new(2500,[
                new(new(100,1),new Jaco(),new MoveAndAttack(10,1)),
                new(new(100,13),new Jaco(),new MoveAndAttack(10,1))
                ]),
            new(4000,[
                new(new(100,2),new Jaco(),new MoveAndAttack(10,1)),
                new(new(100,12),new Jaco(),new MoveAndAttack(10,1))
                ]),
            new(5500,[
                new(new(100,3),new Jaco(),new MoveAndAttack(10,1)),
                new(new(100,11),new Jaco(),new MoveAndAttack(10,1))
                ]),
            new(7000,[
                new(new(100,4),new Jaco(),new MoveAndAttack(10,1)),
                new(new(100,10),new Jaco(),new MoveAndAttack(10,1))
                ]),
            new(8500,[
                new(new(100,5),new Jaco(),new MoveAndAttack(10,1)),
                new(new(100,9),new Jaco(),new MoveAndAttack(10,1))
                ]),
            new(10000,[
                new(new(100,6),new Jaco(),new MoveAndAttack(10,1)),
                new(new(100,8),new Jaco(),new MoveAndAttack(10,1))
                ]),
            new(11500,[
                new(new(100,7),new Jaco(),new MoveAndAttack(10,1),new PowerUp())
                ]),
        };
        return data;
    }
    public static List<WaveData> Stage2()
    {
        List<WaveData> data = new()
        {
            new(1000,[
                new(new(100,2),new Shielder(),new JustMove(6),new SpeedUp())
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
                new(new(100,8),new Shielder(),new JustMove(6), new ShotSpeedUp())
            ]),
            new(2500,[
                new(new(100,9),new Jaco(),new MoveAndAttack(6,1))
            ]),
            new(2700,[
                new(new(100,8),new Jaco(),new MoveAndAttack(6,1)),
                new(new(100,10),new Jaco(),new MoveAndAttack(6,1))
            ]),
            new(2900,[
                new(new(100,7),new Jaco(),new MoveAndAttack(6,1)),
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
                new(new(100,2),new Elite(),new EliteAI(new(80,2),10,3f))
            ]),
            new(1200,[
                new(new(100,8),new Elite(),new EliteAI(new(80,8),10,3.5f))
            ]),
        };
        return data;
    }
    public static List<WaveData> Stage4()
    {
        List<WaveData> data = new()
        {
            
        };
        for(int i = 0 ; i < 100; i++)
        {
            data.Add(new(1000+i*150,[
                new(new(100,GameUtil.RandomRange(0,15)),new KamiKaze(),new JustMove(60))
            ]));
        }
        return data;
    } 
}