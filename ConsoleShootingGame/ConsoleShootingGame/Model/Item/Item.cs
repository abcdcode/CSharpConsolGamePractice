/// <summary>
/// 드랍 아이템 클래스
/// </summary>
public abstract class Item : MapObject
{
    public Item()
    {
        coolTimer = new CoolTimer();
        coolTimer.SetCool(Move,100,100,false,MoveTime);
        coolTimer.SetCool(Remove,10000,0,true,DeleteItem);
    }
    public void Init(Direction dirLR, Direction dirUD)
    {
        directionLR = dirLR;
        directionUD = dirUD;
    }
    public abstract override string[] RenderShape();
    //그래픽상 크기는 1x1이지만 아이템 수집 범위는 넓게 구현
    public override Vector2 GetSize()
    {
        return new Vector2(5,5);
    }
    public override void Update()
    {
        base.Update();
        coolTimer.Update();
        PlayerCheck();

    }
    public void PlayerCheck()
    {
        Player p = GameState.Instance.player;
        //아이템 먹기 편하라고 중앙 좌표로 size만큼 체크
        if(GameUtil.CheckHitCenter(p,this))
        {
            Earn();
            DeleteItem();
        }
    }
    public virtual void Earn()
    {
        
    }
    public virtual void DeleteItem()
    {
        GameState.Instance.DeleteItem(this);
    }
    public void MoveTime()
    {
        var movePos = GameUtil.MovePosByDirection(directionLR | directionUD);
        Move(movePos);
        if(Position.X == 0)
        {
            directionLR = Direction.Right;
        }
        if(Position.X == GameState.MapSizeX-1)
        {
            directionLR = Direction.Left;
        }
        if(Position.Y == 0)
        {
            directionUD = Direction.Down;
        }
        if(Position.Y == GameState.MapSizeY-1)
        {
            directionUD = Direction.Up;
        }
        coolTimer.RefreshCool(Move);
    }
    protected Direction directionLR;
    protected Direction directionUD;
    protected CoolTimer coolTimer;
    public const string Move = "Move";
    public const string Remove = "Remove";
}