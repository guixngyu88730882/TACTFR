// GTA5 Native C# 包装类
// 数据来源: https://github.com/alloc8or/gta5-nativedb-data
// 
// 这个文件提供了常用 Native 函数的类型安全包装
// 使用方法:
//   GTA5Native.Wait(1000);
//   Ped player = GTA5Native.GetPlayerPed(Game.Player);

using GTA;
using GTA.Native;
using GTA.Math;
using System;

namespace GTA5Modding
{
    /// <summary>
    /// GTA5 Native 函数包装类
    /// </summary>
    public static class GTA5Native
    {
        #region BUILTIN (26 natives)

        /// <summary>
        /// 等待指定毫秒数
        /// </summary>
        /// <param name="ms">等待时间（毫秒）</param>
        public static void Wait(int ms) => Function.Call(Hash.WAIT, ms);

        /// <summary>
        /// 随机整数 [min, max]（包含 max）
        /// </summary>
        public static int RandomInt(int minInclusive, int maxInclusive)
        {
            if (maxInclusive < minInclusive)
            {
                int t = minInclusive;
                minInclusive = maxInclusive;
                maxInclusive = t;
            }
            // 使用 TickCount ^ GameTime 做种子（减少同帧/同毫秒重复）
            var r = new Random(unchecked(Environment.TickCount ^ Game.GameTime));
            return r.Next(minInclusive, maxInclusive + 1);
        }

        /// <summary>
        /// 启动新脚本
        /// </summary>
        public static int StartNewScript(string scriptName, int stackSize) => 
            Function.Call<int>(Hash.START_NEW_SCRIPT, scriptName, stackSize);

        /// <summary>
        /// 获取计时器A的值
        /// </summary>
        public static int GetTimerA() => Function.Call<int>(Hash.TIMERA);

        /// <summary>
        /// 获取计时器B的值
        /// </summary>
        public static int GetTimerB() => Function.Call<int>(Hash.TIMERB);

        /// <summary>
        /// 设置计时器A
        /// </summary>
        public static void SetTimerA(int value) => Function.Call(Hash.SETTIMERA, value);

        /// <summary>
        /// 设置计时器B
        /// </summary>
        public static void SetTimerB(int value) => Function.Call(Hash.SETTIMERB, value);

        /// <summary>
        /// 获取时间步长
        /// </summary>
        public static float GetTimeStep() => Function.Call<float>(Hash.TIMESTEP);

        /// <summary>
        /// 计算正弦值
        /// </summary>
        public static float Sin(float value) => Function.Call<float>(Hash.SIN, value);

        /// <summary>
        /// 计算余弦值
        /// </summary>
        public static float Cos(float value) => Function.Call<float>(Hash.COS, value);

        /// <summary>
        /// 计算平方根
        /// </summary>
        public static float Sqrt(float value) => Function.Call<float>(Hash.SQRT, value);

        /// <summary>
        /// 计算幂
        /// </summary>
        public static float Pow(float baseValue, float exponent) => 
            Function.Call<float>(Hash.POW, baseValue, exponent);

        /// <summary>
        /// 计算向量长度
        /// </summary>
        public static float VectorMagnitude(float x, float y, float z) => 
            Function.Call<float>(Hash.VMAG, x, y, z);

        /// <summary>
        /// 计算两点间距离
        /// </summary>
        public static float Distance(float x1, float y1, float z1, float x2, float y2, float z2) => 
            Function.Call<float>(Hash.VDIST, x1, y1, z1, x2, y2, z2);

        /// <summary>
        /// 向下取整
        /// </summary>
        public static int Floor(float value) => Function.Call<int>(Hash.FLOOR, value);

        /// <summary>
        /// 向上取整
        /// </summary>
        public static int Ceil(float value) => Function.Call<int>(Hash.CEIL, value);

        /// <summary>
        /// 四舍五入
        /// </summary>
        public static int Round(float value) => Function.Call<int>(Hash.ROUND, value);

        #endregion

        #region PLAYER (250 natives)

        /// <summary>
        /// 获取玩家ID
        /// </summary>
        public static int GetPlayerId() => Function.Call<int>(Hash.GET_PLAYER_ID);

        /// <summary>
        /// 获取玩家角色
        /// </summary>
        public static Ped GetPlayerPed(Player player) => 
            Function.Call<Ped>(Hash.GET_PLAYER_PED, player);

        /// <summary>
        /// 获取玩家角色（通过索引）
        /// </summary>
        public static Ped GetPlayerPed(int playerIndex) => 
            Function.Call<Ped>(Hash.GET_PLAYER_PED, playerIndex);

        /// <summary>
        /// 设置玩家无敌状态
        /// </summary>
        public static void SetPlayerInvincible(Player player, bool toggle) => 
            Function.Call(Hash.SET_PLAYER_INVINCIBLE, player, toggle);

        /// <summary>
        /// 设置玩家生命值
        /// </summary>
        public static void SetPlayerHealth(Player player, int health) => 
            Function.Call(Hash.SET_PLAYER_HEALTH, player, health);

        /// <summary>
        /// 获取玩家生命值
        /// </summary>
        public static int GetPlayerHealth(Player player) => 
            Function.Call<int>(Hash.GET_PLAYER_HEALTH, player);

        /// <summary>
        /// 设置玩家护甲
        /// </summary>
        public static void SetPlayerMaxArmour(Player player, int armour) => 
            Function.Call(Hash.SET_PLAYER_MAX_ARMOUR, player, armour);

        /// <summary>
        /// 设置玩家 wanted 等级
        /// </summary>
        public static void SetPlayerWantedLevel(Player player, int wantedLevel, bool disableNoise) => 
            Function.Call(Hash.SET_PLAYER_WANTED_LEVEL, player, wantedLevel, disableNoise);

        /// <summary>
        /// 清除玩家 wanted 等级
        /// </summary>
        public static void ClearPlayerWantedLevel(Player player) => 
            Function.Call(Hash.CLEAR_PLAYER_WANTED_LEVEL, player);

        /// <summary>
        /// 设置玩家模型
        /// </summary>
        public static void SetPlayerModel(Player player, uint model) => 
            Function.Call(Hash.SET_PLAYER_MODEL, player, model);

        /// <summary>
        /// 给玩家武器
        /// </summary>
        public static void GiveWeaponToPed(Ped ped, uint weaponHash, int ammoCount, bool isHidden, bool equipNow) => 
            Function.Call(Hash.GIVE_WEAPON_TO_PED, ped, weaponHash, ammoCount, isHidden, equipNow);

        #endregion

        #region VEHICLE (779 natives)

        /// <summary>
        /// 创建载具
        /// </summary>
        public static Vehicle CreateVehicle(uint modelHash, float x, float y, float z, float heading, bool isNetwork, bool thisScriptCheck) => 
            Function.Call<Vehicle>(Hash.CREATE_VEHICLE, modelHash, x, y, z, heading, isNetwork, thisScriptCheck);

        /// <summary>
        /// 创建载具（使用Vector3）
        /// </summary>
        public static Vehicle CreateVehicle(uint modelHash, Vector3 position, float heading, bool isNetwork, bool thisScriptCheck) => 
            Function.Call<Vehicle>(Hash.CREATE_VEHICLE, modelHash, position.X, position.Y, position.Z, heading, isNetwork, thisScriptCheck);

        /// <summary>
        /// 删除载具
        /// </summary>
        public static void DeleteVehicle(ref Vehicle vehicle) => 
            Function.Call(Hash.DELETE_VEHICLE, vehicle);

        /// <summary>
        /// 设置载具颜色
        /// </summary>
        public static void SetVehicleColours(Vehicle vehicle, int colorPrimary, int colorSecondary) => 
            Function.Call(Hash.SET_VEHICLE_COLOURS, vehicle, colorPrimary, colorSecondary);

        /// <summary>
        /// 设置载具自定义颜色
        /// </summary>
        public static void SetVehicleCustomPrimaryColour(Vehicle vehicle, int r, int g, int b) => 
            Function.Call(Hash.SET_VEHICLE_CUSTOM_PRIMARY_COLOUR, vehicle, r, g, b);

        /// <summary>
        /// 设置载具引擎状态
        /// </summary>
        public static void SetVehicleEngineOn(Vehicle vehicle, bool value, bool instantly, bool disableAutoStart) => 
            Function.Call(Hash.SET_VEHICLE_ENGINE_ON, vehicle, value, instantly, disableAutoStart);

        /// <summary>
        /// 设置载具不可损坏
        /// </summary>
        public static void SetVehicleCanBeVisiblyDamaged(Vehicle vehicle, bool state) => 
            Function.Call(Hash.SET_VEHICLE_CAN_BE_VISIBLY_DAMAGED, vehicle, state);

        /// <summary>
        /// 设置载具无敌
        /// </summary>
        public static void SetEntityInvincible(Vehicle vehicle, bool toggle) => 
            Function.Call(Hash.SET_ENTITY_INVINCIBLE, vehicle, toggle);

        /// <summary>
        /// 设置载具轮胎不可损坏
        /// </summary>
        public static void SetVehicleTyresCanBurst(Vehicle vehicle, bool toggle) => 
            Function.Call(Hash.SET_VEHICLE_TYRES_CAN_BURST, vehicle, toggle);

        /// <summary>
        /// 修复载具
        /// </summary>
        public static void SetVehicleFixed(Vehicle vehicle) => 
            Function.Call(Hash.SET_VEHICLE_FIXED, vehicle);

        /// <summary>
        /// 设置载具车门锁定状态
        /// </summary>
        public static void SetVehicleDoorsLocked(Vehicle vehicle, int doorLockStatus) => 
            Function.Call(Hash.SET_VEHICLE_DOORS_LOCKED, vehicle, doorLockStatus);

        /// <summary>
        /// 设置载具Number Plate
        /// </summary>
        public static void SetVehicleNumberPlateText(Vehicle vehicle, string plateText) => 
            Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT, vehicle, plateText);

        #endregion

        #region PED (615 natives)

        /// <summary>
        /// 创建角色
        /// </summary>
        public static Ped CreatePed(int pedType, uint modelHash, float x, float y, float z, float heading, bool isNetwork, bool thisScriptCheck) => 
            Function.Call<Ped>(Hash.CREATE_PED, pedType, modelHash, x, y, z, heading, isNetwork, thisScriptCheck);

        /// <summary>
        /// 删除角色
        /// </summary>
        public static void DeletePed(ref Ped ped) => 
            Function.Call(Hash.DELETE_PED, ped);

        /// <summary>
        /// 设置角色健康值
        /// </summary>
        public static void SetEntityHealth(Entity entity, int health) => 
            Function.Call(Hash.SET_ENTITY_HEALTH, entity, health);

        /// <summary>
        /// 获取角色健康值
        /// </summary>
        public static int GetEntityHealth(Entity entity) => 
            Function.Call<int>(Hash.GET_ENTITY_HEALTH, entity);

        /// <summary>
        /// 设置角色最大健康值
        /// </summary>
        public static void SetEntityMaxHealth(Entity entity, int value) => 
            Function.Call(Hash.SET_ENTITY_MAX_HEALTH, entity, value);

        /// <summary>
        /// 设置角色无敌
        /// </summary>
        public static void SetEntityInvincible(Entity entity, bool toggle) => 
            Function.Call(Hash.SET_ENTITY_INVINCIBLE, entity, toggle);

        /// <summary>
        /// 设置角色可见性
        /// </summary>
        public static void SetEntityVisible(Entity entity, bool visible, bool snap) => 
            Function.Call(Hash.SET_ENTITY_VISIBLE, entity, visible, snap);

        /// <summary>
        /// 设置角色坐标
        /// </summary>
        public static void SetEntityCoords(Entity entity, float x, float y, float z, bool xAxis, bool yAxis, bool zAxis, bool clearArea) => 
            Function.Call(Hash.SET_ENTITY_COORDS, entity, x, y, z, xAxis, yAxis, zAxis, clearArea);

        /// <summary>
        /// 设置角色坐标（简化版）
        /// </summary>
        public static void SetEntityCoords(Entity entity, Vector3 position) => 
            Function.Call(Hash.SET_ENTITY_COORDS, entity, position.X, position.Y, position.Z, false, false, false, false);

        /// <summary>
        /// 获取角色坐标
        /// </summary>
        public static Vector3 GetEntityCoords(Entity entity, bool alive) => 
            Function.Call<Vector3>(Hash.GET_ENTITY_COORDS, entity, alive);

        /// <summary>
        /// 设置角色朝向
        /// </summary>
        public static void SetEntityHeading(Entity entity, float heading) => 
            Function.Call(Hash.SET_ENTITY_HEADING, entity, heading);

        /// <summary>
        /// 获取角色朝向
        /// </summary>
        public static float GetEntityHeading(Entity entity) => 
            Function.Call<float>(Hash.GET_ENTITY_HEADING, entity);

        /// <summary>
        /// 设置角色冻结位置
        /// </summary>
        public static void FreezeEntityPosition(Entity entity, bool toggle) => 
            Function.Call(Hash.FREEZE_ENTITY_POSITION, entity, toggle);

        /// <summary>
        /// 检查角色是否存在
        /// </summary>
        public static bool DoesEntityExist(Entity entity) => 
            Function.Call<bool>(Hash.DOES_ENTITY_EXIST, entity);

        /// <summary>
        /// 检查角色是否死亡
        /// </summary>
        public static bool IsEntityDead(Entity entity) => 
            Function.Call<bool>(Hash.IS_ENTITY_DEAD, entity);

        /// <summary>
        /// 检查角色是否活着
        /// </summary>
        public static bool IsEntityAlive(Entity entity) => 
            Function.Call<bool>(Hash.IS_ENTITY_ALIVE, entity);

        /// <summary>
        /// 设置角色任务
        /// </summary>
        public static void TaskWarpPedIntoVehicle(Ped ped, Vehicle vehicle, int seat) => 
            Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, ped, vehicle, seat);

        /// <summary>
        /// 清除角色任务
        /// </summary>
        public static void ClearPedTasks(Ped ped) => 
            Function.Call(Hash.CLEAR_PED_TASKS, ped);

        /// <summary>
        /// 清除角色所有任务
        /// </summary>
        public static void ClearPedTasksImmediately(Ped ped) => 
            Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, ped);

        #endregion

        #region ENTITY (189 natives)

        /// <summary>
        /// 删除实体
        /// </summary>
        public static void DeleteEntity(ref Entity entity) => 
            Function.Call(Hash.DELETE_ENTITY, entity);

        /// <summary>
        /// 设置实体碰撞
        /// </summary>
        public static void SetEntityCollision(Entity entity, bool toggle, bool keepPhysics) => 
            Function.Call(Hash.SET_ENTITY_COLLISION, entity, toggle, keepPhysics);

        /// <summary>
        /// 设置实体alpha
        /// </summary>
        public static void SetEntityAlpha(Entity entity, int alphaLevel, bool skin) => 
            Function.Call(Hash.SET_ENTITY_ALPHA, entity, alphaLevel, skin);

        /// <summary>
        /// 重置实体alpha
        /// </summary>
        public static void ResetEntityAlpha(Entity entity) => 
            Function.Call(Hash.RESET_ENTITY_ALPHA, entity);

        /// <summary>
        /// 获取实体模型
        /// </summary>
        public static uint GetEntityModel(Entity entity) => 
            Function.Call<uint>(Hash.GET_ENTITY_MODEL, entity);

        /// <summary>
        /// 获取实体速度
        /// </summary>
        public static Vector3 GetEntityVelocity(Entity entity) => 
            Function.Call<Vector3>(Hash.GET_ENTITY_VELOCITY, entity);

        /// <summary>
        /// 设置实体速度
        /// </summary>
        public static void SetEntityVelocity(Entity entity, float x, float y, float z) => 
            Function.Call(Hash.SET_ENTITY_VELOCITY, entity, x, y, z);

        /// <summary>
        /// 设置实体旋转
        /// </summary>
        public static void SetEntityRotation(Entity entity, float pitch, float roll, float yaw, int rotationOrder, bool resetPhysics) => 
            Function.Call(Hash.SET_ENTITY_ROTATION, entity, pitch, roll, yaw, rotationOrder, resetPhysics);

        /// <summary>
        /// 获取实体旋转
        /// </summary>
        public static Vector3 GetEntityRotation(Entity entity, int rotationOrder) => 
            Function.Call<Vector3>(Hash.GET_ENTITY_ROTATION, entity, rotationOrder);

        #endregion

        #region GRAPHICS (401 natives)

        /// <summary>
        /// 绘制通知
        /// </summary>
        public static void BeginTextCommandThefeedPost(string text) => 
            Function.Call(Hash.BEGIN_TEXT_COMMAND_THEFEED_POST, text);

        /// <summary>
        /// 结束通知
        /// </summary>
        public static int EndTextCommandThefeedPostTicker(bool isImportant, bool bHasTokens) => 
            Function.Call<int>(Hash.END_TEXT_COMMAND_THEFEED_POST_TICKER, isImportant, bHasTokens);

        /// <summary>
        /// 显示通知
        /// </summary>
        public static void Notification(string message)
        {
            Function.Call(Hash.BEGIN_TEXT_COMMAND_THEFEED_POST, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, message);
            Function.Call(Hash.END_TEXT_COMMAND_THEFEED_POST_TICKER, false, true);
        }

        /// <summary>
        /// 绘制文本
        /// </summary>
        public static void DrawTxt(float x, float y, float scale, string text, int r, int g, int b, int a)
        {
            Function.Call(Hash.SET_TEXT_FONT, 0);
            Function.Call(Hash.SET_TEXT_SCALE, scale, scale);
            Function.Call(Hash.SET_TEXT_COLOUR, r, g, b, a);
            Function.Call(Hash.SET_TEXT_OUTLINE);
            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
            Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, x, y);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        public static void DrawRect(float x, float y, float width, float height, int r, int g, int b, int a) => 
            Function.Call(Hash.DRAW_RECT, x, y, width, height, r, g, b, a);

        /// <summary>
        /// 绘制线
        /// </summary>
        public static void DrawLine(float x1, float y1, float z1, float x2, float y2, float z2, int r, int g, int b, int a) => 
            Function.Call(Hash.DRAW_LINE, x1, y1, z1, x2, y2, z2, r, g, b, a);

        /// <summary>
        /// 绘制标记
        /// </summary>
        public static void DrawMarker(int type, float posX, float posY, float posZ, float dirX, float dirY, float dirZ, float rotX, float rotY, float rotZ, float scaleX, float scaleY, float scaleZ, int red, int green, int blue, int alpha, bool bobUpAndDown, bool faceCamera, int p19, bool rotate, string textureDict, string textureName, bool drawOnEnts) => 
            Function.Call(Hash.DRAW_MARKER, type, posX, posY, posZ, dirX, dirY, dirZ, rotX, rotY, rotZ, scaleX, scaleY, scaleZ, red, green, blue, alpha, bobUpAndDown, faceCamera, p19, rotate, textureDict, textureName, drawOnEnts);

        /// <summary>
        /// 设置绘制层级
        /// </summary>
        public static void SetDrawOrigin(float x, float y, float z, int p3) => 
            Function.Call(Hash.SET_DRAW_ORIGIN, x, y, z, p3);

        /// <summary>
        /// 清除绘制层级
        /// </summary>
        public static void ClearDrawOrigin() => 
            Function.Call(Hash.CLEAR_DRAW_ORIGIN);

        #endregion

        #region HUD (519 natives)

        /// <summary>
        /// 显示帮助文本
        /// </summary>
        public static void DisplayHelpText(string text)
        {
            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_HELP, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
            Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_HELP, 0, false, true, -1);
        }

        /// <summary>
        /// 显示提示文本
        /// </summary>
        public static void DisplayHud(bool toggle) => 
            Function.Call(Hash.DISPLAY_HUD, toggle);

        /// <summary>
        /// 显示雷达
        /// </summary>
        public static void DisplayRadar(bool toggle) => 
            Function.Call(Hash.DISPLAY_RADAR, toggle);

        /// <summary>
        /// 设置雷达缩放
        /// </summary>
        public static void SetRadarZoom(float zoom) => 
            Function.Call(Hash.SET_RADAR_ZOOM, zoom);

        /// <summary>
        /// 添加地图标记
        /// </summary>
        public static Blip AddBlipForCoord(float x, float y, float z) => 
            Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, x, y, z);

        /// <summary>
        /// 为实体添加地图标记
        /// </summary>
        public static Blip AddBlipForEntity(Entity entity) => 
            Function.Call<Blip>(Hash.ADD_BLIP_FOR_ENTITY, entity);

        /// <summary>
        /// 删除地图标记
        /// </summary>
        public static void RemoveBlip(ref Blip blip) => 
            Function.Call(Hash.REMOVE_BLIP, blip);

        /// <summary>
        /// 设置地图标记颜色
        /// </summary>
        public static void SetBlipColour(Blip blip, int color) => 
            Function.Call(Hash.SET_BLIP_COLOUR, blip, color);

        /// <summary>
        /// 设置地图标记精灵
        /// </summary>
        public static void SetBlipSprite(Blip blip, int spriteId) => 
            Function.Call(Hash.SET_BLIP_SPRITE, blip, spriteId);

        /// <summary>
        /// 设置地图标记缩放
        /// </summary>
        public static void SetBlipScale(Blip blip, float scale) => 
            Function.Call(Hash.SET_BLIP_SCALE, blip, scale);

        /// <summary>
        /// 设置地图标记显示名称
        /// </summary>
        public static void BeginTextCommandSetBlipName(string text) => 
            Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME, text);

        /// <summary>
        /// 结束设置地图标记名称
        /// </summary>
        public static void EndTextCommandSetBlipName(Blip blip) => 
            Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, blip);

        #endregion

        #region WORLD / MISC (334 natives)

        /// <summary>
        /// 获取游戏计时器
        /// </summary>
        public static int GetGameTimer() => Function.Call<int>(Hash.GET_GAME_TIMER);

        /// <summary>
        /// 获取帧计数
        /// </summary>
        public static int GetFrameCount() => Function.Call<int>(Hash.GET_FRAME_COUNT);

        /// <summary>
        /// 获取帧时间
        /// </summary>
        public static float GetFrameTime() => Function.Call<float>(Hash.GET_FRAME_TIME);

        /// <summary>
        /// 获取随机浮点数
        /// </summary>
        public static float GetRandomFloatInRange(float startRange, float endRange) => 
            Function.Call<float>(Hash.GET_RANDOM_FLOAT_IN_RANGE, startRange, endRange);

        /// <summary>
        /// 获取随机整数
        /// </summary>
        public static int GetRandomIntInRange(int startRange, int endRange) => 
            Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, startRange, endRange);

        /// <summary>
        /// 设置天气
        /// </summary>
        public static void SetWeatherTypeNow(string weatherType) => 
            Function.Call(Hash.SET_WEATHER_TYPE_NOW, weatherType);

        /// <summary>
        /// 设置覆盖天气
        /// </summary>
        public static void SetOverrideWeather(string weatherType) => 
            Function.Call(Hash.SET_OVERRIDE_WEATHER, weatherType);

        /// <summary>
        /// 清除覆盖天气
        /// </summary>
        public static void ClearOverrideWeather() => 
            Function.Call(Hash.CLEAR_OVERRIDE_WEATHER);

        /// <summary>
        /// 设置时间
        /// </summary>
        public static void NetworkOverrideClockTime(int hours, int minutes, int seconds) => 
            Function.Call(Hash.NETWORK_OVERRIDE_CLOCK_TIME, hours, minutes, seconds);

        /// <summary>
        /// 添加爆炸
        /// </summary>
        public static void AddExplosion(float x, float y, float z, int explosionType, float damageScale, bool isAudible, bool isInvisible, float cameraShake) => 
            Function.Call(Hash.ADD_EXPLOSION, x, y, z, explosionType, damageScale, isAudible, isInvisible, cameraShake);

        /// <summary>
        /// 添加拥有者的爆炸
        /// </summary>
        public static void AddOwnedExplosion(Ped ped, float x, float y, float z, int explosionType, float damageScale, bool isAudible, bool isInvisible, float cameraShake) => 
            Function.Call(Hash.ADD_OWNED_EXPLOSION, ped, x, y, z, explosionType, damageScale, isAudible, isInvisible, cameraShake);

        #endregion

        #region STREAMING (123 natives)

        /// <summary>
        /// 请求模型
        /// </summary>
        public static void RequestModel(uint model) => 
            Function.Call(Hash.REQUEST_MODEL, model);

        /// <summary>
        /// 检查模型是否已加载
        /// </summary>
        public static bool HasModelLoaded(uint model) => 
            Function.Call<bool>(Hash.HAS_MODEL_LOADED, model);

        /// <summary>
        /// 设置模型为不需要
        /// </summary>
        public static void SetModelAsNoLongerNeeded(uint model) => 
            Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, model);

        /// <summary>
        /// 请求动画字典
        /// </summary>
        public static void RequestAnimDict(string animDict) => 
            Function.Call(Hash.REQUEST_ANIM_DICT, animDict);

        /// <summary>
        /// 检查动画字典是否已加载
        /// </summary>
        public static bool HasAnimDictLoaded(string animDict) => 
            Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, animDict);

        /// <summary>
        /// 移除动画字典
        /// </summary>
        public static void RemoveAnimDict(string animDict) => 
            Function.Call(Hash.REMOVE_ANIM_DICT, animDict);

        #endregion

        #region AUDIO (281 natives)

        /// <summary>
        /// 播放声音
        /// </summary>
        public static void PlaySoundFrontend(int soundId, string audioName, string audioRef, bool p3) => 
            Function.Call(Hash.PLAY_SOUND_FRONTEND, soundId, audioName, audioRef, p3);

        /// <summary>
        /// 停止声音
        /// </summary>
        public static void StopSound(int soundId) => 
            Function.Call(Hash.STOP_SOUND, soundId);

        /// <summary>
        /// 释放声音ID
        /// </summary>
        public static void ReleaseSoundId(int soundId) => 
            Function.Call(Hash.RELEASE_SOUND_ID, soundId);

        /// <summary>
        /// 获取声音ID
        /// </summary>
        public static int GetSoundId() => Function.Call<int>(Hash.GET_SOUND_ID);

        /// <summary>
        /// 播放音乐
        /// </summary>
        public static void TriggerMusicEvent(string eventName) => 
            Function.Call(Hash.TRIGGER_MUSIC_EVENT, eventName);

        /// <summary>
        /// 准备音乐事件
        /// </summary>
        public static bool PrepareMusicEvent(string eventName) => 
            Function.Call<bool>(Hash.PREPARE_MUSIC_EVENT, eventName);

        /// <summary>
        /// 取消音乐事件
        /// </summary>
        public static void CancelMusicEvent(string eventName) => 
            Function.Call(Hash.CANCEL_MUSIC_EVENT, eventName);

        #endregion

        #region CAM (248 natives)

        /// <summary>
        /// 创建摄像机
        /// </summary>
        public static Cam CreateCam(string camName, bool p1) => 
            Function.Call<Cam>(Hash.CREATE_CAM, camName, p1);

        /// <summary>
        /// 创建摄像机（使用Hash）
        /// </summary>
        public static Cam CreateCamWithParams(string camName, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float fov, bool p8, int p9) => 
            Function.Call<Cam>(Hash.CREATE_CAM_WITH_PARAMS, camName, posX, posY, posZ, rotX, rotY, rotZ, fov, p8, p9);

        /// <summary>
        /// 销毁摄像机
        /// </summary>
        public static void DestroyCam(Cam cam, bool thisScriptCheck) => 
            Function.Call(Hash.DESTROY_CAM, cam, thisScriptCheck);

        /// <summary>
        /// 渲染摄像机
        /// </summary>
        public static void RenderScriptCams(bool render, bool ease, int easeTime, bool p3, bool p4, int p5) => 
            Function.Call(Hash.RENDER_SCRIPT_CAMS, render, ease, easeTime, p3, p4, p5);

        /// <summary>
        /// 设置摄像机激活
        /// </summary>
        public static void SetCamActive(Cam cam, bool active) => 
            Function.Call(Hash.SET_CAM_ACTIVE, cam, active);

        /// <summary>
        /// 设置摄像机坐标
        /// </summary>
        public static void SetCamCoord(Cam cam, float posX, float posY, float posZ) => 
            Function.Call(Hash.SET_CAM_COORD, cam, posX, posY, posZ);

        /// <summary>
        /// 设置摄像机旋转
        /// </summary>
        public static void SetCamRot(Cam cam, float rotX, float rotY, float rotZ, int rotationOrder) => 
            Function.Call(Hash.SET_CAM_ROT, cam, rotX, rotY, rotZ, rotationOrder);

        /// <summary>
        /// 设置摄像机视野
        /// </summary>
        public static void SetCamFov(Cam cam, float fieldOfView) => 
            Function.Call(Hash.SET_CAM_FOV, cam, fieldOfView);

        /// <summary>
        /// 将摄像机指向实体
        /// </summary>
        public static void PointCamAtEntity(Cam cam, Entity entity, float offsetX, float offsetY, float offsetZ, bool p5) => 
            Function.Call(Hash.POINT_CAM_AT_ENTITY, cam, entity, offsetX, offsetY, offsetZ, p5);

        /// <summary>
        /// 将摄像机指向坐标
        /// </summary>
        public static void PointCamAtCoord(Cam cam, float x, float y, float z) => 
            Function.Call(Hash.POINT_CAM_AT_COORD, cam, x, y, z);

        /// <summary>
        /// 停止摄像机指向
        /// </summary>
        public static void StopCamPointing(Cam cam) => 
            Function.Call(Hash.STOP_CAM_POINTING, cam);

        /// <summary>
        /// 震动摄像机
        /// </summary>
        public static void ShakeCam(Cam cam, string type, float amplitude) => 
            Function.Call(Hash.SHAKE_CAM, cam, type, amplitude);

        /// <summary>
        /// 停止摄像机震动
        /// </summary>
        public static void StopCamShaking(Cam cam, bool p1) => 
            Function.Call(Hash.STOP_CAM_SHAKING, cam, p1);

        /// <summary>
        /// 将游戏视角摄像机指向实体
        /// </summary>
        public static void SetGameplayCamEntityToLimitFocusOn(Entity entity) => 
            Function.Call(Hash.SET_GAMEPLAY_CAM_ENTITY_TO_LIMIT_FOCUS_ON, entity);

        #endregion

        #region WEAPON (123 natives)

        /// <summary>
        /// 给角色武器
        /// </summary>
        public static void GiveWeaponToPed(Ped ped, uint weaponHash, int ammoCount, bool isHidden, bool equipNow) => 
            Function.Call(Hash.GIVE_WEAPON_TO_PED, ped, weaponHash, ammoCount, isHidden, equipNow);

        /// <summary>
        /// 移除角色武器
        /// </summary>
        public static void RemoveWeaponFromPed(Ped ped, uint weaponHash) => 
            Function.Call(Hash.REMOVE_WEAPON_FROM_PED, ped, weaponHash);

        /// <summary>
        /// 移除角色所有武器
        /// </summary>
        public static void RemoveAllPedWeapons(Ped ped, bool p1) => 
            Function.Call(Hash.REMOVE_ALL_PED_WEAPONS, ped, p1);

        /// <summary>
        /// 检查角色是否有武器
        /// </summary>
        public static bool HasPedGotWeapon(Ped ped, uint weaponHash, bool p2) => 
            Function.Call<bool>(Hash.HAS_PED_GOT_WEAPON, ped, weaponHash, p2);

        /// <summary>
        /// 设置当前武器
        /// </summary>
        public static void SetCurrentPedWeapon(Ped ped, uint weaponHash, bool equipNow) => 
            Function.Call(Hash.SET_CURRENT_PED_WEAPON, ped, weaponHash, equipNow);

        /// <summary>
        /// 获取当前武器
        /// </summary>
        public static uint GetSelectedPedWeapon(Ped ped) => 
            Function.Call<uint>(Hash.GET_SELECTED_PED_WEAPON, ped);

        /// <summary>
        /// 设置弹药
        /// </summary>
        public static void SetPedAmmo(Ped ped, uint weaponHash, int ammo) => 
            Function.Call(Hash.SET_PED_AMMO, ped, weaponHash, ammo);

        /// <summary>
        /// 添加弹药
        /// </summary>
        public static void AddAmmoToPed(Ped ped, uint weaponHash, int ammo) => 
            Function.Call(Hash.ADD_AMMO_TO_PED, ped, weaponHash, ammo);

        /// <summary>
        /// 获取弹药数量
        /// </summary>
        public static int GetAmmoInPedWeapon(Ped ped, uint weaponHash) => 
            Function.Call<int>(Hash.GET_AMMO_IN_PED_WEAPON, ped, weaponHash);

        /// <summary>
        /// 获取弹夹弹药数量
        /// </summary>
        public static int GetAmmoInClip(Ped ped, uint weaponHash, out int ammo) => 
            Function.Call<int>(Hash.GET_AMMO_IN_CLIP, ped, weaponHash, ammo);

        /// <summary>
        /// 设置弹夹弹药数量
        /// </summary>
        public static void SetAmmoInClip(Ped ped, uint weaponHash, int ammo) => 
            Function.Call(Hash.SET_AMMO_IN_CLIP, ped, weaponHash, ammo);

        /// <summary>
        /// 获取最大弹药数量
        /// </summary>
        public static int GetMaxAmmo(Ped ped, uint weaponHash, out int ammo) => 
            Function.Call<int>(Hash.GET_MAX_AMMO, ped, weaponHash, ammo);

        /// <summary>
        /// 获取武器模型
        /// </summary>
        public static uint GetWeapontypeModel(uint weaponHash) => 
            Function.Call<uint>(Hash.GET_WEAPONTYPE_MODEL, weaponHash);

        /// <summary>
        /// 获取武器槽位
        /// </summary>
        public static int GetWeapontypeSlot(uint weaponHash) => 
            Function.Call<int>(Hash.GET_WEAPONTYPE_SLOT, weaponHash);

        /// <summary>
        /// 获取武器组
        /// </summary>
        public static uint GetWeapontypeGroup(uint weaponHash) => 
            Function.Call<uint>(Hash.GET_WEAPONTYPE_GROUP, weaponHash);

        #endregion

        #region OBJECT (166 natives)

        /// <summary>
        /// 创建物体
        /// </summary>
        public static Object CreateObject(uint modelHash, float x, float y, float z, bool isNetwork, bool thisScriptCheck, bool dynamic) => 
            Function.Call<Object>(Hash.CREATE_OBJECT, modelHash, x, y, z, isNetwork, thisScriptCheck, dynamic);

        /// <summary>
        /// 创建物体（无偏移）
        /// </summary>
        public static Object CreateObjectNoOffset(uint modelHash, float x, float y, float z, bool isNetwork, bool thisScriptCheck, bool dynamic) => 
            Function.Call<Object>(Hash.CREATE_OBJECT_NO_OFFSET, modelHash, x, y, z, isNetwork, thisScriptCheck, dynamic);

        /// <summary>
        /// 删除物体
        /// </summary>
        public static void DeleteObject(ref Object obj) => 
            Function.Call(Hash.DELETE_OBJECT, obj);

        /// <summary>
        /// 检查物体是否存在
        /// </summary>
        public static bool DoesObjectOfTypeExistAtCoords(float x, float y, float z, float radius, uint hash, bool p5) => 
            Function.Call<bool>(Hash.DOES_OBJECT_OF_TYPE_EXIST_AT_COORDS, x, y, z, radius, hash, p5);

        /// <summary>
        /// 设置物体可见性
        /// </summary>
        public static void SetObjectVisible(Object obj, bool toggle) => 
            Function.Call(Hash.SET_OBJECT_VISIBLE, obj, toggle);

        /// <summary>
        /// 设置物体碰撞
        /// </summary>
        public static void SetObjectCollision(Object obj, bool toggle, bool keepPhysics) => 
            Function.Call(Hash.SET_OBJECT_COLLISION, obj, toggle, keepPhysics);

        /// <summary>
        /// 设置物体物理参数
        /// </summary>
        public static void SetObjectPhysicsParams(Object obj, float weight, float p2, float p3, float p4, float p5, float gravity, float p7, float p8, float p9, float buoyancy) => 
            Function.Call(Hash.SET_OBJECT_PHYSICS_PARAMS, obj, weight, p2, p3, p4, p5, gravity, p7, p8, p9, buoyancy);

        /// <summary>
        /// 获取最接近的物体
        /// </summary>
        public static Object GetClosestObjectOfType(float x, float y, float z, float radius, uint modelHash, bool isMission, bool p6, bool p7) => 
            Function.Call<Object>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, x, y, z, radius, modelHash, isMission, p6, p7);

        /// <summary>
        /// 设置物体是否可以被损坏
        /// </summary>
        public static void SetObjectCanBeDamaged(Object obj, bool toggle) => 
            Function.Call(Hash.SET_OBJECT_CAN_BE_DAMAGED, obj, toggle);

        #endregion

        #region NETWORK (878 natives)

        /// <summary>
        /// 检查是否在线模式
        /// </summary>
        public static bool NetworkIsGameInProgress() => Function.Call<bool>(Hash.NETWORK_IS_GAME_IN_PROGRESS);

        /// <summary>
        /// 检查是否连接到网络
        /// </summary>
        public static bool NetworkIsSignedOnline() => Function.Call<bool>(Hash.NETWORK_IS_SIGNED_ONLINE);

        /// <summary>
        /// 检查是否是主机
        /// </summary>
        public static bool NetworkIsHost() => Function.Call<bool>(Hash.NETWORK_IS_HOST);

        /// <summary>
        /// 获取网络时间
        /// </summary>
        public static int GetNetworkTime() => Function.Call<int>(Hash.GET_NETWORK_TIME);

        /// <summary>
        /// 获取网络时间精确值
        /// </summary>
        public static int GetNetworkTimeAccurate() => Function.Call<int>(Hash.GET_NETWORK_TIME_ACCURATE);

        /// <summary>
        /// 检查是否处于会话中
        /// </summary>
        public static bool NetworkIsInSession() => Function.Call<bool>(Hash.NETWORK_IS_IN_SESSION);

        /// <summary>
        /// 检查是否处于过渡状态
        /// </summary>
        public static bool NetworkIsInTransition() => Function.Call<bool>(Hash.NETWORK_IS_IN_TRANSITION);

        /// <summary>
        /// 检查是否可以进入多人游戏
        /// </summary>
        public static bool NetworkCanEnterMultiplayer() => Function.Call<bool>(Hash.NETWORK_CAN_ENTER_MULTIPLAYER);

        /// <summary>
        /// 检查玩家是否已连接
        /// </summary>
        public static bool NetworkIsPlayerConnected(Player player) => 
            Function.Call<bool>(Hash.NETWORK_IS_PLAYER_CONNECTED, player);

        /// <summary>
        /// 检查玩家是否活跃
        /// </summary>
        public static bool NetworkIsPlayerActive(Player player) => 
            Function.Call<bool>(Hash.NETWORK_IS_PLAYER_ACTIVE, player);

        /// <summary>
        /// 获取玩家名称
        /// </summary>
        public static string GetPlayerName(Player player) => 
            Function.Call<string>(Hash.GET_PLAYER_NAME, player);

        /// <summary>
        /// 触发服务器事件
        /// </summary>
        public static void TriggerServerEvent(string eventName, params object[] args) => 
            Function.Call(Hash.TRIGGER_SERVER_EVENT, eventName, args);

        /// <summary>
        /// 触发客户端事件
        /// </summary>
        public static void TriggerClientEvent(string eventName, params object[] args) => 
            Function.Call(Hash.TRIGGER_CLIENT_EVENT, eventName, args);

        #endregion

        #region GAME / PAD (50 natives)

        /// <summary>
        /// 检查控制是否刚刚按下
        /// </summary>
        public static bool IsControlJustPressed(int inputGroup, int control) => 
            Function.Call<bool>(Hash.IS_CONTROL_JUST_PRESSED, inputGroup, control);

        /// <summary>
        /// 检查控制是否按下
        /// </summary>
        public static bool IsControlPressed(int inputGroup, int control) => 
            Function.Call<bool>(Hash.IS_CONTROL_PRESSED, inputGroup, control);

        /// <summary>
        /// 检查控制是否释放
        /// </summary>
        public static bool IsControlReleased(int inputGroup, int control) => 
            Function.Call<bool>(Hash.IS_CONTROL_RELEASED, inputGroup, control);

        /// <summary>
        /// 检查控制是否刚刚释放
        /// </summary>
        public static bool IsControlJustReleased(int inputGroup, int control) => 
            Function.Call<bool>(Hash.IS_CONTROL_JUST_RELEASED, inputGroup, control);

        /// <summary>
        /// 禁用控制
        /// </summary>
        public static void DisableControlAction(int inputGroup, int control, bool disable) => 
            Function.Call(Hash.DISABLE_CONTROL_ACTION, inputGroup, control, disable);

        /// <summary>
        /// 启用控制
        /// </summary>
        public static void EnableControlAction(int inputGroup, int control, bool enable) => 
            Function.Call(Hash.ENABLE_CONTROL_ACTION, inputGroup, control, enable);

        /// <summary>
        /// 禁用所有控制
        /// </summary>
        public static void DisableAllControlActions(int inputGroup) => 
            Function.Call(Hash.DISABLE_ALL_CONTROL_ACTIONS, inputGroup);

        /// <summary>
        /// 启用所有控制
        /// </summary>
        public static void EnableAllControlActions(int inputGroup) => 
            Function.Call(Hash.ENABLE_ALL_CONTROL_ACTIONS, inputGroup);

        /// <summary>
        /// 获取控制值
        /// </summary>
        public static float GetControlValue(int inputGroup, int control) => 
            Function.Call<float>(Hash.GET_CONTROL_VALUE, inputGroup, control);

        /// <summary>
        /// 获取控制正常值
        /// </summary>
        public static float GetControlNormal(int inputGroup, int control) => 
            Function.Call<float>(Hash.GET_CONTROL_NORMAL, inputGroup, control);

        /// <summary>
        /// 获取控制位移值
        /// </summary>
        public static float GetControlDisplacement(int inputGroup, int control) => 
            Function.Call<float>(Hash.GET_CONTROL_DISPLACEMENT, inputGroup, control);

        /// <summary>
        /// 设置控制值
        /// </summary>
        public static void SetControlValue(int inputGroup, int control, float value) => 
            Function.Call(Hash.SET_CONTROL_VALUE, inputGroup, control, value);

        /// <summary>
        /// 检查是否使用键盘
        /// </summary>
        public static bool IsUsingKeyboard(int inputGroup) => 
            Function.Call<bool>(Hash.IS_USING_KEYBOARD, inputGroup);

        #endregion

        #region PATHFIND (60 natives)

        /// <summary>
        /// 计算导航网格路径
        /// </summary>
        public static bool CalculateTravelDistanceBetweenPoints(float x1, float y1, float z1, float x2, float y2, float z2) => 
            Function.Call<bool>(Hash.CALCULATE_TRAVEL_DISTANCE_BETWEEN_POINTS, x1, y1, z1, x2, y2, z2);

        /// <summary>
        /// 获取最近的路点
        /// </summary>
        public static bool GetClosestVehicleNode(float x, float y, float z, out Vector3 outPosition, int nodeType, float p5, float p6) => 
            Function.Call<bool>(Hash.GET_CLOSEST_VEHICLE_NODE, x, y, z, outPosition, nodeType, p5, p6);

        /// <summary>
        /// 获取主要路点坐标
        /// </summary>
        public static bool GetClosestMajorVehicleNode(float x, float y, float z, out Vector3 outPosition, float p4, float p5) => 
            Function.Call<bool>(Hash.GET_CLOSEST_MAJOR_VEHICLE_NODE, x, y, z, outPosition, p4, p5);

        /// <summary>
        /// 获取路点位置
        /// </summary>
        public static bool GetNthClosestVehicleNode(float x, float y, float z, int nthClosest, out Vector3 outPosition, int nodeFlags, float p6, float p7) => 
            Function.Call<bool>(Hash.GET_NTH_CLOSEST_VEHICLE_NODE, x, y, z, nthClosest, outPosition, nodeFlags, p6, p7);

        /// <summary>
        /// 获取安全坐标
        /// </summary>
        public static bool GetSafeCoordForPed(float x, float y, float z, bool onGround, out Vector3 outPosition, int flags) => 
            Function.Call<bool>(Hash.GET_SAFE_COORD_FOR_PED, x, y, z, onGround, outPosition, flags);

        /// <summary>
        /// 获取下一辆车将产生在的路点
        /// </summary>
        public static bool GetNthClosestVehicleNodeId(float x, float y, float z, int nth, int nodeFlags, float p5, float p6) => 
            Function.Call<bool>(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_ID, x, y, z, nth, nodeFlags, p5, p6);

        /// <summary>
        /// 设置路点作为排除
        /// </summary>
        public static void SetRoadNodeInAngle(float x, float y, float z, float p3, float p4, float p5, bool p6) => 
            Function.Call(Hash.SET_ROAD_NODE_IN_ANGLE, x, y, z, p3, p4, p5, p6);

        /// <summary>
        /// 设置所有路点作为排除
        /// </summary>
        public static void SetAllVehicleGeneratorsActiveInArea(float x1, float y1, float z1, float x2, float y2, float z2, bool p6, bool p7) => 
            Function.Call(Hash.SET_ALL_VEHICLE_GENERATORS_ACTIVE_IN_AREA, x1, y1, z1, x2, y2, z2, p6, p7);

        #endregion

        #region INTERIOR (45 natives)

        /// <summary>
        /// 获取内部位置
        /// </summary>
        public static Interior GetInteriorAtCoords(float x, float y, float z) => 
            Function.Call<Interior>(Hash.GET_INTERIOR_AT_COORDS, x, y, z);

        /// <summary>
        /// 获取内部位置（使用类型）
        /// </summary>
        public static Interior GetInteriorAtCoordsWithType(float x, float y, float z, string interiorType) => 
            Function.Call<Interior>(Hash.GET_INTERIOR_AT_COORDS_WITH_TYPE, x, y, z, interiorType);

        /// <summary>
        /// 检查坐标是否在内部
        /// </summary>
        public static bool IsInteriorScene() => Function.Call<bool>(Hash.IS_INTERIOR_SCENE);

        /// <summary>
        /// 检查实体是否在内部
        /// </summary>
        public static bool IsInteriorEntitySetActive(Interior interior, string entitySetName) => 
            Function.Call<bool>(Hash.IS_INTERIOR_ENTITY_SET_ACTIVE, interior, entitySetName);

        /// <summary>
        /// 激活内部实体集
        /// </summary>
        public static void ActivateInteriorEntitySet(Interior interior, string entitySetName, int p2) => 
            Function.Call(Hash.ACTIVATE_INTERIOR_ENTITY_SET, interior, entitySetName, p2);

        /// <summary>
        /// 取消激活内部实体集
        /// </summary>
        public static void DeactivateInteriorEntitySet(Interior interior, string entitySetName, int p2) => 
            Function.Call(Hash.DEACTIVATE_INTERIOR_ENTITY_SET, interior, entitySetName, p2);

        /// <summary>
        /// 检查内部是否有效
        /// </summary>
        public static bool IsValidInterior(Interior interior) => 
            Function.Call<bool>(Hash.IS_VALID_INTERIOR, interior);

        /// <summary>
        /// 清除内部
        /// </summary>
        public static void ClearInterior(Interior interior, int p1) => 
            Function.Call(Hash.CLEAR_INTERIOR, interior, p1);

        /// <summary>
        /// 刷新内部
        /// </summary>
        public static void RefreshInterior(Interior interior) => 
            Function.Call(Hash.REFRESH_INTERIOR, interior);

        /// <summary>
        /// 禁用内部音频
        /// </summary>
        public static void DisableInteriorAudio(Interior interior, bool toggle) => 
            Function.Call(Hash.DISABLE_INTERIOR_AUDIO, interior, toggle);

        #endregion

        #region ZONE (9 natives)

        /// <summary>
        /// 获取区域名称
        /// </summary>
        public static string GetNameOfZone(float x, float y, float z) => 
            Function.Call<string>(Hash.GET_NAME_OF_ZONE, x, y, z);

        /// <summary>
        /// 获取区域ID
        /// </summary>
        public static int GetZoneAtCoords(float x, float y, float z) => 
            Function.Call<int>(Hash.GET_ZONE_AT_COORDS, x, y, z);

        /// <summary>
        /// 获取区域人口类型
        /// </summary>
        public static int GetZonePopschedule(int zoneId) => 
            Function.Call<int>(Hash.GET_ZONE_POPSCHEDULE, zoneId);

        /// <summary>
        /// 获取区域扫描器
        /// </summary>
        public static string GetZoneScumminess(int zoneId) => 
            Function.Call<string>(Hash.GET_ZONE_SCUMMINESS, zoneId);

        /// <summary>
        /// 设置区域扫描器
        /// </summary>
        public static void SetZoneEnabled(int zoneId, bool toggle) => 
            Function.Call(Hash.SET_ZONE_ENABLED, zoneId, toggle);

        #endregion

        #region WATER (12 natives)

        /// <summary>
        /// 测试垂直探测
        /// </summary>
        public static bool TestVerticalProbeAgainstAllWater(float x, float y, float z, int p3, out float height) => 
            Function.Call<bool>(Hash.TEST_VERTICAL_PROBE_AGAINST_ALL_WATER, x, y, z, p3, height);

        /// <summary>
        /// 修改水位
        /// </summary>
        public static void ModifyWater(float x, float y, float radius, float height) => 
            Function.Call(Hash.MODIFY_WATER, x, y, radius, height);

        /// <summary>
        /// 设置水位
        /// </summary>
        public static void SetWaterLevel(float x, float y, float z, float height, bool p4) => 
            Function.Call(Hash.SET_WATER_LEVEL, x, y, z, height, p4);

        /// <summary>
        /// 获取水位
        /// </summary>
        public static bool GetWaterHeight(float x, float y, float z, out float height) => 
            Function.Call<bool>(Hash.GET_WATER_HEIGHT, x, y, z, height);

        /// <summary>
        /// 获取水位（无波浪）
        /// </summary>
        public static bool GetWaterHeightNoWaves(float x, float y, float z, out float height) => 
            Function.Call<bool>(Hash.GET_WATER_HEIGHT_NO_WAVES, x, y, z, height);

        /// <summary>
        /// 测试探测
        /// </summary>
        public static bool TestProbeAgainstAllWater(float x1, float y1, float z1, float x2, float y2, float z2, int flags, out float result) => 
            Function.Call<bool>(Hash.TEST_PROBE_AGAINST_ALL_WATER, x1, y1, z1, x2, y2, z2, flags, result);

        /// <summary>
        /// 测试探测
        /// </summary>
        public static bool TestProbeAgainstWater(float x1, float y1, float z1, float x2, float y2, float z2, out Vector3 result) => 
            Function.Call<bool>(Hash.TEST_PROBE_AGAINST_WATER, x1, y1, z1, x2, y2, z2, result);

        /// <summary>
        /// 设置海洋调节器
        /// </summary>
        public static void SetDeepOceanScaler(float scaler) => 
            Function.Call(Hash.SET_DEEP_OCEAN_SCALER, scaler);

        /// <summary>
        /// 获取海洋调节器
        /// </summary>
        public static float GetDeepOceanScaler() => Function.Call<float>(Hash.GET_DEEP_OCEAN_SCALER);

        /// <summary>
        /// 重置海洋调节器
        /// </summary>
        public static void ResetDeepOceanScaler() => Function.Call(Hash.RESET_DEEP_OCEAN_SCALER);

        /// <summary>
        /// 启用/禁用水
        /// </summary>
        public static void SetDisableWaterRendering(bool toggle) => 
            Function.Call(Hash.SET_DISABLE_WATER_RENDERING, toggle);

        #endregion
    }
}
