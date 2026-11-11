using System;
using RPGFramework.Battle.SharedTypes;
using RPGFramework.Hashing;
using UnityEngine;

namespace RPGFramework.Battle.Databases
{
    /// <summary>
    /// One battle a script or a field's table can start, by name: the enemy group fought, its own flags and how its enemies
    /// are levelled. Where it is fought is the caller's, so the same encounter can be fought anywhere.
    /// </summary>
    [Serializable]
    public sealed class BattleEncounterDefinition
    {
        [SerializeField]
        private string m_Name;

        [SerializeField]
        [EnemyGroupName]
        private string m_EnemyGroup;

        [SerializeField]
        [Tooltip("This encounter's own flags, joined to those of the call that starts it. Using the current music is the call's, as the router reads it before the battle loads, and the game-over rules are the battle mode's")]
        private BattleFlags m_Flags;

        [SerializeField]
        private EncounterLevel m_Level;

        internal string        Name       => m_Name;
        internal ulong         EnemyGroup => Fnv1a64.Hash(m_EnemyGroup);
        internal BattleFlags   Flags      => m_Flags;
        internal EncounterLevel Level     => m_Level;

#if UNITY_EDITOR
        /// <summary>Keeps a battle's own flags only, and a level rule that can be met.</summary>
        internal void Validate()
        {
            m_Flags &= ~(BattleFlags.USE_CURRENT_MUSIC | BattleFlags.DISABLE_GAME_OVER_ON_TIME_OUT | BattleFlags.DISABLE_GAME_OVER_ON_DEFEAT);

            m_Level.Validate();
        }
#endif
    }
}
