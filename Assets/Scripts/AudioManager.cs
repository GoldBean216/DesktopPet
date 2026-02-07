using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;   // 用于播放短音效（如击中、提醒）
    public AudioSource loopSource;  // 用于播放循环音效（如咕噜声）

    [Header("Audio Clips")]
    public AudioClip purrClip;      // 咕噜声
    public AudioClip hitClip;       // 击中声
    public AudioClip reminderClip;  // 喝水提醒声

    private bool _isSFXOn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // 防御性编程：如果未手动赋值，尝试自动获取或添加组件
        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
        if (loopSource == null) loopSource = gameObject.AddComponent<AudioSource>();

        // 初始化音效开关状态（从 Settings 或 PlayerPrefs 读取）
        _isSFXOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        UpdateMuteState();
    }

    public void SetSFXOn(bool isOn)
    {
        _isSFXOn = isOn;
        UpdateMuteState();
        
        // 同步保存到 Settings (或者由 Settings 调用此方法)
        if (Settings.Instance != null)
        {
            Settings.Instance.SetSFXOn(isOn);
            Settings.Instance.SaveSettings();
        }
    }
    
    public bool GetSFXOn() => _isSFXOn;

    private void UpdateMuteState()
    {
        if (sfxSource != null) sfxSource.mute = !_isSFXOn;
        if (loopSource != null) loopSource.mute = !_isSFXOn;
    }

    /// <summary>
    /// 播放一次性音效
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        if (!_isSFXOn || clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }
    
    public void PlayHitSound() => PlaySFX(hitClip);
    public void PlayReminderSound() => PlaySFX(reminderClip);

    /// <summary>
    /// 播放循环音效（如咕噜声）
    /// </summary>
    public void PlayLoop(AudioClip clip)
    {
        if (!_isSFXOn || clip == null || loopSource == null) return;
        
        if (loopSource.clip != clip || !loopSource.isPlaying)
        {
            loopSource.clip = clip;
            loopSource.loop = true;
            loopSource.Play();
        }
    }
    
    public void PlayPurr() => PlayLoop(purrClip);

    public void StopLoop()
    {
        if (loopSource != null) loopSource.Stop();
    }
}
