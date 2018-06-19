
using UnityEngine;
using UnityEngine.UI;

public class CText : CCanvas
{
	private readonly GameObject _canvas;
	private TextAnchor _alignment = TextAnchor.UpperLeft;

	public CText()
	{
		_canvas = GetCanvas();	
	}

	public TextAnchor Alignment
	{
		get { return _alignment; }
		set { _alignment = value; }
	}

	public GameObject CreateTextWithDefaultSettings(Transform parent = null)
	{
		var textObj = new GameObject("Text", typeof(Text)) {layer = LayerMask.NameToLayer("UI")};
		textObj.transform.SetParent(parent ?? _canvas.transform);

		// set anchor and pivot
		var rectTransform = textObj.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(160, 30);
		rectTransform.anchorMin = new Vector2(0.5f, 0.5f);		
		rectTransform.anchorMax = new Vector2(0.5f, 0.5f);		
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);

		var text = textObj.GetComponent<Text>();
		text.text = "New Text";
		text.alignment = _alignment;
		text.color = Color.black;

		// this should always be in end.
		const float deltaPosX = 0f;
		const float deltaPosY = 0f;
		
		rectTransform.anchoredPosition = new Vector2 (deltaPosX, deltaPosY);

		return textObj;
	}
	

	public void CreateText(GameObject panelCanvas, UiInformation information)
	{
		var textObj = new GameObject("Text", typeof(Text)) {layer = LayerMask.NameToLayer("UI")};
		textObj.transform.SetParent(panelCanvas.transform);
		
		// set anchor and pivot
		var rectTransform = textObj.GetComponent<RectTransform>();
		rectTransform.anchorMin = information.AnchorMin;
		rectTransform.anchorMax = information.AnchorMax;		
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);

		textObj.GetComponent<Text> ().text = information.Name;
		textObj.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;

		// this should always be in end.
		var deltaPosX = information.DeltaPosFromAnchor.x;
		var deltaPosY = information.DeltaPosFromAnchor.y; 
		
		rectTransform.anchoredPosition = new Vector2 (deltaPosX, deltaPosY);
	}
}
