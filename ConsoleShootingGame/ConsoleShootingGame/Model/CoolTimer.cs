public class CoolTimer
{
    public CoolTimer()
    {
        coolDic = new Dictionary<string, CoolInfo>();
    }
    public void SetCool(string id, int cool, int startCool, bool isOnce, Action callBack)
    {
        CoolInfo info = new (startCool,cool,callBack,isOnce);
        coolDic[id] = info;
    }
    public void DeleteCool(string id)
    {
        if(coolDic.ContainsKey(id))
        {
            coolDic.Remove(id);
        }
    }
    public void RefreshCool(string id)
    {
        if(coolDic.ContainsKey(id))
        {
            coolDic[id].cur = 0;
        }
    }
    public CoolInfo GetCool(string id)
    {
        if(coolDic.ContainsKey(id)) return coolDic[id];
        return null;
    }
    public bool IsCoolComp(string id)
    {
        if(!coolDic.ContainsKey(id)) return true;
        return coolDic[id].cur >= coolDic[id].cool;
    }
    public void Update()
    {
        foreach(var pair in coolDic.ToList())
        {
            var ci = pair.Value;
            ci.cur += GameManager.FrameTime;
            if(ci.cur >= ci.cool)
            {
                if(ci.callBack != null) ci.callBack();
                if(ci.IsOnce) DeleteCool(pair.Key);
            }
        }
    }
    public Dictionary<string,CoolInfo> coolDic;
    public class CoolInfo
    {
        public CoolInfo(int c, int co, Action action, bool io)
        {
            cur = c;
            cool = co;
            callBack = action;
            IsOnce = io;
        }
        public int cur;
        public int cool;
        public Action callBack;
        public bool IsOnce;
    }
}