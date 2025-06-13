using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bestRound;
    [SerializeField] private TMP_Text bestTime;
    [SerializeField] private TMP_Text newRecord;

    private void Start() {
        SaveManager.instance.LoadRound(bestRound, bestTime);
        bool isNewRecord = SaveManager.instance.GetNewRecord();
        newRecord.GetComponent<NewRecordLogic>().SetVisibility(isNewRecord);
        SaveManager.instance.SetNewRecord(false);
    }
}
