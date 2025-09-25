using UnityEngine;
using UnityEngine.EventSystems;

public class CardUIControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] int sortingOrder = 100;

	private Canvas parentCanvas;
	private RectTransform rectTransform;

	void Start()
	{
		parentCanvas = GetComponentInParent<Canvas>(true);
		if (parentCanvas == null) return;

		parentCanvas.overrideSorting = true; // 親の設定に従わず自前で並び替え

		rectTransform = GetComponent<RectTransform>();
	}

	public void OnPointerEnter(PointerEventData e)
	{
		parentCanvas.sortingOrder = sortingOrder;
		rectTransform.localScale = Vector3.one * 1.2f;
	}

	public void OnPointerExit(PointerEventData e)
	{
		parentCanvas.sortingOrder = 0;
		rectTransform.localScale = Vector3.one;
	}
}
