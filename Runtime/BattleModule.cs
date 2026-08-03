using System.Threading.Tasks;
using RPGFramework.Battle.SharedTypes;
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
        private readonly ICoreModule        m_CoreModule;
        private readonly IDIResolver        m_DIResolver;
        private readonly IScreenFadeService m_ScreenFadeService;
        private readonly IBattleModule      m_BattleModule;
        private readonly VisualElement      m_UIContainer;

        private InputAdapter m_InputAdapter;

        public BattleModule(ICoreModule        coreModule,
                            IDIResolver        diResolver,
                            IScreenFadeService screenFadeService)
        {
            m_CoreModule        = coreModule;
            m_DIResolver        = diResolver;
            m_ScreenFadeService = screenFadeService;
            m_BattleModule      = this;

            UIDocument uIDocument = Object.FindAnyObjectByType<UIDocument>();
            m_UIContainer = uIDocument.rootVisualElement;
        }


        async Task IModule.OnEnterAsync(IModuleArgs args)
        {
            await m_ScreenFadeService.FadeOutAsync(true);
            
            m_InputAdapter = Object.FindAnyObjectByType<InputAdapter>();
            m_DIResolver.InjectInto(m_InputAdapter);

            IBattleModuleArgs battleArgs = (IBattleModuleArgs)args;
            Debug.Log($"battle args: arena: {battleArgs.Arena}");
            Debug.Log($"battle args: enemy group: {battleArgs.EnemyGroup}");
            Debug.Log($"battle args: enemy level: {battleArgs.EnemyLevel}");
            
            // TODO:
            // load background
            // load enemy group
            // load player(s)
            // set entity stats
            
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
            
            m_CoreModule.ResumeModuleAsync().FireAndForget();
        }

        async Task IModule.OnExitAsync()
        {
            m_InputAdapter.Disable();
            
            m_ScreenFadeService.SetFadeToSimple();
            await m_ScreenFadeService.FadeOutAsync();

            m_CoreModule.ResetModule<IBattleModule, BattleModule>();
        }
    }
}