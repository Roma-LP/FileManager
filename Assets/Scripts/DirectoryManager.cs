using Microsoft.Cci;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FileManager
{
    public class DirectoryManager : MonoBehaviour
    {
        [SerializeField] private ItemUI _prefabUI;
        [SerializeField] private Transform _container;
        [SerializeField] private FileTypeIcons _fileTypeIcons;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _buttonBack;

        private string _path;
        //private DirectoryInfo _directoryInfo;
        private DirectoryInfo _directoryCurrent;
        private List<ItemUI> _itemsUI = new List<ItemUI>();

        private void Awake()
        {
            _buttonBack.onClick.AddListener(BackDirectory);
        }

        private void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            _directoryCurrent = new DirectoryInfo(Application.dataPath);

            ShowDirectiry(_directoryCurrent.FullName);
        }

        private void ShowDirectiry(string path)
        {
            _text.text = path;

            if (Directory.Exists(path))
            {
                DirectoryInfo[] dirs2 = _directoryCurrent.GetDirectories();
                foreach (DirectoryInfo s in dirs2)
                {
                    ItemUI itemUI = Instantiate(_prefabUI, _container);
                    _itemsUI.Add(itemUI);
                    itemUI.SetItem(_fileTypeIcons.GetSpriteByType(DirectiryItemType.Folder), s);
                    itemUI.ClickItem += EnterInDirectory;
                }

                FileInfo[] files2 = _directoryCurrent.GetFiles();
                foreach (FileInfo s in files2)
                {
                    ItemUI itemUI = Instantiate(_prefabUI, _container);
                    itemUI.SetItem(_fileTypeIcons.GetSpriteByType(DirectiryItemType.File), s.Directory);
                }
            }
        }

        private void EnterInDirectory(DirectoryInfo directoryInfo)
        {
            ClearDirectorys();
           // _text.text = directoryInfo.FullName;
            _directoryCurrent = directoryInfo;
        }

        private void BackDirectory()
        {
            ClearDirectorys();
            ShowDirectiry(_directoryCurrent.Parent.FullName);
        }

        private void ClearDirectorys()
        {
            for (int i = 0; i < _itemsUI.Count; i++)
            {
                _itemsUI[i].ClickItem -= EnterInDirectory;
                Destroy(_itemsUI[i].gameObject);
            }
        }
    }
}
