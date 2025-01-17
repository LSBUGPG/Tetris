﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public class SpriteProcessor : AssetPostprocessor
{
    void OnPostprocessTexture(Texture2D texture)
    {
        string lowerCaseAssetPath = assetPath.ToLower();
        bool isInSpriteDirectory = lowerCaseAssetPath.IndexOf("/sprites/") != -1;

        if (isInSpriteDirectory)
        {
            TextureImporter textureImporter = (TextureImporter) assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
        }
    }
}
