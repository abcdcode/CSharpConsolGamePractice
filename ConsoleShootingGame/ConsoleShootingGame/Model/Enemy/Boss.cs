public class Boss : Enemy
{
    public override int MaxHP => 200;

    public override string[] RenderShape()
    {
        return [
            "   ■■■■",
            "  ■■■■■",
            " ■■■■■ ",
            "--■■■■■",
            " ■■■■■■",
            "□■■■■■ ",
            " ■■■■■■",
            "--■■■■■",
            " ■■■■■ ",
            "  ■■■■■",
            "   ■■■■",
        ];
    }
}