using System;
using UnityEngine;
using TriggerSystem;

public class ExampleTriggerTester : MonoBehaviour
{
    private TriggerBase _trigger;

    private void Awake()
    {
        _trigger = GetComponent<TriggerBase>();
        _trigger.TriggerStayed += OnTriggerStayed;
    }

    private void OnDestroy()
    {
        _trigger.TriggerStayed -= OnTriggerStayed;
    }

    private void OnTriggerStayed(TriggerBase other)
    {
        Debug.Log($"{name} -> {other.name}");
    }
}