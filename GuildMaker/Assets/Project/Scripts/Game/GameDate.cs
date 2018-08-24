using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲーム内時間



public class GameDate
{
    public class LoopRange
    {
        //min以上 max未満
        int _min;
        int _max;

        public int Value { get; set; }

        public LoopRange(int min, int max)
        {
            _min = min;
            _max = max;
            Value = _min;

        }
        public int Advance(int next)
        {
            int _isLoop = 0;
            Value += next;
            if (Value >= _max)
            {
                Value = _min + (Value - _max);
                _isLoop = 1;
            }
            if (Value < _min)
            {
                Value = _max + (Value - _min);
                _isLoop = -1;
            }
            return _isLoop;
        }
    }
    public int _year { get; set; }
    public LoopRange _month { get; set; }
    public LoopRange _week { get; set; }
    public LoopRange _hour { get; set; }
    public GameDate(int y = 1, int m = 1, int w = 1, int h = 0)
    {
        InitDate(y, m, w, h);
    }
    void InitDate(int y, int m, int w, int h)
    {
        _year = y;
        _month = new LoopRange(1, 13);
        _month.Value = m;
        _week = new LoopRange(1, 5);
        _week.Value = w;
        _hour = new LoopRange(0, 24);
        _hour.Value = h;
    }
    //時間を進める next時間
    public void Advance(int next = 1)
    {
        _year += _month.Advance(_week.Advance(_hour.Advance(next)));
    }
}