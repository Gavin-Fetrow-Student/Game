using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject Light;

    [SerializeField] private Vector3 OpenPos;
    [SerializeField] private Vector3 ClosePos;

    [SerializeField] private float DoorSpeed;

    public bool IsOpen;
    public bool IsOn;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = OpenPos;
        IsOpen = true;

        ChangeLights();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOpen)
        {
            if (transform.position != OpenPos)
            {
                if (Vector3.Distance(transform.position, OpenPos) <= 0.01f)
                {
                    transform.position = OpenPos;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, OpenPos, DoorSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (transform.position != ClosePos)
            {
                if (Vector3.Distance(transform.position, ClosePos) <= 0.01f)
                {
                    transform.position = ClosePos;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, ClosePos, DoorSpeed * Time.deltaTime);
                }
            }
        }
    }
    public void ChangeLights()
    {
        IsOn = !IsOn;

        if (IsOn)
        {
            Light.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
        }
    }
}
