using UnityEngine;
using UnityEngine.UI;

internal class ExitButton : MonoBehaviour
{
    [SerializeField] Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ExitApp);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ExitApp);
    }

    private void ExitApp()
    {
        Application.Quit();
    }
}
