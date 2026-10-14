using System.Collections.Generic;
using RPGFramework.Core.Memory;
using RPGFramework.Core.SharedTypes;

namespace RPGFramework.Battle
{
    /// <summary>
    /// The variables the battle module requires of every game's map.
    /// </summary>
    internal sealed class BattleVariables
#if UNITY_EDITOR
        : IRequiredVariables
#endif
    {
        /// <summary>
        /// The battle mode, as its <c>BattleFlags</c>: the flags every random battle is fought with, and whether losing
        /// a battle or a countdown running out ends the game. Its default is a new game's.
        /// </summary>
        public const string BATTLE_MODE = "BattleMode";

#if UNITY_EDITOR
        private static readonly RequiredVariable[] m_Variables =
        {
            new RequiredVariable(BATTLE_MODE, MemoryBank.Persistent, VariableWidth.UShort, "The flags every random battle is fought with, and whether losing a battle or a countdown running out ends the game. Its default is a new game's")
        };

        IReadOnlyList<RequiredVariable> IRequiredVariables.Variables => m_Variables;
#endif
    }
}