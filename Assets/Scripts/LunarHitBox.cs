using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LunarHitBox : MonoBehaviour
{
    public UnityEvent OnHitEvent;
    public UnityEvent ExitHitEvent;
    Collider temp;
    [SerializeField] int Total;
    [SerializeField] int HitCount;
    [SerializeField] int Score;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stone")
        {
            OnHitEvent?.Invoke();
            temp = other;
            Debug.Log("Hit");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stone")
        {
            ExitHitEvent?.Invoke();
            Debug.Log("Exit");
        }
    }

    public void TotalPlus()
    {
        Total++;
    }
    
    public void HitCountPlus()
    {
        temp.gameObject.tag = "Untagged";
    }

    public void CauculateScore(TextMeshProUGUI text)
    {
        Score = Total - HitCount;
        text.text ="Score: " + Score.ToString();
    }


}
