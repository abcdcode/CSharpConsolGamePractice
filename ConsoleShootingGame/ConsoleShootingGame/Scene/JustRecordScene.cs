public class JustRecordBoardScene : Scene
{
    public override void CheckInput(List<KeyAction> keyInputs)
    {
        keyInputs.Add(new([ConsoleKey.D9,ConsoleKey.NumPad9],ReturnToTitle));
    }

    public override char[,] Render()
    {
        BufferClear();
        DrawSString(0,0,RecordInfo.CreateRecordBoard(records));
        var c = records.Count > 10 ? 10 : records.Count;
        DrawString(0,c+2,"9.타이틀로 돌아가기");
        return buffer;
    }

    public override void Update()
    {
    }
    public override void OnChangeScene(Scene prevScene)
    {
        base.OnChangeScene(prevScene);
        records = new List<RecordInfo>();
        if(File.Exists("Record.txt"))
        {
            var str = File.ReadAllText("Record.txt");
            var datas = str.Split('/');
            foreach(var d in datas)
            {
                records.Add(new RecordInfo(d));
            }
        }
        records.Sort((a,b) => b.CompareScore(a));
    }
    public void ReturnToTitle()
    {
        GameManager.Instance.ChangeScene(SceneName.Title);
    }
    public List<RecordInfo> records;
}