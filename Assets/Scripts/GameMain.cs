using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public enum PlayMode
{
    None,
    Ball,
    Petting,
}

public class GameMain : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform catTarget; // 赋值为猫的坐标
    public GameObject tips;
    public ParticleSystem _loveParticleSystem;
    public float gravity = 9.81f; // 对应代码里的 gravity
    private float lastPetTime;
    private PlayMode _mode = PlayMode.None;
    public GameObject btns;
    private GameObject _currentHoverObj; // 当前悬停的物体（用于状态判断）
    [Header("球生成频率控制")]
    [Tooltip("两次生成球的最小间隔（秒），值越小生成越快")]
    public float launchCoolDown = 0.5f;
    private float _coolDownTimer; // 冷却计时器
    
    // 对象池
    private Queue<GameObject> _ballPool = new Queue<GameObject>();

    public PlayMode Mode
    {
        get { return _mode; }
    }
    
    public static GameMain Instance;

    private void Awake()
    {
        Instance = this;
       
    }

    public GameObject GetBall(Vector2 position)
    {
        GameObject ball;
        if (_ballPool.Count > 0)
        {
            ball = _ballPool.Dequeue();
            ball.SetActive(true);
            ball.transform.position = position;
            ball.transform.rotation = Quaternion.identity;
        }
        else
        {
            ball = Instantiate(ballPrefab, position, Quaternion.identity);
        }
        return ball;
    }

    public void ReturnBall(GameObject ball)
    {
        if (ball == null) return;
        ball.SetActive(false);
        _ballPool.Enqueue(ball);
    }

    void Update()
    {
        if (_mode == PlayMode.Ball)
        {
            _coolDownTimer += Time.deltaTime; // 冷却计时
            // 可选：鼠标在UGUI上时，屏蔽生成（避免点UI按钮误生成球）
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0)&& _coolDownTimer >= launchCoolDown) {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                LaunchBall(mousePos);
                _coolDownTimer = 0f; // 重置冷却
            }
        }else if (_mode == PlayMode.Petting)
        {
            // 适配鼠标和触摸屏
            if (Input.GetMouseButton(0)) 
            {
                HandlePetting();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopPetting();
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero);
            
            if (hit.collider == null) {
                Debug.Log("完全没有击中任何物体");
                return;
            }

            if (hit.transform.name == "Sprite Shape Profile")
            {
                if(!btns.activeSelf)
                    btns.SetActive(true);
            }
        }
    }

    void LaunchBall(Vector2 startPos) {
        GameObject ball = GetBall(startPos);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // 1. 计算位移
        Vector2 targetPos = (Vector2)catTarget.position + Random.insideUnitCircle * 0.5f;
        float dx = targetPos.x - startPos.x;
        float dy = targetPos.y - startPos.y;

        // 2. 设定飞行时间 (对应代码中的 duration)
        float distance = Vector2.Distance(startPos, targetPos);
        float duration = 0.5f + (distance * 0.05f); 

        // 3. 物理公式计算初速度 (对应代码中的弹道公式)
        float vx = dx / duration;
        float vy = (dy / duration) + (0.5f * rb.gravityScale * Physics2D.gravity.magnitude * duration);

        rb.velocity = new Vector2(vx, vy);
    }
    
    void HandlePetting()
    {
        // 将屏幕坐标转换为 2D 物理世界的坐标
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // 如果点到了猫咪的 Collider
        if (hit.collider != null && (hit.collider.tag=="cat"))
        {
            // 限制触发频率，防止每帧都生成
            if (Time.time - lastPetTime > 0.2f)
            {
                TriggerHappyEffects(mousePos);
                lastPetTime = Time.time;
            }
        }
    }

    void StopPetting()
    {
        _loveParticleSystem.Stop();
        if (AudioManager.Instance != null) AudioManager.Instance.StopLoop();
    }
    void TriggerHappyEffects(Vector2 pos)
    {
        _loveParticleSystem.Play();

        // 2. 播放声音
        if (AudioManager.Instance != null) AudioManager.Instance.PlayPurr();
        
    }

    public void PlayBall()
    {
        bool isSkip=PlayerPrefs.GetInt("TipsSkip", 0) == 1;
        if(isSkip)
            SwitchMode(1);
        else
        {
            if(tips!=null&&!tips.activeSelf)
               tips.gameObject.SetActive(true);
        }
    }

    public void SwitchMode(int mode)
    {
        if (mode >2||mode<0) return;
        if (mode == 1)
            _mode = PlayMode.Ball;
        else if (mode == 2)
            _mode = PlayMode.Petting;
        else if(mode==0)
            _mode = PlayMode.None;
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
