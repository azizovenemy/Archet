using UnityEngine;

namespace Architecture
{
    public class BankRepository : Repository
    {
        private const string KEY = "TEST_KEY";
        public int money { get; set; }

        public override void Initialize()
        {
            money = PlayerPrefs.GetInt(KEY, 0);
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(KEY, money);
        }
    }
}

