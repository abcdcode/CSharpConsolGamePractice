public abstract class MapObject
{
    public Vector2 Position{get;set;}
    public virtual void Move(Vector2 additive)
    {
        this.Position += additive;
    }
    public virtual void Teleport(Vector2 moveTo)
    {
        this.Position = moveTo;
    }
    public abstract string[] RenderShape();
}