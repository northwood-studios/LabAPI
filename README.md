<p align="center">
  <a href="https://scpslgame.com">
    <picture>
      <source srcset="https://github.com/user-attachments/assets/04f437c8-7fa9-4da9-bf71-fa39db141cf2" media="(prefers-color-scheme: dark)">
      <img src="https://github.com/user-attachments/assets/aa811a11-2b8e-4397-972d-27c7305318d7" width="125" alt="NW logo">
    </picture>
  </a>
</p>

<h1 align="center">LabAPI</h1>
<h6 align="center"><a href="https://store.steampowered.com/app/700330/SCP_Secret_Laboratory/">SCP: Secret Laboratory</a>'s official server-side modding framework.</h6>
<div align="center">
	<a href="https://plugins.scpslgame.com">Plugins</a>
  <span> - </span>
  <a href="https://github.com/northwood-studios/LabAPI/wiki">Docs</a>
  <span> - </span>
	<a href="https://github.com/northwood-studios/LabAPI/issues">Report Bug</a>
  <span> - </span>
  <a href="https://discord.gg/scpsl">Discord</a>
  <p></p>
</div> 

<div align="center">

[![Version](https://img.shields.io/github/v/release/northwood-studios/LabAPI?sort=semver&style=flat-square&color=8DBBE9&label=Version)]()
[![License](https://img.shields.io/github/license/northwood-studios/LabAPI?style=flat-square&label=License&color=df967f)]()
[![Contributors](https://img.shields.io/github/contributors-anon/northwood-studios/LabAPI?color=90E59A&style=flat-square&label=Contributors)]()
[![GitHub Issues](https://img.shields.io/github/issues/northwood-studios/LabAPI.svg?style=flat-square&label=Issues&color=d77982)](https://github.com/northwood-studios/LabAPI/issues)
[![Discord](https://img.shields.io/discord/330432627649544202?color=738adb&label=Discord&logo=discord&logoColor=white&style=flat-square)](https://discord.gg/scpsl)

</div>

The LabAPI project is **[SCP: Secret Laboratory](https://store.steampowered.com/app/700330/SCP_Secret_Laboratory/)**'s official server-side plugin loader and framework. It facilitates development of plugins by providing wrappers and events around various game mechanics found throughout the game.

## Documentation
Code should have self-explanatory documentation, but there's only so much you can explain through comments!
- For guides, tips and tricks, our main source of documentation can be found [here](https://github.com/northwood-studios/LabAPI/wiki).
- For a more practical approach, examples can be found [here](https://github.com/northwood-studios/LabAPI/tree/master/LabApi.Examples).

## Installation
All **SCP: SL Dedicated Server** builds are bundled with a compiled **`LabAPI.dll`**, so you don't need to install it if you are hosting a server.

However, [releases](https://github.com/northwood-studios/LabAPI/releases) may occasionally occur *before* the dedicated server is updated, while we advice you wait for an official update, you can always apply the updated release manually.

To do so, you should update the **`LabAPI.dll`** file located within: `%DEDICATED_SERVER_PATH%/SCPSL_Data/Managed/`.