using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    public RoomManagerScript roomManagerScript = null;
    public MonsterManagerScript monsterManagerScript = null;

    void Awake()
    {
        ParameterLoad();
    }

    void OnApplicationQuit()
    {
        ParameterSave();
    }

    void ParameterLoad()
    {
        Debug.Log("ParameterLoad");
        for(int i=0; i< roomManagerScript.roomList.Count; i++)
        {
            roomManagerScript.roomList[i].roomName = PlayerPrefs.GetString("RoomName_" + i, "");
            roomManagerScript.roomList[i].type = (RoomManagerScript.RoomType)PlayerPrefs.GetInt("RoomType_" + i, -1);
        }

        for (int i = 0; i < monsterManagerScript.monsterScriptList.Count; i++)
        {
            monsterManagerScript.monsterScriptList[i].name = PlayerPrefs.GetString("MonsterName_" + i, "");
            monsterManagerScript.monsterScriptList[i].type = (MonsterScript.MonsterType)PlayerPrefs.GetInt("MonsterType_" + i, -1);
            monsterManagerScript.monsterScriptList[i].roomIndex = PlayerPrefs.GetInt("MonsterRoomIndex_" + i, -1);
            monsterManagerScript.monsterScriptList[i].LV = PlayerPrefs.GetInt("MonsterLV_" + i, -1);
            monsterManagerScript.monsterScriptList[i].Atk = PlayerPrefs.GetInt("MonsterAtk_" + i, -1);
            monsterManagerScript.monsterScriptList[i].Def = PlayerPrefs.GetInt("MonsterDef_" + i, -1);
            monsterManagerScript.monsterScriptList[i].value = PlayerPrefs.GetInt("MonsterValue_" + i, -1);
        }
    }

    void ParameterSave()
    {
        Debug.Log("ParameterSave");
        for (int i = 0; i < roomManagerScript.roomList.Count; i++)
        {
            PlayerPrefs.SetString("RoomName_" + i, roomManagerScript.roomList[i].roomName);
            PlayerPrefs.SetInt("RoomType_" + i, (int)roomManagerScript.roomList[i].type);
        }

        for (int i = 0; i < monsterManagerScript.monsterScriptList.Count; i++)
        {
            PlayerPrefs.SetString("MonsterName_" + i, monsterManagerScript.monsterScriptList[i].name);
            PlayerPrefs.SetInt("MonsterType_" + i, (int)monsterManagerScript.monsterScriptList[i].type);
            PlayerPrefs.SetInt("MonsterRoomIndex_" + i, monsterManagerScript.monsterScriptList[i].roomIndex);
            PlayerPrefs.SetInt("MonsterLV_" + i, monsterManagerScript.monsterScriptList[i].LV);
            PlayerPrefs.SetInt("MonsterAtk_" + i, monsterManagerScript.monsterScriptList[i].Atk);
            PlayerPrefs.SetInt("MonsterDef_" + i, monsterManagerScript.monsterScriptList[i].Def);
            PlayerPrefs.SetInt("MonsterValue_" + i, monsterManagerScript.monsterScriptList[i].value);
        }
    }
}
