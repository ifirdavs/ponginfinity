using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : Singleton<UIManager>
{
    private List<UIController> UIControllers;
    private UIController lastActiveUI;

    protected override void Awake()
    {
        base.Awake();
        UIControllers = GetComponentsInChildren<UIController>().ToList();
        UIControllers.ForEach(x => x.gameObject.SetActive(false));
        SwitchUI(UIType.MainMenu);
    }

    public void SwitchUI(UIType type)
    {
        if (lastActiveUI != null)
        {
            lastActiveUI.gameObject.SetActive(false);
        }
        UIController desiredUI = UIControllers.Find(x => x.canvasType == type);
        if (desiredUI == null)
        {
            Debug.LogWarning("The desired UI was not found!");
            return;
        }
        desiredUI.gameObject.SetActive(true);
        lastActiveUI = desiredUI;
    }

    public void ActiveteObject(UIType type, string name, bool isActive)
    {
        UIController desiredUI = UIControllers.Find(x => x.canvasType == type);
        if (desiredUI == null)
        {
            Debug.LogWarning("The desired UI was not found!");
            return;
        }
        Transform objectTransform = desiredUI.gameObject.transform.Find(name);
        if (objectTransform == null)
        {
            Debug.LogWarning("The desired object was not found!");
            return;
        }
        objectTransform.gameObject.SetActive(isActive);
    }

    public void ChangeText(UIType type, string name, string text)
    {
        UIController desiredUI = UIControllers.Find(x => x.canvasType == type);
        if (desiredUI == null)
        {
            Debug.LogWarning("The desired UI was not found!");
            return;
        }
        Transform objectTransform = desiredUI.gameObject.transform.Find(name);
        if (objectTransform == null)
        {
            Debug.LogWarning(name + " was not found!");
            return;
        }
        Text objectText = objectTransform.gameObject.GetComponent<Text>();
        if (objectText == null)
        {
            Debug.LogWarning(name + " was not found!");
            return;
        }
        objectTransform.gameObject.GetComponent<Text>().text = text;
    }

    public void ShowModeMenu()
    {
        SwitchUI(UIType.ModeMenu);
    }
}
