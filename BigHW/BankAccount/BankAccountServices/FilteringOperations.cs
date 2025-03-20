namespace BigHW.BankAccountServices;

public class FilteringOperations
{
    public static void FilterByTransactionType(BankAccount account, string type)
    {
        for (int i = 0; i < account._balanceHistory.Count; i++)
        {
            if (account._balanceHistory[i].Type == type)
            {
                Console.WriteLine(account._balanceHistory.ToString());
            }
        }
    }
}