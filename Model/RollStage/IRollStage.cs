namespace NecromundaDiceRoller.Model.RollStage;

public interface IRollStage
{
    public int GetStageResult(int previousStageResult, List<int> rolls);
}