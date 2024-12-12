using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void Invoke();
}

public interface Isubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notyfy();
}
