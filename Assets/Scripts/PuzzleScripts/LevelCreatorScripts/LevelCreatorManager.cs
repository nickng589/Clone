﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelCreatorManager : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The GameObjects that we can spawn")]
    private GameObject[] m_Objects;

    [SerializeField]
    [Tooltip("The button to press to spawn those objects (same order)")]
    private string[] m_Buttons;

    [SerializeField]
    [Tooltip("The Texts corrisponding to those objects (same order)")]
    private Text[] m_Texts;

    [SerializeField]
    [Tooltip("The parent objects for the objects (same order)")]
    private GameObject[] m_Parents;

    #endregion

    #region Private Variables
    private Camera p_cam;
    private GameObject currPlaceGO;
    private int p_numObjects;
    private GameObject p_parent;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (m_Objects.Length != m_Texts.Length || m_Texts.Length != m_Buttons.Length || m_Buttons.Length != m_Parents.Length)
        {
            throw new System.ArgumentException("Input Lists must be same size", "original");
        }
        currPlaceGO = m_Objects[0];
        m_Texts[0].color = Color.yellow;
        p_numObjects = m_Objects.Length;
        p_cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i<p_numObjects; i++)
        {
            if(Input.GetKeyDown(m_Buttons[i]))
            {
                currPlaceGO = m_Objects[i];

                foreach(Text tex in m_Texts)
                {
                    tex.color = Color.white;
                }
                m_Texts[i].color = Color.yellow;
                p_parent = m_Parents[i];
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 spawnPos = p_cam.ScreenToWorldPoint(Input.mousePosition);
            spawnPos = new Vector2((float)Math.Round(spawnPos.x),(float)Math.Round(spawnPos.y));
            RaycastHit2D[] hits = Physics2D.RaycastAll(spawnPos, new Vector2(0, 0), 0.1f);  //remove all objects in a location before placing the next object
            foreach (RaycastHit2D hit in hits)
            {
                Destroy(hit.transform.gameObject);
            }
            var newObj = Instantiate(currPlaceGO, spawnPos, Quaternion.identity);
            newObj.transform.parent = p_parent.transform;
        }
        if (Input.GetMouseButton(1))
        {
            Vector2 deletePos = p_cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(deletePos, new Vector2(0, 0), 0.1f);
            foreach(RaycastHit2D hit in hits)
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
                hit.transform.Rotate(new Vector3(0, 0, 90));
            }
        }
    }
}
