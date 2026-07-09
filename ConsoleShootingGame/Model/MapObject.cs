/// <summary>
/// 맵에 표현되는 오브젝트 클래스
/// </summary>
public abstract class MapObject
{
    /// <summary>
    /// 오브젝트의 위치. 좌상단 기준
    /// </summary>
    public Vector2 Position{get;set;}
    /// <summary>
    /// 충돌 체크 시 해당 오브젝트의 크기 판정. RenderShape랑 별개로 운용
    /// </summary>
    /// <returns></returns>
    public virtual Vector2 GetSize()
    {
        return new(1,1);
    }
    public virtual void Move(Vector2 additive)
    {
        this.Position += additive;
    }
    public virtual void Teleport(Vector2 moveTo)
    {
        this.Position = moveTo;
    }
    public virtual void Update()
    {
        
    }
    /// <summary>
    /// 오브젝트 외형. 맵에 해당 외형대로 렌더링됨. 좌상단 기준
    /// </summary>
    /// <returns></returns>
    public abstract string[] RenderShape();
}