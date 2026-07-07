public class ShotSpeedUp : Item
{
    public override void Earn()
    {
        base.Earn();
        GameState.Instance.PStat.AddShopSpeed(2);
    }
    public override string[] RenderShape()
    {
        return ["R"];
    }
}