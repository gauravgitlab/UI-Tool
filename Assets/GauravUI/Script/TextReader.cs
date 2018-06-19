
using UnityEngine;
using System.Collections.Generic;

public static class TextReader
{

	/// <summary>
	/// Reads the text file.
	/// </summary>
	/// <param name="textAsset">Text asset.</param>
	public static List<UiInformation> ReadTextFile(TextAsset textAsset)
	{
		var informationList = new List<UiInformation> ();

		var dataLines = textAsset.text.Split ('\n');
		for (var index = 0; index < dataLines.Length; index++)
		{
			var line = dataLines[index];
			if (line.Contains("#") || string.IsNullOrEmpty(line))
				continue;

			var lineSplits = line.Split(',');

			// get the anchor and name of object from one string
			var anchorName = lineSplits[0].Split('_');
			var anchor = GetAnchorFromString(anchorName[0].Trim());
			var name = anchorName[1].Trim();
			var uiType = GetUiTypeFromString(anchorName[2].Trim());

			var left = float.Parse(lineSplits[1].Trim());
			var top = float.Parse(lineSplits[2].Trim());
			var right = float.Parse(lineSplits[3].Trim());
			var bottom = float.Parse(lineSplits[4].Trim());

			//Debug.Log(anchor + " " + name + " " + uiType + " " + left + " " + top + " " + right + " " + bottom); 
			var information = new UiInformation(anchor, name, uiType, left, top, right, bottom);
			informationList.Add(information);
		}

		return informationList;
	}


	/// <summary>
	/// Gets the anchor from string.
	/// </summary>
	/// <returns>The anchor from string.</returns>
	/// <param name="anchorName">Anchor name.</param>
	private static eANCHOR GetAnchorFromString(string anchorName)
	{
		switch (anchorName.ToLower ()) 
		{
		case "bl":
			return eANCHOR.BottomLeft;
		case "bc":
			return eANCHOR.BottomCenter;
		case "br":
			return eANCHOR.BottomRight;
		case "cl":
			return eANCHOR.CenterLeft;
		case "cc":
			return eANCHOR.Center;
		case "cr":
			return eANCHOR.CenterRight;
		case "tl":
			return eANCHOR.TopLeft;
		case "tc":
			return eANCHOR.TopCenter;
		case "tr":
			return eANCHOR.TopRight;
		default:
			return eANCHOR.Center;
		}
	}



	/// <summary>
	/// Gets the user interface type from string.
	/// </summary>
	/// <returns>The user interface type from string.</returns>
	/// <param name="typeName">Type name.</param>
	private static eUIType GetUiTypeFromString(string typeName)
	{
		switch(typeName.ToLower())
		{
		case "button":
			return eUIType.Button;
		case "text":
			return eUIType.Text;
		case "buttonwithtext":
			return eUIType.ButtonWithText;
		default:
			return eUIType.Button;
		}
	}


}
