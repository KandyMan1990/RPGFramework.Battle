using UnityEngine;

namespace RPGFramework.Battle.Providers
{
    public interface IBattleAudioProvider
    {
        int GetVictoryMusicId { get; }
    }

    [CreateAssetMenu(menuName = "RPG Framework/Audio/Battle Audio Provider", fileName = "Battle Audio Provider")]
    public class BattleAudioProvider : ScriptableObject, IBattleAudioProvider
    {
        [SerializeField]
        private int m_VictoryMusicId;

        int IBattleAudioProvider.GetVictoryMusicId => m_VictoryMusicId;
    }
}