public class EliteAI : EnemyAI
{
        public EliteAI(float moveSpeed, float attackSpeed)
    {
        coolTimer = new CoolTimer();
        //moveSpeed *= GameUtil.RandomRange(0.9f,1.1f);
        //attackSpeed *= GameUtil.RandomRange(0.9f,1.1f);
        coolTimer.SetCool(Move,(int)(1000/moveSpeed),0,false,MoveTime);
        coolTimer.SetCool(Attack,(int)(1000/attackSpeed),0,false,AttackTime);
    }
    public override void Update()
    {
        base.Update();
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