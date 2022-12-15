using System;
using UnityEngine;
using TriggerSystem;

public class ExampleTriggerTester : MonoBehaviour
{
    [SerializeField]
    private SphereTrigger _sphereTrigger;

    private void Awake()
    {
        _sphereTrigger.SphereTriggerStayed += OnTriggerStayed;
    }

    private void OnDestroy()
    {
        _sphereTrigger.SphereTriggerStayed -= OnTriggerStayed;
    }

    private void OnTriggerStayed(SphereTrigger other)
    {
    }
}