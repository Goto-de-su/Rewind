using System.Collections.Generic;
using UnityEngine;

public class HandUIController : MonoBehaviour
{
	[SerializeField] private List<CardVisualController> cardSlots = new List<CardVisualController>(7);

	private CardVisualController selectedVisual;
	private Card selectedCard;

	public int MaxHandSize => cardSlots.Count;

	private void OnEnable()
	{
		SubscribeToSlots(true);
	}

	private void OnDisable()
	{
		SubscribeToSlots(false);
	}

	public void SetHand(IReadOnlyList<Card> cards)
	{
		int availableCards = cards?.Count ?? 0;
		int cardsToShow = Mathf.Min(availableCards, cardSlots.Count);

		for (int i = 0; i < cardSlots.Count; i++)
		{
			CardVisualController slot = cardSlots[i];
			if (slot == null) continue;

			if (i < cardsToShow)
			{
				Card card = cards[i];
				slot.gameObject.SetActive(true);
				slot.SetCard(card);

				if (slot == selectedVisual)
				{
					selectedCard = card;
					slot.SetSelected(true);
				}
			}
			else
			{
				slot.ClearCard();
				slot.gameObject.SetActive(false);

				if (slot == selectedVisual)
				{
					selectedVisual = null;
					selectedCard = null;
				}
			}
		}

		if (availableCards == 0)
		{
			selectedVisual = null;
			selectedCard = null;
		}
	}

	public Card GetSelectCard() => selectedCard;

	private void OnCardSelectionChanged(CardVisualController controller, bool isSelected)
	{
		if (controller == null) return;

		if (isSelected)
		{
			if (selectedVisual != null && selectedVisual != controller)
			{
				selectedVisual.SetSelected(false);
			}

			selectedVisual = controller;
			selectedCard = controller.CurrentCard;
		}
		else if (selectedVisual == controller)
		{
			selectedVisual = null;
			selectedCard = null;
		}
	}

	private void SubscribeToSlots(bool subscribe)
	{
		for (int i = 0; i < cardSlots.Count; i++)
		{
			CardVisualController slot = cardSlots[i];
			if (slot == null) continue;

			if (subscribe)
			{
				slot.SelectionChanged += OnCardSelectionChanged;
			}
			else
			{
				slot.SelectionChanged -= OnCardSelectionChanged;
			}
		}
	}
}
