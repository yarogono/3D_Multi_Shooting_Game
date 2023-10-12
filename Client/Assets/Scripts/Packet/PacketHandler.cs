using ServerCore;
using UnityEditor.PackageManager;
using UnityEngine;

class PacketHandler
{
    public static void S_PlayerInfoHandler(PacketSession session, IPacket packet)
    {
        
    }


    public static void S_PlayerLoginHandler(PacketSession session, IPacket packet)
    {

    }

    public static void S_SavePlayerHandler(PacketSession session, IPacket packet)
    {
        S_SavePlayer savePlayerPacket = packet as S_SavePlayer;

        int saveOk = savePlayerPacket.saveOk;

        if (saveOk != 1)
            return;

        Debug.Log("회원가입이 완료되었습니다.");
        UIManager.Instance.ClosePopupUI();
    }
}
