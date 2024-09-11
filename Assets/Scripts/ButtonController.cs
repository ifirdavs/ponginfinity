using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject imageToShow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageToShow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imageToShow.SetActive(false);
    }
}
