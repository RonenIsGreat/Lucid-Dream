using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SharedResources
{
    public class TargetsInterpreter : IInterpreter<SystemTracks>
    {
        public SystemTracks ByteArrayToStruct(byte[] data)
        {
            string json = Encoding.UTF8.GetString(data);
            SystemTracks trackData = JsonConvert.DeserializeObject<SystemTracks>(json);
            return trackData;
        }
    }
}
