using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamageable, IHealable {
    
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public int CoinValue { get; private set; }

    public float Speed { get; private set; }
    public float AttackRange { get; private set; }
    public float AttackSpeed { get; private set; }

    protected float lastAttack = 0f;

    #region Callbacks
    public delegate void OnDied(GameObject enemyGO);
    public OnDied onDied;
    #endregion

    public void SetStats(CharacterData charData) {
        Health = charData.health;
        Damage = charData.damage;
        CoinValue = charData.coinValue;
        Speed = charData.speed;
        AttackRange = charData.attackRange;
        AttackSpeed = charData.attackSpeed;
        // set sprite
        SpriteRenderer gfx = transform.Find("Gfx").GetComponent<SpriteRenderer>();
        gfx.sprite = charData.spr;
    }

    public virtual void TakeDamage(int amount) {
        // Debug.Log($"{transform.name} take {amount} of damage.");
        Health -= amount;
        if (Health <= 0) {
            Die();
        }
    }

    public void HealAmont(int amount) {
        // Debug.Log($"{transform.name} healed {amount} of health.");
    }

    void Die() {
        onDied?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
