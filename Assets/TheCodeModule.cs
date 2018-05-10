using UnityEngine;
using Rnd = UnityEngine.Random;
using TheCode;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;


//The Code Module Created by ligio90 and Marksam CREATED BY THEM REALLY ©2018 THEM GIVE THEM ALL YO MONEY
//The Code Module Created by ligio90 and Marksam CREATED BY THEM REALLY ©2018 THEM GIVE THEM ALL YO MONEY
//The Code Module Created by ligio90 and Marksam CREATED BY THEM REALLY ©2018 THEM GIVE THEM ALL YO MONEY
//The Code Module Created by ligio90 and Marksam CREATED BY THEM REALLY ©2018 THEM GIVE THEM ALL YO MONEY
//The Code Module Created by ligio90 and Marksam CREATED BY THEM REALLY ©2018 THEM GIVE THEM ALL YO MONEY

public class TheCodeModule : MonoBehaviour
{
    public KMAudio Audio;
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMBombInfo Bomb;
    public KMBombInfo kmbombinfo;
    public KMSelectable Button1;
    public KMSelectable Button2;
    public KMSelectable Button3;
    public KMSelectable Button4;
    public KMSelectable Button5;
    public KMSelectable Button6;
    public KMSelectable Button7;
    public KMSelectable Button8;
    public KMSelectable Button9;
    public KMSelectable Button0;
    public KMSelectable ButtonR;
    public KMSelectable ButtonS;
    public TextMesh Display;
    private int moduleNumber;
    private int solution;
    private int _moduleId;
    private string DOW;
    private int cleardisp = 1;
    private int shownnum;


    void Start()
    {
        _moduleId++;
        Button0.OnInteract = B0;
        Button1.OnInteract = B1;
        Button2.OnInteract = B2;
        Button3.OnInteract = B3;
        Button4.OnInteract = B4;
        Button5.OnInteract = B5;
        Button6.OnInteract = B6;
        Button7.OnInteract = B7;
        Button8.OnInteract = B8;
        Button9.OnInteract = B9;
        ButtonR.OnInteract = BR;
        ButtonS.OnInteract = BS;
        moduleNumber = Rnd.Range(999, 10000);
        Display.text = moduleNumber.ToString();
        LogMessage("Initial module number was {0}", moduleNumber);
        DOW = System.DateTime.Now.DayOfWeek.ToString();


        if ((Bomb.GetBatteryHolderCount() == 1 && Bomb.GetBatteryCount() == 1 && Bomb.GetOffIndicators().Contains("BOB") && Bomb.GetPortCount() == 1 && Bomb.GetPorts().Contains("Serial")))
        {
            solution = moduleNumber;
            LogMessage("There was exactly one battery in one holder, an unlit BOB and a serial port, solution was the displayed number.");
        }

        else
            if (Bomb.GetPorts().Count() % 2 == 0)
        {
            solution = (moduleNumber / 23);
            LogMessage("There is an even number of ports, solution was the displayed number divided by 23.");
        }

        else
        if (DOW == "Sunday")
        {
            solution = (moduleNumber / 8);
            LogMessage("The bomb was started on sunday, solution was the displayed number divided by 8.");
        }
        else
        if (DOW == "Saturday")
        {
            solution = (moduleNumber / 8);
            LogMessage("The bomb was started on saturday, solution was the displayed number divided by 8.");
        }
        else
        if (Bomb.GetSolvableModuleNames().Count() % 2 == 0)
        {
            solution = (moduleNumber / 20);
            LogMessage("There is an even amount of modules of the bomb, solution was the displayed number divided by 20.");
        }
        else
        if (Bomb.GetSolvableModuleNames().Contains("Burglar Alarm"))
        {
            solution = (moduleNumber / 30);
            LogMessage("There is a module called Burglar Alarm on the bomb, solution was the displayed number divided by 30.");
        }
        else
        if (Bomb.GetSerialNumberNumbers().Last() % 2 == 1)
        {
            solution = (moduleNumber / 42);
            LogMessage("The last digit of the serial number is odd, solution was the displayed number divided by 42.");
        }
        else
        if (Bomb.GetOnIndicators().Count() == 3)
        {
            solution = (moduleNumber / 69);
            LogMessage("There are exactly 3 lit indicators, solution was the displayed number divided by 69.");
        }
        else
        {
            solution = (moduleNumber / 12);
            LogMessage("No rule applied, solution was the displayed number divided by 12.");
        }


        LogMessage("Correct answer was {0}", solution);
    }

    private bool BS()
    {
        if (shownnum == solution) {
            LogMessage("Correct answer given. Module solved.");
            Display.text = "1234";
            Module.HandlePass();
                }
        else
        {
            cleardisp = 1;
            LogMessage("Wrong answer given. I expected {0}, but you gave me {1}!", solution, shownnum);
            Module.HandleStrike();
            Display.text = moduleNumber.ToString();
            
        }

            return false;
    }

    private bool BR()
    {
        Display.text = "";
        shownnum = 0;

        return false;
    }

    private bool B9()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999))
        {
            shownnum = (shownnum * 10) + 9;
            SetTexts();
        }
        return false;
    }

    private bool B8()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999))
        {
            shownnum = (shownnum * 10) + 8;
            SetTexts();
        }
        return false;
    }

    private bool B7()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999))
        {
            shownnum = (shownnum * 10) + 7;
            SetTexts();
        }
        return false;
    }

    private bool B6()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999))
        {
            shownnum = (shownnum * 10) + 6;
            SetTexts();
        }
        return false;
    }


    private bool B5()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999))
        {
            shownnum = (shownnum * 10) + 5;
            SetTexts();
        }
        return false;
    }

    private bool B4()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999))
        {
            shownnum = (shownnum * 10) + 4;
            SetTexts();
        }
        return false;
    }

    private bool B3()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999))
        {
            shownnum = (shownnum * 10) + 3;
            SetTexts();
        }
        return false;
    }

    private bool B2()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999))
        {
            shownnum = (shownnum * 10) + 2;
            SetTexts();
        }
        return false;
    }

    private bool B1()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999))
        {
            shownnum = (shownnum * 10) + 1;
            SetTexts();
        }
        return false;
    }

    private bool B0()
    {
        if (cleardisp == 1)
            Display.text = "";
        cleardisp = 0;
        if ((shownnum <= 999)) {
            shownnum = (shownnum * 10) + 0;
            SetTexts();
        }
        return false;

    }

    private bool SetTexts()
    {
        Display.text = shownnum.ToString();


        return false;
    }



    void LogMessage(string message, params object[] parameters)
    {
        Debug.LogFormat("[The Code #{0}] {1}", _moduleId, string.Format(message, parameters));


    }


}


