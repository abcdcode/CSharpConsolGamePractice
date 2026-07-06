public static class GameRule
{
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
}