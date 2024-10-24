using UnityEngine;

public abstract class Setting : ScriptableObject
{
    [SerializeField] protected string title;
    public string Title => title;
    
    public virtual bool isMinvalue { get; }
    public virtual bool isMaxvalue { get; }

    public virtual void SetNextValue() { }
    public virtual void SetPreviousValue() { }

    public virtual object GetValue() { return default(object); }

    public virtual string GetStringValue() { return string.Empty; }

    public virtual void Load() { }
    public virtual void Apply() { }
}
