using EventBusEventData;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// will be using uI toolkit in final version, just quick and dirty rn
public class PerkSelectorUIManager : MonoBehaviour
{
    PerkSelector perkSelector;

    [SerializeField] GameObject perkSelectorCanvas;
    [SerializeField] RectTransform perkChoiceOne;
    [SerializeField] RectTransform perkChoiceTwo;
    [SerializeField] RectTransform perkChoiceThree;

    private void Start()
    {
        perkSelector = GetComponent<PerkSelector>();
        perkSelectorCanvas.SetActive(false);
    }

    public void ShowPerkChoices(List<PerkChoice> perkChoices)
    {
        perkSelectorCanvas.SetActive(true);
        UpdatePerkChoiceInfo(ref perkChoiceOne, perkChoices[0]);
        UpdatePerkChoiceInfo(ref perkChoiceTwo, perkChoices[1]);
        UpdatePerkChoiceInfo(ref perkChoiceThree, perkChoices[2]);
    }

    void UpdatePerkChoiceInfo(ref RectTransform panel, PerkChoice perkChoice)
    {
        TextMeshProUGUI perkName = panel.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI perkDescription = panel.GetChild(1).GetComponent<TextMeshProUGUI>();
        Button button = panel.GetComponent<Button>();

        perkName.text = perkChoice.DisplayPerk.perkName;
        perkDescription.text = perkChoice.DisplayPerk.formattedDescription;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SelectPerk(perkChoice.BasePerk));
    }

    public void SelectPerk(Perk perk)
    {
        perkSelector.SelectPerk(perk);
        perkSelectorCanvas.SetActive(false);
        EventBus.Publish(new PerkSelectedEvent());
    }
}
