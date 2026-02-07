using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    
    private const string PREF_TipsSkip= "TipsSkip";
    
    private bool _isSkip = false;

    public Button skip;
    public Toggle skiptog;
    
    void Start()
    {
        BindUIEvents();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt(PREF_TipsSkip, _isSkip ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    
    private void BindUIEvents()
    {
        skip.onClick.AddListener(SaveSettings);
        skiptog.onValueChanged.AddListener((isOn) =>
        {
            _isSkip = isOn;
        });
        
    }
}
