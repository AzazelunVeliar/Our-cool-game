using UnityEngine;
using System;

public class MeleeEnemyFactory : IMobFactory
{
    static readonly IWeapon[] weapons = { new Sword(), new Dagger() };
    readonly GameObject prefab;
    readonly System.Random rand = new();

    public MeleeEnemyFactory(GameObject prefab) => this.prefab = prefab;

    public void Create(Vector3 position)
    {
        var go    = GameObject.Instantiate(prefab, position, Quaternion.identity);
        var enemy = go.GetComponent<EnemyAI>();

        var w = weapons[rand.Next(weapons.Length)];
        // напрямую устанавливаем урон
        enemy.attackDamage = w.Damage;
        Debug.Log($"[Spawner] Melee Enemy spawned with weapon «{w.Name}» (Damage={w.Damage})");
    }
}
