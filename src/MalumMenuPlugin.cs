﻿using BepInEx;
using BepInEx.Unity.IL2CPP;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using System.Collections.Generic;
using HarmonyLib;

namespace MalumMenu;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
public partial class MalumPlugin : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);
    public static string malumVersion = "1.1.2";
    public static List<string> supportedAU = new List<string> { "2023.7.12", "2023.7.11" };
    private static MenuUI menuUI;

    public override void Load()
    {
        Harmony.PatchAll();
        menuUI = AddComponent<MenuUI>();

        SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>) ((scene, _) =>
        {
            if (scene.name == "MainMenu")
            {
                ModManager.Instance.ShowModStamp();

                //Warn about unsupported AU versions
                if (!supportedAU.Contains(Application.version)){
                    Utils.showPopup("\nThis version of MalumMenu and this version of Among Us are incompatible\n\nInstall the right version to avoid problems");
                }
            }
        }));
    }

}

