public abstract class Enemy : MapObject
{
    public virtual void Init(EnemyAI ai)
    {
        HP = MaxHP;
        this.enemyAI = ai;
        ai.Init(this);
    }
    public virtual int Score => 1;
    public virtual int HP{get;set;}
    public abstract int MaxHP{get;}
    public override abstract string[] RenderShape();
    public override void Update()
    {
        base.Update();
        BulletCheck();
        if(enemyAI != null)
        {
            enemyAI.Update();
        }
    }
    public virtual void BulletCheck()
    {
        var list = GameState.Instance.GetBulletList().FindAll(x => x.faction == Faction.Player);
        foreach(var b in list)
        {
            var isHit = GameRule.CheckHit(this,b);
            if(isHit)
            {
                TakeDamage(1);
                GameState.Instance.DeleteBullet(b);
            }
        }
    }
    public virtual void Die()
    {
        Delete();
    }
    public virtual void Delete()
    {
        GameState.Instance.DeleteEnemy(this);
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