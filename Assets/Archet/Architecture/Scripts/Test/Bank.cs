using System;

namespace Architecture
{
    public static class Bank
    {
        public static event Action OnBankInitializedEvent;

        public static int money { 
            get {
                CheckClass();
                return bankInteractor.money;
            } 
        }  
        public static bool isInitialized { get; private set; }

        private static BankInteractor bankInteractor;

        public static void Initialize(BankInteractor interactor)
        {
            bankInteractor = interactor;
            isInitialized = true;
            OnBankInitializedEvent?.Invoke();
        }

        public static bool IsEnoughtMoney(int value)
        {
            CheckClass();
            return bankInteractor.IsEnoughtMoney(value);
        }

        public static void AddMoney(object sender, int value)
        {
            CheckClass();
            bankInteractor.AddMoney(sender, value);
        }

        public static void SpendMoney(object sender, int value)
        {
            CheckClass();
            bankInteractor.SpendMoney(sender, value);
        }

        public static void CheckClass()
        {
            if (!isInitialized)
            {
                throw new Exception("Bank is not initialized yet");
            }
        }
    }
}

