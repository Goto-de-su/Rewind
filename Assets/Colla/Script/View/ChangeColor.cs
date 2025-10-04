using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // 変更したい色
    public Color targetColor = Color.white;

    // ゲーム開始時に実行
    void Start()
    {
        ChangeAllChildSpriteColors(targetColor);
    }

    // 子オブジェクト（と自身）のSprite Colorを変更するメソッド
    public void ChangeAllChildSpriteColors(Color newColor)
    {
        // 親オブジェクトを含むすべての子オブジェクトのSpriteRendererを取得
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer renderer in renderers)
        {
            // 各SpriteRendererのColorプロパティを変更
            renderer.color = newColor;
        }

        //Debug.Log($"すべての子オブジェクト（と自身）を含む{renderers.Length}個のSprite Colorを変更しました。");
    }

    // テスト用のボタンをInspectorに表示する場合
    /*
    [ContextMenu("Test Change Color")]
    private void TestChangeColor()
    {
        // Debug.Logなどのテスト目的で利用可能
        ChangeAllChildSpriteColors(new Color(Random.value, Random.value, Random.value, 1f));
    }
    */
}