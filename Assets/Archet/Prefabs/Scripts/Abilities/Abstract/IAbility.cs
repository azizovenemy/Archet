public interface IAbility
{
    void ApplyDamage(ICanBeDamaged canBeDamaged);
    int GetDamage();
    DamageType GetDamageType();
}
