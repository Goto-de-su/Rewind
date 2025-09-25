using UnityEngine;

public class TransiitionBuuttonToResult : MonoBehaviour
{
    [SerializeField] private SceneSateManager sceneManager;

    /// <summary>
    /// ボタン押下により、リザルトシーンへ
    /// </summary>
    public void OnClick()
    {
        sceneManager.TransitionToResult();
    }
}
