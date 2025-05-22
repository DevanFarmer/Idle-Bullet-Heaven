using UnityEngine;

public class BasicRanged : BaseAttack
{
    public GameObject projectileprefab;

    public override void Attack(Transform character, Transform target, StatManager statManager)
    {
        GameObject projectile = Instantiate(projectileprefab, character.position, Quaternion.identity);
        // Set all the projectiles components
        DamageComponent dmgComp = projectile.GetComponent<DamageComponent>();
        if (dmgComp == null) dmgComp = projectile.AddComponent<DamageComponent>();
        dmgComp.SetDamage(GetDamage(statManager));
        dmgComp.SetEnemyTag(target.tag);
    }
}
