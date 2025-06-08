using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using TMPro;
using UnityEngine;

public class NewRecordLogic : MonoBehaviour
{
    public void SetVisibility(bool newRecord) { if (newRecord) { Show(); } else if(!newRecord) { Hide(); } }
    private void Hide() { GetComponent<RectTransform>().localScale = new Vector3(0,0,0); }
    private void Show() { GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); }
}
