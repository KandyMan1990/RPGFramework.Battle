using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFramework.Battle.Databases
{
    /// <summary>
    /// Every enemy the game's enemy groups can hold, by name. Only the names for now: what an enemy is, the battle module
    /// gives it once it fights.
    /// </summary>
    [CreateAssetMenu(menuName = "RPG Framework/Battle/Enemy Database", fileName = "Enemy Database")]
    internal sealed class BattleEnemyDatabase : ScriptableObject
    {
        [SerializeField]
        private BattleEnemyDefinition[] m_Enemies = Array.Empty<BattleEnemyDefinition>();

#if UNITY_EDITOR
        internal IReadOnlyList<BattleEnemyDefinition> Enemies => m_Enemies;
#endif
    }
}
