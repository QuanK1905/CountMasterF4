using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public class UIAnimator : MonoBehaviour
    {
        // Level Complete Elements
        public RectTransform topRibbon;
        public RectTransform levelCompleteTextContainer;
        public RectTransform starsContainer;
        public RectTransform nextButton;

        // Start is called before the first frame update
        void Start()
        {
            UIManager.onLevelCompleteSet += StartLevelCompleteAnimation;
        }

        private void OnDestroy()
        {
            UIManager.onLevelCompleteSet -= StartLevelCompleteAnimation;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartLevelCompleteAnimation(int starsCount)
        {
            // Start the level complete animation coroutine
           StartCoroutine(LevelCompleteAnimationCoroutine(starsCount));
        }

        IEnumerator LevelCompleteAnimationCoroutine(int starsCount)
        {
            // 1. Hide or more all the components
            Vector2 topRibbonInitialPos = topRibbon.anchoredPosition;
            topRibbon.anchoredPosition += Vector2.up * 250;

            levelCompleteTextContainer.localScale = Vector2.zero;

            // Hide the golden stars
            for (int i = 0; i < starsContainer.childCount; i++)
            {
                Transform goldenStar = starsContainer.GetChild(i).GetChild(0);
                goldenStar.localScale = Vector2.zero;
            }

            // Hide the next level button
            nextButton.localScale = Vector2.zero;

            // 2. Move the top Ribbon down
            float ribbonDownDuration = 0.5f;
            LeanTween.move(topRibbon, topRibbonInitialPos, ribbonDownDuration).setEase(LeanTweenType.easeSpring);
            yield return new WaitForSeconds(ribbonDownDuration);

            // 3. Scale the level complete container
            float levelCompleteContainerDuration = 0.3f;
            LeanTween.scale(levelCompleteTextContainer, Vector2.one, levelCompleteContainerDuration).setEase(LeanTweenType.easeSpring);
            yield return new WaitForSeconds(levelCompleteContainerDuration);

            // 4. Enable the amount of stars
            float bumpDuration = 0.2f;
            for (int i = 0; i < starsCount; i++)
            {
                Transform goldenStar = starsContainer.GetChild(i).GetChild(0);
                LeanTween.scale(goldenStar.gameObject, Vector2.one, bumpDuration).setEase(LeanTweenType.easeSpring);
                yield return new WaitForSeconds(bumpDuration);
            }

            // 5. Enable the next button
            LeanTween.scale(nextButton, Vector2.one, bumpDuration).setEase(LeanTweenType.easeSpring);


        }
    }
}