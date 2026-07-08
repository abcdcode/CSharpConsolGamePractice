/// <summary>
/// 이동속도 업 아이템
/// </summary>
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