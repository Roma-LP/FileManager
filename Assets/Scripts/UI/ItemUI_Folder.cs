using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace FileManager
{
    public sealed class ItemUI_Folder : ItemUI
    {
        [SerializeField] private Button _button;

        public event Action<DirectoryInfo> ClickItem;

        private void Awake()
        {
            _button.onClick.AddListener(OnClickButton);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnClickButton()
        {
            ClickItem?.Invoke(_directory);
        }

        public void SetItem(Sprite spriteIcon, DirectoryInfo directoryInfo)
        {
            _spriteIcon.sprite = spriteIcon;
            _directory = directoryInfo;

            _nameItem.text = directoryInfo.Name;
        }
    }
}
