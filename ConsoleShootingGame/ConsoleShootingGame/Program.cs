var m = new GameManager();
m.Start();
while(true)
{
    if(!GameManager.Instance.IsRunning) return 0;
}