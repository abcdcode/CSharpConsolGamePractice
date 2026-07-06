public abstract class Enemy : MapObject
{
    public Enemy()
    {
        HP = MaxHP;
    }
    public virtual int HP{get;set;}
    public abstract int MaxHP{get;}
    public override abstract string[] RenderShape();
    public override void Update()
    {
        base.Update();
        if(enemyAI != null)
        {
            enemyAI.Update();
        }
    }
    public virtual void Die()
    {
        
    }
    public virtual void TakeDamage(int value)
    {
        HP -= value;
        if(HP <= 0)
        {
            Die();
        }
    }
    public virtual void Heal(int value)
    {
        HP = Math.Min(MaxHP,HP+value);
    }
    

    protected EnemyAI enemyAI;
}