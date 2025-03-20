namespace BigHW
{
    public interface IBalanceSubject
    {
        void Attach(IBalanceObserver observer);
        void Detach(IBalanceObserver observer);
        void Notify(float oldBalance, float newBalance);
    }
}