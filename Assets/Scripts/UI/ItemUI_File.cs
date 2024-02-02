using System.IO;
using UnityEngine;

namespace FileManager
{
    public sealed class ItemUI_File : ItemUI
    {
        public void SetItem(Sprite spriteIcon, FileInfo fileInfo)
        {
            _spriteIcon.sprite = spriteIcon;
            _directory = fileInfo.Directory;

            _nameItem.text = fileInfo.Name;
        }
    }
}
