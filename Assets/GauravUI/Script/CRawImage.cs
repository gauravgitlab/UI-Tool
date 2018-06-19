
using UnityEngine;
using UnityEngine.UI;

public class CRawImage : CCanvas 
{
	private readonly GameObject _canvas;
	
	public CRawImage()
	{
		_canvas = GetCanvas();	
	}

	public GameObject CreateRawImageWithDefaultSettings()
	{
		var rawImageObj = new GameObject("RawImage", typeof(RawImage)) {layer = LayerMask.NameToLayer("UI")};
		rawImageObj.transform.SetParent(_canvas.transform);
		
		// set size of button
		var rectTransform = rawImageObj.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(100, 100);
		rectTransform.anchorMin = new Vector2(0.5f, 0.5f);		
		rectTransform.anchorMax = new Vector2(0.5f, 0.5f);	
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);
		
		// this should always be in end.
		const float deltaPosX = 0f;
		const float deltaPosY = 0f;
		
		rectTransform.anchoredPosition = new Vector2 (deltaPosX, deltaPosY);

		return rawImageObj;
	}
	
}
