using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterStats", menuName = "Stats/Old/Old Character Stats")]
public class OldCharacterStats : ScriptableObject
{
    public float Health;
    public float AttackPower;
    public float AttackSpeed;

    public int InvincibilityHits;

    // public float MinionAttackSpeed; // have things like player, enemy and minion stats for non general stats
    // Add as needed
}
