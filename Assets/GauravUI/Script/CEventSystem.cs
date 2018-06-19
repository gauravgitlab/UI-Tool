
using UnityEngine;
using UnityEngine.EventSystems;

public class CEventSystem 
{
    public GameObject CreateEventSystem()
    {
        var eventSystemObj = new GameObject("EventSystem", typeof(EventSystem));
        eventSystemObj.AddComponent<StandaloneInputModule>();

        return eventSystemObj;
    }
}
