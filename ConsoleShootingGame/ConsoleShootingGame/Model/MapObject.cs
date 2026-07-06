public abstract class MapObject
{
    public Vector2 Position{get;set;}
    public virtual Vector2 GetSize()
    {
        return new(1,1);
    }
    public virtual void Move(Vector2 additive)
    {
        this.Position += additive;
    }
    public virtual void Teleport(Vector2 moveTo)
    {
        this.Position = moveTo;
    }
    public virtual void Update()
    {
        
    }
    public abstract string[] RenderShape();
}