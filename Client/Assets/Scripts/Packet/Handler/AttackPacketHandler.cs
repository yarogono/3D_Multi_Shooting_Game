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

    public static void S_GunAttackHandler(PacketSession session, IMessage packet)
    {
        S_GunAttack gunAttackPacket = (S_GunAttack)packet;

        if (gunAttackPacket == null)
            return;

        int attackPlayerId = gunAttackPacket.AttackPlayerId;
        GameObject attackPlayer = ObjectManager.Instance.FindById(attackPlayerId);
        if (attackPlayer == null)
            return;

        PlayerSyncAttack playerSyncAttack = attackPlayer.GetComponent<PlayerSyncAttack>();
        if (playerSyncAttack == null)
            return;

        playerSyncAttack.OnSync(gunAttackPacket);
    }

    public static void S_DamageMeleeHandler(PacketSession session, IMessage packet)
    {
        S_DamageMelee damageMeleePacket = (S_DamageMelee)packet;

        if (damageMeleePacket == null)
            return;

        int targetPlayerId = damageMeleePacket.TargetPlayerId;
        GameObject targetPlayer = ObjectManager.Instance.FindById(targetPlayerId);
        if (targetPlayer == null)
            return;

        PlayerSyncAttack playerSyncAttack = targetPlayer.GetComponent<PlayerSyncAttack>();
        if (playerSyncAttack == null)
            return;

        playerSyncAttack.OnSync(damageMeleePacket);
    }


    public static void S_DamageBulletHandler(PacketSession session, IMessage packet)
    {
        S_DamageBullet damageBulletPacket = (S_DamageBullet)packet;

        if (damageBulletPacket == null)
            return;

        int targetPlayerId = damageBulletPacket.TargetPlayerId;
        GameObject targetPlayer = ObjectManager.Instance.FindById(targetPlayerId);
        if (targetPlayer == null)
            return;

        PlayerSyncAttack playerSyncAttack = targetPlayer.GetComponent<PlayerSyncAttack>();
        if (playerSyncAttack == null)
            return;

        playerSyncAttack.OnSync(damageBulletPacket);
    }
}
