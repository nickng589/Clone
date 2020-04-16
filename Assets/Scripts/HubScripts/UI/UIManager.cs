using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The slider to adjust move speed")]
    private Slider m_MoveSpeedSlider;

    [SerializeField]
    [Tooltip("The text for move speed slider")]
    private Text m_MoveSpeedText;

    [SerializeField]
    [Tooltip("The text for controls")]
    private Text m_ControlsText;

    [SerializeField]
    [Tooltip("The button to exit the game")]
    private Button m_exitButton;

    [SerializeField]
    [Tooltip("The button to reset the game")]
    private Button m_resetButton;

    [SerializeField]
    [Tooltip("The menu background")]
    private GameObject m_menuBackgroundSprite;

    [SerializeField]
    [Tooltip("The speed of player")]
    private float m_defaultSpeed;

    [SerializeField]
    [Tooltip("The Game Manager (Only if in puzzle)")]
    private GameObject m_GameManager;

    private bool menuOpen = false;
    private Color menuSpriteOn;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("MoveSpeed"))
        {
            PlayerPrefs.SetFloat("MoveSpeed", m_defaultSpeed);
        }

        m_MoveSpeedSlider.value = 7 - (PlayerPrefs.GetFloat("MoveSpeed") - m_defaultSpeed) / 0.0f;
        
        if (m_resetButton != null)
        {
            m_resetButton.onClick.AddListener(delegate { ResetButtonPressed(); });
        }
        m_MoveSpeedSlider.onValueChanged.AddListener(delegate { SliderValueChanged(m_MoveSpeedSlider); });
        m_exitButton.onClick.AddListener(delegate { ExitButtonPressed(); });
        closeMenu();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (menuOpen)
            {
                closeMenu();
            }
            else
            {
                openMenu();
            }
        }
    }

    void closeMenu()
    {
        if(m_resetButton!=null)
        {
            m_resetButton.gameObject.SetActive(false);
        }
        m_menuBackgroundSprite.SetActive(false);
        m_MoveSpeedSlider.gameObject.SetActive(false);
        m_ControlsText.gameObject.SetActive(false);
        m_exitButton.gameObject.SetActive(false);
        m_MoveSpeedText.gameObject.SetActive(false);
        menuOpen = false;
    }

    void openMenu()
    {
        if (m_resetButton != null)
        {
            m_resetButton.gameObject.SetActive(true);
        }
        m_menuBackgroundSprite.SetActive(true);
        m_MoveSpeedSlider.gameObject.SetActive(true);
        m_ControlsText.gameObject.SetActive(true);
        m_exitButton.gameObject.SetActive(true);
        m_MoveSpeedText.gameObject.SetActive(true);
        menuOpen = true;
    }

    #region UI responses
    void SliderValueChanged(Slider changed)
    {
        float newMoveSpeed = m_defaultSpeed + (7 - changed.value) * 0.05f;
        PlayerPrefs.SetFloat("MoveSpeed", newMoveSpeed);  
        if(m_GameManager != null)
        {
            m_GameManager.GetComponent<GameManager>().UpdateMoveSpeed(newMoveSpeed);
        }
    }

    void ExitButtonPressed()
    {
        Application.Quit();
    }

    void ResetButtonPressed()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("HubWorld");
    }

    #endregion

}
