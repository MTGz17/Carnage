using UnityEngine;
using System.Collections;

public class ScreenAnimator : MonoBehaviour
{
    public static ScreenAnimator Instance;

    [SerializeField] private GameObject cashAnimation;
    [SerializeField] private GameObject speedAnimation;
    [SerializeField] private GameObject doubleAnimation;

    [SerializeField] private float hideDelay = 2f;
    [SerializeField] private float doubleHideDelay = 15f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (cashAnimation != null) cashAnimation.SetActive(false);
        if (speedAnimation != null) speedAnimation.SetActive(false);
        if (doubleAnimation != null) doubleAnimation.SetActive(false);
    }

    public void PlayCashAnimation()
    {
        PlayAnimation(cashAnimation, "CashAnimation", hideDelay);
    }

    public void PlaySpeedAnimation()
    {
        PlayAnimation(speedAnimation, "SpeedAnimation", hideDelay);
    }

    public void PlayDoubleAnimation()
    {
        PlayAnimation(doubleAnimation, "DoubleAnimation", doubleHideDelay);
    }

    private void PlayAnimation(GameObject animationObject, string animationName, float delay)
    {
        if (animationObject == null) return;

        Animator animator = animationObject.GetComponent<Animator>();
        if (animator == null) return;

        animationObject.SetActive(true);
        animator.Play(animationName);

        StartCoroutine(HideAfterDelay(animationObject, delay));
    }

    private IEnumerator HideAfterDelay(GameObject animationObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        animationObject.SetActive(false);
    }
}
