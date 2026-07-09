using System.Text.Json.Serialization;

public class RecordScene : Scene
{
    public override void CheckInput(List<KeyAction> keyInputs)
    {
        if(coolTimer.IsCoolComp("Press"))
        {
            for(int i = (int)ConsoleKey.A; i <= (int)ConsoleKey.Z; i++)
            {
                var c = ((ConsoleKey)i).ToString()[0];
                keyInputs.Add(new((ConsoleKey)i,()=>PressABC(c)));
            }
            keyInputs.Add(new(ConsoleKey.Enter,PressOk));
            keyInputs.Add(new(ConsoleKey.Backspace,PressRemove));
        }
        /*
        keyInputs.Add(new(ConsoleKey.A,()=>PressABC('A')));
        keyInputs.Add(new(ConsoleKey.B,()=>PressABC('B')));
        keyInputs.Add(new(ConsoleKey.C,()=>PressABC('C')));
        keyInputs.Add(new(ConsoleKey.D,()=>PressABC('D')));
        keyInputs.Add(new(ConsoleKey.E,()=>PressABC('E')));
        keyInputs.Add(new(ConsoleKey.F,()=>PressABC('F')));
        keyInputs.Add(new(ConsoleKey.G,()=>PressABC('G')));
        keyInputs.Add(new(ConsoleKey.H,()=>PressABC('H')));
        keyInputs.Add(new(ConsoleKey.I,()=>PressABC('I')));
        keyInputs.Add(new(ConsoleKey.J,()=>PressABC('J')));
        keyInputs.Add(new(ConsoleKey.K,()=>PressABC('K')));
        keyInputs.Add(new(ConsoleKey.L,()=>PressABC('L')));
        keyInputs.Add(new(ConsoleKey.M,()=>PressABC('M')));
        keyInputs.Add(new(ConsoleKey.N,()=>PressABC('N')));
        keyInputs.Add(new(ConsoleKey.O,()=>PressABC('O')));
        keyInputs.Add(new(ConsoleKey.P,()=>PressABC('P')));
        keyInputs.Add(new(ConsoleKey.Q,()=>PressABC('Q')));
        keyInputs.Add(new(ConsoleKey.R,()=>PressABC('R')));
        keyInputs.Add(new(ConsoleKey.S,()=>PressABC('S')));
        keyInputs.Add(new(ConsoleKey.T,()=>PressABC('T')));
        keyInputs.Add(new(ConsoleKey.U,()=>PressABC('U')));
        keyInputs.Add(new(ConsoleKey.V,()=>PressABC('V')));
        keyInputs.Add(new(ConsoleKey.W,()=>PressABC('W')));
        keyInputs.Add(new(ConsoleKey.X,()=>PressABC('X')));
        keyInputs.Add(new(ConsoleKey.Y,()=>PressABC('Y')));
        keyInputs.Add(new(ConsoleKey.Z,()=>PressABC('Z')));
        */
    }

    public override char[,] Render()
    {
        BufferClear();
        var rString = RecordInfo.CreateRecordBoard(records);
        DrawSString(0,0,rString);
        /*
        DrawString(0,0,$"Rank{new string(' ',4)}Score{new string(' ',4)}Name");
        int index = 1;
        foreach(var r in records)
        {
            
            string st = "TH";
            if(index == 1) st = "ST";
            if(index == 2) st = "ND";
            if(index == 3) st = "RD";
            string sc = $"{r.score}";
            DrawString(0,index,$"{index}{st}{new string(' ',5)}{new string('0',5-sc.Length)}{sc}{new string(' ',5)}{r.name}");
            index += 1;
            if(index == 11) break;
        }
        */
        var c = records.Count > 10 ? 10 : records.Count;
        var cc = $"{GameState.Instance.Score}";
        DrawString(0,c+1,$"{new string(' ',8)}{new string('0',5-cc.Length)}{cc}{new string(' ',5)}{new string(recordBuffer)}");
        return buffer;
    }

    public override void Update()
    {
        coolTimer.Update();
    }
    public override void OnChangeScene(Scene prevScene)
    {
        base.OnChangeScene(prevScene);
        GameManager.Instance.IsPlaying = false;
        coolTimer = new CoolTimer();
        recordBuffer = new char[3];
        bufferPointer = 0;
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
    public void SaveRecord()
    {
        string s = string.Empty;
        for(int i = 0 ; i < records.Count; i++)
        {
            var r = records[i];
            s += r.ToString();
            if(i != records.Count-1) s += "/";
        }
        File.WriteAllText("Record.txt",s);
    }
    public void PressRemove()
    {
        if(bufferPointer == 0) return;
        bufferPointer -= 1;
        recordBuffer[bufferPointer] = ' ';
        coolTimer.SetCool("Press",InputDelay,0,true,null);
    }
    public void PressOk()
    {
        if(bufferPointer != 3) return;
        RecordInfo info = new RecordInfo(GameState.Instance.Score,new string(recordBuffer));
        records.Add(info);
        SaveRecord();
        GameManager.Instance.ChangeScene(SceneName.Title);
        coolTimer.SetCool("Press",InputDelay,0,true,null);
    }
    
    public void PressABC(char c)
    {
        if(bufferPointer == 3) return;
        recordBuffer[bufferPointer] = c;
        bufferPointer += 1;
        coolTimer.SetCool("Press",InputDelay,0,true,null);
    }
    public CoolTimer coolTimer;
    public List<RecordInfo> records;
    public int bufferPointer;
    public char[] recordBuffer;
    public const int InputDelay = 150;
}
public class RecordInfo
{
    public RecordInfo(int sc, string n)
    {
        score =sc;
        name = n;
    }
    public RecordInfo(string s)
    {
        var datas = s.Split('.');
        score = int.Parse(datas[0]);
        name = datas[1];
    }
    public static string[] CreateRecordBoard(List<RecordInfo> list)
    {
        List<string> buffer = new List<string>();
        buffer.Add($"Rank{new string(' ',4)}Score{new string(' ',4)}Name");
        int index = 1;
        foreach(var r in list)
        {
            
            string st = "TH";
            if(index == 1) st = "ST";
            if(index == 2) st = "ND";
            if(index == 3) st = "RD";
            string sc = $"{r.score}";
            buffer.Add($"{index}{st}{new string(' ',5)}{new string('0',5-sc.Length)}{sc}{new string(' ',5)}{r.name}");
            index += 1;
            if(index == 11) break;
        }
        return buffer.ToArray();
    }
    public string ToString()
    {
        return $"{score}.{name}";
    }
    public int CompareScore(RecordInfo r)
    {
        if(score > r.score) return 1;
        if(score == r.score) return 0;
        return -1;
    }
    public int score;
    public string name;
}