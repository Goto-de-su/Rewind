using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionButton : MonoBehaviour
{
    [Header("遷移先シーン名")]
    [Tooltip("遷移先のシーン名を記入してください。")]
    [SerializeField] private string sceneName = "GamePlay";

    /// <summary>
    /// スタートボタン押下時の処理
    /// </summary>
    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
