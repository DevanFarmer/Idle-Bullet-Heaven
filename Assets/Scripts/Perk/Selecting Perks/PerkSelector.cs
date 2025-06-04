using EventBusEventData;
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

    private void OnEnable()
    {
        EventBus.Subscribe<LevelUpEvent>(OnLevelUp);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelUpEvent>(OnLevelUp);
    }

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
    }

    void RemoveBasePerksPlayerAlreadyHas() // keeps perks with upgrades
    {
        bool sameName;
        foreach (Perk perk in playerPerkManager.GetPerkList())
        {
            // cannot simply use contains since perk manager creates a new instance
            // so use perk.name instead
            foreach(Perk p in allAvailablePerks)
            {
                sameName = p.name == perk.name;
                if (sameName)
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
        List<Perk> availablePerks = new List<Perk>(allAvailablePerks);
        bool validPerk;
        int tries, maxTries = 500;
        Perk perk;
        for (int i = 0; i < numberOfChoices; i++)
        {
            validPerk = false;
            perk = null;
            tries = 0;
            while (validPerk == false && tries < maxTries)
            {
                perk = GetRandomPerk(availablePerks);
                validPerk = IsValidPerk(perk, perksChosen);
                tries++;
            }
            choices[i] = GetPerkChoice(perk);
            perksChosen[i] = perk;
            availablePerks.Remove(perk);
        }

        return choices.ToList();
    }

    Perk GetRandomPerk(List<Perk> availablePerks)
    {
        int index = Random.Range(0, availablePerks.Count);
        return availablePerks[index];
    }

    PerkChoice GetPerkChoice(Perk perk) // remnants, remove this
    {
        return new PerkChoice(perk);
    }

    // for ui on selecting a perk send a PerkSelectedEvent that passes the perk that was selected then remove it from list
    public void SelectPerk(Perk perk)
    {
        playerPerkManager.GainPerk(perk);
        allAvailablePerks.Remove(perk);
        // PerkSelectedEvent
    }

    bool IsValidPerk(Perk perk, Perk[] perksChosen)
    {
        // haven't already chosen perk
        if (perksChosen.Contains(perk)) { Debug.Log($"Already chosen: {perk.perkName}"); return false; }

        bool hasPrerequisitePerk = perk.prerequisitePerk != null;
        if (!hasPrerequisitePerk) // if doesnt already have and doesn't have a prerequisite, is valid, no need to check if has prerequisite
        {
            //Debug.Log($"{perk.perkName}: doesnt have a prerequisite perk");
            return true;
        }

        bool alreadyHasPrerequisite = false; // using variables for readability
        foreach (Perk p in playerPerkManager.GetPerkList())
        {
            Debug.Log($"{p.perkName};{p.perkName.Length} : {perk.prerequisitePerk.perkName};{perk.prerequisitePerk.perkName.Length} : {p.perkName == perk.prerequisitePerk.perkName}");
            if (p.perkName == perk.prerequisitePerk.perkName)
            {
                alreadyHasPrerequisite = true;
                break;
            }
        }

        if (alreadyHasPrerequisite)
        {
            //Debug.Log($"{perk.perkName}: already has prerequisite");
            return true;
        }

        //Debug.Log($"{perk.perkName} invalid!");
        return false;
        
    }

    public void OnLevelUp(LevelUpEvent e)
    {
        List<PerkChoice> perks = GetPerkChoices(3);
        uiManager.ShowPerkChoices(perks);
    }
}
