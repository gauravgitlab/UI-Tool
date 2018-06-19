
using UnityEngine;
using UnityEngine.UI;

public class CImage : CCanvas 
{
	private readonly GameObject _canvas;
	private Sprite _sprite;
	private Color _spriteColor;
	private Image.Type _imageType;
	private bool _fillCenter;
	
	public Sprite ImageSprite
	{
		get { return _sprite; }
		set { _sprite = value;  }
	}

	public Color SpriteColor
	{
		get { return _spriteColor;  }
		set { _spriteColor = value; }
	}

	public Image.Type ImageType
	{
		get { return _imageType; }
		set { _imageType = value;  }
	}

	public bool FillCenter
	{
		get { return _fillCenter; }
		set { _fillCenter = value; }
	}
	
	public CImage()
	{
		_canvas = GetCanvas();	
	}

	public GameObject CreateImageWithDefaultSettings()
	{
		var imageObj = new GameObject("Image", typeof(Image)) {layer = LayerMask.NameToLayer("UI")};
		imageObj.transform.SetParent(_canvas.transform);
		
		// set size of button
		var rectTransform = imageObj.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(100, 100);
		rectTransform.anchorMin = new Vector2(0.5f, 0.5f);		
		rectTransform.anchorMax = new Vector2(0.5f, 0.5f);	
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);
		
		// this should always be in end.
		const float deltaPosX = 0f;
		const float deltaPosY = 0f;
		
		rectTransform.anchoredPosition = new Vector2 (deltaPosX, deltaPosY);

		return imageObj;
	}
	
}
