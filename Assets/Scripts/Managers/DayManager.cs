using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class DayManager : MonoBehaviour
{
    List<IDayTickable> listners = new List<IDayTickable>();

    public UnityEvent DayEvents;
    [SerializeField] GameObject fadeOutUI;
    Image fadeOutImage;
    public int day;
    EventManager eventManager;
    private void Awake()
    {
        OnAwake();
    }
    void OnAwake()
    {
        fadeOutImage = fadeOutUI.transform.GetComponent<Image>();
        eventManager = GameManager.instance.gameObject.transform.GetComponent<EventManager>();
    }
    public void Resister(IDayTickable target)
    {
        if (!listners.Contains(target))
        {
            listners.Add(target);
        }
    }
    IEnumerator FadeOut()
    {
        float imageAlpha = 0;
        float fadeOutTime = 2f;
        float fadeSpeed = 120;
        fadeOutUI.SetActive(true);
        eventManager.controllAble = false;
        for(int t = 0; t < fadeSpeed; t++)
        {
            
            imageAlpha = Mathf.Cos(Mathf.PI*t/ fadeSpeed);
            fadeOutImage.color = new Color(0, 0, 0, imageAlpha);
            Debug.Log($"time {t} ImageAlpha {imageAlpha}");
            yield return new WaitForSeconds(fadeOutTime/ fadeSpeed);
        }
        eventManager.controllAble = true;
        fadeOutUI.SetActive(false);
    }
    public void NextDay()
    {
        StartCoroutine("FadeOut");
        foreach(IDayTickable target in listners)
        {
            target.DayPassed();
        }
    }

}
