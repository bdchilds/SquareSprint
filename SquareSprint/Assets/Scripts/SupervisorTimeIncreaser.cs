using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupervisorTimeIncreaser : MonoBehaviour
{
    private UnitySharpNEAT.NeatSupervisor NeatSupervisor = null;
    private uint lastGeneration;
    // Start is called before the first frame update
    void Start()
    {
        NeatSupervisor = GetComponent<UnitySharpNEAT.NeatSupervisor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(NeatSupervisor != null)
        {
            if(NeatSupervisor.CurrentGeneration != lastGeneration)
            {
                lastGeneration = NeatSupervisor.CurrentGeneration;
                if (NeatSupervisor.CurrentGeneration % 100 == 0)
                {
                    NeatSupervisor.TrialDuration += 2;
                }
            }
            
        }
    }
}
