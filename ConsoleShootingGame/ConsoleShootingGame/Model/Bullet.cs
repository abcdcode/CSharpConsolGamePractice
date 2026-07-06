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
        var mResult = new Vector2();
        if(direction.HasFlag(Direction.Left))
        {
            mResult += new Vector2(-1,0);
        }
        if(direction.HasFlag(Direction.Right))
        {
            mResult += new Vector2(1,0);
        }
        if(UpDownSpeedLimit >= UpDownCount)
        {
            UpDownSpeedLimit = 0;
            if(direction.HasFlag(Direction.Up))
            {
                mResult += new Vector2(0,-1);
            }
            if(direction.HasFlag(Direction.Down))
            {
                mResult += new Vector2(0,1);
            }
        }
        UpDownSpeedLimit += 1;
        Move(mResult);
    }
    public void CheckOut()
    {
        if(Position.X < 0 || Position.X >= GameState.MapSizeX || Position.Y < 0 || Position.Y >= GameState.MapSizeY)
        {
            GameState.Instance.DeleteBullet(this);
        }
    }
    //실제 대각선 사격 상하 이동 카운팅
    private int UpDownSpeedLimit = 0;
    //대각선 사격 시 속도 제어용
    public int UpDownCount = 3;
    public int posProgress = 0;
    public int bulletSpeed = 100;
    public Direction direction;
    public Faction faction;
}
