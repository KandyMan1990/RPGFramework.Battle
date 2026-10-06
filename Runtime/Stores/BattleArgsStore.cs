using RPGFramework.Battle.SharedTypes;
using RPGFramework.Battle.SharedTypes.Stores;

namespace RPGFramework.Battle.Stores
{
    public sealed class BattleArgsStore : IBattleArgsStore
    {
        private BattleArgs m_Args;

        BattleArgs IBattleArgsStore.Args => m_Args;

        void IBattleArgsStore.Set(BattleArgs args) => m_Args = args;
    }
}
