using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float DecreaseSpeed;
    [SerializeField] float IncreaseSpeed;
    public static bool CanRun;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            slider.value -= DecreaseSpeed * Time.deltaTime;
        else
            slider.value += IncreaseSpeed * Time.deltaTime;

        if (slider.value <= 0)
        {
            CanRun = false;
        }
        else if (slider.value >= 100)
        {
            CanRun = true;
        }
    }
}
