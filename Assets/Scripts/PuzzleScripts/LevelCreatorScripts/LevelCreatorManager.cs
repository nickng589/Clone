using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelCreatorManager : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The image used to show whether the player can place")]
    private Image m_canPlaceImage;

    [SerializeField]
    [Tooltip("The drop down to select the objects")]
    private Dropdown m_dropdown;

    [SerializeField]
    [Tooltip("The GameObjects that we can spawn")]
    private GameObject[] m_Objects;

    [SerializeField]
    [Tooltip("The parent objects for the objects (same order)")]
    private GameObject[] m_Parents;


    #endregion

    #region Private Variables
    private Camera p_cam;
    private GameObject currPlaceGO;
    private int p_numObjects;
    private GameObject p_parent;
    private bool canPlace;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (m_Objects.Length != m_Parents.Length)
        {
            throw new System.ArgumentException("Input Lists must be same size", "original");
        }
        currPlaceGO = m_Objects[0];
        p_parent = m_Parents[0];
        p_numObjects = m_Objects.Length;
        p_cam = Camera.main;
        canPlace = true;
        m_dropdown.onValueChanged.AddListener(delegate {DropdownValueChanged(m_dropdown);});
        m_canPlaceImage.color = Color.green;
    }

    void DropdownValueChanged(Dropdown change)
    {
        currPlaceGO = m_Objects[change.value];
        p_parent = m_Parents[change.value];
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            canPlace = !canPlace;
            if(canPlace)
            {
                m_canPlaceImage.color = Color.green;
            }
            else
            {
                m_canPlaceImage.color = Color.red;
            }
        }

        if (canPlace && Input.GetMouseButton(0) )
        {
            Vector2 spawnPos = p_cam.ScreenToWorldPoint(Input.mousePosition);
            spawnPos = new Vector2((float)Math.Round(spawnPos.x),(float)Math.Round(spawnPos.y));
            RaycastHit2D[] hits = Physics2D.RaycastAll(spawnPos, new Vector2(0, 0), 0.1f);  //remove all objects in a location before placing the next object
            foreach (RaycastHit2D hit in hits)
            {
                if(hit.transform.gameObject.tag == currPlaceGO.tag)
                {
                    Destroy(hit.transform.gameObject);
                } 
            }
            var newObj = Instantiate(currPlaceGO, spawnPos, Quaternion.identity);
            newObj.transform.parent = p_parent.transform;
        }
        if (Input.GetMouseButton(1))
        {

            Vector2 deletePos = p_cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(deletePos, new Vector2(0, 0), 0.1f);
            foreach (RaycastHit2D hit in hits)
            {
                Destroy(hit.transform.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            Vector2 rotatePos = p_cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(rotatePos, new Vector2(0, 0), 0.1f);
            foreach (RaycastHit2D hit in hits)
            {
                if(hit.transform.tag == "Conveyor")
                {
                    int dir = hit.transform.GetComponent<ConveyorBeltController>().direction;
                    dir -= 1;
                    if (dir == 0)
                        dir = 4;
                    hit.transform.GetComponent<ConveyorBeltController>().direction = dir;
                }
                hit.transform.Rotate(new Vector3(0, 0, 90));
            }
        }
    }


}
