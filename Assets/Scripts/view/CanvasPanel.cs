using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class CanvasPanel : MonoBehaviour
{
    private GameObject _panel;
    private Image _image;
    private const float FADE_DELAY = 0.8f;

    void Start ()
	{ 
	    AddPanel();
        Messenger.AddListener<WindowsId>(EventTypes.SHOW_WINDOW, OnOpenWindow);
        Messenger.AddListener<WindowsId>(EventTypes.HIDE_WINDOW, OnHideWindow);
    }

    private void OnHideWindow(WindowsId windowId)
    {
        _image.DOFade(0, FADE_DELAY);
        StartCoroutine(Timer.Start(FADE_DELAY, false, () =>
        {
            _panel.SetActive(false);
        }));
    }

    private void OnOpenWindow(WindowsId windowId)
    {
        _panel.SetActive(true);
        _image.DOFade(0.1f, FADE_DELAY);
    }

    private void AddPanel()
    {
        _panel = new GameObject("Panel");
        _panel.AddComponent<CanvasRenderer>();
        _image = _panel.AddComponent<Image>();
        //DOTween.ToAlpha(() => mySprite.Color, x => mySprite.color = x, 0, 1);
        _image.color = Color.black;
        Color c = _image.color;
        c.a = 0;
        _image.color = c;
        RectTransform rect = (RectTransform)_panel.transform;
        rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        _panel.transform.SetParent(transform);
        _panel.transform.localPosition = new Vector3(0f, 0f, 0f);
        _panel.SetActive(false);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener<WindowsId>(EventTypes.SHOW_WINDOW, OnOpenWindow);
        Messenger.RemoveListener<WindowsId>(EventTypes.HIDE_WINDOW, OnHideWindow);
    }
}
