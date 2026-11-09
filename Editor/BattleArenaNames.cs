using System.Collections.Generic;
using RPGFramework.Battle.SharedTypes;
using UnityEditor;
using UnityEngine;

namespace RPGFramework.Battle.Editor
{
    internal sealed class BattleArenaNames : IBattleArenaNames
    {
        IReadOnlyList<string> IBattleArenaNames.Names
        {
            get
            {
                List<string> names = new List<string>();
                string[]     guids = AssetDatabase.FindAssets("t:" + nameof(BattleArenaDatabaseScriptableObject));

                for (int i = 0; i < guids.Length; i++)
                {
                    BattleArenaDatabaseScriptableObject database = AssetDatabase.LoadAssetAtPath<BattleArenaDatabaseScriptableObject>(AssetDatabase.GUIDToAssetPath(guids[i]));

                    for (int j = 0; j < database.m_Arenas.Length; j++)
                    {
                        GameObject prefab = database.m_Arenas[j].Prefab;

                        if (prefab != null && !names.Contains(prefab.name))
                        {
                            names.Add(prefab.name);
                        }
                    }
                }

                return names;
            }
        }
    }
}
