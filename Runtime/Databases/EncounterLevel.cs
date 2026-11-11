using System;
using UnityEngine;

namespace RPGFramework.Battle.Databases
{
    /// <summary>
    /// How an encounter's enemies are levelled: at a level, at one between two, at the party's, or at the party's give or
    /// take a few, the party's being its average or its leader's. Kept until the battle module gives enemies levels.
    /// </summary>
    [Serializable]
    internal struct EncounterLevel
    {
        [SerializeField]
        private EncounterLevelRule m_Rule;

        [SerializeField]
        [Tooltip("Which of the party's levels the party's rules follow")]
        private EncounterPartyLevel m_PartyLevel;

        [SerializeField]
        [Min(0)]
        [Tooltip("The level; the lowest, between two; how far below the party's it can be, around the party's")]
        private int m_Low;

        [SerializeField]
        [Min(0)]
        [Tooltip("The highest, between two; how far above the party's it can be, around the party's")]
        private int m_High;

        internal EncounterLevelRule  Rule       => m_Rule;
        internal EncounterPartyLevel PartyLevel => m_PartyLevel;
        internal int                 Low        => m_Low;
        internal int                 High       => m_High;

#if UNITY_EDITOR
        internal void Validate()
        {
            m_Low  = Math.Max(m_Low, 0);
            m_High = Math.Max(m_High, m_Rule == EncounterLevelRule.Between ? m_Low : 0);
        }
#endif
    }

    internal enum EncounterLevelRule
    {
        /// <summary>Every enemy at the low level.</summary>
        Fixed,

        /// <summary>Each enemy at a level from the low to the high.</summary>
        Between,

        /// <summary>The party's level.</summary>
        PartyLevel,

        /// <summary>The party's level, from the low below it to the high above it.</summary>
        PartyLevelBetween
    }

    internal enum EncounterPartyLevel
    {
        /// <summary>The party's average level.</summary>
        Average,

        /// <summary>The level of the party's leader.</summary>
        Leader
    }
}
