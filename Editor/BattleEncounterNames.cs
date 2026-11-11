using System.Collections.Generic;
using RPGFramework.Battle.SharedTypes;

namespace RPGFramework.Battle.Editor
{
    internal sealed class BattleEncounterNames : IBattleEncounterNames
    {
        IReadOnlyList<string> IBattleEncounterNames.Names => BattleContentNames.Encounters();
    }
}
