namespace BigHW;

public class Exceptions
{
    public void ValidStringChecking(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            throw new ArgumentException("Поле не может быть пустым", nameof(s));
        }
    }

    public void ValidAmountChecking(float amount, string msg)
    {
        if (amount <= 0)
        {
            throw new ArgumentException(msg, nameof(amount));
        }
    }
}