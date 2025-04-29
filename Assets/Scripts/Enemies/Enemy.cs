using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    // Can make a baseCharacter Class that stores common things like prefab, attack animation etc.
    // Weapon/Attack?

    public GameObject prefab;

    public float expPoints;
}
