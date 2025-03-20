using System;

namespace BigHW.Operation
{
    public class Operation
    {
        public Guid Id { get; private set; }
        public string Type { get; set; }        
        public Guid BankAccountId { get; set; }
        public float Amount { get;  set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid? CategoryId { get; set; }
        
        public Operation(string type,
            BankAccount.BankAccount account,
            float amount,
            string description,
            Category.Category category)
        {
            Id = Guid.NewGuid();
            Type = type;
            BankAccountId = account.Id;
            Amount = amount;
            Date = DateTime.Now;
            Description = description;
            CategoryId = category?.Id;
        }

        public Operation()
        {
            Id = Guid.NewGuid(); 
        }

        public override string ToString()
        {
            return $"Id: {Id}\nType: {Type}\nBankAccountID: {BankAccountId}\n" +
                   $"Amount: {Amount}\nDate: {Date}\nDescription: {Description}\nCategoryID: {CategoryId}";
        }
    }
}