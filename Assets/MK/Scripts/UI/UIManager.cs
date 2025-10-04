using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// UIを一元管理する
/// </summary>
public class UIManager : MonoBehaviour
{
	[Header("参照")]
	[SerializeField] private ClockUIControl clock;

	[Header("入力設定")]
	[SerializeField] private bool wrap = false;			// 0↔12の循環
	[SerializeField] private float repeatDelay = 0.2f; 	// 長押しリピート間隔

	private InputAction horizontal;
	private float heldSign = 0f;
	private float nextRepeatTime = 0f;

	private void Awake()
	{
		// 1D Axis コンポジット（左=-1 / 右=+1）
		horizontal = new InputAction("Horizontal", InputActionType.Value);
		var comp = horizontal.AddCompositeBinding("1DAxis");
		comp.With("negative", "<Keyboard>/leftArrow");
		comp.With("positive", "<Keyboard>/rightArrow");
		// 予備（任意）
		comp.With("negative", "<Keyboard>/a");
		comp.With("positive", "<Keyboard>/d");

		horizontal.performed += OnHorizontal;
		horizontal.canceled  += OnHorizontalCanceled;
	}

	private void OnEnable()  => horizontal.Enable();
	private void OnDisable() => horizontal.Disable();
	private void OnDestroy()
	{
		horizontal.performed -= OnHorizontal;
		horizontal.canceled  -= OnHorizontalCanceled;
		horizontal.Dispose();
	}

	private void OnHorizontal(InputAction.CallbackContext ctx)
	{
		float v = ctx.ReadValue<float>();
		if (Mathf.Abs(v) < 0.5f) return;

		heldSign = Mathf.Sign(v);
		Step((int)heldSign);                 // 1回分
		nextRepeatTime = Time.time + repeatDelay;
	}

	private void OnHorizontalCanceled(InputAction.CallbackContext ctx)
	{
		heldSign = 0f;
	}

	private void Update()
	{
		if (heldSign != 0f && Time.time >= nextRepeatTime)
		{
			Step((int)heldSign);             // 長押しリピート
			nextRepeatTime = Time.time + repeatDelay;
		}

		if (Keyboard.current.digit1Key.wasPressedThisFrame)
			clock.SetEnemyIndices(new int[] { 3, 7 });
		if (Keyboard.current.digit2Key.wasPressedThisFrame)
			clock.SetEnemyIndices(new int[] { 3, 8, 10 });
		if (Keyboard.current.digit3Key.wasPressedThisFrame)
			clock.SetEnemyIndices(new int[] { 12 });
		if (Keyboard.current.digit0Key.wasPressedThisFrame)
			clock.ClearEnemyIcons();
	}

	private void Step(int delta)
	{
		if (clock == null) return;

		int idx = clock.TargetIndex + delta;
		if (wrap)
			idx = (idx % 13 + 13) % 13;      // 0..12 に循環
		else
			idx = Mathf.Clamp(idx, 0, 12);

		clock.SetPosition(idx);              // DOTweenでスムーズ移動
	}
}
