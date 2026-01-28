using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace _Source
{
    public class PlatformGenerator : MonoBehaviour
    {
        [Header("General Settings")]
        [SerializeField] private int platformCount;
        [SerializeField] private int maxHeight;
        [SerializeField] private int minHeight;
        [SerializeField] private int maxGap;
        [SerializeField] private int minGap;
        [SerializeField] private int maxWidth;
        [SerializeField] private int minWidth;
        [Header("Platform Settings")]
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Tile _tile1;
        [SerializeField] private Tile _tile2;
        [SerializeField] private Tile _tile3;
        
        private Random _random;
        private int _height;
        private int _width;
        private int _gap;
        private int _offset;
        private void Awake()
        {
            _random = new Random();
            _offset = 0;
            new Random().Next();
            CreatePlatform();
        }

        private void CreatePlatform()
        {
            for (int i = 0; i < platformCount; i++)
            {
                _height = _random.Next(minHeight, maxHeight + 1);
                _gap = _random.Next(minGap, maxGap + 1);
                _width = _random.Next(minWidth, maxWidth + 1);
                int platformStart = _offset + _gap;
                for (int j = 0; j < _width; j++)
                {
                    int x = platformStart + j;
                     Tile usedTile;
                    if (j == 0)
                        usedTile = _tile1;
                    else if (j == _width - 1)
                        usedTile = _tile3;
                    else
                        usedTile = _tile2;
                    tilemap.SetTile(new Vector3Int(x, _height, 0), usedTile);
                }
                _offset = platformStart + _width;
            }
        }
    }
}
