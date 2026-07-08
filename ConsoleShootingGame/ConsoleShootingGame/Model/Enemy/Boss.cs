/// <summary>
/// 보스몹
/// </summary>
public class Boss : Enemy
{
    public override int Score => 100;
    public override int MaxHP => 250;
    public override Vector2 GetSize()
    {
        return new Vector2(7,11);
    }

    public override string[] RenderShape()
    {
        return [
            "...■■■■",
            "..■■■■■",
            ".■■■■■.",
            "--■■■■■",
            ".■■■■■■",
            "□■■■■■.",
            ".■■■■■■",
            "--■■■■■",
            ".■■■■■.",
            "..■■■■■",
            "...■■■■",
        ];
    }
}