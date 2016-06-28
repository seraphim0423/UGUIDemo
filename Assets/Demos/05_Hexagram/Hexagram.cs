using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasRenderer))]
[ExecuteInEditMode]
public class Hexagram : MonoBehaviour 
{
    public float circle = 360f;
    // 起始方向
    public Vector3 startDirection = new Vector3(0, 1, 0);
    // 星属性
    public int[] attributes = null;
    // 最大值
    public float fullStrength = 100f;

	void Start () 
    {
        CanvasRenderer canvasRenderer = GetComponent<CanvasRenderer>();
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 获取每个属性点的方向向量
        int starsCount = this.attributes.Length;
        List<Vector3> directionList = new List<Vector3>(starsCount);
        float deltaAngle = this.circle / starsCount;
        for (int i = 0; i < attributes.Length; ++i)
        {
            float angle = i * deltaAngle;
            float rad = Mathf.Deg2Rad * angle;
            float sinA = Mathf.Sin(rad);
            float cosA = Mathf.Cos(rad);
            directionList.Add(new Vector3(startDirection.x * cosA + startDirection.y * sinA, 
                -startDirection.x * sinA + startDirection.y * cosA, 0));
        }

        Mesh mesh = new Mesh();
        // 设置顶点位置属性
        int verticesCount = starsCount + 1;
        List<Vector3> vertices = new List<Vector3>(verticesCount);
        vertices.Add(Vector3.zero);
        for (int i = 1; i < verticesCount; ++i)
        {
            int index = i - 1;
            vertices.Add(directionList[index] * this.attributes[index] / this.fullStrength * rectTransform.sizeDelta.x);
        }
        mesh.SetVertices(vertices);
        // 设置UV属性

        // 设置颜色属性
        List<Color32> colors = new List<Color32>();
        colors.Add(new Color32(255, 255, 255, 255));
        for (int i = 0; i < starsCount; ++i)
        {
            float strength = attributes[i] / rectTransform.sizeDelta.x;
            byte r = (byte)(strength * 200);
            Color32 c = new Color32(r, 0, 0, 255);
            colors.Add(c);
        }
        mesh.SetColors(colors);
        // 设置索引属性
        int[] indices = new int[3 * starsCount];
        for (int i = 0; i < starsCount; ++i)
        {
            int index = 3 * i;
            indices[index] = 0;
            indices[index + 1] = i + 1;
            indices[index + 2] = (i + 2) > starsCount ? (i + 2 - starsCount) : (i + 2);
        }
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        canvasRenderer.SetMesh(mesh);

        Material material = new Material(Shader.Find("UI/Default"));
        canvasRenderer.SetMaterial(material, null);
	}
}
