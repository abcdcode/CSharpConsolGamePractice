public abstract class Item : MapObject
{
    public Item()
    {
        coolTimer = new CoolTimer();
    }
    public void Init(Direction dir)
    {
        direction = dir;
    }
    public abstract override string[] RenderShape();
    public override void Update()
    {
        base.Update();
        PlayerCheck();

    }
    public void PlayerCheck()
    {
        Player p = GameState.Instance.player;
        if(GameRule.CheckHit(p,this))
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
        
    }
    protected Direction direction;
    protected CoolTimer coolTimer;
}