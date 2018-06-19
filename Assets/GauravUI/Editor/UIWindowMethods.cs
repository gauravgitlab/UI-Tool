using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

public partial class UIWindow
{
	private List<TextAsset> _textFiles = new List<TextAsset>();
	public List<Texture2D> _spriteAtlases = new List<Texture2D>();
	private List<Sprite> _spriteList = new List<Sprite>();
	private readonly List<string> _texExtensions = new List<string> { ".png", ".jpg", ".jpeg", ".psd", ".tiff", ".dds" };
	private string _textureFolderPath = string.Empty;
	
	private void CreateCanvas()
	{
		var canvas = new CCanvas();
		canvas.CreateCanvasWithDefaultSettings();
	}

	private bool IsEventSystemExist()
	{
		var eventSystem = FindObjectOfType<EventSystem>();
		return eventSystem != null;
	}
	
	
	private void CreateEventSystem()
	{
		var eventSystem = new CEventSystem();
		eventSystem.CreateEventSystem();
	}
	
	
	private void CreatePanel()
	{
		var panel = new CPanel();
		panel.CreatePanelWithDefaultSettings();
	}
	
	private void CreateButton()
	{
		var button = new CButton();
		var buttonObj = button.CreateButtonWithDefaultSettings();
		
		var text = new CText();
		text.Alignment = TextAnchor.MiddleCenter;
		text.CreateTextWithDefaultSettings(buttonObj.transform);
	}
	
	
	private void CreateButtonWithoutText()
	{
		var button = new CButton();
		button.CreateButtonWithDefaultSettings();
	}
	

	private void CreateText()
	{
		var text = new CText();
		text.CreateTextWithDefaultSettings();
	}
	

	private void CreateImage()
	{
		var imageObj = new CImage();
		imageObj.CreateImageWithDefaultSettings();
	}
	

	private void CreateRawImage()
	{
		var rawImageObj = new CRawImage();
		rawImageObj.CreateRawImageWithDefaultSettings();
	}
	
	private void CreateSpriteListFromDirectorySearch(string dir)
	{
		try
		{
			foreach (var file in Directory.GetFiles(dir))
			{
				if(!_texExtensions.Contains(Path.GetExtension(file)))
					continue;
				
				var filePath = file.Replace(Application.dataPath, "Assets");
				var sprite = (Sprite)AssetDatabase.LoadAssetAtPath(filePath, typeof(Sprite));
				_spriteList.Add(sprite);
			}
			foreach (var directory in Directory.GetDirectories(dir)){
				CreateSpriteListFromDirectorySearch(directory);
			}

		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	
}
