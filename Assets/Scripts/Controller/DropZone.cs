﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace controller
{
    public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Image Image { get { return GetComponent<Image>(); } }
        private Color Color { set { Image.color = value; } }
        private bool droppableDragged;

        void Start()
        {
            StopDragging();
        }

        public void StartDragging()
        {
            Image.raycastTarget = true;
            droppableDragged = true;
            UpdateHighlights();
        }

        public void StopDragging()
        {
            Image.raycastTarget = false;
            droppableDragged = false;
            UpdateHighlights();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (droppableDragged)
            {
                Color = Color.cyan;
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            UpdateHighlights();
        }

        private void UpdateHighlights()
        {
            if (droppableDragged)
            {
                HighlightAvailability();
            }
            else
            {
                ResetHighlights();
            }
        }

        private void HighlightAvailability()
        {
            Color = Color.green;
        }

        private void ResetHighlights()
        {
            var reset = Color.white;
            reset.a = 0.1f;
            Color = reset;
        }
    }
}