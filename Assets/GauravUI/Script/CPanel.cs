
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CPanel : CCanvas
{
	private readonly GameObject _canvas;
	private const string BackgroundSpriteResourcePath = "UI/Skin/Background.psd";
	private static readonly Color PanelColor = new Color(1f, 1f, 1f, 0.392f);

	public CPanel()
	{
		_canvas = GetCanvas();
	}
	
	public GameObject CreatePanelWithDefaultSettings()
	{
		var panelObj = new GameObject("Panel") {layer = LayerMask.NameToLayer("UI")};
		panelObj.transform.SetParent(_canvas.transform);
		
		var rectTransform = panelObj.AddComponent<RectTransform>();
		rectTransform.sizeDelta = Vector2.zero;
		rectTransform.anchorMin = Vector2.zero;	
		rectTransform.anchorMax = Vector2.one;
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);

		// add Image Component
		var imgComp = panelObj.AddComponent<Image>();
		imgComp.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(BackgroundSpriteResourcePath);
		imgComp.color = PanelColor;
		imgComp.type = Image.Type.Sliced;
		imgComp.fillCenter = true;
			
		// this should always be in end.
		const float deltaPosX = 0.0f; 
		const float deltaPosY = 0.0f; 
		rectTransform.anchoredPosition = new Vector2 (deltaPosX, deltaPosY);

		return panelObj;
	}
}
