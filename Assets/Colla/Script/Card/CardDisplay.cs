using UnityEngine;
using System.Collections.Generic;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private GameObject pictureObject;
    [SerializeField] private Sprite picture;
    [SerializeField] private List<GameObject> stickersObject;
    [SerializeField] private List<Sprite> stickers;
    [SerializeField] private GameObject costObject;
    [SerializeField] private int cost;

    [SerializeField] GameObject user;

    private Card _cardInstance;

    public void DisplayCard(Card card)
    {
        _cardInstance = card;
        // ‰æ‘œ‚ğ·‚µ‘Ö‚¦‚éˆ—‚ğ‰Á‚¦‚é
    }

    public void SetCard()
    {
        user.GetComponent<Charactor>().SetCard(_cardInstance);
    }


}
