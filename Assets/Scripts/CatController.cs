using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    private int hitCount = 0;
    private float lastHitTime = 0;
    [SerializeField] private Animator fkAnim;
    [SerializeField] private ParticleSystem _particleSystem;
    public GameObject catObj;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (GameMain.Instance != null)
                GameMain.Instance.ReturnBall(other.gameObject); // 回收毛球
            else
                Destroy(other.gameObject);
                
            OnHit();
        }
    }

    void OnHit()
    {
        if (Time.time - lastHitTime < 2f) hitCount++;
        else hitCount = 1;
        lastHitTime = Time.time;
        
        if (hitCount >= 3)
        {
            // 触发右爪动作
            if (fkAnim != null)
                fkAnim.Play("handShow");
            hitCount = 0;
        }
        else
        {
            StartCoroutine(Shake(0.3f, 0.2f));
            // 生成生气符号 (Instantiate 粒子或 Sprite)
            if (_particleSystem != null)
                _particleSystem.Play();
            if (AudioManager.Instance != null) AudioManager.Instance.PlayHitSound();
            
        }
    }
    
    public IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPos = catObj.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            catObj.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null; // 等待下一帧
        }

        catObj.transform.localPosition = originalPos; // 恢复原位
    }


    
}
