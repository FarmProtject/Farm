using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : ButtonBase
{




    protected override void OnButtonClick()
    {
        SceneManager.LoadScene("");
    }
}
