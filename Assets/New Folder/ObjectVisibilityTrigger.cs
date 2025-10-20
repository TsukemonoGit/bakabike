using UnityEngine;
using System;

[Serializable]
public class VisibilityTimeRange
{
    public float showTime;
    public float hideTime;
}

public class ObjectVisibilityTrigger : MonoBehaviour
{
    [Header("設定")]
    public Animator targetAnimator;

    [Header("表示時間範囲")]
    public VisibilityTimeRange[] timeRanges;

    [Header("ステート指定（任意）")]
    public string targetStateName = "";

    private bool isVisible = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (targetAnimator == null) return;

        AnimatorStateInfo state = targetAnimator.GetCurrentAnimatorStateInfo(0);

        if (!string.IsNullOrEmpty(targetStateName) && !state.IsName(targetStateName))
        {
            if (isVisible)
            {
                gameObject.SetActive(false);
                isVisible = false;
            }
            return;
        }

        float normalizedTime = state.normalizedTime % 1f;
        float currentTime = normalizedTime * state.length * state.speed;

        bool shouldShow = false;
        foreach (var range in timeRanges)
        {
            if (currentTime >= range.showTime && currentTime < range.hideTime)
            {
                shouldShow = true;
                break;
            }
        }

        if (shouldShow && !isVisible)
        {
            gameObject.SetActive(true);
            isVisible = true;
        }
        else if (!shouldShow && isVisible)
        {
            gameObject.SetActive(false);
            isVisible = false;
        }
    }
}