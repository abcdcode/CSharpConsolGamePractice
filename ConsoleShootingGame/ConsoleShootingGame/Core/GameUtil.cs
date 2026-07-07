public static class GameUtil
{
    public static int RandomRange(int start, int end)
    {
        return new Random().Next(start,end);
    }
    public static float RandomRange(float start, float end)
    {
        return new Random().NextSingle() * (end-start) + start;
    }
    /// <summary>
    /// 두 좌표간 각도 구하기. 0~90 우하단, 90~180 좌하단, 270~360 좌상단, 180~270 우상단
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static float GetAngle(Vector2 origin, Vector2 target)
    {
        var dy = target.Y - origin.Y;
        var dx = target.X - origin.X;
        float angle = (float)(Math.Atan2(-dy, dx) * 180 / Math.PI);
        if (angle < 0) angle += 360;
        return angle;
    }
    //맵 오브젝트간 충돌 체크. 챗지피티가 해줌
    public static bool CheckHit(MapObject a, MapObject b)
    {
        var posA = a.Position;
        var sizeA = a.GetSize();
        var posB = b.Position;
        var sizeB = b.GetSize();
        return CheckHit(posA,sizeA,posB,sizeB);
    }
    //특정 좌표와 오브젝트 충돌 체크
    public static bool CheckHit(MapObject a, Vector2 vec)
    {
        var posA = a.Position;
        var sizeA = a.GetSize();
        var posB = vec;
        var sizeB = new Vector2(1,1);
        return CheckHit(posA,sizeA,posB,sizeB);
    }
    public static bool CheckHitCenter(MapObject a, MapObject center)
    {
        var posA = a.Position;
        var sizeA = a.GetSize();
        var posB = center.Position-new Vector2(center.GetSize().X/2,center.GetSize().Y/2);
        var sizeB = center.GetSize();
        return CheckHit(posA,sizeA,posB,sizeB);
    }
    public static bool CheckHit(Vector2 posA, Vector2 sizeA, Vector2 posB, Vector2 sizeB)
    {
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