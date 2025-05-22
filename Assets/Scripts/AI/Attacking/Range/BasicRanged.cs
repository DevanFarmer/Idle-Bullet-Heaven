using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Ranged", menuName = "Attack/Ranged/Basic Ranged")]
public class BasicRanged : BaseAttack
{
    public GameObject projectileprefab;
    public float moveSpeed;

    public override void Equip(Transform character)
    {
        // character.GetComponent<TargetComponent>().SetDetectionRange(range);
    }

    public override void Attack(Transform character, Transform target, StatManager statManager)
    {
        GameObject projectile = Instantiate(projectileprefab, character.position, Quaternion.identity);

        // Set damage component
        DamageComponent dmgComp = projectile.GetComponent<DamageComponent>();
        if (dmgComp == null) dmgComp = projectile.AddComponent<DamageComponent>();
        dmgComp.SetDamage(GetDamage(statManager));
        dmgComp.SetEnemyTag(target.tag);
        
        // Set projectile component
        ProjectileComponent projComp = projectile.GetComponent<ProjectileComponent>();
        if (projComp == null) projComp = projectile.AddComponent<ProjectileComponent>();
        projComp.SetMoveSpeed(moveSpeed);
        projComp.SetTarget(target);
    }
}
