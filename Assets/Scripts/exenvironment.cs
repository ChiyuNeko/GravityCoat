using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exenvironment : MonoBehaviour
{
    [SerializeField]
    private Material dissolveMaterial;

    // 動態Dissolve控制參數
    private float dissolveAmount = 0f;
    public bool isIncreasing = true; // 控制變化方向
    public GameObject planet, planet1, planet2;

    // 更新頻率
    public float dissolveSpeed = 1f;

    private void Update()
    {
        // 確保材質已經設置
        if (dissolveMaterial != null)
        {
            // 控制dissolveAmount在 -1 和 1 之間變化
            if (isIncreasing)
            {
                dissolveAmount += Time.deltaTime * dissolveSpeed;
                if (dissolveAmount >= 1f)
                {
                    dissolveAmount = 1f;
                }
            }
            else
            {
                dissolveAmount -= Time.deltaTime * dissolveSpeed;
                if (dissolveAmount <= -1f)
                {
                    dissolveAmount = -1f;
                }
            }

            // 更新材質的Dissolve參數
            dissolveMaterial.SetFloat("_Dissolve", dissolveAmount);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            planet.SetActive(false);
            planet1.SetActive(true);
            planet2.SetActive(false);
            isIncreasing = true;
            Debug.Log("earth");
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            planet.SetActive(false);
            planet1.SetActive(false);
            planet2.SetActive(true);
            isIncreasing = true;
            Debug.Log("mars");
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            planet.SetActive(true);
            planet1.SetActive(false);
            planet2.SetActive(false);
            isIncreasing = true;
            Debug.Log("saturn");
        }else if (Input.GetKeyDown(KeyCode.W))
        {
            isIncreasing = false;
            Debug.Log("cover environment");
        }
    }
    public void showenvironment()
    {
        isIncreasing = true;
    }
    public void coverenvironment()
    {
        isIncreasing = false;
    }
}
