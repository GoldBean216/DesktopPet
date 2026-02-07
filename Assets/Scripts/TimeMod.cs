using TMPro;
using UnityEngine;
using System;

public class TimeMod : MonoBehaviour
{
       private TextMeshProUGUI tmp;
       private int hour;
       private int minute;
      
   
       private void Awake()
       {
           tmp = GetComponent<TextMeshProUGUI>();
       }
   
       private void Update()
       {
           hour = DateTime.Now.Hour;
           minute = DateTime.Now.Minute;
       
   
           tmp.text = hour.ToString("D2") + ":" + minute.ToString("D2");
   
       }
}
