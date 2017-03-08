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

        //Preload the prefabs to decrease lag when drag-selecting

        SimplePool.Preload(highlightPrefab, 500);
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
        while (dragPreviewGameObjects.Count > 0)
        {
            GameObject go = dragPreviewGameObjects[0];
            dragPreviewGameObjects.RemoveAt(0);
            SimplePool.Despawn(go);
        }
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
                        GameObject go = SimplePool.Spawn(highlightPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        go.transform.SetParent(this.transform, true);
                        dragPreviewGameObjects.Add(go);
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
        // camera zoom
        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 20f);
    }
}

