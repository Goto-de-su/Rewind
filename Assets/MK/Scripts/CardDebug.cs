using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CardDebug : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Image image;
	private Color originalColor;

	public void Start()
	{
		image = GetComponent<Image>();
		originalColor = image.color;
	}

	public void OnPointerEnter(PointerEventData e)
	{
		image.color = Color.red;
	}

	public void OnPointerExit(PointerEventData e)
	{
		image.color = originalColor;
	}
}
