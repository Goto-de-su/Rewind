using UnityEngine;
using UnityEngine.InputSystem;

public class UI_HpController_Test : MonoBehaviour
{
	[SerializeField] private UI_HpController hpUi;
	[SerializeField] private int step = 10;

	private InputAction damageAction;
	private InputAction healAction;

	private void Awake()
	{
		if (hpUi == null) hpUi = GetComponent<UI_HpController>();

		damageAction = new InputAction("Damage", binding: "<Keyboard>/leftArrow");
		healAction   = new InputAction("Heal",   binding: "<Keyboard>/rightArrow");

		damageAction.performed += _ => hpUi?.Damage(step);
		healAction.performed   += _ => hpUi?.Heal(step);
	}

	private void OnEnable()  { damageAction.Enable(); healAction.Enable(); }
	private void OnDisable() { damageAction.Disable(); healAction.Disable(); }
	private void OnDestroy() { damageAction.Dispose(); healAction.Dispose(); }
}
