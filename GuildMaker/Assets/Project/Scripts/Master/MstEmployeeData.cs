using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class MstEmployeeData
{
    [Serializable]
    public class DataFormat
    {
        public ParamTable Param;
    }

    public List<DataFormat> Data = new List<DataFormat>();


}