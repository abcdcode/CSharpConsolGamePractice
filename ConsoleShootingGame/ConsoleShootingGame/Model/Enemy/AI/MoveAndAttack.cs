public class MoveAndAttack : EnemyAI
{
    /// <summary>
    /// 좌측으로 계속 이동하면서 전방으로 쏘는 AI
    /// </summary>
    /// <param name="moveSpeed"></param>
    /// <param name="attackSpeed"></param>
    public MoveAndAttack(float moveSpeed, float attackSpeed)
    {
        coolTimer = new CoolTimer();
        //moveSpeed *= GameUtil.RandomRange(0.9f,1.1f);
        //attackSpeed *= GameUtil.RandomRange(0.9f,1.1f);
        coolTimer.SetCool(Move,(int)(1000/moveSpeed),0,false,MoveTime);
        coolTimer.SetCool(Attack,(int)(1000/attackSpeed),0,false,AttackTime);
    }
    public override void Update()
    {
        coolTimer.Update();
    }
    public void AttackTime()
    {
        int purpleRight = owner.GetSize().X/2;
        GameState.Instance.ShootBullet(owner.Position+new Vector2(-1,purpleRight),Direction.Left,50,Faction.Enemy);
        coolTimer.RefreshCool(Attack);
    }
    public void MoveTime()
    {
        this.owner.Move(new Vector2(-1,0));
        coolTimer.RefreshCool(Move);
    }
    public const string Attack = "Attack";
    public const string Move = "Move";
    public CoolTimer coolTimer;
}