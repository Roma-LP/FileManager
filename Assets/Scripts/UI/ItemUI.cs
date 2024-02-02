using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FileManager
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameItem;
        [SerializeField] private Image _spriteIcon;
        [SerializeField] private Button _button;

        private DirectoryInfo _directory;

        public DirectoryInfo Directory => _directory;

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
