public class Player : MapObject, IInputable
{
    public override string[] RenderShape()
    {
        return ["▷"];
    }
    public override void Update()
    {
        base.Update();
        CheckHit();
    }
    public void CheckHit()
    {
        var bList = GameState.Instance.GetBulletList();
        foreach(var b in bList)
        {
            if(b.faction == Faction.Player) continue;
            if(GameRule.CheckHit(this,b))
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
    public void CheckInput()
    {
        KeyAction[] actions =
        {
          new (ConsoleKey.LeftArrow,() => MoveAction(ConsoleKey.LeftArrow)),
          new (ConsoleKey.RightArrow,() => MoveAction(ConsoleKey.RightArrow)),
          new (ConsoleKey.UpArrow,() => MoveAction(ConsoleKey.UpArrow)),
          new (ConsoleKey.DownArrow,() => MoveAction(ConsoleKey.DownArrow)),
          new (ConsoleKey.Z,() => Shoot())
        };
        IInputable.DefaultInputCheck(actions);
    }
    public void Shoot()
    {
        GameState.Instance.ShootBullet(this.Position,Direction.Right,60,Faction.Player);
    }
    public void MoveAction(ConsoleKey key)
    {
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
    }
}