using ConsoleApp.Requests;
using System.Text.Json.Nodes;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\Boomb\Desktop\Devices.json";
            var text = File.ReadAllText(path);

            var devicesJson = JsonArray.Parse(text);
            var deviceInfos = new List<DeviceInfoRequest>();
            foreach (var d in devicesJson.AsArray())
            {
                var deviceToken = d["device"];
                var brigadeToken = d["brigade"];
                var req = new DeviceInfoRequest(
                    deviceToken["serialNumber"].GetValue<string>(),
                    deviceToken["isOnline"].GetValue<bool>(),
                    brigadeToken["code"].GetValue<string>());

                deviceInfos.Add(req);
            }
            var conflicts = new DeviceManager().GetConflicts(deviceInfos);
            var serializedConflicts = System.Text.Json.JsonSerializer.Serialize(conflicts);

            File.WriteAllText(@"C:\Users\Boomb\Desktop\Conflicts.json", serializedConflicts);
        }
    }
}
