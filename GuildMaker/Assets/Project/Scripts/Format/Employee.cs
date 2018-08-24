using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee
{
    public string Name { get; set; }
    public int Level { get; set; }
    public ParamTable Param { get; set; }


}

public class ParamTable
{
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Spd { get; set; }
    public int Mag { get; set; }
}
