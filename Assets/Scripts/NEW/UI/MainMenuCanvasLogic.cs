using TMPro;
using DG.Tweening;
using UnityEngine;
using EasyTextEffects;
using System;

public class MainMenuCanvasLogic : MonoBehaviour
{
    const string _logTag = "MainMenuCanvasLogic";

    [SerializeField] MainMenuCanvasHeaderLogic _header;
    [SerializeField] MainMenuCanvasBodyLogic _body;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _header.Initialize();
        _body.Initialize();
    }

    public void Continue()
    {
        _header.Continue(); // This calls continue on _header after animation complete
    }
}
