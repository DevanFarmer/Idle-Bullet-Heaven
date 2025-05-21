using UnityEngine;
using UnityEngine.UI;

// could use an interface to find and use methods but quick work for now
public class DecayBarUIManager : MonoBehaviour
{
    HealthComponent health;
    Image decayBar;

    void Start()
    {
        health = GetComponentInParent<HealthComponent>();
        decayBar = GetComponent<Image>();
    }

    void LateUpdate()
    {
        decayBar.fillAmount = health.GetCurrentHealth() / health.GetMaxHealth();
    }
}
