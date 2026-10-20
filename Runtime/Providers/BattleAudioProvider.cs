using RPGFramework.Audio.Music;
using RPGFramework.Hashing;
using UnityEngine;

namespace RPGFramework.Battle.Providers
{
    /// <summary>
    /// The music a battle plays itself: its victory fanfare. The battle music is Core's stored setting.
    /// </summary>
    public interface IBattleAudioProvider
    {
        /// <summary>The victory fanfare's music asset name hash.</summary>
        ulong VictoryMusicId { get; }
    }

    [CreateAssetMenu(menuName = "RPG Framework/Audio/Battle Audio Provider", fileName = "Battle Audio Provider")]
    public sealed class BattleAudioProvider : ScriptableObject, IBattleAudioProvider
    {
        [SerializeField]
        private MusicAsset m_VictoryMusicName;

        ulong IBattleAudioProvider.VictoryMusicId => Fnv1a64.Hash(m_VictoryMusicName.name);
    }
}