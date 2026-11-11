using System;
using UnityEngine;

namespace RPGFramework.Battle.Databases
{
    /// <summary>One enemy, by name.</summary>
    [Serializable]
    internal sealed class BattleEnemyDefinition
    {
        [SerializeField]
        private string m_Name;

#if UNITY_EDITOR
        internal string Name => m_Name;
#endif
    }
}
