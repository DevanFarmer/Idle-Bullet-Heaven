using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles validating selectable perks and choosing for selection
public class PerkSelector : MonoBehaviour
{
    [SerializeField] List<Perk> allAvailablePerks = new(); // need a better way to store list of all available perks
    PerkManager playerPerkManager; // use a Observer for getting player, sends player when it is spawned, good for scripts that need player on start

    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        while (playerPerkManager == null)
        {
            yield return new WaitForSeconds(0.5f);
            playerPerkManager = GameManager.Instance.GetPlayer().GetComponent<PerkManager>();
        }
        RemovePerksPlayerAlreadyHas();
    }

    void RemovePerksPlayerAlreadyHas()
    {
        foreach (Perk perk in playerPerkManager.GetPerkList())
        {
            // cannot simply use contains since perk manager creates a new instance
            // so use perk.name instead
            foreach(Perk p in allAvailablePerks)
            {
                if (p.name == perk.name)
                {
                    allAvailablePerks.Remove(p);
                    break; // only removes first instance, but shouldn't have multiple anyway
                }
            }
        }
    }
}
