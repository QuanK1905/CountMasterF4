using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JetSystems
{
    
    public class ShopManager : MonoBehaviour
    {
        #region Delegates
        public delegate void OnRewardedVideoPressed(int amount);
        public static OnRewardedVideoPressed onRewardedVideoPressed;

        public delegate void OnItemSelected(int itemIndex);
        public static OnItemSelected onItemSelected;

        #endregion

        [Header(" Managers ")]
        public UIManager uiManager;

        [Header(" Settings ")]
        public Transform itemParent;
        public Transform buttonsParent;
        public Sprite[] itemsSprites;
        ShopButton[] shopButtons;

        [Header(" Unlocking ")]
        public int itemPrice;
        public GameObject backButton;
        public Button unlockRandomButton;
        public Text unlockRandomPriceText;
        bool unlocking;

        [Header(" Rendering ")]
        public Transform rotatingItemParent;

        [Header(" Design ")]
        public Sprite lockedSprite;
        public Sprite unlockedSprite;

        [Header(" Sounds ")]
        public AudioSource randomSound;
        public AudioSource unlockedItemSound;

        [Header(" Rewarded Video ")]
        public int rewardAmount;
        public Text rewardVideoAmountText;

        // Start is called before the first frame update
        void Start()
        {
            // Configure some Texts
            rewardVideoAmountText.text = "+" + rewardAmount;
            unlockRandomPriceText.text = itemPrice.ToString();

            // Load data
            LoadData();

            // Add the listeners to the buttons
            AddListeners();
        }

        private void OnEnable()
        {
            OpenShop();
        }



        void LoadData()
        {
            // Unlock the first item
            UnlockItem(0);

            shopButtons = new ShopButton[buttonsParent.childCount];

            if(buttonsParent.childCount > itemsSprites.Length)
            {
                Debug.LogError("Not enough Sprites added to the itemsSprites Array, configuration Impossible");
                return;
            }

            for (int i = 0; i < buttonsParent.childCount; i++)
            {
                // Is this item unlocked ?
                bool unlocked = IsItemUnlocked(i);
                ShopButton shopButton = buttonsParent.GetChild(i).GetComponent<ShopButton>();
                shopButton.Configure(itemsSprites[i]);

                if (i == 0)
                    shopButton.SetSelected(true);

                shopButtons[i] = shopButton;

                if (unlocked)
                {
                    // Change the color of the gradient
                    shopButton.SetContainerSprite(unlockedSprite);
                }
                else
                {
                    // Set the button as not interactable
                    shopButton.Lock();

                    shopButton.SetContainerSprite(lockedSprite);
                }
            }


            // Configure the unlock button
            if (UIManager.COINS < itemPrice)
                unlockRandomButton.interactable = false;


        }

        public void OpenShop()
        {
            // If we don't have enough money, disable the unlock button
            if (UIManager.COINS >= itemPrice)
                unlockRandomButton.interactable = true;
            else
                unlockRandomButton.interactable = false;

            // Hide the unlock button if all the items are already unlocked
            if (AllItemsUnlocked())
                unlockRandomButton.gameObject.SetActive(false);
        }

        bool AllItemsUnlocked()
        {
            int unlockedCounter = 0;

            for (int i = 0; i < buttonsParent.childCount; i++)
            {
                if (IsItemUnlocked(i))
                    unlockedCounter++;
            }

            return unlockedCounter == buttonsParent.childCount;
        }

        void AddListeners()
        {
            // Add listeners to the buttons
            int k = 0;
            foreach (Button b in buttonsParent.GetComponentsInChildren<Button>())
            {
                int _k = k;
                b.onClick.AddListener(delegate { SelectItem(_k); });
                k++;
            }
        }

        void SelectItem(int itemIndex)
        {
 

            for (int i = 0; i < shopButtons.Length; i++)
            {
                if (i == itemIndex)
                    shopButtons[i].SetSelected(true);
                else
                    shopButtons[i].SetSelected(false);
            }

            ShowItem(itemIndex);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                UnlockItem();
        }

        public void CloseShop()
        {
            uiManager.CloseShop();
        }

        public void UnlockItem()
        {
            if (!unlocking)
            {
                unlocking = true;

                // If we can click, it means we have enough money
                StartCoroutine("UnlockItemCoroutine");

                // Re Configure the shop
                OpenShop();
            }
        }

        List<int> unlockableItems;
        IEnumerator UnlockItemCoroutine()
        {
            // At first, make a list of all possible items we can unlock

            // Diminish the amount of money
            UIManager.AddCoins(-itemPrice);


            if (unlockableItems == null)
                unlockableItems = new List<int>();
            else
                unlockableItems.Clear();

            for (int i = 0; i < buttonsParent.childCount; i++)
            {
                if (!IsItemUnlocked(i))
                    unlockableItems.Add(i);
            }

            // If we don't have any unlockable items at this point
            // It means all of them have been unlocked
            if (unlockableItems.Count > 0)
            {
                // First phase, loop around
                float suspenseDuration = 3f;
                float timer = 0;
                float diminish = Time.deltaTime;

                float timeToSwitch = Time.deltaTime;
                float switchTimer = 0;

                int unlockableIndex = unlockableItems[0];

                for (int i = 0; i < shopButtons.Length; i++)
                {
                    if (i == unlockableIndex)
                        shopButtons[i].SetSelected(true);
                    else
                        shopButtons[i].SetSelected(false);
                }


                while (timer < suspenseDuration)
                {

                    switchTimer += Time.deltaTime;

                    if (switchTimer >= timeToSwitch)
                    {
                        timeToSwitch += Time.deltaTime;
                        switchTimer = 0;

                        // Switch
                        unlockableIndex = unlockableItems[Random.Range(0, unlockableItems.Count)];

                        Switch(unlockableIndex);

                        if (randomSound != null)
                            randomSound.Play();

                    }

                    timer += Time.deltaTime;
                    yield return null;
                }


                // Unlock that unlockable index Item


                Switch(unlockableIndex);
                shopButtons[unlockableIndex].Unlock();
                shopButtons[unlockableIndex].SetContainerSprite(unlockedSprite);

                UnlockItem(unlockableIndex);
                ShowItem(unlockableIndex);

                if (unlockedItemSound != null)
                    unlockedItemSound.Play();
            }

            unlocking = false;

            yield return null;
        }

        void Switch(int index)
        {
            for (int i = 0; i < shopButtons.Length; i++)
            {
                if (i == index)
                    shopButtons[i].SetSelected(true);
                else
                    shopButtons[i].SetSelected(false);
            }
        }


        void ShowItem(int itemIndex)
        {
            // Callback message
            onItemSelected?.Invoke(itemIndex);

            for (int i = 0; i < rotatingItemParent.childCount; i++)
            {
                if (i == itemIndex)
                {
                    rotatingItemParent.GetChild(i).gameObject.SetActive(true);

                    //TODO : This is replaced by the callback up here
                    //itemParent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    rotatingItemParent.GetChild(i).gameObject.SetActive(false);
                    //itemParent.GetChild(i).gameObject.SetActive(false);
                }
            }

            if (randomSound != null)
                randomSound.Play();


        }

        public void RewardVideoButtonCallback()
        {
            onRewardedVideoPressed?.Invoke(rewardAmount);
        }

        bool IsItemUnlocked(int itemIndex)
        {
            return PlayerPrefsManager.GetItemUnlockedState(itemIndex) == 1;
            //return (PlayerPrefs.GetInt("ITEMUNLOCKED" + itemIndex) == 1);
        }

        void UnlockItem(int itemIndex)
        {
            PlayerPrefsManager.SetItemUnlockedState(itemIndex, 1);
            //PlayerPrefs.SetInt("ITEMUNLOCKED" + itemIndex, 1);
        }
    }
}