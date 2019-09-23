using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Date
{
    public int day;
    public int month;
    public int year;
}

public class WorldHandler : MonoBehaviour
{
    public float dayLength;
    float dayTimer;
    int date;
    int month;
    int[] daysInMonths = new int[12]
    {
        31,
        28,
        31,
        30,
        31,
        30,
        31,
        31,
        30,
        31,
        30,
        31

    };
    int year;

    //Date presentDay = new Date();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DateHandler();
        
    }

    void DateHandler()
    {
        
        dayTimer += Time.deltaTime;
        if (dayTimer >= dayLength)
        {
            dayTimer = 0;
            date += 1;
        }

        if (date >= daysInMonths[month])
        {
            date = 0;
            month += 1;
        }

        if (month >= 12)
        {
            month = 0;
            year += 1;
        }

        print((date + 1) + "/" + (month + 1) + "/" + (year + 1));
    }

    void DateCheck()
    {
        month = Mathf.Clamp(month, 1, 12);
        date = Mathf.Clamp(date, 1, daysInMonths[month]);
        dayTimer = Mathf.Clamp(dayTimer, 0, dayLength);
        if (year < 1)
        {
            year = 1;
        }
    }

    void NewDay()
    {

    }
}
