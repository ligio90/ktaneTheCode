using System;
using System.Linq;
using System.Text.RegularExpressions;
using TheCode;
using UnityEngine;

using Rnd = UnityEngine.Random;

/// <summary>
/// On the Subject of The Code
/// Created by Livio and Marksam32
/// </summary>
public class TheCodeModule : MonoBehaviour
{
    public KMAudio Audio;
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMBombInfo Bomb;
    public KMBombInfo kmbombinfo;
    public KMSelectable[] NumberButtons;
    public KMSelectable ButtonR;
    public KMSelectable ButtonS;
    public TextMesh Display;
    private int moduleNumber;
    private int solution;
    private int _moduleId;
    private string DOW;
    private int shownnum;

    void Start()
    {
        _moduleId++;
        for (int i = 0; i < NumberButtons.Length; i++)
            NumberButtons[i].OnInteract = GetButtonPressHandler(i);
        ButtonR.OnInteract = BR;
        ButtonS.OnInteract = BS;
        moduleNumber = Rnd.Range(999, 10000);
        Display.text = moduleNumber.ToString();
        LogMessage("Initial module number was {0}", moduleNumber);
        DOW = DateTime.Now.DayOfWeek.ToString();

        if ((Bomb.GetBatteryHolderCount() == 1 && Bomb.GetBatteryCount() == 1 && Bomb.GetOffIndicators().Contains("BOB") && Bomb.GetPortCount() == 1 && Bomb.GetPorts().Contains("Serial")))
        {
            solution = moduleNumber;
            LogMessage("There was exactly one battery in one holder, an unlit BOB and a serial port, solution was the displayed number.");
        }
        else if (Bomb.GetPorts().Count() % 2 == 0)
        {
            solution = (moduleNumber / 23);
            LogMessage("There is an even number of ports, solution was the displayed number divided by 23.");
        }
        else if (DOW == "Sunday")
        {
            solution = (moduleNumber / 8);
            LogMessage("The bomb was started on sunday, solution was the displayed number divided by 8.");
        }
        else if (DOW == "Saturday")
        {
            solution = (moduleNumber / 8);
            LogMessage("The bomb was started on saturday, solution was the displayed number divided by 8.");
        }
        else if (Bomb.GetSolvableModuleNames().Count() % 2 == 0)
        {
            solution = (moduleNumber / 20);
            LogMessage("There is an even amount of modules of the bomb, solution was the displayed number divided by 20.");
        }
        else if (Bomb.GetSolvableModuleNames().Contains("Burglar Alarm"))
        {
            solution = (moduleNumber / 30);
            LogMessage("There is a module called Burglar Alarm on the bomb, solution was the displayed number divided by 30.");
        }
        else if (Bomb.GetSerialNumberNumbers().Last() % 2 == 1)
        {
            solution = (moduleNumber / 42);
            LogMessage("The last digit of the serial number is odd, solution was the displayed number divided by 42.");
        }
        else if (Bomb.GetOnIndicators().Count() == 3)
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
        if (shownnum == solution)
        {
            LogMessage("Correct answer given. Module solved.");
            Display.text = "1234";
            Module.HandlePass();
        }
        else
        {
            LogMessage("Wrong answer given. I expected {0}, but you gave me {1}!", solution, shownnum);
            shownnum = 0;
            Module.HandleStrike();
            Display.text = moduleNumber.ToString();
        }

        return false;
    }

    private bool BR()
    {
        Display.text = moduleNumber.ToString();
        shownnum = 0;
        return false;
    }

    private KMSelectable.OnInteractHandler GetButtonPressHandler(int btn)
    {
        return delegate
        {
            if ((shownnum <= 999))
            {
                shownnum = (shownnum * 10) + btn;
                SetTexts();
            }
            return false;
        };
    }

    private void SetTexts()
    {
        Display.text = shownnum.ToString();
    }

    void LogMessage(string message, params object[] parameters)
    {
        Debug.LogFormat("[The Code #{0}] {1}", _moduleId, string.Format(message, parameters));
    }

#pragma warning disable 414
    private string TwitchHelpMessage = @"Submit the answer with “!{0} submit 1234”. Reset the display with “!{0} reset”.";
#pragma warning restore 414

    KMSelectable[] ProcessTwitchCommand(string command)
    {
        Match m;

        command = command.Trim().ToLowerInvariant();
        if (command == "reset")
            return new[] { ButtonR };
        else if ((m = Regex.Match(command, @"^submit (\d+)$", RegexOptions.IgnoreCase)).Success)
            return m.Groups[1].Value.Select(ch => NumberButtons[ch - '0']).Concat(new[] { ButtonS }).ToArray();
        return null;
    }
}
