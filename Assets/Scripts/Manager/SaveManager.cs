using NUnit.Framework.Constraints;
using System;
using System.IO;
using TMPro;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private struct SaveObject
    {
        public int bestRound;
        public string bestTime;

        public bool newRecord;
    }
    private SaveObject saveObject;
    private const string SAVE_FILE_PATH = "/save.txt";
    private string path;

    // Private
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        saveObject = new SaveObject();

        path = Application.dataPath + SAVE_FILE_PATH;
    }
    private bool FileExists() { return File.Exists(path); }

    // Public
    public void SaveRound(int bestRound, string bestTime)
    {
        //Debug.Log("Saving Round: ");
        if (RoundIsBetter(bestRound, bestTime)) 
        {
            saveObject.bestRound = bestRound;
            saveObject.bestTime = bestTime;
            saveObject.newRecord = true;

            string json = JsonUtility.ToJson(saveObject);
            File.WriteAllText(path, json);
        }
    }

    private bool RoundIsBetter(int bestRound, string thisTime)
    {
        if (FileExists())
        {
            string saveString = File.ReadAllText(path);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);


            // Check Round
            if(saveObject.bestRound < bestRound) { return true; }
            else if (saveObject.bestRound > bestRound) { return false; }

            // Check Time
            string[] time = thisTime.Split(':');
            string[] currentBestTime = saveObject.bestTime.Split(":");
            if(currentBestTime.Length <  time.Length) { return true; }
            else if(currentBestTime.Length > time.Length) { return false; }
            else
            {
                for (int i = 0; i < time.Length; i++)
                {
                    if (float.Parse(currentBestTime[i]) < float.Parse(time[i])) { return false; }
                    else if(float.Parse(currentBestTime[i]) > float.Parse(time[i])) { return true; }
                }
                return false;
            }
        }
        else { return true; }
    }

    public void LoadRound(TMP_Text bestRound, TMP_Text bestTime)
    {
        if (FileExists())
        {
            string saveString = File.ReadAllText(path);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            bestRound.text = (saveObject.bestRound).ToString();
            bestTime.text = saveObject.bestTime;
        }
    }

    /* Getters and Setters */
    public bool GetNewRecord() { if(FileExists()) { return saveObject.newRecord; } return false; }
    public void SetNewRecord(bool state) { if (FileExists()) { saveObject.newRecord = state; } }
}
