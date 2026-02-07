using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReminder : MonoBehaviour
{
    private float timer = 0f;
    // 移除硬编码
    // private const float INTERVAL = 1.5f * 3600f; 
    // private const int START_HOUR = 8;
    // private const int END_HOUR = 22;

    public GameObject coffee;
    

    void Update()
    {
        if (Settings.Instance == null) return;

        // 1. 获取当前系统的小时数
        int currentHour = DateTime.Now.Hour;
        
        int startHour = Settings.Instance.GetReminderStartHour();
        int endHour = Settings.Instance.GetReminderEndHour();

        // 2. 判断是否在提醒的时间段内
        // 处理跨天的情况（例如 22点 到 次日8点）
        bool isInTimeRange = false;
        if (startHour <= endHour)
        {
            isInTimeRange = currentHour >= startHour && currentHour < endHour;
        }
        else
        {
            // 跨天：例如 22:00 ~ 08:00，当前是 23:00 (true) 或 07:00 (true)
            isInTimeRange = currentHour >= startHour || currentHour < endHour;
        }

        if (isInTimeRange)
        {
            timer += Time.deltaTime;

            float intervalSeconds = Settings.Instance.GetReminderInterval() * 3600f;
            if (timer >= intervalSeconds)
            {
                TriggerReminder();
                timer = 0f; // 重置计时器
            }
        }
        else
        {
            // 如果不在时间段内，可以重置计时器
            timer = 0f;
        }
    }

    
    void TriggerReminder()
    {
        string currentTime = DateTime.Now.ToString("HH:mm");
        Debug.Log($"<color=cyan>[喝水提醒]</color> 现在是 {currentTime}，该喝水啦！");
        if(AudioManager.Instance != null) AudioManager.Instance.PlayReminderSound();
        if(!coffee.activeSelf) coffee.SetActive(true);
        StartCountdown();
    }
    public void StartCountdown()
    {
        // 开启协程
        StartCoroutine(CloseAfterTime(300f)); 
    }

    IEnumerator CloseAfterTime(float delay)
    {
        // 暂停执行直到时间结束
        yield return new WaitForSeconds(delay);
        
        // 执行关闭逻辑
        if(coffee.activeSelf) coffee.SetActive(false);
    }
}
