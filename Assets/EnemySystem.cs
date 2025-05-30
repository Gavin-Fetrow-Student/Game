using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NMA;
    [SerializeField] private GameObject[] Targets;

    [SerializeField] private int CurrentTarget;

    [SerializeField] private float CoolDownTimer;
    [SerializeField] private float MinCoolDownTime;
    [SerializeField] private float MaxCoolDownTime;

    [SerializeField] private int MinChanceToMove = 1;
    [SerializeField] private int MaxChanceToMove = 20;

    [SerializeField] private int TresholdToPass = 3;

    [SerializeField] private int[] AggressionByHour;

    [SerializeField] private int MinAggressionToAdd = 2;
    [SerializeField] private int MaxAggressionToAdd = 5;

    [SerializeField] private int HoursChanged;

    [SerializeField] private PlayableDirector Director;

    [SerializeField] private bool StartedJumpscare;

    // Start is called before the first frame update
    void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CoolDownTimer <= 0)
        {
            var chanceCheck = Random.Range(MinChanceToMove, MaxChanceToMove);

            if(chanceCheck <= TresholdToPass)
            {
                if (Vector3.Distance(transform.position, Targets[CurrentTarget].transform.position) <= 0.5f)
                {
                    if (Targets[CurrentTarget].GetComponent<DestinationPoint>().IsDoor)
                    {
                        if (Targets[CurrentTarget].GetComponent<DestinationPoint>().Door.IsOpen)
                        {
                            CurrentTarget = Targets.Length - 1;
                        }
                        else
                        {
                            CurrentTarget = 1;
                        }
                    }
                    else if (Targets[CurrentTarget].GetComponent<DestinationPoint>().IsOffice)
                    {
                        Debug.Log("You Died");
                    }
                    else
                    {
                        CurrentTarget += 1;
                        if (CurrentTarget >= Targets.Length)
                        {
                            CurrentTarget = 0;
                        }
                    }
                }
            }

            var CoolDownTime = Random.Range(MinCoolDownTime, MaxCoolDownTime);
            CoolDownTimer = CoolDownTime;
        }
        else
        {
            CoolDownTimer -= Time.deltaTime;
        }

        if (Targets[CurrentTarget].GetComponent<DestinationPoint>().IsOffice)
        {
            if (!StartedJumpscare)
            {
                Director.Play();
                StartedJumpscare = true;
            }
        }

        NMA.destination = Targets[CurrentTarget].transform.position;
    }

    public void ChangeAggressionByHour(int hour)
    {
        if(HoursChanged != hour)
        {
            if (TresholdToPass < hour)
            {
                TresholdToPass = AggressionByHour[hour];
            }

            TresholdToPass += Random.Range(MinAggressionToAdd, MaxAggressionToAdd);
            HoursChanged += 1;
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}