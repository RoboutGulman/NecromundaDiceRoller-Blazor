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
    
    public static List<int> CovertInto6Base(int base10Number, int length)
    {
        var result = new List<int>();
        if (base10Number == 0)
        {
            for (int i = 0; i < length; i++)
            {
                result.Add(6);
            }
            return result;
        }
        
        while (base10Number != 0)
        {
            var converted = base10Number % 6;
            result.Add(converted != 0 ? converted : 6);
            base10Number /= 6;
        }

        for (int i = result.Count; i < length; i++)
        {
            result.Add(6);
        }
        result.Reverse();
        return result;
    }

    public SortedDictionary<int, int> GetStatistics()
    {
        var result = new SortedDictionary<int, int>();

        var diceNumber = GetNumberOfRollsOnStage();

        for (int i = 0; i < diceNumber; i++)
        {
            result[i] = 0;
        }
        
        for (int seed = 0; seed < Math.Pow(6, diceNumber); seed++)
        {
            var currentRoll = CovertInto6Base(seed, diceNumber);

            var stageResult = _firstRollStage.GetStageResult([currentRoll[0]]);
            for (int i = 0; i < _rollStages.Count; i++)
            {
                var currentStagesRolls = _settings.WeaponSettings.isRapid
                    ? currentRoll.GetRange(i * 3 + 1, 3)
                    : currentRoll.GetRange(i + 1, 1);

                stageResult = _rollStages[i].GetStageResult(stageResult, currentStagesRolls);
            }

            if (!result.TryAdd(stageResult, 1))
            {
                result[stageResult]++;
            }
        }

        return result;
    }
}