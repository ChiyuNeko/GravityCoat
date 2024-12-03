using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LunarGameManager : MonoBehaviour
{
    public GameObject waterController;
    public LunarGameObjectGenerater[] lunarGameObjectGenerater; 
    public List<GameObject> Planets;
    public List<float> SwitchTime; // timing for cloth charge befor enter next planets  
    public List<AudioSource> pre_audios;
    public List<AudioSource> audios;
    public float ExperientTime;
    public Vector3 SwitchViewPoint;
    public TextMeshProUGUI title;
    public UnityEvent OpenSwitchPlantEvnt = new UnityEvent();
    public UnityEvent CloseSwitchPlantEvnt = new UnityEvent();
    public UnityEvent GameEndEvnt;
    
    int index; // for record current state
    Vector3 force;

    void Start()
    {
        foreach (GameObject p in Planets)
        {
            p.SetActive(false);
        }

        index = 0;

        //Initialized

        //GameStart();
        force = lunarGameObjectGenerater[0].generater.GameObjectForce;
    }

    void Update()
    {
        switch (index)
        {
            case 0:
                for(int i = 0; i < 3; i++)
                {
                    lunarGameObjectGenerater[i].generater.GameObjectForce = force * 1f;
                    lunarGameObjectGenerater[i].generater.GeanerateFrequency = 0.7f;
                }
                break;
            case 1:
                for(int i = 0; i < 3; i++)
                {
                    lunarGameObjectGenerater[i].generater.GameObjectForce = force * 2.0f;
                    lunarGameObjectGenerater[i].generater.GeanerateFrequency = 2.0f;
                }
                break;
            case 2:
                for(int i = 0; i < 3; i++)
                {
                    lunarGameObjectGenerater[i].generater.GameObjectForce = force * 1.5f;
                    lunarGameObjectGenerater[i].generater.GeanerateFrequency = 1.5f;
                }
                break;
            
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameStart();
        }
    }

    
    IEnumerator ExperientTimer(float time)
    {
        pre_audios[index].Play();
        OpenSwitchPlantEvnt?.Invoke(); // Open Switch Scene
        yield return new WaitForSeconds(SwitchTime[index]);
        audios[index].Play();
        CloseSwitchPlantEvnt?.Invoke();// Close Switch Scene

        yield return new WaitForSeconds(time);
        SwitchPlant();
    }

    public void GameStart() // Trigger the system
    {
        Planets[0].SetActive(true);
        waterController.GetComponent<WaterController>().enabled = true;
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
        title.text ="Thanks For Playing!";
        GameEndEvnt?.Invoke();
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        //SceneChanger.Instance.ChangeToScene(1);
    }
    public void StartIntro()
    {
        StartCoroutine(IntroTimer());
    }
    public IEnumerator IntroTimer()
    {
        yield return new WaitForSeconds(20);
        GameStart();
    }


}
