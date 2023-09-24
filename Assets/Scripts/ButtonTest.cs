using System.Collections;
using System.Collections.Generic;
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
    public void OnInteraction()
    {
        Destroy(gameObject);
    }
    #endregion IInteractable
}
