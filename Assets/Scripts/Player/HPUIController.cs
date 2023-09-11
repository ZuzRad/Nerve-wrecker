using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIController : MonoBehaviour
{
    [SerializeField] private Image[] heartsImg;
    [SerializeField] private Sprite addHeartSprite;
    [SerializeField] private Sprite deleteHeartSprite;
    private int heartsIndex = 3;
    public void DecreaseHeartsAmount()
    {
        heartsIndex--;
        heartsImg[heartsIndex].sprite = deleteHeartSprite;
    }

    public void IncreaseHeartsAmount()
    {
        heartsImg[heartsIndex].sprite = addHeartSprite;
        heartsIndex++;
    }
}
