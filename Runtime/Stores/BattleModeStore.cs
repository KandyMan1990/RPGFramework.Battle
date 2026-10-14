using RPGFramework.Battle.SharedTypes;
using RPGFramework.Battle.SharedTypes.Stores;
using RPGFramework.Core;
using RPGFramework.Core.Memory;
using RPGFramework.Core.SharedTypes;

namespace RPGFramework.Battle.Stores
{
    /// <summary>
    /// Keeps the battle mode in <see cref="BattleVariables.BATTLE_MODE" />, so it is saved with persistent memory and a
    /// new game begins at its default.
    /// </summary>
    public sealed class BattleModeStore : IBattleModeStore
    {
        private readonly IMemoryService m_MemoryService;
        private readonly ushort         m_Address;

        internal BattleModeStore(IMemoryService memoryService, IVariableMap variableMap)
        {
            variableMap.TryGetVariable(BattleVariables.BATTLE_MODE, out VariableDefinition mode);

            m_MemoryService = memoryService;
            m_Address       = (ushort)mode.Offset;
        }

        BattleFlags IBattleModeStore.Mode
        {
            get
            {
                BattleFlags mode = (BattleFlags)m_MemoryService.ReadUshort(MemoryBank.Persistent, m_Address);

                return mode;
            }
        }

        void IBattleModeStore.SetMode(BattleFlags mode)
        {
            m_MemoryService.WriteUshort(MemoryBank.Persistent, m_Address, (ushort)mode);
        }
    }
}