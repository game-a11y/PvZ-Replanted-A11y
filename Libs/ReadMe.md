# Put DLLs from `MelonLoader` and `Il2CppAssemblies` here

> NOTE: You need to open the game at least once to generate the `Il2CppAssemblies` folder after install `MelonLoader`.

1. `PVZ Replanted\MelonLoader\net6\*`
2. `PVZ Replanted\Il2CppAssemblies\*`

## mlink (win)

Open a `cmd` terminal:

```cmd
mklink /d "Libs\net6"  "D:\SteamLibrary\steamapps\common\PVZ Replanted\MelonLoader\net6\"
mklink /d "Libs\Il2CppAssemblies"  "D:\SteamLibrary\steamapps\common\PVZ Replanted\MelonLoader\Il2CppAssemblies\"
```
