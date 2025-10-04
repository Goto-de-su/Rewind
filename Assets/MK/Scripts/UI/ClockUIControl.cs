using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// UIの横ゲージ型時計をコントロールするクラス
/// </summary>
public class ClockUIControl : MonoBehaviour
{
	[Header("短針")]
	[SerializeField] private RectTransform shortHand;

	[Header("位置マーカー(左→右に 13個(0~12時まで))")]
	[SerializeField] private List<RectTransform> slots = new List<RectTransform>();

	[Header("短針移動設定")]
	[Range(0, 12)]
	[SerializeField] private int targetIndex = 0;
	[SerializeField] private float moveDuration = 0.5f;
	[SerializeField] private Ease moveEase = Ease.OutQuad;
	[Tooltip("エディタ上で値変更時に即反映")]
	[SerializeField] private bool autoUpdateInEditor = true;
	[Tooltip("Start時にTargetIndexを適用")]
	[SerializeField] private bool applyOnStart = true;

	private Tween currentTween;

	// ---敵行動アイコン---
	[Header("敵アイコン")]
	[Tooltip("未指定なら shortHand の親を使用")]
	[SerializeField] private RectTransform iconsParent;
	[Tooltip("Enemy用UIプレハブ（Image等のRectTransformが付いたもの）")]
	[SerializeField] private RectTransform enemyIconPrefab;
	[Tooltip("スロット位置からのYオフセット（上方向+）")]
	[SerializeField] private float enemyIconOffsetY = 24f;
	[SerializeField] private Vector2 enemyIconScale = Vector2.one;

	// 生成・参照
	[SerializeField, HideInInspector] private List<RectTransform> enemyIcons = new List<RectTransform>(13);
	private readonly HashSet<int> activeEnemies = new HashSet<int>();

	public int TargetIndex
	{
		get => targetIndex;
		set => SetPosition(value);
	}

	private void Awake()
	{
		EnsureIcons();
	}

	private void Start()
	{
		if (applyOnStart) ApplyPosition(targetIndex, instant: false);
		// 起動時に敵アイコンの位置同期
		ApplyEnemyIcons();
	}


	// ---------------------------------------------------------------------------------------
	// --- 短針移動 ---
	// ---------------------------------------------------------------------------------------
#region 短針移動
	public void SetPosition(int index)
	{
		index = Mathf.Clamp(index, 0, 12);
		targetIndex = index;
		ApplyPosition(targetIndex, instant: false);
	}

	private void ApplyPosition(int index, bool instant)
	{
		if (shortHand == null)
		{
			Debug.LogWarning("[ClockUIControl] ShortHand が未設定です");
			return;
		}

		if (slots == null || slots.Count != 13)
		{
			Debug.LogWarning("[ClockUIControl] Slots は13個必要です。 現在: " + (slots == null ? 0 : slots.Count));
			return;
		}

		var slot = slots[Mathf.Clamp(index, 0, 12)];
		if (slot == null)
		{
			Debug.LogWarning("[ClockUIControl] 指定インデックスの slot が null です。 index= " + index);
			return;
		}

		// 親座標系内でX位置を合わせる
		Vector2 targetPos = shortHand.anchoredPosition;
		targetPos.x = slot.anchoredPosition.x;

		// 既存のTweenを止めて再生成
		currentTween?.Kill();

		if (instant || moveDuration <= 0f)
		{
			shortHand.anchoredPosition = targetPos;
		}
		else
		{
			currentTween = shortHand.DOAnchorPosX(targetPos.x, moveDuration).SetEase(moveEase).SetUpdate(true);
		}
	}
#endregion

	// ---------------------------------------------------------------------------------------
	// --- 敵行動アイコンAPI ---
	// ---------------------------------------------------------------------------------------
#region 敵行動アイコンAPI
	/// <summary>
	/// 表示するスロット番号（0..12）配列を渡す。その他は非表示。
	/// </summary>
	public void SetEnemyIndices(IEnumerable<int> indices)
	{
		activeEnemies.Clear();
		if (indices != null)
		{
			foreach (var raw in indices)
			{
				int i = Mathf.Clamp(raw, 0, 12);
				activeEnemies.Add(i); // 重複は自動で排除
			}
		}
		ApplyEnemyIcons();
	}

	/// <summary>
	/// すべての敵アイコンを非表示。
	/// </summary>
	public void ClearEnemyIcons()
	{
		activeEnemies.Clear();
		ApplyEnemyIcons();
	}

	private void ApplyEnemyIcons()
	{
		if (slots == null || slots.Count != 13) return;
		EnsureIcons();

		for (int i = 0; i < 13; i++)
		{
			var icon = enemyIcons[i];
			var slot = slots[i];
			if (icon == null || slot == null) continue;

			// スロットXに揃え、Yはオフセットで上へ
			Vector2 pos = slot.anchoredPosition;
			pos.y += enemyIconOffsetY;
			icon.anchoredPosition = pos;
			icon.localScale = new Vector3(enemyIconScale.x, enemyIconScale.y, 1f);

			icon.gameObject.SetActive(activeEnemies.Contains(i));
		}
	}

	private void EnsureIcons()
	{
		if (iconsParent == null)
		{
			if (shortHand != null) iconsParent = shortHand.parent as RectTransform;
			else if (slots != null && slots.Count > 0) iconsParent = slots[0]?.parent as RectTransform;
		}
		if (enemyIcons == null) enemyIcons = new List<RectTransform>(13);

		// リスト長を13に揃える
		for (int i = enemyIcons.Count; i < 13; i++) enemyIcons.Add(null);

		for (int i = 0; i < 13; i++)
		{
			if (enemyIcons[i] == null)
			{
				if (enemyIconPrefab == null) { return; } // インスペクター未設定時は何もしない
				var inst = Instantiate(enemyIconPrefab, iconsParent);
				inst.name = $"EnemyIcon_{i}";
				inst.anchorMin = new Vector2(0.5f, 0.5f);
				inst.anchorMax = new Vector2(0.5f, 0.5f);
				inst.pivot = new Vector2(0.5f, 0.5f);
				inst.gameObject.SetActive(false);
				enemyIcons[i] = inst;
			}
		}
	}
#endregion


#if UNITY_EDITOR
	private void OnValidate()
	{
		// エディタ上でも即反映
		if (!Application.isPlaying && autoUpdateInEditor)
		{
			// Repaint で見た目更新
			EditorApplication.delayCall += () =>
			{
				if (this != null) ApplyPosition(targetIndex, instant: true);
			};
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (slots == null) return;
		Gizmos.color = Color.cyan;
		foreach (var s in slots)
		{
			if (s == null) continue;
			// シーンビューで位置確認しやすくする
			Vector3 wpos = s.transform.position;
			Gizmos.DrawWireSphere(wpos, 5f * HandleUtility.GetHandleSize(wpos) * 0.02f);
		}
	}
#endif
}