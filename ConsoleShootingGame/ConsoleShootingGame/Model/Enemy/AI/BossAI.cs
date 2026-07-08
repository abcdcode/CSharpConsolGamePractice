using System.ComponentModel;

/// <summary>
/// 보스 AI
/// 무적 상태로 지정 X좌표까지 온 뒤에, 상하로 반복 이동하면서 아래 패턴 사용
/// 일정 간격으로 3방향 탄환 발사
/// 일정 간격으로 뒷편에서 잡몹 소환. 잡몹은 전방으로 전진하며 탄환 발사
/// 일정 간격마다 포대에서 레이저 발사. 레이저 발사 중엔 이동 및 탄환 발사 패턴 중지
/// </summary>
public class BossAI : EnemyAI
{
    public BossAI()
    {
        coolTimer = new CoolTimer();
        coolTimer.SetCool(StartMove,333,0,false,Action_StartMove);
        coolTimer.SetCool(UpDownMove,300,0,false,Action_UpDownMove);
        upDown = Direction.Down;
    }
    public override void Update()
    {
        base.Update();
        coolTimer.Update();
    }
    //상하 이동. 개막 패턴 및 레이저 패턴 중엔 중지
    public void Action_UpDownMove()
    {
        if(coolTimer.GetCool(StartMove) != null) 
        {
            goto Refresh;
        }
        var vec = GameUtil.MovePosByDirection(upDown);
        owner.Move(vec);
        if(owner.Position.Y == 0)
        {
            upDown = Direction.Down;
        }
        if(owner.Position.Y == GameState.MapSizeY-owner.GetSize().Y)
        {
            upDown = Direction.Up;
        }

        Refresh:
        coolTimer.RefreshCool(UpDownMove);
    }
    //개막. 지정 X 위치까지 전진
    public void Action_StartMove()
    {
        owner.Move(new Vector2(-1,0));
        if(owner.Position.X == 75)
        {
            coolTimer.DeleteCool(StartMove);
        } else
        {
            coolTimer.RefreshCool(StartMove);
        }
    }
    public Direction upDown;
    public CoolTimer coolTimer;
    public const string Summon = "SummonJaco";
    public const string LaserAttack = "LaserAtk";
    public const string NormalAttack = "NormalAtk";
    public const string UpDownMove = "UpDownMove";
    public const string StartMove = "StartMove";
}