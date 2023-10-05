public interface IInteractable
{
    /// <summary>Delegate that will be executed by an object that wants to interact with things.</summary>
    /// <note>This will likely be refactored later and be removed</note>
    public delegate void OnInteract();

    /// <summary>A function to execute when a user interaction happens</summary>
    void OnInteraction();
}
