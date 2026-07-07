public abstract class Enemy : MapObject
{
    public virtual void Init(EnemyAI ai, Item d)
    {
        HP = MaxHP;
        this.enemyAI = ai;
        dropItem = d;
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
        if(!isToken)
        {
            GameState.Instance.AddScore(this.Score);
        }
        if(dropItem != null)
        {
            GameState.Instance.AddItem(dropItem);
        }
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
    public bool isToken = false;
    protected EnemyAI enemyAI;
    protected Item dropItem;
}