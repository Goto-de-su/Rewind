using UnityEngine;

public class DebugLog : MonoBehaviour
{
    [Header("ログ")]
    [Tooltip("出力したい文字列を入力してください。")]
    [SerializeField] string log = "ログ";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(log);
    }
}
