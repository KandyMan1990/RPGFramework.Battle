using System.Collections.Generic;
using RPGFramework.Battle.SharedTypes;

namespace RPGFramework.Battle.Editor
{
    internal sealed class BattleArenaNames : IBattleArenaNames
    {
        IReadOnlyList<string> IBattleArenaNames.Names => BattleContentNames.Arenas();
    }
}
