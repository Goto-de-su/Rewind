using UnityEngine;

public class HorizontalCardHolder : MonoBehaviour
{
	[SerializeField] private GameObject slotPrefab;
	[Header("スポーン設定")]
	[SerializeField] private int cardsToSpawn = 8;

	void Start()
	{
		for (int i = 0; i < cardsToSpawn; i++)
		{
			Instantiate(slotPrefab, transform);
		}
	}
}
