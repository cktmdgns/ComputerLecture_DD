using UnityEngine;
using System.Collections;

public class RoomClickScript : MonoBehaviour {

    public enum ClickType
    {
        Default ,Room, Road
    }
    private Camera _mainCam = null;
    private ClickType mouseState;
    private GameObject target;


    public RoomManagerScript roomManagerScript = null;
    

    void Awake()
    {
        _mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            target = GetClickedObject();

            if (target != null && target.name.Contains("Room") == true)
            {
                mouseState = ClickType.Room;
            }
            else if (target != null && target.name.Contains("Road") == true)
            {
                mouseState = ClickType.Road;
            }
        }
        else
        {
            mouseState = ClickType.Default;
        }

        if(mouseState == ClickType.Room)
        {
            roomManagerScript.currentRoomIndex = System.Convert.ToInt32(target.name.Substring(4));
            roomManagerScript.RoomManageOpen();
        }
        else if(mouseState == ClickType.Road)
        {
        }
    }

    private GameObject GetClickedObject()
    {
        //UI가 클릭되면 종료
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return null;
        }
        
        //충돌이 감지된 영역
        RaycastHit hit;
        //찾은 오브젝트
        GameObject target = null;

        //마우스 포이트 근처 좌표를 만든다.
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        //마우스 근처에 오브젝트가 있는지 확인
        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {
            target = hit.collider.gameObject;
        }

        return target;
    }

}
