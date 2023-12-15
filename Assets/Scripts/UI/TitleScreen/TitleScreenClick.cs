using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TitleScreenClick : MonoBehaviour
{
    public TextMeshProUGUI screenText;

    public UnityEvent titleTransitioned;

    private void Update()
    {
        // Get all keys down
        bool key_now = Input.anyKeyDown;
            
        // Filter out mouse inputs
        for (int i = 0; i < 3; i++)
            if (key_now && Input.GetMouseButtonDown(i))
                key_now = false;

        if (key_now) TransitionTitle();
    }

    public void TransitionTitle() {
        titleTransitioned.Invoke();
    }
}
