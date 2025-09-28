using UnityEngine;
using UnityEngine.InputSystem;

public class UI_TimeController_Test : MonoBehaviour
{
	[SerializeField] private UI_TimeController timeUI;
	[SerializeField] private int cost = 1;

	private InputAction tAction;
	private InputAction rAction;

	private void Awake()
	{
		if (timeUI == null) timeUI = GetComponent<UI_TimeController>();

		tAction = new InputAction("SpendTime", binding: "<Keyboard>/t");
		rAction = new InputAction("ResetTime", binding: "<Keyboard>/r");

		tAction.performed += _ => timeUI?.AdvanceTime(cost);
		rAction.performed += _ => timeUI?.ResetTime();
	}

	private void OnEnable()  { tAction.Enable(); rAction.Enable(); }
	private void OnDisable() { tAction.Disable(); rAction.Disable(); }
	private void OnDestroy() { tAction.Dispose(); rAction.Dispose(); }
}
