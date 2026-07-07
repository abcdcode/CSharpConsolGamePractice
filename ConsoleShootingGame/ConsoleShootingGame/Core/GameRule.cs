public static class GameRule
{
    /// <summary>
    /// 맵오브젝트간 충돌판정. 챗지피티가 짜줌
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool CheckHit(MapObject a, MapObject b)
    {
        var posA = a.Position;
        var sizeA = a.GetSize();
        var posB = b.Position;
        var sizeB = b.GetSize();
        return
        posA.X < posB.X + sizeB.X &&
        posA.X + sizeA.X > posB.X &&
        posA.Y < posB.Y + sizeB.Y &&
        posA.Y + sizeA.Y > posB.Y;
    }
    public static Vector2 MovePosByDirection(Direction dir,int moveValue = 1)
    {
        var mResult = new Vector2();
        if(dir.HasFlag(Direction.Left))
        {
            mResult += new Vector2(-1,0);
        }
        if(dir.HasFlag(Direction.Right))
        {
            mResult += new Vector2(1,0);
        }
        if(dir.HasFlag(Direction.Up))
        {
            mResult += new Vector2(0,-1);
        }
        if(dir.HasFlag(Direction.Down))
        {
            mResult += new Vector2(0,1);
        }
        return mResult;
    }
}