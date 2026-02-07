using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallLifeCycle : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    private void OnEnable()
    {
        RandomizeColor();
        StartCoroutine(AutoRecycle());
    }

    private void RandomizeColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float a = Random.Range(0f, 1f);
        SpriteRenderer.color = new Color(r,g,b,a);
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
        // 重置物理状态，防止下次取出时带有残留速度
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    IEnumerator AutoRecycle() {
        // 5秒后自动回收
        yield return new WaitForSeconds(5f);
        if (GameMain.Instance != null)
            GameMain.Instance.ReturnBall(gameObject);
        else
            Destroy(gameObject);
    }
}
