using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour{

    public enum MonsterType
    {
        Default, Monster, Prisoner, MC
    }

    public string name = "";
    public MonsterType type = MonsterType.Default;
    public int roomIndex = -1;

    public int LV = -1;
    public int Atk = -1;
    public int Def = -1;
    public int value = -1;
}
