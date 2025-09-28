using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameFlowState gameState;

    private void Start()
    {
        SetState(GameFlowState.PlayerTrunStart);
    }

    private void SetState(GameFlowState nextState)
    {
        if(gameState == nextState)
        {
            return;
        }


    }
}
