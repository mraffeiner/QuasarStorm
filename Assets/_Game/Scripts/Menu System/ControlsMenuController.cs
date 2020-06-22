using UnityEngine;

public class ControlsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject keyboardControls = null;
    [SerializeField] private GameObject touchControls = null;

    private void Start()
    {
        if (MobileDetection.isMobile())
            touchControls.SetActive(true);
        else
            keyboardControls.SetActive(true);
    }
}
