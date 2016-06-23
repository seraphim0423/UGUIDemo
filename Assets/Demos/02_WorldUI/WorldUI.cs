using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldUI : MonoBehaviour 
{
    public int cubeCount = 10;

	// Use this for initialization
	void Start () 
	{
        Object cubePrefab = Resources.Load("Cube");
        for (int i = 0; i < this.cubeCount; ++i)
        {
            GameObject cube = GameObject.Instantiate(cubePrefab) as GameObject;
            cube.GetComponentInChildren<Text>().text = string.Format("Cube {0}", i);
            Vector3 pos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 10));
            cube.transform.position = pos;
        }
	}
	
}
