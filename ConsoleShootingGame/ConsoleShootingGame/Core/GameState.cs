public class GameState : IInputable
{
    public static GameState Instance{get;private set;}
    public GameState()
    {
        player = new Player();
        bulletPool = new List<Bullet>();
        enemyPool = new List<Enemy>();
        Wave = new WaveManager();
        Instance = this;
    }
    public void Init()
    {
        player = new Player();
        player.Position = new Vector2(0,MapSizeY/2);
        bulletPool = new List<Bullet>();
        enemyPool = new List<Enemy>();
        Wave.DataInit();
    }
    /// <summary>
    /// 탄환 생성
    /// </summary>
    /// <param name="pos">생성 위치</param>
    /// <param name="dir">발사 방향</param>
    /// <param name="bulletSpeed">탄환 속도. 1이면 1초에 1칸.</param>
    /// <returns></returns>
    public Bullet ShootBullet(Vector2 pos, Direction dir, int bulletSpeed, Faction faction)
    {
        Bullet b = new Bullet();
        b.Position = pos;
        b.direction = dir;
        b.bulletSpeed = 1000/bulletSpeed;
        b.faction = faction;
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
    public int Score{get;private set;}
    public Player player;
    private List<Bullet> bulletPool;
    private List<Enemy> enemyPool;
    public WaveManager Wave{get;private set;}
    public const int MapSizeX = 90;
    public const int MapSizeY = 15;
}