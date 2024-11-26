using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class Generater
{
    public bool GenerateIO;
    public GameObject GenerateObject;
    public float GeanerateFrequency;
    public float GameObjectScale;
    public Vector3 GameObjectForce;
    public float GameObjectLifeTime;
    public Vector3 GenerateZone;
    public UnityEvent GenerateEvnt;
    public Vector3 RandomPositionInZone()
    {
        Vector3 vec = new Vector3( UnityEngine.Random.Range(0, GenerateZone.x),
                                    UnityEngine.Random.Range(0, GenerateZone.y),
                                    UnityEngine.Random.Range(0, GenerateZone.z));

        return vec;
    }
}

public class LunarGameObjectGenerater : MonoBehaviour
{
    public Generater generater;
    void Start()
    {
        StartCoroutine(GenerateProcess());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StopGenerate();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartGenerate();
        }
    }

    public void Generate()
    {
        GameObject gameObject = Instantiate(generater.GenerateObject, this.transform.localPosition + generater.RandomPositionInZone(), this.transform.localRotation);
        float s = UnityEngine.Random.Range(0, generater.GameObjectScale);
        gameObject.transform.localScale  = new Vector3(s, s, s);
        gameObject.GetComponent<Rigidbody>().AddForce(generater.GameObjectForce);
        Destroy(gameObject, generater.GameObjectLifeTime);
    }

    public IEnumerator GenerateProcess()
    {
        if(generater.GenerateIO)
        {
            yield return new WaitForSeconds(1.0f / generater.GeanerateFrequency);
            generater.GenerateEvnt.Invoke();
            StartCoroutine(GenerateProcess());
        }
        else
        {
            yield return null;
        }
    }
    public void StartGenerate()
    {
        generater.GenerateIO = true;
        StartCoroutine(GenerateProcess());
    }

    public void StopGenerate()
    {
        generater.GenerateIO = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position + generater.GenerateZone/2.0f , new Vector3(generater.GenerateZone.x, generater.GenerateZone.y, generater.GenerateZone.z));
    }
}
