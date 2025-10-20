using UnityEngine;
using TMPro;

public class AnimationTextColorTrigger : MonoBehaviour
{
    [Header("設定")]
    public Animator targetAnimator;
    public TMP_Text targetText;

    [Header("秒数で指定")]
    public float triggerTimeInSeconds = 2.0f;
    public float resetTimeInSeconds = 5.0f;

    public Color triggerColor = Color.red;
    public Color normalColor = Color.white;

    [Header("ステート指定（任意）")]
    public string targetStateName = ""; // 空文字ならどのステートでも反応

    private bool triggered = false;

    void Start()
    {
        if (targetText != null)
            targetText.color = normalColor;
    }

    void Update()
    {
        if (targetAnimator == null || targetText == null) return;

        // 現在のステート情報
        AnimatorStateInfo state = targetAnimator.GetCurrentAnimatorStateInfo(0);

        // 特定ステートを監視する場合
        if (!string.IsNullOrEmpty(targetStateName) && !state.IsName(targetStateName))
        {
            if (triggered)
            {
                targetText.color = normalColor;
                triggered = false;
            }
            return;
        }

        // ループ内の進行割合
        float normalizedTime = state.normalizedTime % 1f;

        // アニメーション上の現在位置（秒）
        float currentTime = normalizedTime * state.length*state.speed;

        // トリガー判定
        bool inTriggerRange = currentTime >= triggerTimeInSeconds &&
                              currentTime < resetTimeInSeconds;

        if (inTriggerRange && !triggered)
        {
            targetText.color = triggerColor;
            triggered = true;
            Debug.Log($"[TRIGGERED] Time: {currentTime:F2}s / {state.length:F2}s");
        }
        else if (!inTriggerRange && triggered)
        {
            targetText.color = normalColor;
            triggered = false;
            Debug.Log($"[RESET] Time: {currentTime:F2}s / {state.length:F2}s");
        }
    }
}
