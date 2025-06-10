using UnityEngine;

// intermidiate class for things shared between minions and enemies like baseattack, targetmask and movement
public class NPCSpawnData : CharacterSpawnData
{
    public BaseAttack attack; // limit to one? easy to make into list but not sure if should since range and melee attacks work every differently
    public LayerMask targetLayer;

    public override GameObject Spawn(Vector3 spawnPos)
    {
        GameObject npc = base.Spawn(spawnPos);

        SetAttack(npc);
        SetTargeting(npc);
        SetMovement(npc);

        return npc;
    }

    void SetAttack(GameObject npc)
    {
        AttackHolder holder = npc.GetComponent<AttackHolder>();
        if (holder == null) { Debug.LogWarning($"{npc.name} has no AttackHolder component! One was added!"); holder = npc.AddComponent<AttackHolder>(); }

        holder.SetAttack(attack);
    }

    void SetTargeting(GameObject npc)
    {
        TargetComponent targetComp = npc.GetComponent<TargetComponent>();
        if (targetComp == null) { Debug.LogWarning($"{npc.name} has no TargetComponent! One was added!"); targetComp = npc.AddComponent<TargetComponent>(); }

        targetComp.SetTargetMask(targetLayer);

        // could move to own method but not important
        // StatManager would be setup by now and if doesn't have detection range stat then detection range will be set to 0 as that is what will be returned
        IStats npcStats = npc.GetComponent<IStats>();
        targetComp.SetDetectionRange(npcStats.GetCalculatedStat(StatType.DetectionRange));
    }

    void SetMovement(GameObject npc)
    {
        MovementComponent moveComp = npc.GetComponent<MovementComponent>();
        if (moveComp == null) { Debug.LogWarning($"{npc.name} has no MovementComponent! One was added!"); moveComp = npc.AddComponent<MovementComponent>(); }

        IStats npcStats = npc.GetComponent<IStats>();
        moveComp.SetMoveSpeed(npcStats.GetCalculatedStat(StatType.MoveSpeed));
    }

    // configure:
    // + baseattack
    // + detection range
    // + target mask
    // + movement
}
