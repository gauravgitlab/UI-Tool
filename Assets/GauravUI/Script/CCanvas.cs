
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CCanvas
{
    public GameObject CreateCanvasWithDefaultSettings()
    {
        var canvasObj = new GameObject("Canvas", typeof(Canvas)) {layer = LayerMask.NameToLayer("UI")};

        var canvas = canvasObj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		
        var canvasScaler = canvasObj.AddComponent<CanvasScaler>();
        canvasScaler.scaleFactor = 1.0f;
        canvasScaler.referencePixelsPerUnit = 100;
        
        canvasObj.AddComponent<GraphicRaycaster>();
		
        return canvasObj;
    }
    
    public GameObject CreateCanvasWithScreenSpaceOverlay()
    {
        var canvasObj = new GameObject("Canvas", typeof(Canvas)) {layer = LayerMask.NameToLayer("UI")};
        var canvas = canvasObj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		
        var canvasScaler = canvasObj.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2 (640, 960);

        canvasObj.AddComponent<GraphicRaycaster>();
		
        return canvasObj;
    }
    
    public GameObject GetCanvas()
    {
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Canvas>())
            return Selection.activeGameObject;
		
        var canvas = Object.FindObjectOfType<Canvas>();
        return canvas == null ? CreateCanvasWithDefaultSettings() : canvas.gameObject;
    }
}
