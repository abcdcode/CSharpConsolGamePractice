public class Jaco : Enemy
{
    public override int Score => 1;
    public override int MaxHP => 1;
    public override string[] RenderShape()
    {
        return ["◀"];
    }
}