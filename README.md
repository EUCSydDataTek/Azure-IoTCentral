# AzureIoTCentralDemo

Credit til Jurgen Kevelaers, hans Pluralsight kursus og [eksempel](https://github.com/JurgenOnAzure/microsoft-azure-iot-developer-manage-iot-devices-azure-iot-central).

# iot-central-m1
G� til [Azure IoT Central](https://apps.azureiotcentral.com/home)

### Template
Opret ny Device template: **SimpleDemo**, med *Custom model*

V�lg *Add capability* og opret disse to:

- Temperature, Temperature, Telemetry, Temperature (Unit: Degree Celcius)
- Humidity, Humidity, Telemetry, Humidity (Unit: Percent)
Under Views: V�lg Visualizing the device og fanen Start with devices.

Under *Telemetry* tilf�jes *Temperature*. V�lg *Add tile.*
Under *Edit* findes instillinger som *Display Range* og *Interval* samt *Aggregering*. Hvilken betydning har disse indstillinger for hvor ofte dashboard opdateres og med hvilke v�rdier?

Under *Telemetry* tilf�jes *Humidity*. V�lg *Add tile*.

V�lg *Save*, *Back*, *Publish* og *Publish*.

### Device
Under *Devices* v�lges *New* og *Device name/ID* kaldes **DemoDevice**. V�lg *Create*.

V�lg *Connect* og find *ID scope, Device ID* og *Primary key*. Disse 3 v�rdier skal benyttes i n�ste punkt.

### Eksekver kode
Afpr�v `iot-central-m1 programmet` i Visual Studio.

&nbsp;

# iot-central-m2

Opret ny Device template med navnet **RoomAtmosphere**.

Import interface.json
Publish


### Device

Add a device med navnet **room-device-01**.

### View

G� tilbage til Device template og v�lg *Views*.

**PROPERTIES**
Form name: Device properties
V�lg Device templates | RoomAtmosphere | Views | Editing device and cloud data | Select Building ID, Room number and Target temperature

**TELEMETRY**
V�lg nu: Visualizing the device. Name: Atmosphere
V�lg fanen: Start with devices
Telemetry | Current temperature | Add Tile
Telemetry | Current humidity | Add Tile
Telemetry | Climate control state | Add Tile | �ndre visning til State Chart

Save | Back | Publish

G� til Devices | RoomAtmosphere | room-device-01


### Programming the Device

Eksekver iot-developer-iot-central-m2 programmet i Visual Studio og kig i terminalen.

###Testing

Under Device properties kan Building ID og Room number nu afl�ses.

Default temperatur = 68.2 F. S�t Target Temperatur til 70 F. Begynder at varme. S�t den nu til 65 F. Begynder at k�le.

Under Command k�es en Cleaning mode med mode = "basic". Klik Save.

G� ind under Raw data og fold forskellige v�rdier ud. Der kan sorteres og s�ges p� dem.

G� ud p� Dashboard og maksimer Current temperature. Bagefter ClimateControlState.

Under punktet *Data explorer* v�lges New query og v�lg *Device group = RoomAtmosphere* og *Current temperature* og *humidity*.
Marker for at zoome.