

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class CButton : CCanvas
{
	private readonly GameObject _canvas;
	private const string KStandardSpritePath = "UI/Skin/UISprite.psd";
	private static readonly Color _sDefaultSelectableColor = new Color(1f, 1f, 1f, 1f);
	
	public CButton()
	{
		_canvas = GetCanvas();	
	}
	
	public GameObject CreateButtonWithDefaultSettings()
	{
		var buttonObj = new GameObject("Button", typeof(Button)) {layer = LayerMask.NameToLayer("UI")};
		buttonObj.transform.SetParent(_canvas.transform);
		
		// set size of button
		var rectTransform = buttonObj.AddComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(160, 30);
		rectTransform.anchorMin = new Vector2(0.5f, 0.5f);		
		rectTransform.anchorMax = new Vector2(0.5f, 0.5f);	
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);
		
		// TODO : Use CImage
		var imgComp = buttonObj.AddComponent<Image>();
		imgComp.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(KStandardSpritePath);
		imgComp.color = _sDefaultSelectableColor;
		imgComp.type = Image.Type.Sliced;
		imgComp.fillCenter = true;
		
		buttonObj.GetComponent<Button> ().targetGraphic = imgComp;
		
		// this should always be in end.
		const float deltaPosX = 0f;
		const float deltaPosY = 0f;
		
		rectTransform.anchoredPosition = new Vector2 (deltaPosX, deltaPosY);
		
		return buttonObj;
	}
	
	
	public GameObject CreateButtonWithoutText(GameObject parentCanvas, UiInformation information, Sprite sprite)
	{
		var buttonObj = new GameObject("Button", typeof(Button)) {layer = LayerMask.NameToLayer("UI")};
		buttonObj.transform.SetParent(parentCanvas.transform);
		
		// set size of button
		var rectTransform = buttonObj.AddComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2 (information.Width, information.Height);
		rectTransform.anchorMin = information.AnchorMin;		
		rectTransform.anchorMax = information.AnchorMax;		
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);
		
		// TODO : Use CImage
		var imgComp = buttonObj.AddComponent<Image>();
		imgComp.sprite = sprite;
		imgComp.color = _sDefaultSelectableColor;
		imgComp.type = Image.Type.Simple;
		
		buttonObj.GetComponent<Button> ().targetGraphic = imgComp;
		
		// this should always be in end.
		var deltaPosX = information.DeltaPosFromAnchor.x; 
		var deltaPosY = information.DeltaPosFromAnchor.y;
		
		rectTransform.anchoredPosition = new Vector2 (deltaPosX, deltaPosY);

		return buttonObj;
	}
}
