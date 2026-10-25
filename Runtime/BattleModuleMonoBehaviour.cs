using RPGFramework.Battle.Loaders;
using RPGFramework.Battle.SharedTypes;
using RPGFramework.Core;
using RPGFramework.DI;
using UnityEngine;

namespace RPGFramework.Battle
{
    internal sealed class BattleModuleMonoBehaviour : MonoBehaviour, IModuleBindings
    {
        void IModuleBindings.Bind(IDIContainer sceneContainer)
        {
            sceneContainer.BindSingletonIfNotRegistered<IBattleArenaLoader, BattleArenaLoader>();
            sceneContainer.BindSingletonIfNotRegistered<IBattleArenaPresentation, BattleArenaPresentation>();
            sceneContainer.BindSingletonIfNotRegistered<IBattleModule, BattleModule>();
        }
    }
}