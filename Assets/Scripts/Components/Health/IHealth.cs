using System;
using UnityEngine;

public interface IHealth // could rename if not adding anything else, something like IActionHandler/Manager
{
    public void AddOnDeathAction(Action action);
    public void RemoveOnDeathAction(Action action);

    public void AddOnHitAction(Action<float, Transform> action);
    public void RemoveOnHitAction(Action<float, Transform> action);

    public void SetStatManager(IStats statManager); // doesn't fit with other methods

    public void UpdateMaxHealth();
}
