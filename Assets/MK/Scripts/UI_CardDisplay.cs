using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CardDisplay : MonoBehaviour
{
	/*
	[Header("UI Refs")]
	[SerializeField] private TMP_Text costText;
	[SerializeField] private Image artImage;
	[SerializeField] private Image[] stickerImages = new Image[4]; // 1..4個

	private Card _card;

	// 外部からセット：set時に即反映
	public Card Card
	{
		get => _card;
		set { _card = value; Apply(); }
	}

	// UnityEvent等から呼べる用
	public void SetCard(Card card) => Card = card;

	private void OnEnable()
	{
		// 再表示時に反映を維持
		if (_card != null) Apply();
		else ClearVisuals();
	}

	private void Apply()
	{
		if (costText != null)
			costText.text = _card != null ? _card.cost.ToString() : "0";

		if (artImage != null)
		{
			artImage.sprite = _card != null ? _card.art : null;
			artImage.enabled = artImage.sprite != null;
		}

		// ステッカー
		for (int i = 0; i < stickerImages.Length; i++)
		{
			var img = stickerImages[i];
			if (img == null) continue;

			Sprite s = null;
			if (_card != null && _card.stickers != null && i < _card.stickers.Length)
				s = _card.stickers[i];

			img.sprite = s;
			img.enabled = s != null;               // 画像が無ければ非表示
			if (img.gameObject.activeSelf != (s != null))
				img.gameObject.SetActive(s != null); // オブジェクトごと消したい場合
		}
	}

	public void ClearVisuals()
	{
		if (costText != null) costText.text = "0";
		if (artImage != null) { artImage.sprite = null; artImage.enabled = false; }
		foreach (var img in stickerImages)
		{
			if (img == null) continue;
			img.sprite = null;
			img.enabled = false;
			if (img.gameObject.activeSelf) img.gameObject.SetActive(false);
		}
	}*/
}
