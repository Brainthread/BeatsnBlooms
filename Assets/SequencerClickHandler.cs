using UnityEngine;

public class SequencerClickHandler : MonoBehaviour
{
    [SerializeField] private Camera main_cam;
    [SerializeField] private LayerMask mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //main_cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
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
                print("found tile!");
            }
        }
    }
}
