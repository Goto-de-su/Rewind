using System.Collections.Generic;
using UnityEngine;

public class HandTest : MonoBehaviour
{
	[SerializeField] private List<Card> data;

	public HandUIController handUIController;

	private void Start()
	{
		handUIController.SetHand(data);
	}
}
