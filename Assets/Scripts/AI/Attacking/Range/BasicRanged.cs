using UnityEngine;

public class BasicRanged : BaseAttack
{
    public GameObject projectile;

    public override void Attack(Transform character, Transform target, StatManager statManager)
    {
        Instantiate(projectile, character.position, Quaternion.identity);
        // Set all the projectiles components
    }
}
