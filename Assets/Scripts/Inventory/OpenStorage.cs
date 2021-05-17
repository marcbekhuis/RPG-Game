using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OpenStorage : MonoBehaviour
{
    [SerializeField] private RangeDetection rangeDetection;
    [SerializeField] private GameObject hotkeyCanvas;
    [SerializeField] private string textBeforeAction;
    [SerializeField] private string textAfterAction;
    [SerializeField] private InputActions action;

    private Dictionary<Inventory,GameObject> hotkeyCanvasses = new Dictionary<Inventory, GameObject>();

    private void Update()
    {
        Dictionary<Inventory, GameObject> hotkeyCanvassesInRange = new Dictionary<Inventory, GameObject>(hotkeyCanvasses);
        foreach (var inventory in StoragesInRange())
        {
            if (hotkeyCanvassesInRange.ContainsKey(inventory))
            {
                hotkeyCanvassesInRange.Remove(inventory);
                continue;
            }
            GameObject canvas = Instantiate(hotkeyCanvas, inventory.gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            hotkeyCanvasses.Add(inventory, canvas);
            canvas.GetComponentInChildren<HotkeyHint>().SetText(textBeforeAction, textAfterAction, action);
        }
        while (hotkeyCanvassesInRange.Count > 0)
        {
            Destroy(hotkeyCanvassesInRange.Values.First<GameObject>());
            hotkeyCanvasses.Remove(hotkeyCanvassesInRange.Keys.First<Inventory>());
            hotkeyCanvassesInRange.Remove(hotkeyCanvassesInRange.Keys.First<Inventory>());
        }
    }

    public Inventory StorageInRange()
    {
        foreach (var collider2D in rangeDetection.DetectRangeColliders())
        {
            if(collider2D.TryGetComponent<Inventory>(out Inventory inventory))
            {
                return inventory;
            }
        }
        return null;
    }

    public Inventory[] StoragesInRange()
    {
        List<Inventory> inventories = new List<Inventory>();
        foreach (var collider2D in rangeDetection.DetectRangeColliders())
        {
            if (collider2D.TryGetComponent<Inventory>(out Inventory inventory))
            {
                inventories.Add(inventory);
            }
        }
        return inventories.ToArray();
    }
}
