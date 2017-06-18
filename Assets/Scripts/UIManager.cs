using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour {

    private static float ALPHA_INACTIVE = 0.3f;
    private static float ALPHA_ACTIVE = 0.9f;

    [Inject]
    LevelManager sourceLevelManager;

    public Player sourcePlayer;
    public Slider healthSlider;
    public Text healthText;
    public Slider manaSlider;
    public Text manaText;
    public Image ability1;
    public Image ability2;
    public Image ability3;
    public Image quickAccess1;
    public Image quickAccess2;
    public Image quickAccess3;
    public Text levelText;

    private IDictionary<Player.Ability, Image> uiAbilitiesMap;
    private Image[] uiFastActionsList;

    void Start()
    {
        sourcePlayer.HealthChanged += HealthChanged;
        sourcePlayer.ManaChanged += ManaChanged;
        sourcePlayer.AbilityAvailabilityChanged += AbilityAvailabilityChanged;
        sourceLevelManager.LevelChanged += LevelChanged;
        sourcePlayer.FastActionSlotChanged += FastActionSlotChanged;
        uiAbilitiesMap = new Dictionary<Player.Ability, Image> {
            { Player.Ability.PRIMARY,   ability1 },
            { Player.Ability.SECONDARY, ability2 },
            { Player.Ability.TERTIARY,  ability3 }
        };
        uiFastActionsList = new Image[3] { quickAccess1, quickAccess2, quickAccess3 };
    }

    private void FastActionSlotChanged(object sender, int fastActionSlotIndex, string itemMinature)
    {
        uiFastActionsList[fastActionSlotIndex].sprite = Resources.Load<Sprite>(itemMinature);
        if (uiFastActionsList[fastActionSlotIndex].sprite)
        {
            uiFastActionsList[fastActionSlotIndex].color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            uiFastActionsList[fastActionSlotIndex].color = new Color(1f, 1f, 1f, 0f);
        }
    }

    private void AbilityAvailabilityChanged(object sender, Player.Ability ability, bool available, float cooldown)
    {
        if (available)
        {
            uiAbilitiesMap[ability].color =
                new Color(uiAbilitiesMap[ability].color.r, uiAbilitiesMap[ability].color.g, uiAbilitiesMap[ability].color.b, ALPHA_ACTIVE);
        }
        else
        {
            uiAbilitiesMap[ability].color =
              new Color(uiAbilitiesMap[ability].color.r, uiAbilitiesMap[ability].color.g, uiAbilitiesMap[ability].color.b, ALPHA_INACTIVE);
        }
        uiAbilitiesMap[ability].fillAmount = cooldown;
    }

    private void HealthChanged(object sender, int health)
    {
        healthText.text = health.ToString();
        healthSlider.value = health;
    }

    private void ManaChanged(object sender, int mana)
    {
        manaText.text = mana.ToString();
        manaSlider.value = mana;
    }

    private void LevelChanged(object sender, int level)
    {
        levelText.text = level.ToString();
    }

}
