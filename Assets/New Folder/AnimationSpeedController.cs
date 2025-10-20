using UnityEngine;
using UnityEngine.UI;

public class AnimationSpeedController : MonoBehaviour
{
    public Animator targetAnimator;
    public Slider speedSlider;
    public Text speedText;

    void Start()
    {
        if (speedSlider != null)
        {
            speedSlider.onValueChanged.AddListener(OnSpeedChanged);
            OnSpeedChanged(speedSlider.value);
        }
    }

    void OnSpeedChanged(float value)
    {
        if (targetAnimator != null)
        {
            targetAnimator.speed = value;
            targetAnimator.Play(targetAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0f);
        }

        if (speedText != null)
            speedText.text = $"Speed: {value:F2}";
    }
}