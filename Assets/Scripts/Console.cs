using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    [SerializeField] private int maxConsoleMessages = 10;
    [SerializeField] private GameObject console;
    [SerializeField] private InputField consoleInput;
    [SerializeField] private Transform consoleOutput;
    [SerializeField] private Text consoleText;
    [SerializeField] private Inventory playerInventory;

    private Queue<Text> consoleTexts = new Queue<Text>();

    private void Start()
    {
        consoleInput.onEndEdit.AddListener(CommitCommand);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.GetKeyDown(InputManager.Actions.OpenConsole))
        {
            if (console.activeSelf) console.SetActive(false);
            else
            {
                console.SetActive(true);
                consoleInput.ActivateInputField();
            }
        }
    }

    private void CommitCommand(string command)
    {
        string[] args = command.ToLower().Split(' ');

        switch (args[0])
        {
            case "give":
                if(args.Length < 3)
                {
                    ConsoleMessage("This command needs atleast 3 arguments.");
                    break;
                }
                GiveItem(args);
                consoleInput.text = "";
                break;
            default:
                ConsoleMessage("Command not found");
                consoleInput.text = "";
                break;
        }

        consoleInput.ActivateInputField();
    }

    private void GiveItem(string[] args)
    {
        foreach (var item in AllItems.instance.items)
        {
            if (item.name.ToLower() == args[1])
            {
                if (int.TryParse(args[2], out int amount))
                {
                    playerInventory.AddItem(new Item(item, amount));
                    ConsoleMessage(amount + " of item " + item.name + " Added to the players inventory");
                    return;
                }
                else
                {
                    ConsoleMessage("The item was found, but no valid amount was given.");
                    return;
                }
            }
            else if (item.name.ToLower() == args[1] + " " + args[2] && args.Length > 3)
            {
                if (int.TryParse(args[3], out int amount))
                {
                    playerInventory.AddItem(new Item(item, amount));
                    ConsoleMessage(amount + " of item " + item.name + " Added to the players inventory");
                    return;
                }
                else
                {
                    ConsoleMessage("The item was found, but no valid amount was given.");
                    return;
                }
            }
        }
        ConsoleMessage("Item could not be found.");
    }

    private void ConsoleMessage(string message)
    {
        Text consoleMessage = Instantiate(consoleText, consoleOutput);
        consoleMessage.text = message;
        consoleTexts.Enqueue(consoleMessage);
        if(consoleTexts.Count >= maxConsoleMessages)
        {
            Destroy(consoleTexts.Dequeue().gameObject);
        }
    }
}
