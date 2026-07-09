/// <summary>
/// 플레이어 기체 스탯
/// </summary>
public class PlayerStat
{
    public PlayerStat()
    {
        Atk = 1;
        Speed = 20;
        ShotSpeed = 10;
        BombCount = 3;
    }
    /// <summary>
    /// 공격력. 기본 1
    /// </summary>
    public int Atk{get;private set;}
    /// <summary>
    /// 이동 속도. 1초당 (수치)칸만큼 이동함. 기본 20
    /// </summary>
    public int Speed{get;private set;}
    /// <summary>
    /// 공격 속도 1초당 (수치)회만큼 공격. 기본 10
    /// </summary>
    public int ShotSpeed{get;private set;}
    /// <summary>
    /// 폭탄 갯수. 대충 화면 전체에 대량에 피해를 주는 뭐시깽이로 생각 중
    /// </summary>
    public int BombCount{get;private set;}
    public void AddAtk(int value)
    {
        Atk = Math.Max(1,Atk+value);
    }
    public void AddSpeed(int value)
    {
        Speed = Math.Max(1,Speed+value);
    }
    public void AddShotSpeed(int value)
    {
        ShotSpeed = Math.Max(1,ShotSpeed+value);
    }
    public void UseBomb()
    {
        BombCount -= 1;
    }
}