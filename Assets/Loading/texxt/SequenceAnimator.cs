using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAnimator : MonoBehaviour
{
    public List<Animator> _TextLoading;
    public float WaitBetween = 0.15f;
    public float WaitEnd = 0.5f;
    void Start()
    {
        _TextLoading = new List<Animator>(GetComponentsInChildren<Animator>());
        StartCoroutine(DoAnimation());
    }  

    // Update is called once per frame
    IEnumerator DoAnimation()
    {
        while(true)
        {
            foreach(var animator in _TextLoading )
            {
                animator.SetTrigger("DoAnimation");
                yield return new WaitForSeconds(WaitBetween);
            }
            yield return new WaitForSeconds(WaitEnd);
        }
    }

    public void ResetAllAnimators()
    {
        foreach(var animator in _TextLoading )
        {
            animator.Rebind();
        }
    }
    public void StartAllAnimators()
    {
        StartCoroutine(DoAnimation());
    }
}
