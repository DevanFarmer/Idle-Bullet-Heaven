using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIBarManager : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    Image bar;

    [SerializeField] CharacterType characterType;

    bool setupComplete;

    void Start()
    {
        StartCoroutine(DelayedSetup());
    }

    void LateUpdate()
    {
        if (!setupComplete) return;
        bar.fillAmount = healthComponent.GetCurrentHealth() / healthComponent.GetMaxHealth();
    }

    IEnumerator DelayedSetup()
    {
        setupComplete = false;

        yield return new WaitForSeconds(0.5f);

        GameObject player = GameManager.Instance.GetPlayer();

        if (characterType == CharacterType.Player)
        {
            healthComponent = player.GetComponent<HealthComponent>();
        }
        else if (characterType == CharacterType.Shield)
        {
            healthComponent = FindComponentOnlyInChildren<HealthComponent>(player.transform);
        }
        else
        {
            Debug.LogWarning($"Select either Player or Shield character type to get proper health component! {gameObject.name}");
        }

        bar = GetComponent<Image>();

        setupComplete = true;
    }

    // got from chatgpt but useful so add to utility namespace
    T FindComponentOnlyInChildren<T>(Transform parent) where T : Component
    {
        foreach (Transform child in parent)
        {
            T found = child.GetComponentInChildren<T>();
            if (found != null)
                return found;
        }
        return null;
    }
}
