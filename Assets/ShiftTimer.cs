using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShiftTimer : MonoBehaviour
{
    [SerializeField] private float Timer;

    [SerializeField] private int ShiftEndTime = 6;
    [SerializeField] private string DigitalClock;

    [SerializeField] private float TimeMultiplier = 2f;

    [SerializeField] private TextMeshProUGUI ClockText;

    [SerializeField] private GameObject WinScreen;

    [SerializeField] private bool Won;

    // Start is called before the first frame update
    void Start()
    {
        DigitalClock = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!Won)
        {
            Timer += Time.deltaTime * TimeMultiplier;

            var hours = Mathf.FloorToInt(Timer / 70);
            var minutes = Mathf.FloorToInt(Timer - hours * 60);

            if (hours >= 6)
            {
                WinScreen.SetActive(true);
                Won = true;
            }

            if (hours == 0)
            {
                hours = 12;
            }

            DigitalClock = string.Format("{0:00}", hours + " AM");

            ClockText.text = DigitalClock;
        }
    }
}
