using Microsoft.AspNetCore.Components;
using NecromundaDiceRoller.Model;
using NecromundaDiceRoller.Model.DiceRoller;
using NecromundaDiceRoller.Model.RollStage;

namespace NecromundaDiceRoller
{
    public class DiceRollerComponent : ComponentBase
    {
        public Settings Settings = new Settings()
        {
            WeaponSettings = new WeaponSettings()
            {
                ap = 0,
                isPower = false,
                isRapid = true,
                isShock = false,
                isShred = false,
                str = 4
            },
            TargetSettings = new TargetSettings()
            {
                sv = 7,
                t = 3
            }
        };

        public class Result
        {
             public int NumberOfSuccesses { get; set; }
             public double Calculated { get; set; }
        }

        public List<Result>? Results = null;

        public void Calculate()
        {
            var toHit = new ToHitRollStage(Settings);
            var diceRoller = new DiceRoller(Settings, toHit, []);

            var dict = diceRoller.GetStatistics();

            Results = new List<Result>();

            var sum = 0;
            foreach (var pair in dict)
                sum += pair.Value;

            foreach (var pair in dict)
            {
                Results.Add(new Result()
                {
                    NumberOfSuccesses = pair.Key,
                    Calculated = (double)pair.Value / sum
                });
            }
        }
    }
}

