namespace NecromundaDiceRoller.Model.RollStage;

public class SaveRollStage(Settings settings): IRollStage
{
    public int GetStageResult(int previousStageResult, List<int> rolls)
    {
        var result = 0;
        int totalSave = settings.TargetSettings.sv - settings.WeaponSettings.ap;
        for (var i = 0; i < previousStageResult; i++)
        {
            var currentRoll = rolls[i];
            if (currentRoll < totalSave)
                result++;
        }
        
        return result;
    }
}