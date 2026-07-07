public class Boss : Enemy
{
    public override int MaxHP => 200;
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