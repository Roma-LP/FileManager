using Microsoft.Cci;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

namespace FileManager
{
    public class DirectoryManager : MonoBehaviour
    {
        [SerializeField] private ItemUI_Folder _prefabUIFolder;
        [SerializeField] private ItemUI_File _prefabUIFile;
        [SerializeField] private ItemUI_Drive _prefabUIDrive;
        [SerializeField] private Transform _container;
        [SerializeField] private FileTypeIcons _fileTypeIcons;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonHome;
        [SerializeField] private Button _buttonRefresh;

        private string _path;
        private DirectoryInfo _directoryCurrent;
        public List<ItemUI_Folder> _itemsUIFolder = new List<ItemUI_Folder>();
        public List<ItemUI_File> _itemsUIFile = new List<ItemUI_File>();
        public List<ItemUI_Drive> _itemsUIDrive = new List<ItemUI_Drive>();

        private void Awake()
        {
            _buttonBack.onClick.AddListener(BackDirectory);
            _buttonRefresh.onClick.AddListener(RefreshDirectory);
            _buttonHome.onClick.AddListener(ShowHome);
        }

        private void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
            _buttonRefresh.onClick.RemoveAllListeners();
            _buttonHome.onClick.RemoveAllListeners();
        }

        private void Start()
        {
#if UNITY_EDITOR
            _directoryCurrent = new DirectoryInfo(Application.dataPath);
#endif
#if PLATFORM_ANDROID
            _directoryCurrent = new DirectoryInfo("/storage/emulated/0");
#endif
            ShowDirectiry(_directoryCurrent);
        }

        private void ShowDirectiry(DirectoryInfo path)
        {
            if (Directory.Exists(path.FullName))
            {
                ClearDirectorys();

                _text.text = path.FullName;
                _directoryCurrent = path;

                DirectoryInfo[] dirs2 = path.GetDirectories();
                foreach (DirectoryInfo s in dirs2)
                {
                    ItemUI_Folder itemUI = Instantiate(_prefabUIFolder, _container);
                    _itemsUIFolder.Add(itemUI);
                    itemUI.SetItem(_fileTypeIcons.GetSpriteByType(DirectiryItemType.Folder), s);
                    itemUI.ClickItem += EnterInDirectory;
                }

                FileInfo[] files2 = path.GetFiles();
                foreach (FileInfo s in files2)
                {
                    ItemUI_File itemUI = Instantiate(_prefabUIFile, _container);
                    _itemsUIFile.Add(itemUI);
                    itemUI.SetItem(_fileTypeIcons.GetSpriteByType(DirectiryItemType.File), s);
                }
            }
        }

        private void EnterInDirectory(DirectoryInfo directoryInfo)
        {
            ShowDirectiry(directoryInfo);
        }

        private void BackDirectory()
        {
            if (_directoryCurrent.Parent is not null)
            {
                ShowDirectiry(_directoryCurrent.Parent);
            }
            else
            {
                ShowHome();
            }
        }

        private void ClearDirectorys()
        {
            for (int i = 0; i < _itemsUIFolder.Count; i++)
            {
                _itemsUIFolder[i].ClickItem -= EnterInDirectory;
                Destroy(_itemsUIFolder[i].gameObject);
            }
            _itemsUIFolder.Clear();

            for (int i = 0; i < _itemsUIFile.Count; i++)
            {
                Destroy(_itemsUIFile[i].gameObject);
            }
            _itemsUIFile.Clear();

            for (int i = 0; i < _itemsUIDrive.Count; i++)
            {
                _itemsUIDrive[i].ClickItem -= EnterInDrive;
                Destroy(_itemsUIDrive[i].gameObject);
            }
            _itemsUIDrive.Clear();
        }

        private void RefreshDirectory()
        {
            _directoryCurrent = new DirectoryInfo(Application.dataPath);
            ShowDirectiry(_directoryCurrent);
        }

        private void ShowHome()
        {
            ClearDirectorys();
            _text.text = "\\";
            DriveInfo[] driveInfos = DriveInfo.GetDrives();

            foreach (DriveInfo drive in driveInfos)
            {
                ItemUI_Drive itemUI = Instantiate(_prefabUIDrive, _container);
                _itemsUIDrive.Add(itemUI);
                itemUI.SetItem(_fileTypeIcons.GetSpriteByType(DirectiryItemType.Disk), drive);
                itemUI.ClickItem += EnterInDrive;
            }
        }

        private void EnterInDrive(DriveInfo driveInfo)
        {
            _directoryCurrent = new DirectoryInfo(driveInfo.Name);
            ClearDirectorys();
            ShowDirectiry(_directoryCurrent);
        }
    }
}
