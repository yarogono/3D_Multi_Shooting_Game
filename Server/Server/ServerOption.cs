public class ServerOption
{
    public int Port { get; set; } = 7777;
    public int MaxConnectionCount { get; set; } = 500;
    public int ReceiveBufferSize { get; set; } = 4096;
    public int MaxPacketSize { get; set; } = 1024;

    public void WriteConsole()
    {
        Console.WriteLine("[ ServerOption ]");
        Console.WriteLine($"Port: {Port}");
        Console.WriteLine($"MaxConnectionCount: {MaxConnectionCount}");
        Console.WriteLine($"ReceiveBufferSize: {ReceiveBufferSize}");
        Console.WriteLine($"MaxPacketSize: {MaxPacketSize}");
    }
}
