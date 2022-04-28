using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class VoidEvent : UnityEvent { }
[Serializable] public class OnValueChanged : UnityEvent<bool> { };
public class DelayEvent : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.2f;
    [SerializeField] VoidEvent voidEvent = null;
    [SerializeField] OnValueChanged onValueChanged = null;
    // Start is called before the first frame update
    

    public void VoidAct()
    {
        StartCoroutine(WaitVoidAct(voidEvent));
    }
    IEnumerator WaitVoidAct(UnityEvent @event)
    {
        yield return new WaitForSeconds(lifeTime);
        @event.Invoke();
    }
    
    public void ValueChangedAct_Bool(bool value)
    {
        StartCoroutine(WaitValueAct(onValueChanged, value));
    }
    IEnumerator WaitValueAct(UnityEvent<bool> @event, bool value)
    {
        yield return new WaitForSeconds(lifeTime);
        @event.Invoke(value);
    }
}
