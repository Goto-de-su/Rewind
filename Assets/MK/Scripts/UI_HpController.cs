using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_HpController : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_Text hpText;
	[SerializeField] private Image hpBar; // 前景バー

	[Header("HP")]
	[SerializeField] private int maxHp = 120;
	[SerializeField] private int currentHp = 120;

	[Header("Bar Width")]
	[SerializeField] private float barMaxWidth = 500f;      // 0〜500で制御
	[SerializeField] private bool readBarWidthFromRect = true;

	[Header("Animation")]
	[SerializeField] private bool smoothBar = true;
	[SerializeField] private float barLerpSpeed = 10f;      // 大きいほど速い

	private RectTransform barRect;
	private float targetWidth;

	private void Awake()
	{
		if (hpText == null || hpBar == null)
		{
			Debug.LogError("[HpUI] hpText と hpBar を割り当ててください。");
			enabled = false;
			return;
		}

		barRect = hpBar.rectTransform;

		if (readBarWidthFromRect)
			barMaxWidth = barRect.sizeDelta.x; // 例: 500

		currentHp = Mathf.Clamp(currentHp, 0, maxHp);
		targetWidth = WidthFromHp(currentHp);
		SetBarWidthInstant(targetWidth);
		RefreshText();
	}

	private void Update()
	{
		if (!smoothBar) return;

		float w = barRect.sizeDelta.x;
		if (Mathf.Approximately(w, targetWidth)) return;

		w = Mathf.MoveTowards(w, targetWidth, barLerpSpeed * Time.unscaledDeltaTime * barMaxWidth);
		barRect.sizeDelta = new Vector2(w, barRect.sizeDelta.y);
	}

	private float WidthFromHp(int hp)
	{
		float t = maxHp > 0 ? (float)hp / maxHp : 0f;
		return barMaxWidth * Mathf.Clamp01(t);
	}

	private void SetBarWidthInstant(float width)
	{
		barRect.sizeDelta = new Vector2(width, barRect.sizeDelta.y);
	}

	private void RefreshText()
	{
		hpText.text = $"{currentHp}/{maxHp}";
	}

	// 公開API
	public void SetMaxHp(int newMax, bool keepRatio = true)
	{
		newMax = Mathf.Max(1, newMax);
		if (keepRatio)
			currentHp = Mathf.RoundToInt((float)currentHp / maxHp * newMax);
		else
			currentHp = Mathf.Min(currentHp, newMax);

		maxHp = newMax;
		targetWidth = WidthFromHp(currentHp);
		if (!smoothBar) SetBarWidthInstant(targetWidth);
		RefreshText();
	}

	public void SetHp(int hp)
	{
		currentHp = Mathf.Clamp(hp, 0, maxHp);
		targetWidth = WidthFromHp(currentHp);
		if (!smoothBar) SetBarWidthInstant(targetWidth);
		RefreshText();
	}

	public void Damage(int amount) => SetHp(currentHp - Mathf.Abs(amount));
	public void Heal(int amount)   => SetHp(currentHp + Mathf.Abs(amount));
}
