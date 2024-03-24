using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;

public class LinkEventInvoker : MonoBehaviour
{
    private TextMeshProUGUI _textbox;
    public static event Action<string> LinkFound;
    
    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(CheckForLinkEvent);
    }

    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(CheckForLinkEvent);
    }

    private void Awake()
    {
        _textbox = (TextMeshProUGUI)GetComponent("TextMeshProUGUI");
    }

    private void CheckForLinkEvent(Object obj)
    {
        int amountOfLinksInCurrentText = _textbox.textInfo.linkCount;

        if (amountOfLinksInCurrentText == 0)
            return;

        for (int linkIndex = 0; linkIndex < amountOfLinksInCurrentText; linkIndex++)
        {
            var linkInfo = _textbox.textInfo.linkInfo[linkIndex];

            if (_textbox.maxVisibleCharacters == linkInfo.linkTextfirstCharacterIndex)
            {
                LinkFound?.Invoke(linkInfo.GetLinkID());
                break;
            }
        }
    }
}
