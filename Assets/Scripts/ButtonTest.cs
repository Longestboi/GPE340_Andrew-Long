using UnityEngine;

public class ButtonTest : MonoBehaviour, IInteractable
{

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){}
    #endregion MonoBehaviour

    #region IInteractable
    // Demo function to test interation thing...
    public void OnInteraction()
    {
        Destroy(gameObject);
    }
    #endregion IInteractable
}
