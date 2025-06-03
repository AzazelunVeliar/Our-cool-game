using System;
using UnityEngine;

public class BossFactory : IMobFactory
{
    static readonly IWeapon[] meleeWeapons = { new Hammer() };
    static readonly IWeapon[] rangedWeapons = { new Staff() };
    readonly GameObject prefab;
    readonly System.Random rand = new();

    public BossFactory(GameObject prefab) => this.prefab = prefab;

    public void Create(Vector3 position)
    {
        var go = GameObject.Instantiate(prefab, position, Quaternion.identity);
        var boss = go.GetComponent<BossAI>();

        // Выбор стихии
        var elements = (Element[])Enum.GetValues(typeof(Element));
        var element = elements[rand.Next(elements.Length)];
        
        // Сохраняем выбранный элемент в BossAI
   
        boss.CurrentElement = element;
        float mult = element switch
        {
            Element.Ether => 2f,
            Element.Fire => 1.5f,
            Element.Earth => 1.3f,
            Element.Ice => 1.2f,
            _ => 1f
        };

         Debug.Log(element);

        // Выбор оружий
        var mw = meleeWeapons[rand.Next(meleeWeapons.Length)];
        var rw = rangedWeapons[rand.Next(rangedWeapons.Length)];

        // Применение урона с учетом множителя
        boss.attackDamage = Mathf.RoundToInt(mw.Damage * mult);
        boss.heavyAttackDamage = Mathf.RoundToInt(rw.Damage * mult);

        Debug.Log($"[Spawner] Boss spawned: melee «{mw.Name}» DMG={boss.attackDamage}, " +
                 $"ranged «{rw.Name}» DMG={boss.heavyAttackDamage}, element {element} (mult={mult})");
    }
}