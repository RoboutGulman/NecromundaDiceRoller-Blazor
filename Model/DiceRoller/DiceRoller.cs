using NecromundaDiceRoller.Model.RollStage;

namespace NecromundaDiceRoller.Model.DiceRoller;

public class DiceRoller
{
    private IFirstRollStage _firstRollStage;
    private List<IRollStage> _rollStages;
    private Settings _settings;

    public DiceRoller(Settings settings, IFirstRollStage firstRollStage, List<IRollStage> rollStages)
    {
        _firstRollStage = firstRollStage;
        _rollStages = rollStages;
        _settings = settings;
    }

    private int GetNumberOfRollsOnStage()
    {
        if (_settings.WeaponSettings.isRapid)
        {
            return 1 + _rollStages.Count * 3;
        }

        return _rollStages.Count + 1;
    }
    
    public static int[] CovertInto6Base(int base10Number, int length)
    {
        int[] result = new int[length];
        if (base10Number == 0)
        {
            for (int i = 0; i < length; i++)
            {
                result[i] = 6;
            }
            return result;
        }

        var y = 1;
        while (base10Number != 0)
        {
            var converted = base10Number % 6;
            result[^y] = converted != 0 ? converted : 6;
            y++;
            base10Number /= 6;
        }

        while (y <= length)
        {
            result[^y] = 6;
            y++;
        }

        return result;
    }

    public SortedDictionary<int, int> GetStatistics()
    {
        var result = new SortedDictionary<int, int>();

        var diceNumber = GetNumberOfRollsOnStage();

        var isRapid = _settings.WeaponSettings.isRapid;

        for (int i = 0; i <=  (isRapid ? 3 : 1); i++)
        {
            result[i] = 0;
        }
        
        for (int seed = 0; seed < Math.Pow(6, diceNumber); seed++)
        {
            var currentRoll = CovertInto6Base(seed, diceNumber);
            
            var stageResult = _firstRollStage.GetStageResult(currentRoll);
            for (int i = 0; i < _rollStages.Count; i++)
            {
                var offset = isRapid
                    ? i * 3 + 1
                    : i + 1;

                stageResult = _rollStages[i].GetStageResult(stageResult, currentRoll, offset);
            }

            result[stageResult]++;
        }

        return result;
    }
}