using UnityEngine;

public class PlayerMouseHandler : MonoBehaviour
{
    public static PlayerMouseHandler current;
    [SerializeField] private Camera main_cam;
    [SerializeField] private LayerMask mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = this;
        //main_cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
        if (Input.GetMouseButtonDown(1))
        {
            UnClick();
        }
    }

    public Vector2 GetMouseGridPosition ()
    {
        Ray ray = main_cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector2 playerGridMousePos;
        if (Physics.Raycast(ray, out hit, 9999, mask))
        {
            if (hit.transform.GetComponent<SequencerTile>())
            {
                return new Vector2(-1, -1);
            }
            playerGridMousePos = GridManager.current.WorldPositionToGridPosition(hit.point);
        }
        else
        {
            playerGridMousePos = new Vector2(float.NaN, float.NaN);
        }
        return playerGridMousePos;
    }

    void Click()
    {
        Ray ray = main_cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 9999, mask))
        {
            if(hit.transform.GetComponent<SequencerTile>())
            {
                hit.transform.GetComponent<SequencerTile>().ClickedTile();
            }
            else
            {
                EventHandler.current.OnPlayerClickRow(GetClickedRow());
                print("HANDLED EVENT!");
            }
        }
    }

    void UnClick()
    {
        Ray ray = main_cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 9999, mask))
        {
            if (hit.transform.GetComponent<SequencerTile>())
            {
                hit.transform.GetComponent<SequencerTile>().UnClickedTile();
            }
            else
            {
                EventHandler.current.OnPlayerUnclickRow(GetClickedRow());
            }
        }
    }

    int GetClickedRow ()
    {
        int clickedrow = -1;
        Vector2 mouseGridPos = GetMouseGridPosition();
        mouseGridPos.y = Mathf.Floor(mouseGridPos.y);
        if (mouseGridPos.y >= 0 && mouseGridPos.y <= 3)
        {
            clickedrow = (int)mouseGridPos.y;
        }
        return clickedrow;
    }
}
