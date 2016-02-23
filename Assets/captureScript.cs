using UnityEngine;
using System.Collections;

public class captureScript : MonoBehaviour 
{
	public SpriteRenderer myRenderer;

	private GameObject[,] cubeMatrix = new GameObject[50, 50];
	private bool captured = false;
	private int elapsedFrame = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedFrame += 1;
		if (!captured)
			return;

		for ( int y = 0; y < 50; ++y )
		{
			for ( int x = 0; x < 50; ++x )
			{
				if (cubeMatrix[x,y] != null)
				{
					float cosArgX = (float)((x - 25) * 10 * Mathf.PI / 180.0 + elapsedFrame * Mathf.PI / 180.0);
					float cosArgY = (float)((y - 25) * 5 * Mathf.PI / 180.0 + elapsedFrame * Mathf.PI / 180.0);
					float cosArgZ = (float)((x - 25) * 10 * Mathf.PI / 180.0 + (y - 25) * 5 * Mathf.PI / 180.0 + elapsedFrame * Mathf.PI / 180.0);
					float newX = x + Mathf.Cos(cosArgX);
					float newY = y + Mathf.Cos(cosArgY);
					float newZ = 4 * Mathf.Cos(cosArgZ);
					cubeMatrix[x,y].transform.position = new Vector3(newX,newY,newZ);
				}
			}
		}
	}

	void OnGUI()
	{
		if ( GUILayout.Button("Capture") )
		{
			//myRenderer
			Texture2D tex2D = myRenderer.sprite.texture;
			for ( int y = 0; y < 50; ++y )
			{
				for ( int x = 0; x < 50; ++x )
				{
					Color color = tex2D.GetPixel(x,y);
					// checking alpha
					if (color.a == 0)
					{
						cubeMatrix[x,y] = null;
						continue;
					}
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
					cubeMatrix[x,y] = cube;
					cube.transform.position = new Vector3(x,y,0);
					cube.transform.localScale = new Vector3(1, 1,1);

					Destroy(cube.GetComponent<BoxCollider>());

					Material mat = new Material(Shader.Find("Diffuse"));
					mat.color = color;

					cube.GetComponent<MeshRenderer>().material= mat;
				}
			}

			captured = true;
		}
	}

	void OnDrawGizmos()
	{

	}
}
