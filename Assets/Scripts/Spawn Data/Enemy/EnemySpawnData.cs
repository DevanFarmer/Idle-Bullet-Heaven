using EventBusEventData;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySpawnData", menuName = "Spawn Data/Enemy")]
public class EnemySpawnData : CharacterSpawnData
{
    // Can make a baseCharacter Class that stores common things like prefab, attack animation etc.
    // Weapon/Attack?

    public float expPoints;

    public GameObject damagePopupPrefab;

    public override GameObject Spawn(Vector3 spawnPos)
    {
        GameObject enemy = base.Spawn(spawnPos);

        SetExperienceGiver(enemy);

        AddOnHitActions(enemy);
        AddOnDeathActions(enemy);

        return enemy;
    }

    void SetExperienceGiver(GameObject enemy)
    {
        ExperienceGiver expGiver = enemy.GetComponent<ExperienceGiver>();
        if (expGiver == null) { Debug.LogWarning($"{enemy.name} had no ExperienceGiver! A blank component was added!"); expGiver = enemy.AddComponent<ExperienceGiver>(); }
        expGiver.SetExpPoints(expPoints);

        IHealth health = enemy.GetComponent<IHealth>(); // gonna assume it is added by now
        health.AddOnDeathAction(() => expGiver.GiveExperience());
    }

    protected override void AddOnHitActions(GameObject spawnObject)
    {
        IHealth health = spawnObject.GetComponent<IHealth>();
        health.AddOnHitAction(HandleDamagePopup);
        health.AddOnHitAction(SetCharacterOnHitEvent);
    }

    protected override void AddOnDeathActions(GameObject spawnObject)
    {
        IHealth health = spawnObject.GetComponent<IHealth>();
        health.AddOnDeathAction(() => EventBus.Publish(new EnemyDeathEvent()));
        health.AddOnDeathAction(() => Destroy(spawnObject));
    }

    protected override void SetCharacterOnHitEvent(float damage, Transform spawnObject)
    {
        EventBus.Publish(new EnemyHitEvent(spawnObject.gameObject, damage));
    }

    void HandleDamagePopup(float damage, Transform enemy)
    {
        GameObject popup = Instantiate(damagePopupPrefab, enemy.position, Quaternion.identity);
        popup.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
    }
}
