using UnityEngine;
using System;

public class RangedEnemyFactory : IMobFactory
{
    static readonly IWeapon[] weapons = { new Bow(), new Gun() };
    readonly GameObject prefab;
    readonly System.Random rand = new();

    public RangedEnemyFactory(GameObject prefab) => this.prefab = prefab;

    public void Create(Vector3 position)
    {
        var go    = GameObject.Instantiate(prefab, position, Quaternion.identity);
        var enemy = go.GetComponent<EnemyShooter>();

        var w = weapons[rand.Next(weapons.Length)];
        enemy.damage = w.Damage;
        Debug.Log($"[Spawner] Ranged Enemy spawned with weapon «{w.Name}» (Damage={w.Damage})");
    }
}
