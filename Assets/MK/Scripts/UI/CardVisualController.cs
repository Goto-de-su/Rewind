using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardVisualController : MonoBehaviour, IPointerClickHandler
{
	[Header("UI")]
	[SerializeField] private TMP_Text costText;
	[SerializeField] private Image cardPictureImage;
	[SerializeField] private List<StickerSlot> stickerSlots = new List<StickerSlot>();
	[SerializeField] private GameObject selectionIndicator;

	private Card currentCard;
	private int currentCost;
	private Sprite currentCardPicture;
	private readonly List<StickerInfo> stickerInfos = new List<StickerInfo>();
	private bool isSelected;

	public Card CurrentCard => currentCard;
	public bool IsSelected => isSelected;

	public event Action<CardVisualController, bool> SelectionChanged;

	private void Awake() => UpdateSelectionVisual();

	public void SetCard(Card card)
	{
		currentCard = card;
		CacheCardData();
		ApplyCardDataToView();
	}

	public void ClearCard()
	{
		currentCard = null;
		currentCost = 0;
		currentCardPicture = null;
		stickerInfos.Clear();
		ApplyCardDataToView();
		SetSelected(false, true);
	}

	public void SetSelected(bool selected) => SetSelected(selected, false);

	public void OnPointerClick(PointerEventData eventData)
	{
		if (currentCard == null) return;

		SetSelected(!isSelected);
	}

	private void SetSelected(bool selected, bool suppressEvent)
	{
		if (isSelected == selected) return;

		isSelected = selected;
		UpdateSelectionVisual();

		if (!suppressEvent)
		{
			SelectionChanged?.Invoke(this, isSelected);
		}
	}

	private void UpdateSelectionVisual()
	{
		if (selectionIndicator != null)
		{
			selectionIndicator.SetActive(isSelected);
		}
	}

	private void CacheCardData()
	{
		stickerInfos.Clear();

		if (currentCard == null || currentCard.CardData == null)
		{
			currentCost = 0;
			currentCardPicture = null;
			return;
		}

		CardData data = currentCard.CardData;
		currentCost = data.Cost;
		currentCardPicture = data.CardPicture;

		var stickers = data.Stickers;
		if (stickers == null) { return; }

		for (int i = 0; i < stickers.Count; i++)
		{
			StickerData sticker = stickers[i];
			if (sticker == null) { continue; }

			stickerInfos.Add(new StickerInfo(sticker.Type, sticker.Value, sticker.Icon));
		}
	}

	private void ApplyCardDataToView()
	{
		if (costText != null)
		{
			costText.text = currentCard != null && currentCard.CardData != null ? currentCost.ToString() : string.Empty;
		}

		if (cardPictureImage != null)
		{
			cardPictureImage.sprite = currentCardPicture;
			cardPictureImage.enabled = currentCardPicture != null;
		}

		for (int i = 0; i < stickerSlots.Count; i++)
		{
			StickerInfo? info = i < stickerInfos.Count ? stickerInfos[i] : (StickerInfo?)null;
			stickerSlots[i].Apply(info);
		}
	}

	[Serializable]
	private struct StickerSlot
	{
		[SerializeField] private TMP_Text typeText;
		[SerializeField] private TMP_Text valueText;
		[SerializeField] private Image iconImage;

		public void Apply(StickerInfo? info)
		{
			bool hasInfo = info.HasValue;

			if (typeText != null)
			{
				typeText.text = hasInfo ? info.Value.Type.ToString() : string.Empty;
			}

			if (valueText != null)
			{
				valueText.text = hasInfo ? info.Value.Value.ToString() : string.Empty;
			}

			if (iconImage != null)
			{
				iconImage.sprite = hasInfo ? info.Value.Icon : null;
				iconImage.enabled = hasInfo && info.Value.Icon != null;
			}
		}
	}

	private readonly struct StickerInfo
	{
		public StickerType Type { get; }
		public int Value { get; }
		public Sprite Icon { get; }

		public StickerInfo(StickerType type, int value, Sprite icon)
		{
			Type = type;
			Value = value;
			Icon = icon;
		}
	}
}
