using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Health _player;
    [SerializeField] private Health _boss;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _levelPassed;
    [SerializeField] private float _delayAfterBossDeath;

    private const string Menu = "Menu";

    private void OnEnable()
    {
        _player.Died += OnPlayerDies;
        _boss.Died += OnBossDies;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDies;
        _boss.Died -= OnBossDies;
    }

    private void OnPlayerDies()
    {
        _gameOverMenu.SetActive(true);
        _playerInput.SwitchCurrentActionMap(Menu);
    }

    private void OnBossDies()
    {
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(_delayAfterBossDeath);

        _levelPassed.SetActive(true);
        Time.timeScale = 0f;
        _playerInput.SwitchCurrentActionMap(Menu);
    }
}