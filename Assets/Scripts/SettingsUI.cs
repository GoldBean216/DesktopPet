using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsUI : MonoBehaviour
{
    [Header("Audio控制")]
    public Toggle sfxToggle;
    [Header("Size控制")]
    public Slider Slider;
    
    [Header("喝水提醒设置 (可选绑定)")]
    public InputField intervalInput;
    public InputField startHourInput;
    public InputField endHourInput;

    void Start()
    {
        // 初始化UI显示（加载当前音效设置）
        InitUISettings();

        // 绑定UI事件
        BindUIEvents();
    }
    
    private void InitUISettings()
    {
        if (Settings.Instance == null)
        {
            Debug.LogError("Settings Instance 未找到！请确保场景中存在挂载 Settings 脚本的对象。");
            return;
        }

        if (sfxToggle != null)
            sfxToggle.isOn = Settings.Instance.GetSFXOn();
        
        if (Slider != null)
            Slider.value = Settings.Instance.GetSize();
        
        // 初始化喝水提醒UI
        if (intervalInput != null)
            intervalInput.text = Settings.Instance.GetReminderInterval().ToString("F1");
        if (startHourInput != null)
            startHourInput.text = Settings.Instance.GetReminderStartHour().ToString();
        if (endHourInput != null)
            endHourInput.text = Settings.Instance.GetReminderEndHour().ToString();
    }

    /// <summary>
    /// 绑定UI交互事件
    /// </summary>
    private void BindUIEvents()
    {
        
        Slider.onValueChanged.AddListener((size) =>
        {
            Settings.Instance.SetSize(size);
        });

        // SFX开关
        sfxToggle.onValueChanged.AddListener((isOn) =>
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.SetSFXOn(isOn);
            else 
                Settings.Instance.SetSFXOn(isOn);
        });
        
        // 绑定喝水提醒输入框
        if (intervalInput != null)
        {
            intervalInput.onEndEdit.AddListener((val) => {
                if (float.TryParse(val, out float res)) {
                    Settings.Instance.SetReminderInterval(res);
                    Settings.Instance.SaveSettings(); // 实时保存
                }
            });
        }

        if (startHourInput != null)
        {
            startHourInput.onEndEdit.AddListener((val) => {
                if (int.TryParse(val, out int res)) {
                    Settings.Instance.SetReminderStartHour(res);
                    Settings.Instance.SaveSettings();
                }
            });
        }

        if (endHourInput != null)
        {
            endHourInput.onEndEdit.AddListener((val) => {
                if (int.TryParse(val, out int res)) {
                    Settings.Instance.SetReminderEndHour(res);
                    Settings.Instance.SaveSettings();
                }
            });
        }
        
    }
}
