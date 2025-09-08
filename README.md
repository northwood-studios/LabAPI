[![GitHub release](https://flat.badgen.net/github/release/northwood-studios/LabAPI/)](https://github.com/northwood-studios/NwPluginAPI/releases/) [![NuGet](https://flat.badgen.net/nuget/v/Northwood.LabAPI/latest)](https://www.nuget.org/packages/Northwood.LabAPI/) [![License](https://flat.badgen.net/github/license/northwood-studios/LabAPI)](https://github.com/northwood-studios/LabAPI/blob/master/LICENSE) [![Discord Invitation](https://flat.badgen.net/discord/members/scpsl?icon=discord)](https://discord.gg/scpsl)
# LabAPI
The LabAPI project is **[SCP: Secret Laboratory](https://store.steampowered.com/app/700330/SCP_Secret_Laboratory/)**'s official server-side plugin loader and framework. It facilitates development of plugins by providing wrappers and events around various game mechanics found throughout the game.

## Documentation
Code should have self-explanatory documentation, but there's only so much you can explain through comments!
- For guides, tips and tricks, our main source of documentation can be found [here](https://github.com/northwood-studios/LabAPI/wiki).
- For a more practical approach, examples can be found [here](https://github.com/northwood-studios/LabAPI/tree/master/LabApi.Examples).

## Installation
All **SCP: SL Dedicated Server** builds are bundled with a compiled **`LabAPI.dll`**, so you don't need to install it if you are hosting a server.

However, [releases](https://github.com/northwood-studios/LabAPI/releases) may occasionally occur *before* the dedicated server is updated, while we advice you wait for an official update, you can always apply the updated release manually.

To do so, you should update the **`LabAPI.dll`** file located within: `%DEDICATED_SERVER_PATH%/SCPSL_Data/Managed/`.