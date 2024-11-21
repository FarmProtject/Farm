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
    public UIManager UIManager;
    public KeySettings keySettings = new KeySettings();
    static public GameManager instance { get; set; }



    private void Start()
    {
        SetUpField();
        keySettings.DefaultKeyBinding();
        ChangeInputOnScene();//TestCode
        
        
    }
    private void Update()
    {
        
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
            _InputManager.gameManager = this;
            inputManager.gameManager = this;
            if (EventManager.instance.OnPlayerInput != null)
            {
                EventManager.instance.OnPlayerInput.RemoveAllListeners();
            }
            EventManager.instance.OnPlayerInput.AddListener(inputManager.OnPlayerInput);
        }
        else
        {
            _InputManager = new TownInput();
            inputManager = _InputManager;
            _InputManager.gameManager = this;
            inputManager.gameManager = this;
            if (EventManager.instance.OnPlayerInput != null)
            {
                EventManager.instance.OnPlayerInput.RemoveAllListeners();
            }
            EventManager.instance.OnPlayerInput.AddListener(inputManager.OnPlayerInput);
        }
    }


    public void OnKey()
    {
        _InputManager.OnPlayerInput();
    }

}
