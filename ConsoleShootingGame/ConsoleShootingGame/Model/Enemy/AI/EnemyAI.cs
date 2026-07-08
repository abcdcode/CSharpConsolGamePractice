/// <summary>
/// 적 AI 클래스
/// </summary>
public abstract class EnemyAI
{
    public virtual void Init(Enemy o)
    {
        owner = o;
    }
    public virtual void Update()
    {
        OutCheck();
    }
    /// <summary>
    /// 기본 맵 외각 탈출 체크 코드. 오브젝트 사이즈 보정 있음. 항상 좌측을 향해 이동하므로 우측 바깥으로는 체크 안함
    /// </summary>
    protected void OutCheck()
    {
        var size = owner.GetSize();
        if(owner.Position.X <= 0-size.X || owner.Position.Y <= 0-size.Y || owner.Position.Y >= GameState.MapSizeY)
        {
            owner.Delete();
        }
    }
    public Enemy owner;
}