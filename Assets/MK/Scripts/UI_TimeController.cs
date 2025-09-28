using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UI_TimeController : MonoBehaviour
{
	[Header("UI")]
	[SerializeField] private TMP_Text timeText;

	[Header("Hand (Sprite)")]
	[SerializeField] private Transform hand;

	[Header("Hand Angles (index 0..11)")]
	[Tooltip("要素数12。各インデックスのZ回転角度。")]
	[SerializeField] private float[] handAngles = new float[12];

	[Header("Time")]
	[SerializeField, Min(1)] private int maxTicks = 12;     // 分母
	[SerializeField] private int remainingTicks = 12;       // 分子
	[SerializeField] private bool smoothHand = true;
	[SerializeField] private float handLerpSpeed = 10f;     // 回転補間速度

	[Header("Enemy Turn (1..11)")]
	[SerializeField] private GameObject[] enemyTurns = new GameObject[11];
	[SerializeField, Range(1,11)] private int enemyTurnHour = 1;

	[Header("Events")]
	public UnityEvent<int> OnTimeAdvanced;  // 進んだ量
	public UnityEvent OnTimeReset;
	public UnityEvent OnTimeDepleted;       // 0到達

	private float targetZ;

	private void Awake()
	{
		if (timeText == null || hand == null)
		{
			Debug.LogError("[TimeUI] TMP_Text と Hand(Transform) を割り当ててください。");
			enabled = false; return;
		}
		remainingTicks = Mathf.Clamp(remainingTicks, 0, maxTicks);
		ApplyAll();
	}

	private void Update()
	{
		if (!smoothHand) return;
		var e = hand.localEulerAngles;
		float z = Mathf.MoveTowardsAngle(e.z, targetZ, handLerpSpeed * Time.unscaledDeltaTime * 360f);
		if (!Mathf.Approximately(z, e.z))
			hand.localRotation = Quaternion.Euler(0f, 0f, z);
	}

	// === Public API ===
	public void ResetTime()
	{
		remainingTicks = maxTicks;
		ApplyAll();
		OnTimeReset?.Invoke();
	}

	public void AdvanceTime(int cost)
	{
		int before = remainingTicks;
		remainingTicks = Mathf.Max(0, remainingTicks - Mathf.Max(0, cost));
		ApplyTextAndHand();
		OnTimeAdvanced?.Invoke(before - remainingTicks);
		if (remainingTicks == 0) OnTimeDepleted?.Invoke();
	}

	public void AddTime(int amount)
	{
		remainingTicks = Mathf.Min(maxTicks, remainingTicks + Mathf.Max(0, amount));
		ApplyTextAndHand();
	}

	public void SetEnemyTurn(int hour1to11)
	{
		enemyTurnHour = Mathf.Clamp(hour1to11, 1, 11);
		ApplyEnemyTurnActive();
	}

	public void AdvanceEnemyTurn(int step = 1)
	{
		int h = enemyTurnHour + step;
		while (h > 11) h -= 11;
		while (h < 1)  h += 11;
		enemyTurnHour = h;
		ApplyEnemyTurnActive();
	}

	// === Internal ===
	private void ApplyAll()
	{
		ApplyTextAndHand();
		ApplyEnemyTurnActive();
	}

	private void ApplyTextAndHand()
	{
		// 表示: 例 11/12
		timeText.text = $"{remainingTicks}/{maxTicks}";

		// 経過インデックス: 12進で循環。0(=12時)と12(=0)を同一にする
		int passedRaw = maxTicks - remainingTicks; // 0..∞
		int idx = ((passedRaw % 12) + 12) % 12;    // 0..11 に正規化

		float z = (handAngles != null && handAngles.Length > idx)
			? handAngles[idx]
			: 30f * idx; // フォールバック

		targetZ = z;
		if (!smoothHand)
			hand.localRotation = Quaternion.Euler(0f, 0f, targetZ);
	}

	private void ApplyEnemyTurnActive()
	{
		for (int i = 0; i < enemyTurns.Length; i++)
		{
			if (enemyTurns[i] == null) continue;
			bool on = (i == enemyTurnHour - 1);
			if (enemyTurns[i].activeSelf != on) enemyTurns[i].SetActive(on);
		}
	}
}
