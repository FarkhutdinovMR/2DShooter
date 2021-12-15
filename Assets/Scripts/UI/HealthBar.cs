using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    [SerializeField] private float _changeSpeed;

    private float _currentValue;
    private Coroutine _changeHealth;

    private void OnEnable()
    {
        _health.Changed += OnChanged;
        _slider.value = _health.Value / _health.MaxHealth;
    }

    private void OnDisable()
    {
        _health.Changed -= OnChanged;
    }

    private void OnChanged(float value)
    {
        _currentValue = value / _health.MaxHealth;

        if (_changeHealth != null)
            StopCoroutine(_changeHealth);

        _changeHealth = StartCoroutine(ChangeHealth());
    }

    private IEnumerator ChangeHealth()
    {
        while (Mathf.Approximately(_slider.value, _currentValue) == false)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _currentValue, _changeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}