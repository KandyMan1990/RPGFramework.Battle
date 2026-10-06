using RPGFramework.Battle.SharedTypes;
using RPGFramework.Battle.SharedTypes.Stores;

namespace RPGFramework.Battle.Stores
{
    public sealed class BattleCompleteStateStore : IBattleCompleteStateStore
    {
        private BattleCompleteState m_State;

        BattleCompleteState IBattleCompleteStateStore.State => m_State;

        void IBattleCompleteStateStore.Set(BattleCompleteState state)
        {
            m_State = state;
        }
    }
}
