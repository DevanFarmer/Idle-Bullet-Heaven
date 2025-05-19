using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Orbit Attack")]
public class OrbitAttack : Weapon
{
    [Header("Orbiting")]
    public int numberOfSpawns;
    public int numberOfOrbits;
    public float orbitSpeed;

    [Header("Optional Behaviour")]
    public bool spawnAtCenterThenMoveOut;
    public bool returnToCenterAfterOrbitComplete;
    public float moveSpeed;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);
        lastAttackTime = Time.time;
    }

    public override void OnUpdate(GameObject owner)
    {
        if (lastAttackTime + attackSpeed <= Time.time)
        {
            Attack(owner);
            lastAttackTime = Time.time;
        }
    }

    public override void OnUnEquip(GameObject owner)
    {
        base.OnUnEquip(owner);
    }

    void Attack(GameObject owner)
    {
        for (int i = 0; i < numberOfSpawns; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfSpawns;
            Vector3 targetOffset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * range;
            Vector3 spawnPos = spawnAtCenterThenMoveOut ? owner.transform.position : owner.transform.position + targetOffset;

            Quaternion rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

            GameObject weapon = Instantiate(weaponPrefab, spawnPos, rotation, owner.transform);

            // set damage
            DamageComponent dmgComp = weapon.GetComponent<DamageComponent>();
            if (dmgComp == null) dmgComp = weapon.AddComponent<DamageComponent>();

            dmgComp.SetDamage(statManager.GetCalculatedStat(StatType.AttackPower) + power);
            dmgComp.SetEnemyTag("Enemy");

            // Set orbit component
            OrbitComponent orbitComponent = weapon.GetComponent<OrbitComponent>();
            if (orbitComponent == null) orbitComponent = weapon.AddComponent<OrbitComponent>();

            orbitComponent.InitializeComponent(owner.transform, orbitSpeed, numberOfOrbits, range, moveSpeed, spawnAtCenterThenMoveOut, returnToCenterAfterOrbitComplete);
        }
    }
}
