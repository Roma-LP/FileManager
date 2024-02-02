using System;
using System.Collections.Generic;
using UnityEngine;

namespace FileManager
{
    [CreateAssetMenu(order = 47)]
    public class FileTypeIcons : ScriptableObject
    {
        [SerializeField] private List<FileTypeData> _fileTypeData;
        [SerializeField] private Sprite _defaultSprite;

        public Sprite GetSpriteByType(DirectiryItemType directiryItemType)
        {
            for(int i = 0; i < _fileTypeData.Count; i++)
            {
                if(_fileTypeData[i].DirectiryItemType == directiryItemType)
                {
                    return _fileTypeData[i].Sprite;
                }
            }

            return _defaultSprite;
        }

        [Serializable]
        private class FileTypeData
        {
            [SerializeField] private Sprite _sprite;
            [SerializeField] private DirectiryItemType _directiryItemType;

            public Sprite Sprite => _sprite;
            public DirectiryItemType DirectiryItemType => _directiryItemType;
        }
    }
}
