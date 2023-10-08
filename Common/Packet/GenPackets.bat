START ../../Server/PacketGenerator/bin/PacketGenerator.exe ../../Server/PacketGenerator/PDL.xml
XCOPY /Y GenPackets.cs "../../Server/DummyClient/Packet"
XCOPY /Y GenPackets.cs "../../Client/Assets/Scripts/Packet"
XCOPY /Y GenPackets.cs "../../Server/Server/Packet"
XCOPY /Y ClientPacketManager.cs "../../Server/DummyClient/Packet"
XCOPY /Y ClientPacketManager.cs "../../Client/Assets/Scripts/Packet"
XCOPY /Y ServerPacketManager.cs "../../Server/Server/Packet"