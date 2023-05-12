using System.Text.Json;
using System;

var whitelistedHeaders = new List<string>() { "Ping", "LatencyPingResponse", "FloorHeightMap", "NewFriendRequest", "NewConsole" };
var incomingMessages = new Dictionary<string, int>();
var clientType = Xabbo.ClientType.Flash;
var filePath = "record_1.txt";

if (!File.Exists(filePath))
  throw new Exception("Record file doesn't exist!");
  
var recordedPackets = new List<RecordedPacket>();
var streamReader = new StreamReader(filePath);

while (!streamReader.EndOfStream)
    recordedPackets.Add(JsonSerializer.Deserialize<RecordedPacket>(streamReader.ReadLine()));

streamReader.Close();

for (var i = 0; i <= 4000; i++)
{
  if (Messages.In.TryGetHeader(clientType, (short)i, out Header header))
  {
    incomingMessages.Add(header.GetName(clientType), i);
    
    OnIntercept(header, e => 
    {
      if (whitelistedHeaders.Contains(header.GetName(clientType))) 
        return;
        
      e.Block();
    });
  }
}

foreach (var packet in recordedPackets)
{
  _ = Task.Run(() => SendPacket(packet));
}

Delay((int)recordedPackets.Last().ElapsedMilliseconds + 1000);
Log("Finished playing!");
Finish();

async Task SendPacket(RecordedPacket packet)
{
  await Task.Delay((int)packet.ElapsedMilliseconds);
  
  if (Ct.IsCancellationRequested)
    return;
  
  if (!Messages.In.TryGetHeader(packet.Header, out Header header))
  {
    Log($"Packet \"{packet.Header}\" not found!");
    return;
  }
  
  var protocol = (Xabbo.ClientType)Enum.Parse(typeof(Xabbo.ClientType), packet.Protocol);
  await SendAsync(new Packet(header, DecodeBuffer(packet.Buffer), protocol));
}

Span<byte> DecodeBuffer(string buffer) => new(Convert.FromBase64String(buffer));

class RecordedPacket
{
  public long ElapsedMilliseconds { get; set; }
  public string Header { get; set; }
  public string Buffer { get; set; }
  public string Protocol { get; set; }
}