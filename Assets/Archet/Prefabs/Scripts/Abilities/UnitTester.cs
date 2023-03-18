using UnityEngine;

public class UnitTester : MonoBehaviour, ICanBeDamaged
{
    [SerializeField] private DecoratorExampleUIController _ui;

    public void TakeDamage(int damage, DamageType damageType)
    {
        _ui.CreateWidgetDamageValue(damageType, damage);

        Debug.Log($"Damage received. Type: {damageType}, damage: {damage}.");
    }
}
