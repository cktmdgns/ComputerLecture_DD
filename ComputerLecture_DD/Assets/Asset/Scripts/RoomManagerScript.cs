using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RoomManagerScript : MonoBehaviour {

    public MonsterManagerScript monsterManagerScript = null;

    [Header("RoomManage Object")]
    public GameObject roomManageObj = null;
    public GameObject monsterObj = null;
    public GameObject monsterManageObj = null;
    public GameObject prisonObj = null;
    public GameObject prisonManageObj = null;
    public GameObject roomTypeChangeObj = null;
    public GameObject monsterScrollviewIndexObj = null;
    public Transform monsterScrollviewIndexTransform = null;
    public RectTransform monsterScrollviewPort = null;
    public Text roomTypeText = null;
    public Text roomNameText = null;
    public List<Text> monsterRoomIndexTextList = null;
    public List<Button> inRoomMonsterButtenList = null;

    [Header("RoomManage Settings")]
    public int currentRoomIndex = -1;
    public int currentRoadIndex = -1;
    public int currentInRoomMonsterIndex = -1;
    public List<RoomScript> roomList = null;
    public List<RoadScript> roadList = null;

    public enum RoomType
    {
        Default, Boss, Monster, Prison, Experiment
    }


    void Start()
    {
        for(int i=0; i<3; i++)
        {
            int tempIndex = i;
            inRoomMonsterButtenList[i].onClick.AddListener(() => { AddListener_InRoomMonsterIndex(tempIndex); });
        }

        MakeMonsterScrollviewIndex();
    }

    void AddListener_InRoomMonsterIndex(int i)
    {
        currentInRoomMonsterIndex = i;
    }

    void MakeMonsterScrollviewIndex()
    {
        int monsterCount = 0;

        for(int i=0; i< monsterManagerScript.monsterScriptList.Count; i++)
        {
            GameObject newObject = Instantiate(monsterScrollviewIndexObj) as GameObject;
            newObject.name = "index" + i;
            newObject.transform.SetParent(monsterScrollviewIndexTransform);
            newObject.transform.localScale = new Vector3(1, 1, 1);
            newObject.transform.localPosition = new Vector3(225, -70 - 110 * i, 0);
            newObject.transform.FindChild("Text").GetComponent<Text>().text = monsterManagerScript.monsterScriptList[i].name;

            int tempIndex = i;
            newObject.GetComponent<Button>().onClick.AddListener(() => { AddListener_MonsterScrollview(tempIndex); });

            if (monsterManagerScript.monsterScriptList[i].type != MonsterScript.MonsterType.Default)
            {
                newObject.SetActive(true);
                monsterCount++;
            }
        }
        monsterScrollviewPort.sizeDelta = new Vector2(450,120 * monsterCount);
    }

    void AddListener_MonsterScrollview(int i)
    {
        monsterManagerScript.monsterScriptList[i].roomIndex = currentRoomIndex;
        UpdateMonsterRoomIndex();
        MonsterManageClose();
    }

    void UpdateMonsterRoomIndex()
    {
        int count = 0;
        int j = 0;
        string tempText = "";
        for (int i=0; i<3; i++)
        {
            for(; j<50; j++)
            {
                if (monsterManagerScript.monsterScriptList[j].roomIndex == currentRoomIndex)
                {
                    tempText = "LV" + monsterManagerScript.monsterScriptList[j].LV + "  " + monsterManagerScript.monsterScriptList[j].name;
                    tempText += "\n" + "Atk" + monsterManagerScript.monsterScriptList[j].Atk + "  Def" + monsterManagerScript.monsterScriptList[j].Def + "  Value" + monsterManagerScript.monsterScriptList[j].value + "$";
                    monsterRoomIndexTextList[i].text = tempText;
                    j++;
                    count++;
                    break;
                }
            }
        }
        for(;count<3;count++)
        {
            monsterRoomIndexTextList[count].text = "비어있음";
        }
    }


    public void RoomManageOpen()
    {
        roomManageObj.SetActive(true);
        roomNameText.text = roomList[currentRoomIndex].roomName;

        switch (roomList[currentRoomIndex].type)
        {
            case RoomType.Default:
                Debug.Log("Default Error");
                break;
            case RoomType.Boss:
            case RoomType.Monster:
                if (roomList[currentRoomIndex].type == RoomType.Boss)
                {
                    roomTypeText.text = "보스룸";
                }
                else if (roomList[currentRoomIndex].type == RoomType.Monster)
                {
                    roomTypeText.text = "몬스터룸";
                }
                currentInRoomMonsterIndex = -1;
                prisonObj.SetActive(false);
                monsterManageObj.SetActive(false);
                monsterObj.SetActive(true);
                UpdateMonsterRoomIndex();
                break;
            case RoomType.Prison:
                roomTypeText.text = "감옥";
                monsterObj.SetActive(false);
                prisonObj.SetActive(true);
                break;
            case RoomType.Experiment:
                roomTypeText.text = "실험실";
                break;
            default:
                break;
        }
    }
    public void RoomManageClose()
    {
        roomManageObj.SetActive(false);
        currentRoomIndex = -1;
    }

    //---------------------------------------------------------
    // 몬스터 관리
    public void MonsterManageOpen()
    {
        monsterManageObj.SetActive(true);
    }
    public void MonsterCancle()
    {
        int tempIndex = 0;
        for(int i=0; i<50; i++)
        {
            if(monsterManagerScript.monsterScriptList[i].roomIndex == currentRoomIndex)
            {
                if(tempIndex == currentInRoomMonsterIndex)
                {
                    monsterManagerScript.monsterScriptList[i].roomIndex = -1;
                    break;
                }
                tempIndex++;
            }
        }
        MonsterManageClose();
        UpdateMonsterRoomIndex();
    }
    public void MonsterManageClose()
    {
        monsterManageObj.SetActive(false);
    }

    //---------------------------------------------------------
    // 감옥 관리 
    public void PrisonManageOpen()
    {
        prisonManageObj.SetActive(true);
    }
    public void PrisonManageClose()
    {
        prisonManageObj.SetActive(false);
    }





    //---------------------------------------------------------
    // 룸 타입 변경 
    public void RoomTypeChangeOpen()
    {
        roomTypeChangeObj.SetActive(true);
    }
    public void ChangeRoomType_BossRoom()
    {
        roomTypeChangeObj.SetActive(false);

        roomList[currentRoomIndex].type = RoomType.Boss;
        RoomManageOpen();
    }
    public void ChangeRoomType_MonsterRoom()
    {
        roomTypeChangeObj.SetActive(false);

        roomList[currentRoomIndex].type = RoomType.Monster;
        RoomManageOpen();
    }
    public void ChangeRoomType_PrisonRoom()
    {
        roomTypeChangeObj.SetActive(false);

        roomList[currentRoomIndex].type = RoomType.Prison;
        RoomManageOpen();
    }
    public void ChangeRoomType_ExpRoom()
    {
        roomTypeChangeObj.SetActive(false);

        roomList[currentRoomIndex].type = RoomType.Experiment;
        RoomManageOpen();
    }
}
