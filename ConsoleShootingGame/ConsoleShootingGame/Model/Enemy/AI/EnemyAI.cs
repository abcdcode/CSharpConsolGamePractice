public abstract class EnemyAI
{
    public virtual void Init(Enemy o)
    {
        owner = o;
    }
    public abstract void Update();
    public Enemy owner;
}