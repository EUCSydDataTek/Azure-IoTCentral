# AzureIoTCentralDemo

Credit til Jurgen Kevelaers, hans Pluralsight kursus og [eksempel](https://github.com/JurgenOnAzure/microsoft-azure-iot-developer-manage-iot-devices-azure-iot-central).

# iot-central-m1
Gå til [Azure IoT Central](https://apps.azureiotcentral.com/home)

### Template
Opret ny Device template: **SimpleDemo**, med *Custom model*

Vælg *Add capability* og opret disse to:

- Temperature, Temperature, Telemetry, Temperature (Unit: Degree Celcius)
- Humidity, Humidity, Telemetry, Humidity (Unit: Percent)
Under Views: Vælg Visualizing the device og fanen Start with devices.

Under *Telemetry* tilføjes *Temperature*. Vælg *Add tile.*
Under *Edit* findes instillinger som *Display Range* og *Interval* samt *Aggregering*. Hvilken betydning har disse indstillinger for hvor ofte dashboard opdateres og med hvilke værdier?

Under *Telemetry* tilføjes *Humidity*. Vælg *Add tile*.

Vælg *Save*, *Back*, *Publish* og *Publish*.

### Device
Under *Devices* vælges *New* og *Device name/ID* kaldes **DemoDevice**. Vælg *Create*.

Vælg *Connect* og find *ID scope, Device ID* og *Primary key*. Disse 3 værdier skal benyttes i næste punkt.

### Eksekver kode
Afprøv `iot-central-m1 programmet` i Visual Studio.

&nbsp;

# iot-central-m2

Opret ny Device template med navnet **RoomAtmosphere**.

Import interface.json
Publish


### Device

Add a device med navnet **room-device-01**.

### View

Gå tilbage til Device template og vælg *Views*.

**PROPERTIES**
Form name: Device properties
Vælg Device templates | RoomAtmosphere | Views | Editing device and cloud data | Select Building ID, Room number and Target temperature

**TELEMETRY**
Vælg nu: Visualizing the device. Name: Atmosphere
Vælg fanen: Start with devices
Telemetry | Current temperature | Add Tile
Telemetry | Current humidity | Add Tile
Telemetry | Climate control state | Add Tile | ændre visning til State Chart

Save | Back | Publish

Gå til Devices | RoomAtmosphere | room-device-01


### Programming the Device

Eksekver iot-developer-iot-central-m2 programmet i Visual Studio og kig i terminalen.

###Testing

Under Device properties kan Building ID og Room number nu aflæses.

Default temperatur = 68.2 F. Sæt Target Temperatur til 70 F. Begynder at varme. Sæt den nu til 65 F. Begynder at køle.

Under Command køes en Cleaning mode med mode = "basic". Klik Save.

Gå ind under Raw data og fold forskellige værdier ud. Der kan sorteres og søges på dem.

Gå ud på Dashboard og maksimer Current temperature. Bagefter ClimateControlState.

Under punktet *Data explorer* vælges New query og vælg *Device group = RoomAtmosphere* og *Current temperature* og *humidity*.
Marker for at zoome.