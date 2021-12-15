using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private string _level;
    [SerializeField] private GameObject _mainMenu;

    private const string Menu = "Menu";
    private const string Player = "Player";

    public void Resume()
    {
        Close();
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Open(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Time.timeScale = 0f;
            _mainMenu.SetActive(true);
            _playerInput.SwitchCurrentActionMap(Menu);
        }
    }

    public void Close(InputAction.CallbackContext context)
    {
        if (context.started)
            Close();
    }

    private void Close()
    {
        _mainMenu.SetActive(false);
        Time.timeScale = 1f;
        _playerInput.SwitchCurrentActionMap(Player);
    }
}