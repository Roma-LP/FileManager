using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FileManager
{
    public abstract class ItemUI : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _nameItem;
        [SerializeField] protected Image _spriteIcon;

        protected DirectoryInfo _directory;

        public DirectoryInfo Directory => _directory;
    }
}
