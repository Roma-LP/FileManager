using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace FileManager
{
    public class ItemUI_Drive : ItemUI
    {
        [SerializeField] private Button _button;

        DriveInfo _driveInfo;

        public DriveInfo DriveInfo => _driveInfo;

        public event Action<DriveInfo> ClickItem;

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
            ClickItem?.Invoke(_driveInfo);
        }

        public void SetItem(Sprite spriteIcon, DriveInfo driveInfo)
        {
            _spriteIcon.sprite = spriteIcon;
            _driveInfo = driveInfo;

            _nameItem.text = driveInfo.Name;
        }
    }
}
