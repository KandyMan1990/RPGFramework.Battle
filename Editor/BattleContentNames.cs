using System.Collections.Generic;
using RPGFramework.Battle.Databases;
using UnityEditor;
using UnityEngine;

namespace RPGFramework.Battle.Editor
{
    /// <summary>The names in every one of the project's battle databases, for pickers and checks.</summary>
    internal static class BattleContentNames
    {
        internal static List<string> Enemies()
        {
            List<string>          names     = new List<string>();
            BattleEnemyDatabase[] databases = Load<BattleEnemyDatabase>();

            for (int i = 0; i < databases.Length; i++)
            {
                for (int j = 0; j < databases[i].Enemies.Count; j++)
                {
                    Add(names, databases[i].Enemies[j]?.Name);
                }
            }

            return names;
        }

        internal static List<string> EnemyGroups()
        {
            List<string>               names     = new List<string>();
            BattleEnemyGroupDatabase[] databases = Load<BattleEnemyGroupDatabase>();

            for (int i = 0; i < databases.Length; i++)
            {
                for (int j = 0; j < databases[i].EnemyGroups.Count; j++)
                {
                    Add(names, databases[i].EnemyGroups[j]?.Name);
                }
            }

            return names;
        }

        internal static List<string> Encounters()
        {
            List<string>              names     = new List<string>();
            BattleEncounterDatabase[] databases = Load<BattleEncounterDatabase>();

            for (int i = 0; i < databases.Length; i++)
            {
                for (int j = 0; j < databases[i].Encounters.Count; j++)
                {
                    Add(names, databases[i].Encounters[j]?.Name);
                }
            }

            return names;
        }

        internal static List<string> Arenas()
        {
            List<string>                          names     = new List<string>();
            BattleArenaDatabaseScriptableObject[] databases = Load<BattleArenaDatabaseScriptableObject>();

            for (int i = 0; i < databases.Length; i++)
            {
                for (int j = 0; j < databases[i].m_Arenas.Length; j++)
                {
                    GameObject prefab = databases[i].m_Arenas[j].Prefab;

                    Add(names, prefab != null ? prefab.name : null);
                }
            }

            return names;
        }

        private static void Add(List<string> names, string name)
        {
            if (!string.IsNullOrEmpty(name) && !names.Contains(name))
            {
                names.Add(name);
            }
        }

        private static T[] Load<T>() where T : ScriptableObject
        {
            string[] guids  = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            T[]      loaded = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                loaded[i] = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[i]));
            }

            return loaded;
        }
    }
}
