/// <summary>
/// 공격속도 업 아이템
/// </summary>
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