using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LunarGameManager : MonoBehaviour
{
    public List<GameObject> Planets;
    public List<float> SwitchTime; // timing for cloth charge befor enter next planets  
    public float ExperientTime;
    public Vector3 SwitchViewPoint;
    public UnityEvent OpenSwitchPlantEvnt = new UnityEvent();
    public UnityEvent CloseSwitchPlantEvnt = new UnityEvent();
    
    int index; // for record current state

    void Start()
    {
        foreach (GameObject p in Planets)
        {
            p.SetActive(false);
        }

        index = 0;

        //Initialized

        //GameStart();
    }

    
    IEnumerator ExperientTimer(float time)
    {

        OpenSwitchPlantEvnt?.Invoke(); // Open Switch Scene
        yield return new WaitForSeconds(SwitchTime[index]);
        CloseSwitchPlantEvnt?.Invoke();// Close Switch Scene

        yield return new WaitForSeconds(time);
        SwitchPlant();
    }

    public void GameStart() // Trigger the system
    {
        Planets[0].SetActive(true);
        StartCoroutine(ExperientTimer(ExperientTime));
    }

    public void SwitchPlant()
    {
        Planets[index].SetActive(false);

        index ++;

        if (index < Planets.Count)
            Planets[index].SetActive(true);

        if (index == Planets.Count)
            GameEnd();
        else
            StartCoroutine(ExperientTimer(ExperientTime));

    }

    public void SwitchView(GameObject targetObject)
    {
        targetObject.transform.Translate(SwitchViewPoint);
    }
    public void ResetView(GameObject targetObject)
    {
        targetObject.transform.Translate(-SwitchViewPoint);
    }

    public void GameEnd() // Enter Ending scene
    {

    }


}
