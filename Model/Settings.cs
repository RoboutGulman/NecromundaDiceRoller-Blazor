namespace NecromundaDiceRoller.Model;

public record Settings()
{
    public required WeaponSettings WeaponSettings { get; set; }
    public required TargetSettings TargetSettings { get; set; }
}

public record WeaponSettings
{
    public int str { get; set; }
    public int ap { get; set; }
    public bool isRapid { get; set; }
    public bool isShock { get; set; }
    public bool isShred { get; set; }
    public bool isPower { get; set; }
}

public record TargetSettings
{
    public int t { get; set; }
    public int sv { get; set; }
}