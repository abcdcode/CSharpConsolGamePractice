public class Elite : Enemy
{
    public override int MaxHP => throw new NotImplementedException();
    public override int Score => 20;
    public override Vector2 GetSize()
    {
        return new Vector2(3,5);
    }
    public override string[] RenderShape()
    {
        return [
            "  ■ ",
            " ■■■",
            "■■■ ",
            " ■■■",
            "  ■ ",
        ];
    }
}