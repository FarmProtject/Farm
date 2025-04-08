using UnityEngine;
using System;
using System.Collections.Generic;
public class LanguageChange : MonoBehaviour
{
    StringKeyManager skManager;


    void LanguageChangeFunction(languges language)
    {
        skManager.LanguageChange(language);
    }

}
