public class Jaco : Enemy
{
    public override int MaxHP => 1;
    public override string[] RenderShape()
    {
        return ["◀"];
    }
}