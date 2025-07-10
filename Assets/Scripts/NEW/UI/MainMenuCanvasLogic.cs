using UnityEngine;


public class MainMenuCanvasLogic : MonoBehaviour
{
    const string _logTag = "MainMenuCanvasLogic";

    [SerializeField] MainMenuCanvasHeaderLogic _header;
    [SerializeField] MainMenuCanvasBodyLogic _body;

    private void Start()
    {
        _header.Initialize();
        _body.Initialize();
    }

    public void Continue()
    {
        _header.Continue(); // This calls continue on _header after animation complete
    }

    public void SetActive(bool active)
    {
        if(active)
        {
            _header.gameObject.SetActive(true);
            _body.gameObject.SetActive(true);
        }
        else
        {
            _header.gameObject.SetActive(false);
            _body.gameObject.SetActive(false);
        }
    }
}
