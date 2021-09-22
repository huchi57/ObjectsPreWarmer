using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-15)]
public class ObjectsPreWarmer : MonoBehaviour
{
    [System.Serializable]
    public enum PreWarmTimeStepInterval
    {
        [InspectorName("Use Time.deltaTime")]
        UseDeltaTime,
        UseCustomTimeInterval
    }

    [SerializeField]
    private bool autoWarmingOnAwake = true;

    [SerializeField]
    private int objectsToAwakePerOperation = 1;

    [Header("Time Interval Between Awake Operations")]

    [SerializeField]
    private PreWarmTimeStepInterval timeStepIntervalMode;

    [SerializeField]
    private float customTimeStepInterval = 0.01f;

    [Space(10)]

    [SerializeField]
    private GameObject[] preWarmObjects;

    private WaitForSeconds wait;
    private bool[] cacheObjectActiveStates;

    private void Awake()
    {
        HideObjects();
        if (autoWarmingOnAwake)
        {
            WarmObjects();
        }
    }

    private void HideObjects()
    {
        if (preWarmObjects == null)
        {
            return;
        }
        cacheObjectActiveStates = new bool[preWarmObjects.Length];
        for (int i = 0; i < preWarmObjects.Length; i++)
        {
            if (preWarmObjects[i] != null)
            {
                cacheObjectActiveStates[i] = preWarmObjects[i].activeSelf;
                preWarmObjects[i].SetActive(false);
            }
        }
    }

    public void WarmObjects()
    {
        if (preWarmObjects != null)
        {
            StartCoroutine(DoWarm());
        }
    }

    IEnumerator DoWarm()
    {
        wait = new WaitForSeconds(customTimeStepInterval);
        for (int i = 0; i < preWarmObjects.Length; i++)
        {
            yield return timeStepIntervalMode == PreWarmTimeStepInterval.UseDeltaTime ? null : wait;
            if (preWarmObjects[i] != null)
            {
                WarmObjectsFromIndex(i);
                i += objectsToAwakePerOperation - 1;
            }
        }
    }
    
    private void WarmObjectsFromIndex(int currentIndex)
    {
        for (int i = currentIndex; i < currentIndex + objectsToAwakePerOperation; i++)
        {
            if (i >= preWarmObjects.Length) 
            {
                break;
            }
            if (preWarmObjects[i] != null)
            {
                preWarmObjects[i].SetActive(cacheObjectActiveStates[i]);
            }
        }
    }

    private void OnValidate()
    {
        if (objectsToAwakePerOperation < 1)
        {
            objectsToAwakePerOperation = 1;
        }
        if (customTimeStepInterval < 0)
        {
            customTimeStepInterval = 0;
        }
    }

}
