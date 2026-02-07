using UnityEngine;

public class DragSprite : MonoBehaviour
{
    [Tooltip("限制在屏幕范围内")]
    public bool confineTranslation = true;
    public Camera currentCamera;

    private bool isDragging = false;
    private Vector3 offset;
    private Vector2 screenBounds;

    void Start()
    {
        if (!currentCamera) currentCamera = Camera.main;
        
        // 预计算屏幕边界（世界坐标）
        UpdateScreenBounds();
    }

    void Update()
    {
        if (GameMain.Instance.Mode != PlayMode.None) return;
        HandleInput();
        if (isDragging)
        {
            PerformDrag();
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 发射射线检测
            Vector3 mouseWorldPos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null &&hit.transform.name == "Sprite Shape Profile")
            {
                isDragging = true;
                // 计算点击点与物体中心的偏移，防止物体“瞬移”到鼠标中心
                offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void PerformDrag()
    {
        Vector3 mouseWorldPos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = mouseWorldPos + offset;

        if (confineTranslation)
        {
            targetPos = ClampToScreen(targetPos);
        }

        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
    }

    private Vector3 ClampToScreen(Vector3 pos)
    {
        // 简单的屏幕空间限制逻辑
        Vector3 screenPos = currentCamera.WorldToViewportPoint(pos);
        screenPos.x = Mathf.Clamp01(screenPos.x);
        screenPos.y = Mathf.Clamp01(screenPos.y);
        return currentCamera.ViewportToWorldPoint(screenPos);
    }

    private void UpdateScreenBounds() 
    {
        // 如果需要更精确的边界限制（考虑Sprite大小），在此扩展
    }
}