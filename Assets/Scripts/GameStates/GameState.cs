using Events;
using IngameStateMachine;
using ScreenManager.Core;
using Screens.GameScreen;
using SimpleEventBus.Disposables;

namespace GameStates
{
    public class GameState :IState
    {
        private StateMachine _stateMachine;
        private CompositeDisposable _subscriptions;
    
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            ScreensManager.OpenScreen<GameScreen, GameScreenContext>(new GameScreenContext());
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<MenuButtonPressedEvent>(EnterMetaGameState)
            };
        }
    
        private void EnterMetaGameState(MenuButtonPressedEvent obj)
        {
            ScreensManager.CloseScreen<GameScreen>();
            _stateMachine.Enter<MetaGameState>();
        }

        public void OnExit()
        {
            _subscriptions?.Dispose();
        }
    }
}