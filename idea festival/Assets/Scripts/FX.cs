using UnityEngine;
public class FX : MonoBehaviour
{
    [SerializeField]
    private bool isLoop = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        gameObject.SetActive(false);
    }
    private void Update()
    {
        if(!isLoop)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}