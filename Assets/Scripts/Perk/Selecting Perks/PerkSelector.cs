using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// handles validating selectable perks and choosing for selection
public class PerkSelector : MonoBehaviour
{
    [SerializeField] List<Perk> allAvailablePerks = new(); // need a better way to store list of all available perks
    PerkManager playerPerkManager; // use a Observer for getting player, sends player when it is spawned, good for scripts that need player on start

    PerkSelectorUIManager uiManager;

    void Start()
    {
        uiManager = GetComponent<PerkSelectorUIManager>();
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        while (playerPerkManager == null)
        {
            yield return new WaitForSeconds(0.25f);
            playerPerkManager = GameManager.Instance.GetPlayer().GetComponent<PerkManager>();
        }
        RemoveBasePerksPlayerAlreadyHas();

        List<PerkChoice> perks = GetPerkChoices(3);
        uiManager.ShowPerkChoices(perks);
    }

    void RemoveBasePerksPlayerAlreadyHas() // keeps perks with upgrades
    {
        bool sameName, hasUpgrade;
        foreach (Perk perk in playerPerkManager.GetPerkList())
        {
            // cannot simply use contains since perk manager creates a new instance
            // so use perk.name instead
            foreach(Perk p in allAvailablePerks)
            {
                sameName = p.name == perk.name;
                hasUpgrade = p.upgrade != null;
                if (sameName && !hasUpgrade)
                {
                    allAvailablePerks.Remove(p);
                    break; // only removes first instance, but shouldn't have multiple anyway
                }
            }
        }
    }

    List<PerkChoice> GetPerkChoices(int numberOfChoices) // check here if perk is upgrade or new
    {
        PerkChoice[] choices = new PerkChoice[numberOfChoices];
        Perk[] perksChosen = new Perk[numberOfChoices];
        bool validPerk;
        int tries, maxTries = 20;
        Perk perk;
        for (int i = 0; i < numberOfChoices; i++)
        {
            validPerk = false;
            perk = null;
            tries = 0;
            while (validPerk == false && tries < maxTries)
            {
                perk = GetRandomPerk();
                if (!perksChosen.Contains(perk)) validPerk = true; // what is happening here?
                tries++;
            }
            choices[i] = GetPerkChoice(perk);
            perksChosen[i] = perk;
        }

        return choices.ToList();
    }

    Perk GetRandomPerk()
    {
        int index = Random.Range(0, allAvailablePerks.Count);
        return allAvailablePerks[index];
    }

    PerkChoice GetPerkChoice(Perk perk)
    {
        if (playerPerkManager.HasPerk(perk.perkName))
        {
            return new PerkChoice(perk.upgrade, perk);
        }
        else
        {
            return new PerkChoice(perk);
        }
    }

    // for ui on selecting a perk send a PerkSelectedEvent that passes the perk that was selected then remove it from list
    public void SelectPerk(Perk perk)
    {
        playerPerkManager.GainPerk(perk);
        
        // remove from list, instead of looping through everything
        allAvailablePerks.Remove(perk);

        // PerkSelectedEvent
    }
}
