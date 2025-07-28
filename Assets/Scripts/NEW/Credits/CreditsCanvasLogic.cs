using TMPro;
using UnityEngine;

public class CreditsCanvasLogic : MonoBehaviour
{
    private static readonly string _logTag = "CreditsCanvasLogic";
    [SerializeField] private TextMeshProUGUI TMP_Credits;

    [SerializeField] private float scrollSpeed;
    [SerializeField] private float scrollSpeedMultiplier;
    [SerializeField] private float creditsSpawnPosY;
    private Vector3 scrollDirection;

    private void Awake()
    {
        TMP_Credits.text = Credits.GetCredits(GameDataSystem.currentChapter);

        scrollDirection = Vector3.up * scrollSpeed;
    }

    private void Start()
    {
        TMP_Credits.transform.position = new Vector3(TMP_Credits.transform.position.x, creditsSpawnPosY, TMP_Credits.transform.position.z);
    }

    private void Update()
    {
        TMP_Credits.rectTransform.position += scrollDirection * Time.deltaTime;
    }

    public void Navigate(Vector2 direction)
    {
        scrollDirection = (direction == Vector2.zero)? Vector3.up * scrollSpeed : new Vector3(0f, direction.normalized.y * (scrollSpeedMultiplier * scrollSpeed), 0f);
    }
}
