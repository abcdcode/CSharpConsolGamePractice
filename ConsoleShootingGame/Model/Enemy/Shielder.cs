/// <summary>
/// 몸빵몹
/// </summary>
public class Shielder : Enemy
{
    public override int Score => 5;
    public override int MaxHP => 30;

    public override Vector2 GetSize()
    {
        return new Vector2(3,3);
    }
    public override string[] RenderShape()
    {
        return[
            "■■■",
            "■■",
            "■■■"
        ];
    }
}