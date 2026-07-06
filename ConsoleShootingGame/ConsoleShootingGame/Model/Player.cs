public class Player : MapObject, IInputable
{
    public Player()
    {
        coolTimer = new CoolTimer();
        coolTimer.SetCool(shootCool,100,100,false,null);
        coolTimer.SetCool(moveCool,50,50,false,null);
    }
    public override string[] RenderShape()
    {
        return ["▷"];
    }
    public override void Update()
    {
        base.Update();
        coolTimer.Update();
        CheckHit();
    }
    public void CheckHit()
    {
        var bList = GameState.Instance.GetBulletList().FindAll(x => x.faction == Faction.Enemy);
        foreach(var b in bList)
        {
            if(GameRule.CheckHit(this,b))
            {
                Hit();
                return;
            }
        }
        var eList = GameState.Instance.GetEnemyList();
        foreach(var e in eList)
        {
            if(GameRule.CheckHit(this,e))
            {
                Hit();
                return;
            }
        }
    }
    public void Hit()
    {
        GameManager.Instance.ChangeScene(SceneName.Title);
    }
    public void CheckInput(List<KeyAction> actions)
    {
         actions.AddRange(
        [
          new (ConsoleKey.LeftArrow,() => MoveAction(ConsoleKey.LeftArrow)),
          new (ConsoleKey.RightArrow,() => MoveAction(ConsoleKey.RightArrow)),
          new (ConsoleKey.UpArrow,() => MoveAction(ConsoleKey.UpArrow)),
          new (ConsoleKey.DownArrow,() => MoveAction(ConsoleKey.DownArrow)),
          new (ConsoleKey.Z,() => Shoot())
        ]);
    }
    public void Shoot()
    {
        if(!coolTimer.IsCoolComp(shootCool)) return;
        GameState.Instance.ShootBullet(this.Position+new Vector2(1,0),Direction.Right,100,Faction.Player);
        coolTimer.RefreshCool(shootCool);
    }
    public void MoveAction(ConsoleKey key)
    {
        if(!coolTimer.IsCoolComp(moveCool)) return;
        if(key == ConsoleKey.LeftArrow)
        {
            Move(new Vector2(-1,0));
        }
        if(key == ConsoleKey.RightArrow)
        {
            Move(new Vector2(1,0));
        }
        if(key == ConsoleKey.UpArrow)
        {
            Move(new Vector2(0,-1));
        }
        if(key == ConsoleKey.DownArrow)
        {
            Move(new Vector2(0,1));
        }
        if(Position.X < 0)
        {
            Teleport(new Vector2(0,Position.Y));
        }
        if(Position.X >= GameState.MapSizeX)
        {
            Teleport(new Vector2(GameState.MapSizeX-1,Position.Y));
        }
        if(Position.Y < 0)
        {
            Teleport(new Vector2(Position.X,0));
        }
        if(Position.Y >= GameState.MapSizeY)
        {
            Teleport(new Vector2(Position.X,GameState.MapSizeY-1));
        }
        coolTimer.RefreshCool(moveCool);
    }
    public CoolTimer coolTimer;
    public const string shootCool = "shootCool";
    public const string moveCool = "moveCool";
}