/// <summary>
/// 자폭 기체
/// </summary>
public class KamiKaze : Enemy
{
    public override int Score => 1;
    public override int MaxHP => 4;
    public override Vector2 GetSize()
    {
        return new Vector2(5,1);
    }
    public override string[] RenderShape()
    {
        return ["◀----"];
    }
}