using UnityEngine;

public class Tester : MonoBehaviour
{
    private IInventory _inventory;

    private void Awake()
    {
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //AddRandomApples();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            //RemoveRandomApples();
        }
    }

    //private void AddRandomApples()
    //{
    //    var random = Random.Range(1, 10);
    //    var apple = new Apple(10);
    //    apple.amount = random;
    //    _inventory.TryToAdd(this, apple);
    //}

    //private void RemoveRandomApples()
    //{
    //    var random = Random.Range(1, 20);
    //    _inventory.Remove(this, typeof(Apple), random);
    //}
}
