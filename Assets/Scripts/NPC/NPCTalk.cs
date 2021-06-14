using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCTalk : MonoBehaviour
{
    [SerializeField] private RangeDetection rangeDetection;
    [SerializeField] private GameObject hotkeyCanvas;
    [SerializeField] private string textBeforeAction;
    [SerializeField] private string textAfterAction;
    [SerializeField] private InputActions action;

    private Dictionary<NPCDialog,GameObject> hotkeyCanvasses = new Dictionary<NPCDialog, GameObject>();

    private void Update()
    {
        Dictionary<NPCDialog, GameObject> hotkeyCanvassesInRange = new Dictionary<NPCDialog, GameObject>(hotkeyCanvasses);
        foreach (var dialog in DialogsInRange())
        {
            if (hotkeyCanvassesInRange.ContainsKey(dialog))
            {
                hotkeyCanvassesInRange.Remove(dialog);
                continue;
            }
            GameObject canvas = Instantiate(hotkeyCanvas, dialog.gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            hotkeyCanvasses.Add(dialog, canvas);
            canvas.GetComponentInChildren<HotkeyHint>().SetText(textBeforeAction, textAfterAction, action);
        }
        while (hotkeyCanvassesInRange.Count > 0)
        {
            Destroy(hotkeyCanvassesInRange.Values.First<GameObject>());
            hotkeyCanvasses.Remove(hotkeyCanvassesInRange.Keys.First<NPCDialog>());
            hotkeyCanvassesInRange.Remove(hotkeyCanvassesInRange.Keys.First<NPCDialog>());
        }
    }

    public NPCDialog DialogInRange()
    {
        foreach (var collider2D in rangeDetection.DetectRangeColliders())
        {
            if(collider2D.TryGetComponent<NPCDialog>(out NPCDialog dialog))
            {
                return dialog;
            }
        }
        return null;
    }

    public NPCDialog[] DialogsInRange()
    {
        List<NPCDialog> dialogs = new List<NPCDialog>();
        foreach (var collider2D in rangeDetection.DetectRangeColliders())
        {
            if (collider2D.TryGetComponent<NPCDialog>(out NPCDialog dialog))
            {
                dialogs.Add(dialog);
            }
        }
        return dialogs.ToArray();
    }
}
