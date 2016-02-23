using UnityEngine;
using System.Collections;

public class captureScript : MonoBehaviour 
{
	public SpriteRenderer myRenderer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
					cube.transform.position = new Vector3(x,y,0);
					cube.transform.localScale = new Vector3(1, 1,1);

					Destroy(cube.GetComponent<BoxCollider>());

					Material mat = new Material(Shader.Find("Diffuse"));
					mat.color = color;

					cube.GetComponent<MeshRenderer>().material= mat;
				}
			}
		}
	}

	void OnDrawGizmos()
	{

	}
}
