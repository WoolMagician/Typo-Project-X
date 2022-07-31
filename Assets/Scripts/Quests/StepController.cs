using UnityEngine;

//this script needs to be put on the actor, and takes care of the current step to accomplish.
//the step contains a dialogue and maybe an event.
public class StepController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ActorSO _actor = default;
    [SerializeField] private DialogueDataSO _defaultDialogue = default;
    [SerializeField] private QuestManagerSO _questData = default;
    [SerializeField] private GameStateSO _gameStateManager = default;

    [Header("Listening to channels")]
    [SerializeField] private VoidEventChannelSO _winDialogueEvent = default;
    [SerializeField] private VoidEventChannelSO _loseDialogueEvent = default;
    [SerializeField] private IntEventChannelSO _endDialogueEvent = default;

    [Header("Broadcasting on channels")]
    public DialogueDataChannelSO _startDialogueEvent = default;

    [Header("Dialogue Shot Camera")]
    public GameObject dialogueShot;

    //check if character is actif. An actif character is the character concerned by the step.
    private DialogueDataSO _currentDialogue;

    public bool _isInDialogue; //Consumed by the state machine

    private void Start()
    {
        if (dialogueShot)
        {
            dialogueShot.transform.parent = null;
            dialogueShot.SetActive(false);
        }
    }

    //start a dialogue when interaction
    //some Steps need to be instantanious. And do not need the interact button.
    //when interaction again, restart same dialogue.
    public void InteractWithCharacter()
    {
        if (_gameStateManager.CurrentGameState == GameState.Gameplay)
        {
            DialogueDataSO dialogueToDisplay = _questData.InteractWithCharacter(_actor, false, false);

            if (dialogueToDisplay != null)
            {
                this.StartDialogue(dialogueToDisplay);
            }
            else
            {
                this.StartDialogue(_defaultDialogue);
            }
        }
    }

    void StartDialogue(DialogueDataSO dialogue)
    {
        if (dialogue == null) return;

        //Set current dialogue
        _currentDialogue = dialogue;

        //Raise start dialouge event
        _startDialogueEvent.RaiseEvent(_currentDialogue);

        //Attach end dialogue handler
        _endDialogueEvent.OnEventRaised += EndDialogue;

        //Make sure to stop any previous dialogues
        this.StopConversation();

        //Attach win and lose events
        _winDialogueEvent.OnEventRaised += PlayWinDialogue;
        _loseDialogueEvent.OnEventRaised += PlayLoseDialogue;

        //Set is in dialogue
        this._isInDialogue = true;

        this.SetDialogueCamera(true);
    }

    void EndDialogue(int dialogueType)
    {
        _endDialogueEvent.OnEventRaised -= EndDialogue;
        _winDialogueEvent.OnEventRaised -= PlayWinDialogue;
        _loseDialogueEvent.OnEventRaised -= PlayLoseDialogue;

        this.ResumeConversation();
        this._isInDialogue = false;
        this.SetDialogueCamera(false);
    }

    private void SetDialogueCamera(bool flag)
    {
        if (dialogueShot)
            dialogueShot.SetActive(flag);
    }

    void PlayLoseDialogue()
    {
        if (_questData != null)
        {
            DialogueDataSO displayDialogue = _questData.InteractWithCharacter(_actor, true, false);
            StartDialogue(displayDialogue);           
        }
    }

    void PlayWinDialogue()
    {
        if (_questData != null)
        {
            DialogueDataSO displayDialogue = _questData.InteractWithCharacter(_actor, true, true);
            StartDialogue(displayDialogue);
        }
    }

    private void StopConversation()
    {
        GameObject[] talkingTo = gameObject.GetComponent<NPC>().talkingTo;
        if (talkingTo != null)
        {
            for (int i = 0; i < talkingTo.Length; ++i)
            {
                talkingTo[i].GetComponent<NPC>().npcState = NPCState.Idle;
            }
        }
    }

    private void ResumeConversation()
    {
        GameObject[] talkingTo = GetComponent<NPC>().talkingTo;
        if (talkingTo != null)
        {

            for (int i = 0; i < talkingTo.Length; ++i)
            {
                talkingTo[i].GetComponent<NPC>().npcState = NPCState.Talk;
            }
        }
    }
}