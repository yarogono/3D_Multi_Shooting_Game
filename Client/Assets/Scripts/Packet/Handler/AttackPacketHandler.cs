using Google.Protobuf.Protocol;
using Google.Protobuf;
using ServerCore;
using UnityEngine;

partial class PacketHandler
{
    public static void S_MeleeAttackHandler(PacketSession session, IMessage packet)
    {
        S_MeleeAttack meleeAttackPacket = (S_MeleeAttack)packet;

        if (meleeAttackPacket == null)
            return;

        int attackPlayerId = meleeAttackPacket.AttackPlayerId;
        GameObject attackPlayer = ObjectManager.Instance.FindById(attackPlayerId);
        if (attackPlayer == null)
            return;

        PlayerSyncAttack playerSyncAttack = attackPlayer.GetComponent<PlayerSyncAttack>();
        if (playerSyncAttack == null)
            return;

        playerSyncAttack.OnSync(meleeAttackPacket);
    }
}
