public class Elite : Enemy
{
    public override int MaxHP => 75;
    public override int Score => 20;
    public override Vector2 GetSize()
    {
        return new Vector2(4,5);
    }
    public override string[] RenderShape()
    {
        return [
            "..■.",
            ".■■■",
            "■■■.",
            ".■■■",
            "..■.",
        ];
    }
}