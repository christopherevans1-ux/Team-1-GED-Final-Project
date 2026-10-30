using Unity.VisualScripting;
using UnityEngine;

public class KeyRebineMenu : MonoBehaviour
{
    [SerializeField] private GameObject rebinePanel;
    [SerializeField] private GameObject openButton;

    public static bool isOpen;
    private float previousTimeScale = 1f;

    private void Awake()
    {
        isOpen = false;
        rebinePanel.SetActive(false);
        openButton.SetActive(true);
    }

    public void OpenMenu()
    {
        if(isOpen)
            return;

        previousTimeScale = Time.timeScale;
        isOpen = true;

        rebinePanel.SetActive(true);
        openButton.SetActive(false);

        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        if (!isOpen)
            return;

        rebinePanel.SetActive(false);
        openButton.SetActive(true);

        isOpen = false;
        Time.timeScale = previousTimeScale;
    }
}
