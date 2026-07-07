public class ShotSpeedUp : Item
{
    public override void Earn()
    {
        base.Earn();
        GameState.Instance.PStat.AddShotSpeed(2);
    }
    public override string[] RenderShape()
    {
        return ["R"];
    }
}