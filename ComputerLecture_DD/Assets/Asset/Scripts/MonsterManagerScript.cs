using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MonsterManagerScript : MonoBehaviour {

    [Header("RoomManage Settings")]
    public int currentMonsterIndex = -1;
    public List<MonsterScript> monsterScriptList = null;
}
