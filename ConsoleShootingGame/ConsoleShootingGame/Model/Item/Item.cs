public abstract class Item : MapObject
{
    public abstract override string[] RenderShape();
    public override void Update()
    {
        base.Update();
    }
    public void PlayerCheck()
    {
        Player p = GameState.Instance.player;
        if(GameRule.CheckHit(p,this))
        {
            Earn();
        }
    }
    public virtual void Earn()
    {
        
    }
    public virtual void DeleteItem()
    {
        
    }
}