using System;

public interface IPooledObject
{
    public event Action<Resource> Died;

    public void Die();
}