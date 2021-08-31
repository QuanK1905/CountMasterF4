using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JetSystems
{

    public class AnimatedElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        
        public float duration;

        // Start is called before the first frame update
   

        public void OnPointerDown(PointerEventData eventData)
        {
            LeanTween.scale(gameObject, Vector3.one * 0.98f, duration);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            LeanTween.scale(gameObject, Vector3.one, duration);
        }
    }
}