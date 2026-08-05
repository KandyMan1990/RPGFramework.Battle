using System.Threading.Tasks;
using RPGFramework.Audio;
using RPGFramework.Battle.Loaders;
using RPGFramework.Battle.Providers;
using RPGFramework.Battle.SharedTypes;
using RPGFramework.Battle.SharedTypes.Providers;
using RPGFramework.Core;
using RPGFramework.Core.Input;
using RPGFramework.Core.Rendering;
using RPGFramework.Core.SharedTypes;
using RPGFramework.DI;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPGFramework.Battle
{
    public class BattleModule : IBattleModule
    {
        private readonly ICoreModule                  m_CoreModule;
        private readonly IDIResolver                  m_DIResolver;
        private readonly IScreenFadeService           m_ScreenFadeService;
        private readonly IBattleArgsProvider          m_BattleArgsProvider;
        private readonly IBattleCompleteStateProvider m_BattleCompleteStateProvider;
        private readonly IBattleModule                m_BattleModule;
        private readonly VisualElement                m_UIContainer;
        private readonly IMusicPlayer                 m_MusicPlayer;
        private readonly ISfxPlayer                   m_SfxPlayer;
        private readonly IBattleAudioProvider         m_AudioProvider;
        private readonly IBattleArenaLoader           m_ArenaLoader;

        private InputAdapter m_InputAdapter;
        private BattleArgs   m_BattleArgs;

        public BattleModule(ICoreModule                  coreModule,
                            IDIResolver                  diResolver,
                            IScreenFadeService           screenFadeService,
                            IBattleArgsProvider          battleArgsProvider,
                            IBattleCompleteStateProvider battleCompleteStateProvider,
                            IMusicPlayer                 musicPlayer,
                            ISfxPlayer                   sfxPlayer,
                            IBattleAudioProvider         battleAudioProvider,
                            IBattleArenaLoader           arenaLoader)
        {
            m_CoreModule                  = coreModule;
            m_DIResolver                  = diResolver;
            m_ScreenFadeService           = screenFadeService;
            m_BattleArgsProvider          = battleArgsProvider;
            m_BattleCompleteStateProvider = battleCompleteStateProvider;
            m_MusicPlayer                 = musicPlayer;
            m_SfxPlayer                   = sfxPlayer;
            m_AudioProvider               = battleAudioProvider;
            m_ArenaLoader                 = arenaLoader;
            m_BattleModule                = this;

            UIDocument uIDocument = Object.FindAnyObjectByType<UIDocument>();
            m_UIContainer = uIDocument.rootVisualElement;
        }

        async Task IModule.OnEnterAsync(IModuleArgs _)
        {
            await m_ScreenFadeService.FadeOutAsync(true);

            m_InputAdapter = Object.FindAnyObjectByType<InputAdapter>();
            m_DIResolver.InjectInto(m_InputAdapter);
            m_InputAdapter.Disable();

            m_BattleCompleteStateProvider.Set(BattleCompleteState.BATTLE_STILL_ACTIVE);

            m_BattleArgs = m_BattleArgsProvider.Get;

            // TODO: how to handle both a 3d arena and a 2d background?
            GameObject arenaGameObject = await m_ArenaLoader.LoadAsync(m_BattleArgs.Arena);

            // TODO:
            // get asset bundle holding player(s) from battle actors db
            // get list of enemies using enemy group id from battle actors db
            // for each enemy
            //     get asset bundle holding enemy from battle actors db
            // load and init players
            // load and init enemies
            // resolve appropriate battle flags (back attack, preemptive etc)

            m_ScreenFadeService.SetFadeToBattleReveal();
            await m_ScreenFadeService.FadeInAsync();

            // TODO:
            // await any background intro (camera movement etc)
            // await any character reveals (could be fade in + sfx)
            // await any character reveals (could come from their init script)

            m_InputAdapter.Enable();

            // TODO:
            // start battle state machine

            // TODO: temp tor testing, remove
            await Awaitable.WaitForSecondsAsync(3f);

            SetBattleComplete(BattleCompleteState.VICTORY);
        }

        async Task IModule.OnExitAsync()
        {
            m_InputAdapter.Disable();

            m_ScreenFadeService.SetFadeToSimple();
            await m_ScreenFadeService.FadeOutAsync();

            await m_ArenaLoader.UnloadAsync();

            m_CoreModule.ResetModule<IBattleModule, BattleModule>();
        }

        private void SetBattleComplete(BattleCompleteState state)
        {
            // potentially sequence the default flow and just early return if the flag is set
            // i.e. play victory (if victory disabled, early return), go to spoils (if spoils disabled, early return etc)

            // TODO:
            // write hp/status effects to save map
            // add spoils to inventory

            if (state == BattleCompleteState.GAME_OVER && m_BattleArgs.HasFlag(BattleFlags.DISABLE_GAME_OVER))
            {
                m_BattleCompleteStateProvider.Set(BattleCompleteState.VICTORY);

                ReturnToPreviousModuleAsync().FireAndForget();
                return;
            }

            m_BattleCompleteStateProvider.Set(state);

            if (state == BattleCompleteState.ESCAPE)
            {
                TransitionToSpoilsScreenAsync().FireAndForget();
                return;
            }

            // at this point we're at state == BattleCompleteState.VICTORY

            if (!m_BattleArgs.HasFlag(BattleFlags.DISABLE_VICTORY))
            {
                VictorySequenceAsync().FireAndForget();
                return;
            }

            if (!m_BattleArgs.HasFlag(BattleFlags.DISABLE_SPOILS))
            {
                TransitionToSpoilsScreenAsync().FireAndForget();
                return;
            }

            ReturnToPreviousModuleAsync().FireAndForget();
        }

        private async Task VictorySequenceAsync()
        {
            Debug.Log("VictorySequenceAsync");

            await m_MusicPlayer.Stop();

            // TODO: use correct victory music ID
            int winId = m_AudioProvider.GetVictoryMusicId;
            await m_MusicPlayer.Play(winId);

            // TODO: await any victory dances/ui popup etc

            if (!m_BattleArgs.HasFlag(BattleFlags.DISABLE_SPOILS))
            {
                await TransitionToSpoilsScreenAsync();
                return;
            }

            await ReturnToPreviousModuleAsync();
        }

        private Task TransitionToSpoilsScreenAsync()
        {
            // TODO:
            // show UI
            // wait for input to next screen(s)
            // repeat until no more screens

            Debug.Log("TransitionToSpoilsScreenAsync");
            return ReturnToPreviousModuleAsync();
        }

        private Task ReturnToPreviousModuleAsync()
        {
            Debug.Log("ReturnToPreviousModuleAsync");
            return m_CoreModule.ResumeModuleAsync();
        }
    }
}