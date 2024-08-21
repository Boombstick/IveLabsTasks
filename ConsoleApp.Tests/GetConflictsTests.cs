using ConsoleApp.Requests;
using System.Text.Json.Nodes;

namespace ConsoleApp.Tests
{
    public class GetConflictsTests
    {
        private DeviceManager _deviceManager;
        [SetUp]
        public void Setup()
        {
            _deviceManager = new DeviceManager();
        }
        /// <summary>
        /// У одной бригады по 1 device - В итоге должен быть пустой массив
        /// </summary>
        [TestCase("[\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"913580515e13\",\r\n      \"isOnline\": false\r\n    },\r\n    \"brigade\": { \"code\": \"2027154745\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n        \"serialNumber\": \"624535123e13\",\r\n      \"isOnline\": false\r\n    },\r\n    \"brigade\": { \"code\": \"2027154745\" }\r\n},\r\n  {\r\n    \"device\": {\r\n        \"serialNumber\": \"834187674e13\",\r\n      \"isOnline\": true\r\n    },\r\n    \"brigade\": { \"code\": \"701859686\" }\r\n}]")]
        public void Test1(string json)
        {
            var deviceInfos = ConvertJsonToDeviceRequest(json);
            Assert.That(_deviceManager.GetConflicts(deviceInfos), Has.Count.EqualTo(0));
        }
        /// <summary>
        /// У одной бригады 2 device, но один online - В итоге должен быть массив c одной записью
        /// </summary>
        [TestCase("[{\"device\": {\"serialNumber\": \"913580515e13\",\"isOnline\": false},\"brigade\": { \"code\": \"2027154745\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"624535123e13\",\r\n      \"isOnline\": false\r\n    },\r\n    \"brigade\": { \"code\": \"2027154745\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"834187674e13\",\r\n      \"isOnline\": true\r\n    },\r\n    \"brigade\": { \"code\": \"701859686\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"834187674e133\",\r\n      \"isOnline\": false\r\n    },\r\n    \"brigade\": { \"code\": \"701859686\" }\r\n  }\r\n]")]
        [TestCase("[{\"device\": {\"serialNumber\": \"913580515e13\",\"isOnline\": false},\"brigade\": { \"code\": \"2027154745\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"624535123e13\",\r\n      \"isOnline\": false\r\n    },\r\n    \"brigade\": { \"code\": \"2027154745\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"834187674e13\",\r\n      \"isOnline\": true\r\n    },\r\n    \"brigade\": { \"code\": \"701859686\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"834187674e133\",\r\n      \"isOnline\": true\r\n    },\r\n    \"brigade\": { \"code\": \"701859686\" }\r\n  }\r\n]")]
        public void Test2(string json)
        {
            var deviceInfos = ConvertJsonToDeviceRequest(json);
            Assert.That(_deviceManager.GetConflicts(deviceInfos), Has.Count.EqualTo(1));
        }

       /// <summary>
       /// У одной бригады 2 device, но оба offline - В итоге должен быть пустой массив
       /// </summary>
        [TestCase("[{\"device\": {\"serialNumber\": \"913580515e13\",\"isOnline\": false},\"brigade\": { \"code\": \"2027154745\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"624535123e13\",\r\n      \"isOnline\": false\r\n    },\r\n    \"brigade\": { \"code\": \"2027154745\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"834187674e13\",\r\n      \"isOnline\": false\r\n    },\r\n    \"brigade\": { \"code\": \"701859686\" }\r\n  },\r\n  {\r\n    \"device\": {\r\n      \"serialNumber\": \"834187674e133\",\r\n      \"isOnline\": false\r\n    },\r\n    \"brigade\": { \"code\": \"701859686\" }\r\n  }\r\n]")]
        public void Test3(string json)
        {
            var deviceInfos = ConvertJsonToDeviceRequest(json);
            Assert.That(_deviceManager.GetConflicts(deviceInfos), Has.Count.EqualTo(0));
        }

        private IEnumerable<DeviceInfoRequest> ConvertJsonToDeviceRequest(string json)
        {
            var devicesJson = JsonArray.Parse(json);
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
            return deviceInfos;
        }
    }
}