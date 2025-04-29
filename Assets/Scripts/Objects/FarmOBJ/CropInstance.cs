using UnityEngine;
using System;
using System.Collections.Generic;
public enum FertilizerType
{
    none
}
public class CropInstance
{
    public CropData data;        // CropData�� ���� ����
    public int growthStage;      // ���� ���� �ܰ�
    public bool isWatered;       // �� �־����� ����
    public FertilizerType fertilizer;  // ��� ����

    public CropInstance(CropData data)
    {
        this.data = data;
        this.growthStage = data.level; // �ʱ�ȭ �� ���� ���� �ܰ�� ����
        this.isWatered = false;
        this.fertilizer = FertilizerType.none;
    }

    // ���� �Լ�
    public void Grow()
    {
        if (!isWatered) return;  // ���� �� �ָ� �������� ����
        data.myTime++;
        if (data.myTime >= data.reqTime)
        {
            data.myTime -= data.reqTime;
            growthStage = Mathf.Min(growthStage + 1, data.nextLevel);
            data.level = (data.level + 1);
        }
        
        isWatered = false; // ���� �� ���� ����
    }

    // ������ �����ߴ��� üũ
    public bool IsFullyGrown()
    {
        return growthStage >= data.nextLevel;
    }
}

