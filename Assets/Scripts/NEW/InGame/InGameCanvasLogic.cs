using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameCanvasLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] TextMeshProUGUI _numEnemiesLeftText;
    [SerializeField] TextMeshProUGUI _timerText;

    private float _timer;
    public bool isTimerActive { get; private set; }


    private void Awake()
    {
        _levelText.text = "";
        _numEnemiesLeftText.text = "0";
        _timerText.text = "00:00";
        isTimerActive = false;
    }

    private void Update()
    {
        if(isTimerActive)
        {
            _timer -= Time.deltaTime;
            _timerText.text = $"{_timer / 60:00}:{_timer % 60:00}";
        }
    }

    public void Initialize(string levelName, int numEnemiesLeft, float timer)
    {
        _levelText.text = levelName;

        _numEnemiesLeftText.text = numEnemiesLeft.ToString();

        isTimerActive = true;
        _timer = timer;
        _timerText.text = $"{_timer / 60:00}:{_timer % 60:00}";
    }
}
