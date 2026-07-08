public class Player : MapObject, IInputable
{
    public Player()
    {
        coolTimer = new CoolTimer();
        coolTimer.SetCool(shootCool,1000/GameState.Instance.PStat.ShotSpeed,1000,false,null);
        coolTimer.SetCool(moveCool,1000/GameState.Instance.PStat.Speed,1000,false,null);
        coolTimer.SetCool(bombCool,2000,0,false,null);
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
            if(GameUtil.CheckHit(this,b))
            {
                Hit();
                return;
            }
        }
        var eList = GameState.Instance.GetEnemyList();
        foreach(var e in eList)
        {
            if(GameUtil.CheckHit(this,e))
            {
                Hit();
                return;
            }
        }
    }
    public void Hit()
    {
        GameOverCoroutine();
    }
    public async void GameOverCoroutine()
    {
        GameManager.Instance.IsPlaying = false;
        await Task.Delay(3000);
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
          new (ConsoleKey.Z,() => Shoot()),
          new (ConsoleKey.X,UseBomb)
        ]);
    }
    public void UseBomb()
    {
        if(GameState.Instance.PStat.BombCount > 0 && GameState.Instance.GetObjectList().Find(x => x is Bomb) == null)
        {
            Bomb.StartBomb(this.Position);
            GameState.Instance.PStat.UseBomb();
            coolTimer.RefreshCool(bombCool);
        }
        
    }
    public void Shoot()
    {
        if(!coolTimer.IsCoolComp(shootCool)) return;
        GameState.Instance.ShootBullet(this.Position+new Vector2(1,0),Direction.Right,100,Faction.Player);
        coolTimer.RefreshCool(shootCool,1000/GameState.Instance.PStat.ShotSpeed);
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
        coolTimer.RefreshCool(moveCool,1000/GameState.Instance.PStat.Speed);
    }
    public CoolTimer coolTimer;
    public const string bombCool = "bombCool";
    public const string shootCool = "shootCool";
    public const string moveCool = "moveCool";
}