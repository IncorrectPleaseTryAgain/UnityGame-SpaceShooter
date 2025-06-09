using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bestRound;
    [SerializeField] private TMP_Text bestTime;
    [SerializeField] private TMP_Text newRecord;
    [SerializeField] private GameObject earthIcon;

    private void Start() {
        SaveManager.instance.LoadRound(bestRound, bestTime);
        bool isNewRecord = SaveManager.instance.GetNewRecord();
        newRecord.GetComponent<NewRecordLogic>().SetVisibility(isNewRecord);
        earthIcon.SetActive(isNewRecord);
        SaveManager.instance.SetNewRecord(false);
    }
}
