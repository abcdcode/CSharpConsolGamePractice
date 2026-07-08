public class Bomb : MapObject
{
    public Bomb(Vector2 pos)
    {
        cool = new CoolTimer();
        cool.SetCool("Bomb",15,0,true,Explosion);
        Position = pos;
        BombVisited[this.Position.Y,this.Position.X] = true;
    }
    public override string[] RenderShape()
    {
        return ["◆"];
    }
    public static void StartBomb(Vector2 pos)
    {
        BombVisited = new bool[GameState.MapSizeY,GameState.MapSizeX];
        alreadyDamaged = new List<Enemy>();
        Bomb b = new Bomb(pos);
        GameState.Instance.AddObj(b);
    }
    public override void Update()
    {
        base.Update();
        cool.Update();
        var blist = GameState.Instance.GetBulletList();
        foreach(var b in blist)
        {
            if(GameUtil.CheckHit(this,b))
            {
                GameState.Instance.DeleteBullet(b);
            }
        }
        var elist = GameState.Instance.GetEnemyList();
        foreach(var e in elist)
        {
            if(!alreadyDamaged.Contains(e) && GameUtil.CheckHit(this,e))
            {
                e.TakeDamage(BombDmg);
            }
        }
    }
    public void Explosion()
    {
        int X = Position.X;
        int Y = Position.Y;
        
        if(X-1 >= 0 && !BombVisited[Y,X-1])
        {
            Bomb b = new Bomb(new Vector2(X-1,Y));
            GameState.Instance.AddObj(b);
        }
        if(X+1 < GameState.MapSizeX && !BombVisited[Y,X+1])
        {
            Bomb b = new Bomb(new Vector2(X+1,Y));
            GameState.Instance.AddObj(b);
        }
        if(Y-1 >= 0 && !BombVisited[Y-1,X])
        {
            Bomb b = new Bomb(new Vector2(X,Y-1));
            GameState.Instance.AddObj(b);
        }
        if(Y+1 < GameState.MapSizeY && !BombVisited[Y+1,X])
        {
            Bomb b = new Bomb(new Vector2(X,Y+1));
            GameState.Instance.AddObj(b);
        }
        GameState.Instance.DeleteObj(this);
    }
    public CoolTimer cool;
    public static List<Enemy> alreadyDamaged;
    public static bool[,] BombVisited;
    public const int BombDmg = 50;
}