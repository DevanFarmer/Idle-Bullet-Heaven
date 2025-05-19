using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Weapon> weaponList;

    void Start()
    {
        foreach (var perk in weaponList)
        {
            perk.OnObtained(gameObject);
        }
    }

    void Update()
    {
        foreach (var perk in weaponList)
        {
            // check if hasActiveLogic? how performance changing is it?
            perk.OnUpdate(gameObject);
        }
    }

    public void ObtainWeapon(Weapon weapon)
    {
        weaponList.Add(weapon);
        weapon.OnObtained(gameObject);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        weaponList.Remove(weapon);
    }
}
