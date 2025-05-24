using UnityEngine;

namespace PerkUtilities
{
    #region Healing
    public static class HealHelper
    {
        // Be more specific
        public static void Heal(GameObject character, float healValue, ModifierType valueType)
        {
            HealthComponent health = character.GetComponent<HealthComponent>();
            // check if has, throw error if not

            // chance if for lifesteal, using current health or make health etc.
            float healAmount = healValue;

            if (valueType == ModifierType.Percentage)
            {
                healAmount = health.GetMaxHealth() * (healValue / 100f);
            }
            ////

            health.Heal(healAmount);
        }
    }
    #endregion
}
