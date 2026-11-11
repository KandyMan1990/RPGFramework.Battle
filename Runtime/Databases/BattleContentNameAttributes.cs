using System.Diagnostics;
using UnityEngine;

namespace RPGFramework.Battle.Databases
{
    /// <summary>A string naming an enemy, picked in the inspector from the project's enemy databases. Left out of players.</summary>
    [Conditional("UNITY_EDITOR")]
    internal sealed class EnemyNameAttribute : PropertyAttribute
    {
    }

    /// <summary>A string naming an enemy group, picked from the project's enemy group databases. Left out of players.</summary>
    [Conditional("UNITY_EDITOR")]
    internal sealed class EnemyGroupNameAttribute : PropertyAttribute
    {
    }
}
