using UnityEngine;

[ExecuteAlways]
public class GridArranger : MonoBehaviour
{
    public int rows = 4;
    public float spacingY = 5f;   // 縦の間隔（大きく）
    public float spacingX = 8f;   // 横の間隔（大きく）
    public Vector3 startPos = Vector3.zero;

    void OnValidate()
    {
        ArrangeObjects();
    }

    void Start()
    {
        ArrangeObjects();
    }

    void ArrangeObjects()
    {
        int count = 0;
        int column = 0;

        foreach (Transform child in transform)
        {
            int row = count % rows;

            Vector3 pos = new Vector3(
                startPos.x + (spacingX * column),
                startPos.y - (spacingY * row),
                startPos.z
            );

            child.localPosition = pos;

            count++;
            if (count % rows == 0) column++;
        }
    }
}
