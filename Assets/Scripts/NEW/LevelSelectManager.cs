using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Enviroment")]
    [SerializeField] new Camera camera;
    [SerializeField] GameObject background;

    [Header("Level Select")]
    [SerializeField] Canvas levelSelectCanvas;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Instantiate(camera);
        Instantiate(background);
        levelSelectCanvas.GetComponent<LevelSelectCanvasLogic>().Initialize();
        Instantiate(levelSelectCanvas);
    }
}
