protoc.exe -I=./proto --csharp_out=./ ./proto/*.proto
IF ERRORLEVEL 1 PAUSE

START ../../../Server/PacketGenerator/bin/PacketGenerator.exe ./proto/Protocol.proto ./proto/Item.proto ./proto/Attack.proto
XCOPY /Y Protocol.cs "../../../Client/Assets/Scripts/Packet/Proto"
XCOPY /Y Item.cs "../../../Client/Assets/Scripts/Packet/Proto"
XCOPY /Y Attack.cs "../../../Client/Assets/Scripts/Packet/Proto"
XCOPY /Y ClientPacketManager.cs "../../../Client/Assets/Scripts/Packet"