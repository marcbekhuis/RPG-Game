using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialStep[] tutorialSteps;
    [SerializeField] private Text informationText;
    [SerializeField] private Text inputText;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private Text tutorialSkipText;
    [SerializeField] private InputActions skipAction = InputActions.SkipTutorial;

    private int tutorialStepIndex = 0;

    [System.Serializable]
    public class TutorialStep 
    {
        public InputActions[] actions;
        public string information;
        public string textBeforeInput = "Press ";
        public string textAfterInput = ".";
        public bool anyInput = false;
        [HideInInspector] public List<InputActions> inputPressed = new List<InputActions>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tutorialSkipText.text = "To skip the tutorial press " + '"' + KeycodeVisually.GetVisualKeycode(InputManager.instance.Keybinds[skipAction]) + '"';
        EnableTutorialStep(tutorialSteps[tutorialStepIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.GamePaused() || !tutorial.activeSelf) return;
        if (TutorialStepActionPressed(tutorialSteps[tutorialStepIndex]))
        {
            tutorialStepIndex++;
            if (tutorialStepIndex < tutorialSteps.Length) EnableTutorialStep(tutorialSteps[tutorialStepIndex]);
            else tutorial.SetActive(false);
        }
        if(InputManager.instance.GetKeyDown(skipAction)) tutorial.SetActive(false);
    }

    public bool TutorialStepActionPressed(TutorialStep tutorialStep)
    {
        foreach (var action in tutorialStep.actions)
        {
            if (tutorialStep.inputPressed.Contains(action)) continue;
            if (InputManager.instance.GetKeyDown(action))
            {
                if (tutorialStep.anyInput) return true;
                tutorialStep.inputPressed.Add(action);
            }
        }
        if (tutorialStep.actions.Length == tutorialStep.inputPressed.Count) return true;
        else return false;
    }

    public void EnableTutorialStep(TutorialStep tutorialStep)
    {
        informationText.text = tutorialStep.information;

        string inputInformation = tutorialStep.textBeforeInput;
        for (int i = 0; i < tutorialStep.actions.Length; i++)
        {
            inputInformation += '"' + KeycodeVisually.GetVisualKeycode(InputManager.instance.Keybinds[tutorialStep.actions[i]]) + '"';
            if (i < tutorialStep.actions.Length - 2) inputInformation += ", ";
            else if (i < tutorialStep.actions.Length - 1)
            {
                if (tutorialStep.anyInput) inputInformation += " or ";
                else inputInformation += " and ";
            }
            else inputInformation += tutorialStep.textAfterInput;
        }

        inputText.text = inputInformation;
    }
}
