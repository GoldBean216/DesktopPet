using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;
    [Header("默认窗口大小（0~1）")]
    [Range(0f, 1f)] public float defaultSize = 0f;
    
    [Header("喝水提醒默认设置")]
    public float defaultIntervalHours = 1.5f;
    public int defaultStartHour = 8;
    public int defaultEndHour = 22;

    private bool _isSFXOn = true;
    private float _size;
    
    // 喝水提醒设置
    private float _reminderInterval;
    private int _startHour;
    private int _endHour;

    public Transform root;
    
    
    private const string PREF_SFX_ON = "SFXOn";
    private const string PREF_Size = "Size";
    private const string PREF_ReminderInterval = "ReminderInterval";
    private const string PREF_ReminderStart = "ReminderStart";
    private const string PREF_ReminderEnd = "ReminderEnd";
    
    void Awake()
    {
        // 单例模式（确保全局唯一）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景不销毁
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 加载本地保存的设置（启动游戏时自动加载）
        LoadAudioSettings();
        LoadReminderSettings();
    }
    
    private void LoadAudioSettings()
    {
        _isSFXOn = PlayerPrefs.GetInt(PREF_SFX_ON, 1) == 1;
        _size = PlayerPrefs.GetFloat(PREF_Size, defaultSize);
        
    }

    private void LoadReminderSettings()
    {
        _reminderInterval = PlayerPrefs.GetFloat(PREF_ReminderInterval, defaultIntervalHours);
        _startHour = PlayerPrefs.GetInt(PREF_ReminderStart, defaultStartHour);
        _endHour = PlayerPrefs.GetInt(PREF_ReminderEnd, defaultEndHour);
    }
    
    public void SaveSettings()
    {
        PlayerPrefs.SetInt(PREF_SFX_ON, _isSFXOn ? 1 : 0);
        // 保存音量值
        PlayerPrefs.SetFloat(PREF_Size, _size);
        
        // 保存喝水提醒设置
        PlayerPrefs.SetFloat(PREF_ReminderInterval, _reminderInterval);
        PlayerPrefs.SetInt(PREF_ReminderStart, _startHour);
        PlayerPrefs.SetInt(PREF_ReminderEnd, _endHour);

        // 立即写入本地（避免数据丢失）
        PlayerPrefs.Save();
        
    }
    
    private void ApplySizeSetting()
    {
        float finalSize = Mathf.Lerp(0.3f, 0.5f, _size);
        if (root!=null) 
            root.localScale = new Vector3(finalSize, finalSize, 1);
    }
    public void SetSFXOn(bool isOn)
    {
        _isSFXOn = isOn;
    }
    
    public void SetSize(float size)
    {
        _size = Mathf.Clamp01(size);
        ApplySizeSetting();
    }
    
    
    public bool GetSFXOn() => _isSFXOn;
    public float GetSize() => _size;

    // 喝水提醒 Getter/Setter
    public float GetReminderInterval() => _reminderInterval;
    public int GetReminderStartHour() => _startHour;
    public int GetReminderEndHour() => _endHour;

    public void SetReminderInterval(float hours) => _reminderInterval = Mathf.Max(0.1f, hours); // 最小间隔0.1小时
    public void SetReminderStartHour(int hour) => _startHour = Mathf.Clamp(hour, 0, 23);
    public void SetReminderEndHour(int hour) => _endHour = Mathf.Clamp(hour, 0, 23);
}
