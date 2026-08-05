using System.Threading.Tasks;
using RPGFramework.Battle.Databases;
using UnityEngine;

namespace RPGFramework.Battle.Loaders
{
    public interface IBattleArenaLoader
    {
        Task<GameObject> LoadAsync(int arenaId);
        Task             UnloadAsync();
    }

    public sealed class BattleArenaLoader : IBattleArenaLoader
    {
        private readonly IBattleArenaDatabase     m_ArenaDatabase;
        private readonly IBattleArenaPresentation m_ArenaPresentation;

        internal BattleArenaLoader(IBattleArenaDatabase     arenaDatabase,
                                   IBattleArenaPresentation arenaPresentation)
        {
            m_ArenaDatabase     = arenaDatabase;
            m_ArenaPresentation = arenaPresentation;
        }

        Task<GameObject> IBattleArenaLoader.LoadAsync(int arenaId)
        {
            BattleArenaDefinition arenaDefinition = m_ArenaDatabase.Get(arenaId);
            return m_ArenaPresentation.LoadAsync(arenaDefinition);
        }

        Task IBattleArenaLoader.UnloadAsync()
        {
            return m_ArenaPresentation.UnloadAsync();
        }
    }
}