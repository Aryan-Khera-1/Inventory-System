using System;

public class EventController<T, TU>
{
    public event Action<T, TU> baseEvent;
    public void InvokeEvent(T type, TU type2) => baseEvent?.Invoke(type, type2);
    public void AddListener(Action<T, TU> listener) => baseEvent += listener;
    public void RemoveListener(Action<T, TU> listener) => baseEvent -= listener;
}

public class EventController<T>
{
    public event Action<T> baseEvent;
    public void InvokeEvent(T type) => baseEvent?.Invoke(type);
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
}

public class EventController
{
    public event Action baseEvent;
    public void InvokeEvent() => baseEvent?.Invoke();
    public void AddListener(Action listener) => baseEvent += listener;
    public void RemoveListener(Action listener) => baseEvent -= listener;

}
