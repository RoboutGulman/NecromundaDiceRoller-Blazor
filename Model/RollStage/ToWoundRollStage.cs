namespace NecromundaDiceRoller.Model.RollStage;

public class ToWoundRollStage(Settings settings): IRollStage
{
    private int RollToWound()
    {
        var str = settings.WeaponSettings.str;
        var t = settings.TargetSettings.t;
        if (str >= t * 2) return 2;
        if (str > t) return 3;
        if (str == t) return 4;
        if (str <= t / 2) return 6;
        return 5;
    }
    public int GetStageResult(int previousStageResult, int[] rolls, int offset)
    {
        var result = 0;
        var rollToWound = RollToWound();
        for (var i = 0; i < previousStageResult; i++)
        {
            var currentRoll = rolls[offset + i];
            if (currentRoll >= rollToWound)
                result++;
        }

        return result;
    }
}