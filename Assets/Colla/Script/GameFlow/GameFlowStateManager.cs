using UnityEngine;

public class GameFlowStateManager : MonoBehaviour
{
    public GameFlowState gameState;

    private void Start()
    {
        SetState(GameFlowState.Initialize);
    }

    private void SetState(GameFlowState nextState)
    {
        if (gameState == nextState)
        {
            return;
        }
    }

    private void StateMachine()
    {
        switch (gameState)
        {
            case GameFlowState.Initialize:
                Initialize();
                break;
            case GameFlowState.GamePlay:
                GamePlay();
                break;
            case GameFlowState.GameClear:
                GameClear();
                break;
            case GameFlowState.GameOver:
                GameOver();
                break;
        }
    }

    private void Initialize()
    {

    }

    private void GamePlay()
    {

    }

    private void GameClear()
    {

    }

    private void GameOver()
    {

    }
}
