using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecoratorExampleUIController : MonoBehaviour
{
    [SerializeField] private UnitTester _unit;
    [SerializeField] private Button _button;
    [SerializeField] private WidgetDamageValue _widgetDamageValuePrefab;
    [SerializeField] private Transform _damageValuesContainer;

    private Dictionary<DamageType, Color> _damageColorDict = new Dictionary<DamageType, Color>
    {
        {DamageType.Physical, Color.yellow },
        {DamageType.Magical, Color.blue }
    };

    private void OnEnable()
    {
        _button.onClick.AddListener(OnDamageButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnDamageButtonClick);
    }

    public void CreateWidgetDamageValue(DamageType damageType, int damage)
    {
        var color = _damageColorDict[damageType];
        var widget = Instantiate(_widgetDamageValuePrefab, _damageValuesContainer);
        var maxDistance = 0.5f;
        var randomOffset = Random.insideUnitCircle * maxDistance;
        var position = _damageValuesContainer.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
        widget.transform.position = position;

        widget.SetValue(damage.ToString());
        widget.SetColor(color);
    }

    private void OnDamageButtonClick()
    {
        Debug.Log("Damage");

        IAbility ability = new Ability(10, DamageType.Physical);
        ability = new AbilityDurationalDamage(ability, 5, 10);
        ability = new AbilityAdditionalDamage(ability, 20, DamageType.Magical);
        ability.ApplyDamage(_unit);
    }
}
