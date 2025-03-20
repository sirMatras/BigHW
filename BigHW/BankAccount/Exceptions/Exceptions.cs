namespace BigHW;

public class InvalidAmountException : Exception
{
    public InvalidAmountException() {}
    public InvalidAmountException(string message) : base(message) {}
}