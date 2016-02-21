using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerColorizer : MonoBehaviour {

	public List<Texture2D> textures;

	public Color oldcolor;

	void Start()
	{
		ColorizeAll(Color.green);
	}

	public void ColorizeAll(Color color)
	{
		foreach(Texture2D tex in textures)
		{
			Colorize(color, tex);
		}

	}

	public void Colorize(Color newcolor, Texture2D texture)
	{		
		int y = 0;
		while (y < texture.height) {
			int x = 0;
			while (x < texture.width) {
				if(texture.GetPixel(x,y) == oldcolor)
					texture.SetPixel(x, y, newcolor);
				++x;
			}
			++y;
		}
		texture.Apply();
	}
}
