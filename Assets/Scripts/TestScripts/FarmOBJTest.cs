using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmOBJTest : MonoBehaviour
{
    public float gridSize = 1f; // �׸����� ũ��
    public Vector2 offset = Vector2.zero; // �׸����� ������ (�ʿ��� ���)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // �׽�Ʈ������ GŰ�� ������ �� ������Ʈ ����
        {
            AlignToGrid();
        }
    }

    public void AlignToGrid()
    {
        Vector3 position = transform.position;

        // ��ġ�� �׸��� ũ�⿡ �°� ����
        position.x = Mathf.Round((position.x - offset.x) / gridSize) * gridSize + offset.x;
        position.y = Mathf.Round((position.y - offset.y) / gridSize) * gridSize + offset.y;

        transform.position = position;
    }
}
