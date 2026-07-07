public class SpeedUp : Item
{
    public override void Earn()
    {
        base.Earn();
        GameState.Instance.PStat.AddSpeed(5);
    }
    public override string[] RenderShape()
    {
        return ["S"];
    }
}