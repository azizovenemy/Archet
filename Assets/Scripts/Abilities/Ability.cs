public class Ability : IAbility
{
    private int _damage;
    private DamageType _damageType;

    public Ability(int damage, DamageType damageType)
    {
        _damage = damage;
        _damageType = damageType;
    }

    public void ApplyDamage(ICanBeDamaged canBeDamaged)
    {
        canBeDamaged.TakeDamage(_damage, _damageType);
    }

    public int GetDamage()
    {
        return _damage;
    }

    public DamageType GetDamageType()
    {
        return _damageType;
    }
}
