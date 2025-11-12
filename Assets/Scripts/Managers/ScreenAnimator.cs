using UnityEngine;
using System.Collections;

public class ScreenAnimator : MonoBehaviour
{
    public static ScreenAnimator Instance;

    [SerializeField] private GameObject cashAnimation;
    [SerializeField] private GameObject speedAnimation;
    [SerializeField] private float hideDelay = 1f;

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
    }

    public void PlayCashAnimation()
    {
        PlayAnimation(cashAnimation, "CashAnimation");
    }

    public void PlaySpeedAnimation()
    {
        PlayAnimation(speedAnimation, "SpeedAnimation");
    }

    private void PlayAnimation(GameObject animationObject, string animationName)
    {
        if (animationObject == null) return;

        Animator animator = animationObject.GetComponent<Animator>();
        if (animator == null) return;

        animationObject.SetActive(true);
        animator.Play(animationName);

        StartCoroutine(HideAfterDelay(animationObject, hideDelay));
    }

    private IEnumerator HideAfterDelay(GameObject animationObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        animationObject.SetActive(false);
    }
}
