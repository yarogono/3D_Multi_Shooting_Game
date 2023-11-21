protoc.exe -I=./ --csharp_out=./ ./Protocol.proto
IF ERRORLEVEL 1 PAUSE

START ../../../Server/PacketGenerator/bin/PacketGenerator.exe ./Protocol.proto
XCOPY /Y Protocol.cs "../../../Server/Server/Packet"
XCOPY /Y ServerPacketManager.cs "../../../Server/Server/Packet"

Rem XCOPY /Y Protocol.cs "../../../Server/DummyClient/Packet"
Rem XCOPY /Y ClientPacketManager.cs "../../../Server/DummyClient/Packet"
