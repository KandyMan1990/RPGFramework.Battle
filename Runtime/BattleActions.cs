#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using RPGFramework.Core.Input;

namespace RPGFramework.Battle
{
    /// <summary>The actions the battle reads, declared for the editor's check of the battle scene's map.</summary>
    internal sealed class BattleActions : IRequiredActions
    {
        private static readonly RequiredAction[] m_Required =
        {
            new RequiredAction(CoreActions.CANCEL, RequiredActionKind.Button, "loses the placeholder battle")
        };

        Type IRequiredActions.Module => typeof(BattleModuleMonoBehaviour);

        string IRequiredActions.Map => null;

        bool IRequiredActions.Moves => false;

        IReadOnlyList<RequiredAction> IRequiredActions.Actions => m_Required;
    }
}
#endif
