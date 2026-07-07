
/// <summary>
/// 지정한 좌표까지 이동 완료후 공격 시작.
/// 공격은 정면-대각선1-대각선2 반복
/// </summary>
public class EliteAI : EnemyAI
{
        public EliteAI(Vector2 finalPos,float moveSpeed, float attackSpeed)
    {
        coolTimer = new CoolTimer();
        this.finalPos = finalPos;
        atkSpeed = attackSpeed;
        coolTimer.SetCool(Move,(int)(1000/moveSpeed),0,false,MoveTime);
    }
    public override void Update()
    {
        base.Update();
        coolTimer.Update();
    }
    public void AttackTime()
    {
        int purpleRight = owner.GetSize().Y/2;
        if(attackPattern == 0)
        {
            GameState.Instance.ShootBullet(owner.Position+new Vector2(-1,purpleRight),Direction.Left,50,Faction.Enemy);
            coolTimer.RefreshCool(Attack);
        }
        else if(attackPattern == 1)
        {
            GameState.Instance.ShootBullet(owner.Position+new Vector2(-1,purpleRight),Direction.Left | Direction.Up,50,Faction.Enemy,8);
            GameState.Instance.ShootBullet(owner.Position+new Vector2(-1,purpleRight),Direction.Left | Direction.Up,50,Faction.Enemy,8);
            coolTimer.RefreshCool(Attack);
        }
        else if(attackPattern == 2)
        {
            GameState.Instance.ShootBullet(owner.Position+new Vector2(-1,purpleRight),Direction.Left | Direction.Up,50,Faction.Enemy,5);
            GameState.Instance.ShootBullet(owner.Position+new Vector2(-1,purpleRight),Direction.Left | Direction.Up,50,Faction.Enemy,5);
            coolTimer.RefreshCool(Attack);
        }
        attackPattern += 1;
    }
    public void MoveTime()
    {
        var vector = new Vector2();
        if(finalPos.X < owner.Position.X) vector += new Vector2(-1,0);
        if(finalPos.X > owner.Position.X) vector += new Vector2(1,0);
        if(finalPos.Y < owner.Position.Y) vector += new Vector2(0,-1);
        if(finalPos.Y > owner.Position.Y) vector += new Vector2(0,1);
        this.owner.Move(vector);
        if(this.owner.Position == finalPos)
        {
            coolTimer.DeleteCool(Move);
            coolTimer.SetCool(Attack,(int)(1000/atkSpeed),0,false,AttackTime);
        } else
        {
            coolTimer.RefreshCool(Move);
        }
    }
    private int attackPattern = 0;
    private Vector2 finalPos;
    private float atkSpeed;
    public CoolTimer coolTimer;
    public const string Attack = "Attack";
    public const string Move = "Move";
}