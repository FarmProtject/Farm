using UnityEngine;
using System;
using System.Collections.Generic;
public enum FertilizerType
{
    none
}
public class CropInstance
{
    public CropData data;        // CropData에 대한 참조
    public int growthStage;      // 현재 성장 단계
    public bool isWatered;       // 물 주었는지 여부
    public FertilizerType fertilizer;  // 비료 상태

    public CropInstance(CropData data)
    {
        this.data = data;
        this.growthStage = data.level; // 초기화 시 현재 성장 단계로 시작
        this.isWatered = false;
        this.fertilizer = FertilizerType.none;
    }

    // 성장 함수
    public void Grow()
    {
        if (!isWatered) return;  // 물을 안 주면 성장하지 않음
        data.myTime++;
        if (data.myTime >= data.reqTime)
        {
            data.myTime -= data.reqTime;
            growthStage = Mathf.Min(growthStage + 1, data.nextLevel);
            data.level = (data.level + 1);
        }
        
        isWatered = false; // 성장 후 물은 리셋
    }

    // 완전히 성장했는지 체크
    public bool IsFullyGrown()
    {
        return growthStage >= data.nextLevel;
    }
}

