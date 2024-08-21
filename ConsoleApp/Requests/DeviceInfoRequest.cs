namespace ConsoleApp.Requests
{
    public record DeviceInfoRequest(
        string DeviceSerialNumber,
        bool DeviceStatus,
        string BrigadeCode);
}
