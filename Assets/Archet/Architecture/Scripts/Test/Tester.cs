using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture
{
    public class Tester : MonoBehaviour
    {
        private int value = 10;

        private void Start()
        {
            Game.Run();
        }

        private void Update()
        {
            if (!Bank.isInitialized)
                return;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Bank.AddMoney(this, value);
                Debug.Log($"Money added {value}, {Bank.money}");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(Bank.IsEnoughtMoney(value)) 
                {
                    Bank.SpendMoney(this, value);
                    Debug.Log($"Money spent {value}, {Bank.money}");
                }
            }
        }
    }
}

