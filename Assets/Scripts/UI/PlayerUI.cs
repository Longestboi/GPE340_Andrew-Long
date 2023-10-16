using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerUI : MonoBehaviour
{
    #region Fields
    public Health health;
    private TextMeshProUGUI text;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        health = GetComponentInParent<Controller>().pawn.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("Health: {0}", health.health);
    }
    #endregion MonoBehaviour
}
