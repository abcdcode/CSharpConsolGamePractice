public class Bullet : MapObject
{
    
    public override string[] RenderShape()
    {
        if(faction == Faction.Player)
        {
            return ["○"];
        }
        return ["●"];
    }
    public override void Update()
    {
        base.Update();
        posProgress += GameManager.FrameTime;
        if(posProgress >= bulletSpeed)
        {
            MoveByDir();
            posProgress = 0;
        }
        CheckOut();
    }
    public void MoveByDir()
    {
        if(direction == Direction.Left)
        {
            Move(new Vector2(-1,0));
        }
        if(direction == Direction.Right)
        {
            Move(new Vector2(1,0));
        }
        if(direction == Direction.Up)
        {
            Move(new Vector2(0,-1));
        }
        if(direction == Direction.Down)
        {
            Move(new Vector2(0,1));
        }
    }
    public void CheckOut()
    {
        if(Position.X < 0 || Position.X >= GameState.MapSizeX || Position.Y < 0 || Position.Y >= GameState.MapSizeY)
        {
            GameState.Instance.DeleteBullet(this);
        }
    }
    public int posProgress = 0;
    public int bulletSpeed = 100;
    public Direction direction;
    public Faction faction;
}
