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
}