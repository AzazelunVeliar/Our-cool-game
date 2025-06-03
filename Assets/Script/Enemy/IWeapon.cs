using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    string Name { get; }
    int Damage { get; }
}

public class Sword  : IWeapon { public string Name => "Меч";    public int Damage => 10; }
public class Dagger : IWeapon { public string Name => "Кинжал"; public int Damage =>  7; }
public class Bow    : IWeapon { public string Name => "Лук";    public int Damage =>  8; }
public class Gun    : IWeapon { public string Name => "Огнемёт";public int Damage => 12; }
public class Hammer : IWeapon { public string Name => "Молот";  public int Damage => 10; }
public class Staff  : IWeapon { public string Name => "Посох";  public int Damage => 15; }