
using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Object = UnityEngine.Object;

public partial class UIWindow : EditorWindow
{
	[MenuItem("GauravUI/UI")]
	public static void InitWindow()
	{
		var window = GetWindow (typeof(UIWindow)) as UIWindow;
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal ();

		// Create event system
		if (GUILayout.Button ("Event System")) 
		{
			if(!IsEventSystemExist())
				CreateEventSystem();
			else
				EditorUtility.DisplayDialog("Event System Already Exist", "UI Tool Cant Generate Multiple EventSystem", "OK");
		}


		// Create Screen Space Overlay Canvas
		if (GUILayout.Button ("Canvas"))  
		{
			// Create canvas
			CreateCanvas();

			// Add EventSystem, if doesnot exist
			if(!IsEventSystemExist())
				CreateEventSystem();
		}


		// Create Panel
		if (GUILayout.Button ("Panel"))  
		{
			CreatePanel();
			
			if(!IsEventSystemExist())
				CreateEventSystem();
		}

		// Create Text
		if (GUILayout.Button ("Text"))  
		{	
			CreateText();
			
			if(!IsEventSystemExist())
				CreateEventSystem();
		}

		EditorGUILayout.EndHorizontal ();


		EditorGUILayout.BeginHorizontal ();
	
		// Create button
		if (GUILayout.Button ("Button With Text")) 
		{
			CreateButton();
			
			if(!IsEventSystemExist())
				CreateEventSystem();
		}


		// Create button without text
		if (GUILayout.Button ("Button Without Text")) 
		{
			CreateButtonWithoutText();
			
			if(!IsEventSystemExist())
				CreateEventSystem();
			
		}


		// Create Image
		if (GUILayout.Button ("Image")) 
		{
			CreateImage();
			
			if(!IsEventSystemExist())
				CreateEventSystem();
		}


		// Create raw Image
		if (GUILayout.Button ("Raw Image")) 
		{
			CreateRawImage();
			
			if(!IsEventSystemExist())
				CreateEventSystem();
		}

		EditorGUILayout.EndHorizontal ();


		// Space area
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		//========================================================================================
		//========================================================================================
		EditorGUILayout.BeginVertical ();

		// Begin Horizontal To add Image realted buttons
		EditorGUILayout.LabelField("Add Photoshop Text files.");
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("-")) 
		{
			if (_textFiles.Count > 0)
				_textFiles.RemoveAt (_textFiles.Count - 1);
		}
		if (GUILayout.Button ("Clear")) 
		{
			_textFiles = new List<TextAsset>();
		}
		if (GUILayout.Button ("+")) 
		{
			_textFiles.Add(new TextAsset());
		}

		EditorGUILayout.EndHorizontal ();

		for(var i=0; i<_textFiles.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();
			_textFiles[i] = (TextAsset)EditorGUILayout.ObjectField("Photoshop text file : ", _textFiles[i], typeof(TextAsset), false);

			if (GUILayout.Button ("-", GUILayout.MaxWidth(50))) 
				_textFiles.Remove(_textFiles[i]);

			EditorGUILayout.EndHorizontal();
		}
		
		//========================================================================================
		//========================================================================================
		
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		
		
		EditorGUILayout.LabelField("Add Image Folder");
		EditorGUILayout.BeginHorizontal ();
		
		if (GUILayout.Button("+", GUILayout.MaxWidth(50)))
		{
			_textureFolderPath = EditorUtility.OpenFolderPanel("Load Textures", "Images", "");
			if (!_textureFolderPath.Contains(Application.dataPath))
			{
				EditorUtility.DisplayDialog("Folder Warning", "Folder Must Exist in Assets", "OK");
				return;
			}
				
			_spriteList = new List<Sprite>();
			if(!string.IsNullOrEmpty(_textureFolderPath))
			{
				// get directories and load images
				CreateSpriteListFromDirectorySearch(_textureFolderPath);
				Debug.Log(_spriteList.Count);
			}
		}
		EditorGUILayout.LabelField(_textureFolderPath);
		EditorGUILayout.EndHorizontal();
		
		//========================================================================================
		//========================================================================================
		
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		if (_textFiles.Count > 0 && _spriteList.Count > 0)
		{
			if (GUILayout.Button("Create Menu", GUILayout.ExpandWidth(false)))
			{
				var sprite = new Sprite();

				// create canvas 
				var canvasObj = new CCanvas();
				canvasObj.GetCanvas();

				// create event system
				if (!IsEventSystemExist())
					CreateEventSystem();

				foreach (var textFile in _textFiles)
				{
					if (textFile == null)
						continue;

					var informationList = new List<UiInformation>();
					informationList = TextReader.ReadTextFile(textFile);

					// Create Panel
					var panel = new CPanel();
					var panelObj = panel.CreatePanelWithDefaultSettings();

					// Create UI
					foreach (var uiInfo in informationList)
					{
						switch (uiInfo.UIType)
						{
							case eUIType.Button:
								foreach (var spriteImage in _spriteList)
								{
									if (spriteImage.name.Equals(uiInfo.Name))
									{
										sprite = spriteImage;
										break;
									}
								}

								// create button
								var button = new CButton();
								button.CreateButtonWithoutText(panelObj, uiInfo, sprite);
								break;
								
							case eUIType.Text:
								// create text
								var text = new CText();
								text.CreateText(panelObj, uiInfo);
								break;
								
							case eUIType.ButtonWithText:
								Debug.Log("Create button with text");
								foreach (var spriteImage in _spriteList)
								{
									if (spriteImage.name.Equals(uiInfo.Name))
									{
										sprite = spriteImage;
										break;
									}
								}

								// create button
								var btnWithText = new CButton();
								var btnObj = btnWithText.CreateButtonWithoutText(panelObj, uiInfo, sprite);
								
								// create text
								var txt = new CText();
								txt.CreateText(btnObj, uiInfo);
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
				}
			}
		}

		// End vertical layout in the end
		EditorGUILayout.EndVertical ();
	}
}



// Unused code
/*

// drag texture in object field
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		
		
		EditorGUILayout.LabelField("Add Sprite Atlases.");
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("-")) 
		{
			if (_spriteAtlases.Count > 0)
				_spriteAtlases.RemoveAt (_spriteAtlases.Count - 1);
		}
		if (GUILayout.Button ("Clear")) 
		{
			_spriteAtlases = new List<Texture2D>();
		}
		if (GUILayout.Button ("+")) 
		{
			Texture2D tex = null;
			_spriteAtlases.Add(tex);
		}

		EditorGUILayout.EndHorizontal ();

		for (var i=0; i<_spriteAtlases.Count; i++) 
		{
			EditorGUILayout.BeginHorizontal();
			_spriteAtlases [i] = (Texture2D)EditorGUILayout.ObjectField ("Texture : ", _spriteAtlases [i], typeof(Texture2D), false);

			if (GUILayout.Button ("-", GUILayout.MaxWidth(50))) 
				_spriteAtlases.Remove(_spriteAtlases[i]);

			EditorGUILayout.EndHorizontal();
		}


		// get an sprite array from the texture
		if (_spriteAtlases.Count > 0) 
		{
			if (GUILayout.Button ("Make Sprites")) 
			{
				_spriteList = new List<Object>(); 
				for(var i=0; i<_spriteAtlases.Count; i++)
				{
					if (_spriteAtlases[i] == null) continue;
					var path = AssetDatabase.GetAssetPath(_spriteAtlases[i]);
					var spriteArray = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);
					
					for(var j=0; j<spriteArray.Length; j++)
					{
						_spriteList.Add(spriteArray[j]);
					}
				}
			}
		}



		// if text file exist, show the button to create Menu
		if(_textFiles.Count > 0)
		{
			if(GUILayout.Button("Create Menu", GUILayout.ExpandWidth(false))) 
			{
				var sprite = new Sprite();

				// create canvas 
				var canvasObj = GameObject.Find("Canvas") ?? CreateCanvas ();

				// create event system
				CreateEventSystem();

				for(var i=0; i<_textFiles.Count; i++)
				{
					if(_textFiles[i] == null)
						continue;

					var informationList = new List<UiInformation> ();
					informationList = TextReader.ReadTextFile(_textFiles[i]);

					// Create Panel
					var panel = new CPanel();
					var panelObj = panel.CreatePanelWithDefaultSettings();


					for(var j=0; j<informationList.Count; j++)
					{
						if(informationList[j].UIType == eUIType.Button)
						{
							// get sprite from texture
							// get sprite from sprite array
							for(var k=0; k<_spriteList.Count; k++)
							{
								if(_spriteList[k].name.Equals(informationList[j].Name))
								{
									sprite = _spriteList[k] as Sprite;
									break;
								}
							}

							// create button
							var button = new CButton();
							//button.CreateButtonWithoutText(panelObj, informationList[j], sprite);
						}
						else if(informationList[j].UIType == eUIType.Text)
						{
							// create text
							var text = new CText();
							//text.CreateText(informationList[j]);
						}
					}
				}
			}
		}
		*/








