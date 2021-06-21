using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFeedback : MonoBehaviour
{
    [SerializeField] private float displayTime = 5;
    [SerializeField] private List<Inventory> inventories;
    [SerializeField] private GameObject feedbackItem;
    [SerializeField] private GameObject feedbackContainer;
    [SerializeField] private Transform feedbackItemsparent;

    private Dictionary<string, FeedbackItem> displayedAddFeedbackItems = new Dictionary<string, FeedbackItem>();
    private Dictionary<string, FeedbackItem> displayedRemoveFeedbackItems = new Dictionary<string, FeedbackItem>();

    private Dictionary<Item, int> recentUnusedItems = new Dictionary<Item, int>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var inventory in inventories)
        {
            inventory.onItemAdded += AddAddFeedbackItem;
            inventory.onItemRemoved += AddRemoveFeedbackItem;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < displayedAddFeedbackItems.Count; i++)
        {
            FeedbackItem displayedFeedbackItem = displayedAddFeedbackItems.Values.ElementAt(i);
            displayedFeedbackItem.displayedTime += Time.deltaTime;

            if (displayedFeedbackItem.displayedTime >= displayTime)
            {
                RemoveFeedbackItem(displayedFeedbackItem, displayedAddFeedbackItems);
                i--;
            }
        }

        for (int i = 0; i < displayedRemoveFeedbackItems.Count; i++)
        {
            FeedbackItem displayedFeedbackItem = displayedRemoveFeedbackItems.Values.ElementAt(i);
            displayedFeedbackItem.displayedTime += Time.deltaTime;

            if (displayedFeedbackItem.displayedTime >= displayTime)
            {
                RemoveFeedbackItem(displayedFeedbackItem, displayedRemoveFeedbackItems);
                i--;
            }
        }
    }

    public class FeedbackItem 
    {
        public Item item;
        public int amount;
        public Image iconImage;
        public Text amountText;
        public GameObject container;
        public float displayedTime;

        public FeedbackItem(Item item, int amount, Image iconImage, Text amountText, GameObject container)
        {
            this.item = item;
            this.amount = amount;
            this.iconImage = iconImage;
            this.amountText = amountText;
            this.container = container;
            this.displayedTime = 0;
        }
    }

    public void AddAddFeedbackItem(Item item, int amount)
    {
        if (inventories.Contains(item.inventory))
        {
            if (displayedRemoveFeedbackItems.ContainsKey(item.name))
            {
                FeedbackItem displayedFeedbackItem = displayedRemoveFeedbackItems[item.name];
                displayedFeedbackItem.amount -= amount;
                if (displayedFeedbackItem.amount <= 0) RemoveFeedbackItem(displayedFeedbackItem, displayedRemoveFeedbackItems);
                else displayedFeedbackItem.amountText.text = "-" + displayedFeedbackItem.amount.ToString();
            }
            else
            {
                recentUnusedItems.Add(item, amount);
            }
            return;
        }
        if (displayedAddFeedbackItems.ContainsKey(item.name))
        {
            FeedbackItem displayedFeedbackItem = displayedAddFeedbackItems[item.name];
            displayedFeedbackItem.amount += amount;
            displayedFeedbackItem.amountText.text = "+" + displayedFeedbackItem.amount.ToString();
            displayedFeedbackItem.displayedTime = 0;
        }
        else
        {
            GameObject placedFeedbackContainer = Instantiate(feedbackContainer, feedbackItemsparent);
            GameObject placedFeedbackItem = Instantiate(feedbackItem, placedFeedbackContainer.transform);
            Image iconImage = placedFeedbackItem.transform.Find("Icon Image").GetComponent<Image>();
            iconImage.sprite = item.icon;
            Text amountText = placedFeedbackItem.transform.Find("Amount Text").GetComponent<Text>();
            amountText.color = Color.green;
            amountText.text = "+" + amount.ToString();
            displayedAddFeedbackItems.Add(item.name, new FeedbackItem(item, amount, iconImage, amountText, placedFeedbackContainer));
        }
    }

    public void AddRemoveFeedbackItem(Item item, int amount)
    {
        if (recentUnusedItems.ContainsKey(item))
        {
            recentUnusedItems.Remove(item);
            return;
        }
        if (displayedRemoveFeedbackItems.ContainsKey(item.name))
        {
            FeedbackItem displayedFeedbackItem = displayedRemoveFeedbackItems[item.name];
            displayedFeedbackItem.amount += amount;
            displayedFeedbackItem.amountText.text = "-" + displayedFeedbackItem.amount.ToString();
            displayedFeedbackItem.displayedTime = 0;
        }
        else
        {
            GameObject placedFeedbackContainer = Instantiate(feedbackContainer, feedbackItemsparent);
            GameObject placedFeedbackItem = Instantiate(feedbackItem, placedFeedbackContainer.transform);
            Image iconImage = placedFeedbackItem.transform.Find("Icon Image").GetComponent<Image>();
            iconImage.sprite = item.icon;
            Text amountText = placedFeedbackItem.transform.Find("Amount Text").GetComponent<Text>();
            amountText.color = Color.red;
            amountText.text = "-" + amount.ToString();
            displayedRemoveFeedbackItems.Add(item.name, new FeedbackItem(item, amount, iconImage, amountText, placedFeedbackContainer));
        }
    }

    public void RemoveFeedbackItem(FeedbackItem displayedFeedbackItem, Dictionary<string, FeedbackItem> displayedFeedbackItems)
    {
        Destroy(displayedFeedbackItem.container);
        displayedFeedbackItems.Remove(displayedFeedbackItem.item.name);
    }
}
