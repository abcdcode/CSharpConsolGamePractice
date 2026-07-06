/// <summary>
/// 좌측으로 계속 이동만 하는 AI
/// </summary>
public class JustMove : EnemyAI
{
    public JustMove(int moveSpeed)
    {
        coolTimer = new CoolTimer();
        coolTimer.SetCool(Move,1000/moveSpeed,0,false,MoveTime);
    }
    public override void Update()
    {
        base.Update();
        coolTimer.Update();
    }
    public void MoveTime()
    {
        this.owner.Move(new Vector2(-1,0));
        coolTimer.RefreshCool(Move);
    }
    public const string Move = "Move";
    public CoolTimer coolTimer;
}