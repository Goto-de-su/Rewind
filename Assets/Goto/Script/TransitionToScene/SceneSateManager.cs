using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSateManager : MonoBehaviour
{
    // シーン
    private SceneState scene = SceneState.GamePlay;

    [Header("特定シーンのみのオブジェクト")]
    //[Tooltip("リザルトシーンのみのボタン")]
    //[SerializeField] private List<Button> onlyResultButton;
    [Tooltip("リザルトシーンのみのオブジェクト")]
    private GameObject onlyResultObj;
    [Tooltip("プレイシーンのみのオブジェクト")]
    private GameObject onlyPlayObj;

    private void Start()
    {
        onlyResultObj = GameObject.Find("ResultCanvasGroup");
        onlyPlayObj = GameObject.Find("PlayCanvasGroup");
        TransitionToPlay();
    }

    /// <summary>
    /// プレイシーンへシーン遷移
    /// </summary>
    public void TransitionToPlay()
    {
        // ステータスの変更
        scene = SceneState.GamePlay;
        // オブジェクトの表示/非表示
        ManageDisplayObj(onlyResultObj, false);
        ManageDisplayObj(onlyPlayObj, true);
    }

    /// <summary>
    /// リザルトシーンへシーン遷移
    /// </summary>
    public void TransitionToResult()
    {
        // ステータスの変更
        scene = SceneState.Result;
        // オブジェクトの表示/非表示
        ManageDisplayObj(onlyPlayObj, false);
        ManageDisplayObj(onlyResultObj, true);
    }

    private void ManageDisplayObj(GameObject obj, bool displayable)
    {
        obj.SetActive(displayable);
    }

    

}

/// <summary>
/// ゲームプレイシーン内のシーン状態
/// </summary>
enum SceneState
{
    GamePlay,
    Result
}