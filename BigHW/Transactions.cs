namespace BigHW;

public class Transactions
{
    private static int _lastId = 0;
    public int Id { get; set; }
    public float Amount { get; set; }
    public DateTime Time { get; set; }
    public string Type { get; set; }

    public Transactions(float amount, string type)
    {
        Id = _lastId++;
        Amount = amount;
        Time = DateTime.Now;
        Type = type;
    }

    public override string ToString()
    {
        return $"ID: {Id}\nAmount: {Amount}\nTime: {Time}\nType: {Type}";
    }
}