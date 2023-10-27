/*
    Simple IoT Central device program for a simulated temperature and humidity sensor.
*/

using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Provisioning.Client.Transport;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


// TODO: set your IoT Central settings here
const string provisioningGlobalDeviceEndpoint = "global.azure-devices-provisioning.net";
const string provisioningIdScope = "Todo";
const string deviceId = "DemoDevice";
const string devicePrimaryKey = "Todo";

object lockObject = new object();

Random random = new Random();
double currentHumidity = 45;
double currentTemperature = 68.2;

Console.WriteLine("*** Starting... ***");

await Task.Delay(1000);

var deviceRegistrationResult = await RegisterDevice();
if (deviceRegistrationResult == null)
{
    return;
}

using var deviceClient = NewDeviceClient(deviceRegistrationResult.AssignedHub);
if (deviceClient == null)
{
    return;
}

using var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;

var sendDeviceDataTask = SendDeviceDataUntilCancelled(deviceClient, cancellationToken);

Console.ReadLine();
Console.WriteLine("Shutting down...");

cancellationTokenSource.Cancel(); // request cancel
sendDeviceDataTask.Wait(); // wait for cancel


#region Provisioning
async Task<DeviceRegistrationResult> RegisterDevice()
{
    try
    {
        Console.WriteLine($"Will register device {deviceId}...");

        // using symmetric keys
        using var securityProvider = new SecurityProviderSymmetricKey(
          registrationId: deviceId,
          primaryKey: devicePrimaryKey,
          secondaryKey: null);

        using var transportHandler = new ProvisioningTransportHandlerMqtt(TransportFallbackType.TcpOnly);

        // set up provisioning client for given device
        var provisioningDeviceClient = ProvisioningDeviceClient.Create(
          globalDeviceEndpoint: provisioningGlobalDeviceEndpoint,
          idScope: provisioningIdScope,
          securityProvider: securityProvider,
          transport: transportHandler);

        // register device
        var deviceRegistrationResult = await provisioningDeviceClient.RegisterAsync();

        Console.WriteLine($"Device {deviceId} registration result: {deviceRegistrationResult.Status}");

        if (deviceRegistrationResult.Status != ProvisioningRegistrationStatusType.Assigned)
        {
            throw new Exception($"Failed to register device {deviceId}");
        }

        Console.WriteLine($"Device {deviceId} was assigned to hub '{deviceRegistrationResult.AssignedHub}'");
        Console.WriteLine();

        return deviceRegistrationResult;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"* ERROR * {ex.Message}", ConsoleColor.Red);
    }

    return null;
}

#endregion

#region DeviceClient
DeviceClient NewDeviceClient(string assignedHub)
{
    try
    {
        Console.WriteLine();
        Console.WriteLine($"Will create client for device {deviceId}...");

        var authenticationMethod = new DeviceAuthenticationWithRegistrySymmetricKey(
          deviceId: deviceId,
          key: devicePrimaryKey);

        var deviceClient = DeviceClient.Create(
           hostname: assignedHub,
           authenticationMethod: authenticationMethod,
           transportType: TransportType.Mqtt_Tcp_Only);

        Console.WriteLine($"Successfully created client for device {deviceId}");

        return deviceClient;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"* ERROR * {ex.Message}");
    }

    return null;
}


async Task SendDeviceDataUntilCancelled(DeviceClient deviceClient, CancellationToken cancellationToken)
{
    try
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // simulate sensor data
            currentTemperature = Convert.ToDouble(random.Next(0, 50));
            currentHumidity = Convert.ToDouble(random.Next(0, 100));

            // create message, propertynams should match IoT Central template!!!
            var payload = new
            {
                Temperature = currentTemperature,
                Humidity = currentHumidity,
            };

            var bodyJson = JsonConvert.SerializeObject(payload, Formatting.Indented);
            var message = new Message(Encoding.UTF8.GetBytes(bodyJson))
            {
                ContentType = "application/json",
                ContentEncoding = "utf-8"
            };

            // send message
            Console.WriteLine();
            Console.WriteLine($"Current temperature: {currentTemperature}");
            Console.WriteLine($"Will send message for device {deviceId}:");
            Console.WriteLine(bodyJson);
            await deviceClient.SendEventAsync(message);

            Console.WriteLine($"Successfully sent message for device {deviceId}");

            await Task.Delay(10000);    // time between messages
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"* ERROR * {ex.Message}");
    }
}
#endregion

