

using System;
using UnityEngine;


public enum eANCHOR { TopLeft, TopCenter, TopRight,
					  CenterLeft, Center, CenterRight,
					  BottomLeft, BottomCenter, BottomRight	}

public enum eUIType { Button, Text, ButtonWithText }

public class UiInformation
{
	private const float ScreenWidth = 640;
	private const float ScreenHeight = 960;

	private readonly float _left;
	private readonly float _top;
	private readonly float _right;
	private readonly float _bottom;

	private Vector2 _deltaPosFromAnchor;

	private void SetDeltaPosFromAnchor()
	{
		switch(Anchor)
    	{
		case eANCHOR.BottomLeft:
			_deltaPosFromAnchor.x = _left + Width * 0.5f;
			_deltaPosFromAnchor.y = ScreenHeight - _bottom + Height * 0.5f;
			break;
		case eANCHOR.BottomCenter:
			_deltaPosFromAnchor.x = (_left + _right - ScreenWidth) / 2;
			_deltaPosFromAnchor.y = ScreenHeight - _bottom + Height * 0.5f;
			break;
		case eANCHOR.BottomRight:
			_deltaPosFromAnchor.x = -(Width * 0.5f + (ScreenWidth - _right));
			_deltaPosFromAnchor.y = ScreenHeight - _bottom + Height * 0.5f;
			break;
		
				
		case eANCHOR.CenterLeft:
			_deltaPosFromAnchor.x = _left + Width * 0.5f;
			_deltaPosFromAnchor.y = (ScreenHeight - (_top + _bottom)) / 2;
			break;
		case eANCHOR.Center:
			_deltaPosFromAnchor.x = (_left + _right - ScreenWidth) / 2;
			_deltaPosFromAnchor.y = (ScreenHeight - (_top + _bottom)) / 2;
			break;
		case eANCHOR.CenterRight:
			_deltaPosFromAnchor.x = -(Width * 0.5f + (ScreenWidth - _right));
			_deltaPosFromAnchor.y = (ScreenHeight - (_top + _bottom)) / 2;
			break;


		case eANCHOR.TopLeft:
			_deltaPosFromAnchor.x = _right - Width * 0.5f;
			_deltaPosFromAnchor.y = Height * 0.5f - _bottom;
			break;
		case eANCHOR.TopCenter:
			_deltaPosFromAnchor.x = (_left + _right - ScreenWidth) / 2;
			_deltaPosFromAnchor.y = Height * 0.5f - _bottom;
			break;
		case eANCHOR.TopRight:
			_deltaPosFromAnchor.x = -(Width * 0.5f + (ScreenWidth - _right));
			_deltaPosFromAnchor.y = Height * 0.5f - _bottom;
			break;


		    default:
			    throw new ArgumentOutOfRangeException();
	    }
	}




	private void SetAnchor()
	{
		switch(Anchor)
		{
		case eANCHOR.TopLeft:
			AnchorMin = new Vector2(0, 1);
			AnchorMax = new Vector2(0, 1);
			break;
		case eANCHOR.TopCenter:
			AnchorMin = new Vector2(0.5f, 1);
			AnchorMax = new Vector2(0.5f, 1);
			break;
		case eANCHOR.TopRight:
			AnchorMin = new Vector2(1, 1);
			AnchorMax = new Vector2(1, 1);
			break;
		case eANCHOR.CenterLeft:
			AnchorMin = new Vector2(0, 0.5f);
			AnchorMax = new Vector2(0, 0.5f);
			break;
		case eANCHOR.Center:
			AnchorMin = new Vector2(0.5f, 0.5f);
			AnchorMax = new Vector2(0.5f, 0.5f);
			break;
		case eANCHOR.CenterRight:
			AnchorMin = new Vector2(1, 0.5f);
			AnchorMax = new Vector2(1, 0.5f);
			break;
		case eANCHOR.BottomLeft:
			AnchorMin = new Vector2(0, 0);
			AnchorMax = new Vector2(0, 0);
			break;
		case eANCHOR.BottomCenter:
			AnchorMin = new Vector2(0.5f, 0);
			AnchorMax = new Vector2(0.5f, 0);
			break;
		case eANCHOR.BottomRight:
			AnchorMin = new Vector2(1, 0);
			AnchorMax = new Vector2(1, 0);
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}



	public UiInformation(eANCHOR anchor, string name, eUIType type, float left, float top, float right, float bottom)
	{
		Anchor = anchor;
		Name = name;
		UIType = type;

		_left = left;
		_top = top;
		_right = right;
		_bottom = bottom;

		// calculate width and height
		Width = right - left;
		Height = bottom - top;

		// calcualte min and max anchor
		SetAnchor ();

		// set delta position from anchor
		SetDeltaPosFromAnchor ();
	}



	public float Width { get; }

	public float Height { get; }

	public eANCHOR Anchor { get; }

	public eUIType UIType { get; }

	public string Name { get; }

	public Vector2 AnchorMin { get; private set; }

	public Vector2 AnchorMax { get; private set; }

	public Vector2 DeltaPosFromAnchor
	{
		get { return _deltaPosFromAnchor; }
	}
}



