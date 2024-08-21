using ConsoleApp.Models;
using ConsoleApp.Requests;

namespace ConsoleApp
{
    public class DeviceManager
    {
        public IEnumerable<Conflict> GetConflicts(IEnumerable<DeviceInfoRequest> deviceInfos)
        {
            var conflicts = deviceInfos
                .GroupBy(x => x.BrigadeCode)
                .Where(x => x.Any(d => d.DeviceStatus))
                .Where(x => x.Count() > 1)
                .Select(b => new Conflict()
                {
                    BrigadeCode = b.Key,
                    DevicesSerials = b.Select(d => d.DeviceSerialNumber).ToArray()
                })
                .ToList();
            return conflicts;
        }
    }
}
