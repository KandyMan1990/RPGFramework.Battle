using System;
using UnityEngine;

namespace RPGFramework.Battle.Databases
{
    /// <summary>A group of enemies fought together, by name, holding its enemies by theirs.</summary>
    [Serializable]
    internal sealed class BattleEnemyGroupDefinition
    {
        [SerializeField]
        private string m_Name;

        [SerializeField]
        [EnemyName]
        private string[] m_Enemies = Array.Empty<string>();

#if UNITY_EDITOR
        internal string Name => m_Name;
#endif
    }
}
