public class PowerUp : Item
{
    public override void Earn()
    {
        base.Earn();
        GameState.Instance.PStat.AddAtk(1);
    }
    public override string[] RenderShape()
    {
        return ["P"];
    }
}