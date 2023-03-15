using System.Collections;
using UnityEngine;

public class AbilityDurationalDamage : AbilityUpgrade
{
    private float _duration;
    private int _partsAmount;

    public AbilityDurationalDamage(IAbility ability, float duration, int partsAmount) : base(ability)
    {
        _duration = duration;
        _partsAmount = partsAmount;
    }

    public override void ApplyDamage(ICanBeDamaged canBeDamaged)
    {
        Coroutines.StartRoutine(ApplyDamageRoutine(canBeDamaged));
    }

    private IEnumerator ApplyDamageRoutine(ICanBeDamaged canBeDamaged)
    {
        int damage = Mathf.CeilToInt(MainAbility.GetDamage() / (float)_partsAmount);
        float partDuration = _duration / _partsAmount;

        for (int i = 1; i< _partsAmount; i++)
        {
            yield return new WaitForSeconds(partDuration);

            canBeDamaged.TakeDamage(damage, MainAbility.GetDamageType());
        }
    }
}
