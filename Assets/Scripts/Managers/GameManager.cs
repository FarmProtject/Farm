using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    GameManager _instance;
    UIManager _UIManager;
    InputBase _InputManager { get; set; }
    public InputBase inputManager { get; set; }
    static public GameManager instance { get; set; }
    public UIManager UIManager;
    

    private void Start()
    {
        SetUpField();
        ChangeInputOnScene();//TestCode
    }
    private void Update()
    {
        inputManager.OnPlayerInput();
        
    }
    public void SetUpField()
    {
        if (_instance == null)
        {
            _instance = this;
            instance = _instance;
        }

    }
    
    public void ChangeInputOnScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "MainScene")
        {
            _InputManager = new MainInput();
            inputManager = _InputManager;
        }
        else
        {
            _InputManager = new TownInput();
            inputManager = _InputManager;
        }
    }


    public void OnKey()
    {
        _InputManager.OnPlayerInput();
    }

}
