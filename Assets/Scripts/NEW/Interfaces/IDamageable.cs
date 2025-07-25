using UnityEngine;

public interface IDamageable
{
    public float _MaxHealth { get; set; }
    public float _Health { get; set; }
    public bool _IsAlive { get; set; }

    public void ApplyDamage(float damage)
    {
        if (_IsAlive)
        {
            _Health -= damage;

            _IsAlive = _Health > 0;
            if (!_IsAlive) { DeathHandler(); }
        }
    }

    public void ApplyHealth(float amount)
    {
        if (_IsAlive)
        {
            _Health += amount;
            if (_Health > _MaxHealth) { _Health = _MaxHealth; }
        }
    }

    public void DeathHandler();
}