using UnityEngine;

[CreateAssetMenu(fileName = "New SimpleStatModifier", menuName = "Perk/Passive/Simple Stat Modifier")]
public class SimpleStatModifier : Perk
{
    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);
        hasActiveLogic = false;
    }
}
