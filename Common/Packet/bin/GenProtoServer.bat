protoc.exe -I=./proto --csharp_out=./ ./proto/*.proto
IF ERRORLEVEL 1 PAUSE

START ../../../Server/PacketGenerator/bin/PacketGenerator.exe ./proto/protocol.proto ./proto/item.proto
XCOPY /Y Protocol.cs "../../../Server/Server/Packet/Proto"
XCOPY /Y Item.cs "../../../Server/Server/Packet/Proto"
XCOPY /Y ServerPacketManager.cs "../../../Server/Server/Packet"