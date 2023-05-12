using System.Diagnostics;
using System.Text.Json;

var whitelistedHeaders = new HashSet<string>() { "Ping", "LatencyPingResponse" };
var clientType = Xabbo.ClientType.Flash;
var filePath = "record_1.txt";

File.Create(filePath).Dispose();

var stopwatch = Stopwatch.StartNew();

for (var i = 0; i <= 4000; i++)
{
  if (!Messages.In.TryGetHeader(clientType, (short)i, out Header header) || whitelistedHeaders.Contains(header.GetName(clientType)))
    continue;
  
  OnIntercept(header, e => 
  {
    var recordedPacket = new RecordedPacket
    {
      ElapsedMilliseconds = stopwatch.ElapsedMilliseconds,
      Header = e.Packet.Header.GetName(clientType),
      Buffer = Convert.ToBase64String(e.Packet.Buffer),
      Protocol = e.Packet.Protocol.ToString()
    };
    
    var streamWriter = new StreamWriter(filePath, true);
    streamWriter.WriteLine(JsonSerializer.Serialize(recordedPacket));
    streamWriter.Close();
  });
}

class RecordedPacket
{
  public long ElapsedMilliseconds { get; set; }
  public string Header { get; set; }
  public string Buffer { get; set; }
  public string Protocol { get; set; }
}

Delay(-1);