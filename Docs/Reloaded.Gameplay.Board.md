```cs
		// Token: 0x0400134B RID: 4939
		[Token(Token = "0x400134B")]
		[FieldOffset(Offset = "0xF0")]
		public GameButton mMenuButton;

		// Token: 0x0400134C RID: 4940
		[Token(Token = "0x400134C")]
		[FieldOffset(Offset = "0xF8")]
		public GameButton mStoreButton;
```

## 植物相关函数

### 植物管理函数

- `GetTopPlantAt` - 获取指定位置的顶层植物
- `CanPlantAt` - 检查是否可以在指定位置种植
- `RemoveAllPlants` - 移除所有植物
- `KillAllPlantsInRadius` - 杀死指定半径内的所有植物
- `CountPlantByType` - 按类型统计植物数量
- `FindUmbrellaPlant` - 查找雨伞叶植物
- `SpecialPlantHitTest` - 特殊植物点击测试

### 植物种植相关函数

- `PlantingPixelToGridX` - 将种植像素坐标转换为网格X坐标
- `PlantingPixelToGridY` - 将种植像素坐标转换为网格Y坐标
- `PlantingRequirementsMet` - 检查种植需求是否满足
- `GetCurrentPlantCost` - 获取当前植物成本
- `PlantUsesAcceleratedPricing` - 检查植物是否使用加速定价
- `OffsetYForPlanting` - 为种植调整Y偏移

### 植物特殊功能函数

- `HasValidCobCannonSpot` - 检查是否有有效的玉米加农炮位置
- `IsValidCobCannonSpot` - 检查是否为有效的玉米加农炮位置
- `IsValidCobCannonSpotHelper` - 玉米加农炮位置检查辅助函数
- `IsPlantInGoldWateringCanRange` - 检查植物是否在金水壶范围内


## 僵尸相关函数

### 僵尸管理函数

- `AddZombie` - 添加僵尸
- `AddZombieInRow` - 在指定行添加僵尸
- `AddZombieAtCell` - 在指定格子添加僵尸
- `RemoveAllZombies` - 移除所有僵尸
- `RemoveCutsceneZombies` - 移除过场动画僵尸
- `KillAllZombiesInRadius` - 杀死指定半径内的所有僵尸

### 僵尸生成和波次管理

- `SpawnZombieWave` - 生成僵尸波次
- `SpawnZombiesFromGraves` - 从墓碑生成僵尸
- `UpdateZombieSpawning` - 更新僵尸生成
- `PickZombieType` - 选择僵尸类型
- `PickRowForNewZombie` - 为新僵尸选择行
- `PickGraveRisingZombieType` - 选择从墓碑升起的僵尸类型

### 僵尸查询和检测函数

- `ZombieGetID` - 获取僵尸ID
- `ZombieGet` - 根据ID获取僵尸
- `ZombieTryToGet` - 尝试根据ID获取僵尸
- `ZombieHitTest` - 僵尸点击测试
- `GetWinningZombie` - 获取获胜的僵尸
- `GetLiveGargantuarCount` - 获取存活的巨人僵尸数量
- `IterateZombies` - 遍历僵尸

### 僵尸类型和位置检查

- `ZombieTypeCanGoInPool` - 检查僵尸类型是否可以进入泳池
- `ZombieTypeCanGoOnHighGround` - 检查僵尸类型是否可以上高地
- `RowCanHaveZombieType` - 检查行是否可以有指定僵尸类型
- `RowCanHaveZombies` - 检查行是否可以有僵尸
- `CanZombieSpawnOnLevel` - 检查僵尸是否可以在关卡中生成
- `StageHasZombieWalkInFromRight` - 检查关卡是否有僵尸从右侧走入

### 僵尸波次和统计函数

- `NumberZombiesInWave` - 获取波次中的僵尸数量
- `TotalZombiesHealthInWave` - 获取波次中僵尸的总血量
- `ZombiesWon` - 僵尸获胜处理

### 僵尸选择器相关

- `ZombiePickerInit` - 初始化僵尸选择器
- `ZombiePickerInitForWave` - 为波次初始化僵尸选择器

## 数据字段

### 植物相关字段

- m_plants - 植物数据数组
- mPlantRow - 植物行类型数组

### 僵尸相关字段

- m_zombies - 僵尸数据数组
- m_vsGravestones - 对战模式墓碑僵尸列表
这些函数涵盖了植物和僵尸的创建、管理、查询、位置检测、类型验证等各个方面，是游戏核心逻辑的重要组成部分。

-------------------------------------------------------------------------------

## Get类函数（获取对象/值）
### 基础获取函数
- `GetTopPlantAt` - 获取指定位置的顶层植物
- `GetPlantAt` - 获取指定位置的植物
- `GetZenToolAt` - 获取指定位置的禅境花园工具
- `GetGridItemAt` - 获取指定位置的网格物品
- `GetRake` - 获取耙子
### 僵尸相关获取函数
- `ZombieGet` - 根据ID获取僵尸
- `ZombieTryToGet` - 尝试根据ID获取僵尸
- `ZombieGetID` - 获取僵尸ID
- `GetWinningZombie` - 获取获胜的僵尸
- `GetLiveGargantuarCount` - 获取存活的巨人僵尸数量
- `GetVSGravestoneAt` - 获取对战模式指定位置的墓碑
### 坐标转换函数
- `GridToPixelX` - 网格X坐标转像素坐标
- `GridToPixelY` - 网格Y坐标转像素坐标
- `PixelToGridX` - 像素X坐标转网格坐标
- `PixelToGridY` - 像素Y坐标转网格坐标
- `GetIceZPos` - 获取冰层Z位置
### 游戏状态获取函数
- `GetCurrentPlantCost` - 获取当前植物成本
- `GetNumWavesPerFlag` - 获取每面旗帜的波次数
- `GetSeedTypeInCursor` - 获取光标中的种子类型
- `GetSurvivalFlagsCompleted` - 获取生存模式完成的旗帜数
- `GetButterReanimation` - 获取黄油动画
## Has类函数（检查是否存在）
- `HasLevelAwardDropped` - 检查关卡奖励是否已掉落
- `HasProgressMeter` - 检查是否有进度条
- `HasValidCobCannonSpot` - 检查是否有有效的玉米加农炮位置
- `HasConveyorBeltSeedBank` - 检查是否有传送带种子库
## Count类函数（计数统计）
- `CountSunBeingCollected` - 统计正在收集的阳光数量
- `CountSunFlowers` - 统计向日葵数量
- `CountUntriggerLawnMowers` - 统计未触发的割草机数量
- `CountPlantByType` - 按类型统计植物数量
- `CountCoinsBeingCollected` - 统计正在收集的金币数量
- `CountZombiesOnScreen` - 统计屏幕上的僵尸数量
- `CountEmptyPotsOrLilies` - 统计空花盆或睡莲数量
- `CountCoinByType` - 按类型统计金币数量
## Is类函数（状态检查）
- `IsIceAt` - 检查指定位置是否有冰
- `IsPoolSquare` - 检查是否为泳池格子
- `IsZombieWaveDistributionOk` - 检查僵尸波次分布是否正常
- `IsZombieTypePoolOnly` - 检查僵尸类型是否仅限泳池
- `IsValidCobCannonSpot` - 检查是否为有效的玉米加农炮位置
- `IsValidCobCannonSpotHelper` - 玉米加农炮位置检查辅助函数
- `IsFlagWave` - 检查是否为旗帜波次
- `IsFinalSurvivalStage` - 检查是否为最终生存阶段
- `IsPlantInCursor` - 检查光标中是否有植物
- `IsFinalScaryPotterStage` - 检查是否为最终恐怖花瓶阶段
- `IsPlantInGoldWateringCanRange` - 检查植物是否在金水壶范围内
- `IsScaryPotterDaveTalking` - 检查恐怖花瓶戴夫是否在说话
- `IsLastStandFinalStage` - 检查是否为坚不可摧最终阶段
- `IsSurvivalStageWithRepick` - 检查是否为可重选的生存阶段
- `IsLastStandStageWithRepick` - 检查是否为可重选的坚不可摧阶段
- `IsGamepadEnabled` - 检查手柄是否启用
## Can类函数（能力检查）
- `CanPlantAt` - 检查是否可以在指定位置种植
- `CanTakeSunMoney` - 检查是否可以获取阳光金钱
- `CanInteractWithBoardButtons` - 检查是否可以与棋盘按钮交互
- `CanAddBobSled` - 检查是否可以添加雪橇
- `CanZombieSpawnOnLevel` - 检查僵尸是否可以在关卡中生成
- `CanDropLoot` - 检查是否可以掉落战利品
- `CanAddGraveStoneAt` - 检查是否可以在指定位置添加墓碑
- `CanUseGameObject` - 检查是否可以使用游戏对象
## Find类函数（查找对象）
- `FindLawnMowerInRow` - 在指定行查找割草机
- `FindUmbrellaPlant` - 查找雨伞叶植物
## Check类函数（检查验证）
- `CheckForPostGameAchievements` - 检查游戏后成就
