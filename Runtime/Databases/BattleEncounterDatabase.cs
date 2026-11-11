using System;
using System.Collections.Generic;
using RPGFramework.Hashing;
using UnityEngine;
#if ENABLE_RPG_FRAMEWORK_DEBUG
using RPGFramework.Battle.SharedTypes;
#endif

namespace RPGFramework.Battle.Databases
{
    /// <summary>Every encounter the game's battles start, by the hash of its name, which <c>BattleArgs.Encounter</c> holds.</summary>
    public interface IBattleEncounterDatabase
    {
        /// <summary>The encounter named by <paramref name="nameHash" />.</summary>
        BattleEncounterDefinition Get(ulong nameHash);
    }

    [CreateAssetMenu(menuName = "RPG Framework/Battle/Encounter Database", fileName = "Encounter Database")]
    public sealed class BattleEncounterDatabase : ScriptableObject, IBattleEncounterDatabase
#if ENABLE_RPG_FRAMEWORK_DEBUG
        , IDebugBattleEncounterDatabase
#endif
    {
        [SerializeField]
        private BattleEncounterDefinition[] m_Encounters = Array.Empty<BattleEncounterDefinition>();

        private Dictionary<ulong, BattleEncounterDefinition> m_ByNameHash;

#if UNITY_EDITOR
        internal IReadOnlyList<BattleEncounterDefinition> Encounters => m_Encounters;

        // As it is edited; a player only ever loads it.
        private void OnValidate()
        {
            for (int i = 0; i < m_Encounters.Length; i++)
            {
                m_Encounters[i]?.Validate();
            }

            BuildLookup();
        }
#endif

        private void OnEnable()
        {
            BuildLookup();
        }

        BattleEncounterDefinition IBattleEncounterDatabase.Get(ulong nameHash)
        {
            return m_ByNameHash[nameHash];
        }

#if ENABLE_RPG_FRAMEWORK_DEBUG
        IReadOnlyList<string> IDebugBattleEncounterDatabase.EncounterNames
        {
            get
            {
                string[] names = new string[m_Encounters.Length];

                for (int i = 0; i < m_Encounters.Length; i++)
                {
                    names[i] = m_Encounters[i].Name;
                }

                return names;
            }
        }
#endif

        private void BuildLookup()
        {
            m_ByNameHash = new Dictionary<ulong, BattleEncounterDefinition>(m_Encounters.Length);

            for (int i = 0; i < m_Encounters.Length; i++)
            {
                if (m_Encounters[i] != null)
                {
                    m_ByNameHash[Fnv1a64.Hash(m_Encounters[i].Name)] = m_Encounters[i];
                }
            }
        }
    }
}
