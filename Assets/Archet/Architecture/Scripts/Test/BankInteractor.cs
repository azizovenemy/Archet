namespace Architecture
{
    public class BankInteractor : Interactor
    {
        private BankRepository repository;

        public int money => repository.money;

        public override void OnCreate()
        {
            base.OnCreate();
            this.repository = Game.GetRepository<BankRepository>();
        }

        public override void Initialize()
        {
            Bank.Initialize(this);
        }

        public bool IsEnoughtMoney(int value)
        {
            return money >= value;
        }

        public void AddMoney(object sender, int value)
        {
            this.repository.money += value;
            this.repository.Save();
        }

        public void SpendMoney(object sender, int value)
        {
            this.repository.money -= value;
            this.repository.Save();
        }
    }
}
