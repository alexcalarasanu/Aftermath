using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    Vector3 lastFPSPosition;
    Vector3 currentFPSPosition;

    public GameObject highlightPrefab;

    Vector3 startDragCoord;
    List<GameObject> dragPreviewGameObjects;

    // Use this for initialization
    void Start()
    {
        dragPreviewGameObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        currentFPSPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentFPSPosition.z = 0;

        //UpdateHighlight();
        UpdateCameraMovement();
        UpdateDragging();

        lastFPSPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFPSPosition.z = 0;
    }

    //void UpdateHighlight()
    //{
    //    //update highlight
    //    Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCoord(currentFPSPosition);
    //    if (tileUnderMouse != null)
    //    {
    //        highlight.SetActive(enabled);
    //        Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
    //        highlight.transform.position = cursorPosition;
    //    }
    //    else
    //    {
    //        highlight.SetActive(false);
    //    }
    //}

    void UpdateDragging()
    {
        //LEFT CLICK
        //start
        if (Input.GetMouseButtonDown(0))
        {
            startDragCoord = currentFPSPosition;
        }
        int startX = Mathf.FloorToInt(startDragCoord.x);
        int endX = Mathf.FloorToInt(currentFPSPosition.x);
        int startY = Mathf.FloorToInt(startDragCoord.y);
        int endY = Mathf.FloorToInt(currentFPSPosition.y);

        if (endX < startX)
        {
            int temp = endX;
            endX = startX;
            startX = temp;
        }
        if (endY < startY)
        {
            int temp = endY;
            endY = startY;
            startY = temp;
        }
        //Cleanup past previews
        for (int i = 0; i < dragPreviewGameObjects.Count; i++)
        {
            Destroy(dragPreviewGameObjects[i]);
        }
        dragPreviewGameObjects.Clear();
        //Holding
        if (Input.GetMouseButton(0))
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                    if (t != null)
                    {
                      dragPreviewGameObjects.Add((GameObject)Instantiate(highlightPrefab, new Vector3(x, y, 0), Quaternion.identity));
                    }

                }
            }
        }
        //end
        if (Input.GetMouseButtonUp(0))
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                }
            }
        }
    }

    void UpdateCameraMovement()
    {
        // camera movement
        if (Input.GetMouseButton(1)) //Right mouse button
        {
            Vector3 difference = lastFPSPosition - currentFPSPosition;
            Camera.main.transform.Translate(difference);
        }

    }
}

