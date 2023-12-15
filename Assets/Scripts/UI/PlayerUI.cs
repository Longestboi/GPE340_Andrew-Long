using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerUI : MonoBehaviour
{
    #region Fields
    public GameManager gameManager;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI livesText;

    private Image weaponImage;
    private Health health;

    private WeaponType _currentWeaponType = WeaponType.None;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        healthText = transform.Find("HealthText").GetComponent<TextMeshProUGUI>();
        livesText = transform.Find("LivesText").GetComponent<TextMeshProUGUI>();
        weaponImage = transform.Find("WeaponImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.playerController.pawn) return;
        if (!gameManager.playerController.pawn.weapon) _currentWeaponType = WeaponType.None;

        if (_currentWeaponType != gameManager.playerController.pawn.weapon.weaponType)
        {
            _currentWeaponType = gameManager.playerController.pawn.weapon.weaponType;
            weaponImage.sprite = gameManager.WeaponIcons[_currentWeaponType];
        }

        healthText.text = string.Format("Health: {0}", gameManager.playerController.pawn.GetComponent<Health>().health);
        livesText.text = string.Format("Lives: {0}", gameManager.playerController.lives.ToString("D2"));
    }
    #endregion MonoBehaviour
}
