using System;
using System.Collections.Generic;

namespace BigHW.BankAccount
{
    public class BankAccount : IBalanceSubject
    {
        public List<Operation.Operation> _balanceHistory { get; set; }
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public float Balance { get; set; }
        
        private List<IBalanceObserver> _observers = new List<IBalanceObserver>();

        public BankAccount(string name, float balance)
        {
            _balanceHistory = new List<Operation.Operation>();
            Id = Guid.NewGuid();
            Name = name;
            Balance = balance;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nName: {Name}\nBalance: {Balance}";
        }
        
        public void Attach(IBalanceObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Detach(IBalanceObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void Notify(float oldBalance, float newBalance)
        {
            foreach (var observer in _observers)
            {
                observer.OnBalanceChanged(this, oldBalance, newBalance);
            }
        }
    }
}