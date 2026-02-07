using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))] 
public class UIPosSet : MonoBehaviour
{
    [Header("跟随的3D模型目标点（模型/挂点）")]
    public Transform targetModel; // 赋值模型或模型上的挂点（如头顶空物体）
    [Header("UI在屏幕中的偏移量（像素）")]
    public Vector2 uiOffset = new Vector2(0, 50); // 向上偏移50像素，避免UI贴模型
    [Header("场景主摄像机（Overlay模式可留空）")]
    public Camera mainCamera;

    private RectTransform _uiRect; // UI的矩形变换组件

    void Awake()
    {
        // 获取UI自身的RectTransform
        _uiRect = GetComponent<RectTransform>();
        // Overlay模式自动获取主摄像机
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        if (targetModel == null || mainCamera == null)
        {
            return;
        }
        
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, targetModel.position);
        
        _uiRect.SetPositionAndRotation(screenPos + uiOffset, Quaternion.identity);
    }
}
