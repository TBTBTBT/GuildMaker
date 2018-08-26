using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee
{
    public string Name { get; set; }
    public int Level { get; set; }
    public ParamTable Param { get; set; }
    public int Sat { get; set; }//満足
    public int Cdt { get; set; }//調子

}

public class ParamTable
{
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Spd { get; set; }
    public int Mag { get; set; }
    public int Salary { get; set; }
}
public class Recruit{
    public Employee Employee;
    public int Week = 2;

}