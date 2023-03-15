public class AbilityAdditionalDamage : AbilityUpgrade
{
    private int _additionalDamage;
    private DamageType _additionalDamageType;

    public AbilityAdditionalDamage(IAbility ability, int additionalDamage, DamageType additionalDamageType) : base(ability)
    {
        _additionalDamage = additionalDamage;
        _additionalDamageType = additionalDamageType;
    }

    public override void ApplyDamage(ICanBeDamaged canBeDamaged)
    {
        base.ApplyDamage(canBeDamaged);

        canBeDamaged.TakeDamage(_additionalDamage, _additionalDamageType);
    }
}
