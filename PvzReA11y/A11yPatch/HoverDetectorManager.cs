using UnityEngine;
using UnityEngine.UI;
using MelonLoader;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// 悬停检测管理器
/// 自动为所有可交互的 UI 元素添加悬停检测组件
/// </summary>
public class HoverDetectorManager
{
    private static readonly HashSet<GameObject> _processedObjects = new HashSet<GameObject>();
    private static bool _isInitialized = false;

    /// <summary>
    /// 初始化管理器
    /// </summary>
    public static void Initialize()
    {
        try
        {
            _isInitialized = true;
            MelonLogger.Msg("HoverDetectorManager initialized successfully");
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"HoverDetectorManager initialization failed: {ex.Message}");
        }
    }

    /// <summary>
    /// 每帧更新，查找新的 UI 元素并添加检测组件
    /// </summary>
    public static void Update()
    {
        if (!_isInitialized) return;

        try
        {
            // 查找所有 Selectable 组件
            var selectables = UnityEngine.Object.FindObjectsOfType<Selectable>();
            
            foreach (var selectable in selectables)
            {
                if (selectable == null || selectable.gameObject == null) continue;
                
                // 跳过已处理的对象
                if (_processedObjects.Contains(selectable.gameObject)) continue;
                
                // 跳过不可交互的对象
                if (!selectable.interactable) continue;
                
                // 添加悬停检测组件
                AddHoverDetector(selectable.gameObject);
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"HoverDetectorManager.Update error: {ex.Message}");
        }
    }

    /// <summary>
    /// 为指定的游戏对象添加悬停检测组件
    /// </summary>
    /// <param name="gameObject">游戏对象</param>
    private static void AddHoverDetector(GameObject gameObject)
    {
        try
        {
            // 检查是否已经有悬停检测组件 - 使用非泛型方法避免Il2Cpp问题
            var existingComponent = gameObject.GetComponent<HoverDetectorComponent>();
            if (existingComponent != null)
            {
                _processedObjects.Add(gameObject);
                return;
            }

            // 添加悬停检测组件 - 使用非泛型方法
            var hoverDetector = gameObject.AddComponent<HoverDetectorComponent>();
            _processedObjects.Add(gameObject);

            MelonLogger.Msg($"Added HoverDetector to: {gameObject.name}");
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"AddHoverDetector error for {gameObject?.name}: {ex.Message}");
        }
    }

    /// <summary>
    /// 清理已销毁的对象引用
    /// </summary>
    public static void CleanupDestroyedObjects()
    {
        try
        {
            var objectsToRemove = new List<GameObject>();
            
            foreach (var obj in _processedObjects)
            {
                if (obj == null)
                {
                    objectsToRemove.Add(obj);
                }
            }

            foreach (var obj in objectsToRemove)
            {
                _processedObjects.Remove(obj);
            }

            if (objectsToRemove.Count > 0)
            {
                //MelonLogger.Msg($"Cleaned up {objectsToRemove.Count} destroyed object references");
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"CleanupDestroyedObjects error: {ex.Message}");
        }
    }

    /// <summary>
    /// 手动为特定对象添加悬停检测
    /// </summary>
    /// <param name="gameObject">游戏对象</param>
    public static void AddHoverDetectorManually(GameObject gameObject)
    {
        if (gameObject == null) return;
        
        AddHoverDetector(gameObject);
    }
}