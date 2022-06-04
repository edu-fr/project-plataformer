using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPlataformer
{
    public class ItemScript : MonoBehaviour
    {
        public enum ItemType
        {
            Donut = 1,
            Pepper = 2
        }

        public ItemType itemType;

        public void SetType(ItemType type)
        {
            itemType = type;
        }
    }
}
