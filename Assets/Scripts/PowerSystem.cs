using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    public int SystemsOn;
    public float Power = 100;

    [SerializeField] private TextMeshProUGUI PowerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SystemsOn < 0)
        {
            SystemsOn = 0;
        }


        if(SystemsOn == 1)
        {
            Power -=0.1f * Time.deltaTime;
        }
        else if(SystemsOn == 2)
        {
            Power -= 0.5f * Time.deltaTime;
        }
        else if (SystemsOn == 3)
        {
            Power -= 1f * Time.deltaTime;
        }
        else if (SystemsOn == 4)
        {
            Power -= 1.5f * Time.deltaTime;
        }
        else if (SystemsOn == 5)
        {
            Power -= 2f * Time.deltaTime;
        }

        var power = string.Format("{0:0}", Power);
        PowerText.text = $"{power}%";
    }
}
