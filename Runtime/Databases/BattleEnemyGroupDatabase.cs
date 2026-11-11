using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFramework.Battle.Databases
{
    /// <summary>Every group of enemies the game's encounters can fight, by name.</summary>
    [CreateAssetMenu(menuName = "RPG Framework/Battle/Enemy Group Database", fileName = "Enemy Group Database")]
    internal sealed class BattleEnemyGroupDatabase : ScriptableObject
    {
        [SerializeField]
        private BattleEnemyGroupDefinition[] m_EnemyGroups = Array.Empty<BattleEnemyGroupDefinition>();

#if UNITY_EDITOR
        internal IReadOnlyList<BattleEnemyGroupDefinition> EnemyGroups => m_EnemyGroups;
#endif
    }
}
