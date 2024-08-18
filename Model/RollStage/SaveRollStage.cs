namespace NecromundaDiceRoller.Model.RollStage;

public class SaveRollStage(Settings settings): IRollStage
{
    public int GetStageResult(int previousStageResult, int[]rolls, int offset)
    {
        var result = 0;
        int totalSave = settings.TargetSettings.sv - settings.WeaponSettings.ap;
        for (var i = 0; i < previousStageResult; i++)
        {
            var currentRoll = rolls[offset + i];
            if (currentRoll < totalSave)
                result++;
        }
        
        return result;
    }
}