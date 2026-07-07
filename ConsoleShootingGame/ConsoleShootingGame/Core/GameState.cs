/// <summary>
/// 인게임 상태 전체 총괄 클래스
/// </summary>
public class GameState : IInputable
{
    public static GameState Instance{get;private set;}
    public GameState()
    {
        Instance = this;
        Init();
    }
    public void Init()
    {
        Score = 0;
        player = new Player();
        player.Position = new Vector2(0,MapSizeY/2);
        PStat = new PlayerStat();
        bulletPool = new List<Bullet>();
        enemyPool = new List<Enemy>();
        itemPool = new List<Item>();
        Wave = new WaveManager();
        Wave.DataInit();
    }
    /// <summary>
    /// 탄환 생성
    /// </summary>
    /// <param name="pos">생성 위치</param>
    /// <param name="dir">발사 방향. Direction.Left | Direction.Up 이런식으로 대각선 방향도 가능</param>
    /// <param name="bulletSpeed">탄환 속도. 1이면 1초에 1칸.</param>
    /// <param name="updownSpeed">탄환 상하 속도. upDownSpeed 횟수만큼 이동을 스킵함. 이걸로 대각선 사격 시 각도를 조정가능(45도 이상은 불가능)</param>
    /// <returns></returns>
    public Bullet ShootBullet(Vector2 pos, Direction dir, int bulletSpeed, Faction faction, int updownSpeed = 1)
    {
        Bullet b = new Bullet();
        b.Position = pos;
        b.direction = dir;
        b.bulletSpeed = 1000/bulletSpeed;
        b.faction = faction;
        b.UpDownCount = updownSpeed;
        bulletPool.Add(b);
        return b;
    }
    public void DeleteBullet(Bullet b)
    {
        bulletPool.Remove(b);
    }
    public void AddEnemy(Enemy enemy)
    {
        enemyPool.Add(enemy);
    }
    public void DeleteEnemy(Enemy enemy)
    {
        enemyPool.Remove(enemy);
    }
    public void AddItem(Item item)
    {
        itemPool.Add(item);
    }
    public void DeleteItem(Item item)
    {
        itemPool.Remove(item);
    }
    public void CheckInput(List<KeyAction> keyInputs)
    {
        player.CheckInput(keyInputs);
    }
    public void AddScore(int value)
    {
        Score += value;
        if(Score < 0) Score = 0;
    }
    public void Update()
    {
        if(!GameManager.Instance.IsPlaying) return;
        player.Update();
        foreach(var b in GetBulletList())
        {
            b.Update();
        }
        foreach(var e in GetEnemyList())
        {
            e.Update();
        }
        foreach(var i in GetItemList())
        {
            i.Update();
        }
        Wave.Update();
    }
    public List<Bullet> GetBulletList()
    {
        return bulletPool.ToList();
    }
    public List<Enemy> GetEnemyList()
    {
        return enemyPool.ToList();
    }
    public List<Item> GetItemList()
    {
        return itemPool.ToList();
    }
    public int Score{get;private set;}
    public Player player;
    public PlayerStat PStat{get;private set;}
    private List<Bullet> bulletPool;
    private List<Enemy> enemyPool;
    private List<Item> itemPool;
    public WaveManager Wave{get;private set;}
    public const int MapSizeX = 90;
    public const int MapSizeY = 15;
}