using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmOBJTest : MonoBehaviour
{
    public float gridSize = 1f; // 그리드의 크기
    public Vector2 offset = Vector2.zero; // 그리드의 오프셋 (필요한 경우)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // 테스트용으로 G키를 눌렀을 때 오브젝트 정렬
        {
            AlignToGrid();
        }
    }

    public void AlignToGrid()
    {
        Vector3 position = transform.position;

        // 위치를 그리드 크기에 맞게 조정
        position.x = Mathf.Round((position.x - offset.x) / gridSize) * gridSize + offset.x;
        position.y = Mathf.Round((position.y - offset.y) / gridSize) * gridSize + offset.y;

        transform.position = position;
    }
}
