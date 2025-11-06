using System.Text.Json.Serialization;

namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad;

/// <summary>
/// Location Information
/// </summary>
public class Location
{
    [JsonPropertyName("Addr")]
    public Addr Addr { get; set; } = new();

    [JsonPropertyName("SubLocation")]
    public List<SubLocation> SubLocation { get; set; } = new();

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("CrimeScore")]
    public string CrimeScore { get; set; } = string.Empty;
}

public class SubLocation
{
    [JsonPropertyName("Addr")]
    public Addr Addr { get; set; } = new();

    [JsonPropertyName("id")]
    public int Id { get; set; }
}

/// <summary>
/// Location Underwriting Information
/// </summary>
public class LocationUWInfo
{
    [JsonPropertyName("Construction")]
    public Construction Construction { get; set; } = new();

    [JsonPropertyName("BldgProtection")]
    public BldgProtection BldgProtection { get; set; } = new();

    [JsonPropertyName("BldgImprovements")]
    public BldgImprovements BldgImprovements { get; set; } = new();

    [JsonPropertyName("BldgOccupancy")]
    public BldgOccupancy BldgOccupancy { get; set; } = new();

    [JsonPropertyName("AlarmAndSecurity")]
    public List<string> AlarmAndSecurity { get; set; } = new();

    [JsonPropertyName("TerritoryCd")]
    public string TerritoryCd { get; set; } = string.Empty;

    [JsonPropertyName("DistanceToOceanOrOtherBodyWater")]
    public BldgArea? DistanceToOceanOrOtherBodyWater { get; set; }

    [JsonPropertyName("LocationRef")]
    public int LocationRef { get; set; }

    [JsonPropertyName("SubLocationRef")]
    public int SubLocationRef { get; set; }

    [JsonPropertyName("RoofType")]
    public string RoofType { get; set; } = string.Empty;

    [JsonPropertyName("SquareFootage")]
    public string SquareFootage { get; set; } = string.Empty;

    [JsonPropertyName("CoverageTotalInsuredValue")]
    public string CoverageTotalInsuredValue { get; set; } = string.Empty;

    [JsonPropertyName("TotalNoOfLoss")]
    public string TotalNoOfLoss { get; set; } = string.Empty;

    [JsonPropertyName("Exclusions")]
    public Exclusions Exclusions { get; set; } = new();
}

/// <summary>
/// Construction Details
/// </summary>
public class Construction
{
    /// <summary>
    /// Construction Code: F (Frame), JM (Joisted Masonry), MNC (Masonry Non-Combustible), etc.
    /// </summary>
    [JsonPropertyName("ConstructionCd")]
    public string ConstructionCd { get; set; } = string.Empty;

    [JsonPropertyName("YearBuilt")]
    public string YearBuilt { get; set; } = string.Empty;

    [JsonPropertyName("BldgArea")]
    public BldgArea BldgArea { get; set; } = new();

    [JsonPropertyName("NumStories")]
    public string NumStories { get; set; } = string.Empty;

    [JsonPropertyName("RoofingMaterial")]
    public RoofingMaterial RoofingMaterial { get; set; } = new();

    [JsonPropertyName("RoofGeometryTypeCd")]
    public string RoofGeometryTypeCd { get; set; } = string.Empty;

    [JsonPropertyName("CladdingTypeCd")]
    public string CladdingTypeCd { get; set; } = string.Empty;

    [JsonPropertyName("ConstructionQualityCd")]
    public string ConstructionQualityCd { get; set; } = string.Empty;

    [JsonPropertyName("FrameFoundationConnectionCd")]
    public string FrameFoundationConnectionCd { get; set; } = string.Empty;

    [JsonPropertyName("RoofWallAttachmentCd")]
    public string RoofWallAttachmentCd { get; set; } = string.Empty;

    [JsonPropertyName("RoofDeckMaterialCd")]
    public string RoofDeckMaterialCd { get; set; } = string.Empty;

    [JsonPropertyName("RoofEquipmentHurricaneCd")]
    public string RoofEquipmentHurricaneCd { get; set; } = string.Empty;

    [JsonPropertyName("WindOpeningProtectionCd")]
    public string WindOpeningProtectionCd { get; set; } = string.Empty;
}

public class BldgArea
{
    [JsonPropertyName("NumUnits")]
    public int NumUnits { get; set; }

    [JsonPropertyName("UnitMeasurementCd")]
    public string UnitMeasurementCd { get; set; } = string.Empty;

    [JsonPropertyName("RangeValue")]
    public string RangeValue { get; set; } = string.Empty;
}

public class RoofingMaterial
{
    [JsonPropertyName("RoofMaterialCd")]
    public string RoofMaterialCd { get; set; } = string.Empty;
}

/// <summary>
/// Building Protection (Sprinklers, Alarms, Fire Protection)
/// </summary>
public class BldgProtection
{
    [JsonPropertyName("DistanceToFireStation")]
    public string DistanceToFireStation { get; set; } = string.Empty;

    [JsonPropertyName("DistanceToHydrant")]
    public string DistanceToHydrant { get; set; } = string.Empty;

    [JsonPropertyName("ProtectionDeviceBurglarCd")]
    public string ProtectionDeviceBurglarCd { get; set; } = string.Empty;

    [JsonPropertyName("ProtectionDeviceFireCd")]
    public string ProtectionDeviceFireCd { get; set; } = string.Empty;

    /// <summary>
    /// Protection Class Grade: 1-10 (1 is best)
    /// </summary>
    [JsonPropertyName("ProtectionClassGradeCd")]
    public string ProtectionClassGradeCd { get; set; } = string.Empty;

    /// <summary>
    /// Sprinkler: YES, NO, PARTIAL
    /// </summary>
    [JsonPropertyName("ProtectionDeviceSprinklerCd")]
    public string ProtectionDeviceSprinklerCd { get; set; } = "NO";

    [JsonPropertyName("SprinkleredPct")]
    public string SprinkleredPct { get; set; } = string.Empty;

    [JsonPropertyName("ElectricalProtectionCd")]
    public string ElectricalProtectionCd { get; set; } = string.Empty;

    [JsonPropertyName("ProtectionDeviceMotionDetectorCd")]
    public string ProtectionDeviceMotionDetectorCd { get; set; } = string.Empty;

    [JsonPropertyName("ProtectionDeviceSmokeCd")]
    public string ProtectionDeviceSmokeCd { get; set; } = string.Empty;

    [JsonPropertyName("DoorLockCd")]
    public string DoorLockCd { get; set; } = string.Empty;

    [JsonPropertyName("ProtectionDeviceBurglarLevel")]
    public string ProtectionDeviceBurglarLevel { get; set; } = string.Empty;

    [JsonPropertyName("IsStormShutters")]
    public string IsStormShutters { get; set; } = "false";

    [JsonPropertyName("IsSmokeDetectors")]
    public string IsSmokeDetectors { get; set; } = "false";

    [JsonPropertyName("IsAnsulSystem")]
    public string IsAnsulSystem { get; set; } = "false";
}

/// <summary>
/// Building Improvements
/// </summary>
public class BldgImprovements
{
    [JsonPropertyName("HeatingImprovementYear")]
    public string HeatingImprovementYear { get; set; } = string.Empty;

    [JsonPropertyName("comIRH_PlumbingTypeCdExt")]
    public string ComIRH_PlumbingTypeCdExt { get; set; } = string.Empty;

    [JsonPropertyName("PlumbingImprovementYear")]
    public string PlumbingImprovementYear { get; set; } = string.Empty;

    [JsonPropertyName("RoofingImprovementYear")]
    public string RoofingImprovementYear { get; set; } = string.Empty;

    [JsonPropertyName("WiringImprovementYear")]
    public string WiringImprovementYear { get; set; } = string.Empty;

    [JsonPropertyName("comIRH_WiringYearExt")]
    public string ComIRH_WiringYearExt { get; set; } = string.Empty;

    [JsonPropertyName("comIRH_HeatingTypeCdExt")]
    public string ComIRH_HeatingTypeCdExt { get; set; } = string.Empty;

    [JsonPropertyName("comIRH_WiringTypeCdExt")]
    public string ComIRH_WiringTypeCdExt { get; set; } = string.Empty;

    [JsonPropertyName("comIRH_RoofingTypeCdExt")]
    public string ComIRH_RoofingTypeCdExt { get; set; } = string.Empty;
}

/// <summary>
/// Building Occupancy
/// </summary>
public class BldgOccupancy
{
    [JsonPropertyName("OccupancyTypeCd")]
    public string OccupancyTypeCd { get; set; } = string.Empty;

    [JsonPropertyName("OccupancyDesc")]
    public string OccupancyDesc { get; set; } = string.Empty;
}

/// <summary>
/// Property Exclusions
/// </summary>
public class Exclusions
{
    [JsonPropertyName("IsVandalismExclusion")]
    public string IsVandalismExclusion { get; set; } = "false";

    [JsonPropertyName("IsSprinklerLeakageExclusion")]
    public string IsSprinklerLeakageExclusion { get; set; } = "false";

    [JsonPropertyName("IsSinkholeExclusion")]
    public string IsSinkholeExclusion { get; set; } = "false";

    [JsonPropertyName("IsWaterDamageExclusion")]
    public string IsWaterDamageExclusion { get; set; } = "false";

    [JsonPropertyName("WaterDamageSublimitAmount")]
    public string WaterDamageSublimitAmount { get; set; } = string.Empty;

    [JsonPropertyName("RoofExclusion")]
    public string RoofExclusion { get; set; } = "false";
}
