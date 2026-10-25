using UnityEditor;
using UnityEngine;

namespace RPGFramework.Battle.Editor
{
    internal static class BattleModuleCreator
    {
        [MenuItem("GameObject/RPG Framework/Battle/Battle Module", false, 10)]
        private static void Create(MenuCommand command)
        {
            GameObject module = new GameObject("Battle Module", typeof(BattleModuleMonoBehaviour));

            GameObjectUtility.SetParentAndAlign(module, command.context as GameObject);
            Undo.RegisterCreatedObjectUndo(module, "Create Battle Module");
            Selection.activeObject = module;
        }
    }
}
