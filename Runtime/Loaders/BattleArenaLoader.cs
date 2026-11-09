using System.Threading.Tasks;
using RPGFramework.Battle.Databases;
using UnityEngine;

namespace RPGFramework.Battle.Loaders
{
    /// <summary>Loads a battle's arena by the hash of its name, looking it up in the arena database.</summary>
    public interface IBattleArenaLoader
    {
        /// <summary>Loads the arena named by <paramref name="nameHash" /> into the scene, returning its root.</summary>
        Task<GameObject> LoadAsync(ulong nameHash);

        /// <summary>Takes the arena loaded last out of the scene.</summary>
        Task UnloadAsync();
    }

    internal sealed class BattleArenaLoader : IBattleArenaLoader
    {
        private readonly IBattleArenaDatabase     m_ArenaDatabase;
        private readonly IBattleArenaPresentation m_ArenaPresentation;

        internal BattleArenaLoader(IBattleArenaDatabase     arenaDatabase,
                                   IBattleArenaPresentation arenaPresentation)
        {
            m_ArenaDatabase     = arenaDatabase;
            m_ArenaPresentation = arenaPresentation;
        }

        Task<GameObject> IBattleArenaLoader.LoadAsync(ulong nameHash)
        {
            BattleArenaDefinition arenaDefinition = m_ArenaDatabase.Get(nameHash);
            return m_ArenaPresentation.LoadAsync(arenaDefinition);
        }

        Task IBattleArenaLoader.UnloadAsync()
        {
            return m_ArenaPresentation.UnloadAsync();
        }
    }
}