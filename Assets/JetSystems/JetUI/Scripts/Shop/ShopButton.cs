using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JoshH.UI;

namespace JetSystems
{
    public class ShopButton : MonoBehaviour
    {
        [Header(" Settings ")]
        public Image containerImage;
        public Image itemImage;
        public Image contour;
        Button thisButton;

        private void Awake()
        {
            thisButton = GetComponent<Button>();
        }

        // Start is called before the first frame update
        void Start()
        {
            if(thisButton == null)
                thisButton = GetComponent<Button>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Lock()
        {
            itemImage.gameObject.SetActive(false);
            thisButton.interactable = false;
        }

        public void Unlock()
        {
            itemImage.gameObject.SetActive(true);
            thisButton.interactable = true;
        }

        public void Configure(Sprite sprite)
        {
            itemImage.sprite = sprite;
        }

        public void SetSelected(bool state)
        {
            if (state)
                contour.color = new Color(1, 1, 1, 1);
            else
                contour.color = new Color(1, 1, 1, 0.15f);
        }

        public void SetContainerSprite(Sprite containerSprite)
        {
            containerImage.sprite = containerSprite;
        }
    }
}