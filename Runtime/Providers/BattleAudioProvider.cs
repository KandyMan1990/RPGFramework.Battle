using RPGFramework.Audio.Music;
using RPGFramework.Hashing;
using UnityEngine;

namespace RPGFramework.Battle.Providers
{
    public interface IBattleAudioProvider
    {
        ulong GetVictoryMusicId { get; }
    }

    [CreateAssetMenu(menuName = "RPG Framework/Audio/Battle Audio Provider", fileName = "Battle Audio Provider")]
    public class BattleAudioProvider : ScriptableObject, IBattleAudioProvider
    {
        [SerializeField]
        private MusicAsset m_VictoryMusicName;

        ulong IBattleAudioProvider.GetVictoryMusicId => Fnv1a64.Hash(m_VictoryMusicName.name);
    }
}