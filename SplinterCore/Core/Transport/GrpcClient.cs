using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace splinterclient.Core.Transport
{
    internal class GrpcClient
    {
    }
}


//public class AcousticGrpcClient
//{
//    private readonly Channel _channel;
//    private readonly AcousticService.AcousticServiceClient _client;

//    public AcousticGrpcClient(string host)
//    {
//        _channel = new Channel(host, ChannelCredentials.Insecure);
//        _client = new AcousticService.AcousticServiceClient(_channel);
//    }

//    public async Task<PressureMap> CalculatePressureAsync(Room room, Speaker[] speakers)
//    {
//        var request = new AcousticRequest
//        {
//            Room = room.ToGrpcDto(),
//            Speakers = { speakers.Select(s => s.ToGrpcDto()) }
//        };

//        return await _client.CalculatePressureAsync(request);
//    }
//}