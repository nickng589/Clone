using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The time it takes the player to move (in seconds)")]
    private float m_speed;

    private Vector2 upV = new Vector2(0, 1);
    private Vector2 downV = new Vector2(0, -1);
    private Vector2 rightV = new Vector2(1, 0);
    private Vector2 leftV = new Vector2(-1, 0);
    private LayerMask p_wallMask;
    // Start is called before the first frame update
    void Start()
    {
        p_wallMask = LayerMask.GetMask("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int PushUp(int distance)
    {
        for (int i = Mathf.Abs(distance); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
        {
            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + upV, upV, i - 1, p_wallMask))
            {              
                StartCoroutine(MoveUpCoroutine(i));
                return i;
            }
        }
        return 0;
    }

    public int PushDown(int distance)
    {
        for (int i = Mathf.Abs(distance); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
        {
            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + downV, downV, i - 1, p_wallMask))
            {
                StartCoroutine(MoveDownCoroutine(i));
                return i;
            }
        }
        return 0;
    }

    public int PushRight(int distance)
    {
        for (int i = Mathf.Abs(distance); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
        {
            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + rightV, rightV, i - 1, p_wallMask))
            {
                StartCoroutine(MoveRightCoroutine(i));
                return i;
            }
        }
        return 0;
    }

    public int PushLeft(int distance)
    {
        for (int i = Mathf.Abs(distance); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
        {
            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + leftV, leftV, i - 1, p_wallMask))
            {
                StartCoroutine(MoveLeftCoroutine(i));
                return i;
            }
        }
        return 0;
    }


    IEnumerator MoveUpCoroutine(int distance)
    {
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + distance, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MoveDownCoroutine(int distance)
    {
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - distance, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MoveLeftCoroutine(int distance)
    {
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x - distance, gameObject.transform.position.y, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MoveRightCoroutine(int distance)
    {
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x + distance, gameObject.transform.position.y, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
