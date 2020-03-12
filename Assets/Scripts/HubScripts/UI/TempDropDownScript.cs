using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TempDropDownScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The drop down to select the levels")]
    private Dropdown m_dropdown;

    [SerializeField]
    [Tooltip("The names of the levels to change to")]
    private string[] m_scenes;
    // Start is called before the first frame update
    void Start()
    {
        m_dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(m_dropdown); });
    }
    void DropdownValueChanged(Dropdown change)
    {
        SceneManager.LoadScene(m_scenes[change.value-1]);
    }

}
