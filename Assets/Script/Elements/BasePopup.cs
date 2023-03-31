using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    public event Action ClickEvent = delegate {  };

    public void OnClick()
    {
        gameObject.SetActive(false);
        ClickEvent();
    }
}