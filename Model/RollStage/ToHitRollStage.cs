namespace NecromundaDiceRoller.Model.RollStage;

public class ToHitRollStage(Settings settings): IFirstRollStage
{
    public int GetStageResult(List<int> rolls)
    {
        if (!settings.WeaponSettings.isRapid)
            return 1;
        var rapidFireRoll = rolls.First();
        if (rapidFireRoll == 6)
            return 3;
        if (rapidFireRoll > 3)
            return 2;
        return 1;
    }
}