/// <summary>
/// 적 기체 클래스
/// </summary>
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
    /// <summary>
    /// 플레이어 공격에 이 기체가 적중했는지 판정 메소드
    /// </summary>
    public virtual void BulletCheck()
    {
        var list = GameState.Instance.GetBulletList().FindAll(x => x.faction == Faction.Player);
        foreach(var b in list)
        {
            //탄환 1프레임 이전 위치도 충돌 체크
            var isHit = GameUtil.CheckHit(this,b) || GameUtil.CheckHit(this,b.prevPos);
            if(isHit)
            {
                TakeDamage(GameState.Instance.PStat.Atk);
                GameState.Instance.DeleteBullet(b);
            }
        }
    }
    public virtual void Die()
    {
        //다른 적에 의해 소환된 토큰형 몹이라면 점수를 제공하지 않음
        if(!isToken)
        {
            GameState.Instance.AddScore(this.Score);
        }
        //드랍 정보가 있다면 드랍. 좌측 방향 디폴트에 무작위로 위아래 중 한 방향으로 대각선 드랍
        if(dropItem != null)
        {
            int rand = GameUtil.RandomRange(0,2);
            Direction dir = Direction.Up;
            if(rand == 0)
            {
                dir = Direction.Down;
            }
            dropItem.Init(Direction.Left,dir);
            dropItem.Position = this.Position;
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