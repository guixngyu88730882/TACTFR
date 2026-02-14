# HUD_UI - Native 函数参考

> 命名空间: HUD, MOBILE

---

## 快速索引

| 函数名 | C# 签名 | 返回值 | 说明 |
|--------|---------|--------|------|
| `ACTIVATE_FRONTEND_MENU` | `void ACTIVATE_FRONTEND_MENU(uint menuhash, bool togglePause, int component)` | void | Does stuff like this:<br>gyazo.com/7fcb78ea3520e3dbc5b2c0c0f3712617<br><br>Example:<br>int GetHash = GET_HASH_KE... |
| `ADD_BLIP_FOR_AREA` | `int ADD_BLIP_FOR_AREA(float x, float y, float z, float width, float height)` | int | Adds a rectangular blip for the specified coordinates/area.<br><br>It is recommended to use SET_BLIP_ROTAT... |
| `ADD_BLIP_FOR_COORD` | `int ADD_BLIP_FOR_COORD(float x, float y, float z)` | int | Creates an orange ( default ) Blip-object. Returns a Blip-object which can then be modified. |
| `ADD_BLIP_FOR_ENTITY` | `int ADD_BLIP_FOR_ENTITY(int entity)` | int | Returns red ( default ) blip attached to entity.<br><br>Example:<br>Blip blip; //Put this outside your case o... |
| `ADD_BLIP_FOR_PICKUP` | `int ADD_BLIP_FOR_PICKUP(int pickup)` | int | - |
| `ADD_BLIP_FOR_RADIUS` | `int ADD_BLIP_FOR_RADIUS(float posX, float posY, float posZ, float radius)` | int | - |
| `ADD_NEXT_MESSAGE_TO_PREVIOUS_BRIEFS` | `void ADD_NEXT_MESSAGE_TO_PREVIOUS_BRIEFS(bool p0)` | void | - |
| `ADD_POINT_TO_GPS_CUSTOM_ROUTE` | `void ADD_POINT_TO_GPS_CUSTOM_ROUTE(float x, float y, float z)` | void | - |
| `ADD_POINT_TO_GPS_MULTI_ROUTE` | `void ADD_POINT_TO_GPS_MULTI_ROUTE(float x, float y, float z)` | void | - |
| `ADD_TEXT_COMPONENT_FLOAT` | `void ADD_TEXT_COMPONENT_FLOAT(float value, int decimalPlaces)` | void | - |
| `ADD_TEXT_COMPONENT_FORMATTED_INTEGER` | `void ADD_TEXT_COMPONENT_FORMATTED_INTEGER(int value, bool commaSeparated)` | void | - |
| `ADD_TEXT_COMPONENT_INTEGER` | `void ADD_TEXT_COMPONENT_INTEGER(int value)` | void | - |
| `ADD_TEXT_COMPONENT_SUBSTRING_BLIP_NAME` | `void ADD_TEXT_COMPONENT_SUBSTRING_BLIP_NAME(int blip)` | void | - |
| `ADD_TEXT_COMPONENT_SUBSTRING_KEYBOARD_DISPLAY` | `void ADD_TEXT_COMPONENT_SUBSTRING_KEYBOARD_DISPLAY(string string)` | void | - |
| `ADD_TEXT_COMPONENT_SUBSTRING_PHONE_NUMBER` | `void ADD_TEXT_COMPONENT_SUBSTRING_PHONE_NUMBER(string p0, int p1)` | void | p1 was always -1 |
| `ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME` | `void ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(string text)` | void | - |
| `ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL` | `void ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL(string labelName)` | void | - |
| `ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL_HASH_KEY` | `void ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL_HASH_KEY(uint gxtEntryHash)` | void | It adds the localized text of the specified GXT entry name. Eg. if the argument is GET_HASH_KEY("ES_... |
| `ADD_TEXT_COMPONENT_SUBSTRING_TIME` | `void ADD_TEXT_COMPONENT_SUBSTRING_TIME(int timestamp, int flags)` | void | Adds a timer (e.g. "00:00:00:000"). The appearance of the timer depends on the flags, which needs mo... |
| `ADD_TEXT_COMPONENT_SUBSTRING_WEBSITE` | `void ADD_TEXT_COMPONENT_SUBSTRING_WEBSITE(string website)` | void | This native (along with ADD_TEXT_COMPONENT_SUBSTRING_KEYBOARD_DISPLAY and ADD_TEXT_COMPONENT_SUBSTRI... |
| `ADD_VALID_VEHICLE_HIT_HASH` | `void ADD_VALID_VEHICLE_HIT_HASH(object p0)` | void | - |
| `ALLOW_DISPLAY_OF_MULTIPLAYER_CASH_TEXT` | `void ALLOW_DISPLAY_OF_MULTIPLAYER_CASH_TEXT(bool allow)` | void | Controls whether to display 'Cash'/'Bank' next to the money balance HUD in Multiplayer (https://i.im... |
| `ALLOW_PAUSE_WHEN_NOT_IN_STATE_OF_PLAY_THIS_FRAME` | `void ALLOW_PAUSE_WHEN_NOT_IN_STATE_OF_PLAY_THIS_FRAME()` | void | Allows opening the pause menu this frame, when the player is dead. |
| `ALLOW_SONAR_BLIPS` | `void ALLOW_SONAR_BLIPS(bool toggle)` | void | - |
| `ARE_ONLINE_POLICIES_UP_TO_DATE` | `bool ARE_ONLINE_POLICIES_UP_TO_DATE()` | bool | - |
| `BEGIN_TEXT_COMMAND_ADD_DIRECTLY_TO_PREVIOUS_BRIEFS` | `void BEGIN_TEXT_COMMAND_ADD_DIRECTLY_TO_PREVIOUS_BRIEFS(string p0)` | void | - |
| `BEGIN_TEXT_COMMAND_BUSYSPINNER_ON` | `void BEGIN_TEXT_COMMAND_BUSYSPINNER_ON(string string)` | void | Initializes the text entry for the the text next to a loading prompt. All natives for building UI te... |
| `BEGIN_TEXT_COMMAND_CLEAR_PRINT` | `void BEGIN_TEXT_COMMAND_CLEAR_PRINT(string text)` | void | clears a print text command with this text |
| `BEGIN_TEXT_COMMAND_DISPLAY_HELP` | `void BEGIN_TEXT_COMMAND_DISPLAY_HELP(string inputType)` | void | Used to be known as _SET_TEXT_COMPONENT_FORMAT |
| `BEGIN_TEXT_COMMAND_DISPLAY_TEXT` | `void BEGIN_TEXT_COMMAND_DISPLAY_TEXT(string text)` | void | The following were found in the decompiled script files:<br>STRING, TWOSTRINGS, NUMBER, PERCENTAGE, FO_... |
| `BEGIN_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING` | `void BEGIN_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING(string entry)` | void | int GetLineCount(char *text, float x, float y)<br>    {<br>     BEGIN_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR... |
| `BEGIN_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT` | `void BEGIN_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT(string text)` | void | - |
| `BEGIN_TEXT_COMMAND_IS_MESSAGE_DISPLAYED` | `void BEGIN_TEXT_COMMAND_IS_MESSAGE_DISPLAYED(string text)` | void | nothin doin. <br><br>BOOL Message(const char* text)<br>   {<br>     BEGIN_TEXT_COMMAND_IS_MESSAGE_DISPLAYED("STR... |
| `BEGIN_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED` | `void BEGIN_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED(string labelName)` | void | BOOL IsContextActive(char *ctx)<br>    {<br>     BEGIN_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED(c... |
| `BEGIN_TEXT_COMMAND_OVERRIDE_BUTTON_TEXT` | `void BEGIN_TEXT_COMMAND_OVERRIDE_BUTTON_TEXT(string gxtEntry)` | void | - |
| `BEGIN_TEXT_COMMAND_PRINT` | `void BEGIN_TEXT_COMMAND_PRINT(string GxtEntry)` | void | void ShowSubtitle(const char *text)<br>{<br>  BEGIN_TEXT_COMMAND_PRINT("STRING");<br> ADD_TEXT_COMPONENT_SUBS... |
| `BEGIN_TEXT_COMMAND_SET_BLIP_NAME` | `void BEGIN_TEXT_COMMAND_SET_BLIP_NAME(string textLabel)` | void | Starts a text command to change the name of a blip displayed in the pause menu.<br>This should be paire... |
| `BEGIN_TEXT_COMMAND_THEFEED_POST` | `void BEGIN_TEXT_COMMAND_THEFEED_POST(string text)` | void | Declares the entry type of a notification, for example "STRING".<br><br>int ShowNotification(char *text)<br>{... |
| `BUSYSPINNER_IS_DISPLAYING` | `bool BUSYSPINNER_IS_DISPLAYING()` | bool | - |
| `BUSYSPINNER_IS_ON` | `bool BUSYSPINNER_IS_ON()` | bool | - |
| `BUSYSPINNER_OFF` | `void BUSYSPINNER_OFF()` | void | Removes the loading prompt at the bottom right of the screen. |
| `CAN_PHONE_BE_SEEN_ON_SCREEN` | `bool CAN_PHONE_BE_SEEN_ON_SCREEN()` | bool | This one is weird and seems to return a TRUE state regardless of whether the phone is visible on scr... |
| `CELL_CAM_ACTIVATE` | `void CELL_CAM_ACTIVATE(bool p0, bool p1)` | void | - |
| `CELL_CAM_ACTIVATE_SELFIE_MODE` | `void CELL_CAM_ACTIVATE_SELFIE_MODE(bool toggle)` | void | - |
| `CELL_CAM_ACTIVATE_SHALLOW_DOF_MODE` | `void CELL_CAM_ACTIVATE_SHALLOW_DOF_MODE(bool toggle)` | void | - |
| `CELL_CAM_IS_CHAR_VISIBLE_NO_FACE_CHECK` | `bool CELL_CAM_IS_CHAR_VISIBLE_NO_FACE_CHECK(int entity)` | bool | - |
| `CELL_CAM_SET_SELFIE_MODE_DISTANCE_SCALING` | `void CELL_CAM_SET_SELFIE_MODE_DISTANCE_SCALING(float distanceScaling)` | void | - |
| `CELL_CAM_SET_SELFIE_MODE_HEAD_PITCH_OFFSET` | `void CELL_CAM_SET_SELFIE_MODE_HEAD_PITCH_OFFSET(float pitch)` | void | - |
| `CELL_CAM_SET_SELFIE_MODE_HEAD_ROLL_OFFSET` | `void CELL_CAM_SET_SELFIE_MODE_HEAD_ROLL_OFFSET(float roll)` | void | - |
| `CELL_CAM_SET_SELFIE_MODE_HEAD_YAW_OFFSET` | `void CELL_CAM_SET_SELFIE_MODE_HEAD_YAW_OFFSET(float yaw)` | void | - |
| `CELL_CAM_SET_SELFIE_MODE_HORZ_PAN_OFFSET` | `void CELL_CAM_SET_SELFIE_MODE_HORZ_PAN_OFFSET(float horizontalPan)` | void | - |
| `CELL_CAM_SET_SELFIE_MODE_ROLL_OFFSET` | `void CELL_CAM_SET_SELFIE_MODE_ROLL_OFFSET(float roll)` | void | - |
| `CELL_CAM_SET_SELFIE_MODE_SIDE_OFFSET_SCALING` | `void CELL_CAM_SET_SELFIE_MODE_SIDE_OFFSET_SCALING(float p0)` | void | - |
| `CELL_CAM_SET_SELFIE_MODE_VERT_PAN_OFFSET` | `void CELL_CAM_SET_SELFIE_MODE_VERT_PAN_OFFSET(float vertPan)` | void | - |
| `CELL_HORIZONTAL_MODE_TOGGLE` | `void CELL_HORIZONTAL_MODE_TOGGLE(bool toggle)` | void | if the bool "Toggle" is "true" so the phone is lean.<br>if the bool "Toggle" is "false" so the phone is... |
| `CELL_SET_INPUT` | `void CELL_SET_INPUT(int direction)` | void | For move the finger of player, the value of int goes 1 at 5. |
| `CHANGE_FAKE_MP_CASH` | `void CHANGE_FAKE_MP_CASH(int cash, int bank)` | void | Displays cash change notifications on HUD. |
| `CLEAR_ADDITIONAL_TEXT` | `void CLEAR_ADDITIONAL_TEXT(int p0, bool p1)` | void | - |
| `CLEAR_ALL_BLIP_ROUTES` | `void CLEAR_ALL_BLIP_ROUTES()` | void | - |
| `CLEAR_ALL_HELP_MESSAGES` | `void CLEAR_ALL_HELP_MESSAGES()` | void | - |
| `CLEAR_BRIEF` | `void CLEAR_BRIEF()` | void | - |
| `CLEAR_DYNAMIC_PAUSE_MENU_ERROR_MESSAGE` | `void CLEAR_DYNAMIC_PAUSE_MENU_ERROR_MESSAGE()` | void | - |
| `CLEAR_FAKE_CONE_ARRAY` | `void CLEAR_FAKE_CONE_ARRAY()` | void | - |
| `CLEAR_FLOATING_HELP` | `void CLEAR_FLOATING_HELP(int hudIndex, bool p1)` | void | - |
| `CLEAR_GPS_CUSTOM_ROUTE` | `void CLEAR_GPS_CUSTOM_ROUTE()` | void | - |
| `CLEAR_GPS_FLAGS` | `void CLEAR_GPS_FLAGS()` | void | Clears the GPS flags. Only the script that originally called SET_GPS_FLAGS can clear them.<br><br>Doesn't ... |
| `CLEAR_GPS_MULTI_ROUTE` | `void CLEAR_GPS_MULTI_ROUTE()` | void | Does the same as SET_GPS_MULTI_ROUTE_RENDER(false); |
| `CLEAR_GPS_PLAYER_WAYPOINT` | `void CLEAR_GPS_PLAYER_WAYPOINT()` | void | - |
| `CLEAR_GPS_RACE_TRACK` | `void CLEAR_GPS_RACE_TRACK()` | void | Does the same as SET_RACE_TRACK_RENDER(false); |
| `CLEAR_HELP` | `void CLEAR_HELP(bool toggle)` | void | - |
| `CLEAR_PED_IN_PAUSE_MENU` | `void CLEAR_PED_IN_PAUSE_MENU()` | void | - |
| `CLEAR_PRINTS` | `void CLEAR_PRINTS()` | void | - |
| `CLEAR_REMINDER_MESSAGE` | `void CLEAR_REMINDER_MESSAGE()` | void | This native does absolutely nothing, just a nullsub |
| `CLEAR_SMALL_PRINTS` | `void CLEAR_SMALL_PRINTS()` | void | - |
| `CLEAR_THIS_PRINT` | `void CLEAR_THIS_PRINT(string p0)` | void | p0: found arguments in the b617d scripts: https://pastebin.com/X5akCN7z |
| `CLEAR_VALID_VEHICLE_HIT_HASHES` | `void CLEAR_VALID_VEHICLE_HIT_HASHES()` | void | - |
| `CLOSE_MP_TEXT_CHAT` | `void CLOSE_MP_TEXT_CHAT()` | void | - |
| `CLOSE_SOCIAL_CLUB_MENU` | `void CLOSE_SOCIAL_CLUB_MENU()` | void | - |
| `CODE_WANTS_SCRIPT_TO_TAKE_CONTROL` | `bool CODE_WANTS_SCRIPT_TO_TAKE_CONTROL()` | bool | - |
| `CREATE_FAKE_MP_GAMER_TAG` | `int CREATE_FAKE_MP_GAMER_TAG(int ped, string username, bool pointedClanTag, bool isRockstarClan, string clanTag, int clanFlag)` | int | clanFlag: takes a number 0-5 |
| `CREATE_MOBILE_PHONE` | `void CREATE_MOBILE_PHONE(int phoneType)` | void | Creates a mobile phone of the specified type.<br><br>Possible phone types:<br><br>0 - Default phone / Michael's ... |
| `CREATE_MP_GAMER_TAG_WITH_CREW_COLOR` | `void CREATE_MP_GAMER_TAG_WITH_CREW_COLOR(int player, string username, bool pointedClanTag, bool isRockstarClan, string clanTag, int clanFlag, int r, int g, int b)` | void | clanFlag: takes a number 0-5 |
| `CUSTOM_MINIMAP_CLEAR_BLIPS` | `void CUSTOM_MINIMAP_CLEAR_BLIPS()` | void | - |
| `CUSTOM_MINIMAP_CREATE_BLIP` | `int CUSTOM_MINIMAP_CREATE_BLIP(float x, float y, float z)` | int | Add a BLIP_GALLERY at the specific coordinate. Used in fm_maintain_transition_players to display rac... |
| `CUSTOM_MINIMAP_SET_ACTIVE` | `void CUSTOM_MINIMAP_SET_ACTIVE(bool toggle)` | void | If toggle is true, the map is shown in full screen<br>If toggle is false, the map is shown in normal mo... |
| `CUSTOM_MINIMAP_SET_BLIP_OBJECT` | `void CUSTOM_MINIMAP_SET_BLIP_OBJECT(int spriteId)` | void | Sets the sprite of the next BLIP_GALLERY blip, values used in the native scripts: 143 (ObjectiveBlue... |
| `DELETE_WAYPOINTS_FROM_THIS_PLAYER` | `void DELETE_WAYPOINTS_FROM_THIS_PLAYER()` | void | - |
| `DESTROY_MOBILE_PHONE` | `void DESTROY_MOBILE_PHONE()` | void | Destroys the currently active mobile phone. |
| `DISABLE_FRONTEND_THIS_FRAME` | `void DISABLE_FRONTEND_THIS_FRAME()` | void | - |
| `DISABLE_PAUSEMENU_SPINNER` | `void DISABLE_PAUSEMENU_SPINNER(bool p0)` | void | - |
| `DISPLAY_AMMO_THIS_FRAME` | `void DISPLAY_AMMO_THIS_FRAME(bool display)` | void | - |
| `DISPLAY_AREA_NAME` | `void DISPLAY_AREA_NAME(bool toggle)` | void | - |
| `DISPLAY_CASH` | `void DISPLAY_CASH(bool toggle)` | void | "DISPLAY_CASH(false);" makes the cash amount render on the screen when appropriate<br>"DISPLAY_CASH(tru... |
| `DISPLAY_HELP_TEXT_THIS_FRAME` | `void DISPLAY_HELP_TEXT_THIS_FRAME(string message, bool p1)` | void | The messages are localized strings.<br>Examples:<br>"No_bus_money"<br>"Enter_bus"<br>"Tour_help"<br>"LETTERS_HELP2"... |
| `DISPLAY_HUD` | `void DISPLAY_HUD(bool toggle)` | void | If Hud should be displayed |
| `DISPLAY_HUD_WHEN_NOT_IN_STATE_OF_PLAY_THIS_FRAME` | `void DISPLAY_HUD_WHEN_NOT_IN_STATE_OF_PLAY_THIS_FRAME()` | void | Enables drawing some hud components, such as help labels, this frame, when the player is dead. |
| `DISPLAY_HUD_WHEN_PAUSED_THIS_FRAME` | `void DISPLAY_HUD_WHEN_PAUSED_THIS_FRAME()` | void | - |
| `DISPLAY_PLAYER_NAME_TAGS_ON_BLIPS` | `void DISPLAY_PLAYER_NAME_TAGS_ON_BLIPS(bool toggle)` | void | Toggles whether or not name labels are shown on the expanded minimap next to player blips, like in G... |
| `DISPLAY_RADAR` | `void DISPLAY_RADAR(bool toggle)` | void | If Minimap / Radar should be displayed. |
| `DISPLAY_SNIPER_SCOPE_THIS_FRAME` | `void DISPLAY_SNIPER_SCOPE_THIS_FRAME()` | void | Displays the crosshair for this frame. |
| `DOES_BLIP_EXIST` | `bool DOES_BLIP_EXIST(int blip)` | bool | - |
| `DOES_BLIP_HAVE_GPS_ROUTE` | `bool DOES_BLIP_HAVE_GPS_ROUTE(int blip)` | bool | - |
| `DOES_PED_HAVE_AI_BLIP` | `bool DOES_PED_HAVE_AI_BLIP(int ped)` | bool | - |
| `DOES_TEXT_BLOCK_EXIST` | `bool DOES_TEXT_BLOCK_EXIST(string gxt)` | bool | - |
| `DOES_TEXT_LABEL_EXIST` | `bool DOES_TEXT_LABEL_EXIST(string gxt)` | bool | Checks if the passed gxt name exists in the game files. |
| `DONT_TILT_MINIMAP_THIS_FRAME` | `void DONT_TILT_MINIMAP_THIS_FRAME()` | void | When calling this, the current frame will have the players "arrow icon" be focused on the dead cente... |
| `DONT_ZOOM_MINIMAP_WHEN_RUNNING_THIS_FRAME` | `void DONT_ZOOM_MINIMAP_WHEN_RUNNING_THIS_FRAME()` | void | - |
| `DONT_ZOOM_MINIMAP_WHEN_SNIPING_THIS_FRAME` | `void DONT_ZOOM_MINIMAP_WHEN_SNIPING_THIS_FRAME()` | void | - |
| `DRAW_FRONTEND_BACKGROUND_THIS_FRAME` | `void DRAW_FRONTEND_BACKGROUND_THIS_FRAME()` | void | This native does absolutely nothing, just a nullsub |
| `DRAW_HUD_OVER_FADE_THIS_FRAME` | `void DRAW_HUD_OVER_FADE_THIS_FRAME()` | void | - |
| `END_TEXT_COMMAND_ADD_DIRECTLY_TO_PREVIOUS_BRIEFS` | `void END_TEXT_COMMAND_ADD_DIRECTLY_TO_PREVIOUS_BRIEFS(bool p0)` | void | - |
| `END_TEXT_COMMAND_BUSYSPINNER_ON` | `void END_TEXT_COMMAND_BUSYSPINNER_ON(int busySpinnerType)` | void | enum eBusySpinnerType<br>{<br>	BUSY_SPINNER_LEFT,<br>	BUSY_SPINNER_LEFT_2,<br>	BUSY_SPINNER_LEFT_3,<br>	BUSY_SPINNE... |
| `END_TEXT_COMMAND_CLEAR_PRINT` | `void END_TEXT_COMMAND_CLEAR_PRINT()` | void | - |
| `END_TEXT_COMMAND_DISPLAY_HELP` | `void END_TEXT_COMMAND_DISPLAY_HELP(int p0, bool loop, bool beep, int shape)` | void | shape goes from -1 to 50 (may be more).<br>p0 is always 0.<br><br>Example:<br>void FloatingHelpText(const char* ... |
| `END_TEXT_COMMAND_DISPLAY_TEXT` | `void END_TEXT_COMMAND_DISPLAY_TEXT(float x, float y, int p2)` | void | After applying the properties to the text (See HUD::SET_TEXT_), this will draw the text in the appli... |
| `END_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING` | `int END_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING(float x, float y)` | int | Determines how many lines the text string will use when drawn on screen. <br>Must use 0x521FB041D93DD0E... |
| `END_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT` | `float END_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT(bool p0)` | float | - |
| `END_TEXT_COMMAND_IS_MESSAGE_DISPLAYED` | `bool END_TEXT_COMMAND_IS_MESSAGE_DISPLAYED()` | bool | - |
| `END_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED` | `bool END_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED(int p0)` | bool | - |
| `END_TEXT_COMMAND_OVERRIDE_BUTTON_TEXT` | `void END_TEXT_COMMAND_OVERRIDE_BUTTON_TEXT(int p0)` | void | - |
| `END_TEXT_COMMAND_PRINT` | `void END_TEXT_COMMAND_PRINT(int duration, bool drawImmediately)` | void | Draws the subtitle at middle center of the screen.<br><br>int duration = time in milliseconds to show text... |
| `END_TEXT_COMMAND_SET_BLIP_NAME` | `void END_TEXT_COMMAND_SET_BLIP_NAME(int blip)` | void | Finalizes a text command started with BEGIN_TEXT_COMMAND_SET_BLIP_NAME, setting the name of the spec... |
| `END_TEXT_COMMAND_THEFEED_POST_AWARD` | `int END_TEXT_COMMAND_THEFEED_POST_AWARD(string textureDict, string textureName, int rpBonus, int colorOverlay, string titleLabel)` | int | Shows an "award" notification above the minimap, example: https://i.imgur.com/e2DNaKX.png<br>Example:<br><br>... |
| `END_TEXT_COMMAND_THEFEED_POST_CREWTAG` | `int END_TEXT_COMMAND_THEFEED_POST_CREWTAG(bool p0, bool p1, ref int p2, int p3, bool isLeader, bool unk0, int clanDesc, int R, int G, int B)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_CREWTAG_WITH_GAME_NAME` | `int END_TEXT_COMMAND_THEFEED_POST_CREWTAG_WITH_GAME_NAME(bool p0, bool p1, ref int p2, int p3, bool isLeader, bool unk0, int clanDesc, string playerName, int R, int G, int B)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_CREW_RANKUP_WITH_LITERAL_FLAG` | `int END_TEXT_COMMAND_THEFEED_POST_CREW_RANKUP_WITH_LITERAL_FLAG(string p0, string p1, string p2, bool p3, bool p4)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT` | `int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(string txdName, string textureName, bool flash, int iconType, string sender, string subject)` | int | This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_D... |
| `END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_SUBTITLE_LABEL` | `int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_SUBTITLE_LABEL(string txdName, string textureName, bool flash, int iconType, string sender, string subject)` | int | This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_D... |
| `END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_TU` | `int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_TU(string txdName, string textureName, bool flash, int iconType, string sender, string subject, float duration)` | int | This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_D... |
| `END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_WITH_CREW_TAG` | `int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_WITH_CREW_TAG(string txdName, string textureName, bool flash, int iconType, string sender, string subject, float duration, string clanTag)` | int | This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_D... |
| `END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_WITH_CREW_TAG_AND_ADDITIONAL_ICON` | `int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_WITH_CREW_TAG_AND_ADDITIONAL_ICON(string txdName, string textureName, bool flash, int iconType1, string sender, string subject, float duration, string clanTag, int iconType2, int p9)` | int | This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_D... |
| `END_TEXT_COMMAND_THEFEED_POST_MPTICKER` | `int END_TEXT_COMMAND_THEFEED_POST_MPTICKER(bool blink, bool p1)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_REPLAY` | `int END_TEXT_COMMAND_THEFEED_POST_REPLAY(int type, int image, string text)` | int | returns a notification handle, prints out a notification like below:<br>type range: 0 - 2<br>if you set ty... |
| `END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT` | `int END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT(int type, string button, string text)` | int | returns a notification handle, prints out a notification like below:<br>type range: 0 - 2<br>if you set ty... |
| `END_TEXT_COMMAND_THEFEED_POST_STATS` | `int END_TEXT_COMMAND_THEFEED_POST_STATS(string statTitle, int iconEnum, bool stepVal, int barValue, bool isImportant, string pictureTextureDict, string pictureTextureName)` | int | List of picture names: https://pastebin.com/XdpJVbHz<br>Example result: https://i.imgur.com/SdEZ22m.png |
| `END_TEXT_COMMAND_THEFEED_POST_TICKER` | `int END_TEXT_COMMAND_THEFEED_POST_TICKER(bool blink, bool p1)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_TICKER_FORCED` | `int END_TEXT_COMMAND_THEFEED_POST_TICKER_FORCED(bool blink, bool p1)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_TICKER_WITH_TOKENS` | `int END_TEXT_COMMAND_THEFEED_POST_TICKER_WITH_TOKENS(bool blink, bool p1)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_UNLOCK` | `int END_TEXT_COMMAND_THEFEED_POST_UNLOCK(string gxtLabel1, int p1, string gxtLabel2)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_UNLOCK_TU` | `int END_TEXT_COMMAND_THEFEED_POST_UNLOCK_TU(string gxtLabel1, int p1, string gxtLabel2, int p3)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_UNLOCK_TU_WITH_COLOR` | `int END_TEXT_COMMAND_THEFEED_POST_UNLOCK_TU_WITH_COLOR(object p0, object p1, object p2, object p3, object p4, object p5)` | int | - |
| `END_TEXT_COMMAND_THEFEED_POST_VERSUS_TU` | `int END_TEXT_COMMAND_THEFEED_POST_VERSUS_TU(string txdName1, string textureName1, int count1, string txdName2, string textureName2, int count2, int hudColor1, int hudColor2)` | int | This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_D... |
| `FLAG_PLAYER_CONTEXT_IN_TOURNAMENT` | `void FLAG_PLAYER_CONTEXT_IN_TOURNAMENT(bool toggle)` | void | - |
| `FLASH_ABILITY_BAR` | `void FLASH_ABILITY_BAR(int millisecondsToFlash)` | void | - |
| `FLASH_MINIMAP_DISPLAY` | `void FLASH_MINIMAP_DISPLAY()` | void | adds a short flash to the Radar/Minimap<br>Usage: UI.FLASH_MINIMAP_DISPLAY |
| `FLASH_MINIMAP_DISPLAY_WITH_COLOR` | `void FLASH_MINIMAP_DISPLAY_WITH_COLOR(int hudColorIndex)` | void | - |
| `FLASH_WANTED_DISPLAY` | `void FLASH_WANTED_DISPLAY(bool p0)` | void | - |
| `FORCE_CLOSE_REPORTUGC_MENU` | `void FORCE_CLOSE_REPORTUGC_MENU()` | void | - |
| `FORCE_CLOSE_TEXT_INPUT_BOX` | `void FORCE_CLOSE_TEXT_INPUT_BOX()` | void | - |
| `FORCE_NEXT_MESSAGE_TO_PREVIOUS_BRIEFS_LIST` | `void FORCE_NEXT_MESSAGE_TO_PREVIOUS_BRIEFS_LIST(int p0)` | void | - |
| `FORCE_OFF_WANTED_STAR_FLASH` | `void FORCE_OFF_WANTED_STAR_FLASH(bool toggle)` | void | - |
| `FORCE_SCRIPTED_GFX_WHEN_FRONTEND_ACTIVE` | `void FORCE_SCRIPTED_GFX_WHEN_FRONTEND_ACTIVE(string p0)` | void | Not present in retail version of the game, actual definiton seems to be<br>_LOG_DEBUG_INFO(const char* ... |
| `FORCE_SONAR_BLIPS_THIS_FRAME` | `bool FORCE_SONAR_BLIPS_THIS_FRAME()` | bool | Doesn't actually return anything. |
| `GET_AI_PED_PED_BLIP_INDEX` | `int GET_AI_PED_PED_BLIP_INDEX(int ped)` | int | - |
| `GET_AI_PED_VEHICLE_BLIP_INDEX` | `int GET_AI_PED_VEHICLE_BLIP_INDEX(int ped)` | int | Returns the current AI BLIP for the specified ped |
| `GET_BLIP_ALPHA` | `int GET_BLIP_ALPHA(int blip)` | int | - |
| `GET_BLIP_COLOUR` | `int GET_BLIP_COLOUR(int blip)` | int | - |
| `GET_BLIP_COORDS` | `Vector3 GET_BLIP_COORDS(int blip)` | Vector3 | - |
| `GET_BLIP_FADE_DIRECTION` | `int GET_BLIP_FADE_DIRECTION(int blip)` | int | Returns -1, 0, +1, depending on if the blip is fading out, doing nothing, or fading in respectively. |
| `GET_BLIP_FROM_ENTITY` | `int GET_BLIP_FROM_ENTITY(int entity)` | int | Returns the Blip handle of given Entity. |
| `GET_BLIP_HUD_COLOUR` | `int GET_BLIP_HUD_COLOUR(int blip)` | int | - |
| `GET_BLIP_INFO_ID_COORD` | `Vector3 GET_BLIP_INFO_ID_COORD(int blip)` | Vector3 | - |
| `GET_BLIP_INFO_ID_DISPLAY` | `int GET_BLIP_INFO_ID_DISPLAY(int blip)` | int | - |
| `GET_BLIP_INFO_ID_ENTITY_INDEX` | `int GET_BLIP_INFO_ID_ENTITY_INDEX(int blip)` | int | - |
| `GET_BLIP_INFO_ID_PICKUP_INDEX` | `int GET_BLIP_INFO_ID_PICKUP_INDEX(int blip)` | int | This function is hard-coded to always return 0. |
| `GET_BLIP_INFO_ID_TYPE` | `int GET_BLIP_INFO_ID_TYPE(int blip)` | int | Returns a value based on what the blip is attached to<br>1 - Vehicle<br>2 - Ped<br>3 - Object<br>4 - Coord<br>5 - u... |
| `GET_BLIP_ROTATION` | `int GET_BLIP_ROTATION(int blip)` | int | - |
| `GET_BLIP_SPRITE` | `int GET_BLIP_SPRITE(int blip)` | int | Blips Images + IDs:<br>gtaxscripting.blogspot.com/2016/05/gta-v-blips-id-and-image.html |
| `GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME` | `string GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME(string text, int position, int length)` | string | Returns a substring of a specified length starting at a specified position.<br><br>Example:<br>// Get "STRING... |
| `GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_BYTES` | `string GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_BYTES(string text, int startPosition, int endPosition)` | string | Returns a substring that is between two specified positions. The length of the string will be calcul... |
| `GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_WITH_BYTE_LIMIT` | `string GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_WITH_BYTE_LIMIT(string text, int position, int length, int maxLength)` | string | Returns a substring of a specified length starting at a specified position. The result is guaranteed... |
| `GET_CHARACTER_MENU_PED_FLOAT_STAT` | `bool GET_CHARACTER_MENU_PED_FLOAT_STAT(float statHash, ref float outValue, bool p2)` | bool | - |
| `GET_CHARACTER_MENU_PED_INT_STAT` | `bool GET_CHARACTER_MENU_PED_INT_STAT(object p0, ref object p1, object p2)` | bool | - |
| `GET_CHARACTER_MENU_PED_MASKED_INT_STAT` | `bool GET_CHARACTER_MENU_PED_MASKED_INT_STAT(uint statHash, ref object outValue, int p2, int mask, bool p4)` | bool | - |
| `GET_CLOSEST_BLIP_INFO_ID` | `int GET_CLOSEST_BLIP_INFO_ID(int blipSprite)` | int | - |
| `GET_CURRENT_FRONTEND_MENU_VERSION` | `uint GET_CURRENT_FRONTEND_MENU_VERSION()` | uint | if (HUD::GET_CURRENT_FRONTEND_MENU_VERSION() == joaat("fe_menu_version_empty_no_background")) |
| `GET_CURRENT_WEBPAGE_ID` | `int GET_CURRENT_WEBPAGE_ID()` | int | - |
| `GET_CURRENT_WEBSITE_ID` | `int GET_CURRENT_WEBSITE_ID()` | int | - |
| `GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID` | `int GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID()` | int | This function is hard-coded to always return 1. |
| `GET_FAKE_SPECTATOR_MODE` | `bool GET_FAKE_SPECTATOR_MODE()` | bool | Getter for SET_FAKE_SPECTATOR_MODE |
| `GET_FILENAME_FOR_AUDIO_CONVERSATION` | `string GET_FILENAME_FOR_AUDIO_CONVERSATION(string labelName)` | string | Gets a localized string literal from a label name. Can be used for output of e.g. VEHICLE::GET_LIVER... |
| `GET_FIRST_BLIP_INFO_ID` | `int GET_FIRST_BLIP_INFO_ID(int blipSprite)` | int | - |
| `GET_FIRST_N_CHARACTERS_OF_LITERAL_STRING` | `string GET_FIRST_N_CHARACTERS_OF_LITERAL_STRING(string string, int length)` | string | - |
| `GET_GLOBAL_ACTIONSCRIPT_FLAG` | `int GET_GLOBAL_ACTIONSCRIPT_FLAG(int flagIndex)` | int | Returns the ActionScript flagValue.<br>ActionScript flags are global flags that scaleforms use<br>Flags fo... |
| `GET_HUD_COLOUR` | `void GET_HUD_COLOUR(int hudColorIndex, ref int r, ref int g, ref int b, ref int a)` | void | - |
| `GET_HUD_COMPONENT_POSITION` | `Vector3 GET_HUD_COMPONENT_POSITION(int id)` | Vector3 | - |
| `GET_HUD_SCREEN_POSITION_FROM_WORLD_POSITION` | `int GET_HUD_SCREEN_POSITION_FROM_WORLD_POSITION(float worldX, float worldY, float worldZ, ref float screenX, ref float screenY)` | int | World to relative screen coords, this world to screen will keep the text on screen. Was named _GET_S... |
| `GET_LENGTH_OF_LITERAL_STRING` | `int GET_LENGTH_OF_LITERAL_STRING(string string)` | int | Returns the length of the string passed (much like strlen). |
| `GET_LENGTH_OF_LITERAL_STRING_IN_BYTES` | `int GET_LENGTH_OF_LITERAL_STRING_IN_BYTES(string string)` | int | - |
| `GET_LENGTH_OF_STRING_WITH_THIS_TEXT_LABEL` | `int GET_LENGTH_OF_STRING_WITH_THIS_TEXT_LABEL(string gxt)` | int | Returns the string length of the string from the gxt string . |
| `GET_MAIN_PLAYER_BLIP_ID` | `int GET_MAIN_PLAYER_BLIP_ID()` | int | - |
| `GET_MENU_LAYOUT_CHANGED_EVENT_DETAILS` | `void GET_MENU_LAYOUT_CHANGED_EVENT_DETAILS(ref int lastItemMenuId, ref int selectedItemMenuId, ref int selectedItemUniqueId)` | void | lastItemMenuId: this is the menuID of the last selected item minus 1000 (lastItem.menuID - 1000)<br>sel... |
| `GET_MENU_PED_BOOL_STAT` | `bool GET_MENU_PED_BOOL_STAT(uint statHash, ref bool outValue)` | bool | p0 was always 0xAE2602A3. |
| `GET_MENU_PED_FLOAT_STAT` | `bool GET_MENU_PED_FLOAT_STAT(uint statHash, ref float outValue)` | bool | - |
| `GET_MENU_PED_INT_STAT` | `bool GET_MENU_PED_INT_STAT(object p0, ref object p1)` | bool | - |
| `GET_MENU_PED_MASKED_INT_STAT` | `bool GET_MENU_PED_MASKED_INT_STAT(uint statHash, ref int outValue, int mask, bool p3)` | bool | - |
| `GET_MENU_TRIGGER_EVENT_DETAILS` | `void GET_MENU_TRIGGER_EVENT_DETAILS(ref int lastItemMenuId, ref int selectedItemUniqueId)` | void | - |
| `GET_MINIMAP_FOW_COORDINATE_IS_REVEALED` | `bool GET_MINIMAP_FOW_COORDINATE_IS_REVEALED(float x, float y, float z)` | bool | - |
| `GET_MINIMAP_FOW_DISCOVERY_RATIO` | `float GET_MINIMAP_FOW_DISCOVERY_RATIO()` | float | - |
| `GET_MOBILE_PHONE_POSITION` | `void GET_MOBILE_PHONE_POSITION(ref Vector3 position)` | void | - |
| `GET_MOBILE_PHONE_RENDER_ID` | `void GET_MOBILE_PHONE_RENDER_ID(ref int renderId)` | void | - |
| `GET_MOBILE_PHONE_ROTATION` | `void GET_MOBILE_PHONE_ROTATION(ref Vector3 rotation, int p1)` | void | - |
| `GET_MOUSE_EVENT` | `bool GET_MOUSE_EVENT(int scaleformHandle, ref object p1, ref object p2, ref object p3)` | bool | - |
| `GET_NAMED_RENDERTARGET_RENDER_ID` | `int GET_NAMED_RENDERTARGET_RENDER_ID(string name)` | int | - |
| `GET_NEW_SELECTED_MISSION_CREATOR_BLIP` | `int GET_NEW_SELECTED_MISSION_CREATOR_BLIP()` | int | - |
| `GET_NEXT_BLIP_INFO_ID` | `int GET_NEXT_BLIP_INFO_ID(int blipSprite)` | int | - |
| `GET_NORTH_BLID_INDEX` | `int GET_NORTH_BLID_INDEX()` | int | - |
| `GET_NUMBER_OF_ACTIVE_BLIPS` | `int GET_NUMBER_OF_ACTIVE_BLIPS()` | int | - |
| `GET_PAUSE_MENU_POSITION` | `Vector3 GET_PAUSE_MENU_POSITION()` | Vector3 | - |
| `GET_PAUSE_MENU_STATE` | `int GET_PAUSE_MENU_STATE()` | int | Returns:<br><br>0<br>5<br>10<br>15<br>20<br>25<br>30<br>35<br> |
| `GET_PM_PLAYER_CREW_COLOR` | `bool GET_PM_PLAYER_CREW_COLOR(ref int r, ref int g, ref int b)` | bool | - |
| `GET_RENDERED_CHARACTER_HEIGHT` | `float GET_RENDERED_CHARACTER_HEIGHT(float size, int font)` | float | This gets the height of the FONT and not the total text. You need to get the number of lines your te... |
| `GET_SCREEN_CODE_WANTS_SCRIPT_TO_CONTROL` | `int GET_SCREEN_CODE_WANTS_SCRIPT_TO_CONTROL()` | int | - |
| `GET_STANDARD_BLIP_ENUM_ID` | `int GET_STANDARD_BLIP_ENUM_ID()` | int | - |
| `GET_STREET_NAME_FROM_HASH_KEY` | `string GET_STREET_NAME_FROM_HASH_KEY(uint hash)` | string | This functions converts the hash of a street name into a readable string.<br><br>For how to get the hashes... |
| `GET_WARNING_SCREEN_MESSAGE_HASH` | `uint GET_WARNING_SCREEN_MESSAGE_HASH()` | uint | Has to do with the confirmation overlay (E.g. confirm exit) |
| `GET_WAYPOINT_BLIP_ENUM_ID` | `int GET_WAYPOINT_BLIP_ENUM_ID()` | int | - |
| `GET_WAYPOINT_CLEAR_ON_ARRIVAL_MODE` | `int GET_WAYPOINT_CLEAR_ON_ARRIVAL_MODE()` | int | - |
| `GIVE_PED_TO_PAUSE_MENU` | `void GIVE_PED_TO_PAUSE_MENU(int ped, int p1)` | void | p1 is either 1 or 2 in the PC scripts. |
| `HAS_ADDITIONAL_TEXT_LOADED` | `bool HAS_ADDITIONAL_TEXT_LOADED(int slot)` | bool | - |
| `HAS_DIRECTOR_MODE_BEEN_LAUNCHED_BY_CODE` | `bool HAS_DIRECTOR_MODE_BEEN_LAUNCHED_BY_CODE()` | bool | - |
| `HAS_MENU_LAYOUT_CHANGED_EVENT_OCCURRED` | `bool HAS_MENU_LAYOUT_CHANGED_EVENT_OCCURRED()` | bool | - |
| `HAS_MENU_TRIGGER_EVENT_OCCURRED` | `bool HAS_MENU_TRIGGER_EVENT_OCCURRED()` | bool | - |
| `HAS_SCRIPT_HIDDEN_HELP_THIS_FRAME` | `bool HAS_SCRIPT_HIDDEN_HELP_THIS_FRAME()` | bool | - |
| `HAS_THIS_ADDITIONAL_TEXT_LOADED` | `bool HAS_THIS_ADDITIONAL_TEXT_LOADED(string gxt, int slot)` | bool | Checks if the specified gxt has loaded into the passed slot. |
| `HIDE_HELP_TEXT_THIS_FRAME` | `void HIDE_HELP_TEXT_THIS_FRAME()` | void | - |
| `HIDE_HUDMARKERS_THIS_FRAME` | `void HIDE_HUDMARKERS_THIS_FRAME()` | void | - |
| `HIDE_HUD_AND_RADAR_THIS_FRAME` | `void HIDE_HUD_AND_RADAR_THIS_FRAME()` | void | Hides HUD and radar this frame and prohibits switching to other weapons (or accessing the weapon whe... |
| `HIDE_HUD_COMPONENT_THIS_FRAME` | `void HIDE_HUD_COMPONENT_THIS_FRAME(int id)` | void | This function hides various HUD (Heads-up Display) components.<br>Listed below are the integers and the... |
| `HIDE_LOADING_ON_FADE_THIS_FRAME` | `void HIDE_LOADING_ON_FADE_THIS_FRAME()` | void | - |
| `HIDE_MINIMAP_EXTERIOR_MAP_THIS_FRAME` | `void HIDE_MINIMAP_EXTERIOR_MAP_THIS_FRAME()` | void | - |
| `HIDE_MINIMAP_INTERIOR_MAP_THIS_FRAME` | `void HIDE_MINIMAP_INTERIOR_MAP_THIS_FRAME()` | void | - |
| `HIDE_NUMBER_ON_BLIP` | `void HIDE_NUMBER_ON_BLIP(int blip)` | void | - |
| `HIDE_SCRIPTED_HUD_COMPONENT_THIS_FRAME` | `void HIDE_SCRIPTED_HUD_COMPONENT_THIS_FRAME(int id)` | void | - |
| `HIDE_STREET_AND_CAR_NAMES_THIS_FRAME` | `void HIDE_STREET_AND_CAR_NAMES_THIS_FRAME()` | void | Hides area and vehicle name HUD components for one frame. |
| `HUD_FORCE_SPECIAL_VEHICLE_WEAPON_WHEEL` | `void HUD_FORCE_SPECIAL_VEHICLE_WEAPON_WHEEL()` | void | Displays "blazer_wheels_up" and "blazer_wheels_down" "weapon" icons when switching between jetski an... |
| `HUD_FORCE_WEAPON_WHEEL` | `void HUD_FORCE_WEAPON_WHEEL(bool show)` | void | Forces the weapon wheel to show/hide. |
| `HUD_GET_WEAPON_WHEEL_CURRENTLY_HIGHLIGHTED` | `uint HUD_GET_WEAPON_WHEEL_CURRENTLY_HIGHLIGHTED()` | uint | Returns the weapon hash to the selected/highlighted weapon in the wheel |
| `HUD_GET_WEAPON_WHEEL_TOP_SLOT` | `uint HUD_GET_WEAPON_WHEEL_TOP_SLOT(int weaponTypeIndex)` | uint | Returns the weapon hash active in a specific weapon wheel slotList |
| `HUD_SET_WEAPON_WHEEL_TOP_SLOT` | `void HUD_SET_WEAPON_WHEEL_TOP_SLOT(uint weaponHash)` | void | Set the active slotIndex in the wheel weapon to the slot associated with the provided Weapon hash |
| `HUD_SHOWING_CHARACTER_SWITCH_SELECTION` | `void HUD_SHOWING_CHARACTER_SWITCH_SELECTION(bool toggle)` | void | Sets a global that disables many weapon input tasks (shooting, aiming, etc.). Does not work with veh... |
| `HUD_SUPPRESS_WEAPON_WHEEL_RESULTS_THIS_FRAME` | `void HUD_SUPPRESS_WEAPON_WHEEL_RESULTS_THIS_FRAME()` | void | Calling this each frame, stops the player from receiving a weapon via the weapon wheel. |
| `IS_BLIP_FLASHING` | `bool IS_BLIP_FLASHING(int blip)` | bool | - |
| `IS_BLIP_ON_MINIMAP` | `bool IS_BLIP_ON_MINIMAP(int blip)` | bool | - |
| `IS_BLIP_SHORT_RANGE` | `bool IS_BLIP_SHORT_RANGE(int blip)` | bool | - |
| `IS_FLOATING_HELP_TEXT_ON_SCREEN` | `bool IS_FLOATING_HELP_TEXT_ON_SCREEN(int hudIndex)` | bool | - |
| `IS_FRONTEND_READY_FOR_CONTROL` | `bool IS_FRONTEND_READY_FOR_CONTROL()` | bool | - |
| `IS_HELP_MESSAGE_BEING_DISPLAYED` | `bool IS_HELP_MESSAGE_BEING_DISPLAYED()` | bool | - |
| `IS_HELP_MESSAGE_FADING_OUT` | `bool IS_HELP_MESSAGE_FADING_OUT()` | bool | - |
| `IS_HELP_MESSAGE_ON_SCREEN` | `bool IS_HELP_MESSAGE_ON_SCREEN()` | bool | - |
| `IS_HOVERING_OVER_MISSION_CREATOR_BLIP` | `bool IS_HOVERING_OVER_MISSION_CREATOR_BLIP()` | bool | - |
| `IS_HUD_COMPONENT_ACTIVE` | `bool IS_HUD_COMPONENT_ACTIVE(int id)` | bool | Full list of components below<br><br>HUD = 0;<br>HUD_WANTED_STARS = 1;<br>HUD_WEAPON_ICON = 2;<br>HUD_CASH = 3;<br>HUD... |
| `IS_HUD_COMPONENT_HIDDEN_THIS_FRAME` | `bool IS_HUD_COMPONENT_HIDDEN_THIS_FRAME(int id)` | bool | - |
| `IS_HUD_HIDDEN` | `bool IS_HUD_HIDDEN()` | bool | - |
| `IS_HUD_PREFERENCE_SWITCHED_ON` | `bool IS_HUD_PREFERENCE_SWITCHED_ON()` | bool | - |
| `IS_IME_IN_PROGRESS` | `bool IS_IME_IN_PROGRESS()` | bool | - |
| `IS_MESSAGE_BEING_DISPLAYED` | `bool IS_MESSAGE_BEING_DISPLAYED()` | bool | - |
| `IS_MINIMAP_RENDERING` | `bool IS_MINIMAP_RENDERING()` | bool | - |
| `IS_MISSION_CREATOR_BLIP` | `bool IS_MISSION_CREATOR_BLIP(int blip)` | bool | - |
| `IS_MOUSE_ROLLED_OVER_INSTRUCTIONAL_BUTTONS` | `bool IS_MOUSE_ROLLED_OVER_INSTRUCTIONAL_BUTTONS()` | bool | Returns TRUE if mouse is hovering above instructional buttons. Works with all buttons gfx, such as p... |
| `IS_MP_GAMER_TAG_ACTIVE` | `bool IS_MP_GAMER_TAG_ACTIVE(int gamerTagId)` | bool | - |
| `IS_MP_GAMER_TAG_FREE` | `bool IS_MP_GAMER_TAG_FREE(int gamerTagId)` | bool | - |
| `IS_MP_GAMER_TAG_MOVIE_ACTIVE` | `bool IS_MP_GAMER_TAG_MOVIE_ACTIVE()` | bool | - |
| `IS_MP_TEXT_CHAT_TYPING` | `bool IS_MP_TEXT_CHAT_TYPING()` | bool | Returns whether or not the text chat (MULTIPLAYER_CHAT Scaleform component) is active. |
| `IS_NAMED_RENDERTARGET_LINKED` | `bool IS_NAMED_RENDERTARGET_LINKED(uint modelHash)` | bool | - |
| `IS_NAMED_RENDERTARGET_REGISTERED` | `bool IS_NAMED_RENDERTARGET_REGISTERED(string name)` | bool | - |
| `IS_NAVIGATING_MENU_CONTENT` | `bool IS_NAVIGATING_MENU_CONTENT()` | bool | - |
| `IS_ONLINE_POLICIES_MENU_ACTIVE` | `bool IS_ONLINE_POLICIES_MENU_ACTIVE()` | bool | Returns the same as IS_SOCIAL_CLUB_ACTIVE |
| `IS_PAUSEMAP_IN_INTERIOR_MODE` | `bool IS_PAUSEMAP_IN_INTERIOR_MODE()` | bool | - |
| `IS_PAUSE_MENU_ACTIVE` | `bool IS_PAUSE_MENU_ACTIVE()` | bool | - |
| `IS_PAUSE_MENU_RESTARTING` | `bool IS_PAUSE_MENU_RESTARTING()` | bool | - |
| `IS_RADAR_HIDDEN` | `bool IS_RADAR_HIDDEN()` | bool | - |
| `IS_RADAR_PREFERENCE_SWITCHED_ON` | `bool IS_RADAR_PREFERENCE_SWITCHED_ON()` | bool | - |
| `IS_REPORTUGC_MENU_OPEN` | `bool IS_REPORTUGC_MENU_OPEN()` | bool | - |
| `IS_SCRIPTED_HUD_COMPONENT_ACTIVE` | `bool IS_SCRIPTED_HUD_COMPONENT_ACTIVE(int id)` | bool | - |
| `IS_SCRIPTED_HUD_COMPONENT_HIDDEN_THIS_FRAME` | `bool IS_SCRIPTED_HUD_COMPONENT_HIDDEN_THIS_FRAME(int id)` | bool | - |
| `IS_SOCIAL_CLUB_ACTIVE` | `bool IS_SOCIAL_CLUB_ACTIVE()` | bool | - |
| `IS_STORE_PENDING_NETWORK_SHUTDOWN_TO_OPEN` | `bool IS_STORE_PENDING_NETWORK_SHUTDOWN_TO_OPEN()` | bool | - |
| `IS_STREAMING_ADDITIONAL_TEXT` | `bool IS_STREAMING_ADDITIONAL_TEXT(int p0)` | bool | - |
| `IS_SUBTITLE_PREFERENCE_SWITCHED_ON` | `bool IS_SUBTITLE_PREFERENCE_SWITCHED_ON()` | bool | - |
| `IS_UPDATING_MP_GAMER_TAG_NAME_AND_CREW_DETAILS` | `bool IS_UPDATING_MP_GAMER_TAG_NAME_AND_CREW_DETAILS(int gamerTagId)` | bool | - |
| `IS_WARNING_MESSAGE_ACTIVE` | `bool IS_WARNING_MESSAGE_ACTIVE()` | bool | - |
| `IS_WARNING_MESSAGE_READY_FOR_CONTROL` | `bool IS_WARNING_MESSAGE_READY_FOR_CONTROL()` | bool | - |
| `IS_WAYPOINT_ACTIVE` | `bool IS_WAYPOINT_ACTIVE()` | bool | - |
| `LINK_NAMED_RENDERTARGET` | `void LINK_NAMED_RENDERTARGET(uint modelHash)` | void | - |
| `LOCK_MINIMAP_ANGLE` | `void LOCK_MINIMAP_ANGLE(int angle)` | void | Locks the minimap to the specified angle in integer degrees.<br><br>angle: The angle in whole degrees. If ... |
| `LOCK_MINIMAP_POSITION` | `void LOCK_MINIMAP_POSITION(float x, float y)` | void | Locks the minimap to the specified world position. |
| `MP_TEXT_CHAT_DISABLE` | `void MP_TEXT_CHAT_DISABLE(bool toggle)` | void | Hides the chat history, closes the input box and makes it unable to be opened unless called again wi... |
| `MP_TEXT_CHAT_IS_TEAM_JOB` | `void MP_TEXT_CHAT_IS_TEAM_JOB(object p0)` | void | - |
| `OPEN_ONLINE_POLICIES_MENU` | `void OPEN_ONLINE_POLICIES_MENU()` | void | - |
| `OPEN_REPORTUGC_MENU` | `void OPEN_REPORTUGC_MENU()` | void | Shows a menu for reporting UGC content. |
| `OPEN_SOCIAL_CLUB_MENU` | `void OPEN_SOCIAL_CLUB_MENU(uint menu)` | void | Uses the `SOCIAL_CLUB2` scaleform.<br>menu: GALLERY, MISSIONS, CREWS, MIGRATE, PLAYLISTS, JOBS |
| `OVERRIDE_MP_TEXT_CHAT_COLOR` | `void OVERRIDE_MP_TEXT_CHAT_COLOR(int p0, int hudColor)` | void | - |
| `OVERRIDE_MP_TEXT_CHAT_TEAM_STRING` | `void OVERRIDE_MP_TEXT_CHAT_TEAM_STRING(uint gxtEntryHash)` | void | - |
| `PAUSE_MENUCEPTION_GO_DEEPER` | `void PAUSE_MENUCEPTION_GO_DEEPER(int page)` | void | - |
| `PAUSE_MENUCEPTION_THE_KICK` | `void PAUSE_MENUCEPTION_THE_KICK()` | void | - |
| `PAUSE_MENU_ACTIVATE_CONTEXT` | `void PAUSE_MENU_ACTIVATE_CONTEXT(uint contextHash)` | void | Activates the specified frontend menu context.<br>pausemenu.xml defines some specific menu options usin... |
| `PAUSE_MENU_DEACTIVATE_CONTEXT` | `void PAUSE_MENU_DEACTIVATE_CONTEXT(uint contextHash)` | void | - |
| `PAUSE_MENU_GET_HAIR_COLOUR_INDEX` | `int PAUSE_MENU_GET_HAIR_COLOUR_INDEX()` | int | - |
| `PAUSE_MENU_GET_MOUSE_CLICK_EVENT` | `bool PAUSE_MENU_GET_MOUSE_CLICK_EVENT(ref object p0, ref object p1, ref object p2)` | bool | - |
| `PAUSE_MENU_GET_MOUSE_HOVER_INDEX` | `int PAUSE_MENU_GET_MOUSE_HOVER_INDEX()` | int | - |
| `PAUSE_MENU_GET_MOUSE_HOVER_UNIQUE_ID` | `int PAUSE_MENU_GET_MOUSE_HOVER_UNIQUE_ID()` | int | - |
| `PAUSE_MENU_IS_CONTEXT_ACTIVE` | `bool PAUSE_MENU_IS_CONTEXT_ACTIVE(uint contextHash)` | bool | - |
| `PAUSE_MENU_IS_CONTEXT_MENU_ACTIVE` | `bool PAUSE_MENU_IS_CONTEXT_MENU_ACTIVE()` | bool | - |
| `PAUSE_MENU_REDRAW_INSTRUCTIONAL_BUTTONS` | `void PAUSE_MENU_REDRAW_INSTRUCTIONAL_BUTTONS(int p0)` | void | - |
| `PAUSE_MENU_SET_BUSY_SPINNER` | `void PAUSE_MENU_SET_BUSY_SPINNER(bool p0, int position, int spinnerIndex)` | void | - |
| `PAUSE_MENU_SET_WARN_ON_TAB_CHANGE` | `void PAUSE_MENU_SET_WARN_ON_TAB_CHANGE(bool p0)` | void | - |
| `PAUSE_TOGGLE_FULLSCREEN_MAP` | `void PAUSE_TOGGLE_FULLSCREEN_MAP(object p0)` | void | - |
| `PRELOAD_BUSYSPINNER` | `void PRELOAD_BUSYSPINNER()` | void | - |
| `PULSE_BLIP` | `void PULSE_BLIP(int blip)` | void | - |
| `REFRESH_WAYPOINT` | `void REFRESH_WAYPOINT()` | void | - |
| `REGISTER_NAMED_RENDERTARGET` | `bool REGISTER_NAMED_RENDERTARGET(string name, bool p1)` | bool | - |
| `RELEASE_CONTROL_OF_FRONTEND` | `void RELEASE_CONTROL_OF_FRONTEND()` | void | Enables frontend (works in custom frontends, not sure about regular pause menu) navigation keys on k... |
| `RELEASE_NAMED_RENDERTARGET` | `bool RELEASE_NAMED_RENDERTARGET(string name)` | bool | - |
| `RELOAD_MAP_MENU` | `void RELOAD_MAP_MENU()` | void | - |
| `REMOVE_BLIP` | `void REMOVE_BLIP(Blip* blip)` | void | In the C++ SDK, this seems not to work-- the blip isn't removed immediately. I use it for saving car... |
| `REMOVE_COP_BLIP_FROM_PED` | `void REMOVE_COP_BLIP_FROM_PED(int ped)` | void | Interesting fact: A hash collision for this is RESET_JETPACK_MODEL_SETTINGS |
| `REMOVE_FAKE_CONE_DATA` | `void REMOVE_FAKE_CONE_DATA(int blip)` | void | - |
| `REMOVE_MP_GAMER_TAG` | `void REMOVE_MP_GAMER_TAG(int gamerTagId)` | void | - |
| `REMOVE_MULTIPLAYER_BANK_CASH` | `void REMOVE_MULTIPLAYER_BANK_CASH()` | void | - |
| `REMOVE_MULTIPLAYER_HUD_CASH` | `void REMOVE_MULTIPLAYER_HUD_CASH()` | void | Removes multiplayer cash hud each frame |
| `REMOVE_MULTIPLAYER_WALLET_CASH` | `void REMOVE_MULTIPLAYER_WALLET_CASH()` | void | - |
| `REMOVE_WARNING_MESSAGE_OPTION_ITEMS` | `void REMOVE_WARNING_MESSAGE_OPTION_ITEMS()` | void | - |
| `REPLACE_HUD_COLOUR` | `void REPLACE_HUD_COLOUR(int hudColorIndex, int hudColorIndex2)` | void | makes hudColorIndex2 color into hudColorIndex color |
| `REPLACE_HUD_COLOUR_WITH_RGBA` | `void REPLACE_HUD_COLOUR_WITH_RGBA(int hudColorIndex, int r, int g, int b, int a)` | void | - |
| `REQUEST_ADDITIONAL_TEXT` | `void REQUEST_ADDITIONAL_TEXT(string gxt, int slot)` | void | Request a gxt into the passed slot. |
| `REQUEST_ADDITIONAL_TEXT_FOR_DLC` | `void REQUEST_ADDITIONAL_TEXT_FOR_DLC(string gxt, int slot)` | void | - |
| `RESET_GLOBAL_ACTIONSCRIPT_FLAG` | `void RESET_GLOBAL_ACTIONSCRIPT_FLAG(int flagIndex)` | void | - |
| `RESET_HUD_COMPONENT_VALUES` | `void RESET_HUD_COMPONENT_VALUES(int id)` | void | - |
| `RESET_RETICULE_VALUES` | `void RESET_RETICULE_VALUES()` | void | - |
| `RESTART_FRONTEND_MENU` | `void RESTART_FRONTEND_MENU(uint menuHash, int p1)` | void | Before using this native click the native above and look at the decription.<br><br>Example:<br>int GetHash = ... |
| `SCRIPT_IS_MOVING_MOBILE_PHONE_OFFSCREEN` | `void SCRIPT_IS_MOVING_MOBILE_PHONE_OFFSCREEN(bool toggle)` | void | If bool Toggle = true so the mobile is hide to screen.<br>If bool Toggle = false so the mobile is show ... |
| `SETUP_FAKE_CONE_DATA` | `void SETUP_FAKE_CONE_DATA(int blip, float p1, float p2, float p3, float p4, float p5, float p6, object p7, int p8)` | void | - |
| `SET_ABILITY_BAR_VALUE` | `void SET_ABILITY_BAR_VALUE(float p0, float p1)` | void | - |
| `SET_ABILITY_BAR_VISIBILITY` | `void SET_ABILITY_BAR_VISIBILITY(bool visible)` | void | - |
| `SET_ALLOW_ABILITY_BAR` | `void SET_ALLOW_ABILITY_BAR(bool toggle)` | void | - |
| `SET_ALLOW_COMMA_ON_TEXT_INPUT` | `void SET_ALLOW_COMMA_ON_TEXT_INPUT(object p0)` | void | - |
| `SET_ALL_MP_GAMER_TAGS_VISIBILITY` | `void SET_ALL_MP_GAMER_TAGS_VISIBILITY(int gamerTagId, bool toggle)` | void | - |
| `SET_BIGMAP_ACTIVE` | `void SET_BIGMAP_ACTIVE(bool toggleBigMap, bool showFullMap)` | void | Toggles the big minimap state like in GTA:Online. |
| `SET_BLIP_ALPHA` | `void SET_BLIP_ALPHA(int blip, int alpha)` | void | Sets alpha-channel for blip color.<br><br>Example:<br><br>Blip blip = HUD::ADD_BLIP_FOR_ENTITY(entity);<br>HUD::SET... |
| `SET_BLIP_AS_FRIENDLY` | `void SET_BLIP_AS_FRIENDLY(int blip, bool toggle)` | void | false for enemy<br>true for friendly |
| `SET_BLIP_AS_MINIMAL_ON_EDGE` | `void SET_BLIP_AS_MINIMAL_ON_EDGE(int blip, bool toggle)` | void | Makes a blip go small when off the minimap. |
| `SET_BLIP_AS_MISSION_CREATOR_BLIP` | `void SET_BLIP_AS_MISSION_CREATOR_BLIP(int blip, bool toggle)` | void | - |
| `SET_BLIP_AS_SHORT_RANGE` | `void SET_BLIP_AS_SHORT_RANGE(int blip, bool toggle)` | void | Sets whether or not the specified blip should only be displayed when nearby, or on the minimap. |
| `SET_BLIP_BRIGHT` | `void SET_BLIP_BRIGHT(int blip, bool toggle)` | void | - |
| `SET_BLIP_CATEGORY` | `void SET_BLIP_CATEGORY(int blip, int index)` | void | Example: https://i.imgur.com/skY6vAJ.png<br><br>Index:<br>1 = No distance shown in legend<br>2 = Distance shown ... |
| `SET_BLIP_COLOUR` | `void SET_BLIP_COLOUR(int blip, int color)` | void | https://gtaforums.com/topic/864881-all-blip-color-ids-pictured/ |
| `SET_BLIP_COORDS` | `void SET_BLIP_COORDS(int blip, float posX, float posY, float posZ)` | void | - |
| `SET_BLIP_DISPLAY` | `void SET_BLIP_DISPLAY(int blip, int displayId)` | void | Display Id behaviours:<br>0 = Doesn't show up, ever, anywhere.<br>1 = Doesn't show up, ever, anywhere.<br>2 =... |
| `SET_BLIP_EXTENDED_HEIGHT_THRESHOLD` | `void SET_BLIP_EXTENDED_HEIGHT_THRESHOLD(int blip, bool toggle)` | void | Must be toggled before being queued for animation |
| `SET_BLIP_FADE` | `void SET_BLIP_FADE(int blip, int opacity, int duration)` | void | - |
| `SET_BLIP_FLASHES` | `void SET_BLIP_FLASHES(int blip, bool toggle)` | void | - |
| `SET_BLIP_FLASHES_ALTERNATE` | `void SET_BLIP_FLASHES_ALTERNATE(int blip, bool toggle)` | void | - |
| `SET_BLIP_FLASH_INTERVAL` | `void SET_BLIP_FLASH_INTERVAL(int blip, object p1)` | void | - |
| `SET_BLIP_FLASH_TIMER` | `void SET_BLIP_FLASH_TIMER(int blip, int duration)` | void | Adds up after viewing multiple R* scripts. I believe that the duration is in miliseconds. |
| `SET_BLIP_HIDDEN_ON_LEGEND` | `void SET_BLIP_HIDDEN_ON_LEGEND(int blip, bool toggle)` | void | - |
| `SET_BLIP_HIGH_DETAIL` | `void SET_BLIP_HIGH_DETAIL(int blip, bool toggle)` | void | - |
| `SET_BLIP_MARKER_LONG_DISTANCE` | `void SET_BLIP_MARKER_LONG_DISTANCE(object p0, object p1)` | void | - |
| `SET_BLIP_NAME_FROM_TEXT_FILE` | `void SET_BLIP_NAME_FROM_TEXT_FILE(int blip, string gxtEntry)` | void | Doesn't work if the label text of gxtEntry is >= 80. |
| `SET_BLIP_NAME_TO_PLAYER_NAME` | `void SET_BLIP_NAME_TO_PLAYER_NAME(int blip, int player)` | void | - |
| `SET_BLIP_PRIORITY` | `void SET_BLIP_PRIORITY(int blip, int priority)` | void | See this topic for more details : gtaforums.com/topic/717612-v-scriptnative-documentation-and-resear... |
| `SET_BLIP_ROTATION` | `void SET_BLIP_ROTATION(int blip, int rotation)` | void | After some testing, looks like you need to use CEIL() on the rotation (vehicle/ped heading) before u... |
| `SET_BLIP_ROTATION_WITH_FLOAT` | `void SET_BLIP_ROTATION_WITH_FLOAT(int blip, float heading)` | void | Does not require whole number/integer rotations. |
| `SET_BLIP_ROUTE` | `void SET_BLIP_ROUTE(int blip, bool enabled)` | void | Enable / disable showing route for the Blip-object. |
| `SET_BLIP_ROUTE_COLOUR` | `void SET_BLIP_ROUTE_COLOUR(int blip, int colour)` | void | - |
| `SET_BLIP_SCALE` | `void SET_BLIP_SCALE(int blip, float scale)` | void | - |
| `SET_BLIP_SCALE_2D` | `void SET_BLIP_SCALE_2D(int blip, float xScale, float yScale)` | void | See https://imgur.com/a/lLkEsMN |
| `SET_BLIP_SECONDARY_COLOUR` | `void SET_BLIP_SECONDARY_COLOUR(int blip, int r, int g, int b)` | void | Can be used to give blips any RGB colour with SET_BLIP_COLOUR(blip, 84). |
| `SET_BLIP_SHORT_HEIGHT_THRESHOLD` | `void SET_BLIP_SHORT_HEIGHT_THRESHOLD(object p0, object p1)` | void | - |
| `SET_BLIP_SHOW_CONE` | `void SET_BLIP_SHOW_CONE(int blip, bool toggle, int hudColorIndex)` | void | As of b2189, the third parameter sets the color of the cone (before b2189 it was ignored). Note that... |
| `SET_BLIP_SPRITE` | `void SET_BLIP_SPRITE(int blip, int spriteId)` | void | Sets the displayed sprite for a specific blip..<br><br>You may have your own list, but since dev-c didn't ... |
| `SET_BLIP_USE_HEIGHT_INDICATOR_ON_EDGE` | `void SET_BLIP_USE_HEIGHT_INDICATOR_ON_EDGE(int blip, object p1)` | void | - |
| `SET_BLOCK_WANTED_FLASH` | `void SET_BLOCK_WANTED_FLASH(bool disabled)` | void | - |
| `SET_COLOUR_OF_NEXT_TEXT_COMPONENT` | `void SET_COLOUR_OF_NEXT_TEXT_COMPONENT(int hudColor)` | void | - |
| `SET_COP_BLIP_SPRITE` | `void SET_COP_BLIP_SPRITE(int p0, float p1)` | void | - |
| `SET_COP_BLIP_SPRITE_AS_STANDARD` | `void SET_COP_BLIP_SPRITE_AS_STANDARD()` | void | - |
| `SET_CUSTOM_MP_HUD_COLOR` | `void SET_CUSTOM_MP_HUD_COLOR(int hudColorId)` | void | - |
| `SET_DESCRIPTION_FOR_UGC_MISSION_EIGHT_STRINGS` | `void SET_DESCRIPTION_FOR_UGC_MISSION_EIGHT_STRINGS(bool p0, string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8)` | void | - |
| `SET_DIRECTOR_MODE_AVAILABLE` | `void SET_DIRECTOR_MODE_AVAILABLE(bool toggle)` | void | - |
| `SET_DIRECTOR_MODE_LAUNCHED_BY_SCRIPT` | `void SET_DIRECTOR_MODE_LAUNCHED_BY_SCRIPT()` | void | - |
| `SET_FAKE_GPS_PLAYER_POSITION_THIS_FRAME` | `void SET_FAKE_GPS_PLAYER_POSITION_THIS_FRAME(float x, float y, float z)` | void | - |
| `SET_FAKE_MINIMAP_MAX_ALTIMETER_HEIGHT` | `void SET_FAKE_MINIMAP_MAX_ALTIMETER_HEIGHT(float altitude, bool p1, object p2)` | void | Argument must be 0.0f or above 38.0f, or it will be ignored. |
| `SET_FAKE_PAUSEMAP_PLAYER_POSITION_THIS_FRAME` | `void SET_FAKE_PAUSEMAP_PLAYER_POSITION_THIS_FRAME(float x, float y)` | void | Sets the position of the arrow icon representing the player on both the minimap and world map.<br><br>Too ... |
| `SET_FAKE_SPECTATOR_MODE` | `void SET_FAKE_SPECTATOR_MODE(bool toggle)` | void | Setter for GET_FAKE_SPECTATOR_MODE |
| `SET_FLOATING_HELP_TEXT_SCREEN_POSITION` | `void SET_FLOATING_HELP_TEXT_SCREEN_POSITION(int hudIndex, float x, float y)` | void | - |
| `SET_FLOATING_HELP_TEXT_STYLE` | `void SET_FLOATING_HELP_TEXT_STYLE(int hudIndex, int p1, int p2, int p3, int p4, int p5)` | void | - |
| `SET_FLOATING_HELP_TEXT_TO_ENTITY` | `void SET_FLOATING_HELP_TEXT_TO_ENTITY(int hudIndex, int entity, float offsetX, float offsetY)` | void | - |
| `SET_FLOATING_HELP_TEXT_WORLD_POSITION` | `void SET_FLOATING_HELP_TEXT_WORLD_POSITION(int hudIndex, float x, float y, float z)` | void | - |
| `SET_FORCE_SHOW_GPS` | `void SET_FORCE_SHOW_GPS(bool toggle)` | void | - |
| `SET_FRONTEND_ACTIVE` | `void SET_FRONTEND_ACTIVE(bool active)` | void | - |
| `SET_GPS_CUSTOM_ROUTE_RENDER` | `void SET_GPS_CUSTOM_ROUTE_RENDER(bool toggle, int radarThickness, int mapThickness)` | void | radarThickness: The width of the GPS route on the radar<br>mapThickness: The width of the GPS route on ... |
| `SET_GPS_FLAGS` | `void SET_GPS_FLAGS(int p0, float p1)` | void | Only the script that originally called SET_GPS_FLAGS can set them again. Another script cannot set t... |
| `SET_GPS_FLASHES` | `void SET_GPS_FLASHES(bool toggle)` | void | - |
| `SET_GPS_MULTI_ROUTE_RENDER` | `void SET_GPS_MULTI_ROUTE_RENDER(bool toggle)` | void | - |
| `SET_HEALTH_HUD_DISPLAY_VALUES` | `void SET_HEALTH_HUD_DISPLAY_VALUES(int health, int capacity, bool wasAdded)` | void | - |
| `SET_HELP_MESSAGE_STYLE` | `void SET_HELP_MESSAGE_STYLE(int style, int hudColor, int alpha, int p3, int p4)` | void | - |
| `SET_HUD_COMPONENT_POSITION` | `void SET_HUD_COMPONENT_POSITION(int id, float x, float y)` | void | - |
| `SET_INSIDE_VERY_LARGE_INTERIOR` | `void SET_INSIDE_VERY_LARGE_INTERIOR(bool toggle)` | void | - |
| `SET_INSIDE_VERY_SMALL_INTERIOR` | `void SET_INSIDE_VERY_SMALL_INTERIOR(bool toggle)` | void | - |
| `SET_MAX_ARMOUR_HUD_DISPLAY` | `void SET_MAX_ARMOUR_HUD_DISPLAY(int maximumValue)` | void | - |
| `SET_MAX_HEALTH_HUD_DISPLAY` | `void SET_MAX_HEALTH_HUD_DISPLAY(int maximumValue)` | void | - |
| `SET_MINIMAP_BACKGROUND_HIDDEN` | `void SET_MINIMAP_BACKGROUND_HIDDEN(bool toggle)` | void | This native does absolutely nothing on PC master builds, just a nullsub. |
| `SET_MINIMAP_BLOCK_WAYPOINT` | `void SET_MINIMAP_BLOCK_WAYPOINT(bool toggle)` | void | - |
| `SET_MINIMAP_COMPONENT` | `bool SET_MINIMAP_COMPONENT(int componentId, bool toggle, int overrideColor)` | bool | This native is used to colorize certain map components like the army base at the top of the map.<br>p2 ... |
| `SET_MINIMAP_FOW_DO_NOT_UPDATE` | `void SET_MINIMAP_FOW_DO_NOT_UPDATE(bool p0)` | void | - |
| `SET_MINIMAP_FOW_REVEAL_COORDINATE` | `void SET_MINIMAP_FOW_REVEAL_COORDINATE(float x, float y, float z)` | void | Up to eight coordinates may be revealed per frame |
| `SET_MINIMAP_GOLF_COURSE` | `void SET_MINIMAP_GOLF_COURSE(int hole)` | void | Not much is known so far on what it does _exactly_.<br>All I know for sure is that it draws the specifi... |
| `SET_MINIMAP_GOLF_COURSE_OFF` | `void SET_MINIMAP_GOLF_COURSE_OFF()` | void | - |
| `SET_MINIMAP_HIDE_FOW` | `void SET_MINIMAP_HIDE_FOW(bool toggle)` | void | If true, the entire map will be revealed.<br><br>FOW = Fog of War |
| `SET_MINIMAP_IN_PROLOGUE` | `void SET_MINIMAP_IN_PROLOGUE(bool toggle)` | void | Toggles the North Yankton map |
| `SET_MINIMAP_IN_SPECTATOR_MODE` | `void SET_MINIMAP_IN_SPECTATOR_MODE(bool toggle, int ped)` | void | - |
| `SET_MINIMAP_SONAR_SWEEP` | `void SET_MINIMAP_SONAR_SWEEP(bool toggle)` | void | - |
| `SET_MISSION_NAME` | `void SET_MISSION_NAME(bool p0, string name)` | void | - |
| `SET_MISSION_NAME_FOR_UGC_MISSION` | `void SET_MISSION_NAME_FOR_UGC_MISSION(bool p0, string name)` | void | - |
| `SET_MOBILE_PHONE_DOF_STATE` | `void SET_MOBILE_PHONE_DOF_STATE(bool toggle)` | void | - |
| `SET_MOBILE_PHONE_POSITION` | `void SET_MOBILE_PHONE_POSITION(float posX, float posY, float posZ)` | void | - |
| `SET_MOBILE_PHONE_ROTATION` | `void SET_MOBILE_PHONE_ROTATION(float rotX, float rotY, float rotZ, object p3)` | void | Last parameter is unknown and always zero. |
| `SET_MOBILE_PHONE_SCALE` | `void SET_MOBILE_PHONE_SCALE(float scale)` | void | The minimum/default is 500.0f. If you plan to make it bigger set it's position as well. Also this se... |
| `SET_MOUSE_CURSOR_STYLE` | `void SET_MOUSE_CURSOR_STYLE(int spriteId)` | void | Changes the mouse cursor's sprite. <br>1 = Normal<br>6 = Left Arrow<br>7 = Right Arrow |
| `SET_MOUSE_CURSOR_THIS_FRAME` | `void SET_MOUSE_CURSOR_THIS_FRAME()` | void | Shows the cursor on screen for one frame. |
| `SET_MOUSE_CURSOR_VISIBLE` | `void SET_MOUSE_CURSOR_VISIBLE(bool toggle)` | void | Shows/hides the frontend cursor on the pause menu or similar menus.<br>Clicking off and then on the gam... |
| `SET_MP_GAMER_TAGS_POINT_HEALTH` | `void SET_MP_GAMER_TAGS_POINT_HEALTH(int gamerTagId, int value, int maximumValue)` | void | - |
| `SET_MP_GAMER_TAGS_SHOULD_USE_POINTS_HEALTH` | `void SET_MP_GAMER_TAGS_SHOULD_USE_POINTS_HEALTH(int gamerTagId, bool toggle)` | void | - |
| `SET_MP_GAMER_TAGS_SHOULD_USE_VEHICLE_HEALTH` | `void SET_MP_GAMER_TAGS_SHOULD_USE_VEHICLE_HEALTH(int gamerTagId, bool toggle)` | void | Displays a bunch of icons above the players name, and level, and their name twice |
| `SET_MP_GAMER_TAG_ALPHA` | `void SET_MP_GAMER_TAG_ALPHA(int gamerTagId, int component, int alpha)` | void | Sets flag's sprite transparency. 0-255. |
| `SET_MP_GAMER_TAG_BIG_TEXT` | `void SET_MP_GAMER_TAG_BIG_TEXT(int gamerTagId, string string)` | void | - |
| `SET_MP_GAMER_TAG_COLOUR` | `void SET_MP_GAMER_TAG_COLOUR(int gamerTagId, int component, int hudColorIndex)` | void | Sets a gamer tag's component colour<br><br>gamerTagId is obtained using for example CREATE_FAKE_MP_GAMER_T... |
| `SET_MP_GAMER_TAG_HEALTH_BAR_COLOUR` | `void SET_MP_GAMER_TAG_HEALTH_BAR_COLOUR(int gamerTagId, int hudColorIndex)` | void | Ranges from 0 to 255. 0 is grey health bar, ~50 yellow, 200 purple.<br>Should be enabled as flag (2). H... |
| `SET_MP_GAMER_TAG_NAME` | `void SET_MP_GAMER_TAG_NAME(int gamerTagId, string string)` | void | - |
| `SET_MP_GAMER_TAG_NUM_PACKAGES` | `void SET_MP_GAMER_TAG_NUM_PACKAGES(int gamerTagId, int p1)` | void | - |
| `SET_MP_GAMER_TAG_VISIBILITY` | `void SET_MP_GAMER_TAG_VISIBILITY(int gamerTagId, int component, bool toggle, object p3)` | void | enum eMpGamerTagComponent<br>{<br>	MP_TAG_GAMER_NAME,<br>	MP_TAG_CREW_TAG,<br>	MP_TAG_HEALTH_ARMOUR,<br>	MP_TAG_BIG... |
| `SET_MP_GAMER_TAG_WANTED_LEVEL` | `void SET_MP_GAMER_TAG_WANTED_LEVEL(int gamerTagId, int wantedlvl)` | void | displays wanted star above head |
| `SET_MULTIPLAYER_BANK_CASH` | `void SET_MULTIPLAYER_BANK_CASH()` | void | - |
| `SET_MULTIPLAYER_HUD_CASH` | `void SET_MULTIPLAYER_HUD_CASH(int p0, bool p1)` | void | This native does absolutely nothing, just a nullsub |
| `SET_MULTIPLAYER_WALLET_CASH` | `void SET_MULTIPLAYER_WALLET_CASH()` | void | - |
| `SET_NEW_WAYPOINT` | `void SET_NEW_WAYPOINT(float x, float y)` | void | - |
| `SET_PAUSE_MENU_ACTIVE` | `void SET_PAUSE_MENU_ACTIVE(bool toggle)` | void | - |
| `SET_PAUSE_MENU_PED_LIGHTING` | `void SET_PAUSE_MENU_PED_LIGHTING(bool state)` | void | Toggles the light state for the pause menu ped in frontend menus.<br><br>This is used by R* in combination... |
| `SET_PAUSE_MENU_PED_SLEEP_STATE` | `void SET_PAUSE_MENU_PED_SLEEP_STATE(bool state)` | void | Toggles the pause menu ped sleep state for frontend menus.<br><br>Example: https://vespura.com/hi/i/2019-0... |
| `SET_PED_AI_BLIP_FORCED_ON` | `void SET_PED_AI_BLIP_FORCED_ON(int ped, bool toggle)` | void | - |
| `SET_PED_AI_BLIP_GANG_ID` | `void SET_PED_AI_BLIP_GANG_ID(int ped, int gangId)` | void | - |
| `SET_PED_AI_BLIP_HAS_CONE` | `void SET_PED_AI_BLIP_HAS_CONE(int ped, bool toggle)` | void | - |
| `SET_PED_AI_BLIP_NOTICE_RANGE` | `void SET_PED_AI_BLIP_NOTICE_RANGE(int ped, float range)` | void | - |
| `SET_PED_AI_BLIP_SPRITE` | `void SET_PED_AI_BLIP_SPRITE(int ped, int spriteId)` | void | - |
| `SET_PED_HAS_AI_BLIP` | `void SET_PED_HAS_AI_BLIP(int ped, bool hasCone)` | void | This native turns on the AI blip on the specified ped. It also disappears automatically when the ped... |
| `SET_PED_HAS_AI_BLIP_WITH_COLOUR` | `void SET_PED_HAS_AI_BLIP_WITH_COLOUR(int ped, bool hasCone, int color)` | void | color: see SET_BLIP_COLOUR |
| `SET_PLAYER_ICON_COLOUR` | `void SET_PLAYER_ICON_COLOUR(int color)` | void | - |
| `SET_PLAYER_IS_IN_DIRECTOR_MODE` | `void SET_PLAYER_IS_IN_DIRECTOR_MODE(bool toggle)` | void | If toggle is true, hides special ability bar / character name in the pause menu<br>If toggle is false, ... |
| `SET_PM_WARNINGSCREEN_ACTIVE` | `void SET_PM_WARNINGSCREEN_ACTIVE(bool p0)` | void | - |
| `SET_RACE_TRACK_RENDER` | `void SET_RACE_TRACK_RENDER(bool toggle)` | void | - |
| `SET_RADAR_AS_EXTERIOR_THIS_FRAME` | `void SET_RADAR_AS_EXTERIOR_THIS_FRAME()` | void | - |
| `SET_RADAR_AS_INTERIOR_THIS_FRAME` | `void SET_RADAR_AS_INTERIOR_THIS_FRAME(uint interior, float x, float y, int z, int zoom)` | void | List of interior hashes: https://pastebin.com/1FUyXNqY<br>Not for every interior zoom > 0 available. |
| `SET_RADAR_ZOOM` | `void SET_RADAR_ZOOM(int zoomLevel)` | void | zoomLevel ranges from 0 to 1400 in R* Scripts |
| `SET_RADAR_ZOOM_PRECISE` | `void SET_RADAR_ZOOM_PRECISE(float zoom)` | void | zoom ranges from 0 to 90f in R* Scripts |
| `SET_RADAR_ZOOM_TO_BLIP` | `void SET_RADAR_ZOOM_TO_BLIP(int blip, float zoom)` | void | - |
| `SET_RADAR_ZOOM_TO_DISTANCE` | `void SET_RADAR_ZOOM_TO_DISTANCE(float zoom)` | void | - |
| `SET_RADIUS_BLIP_EDGE` | `void SET_RADIUS_BLIP_EDGE(int blip, bool toggle)` | void | Enabling this on a radius blip will make it outline only. See https://cdn.discordapp.com/attachments... |
| `SET_SAVEGAME_LIST_UNIQUE_ID` | `void SET_SAVEGAME_LIST_UNIQUE_ID(object p0)` | void | - |
| `SET_SCRIPT_VARIABLE_HUD_COLOUR` | `void SET_SCRIPT_VARIABLE_HUD_COLOUR(int r, int g, int b, int a)` | void | Sets the color of HUD_COLOUR_SCRIPT_VARIABLE |
| `SET_SECOND_SCRIPT_VARIABLE_HUD_COLOUR` | `void SET_SECOND_SCRIPT_VARIABLE_HUD_COLOUR(int r, int g, int b, int a)` | void | Sets the color of HUD_COLOUR_SCRIPT_VARIABLE_2 |
| `SET_SOCIAL_CLUB_TOUR` | `void SET_SOCIAL_CLUB_TOUR(string name)` | void | HUD::SET_SOCIAL_CLUB_TOUR("Gallery");<br>HUD::SET_SOCIAL_CLUB_TOUR("Missions");<br>HUD::SET_SOCIAL_CLUB_TO... |
| `SET_TEXT_CENTRE` | `void SET_TEXT_CENTRE(bool align)` | void | - |
| `SET_TEXT_COLOUR` | `void SET_TEXT_COLOUR(int red, int green, int blue, int alpha)` | void | colors you input not same as you think?<br>A: for some reason its R B G A |
| `SET_TEXT_DROPSHADOW` | `void SET_TEXT_DROPSHADOW(int distance, int r, int g, int b, int a)` | void | distance - shadow distance in pixels, both horizontal and vertical<br>r, g, b, a - color |
| `SET_TEXT_DROP_SHADOW` | `void SET_TEXT_DROP_SHADOW()` | void | - |
| `SET_TEXT_EDGE` | `void SET_TEXT_EDGE(int p0, int r, int g, int b, int a)` | void | This native does absolutely nothing, just a nullsub |
| `SET_TEXT_FONT` | `void SET_TEXT_FONT(int fontType)` | void | fonts that mess up your text where made for number values/misc stuff |
| `SET_TEXT_INPUT_BOX_ENABLED` | `void SET_TEXT_INPUT_BOX_ENABLED(bool p0)` | void | - |
| `SET_TEXT_JUSTIFICATION` | `void SET_TEXT_JUSTIFICATION(int justifyType)` | void | Types -<br>0: Center-Justify<br>1: Left-Justify<br>2: Right-Justify<br><br>Right-Justify requires SET_TEXT_WRAP, ot... |
| `SET_TEXT_LEADING` | `void SET_TEXT_LEADING(int p0)` | void | - |
| `SET_TEXT_LINE_HEIGHT_MULT` | `void SET_TEXT_LINE_HEIGHT_MULT(float lineHeightMult)` | void | - |
| `SET_TEXT_OUTLINE` | `void SET_TEXT_OUTLINE()` | void | - |
| `SET_TEXT_PROPORTIONAL` | `void SET_TEXT_PROPORTIONAL(bool p0)` | void | This native does absolutely nothing, just a nullsub |
| `SET_TEXT_RENDER_ID` | `void SET_TEXT_RENDER_ID(int renderId)` | void | - |
| `SET_TEXT_RIGHT_JUSTIFY` | `void SET_TEXT_RIGHT_JUSTIFY(bool toggle)` | void | - |
| `SET_TEXT_SCALE` | `void SET_TEXT_SCALE(float scale, float size)` | void | Size range : 0F to 1.0F<br>p0 is unknown and doesn't seem to have an effect, yet in the game scripts it... |
| `SET_TEXT_WRAP` | `void SET_TEXT_WRAP(float start, float end)` | void | It sets the text in a specified box and wraps the text if it exceeds the boundries. Both values are ... |
| `SET_USE_ISLAND_MAP` | `void SET_USE_ISLAND_MAP(bool toggle)` | void | Toggles the Cayo Perico map. |
| `SET_USE_SET_DESTINATION_IN_PAUSE_MAP` | `void SET_USE_SET_DESTINATION_IN_PAUSE_MAP(bool toggle)` | void | - |
| `SET_WARNING_MESSAGE` | `void SET_WARNING_MESSAGE(string titleMsg, int flags, string promptMsg, bool p3, int p4, string p5, string p6, bool showBackground, int errorCode)` | void | You can only use text entries. No custom text.<br><br>Example: SET_WARNING_MESSAGE("t20", 3, "adder", fals... |
| `SET_WARNING_MESSAGE_OPTION_HIGHLIGHT` | `bool SET_WARNING_MESSAGE_OPTION_HIGHLIGHT(object p0)` | bool | - |
| `SET_WARNING_MESSAGE_OPTION_ITEMS` | `bool SET_WARNING_MESSAGE_OPTION_ITEMS(int index, string name, int cash, int rp, int lvl, int colour)` | bool | Some sort of list displayed in a warning message. Yet unknown how to prevent repeating.<br>Param names ... |
| `SET_WARNING_MESSAGE_WITH_HEADER` | `void SET_WARNING_MESSAGE_WITH_HEADER(string entryHeader, string entryLine1, int instructionalKey, string entryLine2, bool p4, object p5, ref object showBackground, ref object p7, bool p8, object p9)` | void | Shows a warning message on screen with a header.<br>Note: You can only use text entries. No custom text... |
| `SET_WARNING_MESSAGE_WITH_HEADER_AND_SUBSTRING_FLAGS` | `void SET_WARNING_MESSAGE_WITH_HEADER_AND_SUBSTRING_FLAGS(string entryHeader, string entryLine1, int instructionalKey, string entryLine2, bool p4, object p5, object additionalIntInfo, string additionalTextInfoLine1, string additionalTextInfoLine2, bool showBackground, int errorCode)` | void | You can use this native for custom input, without having to use any scaleform-related natives.<br>The n... |
| `SET_WARNING_MESSAGE_WITH_HEADER_AND_SUBSTRING_FLAGS_EXTENDED` | `void SET_WARNING_MESSAGE_WITH_HEADER_AND_SUBSTRING_FLAGS_EXTENDED(string labelTitle, string labelMessage, int p2, int p3, string labelMessage2, bool p5, int p6, int p7, string p8, string p9, bool background, int errorCode)` | void | labelTitle: Label of the alert's title.<br>labelMsg: Label of the alert's message.<br>p2: This is an enum,... |
| `SET_WARNING_MESSAGE_WITH_HEADER_EXTENDED` | `void SET_WARNING_MESSAGE_WITH_HEADER_EXTENDED(string entryHeader, string entryLine1, int flags, string entryLine2, bool p4, object p5, ref object p6, ref object p7, bool showBg, object p9, object p10)` | void | - |
| `SET_WAYPOINT_CLEAR_ON_ARRIVAL_MODE` | `void SET_WAYPOINT_CLEAR_ON_ARRIVAL_MODE(int mode)` | void | - |
| `SET_WAYPOINT_OFF` | `void SET_WAYPOINT_OFF()` | void | This native removes the current waypoint from the map.<br><br>Example:<br>C#:<br>Function.Call(Hash.SET_WAYPOINT... |
| `SET_WIDESCREEN_FORMAT` | `void SET_WIDESCREEN_FORMAT(object p0)` | void | - |
| `SHOW_ACCOUNT_PICKER` | `void SHOW_ACCOUNT_PICKER()` | void | - |
| `SHOW_CONTACT_INSTRUCTIONAL_BUTTON` | `void SHOW_CONTACT_INSTRUCTIONAL_BUTTON(bool toggle)` | void | - |
| `SHOW_CREW_INDICATOR_ON_BLIP` | `void SHOW_CREW_INDICATOR_ON_BLIP(int blip, bool toggle)` | void | Enables or disables the blue half circle https://i.imgur.com/iZes9Ec.png around the specified blip o... |
| `SHOW_FOR_SALE_ICON_ON_BLIP` | `void SHOW_FOR_SALE_ICON_ON_BLIP(int blip, bool toggle)` | void | - |
| `SHOW_FRIEND_INDICATOR_ON_BLIP` | `void SHOW_FRIEND_INDICATOR_ON_BLIP(int blip, bool toggle)` | void | Highlights a blip by a half cyan circle on the right side of the blip. https://i.imgur.com/FrV9M4e.p... |
| `SHOW_GOLD_TICK_ON_BLIP` | `void SHOW_GOLD_TICK_ON_BLIP(int blip, bool toggle)` | void | Adds a orange checkmark on top of a given blip handle: https://imgur.com/a/aw5OTMF<br>_SHOW_FRIEND_INDI... |
| `SHOW_HEADING_INDICATOR_ON_BLIP` | `void SHOW_HEADING_INDICATOR_ON_BLIP(int blip, bool toggle)` | void | Adds the GTA: Online player heading indicator to a blip. |
| `SHOW_HEIGHT_ON_BLIP` | `void SHOW_HEIGHT_ON_BLIP(int blip, bool toggle)` | void | - |
| `SHOW_HUD_COMPONENT_THIS_FRAME` | `void SHOW_HUD_COMPONENT_THIS_FRAME(int id)` | void | This function hides various HUD (Heads-up Display) components.<br>Listed below are the integers and the... |
| `SHOW_NUMBER_ON_BLIP` | `void SHOW_NUMBER_ON_BLIP(int blip, int number)` | void | - |
| `SHOW_OUTLINE_INDICATOR_ON_BLIP` | `void SHOW_OUTLINE_INDICATOR_ON_BLIP(int blip, bool toggle)` | void | Highlights a blip by a cyan color circle.<br><br>Color can be changed with SET_BLIP_SECONDARY_COLOUR |
| `SHOW_SCRIPTED_HUD_COMPONENT_THIS_FRAME` | `void SHOW_SCRIPTED_HUD_COMPONENT_THIS_FRAME(int id)` | void | - |
| `SHOW_START_MISSION_INSTRUCTIONAL_BUTTON` | `void SHOW_START_MISSION_INSTRUCTIONAL_BUTTON(bool toggle)` | void | - |
| `SHOW_TICK_ON_BLIP` | `void SHOW_TICK_ON_BLIP(int blip, bool toggle)` | void | Adds a green checkmark on top of a blip. |
| `START_GPS_CUSTOM_ROUTE` | `void START_GPS_CUSTOM_ROUTE(int hudColor, bool displayOnFoot, bool followPlayer)` | void | Starts a new GPS custom-route, allowing you to plot lines on the map.<br>Lines are drawn directly betwe... |
| `START_GPS_MULTI_ROUTE` | `void START_GPS_MULTI_ROUTE(int hudColor, bool routeFromPlayer, bool displayOnFoot)` | void | Starts a new GPS multi-route, allowing you to create custom GPS paths.<br>GPS functions like the waypoi... |
| `SUPPRESS_FRONTEND_RENDERING_THIS_FRAME` | `void SUPPRESS_FRONTEND_RENDERING_THIS_FRAME()` | void | - |
| `TAKE_CONTROL_OF_FRONTEND` | `void TAKE_CONTROL_OF_FRONTEND()` | void | Disables frontend (works in custom frontends, not sure about regular pause menu) navigation keys on ... |
| `THEFEED_AUTO_POST_GAMETIPS_OFF` | `void THEFEED_AUTO_POST_GAMETIPS_OFF()` | void | Displays "normal" notifications again after calling `THEFEED_AUTO_POST_GAMETIPS_ON` (those that were... |
| `THEFEED_AUTO_POST_GAMETIPS_ON` | `void THEFEED_AUTO_POST_GAMETIPS_ON()` | void | Enables loading screen tips to be be shown (`THEFEED_SHOW`), blocks other kinds of notifications fro... |
| `THEFEED_CLEAR_FROZEN_POST` | `void THEFEED_CLEAR_FROZEN_POST()` | void | - |
| `THEFEED_FLUSH_QUEUE` | `void THEFEED_FLUSH_QUEUE()` | void | - |
| `THEFEED_FORCE_RENDER_OFF` | `void THEFEED_FORCE_RENDER_OFF()` | void | Enables loading screen tips to be be shown (`THEFEED_SHOW`), blocks other kinds of notifications fro... |
| `THEFEED_FORCE_RENDER_ON` | `void THEFEED_FORCE_RENDER_ON()` | void | - |
| `THEFEED_FREEZE_NEXT_POST` | `void THEFEED_FREEZE_NEXT_POST()` | void | Requires manual management of game stream handles (i.e., 0xBE4390CB40B3E627). |
| `THEFEED_GET_LAST_SHOWN_PHONE_ACTIVATABLE_FEED_ID` | `int THEFEED_GET_LAST_SHOWN_PHONE_ACTIVATABLE_FEED_ID()` | int | Returns the handle for the notification currently displayed on the screen. Name may be a hash collis... |
| `THEFEED_HIDE` | `void THEFEED_HIDE()` | void | Stops loading screen tips shown by invoking `THEFEED_SHOW` |
| `THEFEED_HIDE_THIS_FRAME` | `void THEFEED_HIDE_THIS_FRAME()` | void | Once called each frame hides all above radar notifications. |
| `THEFEED_IS_PAUSED` | `bool THEFEED_IS_PAUSED()` | bool | - |
| `THEFEED_ONLY_SHOW_TOOLTIPS` | `void THEFEED_ONLY_SHOW_TOOLTIPS(bool toggle)` | void | - |
| `THEFEED_PAUSE` | `void THEFEED_PAUSE()` | void | - |
| `THEFEED_REMOVE_ITEM` | `void THEFEED_REMOVE_ITEM(int notificationId)` | void | Removes a notification instantly instead of waiting for it to disappear |
| `THEFEED_REPORT_LOGO_OFF` | `void THEFEED_REPORT_LOGO_OFF()` | void | - |
| `THEFEED_REPORT_LOGO_ON` | `void THEFEED_REPORT_LOGO_ON()` | void | - |
| `THEFEED_RESET_ALL_PARAMETERS` | `void THEFEED_RESET_ALL_PARAMETERS()` | void | - |
| `THEFEED_RESUME` | `void THEFEED_RESUME()` | void | - |
| `THEFEED_SET_BACKGROUND_COLOR_FOR_NEXT_POST` | `void THEFEED_SET_BACKGROUND_COLOR_FOR_NEXT_POST(int hudColorIndex)` | void | From the decompiled scripts:<br>HUD::THEFEED_SET_BACKGROUND_COLOR_FOR_NEXT_POST(6);<br>HUD::THEFEED_SET_BA... |
| `THEFEED_SET_FLASH_DURATION_PARAMETER_FOR_NEXT_MESSAGE` | `void THEFEED_SET_FLASH_DURATION_PARAMETER_FOR_NEXT_MESSAGE(int count)` | void | Related to notification color flashing, setting count to 0 invalidates a `THEFEED_SET_RGBA_PARAMETER... |
| `THEFEED_SET_RGBA_PARAMETER_FOR_NEXT_MESSAGE` | `void THEFEED_SET_RGBA_PARAMETER_FOR_NEXT_MESSAGE(int red, int green, int blue, int alpha)` | void | - |
| `THEFEED_SET_SCRIPTED_MENU_HEIGHT` | `void THEFEED_SET_SCRIPTED_MENU_HEIGHT(float pos)` | void | - |
| `THEFEED_SET_SNAP_FEED_ITEM_POSITIONS` | `void THEFEED_SET_SNAP_FEED_ITEM_POSITIONS(bool p0)` | void | - |
| `THEFEED_SET_VIBRATE_PARAMETER_FOR_NEXT_MESSAGE` | `void THEFEED_SET_VIBRATE_PARAMETER_FOR_NEXT_MESSAGE(bool toggle)` | void | - |
| `THEFEED_SHOW` | `void THEFEED_SHOW()` | void | Displays loading screen tips, requires `THEFEED_AUTO_POST_GAMETIPS_ON` to be called beforehand. |
| `THEFEED_UPDATE_ITEM_TEXTURE` | `void THEFEED_UPDATE_ITEM_TEXTURE(string txdString1, string txnString1, string txdString2, string txnString2)` | void | Used in the native scripts to reference "GET_PEDHEADSHOT_TXD_STRING" and "CHAR_DEFAULT". |
| `TOGGLE_STEALTH_RADAR` | `void TOGGLE_STEALTH_RADAR(bool toggle)` | void | - |
| `TRIGGER_SONAR_BLIP` | `void TRIGGER_SONAR_BLIP(float posX, float posY, float posZ, float radius, int p4)` | void | - |
| `UNLOCK_MINIMAP_ANGLE` | `void UNLOCK_MINIMAP_ANGLE()` | void | - |
| `UNLOCK_MINIMAP_POSITION` | `void UNLOCK_MINIMAP_POSITION()` | void | - |
| `UPDATE_RADAR_ZOOM_TO_BLIP` | `void UPDATE_RADAR_ZOOM_TO_BLIP()` | void | Does nothing (it's a nullsub). |
| `USE_FAKE_MP_CASH` | `void USE_FAKE_MP_CASH(bool toggle)` | void | Related to displaying cash on the HUD<br>Always called before HUD::CHANGE_FAKE_MP_CASH in decompiled sc... |
| `USE_VEHICLE_TARGETING_RETICULE` | `void USE_VEHICLE_TARGETING_RETICULE(object p0)` | void | - |
| `_SET_BLIP_GPS_ROUTE_DISPLAY_DISTANCE` | `void _SET_BLIP_GPS_ROUTE_DISPLAY_DISTANCE(int blip, int blipChangeParam46, bool blipChangeParam47)` | void | Applies to new eBlipParams _BLIP_CHANGE_46* and _BLIP_CHANGE_47* |
| `_SET_PAUSE_EXTERIOR_RENDERING_WHILE_IN_INTERIOR` | `void _SET_PAUSE_EXTERIOR_RENDERING_WHILE_IN_INTERIOR()` | void | - |
| `_SHOW_PURCHASE_INSTRUCTIONAL_BUTTON` | `void _SHOW_PURCHASE_INSTRUCTIONAL_BUTTON(bool toggle)` | void | - |
| `_USE_VEHICLE_TARGETING_RETICULE_ON_VEHICLES` | `void _USE_VEHICLE_TARGETING_RETICULE_ON_VEHICLES(bool enable)` | void | - |

---

## 详细说明

### ACTIVATE_FRONTEND_MENU

- **命名空间:** `HUD`
- **Hash:** `0xEF01D36B9C9D0C7B`
- **JHash:** `0x01D83872`
- **Build:** `323`

**C# 签名:**
```csharp
void ACTIVATE_FRONTEND_MENU(uint menuhash, bool togglePause, int component)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `menuhash` | `Hash` | `uint` | - |
| `togglePause` | `BOOL` | `bool` | - |
| `component` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Does stuff like this:
gyazo.com/7fcb78ea3520e3dbc5b2c0c0f3712617

Example:
int GetHash = GET_HASH_KEY("fe_menu_version_corona_lobby");
ACTIVATE_FRONTEND_MENU(GetHash, 0, -1);

BOOL p1 is a toggle to define the game in pause.
int p2 is unknown but -1 always works, not sure why though.

[30/03/2017] ins1de :

the int p2 is actually a component variable. When the pause menu is visible, it opens the tab related to it.

Example : Function.Call(Hash.ACTIVATE_FRONTEND_MENU,-1171018317, 0, 42);
Result : Opens the "Online" tab without pausing the menu, with -1 it opens the map.Below is a list of all known Frontend Menu Hashes.
- FE_MENU_VERSION_SP_PAUSE
- FE_MENU_VERSION_MP_PAUSE
- FE_MENU_VERSION_CREATOR_PAUSE
- FE_MENU_VERSION_CUTSCENE_PAUSE
- FE_MENU_VERSION_SAVEGAME
- FE_MENU_VERSION_PRE_LOBBY
- FE_MENU_VERSION_LOBBY
- FE_MENU_VERSION_MP_CHARACTER_SELECT
- FE_MENU_VERSION_MP_CHARACTER_CREATION
- FE_MENU_VERSION_EMPTY
- FE_MENU_VERSION_EMPTY_NO_BACKGROUND
- FE_MENU_VERSION_TEXT_SELECTION
- FE_MENU_VERSION_CORONA
- FE_MENU_VERSION_CORONA_LOBBY
- FE_MENU_VERSION_CORONA_JOINED_PLAYERS
- FE_MENU_VERSION_CORONA_INVITE_PLAYERS
- FE_MENU_VERSION_CORONA_INVITE_FRIENDS
- FE_MENU_VERSION_CORONA_INVITE_CREWS
- FE_MENU_VERSION_CORONA_INVITE_MATCHED_PLAYERS
- FE_MENU_VERSION_CORONA_INVITE_LAST_JOB_PLAYERS
- FE_MENU_VERSION_CORONA_RACE
- FE_MENU_VERSION_CORONA_BETTING
- FE_MENU_VERSION_JOINING_SCREEN
- FE_MENU_VERSION_LANDING_MENU
- FE_MENU_VERSION_LANDING_KEYMAPPING_MENU
```

---

### ADD_BLIP_FOR_AREA

- **命名空间:** `HUD`
- **Hash:** `0xCE5D0E5E315DB238`
- **Build:** `463`

**C# 签名:**
```csharp
int ADD_BLIP_FOR_AREA(float x, float y, float z, float width, float height)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `float` | `float` | - |
| `width` | `float` | `float` | - |
| `height` | `float` | `float` | - |

**返回值:** `int` (Native: `Blip`)

**说明:**
```
Adds a rectangular blip for the specified coordinates/area.

It is recommended to use SET_BLIP_ROTATION and SET_BLIP_COLOUR to make the blip not rotate along with the camera.

By default, the blip will show as a _regular_ blip with the specified color/sprite if it is outside of the minimap view.

Example image:
minimap https://w.wew.wtf/pdcjig.png
big map https://w.wew.wtf/zgcjcm.png

(Native name is _likely_ to actually be ADD_BLIP_FOR_AREA, but due to the usual reasons this can't be confirmed)
```

---

### ADD_BLIP_FOR_COORD

- **命名空间:** `HUD`
- **Hash:** `0x5A039BB0BCA604B6`
- **JHash:** `0xC6F43D0E`
- **Build:** `323`

**C# 签名:**
```csharp
int ADD_BLIP_FOR_COORD(float x, float y, float z)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `float` | `float` | - |

**返回值:** `int` (Native: `Blip`)

**说明:**
```
Creates an orange ( default ) Blip-object. Returns a Blip-object which can then be modified.
```

---

### ADD_BLIP_FOR_ENTITY

- **命名空间:** `HUD`
- **Hash:** `0x5CDE92C702A8FCE7`
- **JHash:** `0x30822554`
- **Build:** `323`

**C# 签名:**
```csharp
int ADD_BLIP_FOR_ENTITY(int entity)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `entity` | `Entity` | `int` | - |

**返回值:** `int` (Native: `Blip`)

**说明:**
```
Returns red ( default ) blip attached to entity.

Example:
Blip blip; //Put this outside your case or option
blip = HUD::ADD_BLIP_FOR_ENTITY(YourPedOrBodyguardName);
HUD::SET_BLIP_AS_FRIENDLY(blip, true);
```

---

### ADD_BLIP_FOR_PICKUP

- **命名空间:** `HUD`
- **Hash:** `0xBE339365C863BD36`
- **JHash:** `0x16693C3A`
- **Build:** `323`

**C# 签名:**
```csharp
int ADD_BLIP_FOR_PICKUP(int pickup)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `pickup` | `Pickup` | `int` | - |

**返回值:** `int` (Native: `Blip`)

---

### ADD_BLIP_FOR_RADIUS

- **命名空间:** `HUD`
- **Hash:** `0x46818D79B1F7499A`
- **JHash:** `0x4626756C`
- **Build:** `323`

**C# 签名:**
```csharp
int ADD_BLIP_FOR_RADIUS(float posX, float posY, float posZ, float radius)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `posX` | `float` | `float` | - |
| `posY` | `float` | `float` | - |
| `posZ` | `float` | `float` | - |
| `radius` | `float` | `float` | - |

**返回值:** `int` (Native: `Blip`)

---

### ADD_NEXT_MESSAGE_TO_PREVIOUS_BRIEFS

- **命名空间:** `HUD`
- **Hash:** `0x60296AF4BA14ABC5`
- **JHash:** `0xB58B25BD`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_NEXT_MESSAGE_TO_PREVIOUS_BRIEFS(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_POINT_TO_GPS_CUSTOM_ROUTE

- **命名空间:** `HUD`
- **Hash:** `0x311438A071DD9B1A`
- **JHash:** `0xEEBDFE55`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_POINT_TO_GPS_CUSTOM_ROUTE(float x, float y, float z)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_POINT_TO_GPS_MULTI_ROUTE

- **命名空间:** `HUD`
- **Hash:** `0xA905192A6781C41B`
- **JHash:** `0xFE485135`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_POINT_TO_GPS_MULTI_ROUTE(float x, float y, float z)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_TEXT_COMPONENT_FLOAT

- **命名空间:** `HUD`
- **Hash:** `0xE7DCB5B874BCD96E`
- **JHash:** `0x24D78013`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_FLOAT(float value, int decimalPlaces)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `value` | `float` | `float` | - |
| `decimalPlaces` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_TEXT_COMPONENT_FORMATTED_INTEGER

- **命名空间:** `HUD`
- **Hash:** `0x0E4C749FF9DE9CC4`
- **JHash:** `0x12929BDF`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_FORMATTED_INTEGER(int value, bool commaSeparated)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `value` | `int` | `int` | - |
| `commaSeparated` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_TEXT_COMPONENT_INTEGER

- **命名空间:** `HUD`
- **Hash:** `0x03B504CF259931BC`
- **JHash:** `0xFE272A57`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_INTEGER(int value)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `value` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_TEXT_COMPONENT_SUBSTRING_BLIP_NAME

- **命名空间:** `HUD`
- **Hash:** `0x80EAD8E2E1D5D52E`
- **JHash:** `0x5DE98F0A`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_SUBSTRING_BLIP_NAME(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_TEXT_COMPONENT_SUBSTRING_KEYBOARD_DISPLAY

- **命名空间:** `HUD`
- **Hash:** `0x5F68520888E69014`
- **JHash:** `0x0829A799`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_SUBSTRING_KEYBOARD_DISPLAY(string string)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `string` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_TEXT_COMPONENT_SUBSTRING_PHONE_NUMBER

- **命名空间:** `HUD`
- **Hash:** `0x761B77454205A61D`
- **JHash:** `0x65E1D404`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_SUBSTRING_PHONE_NUMBER(string p0, int p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `const char*` | `string` | - |
| `p1` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
p1 was always -1
```

---

### ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME

- **命名空间:** `HUD`
- **Hash:** `0x6C188BE134E074AA`
- **JHash:** `0x27A244D8`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(string text)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `text` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL

- **命名空间:** `HUD`
- **Hash:** `0xC63CD5D2920ACBE7`
- **JHash:** `0xDCE05406`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL(string labelName)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `labelName` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL_HASH_KEY

- **命名空间:** `HUD`
- **Hash:** `0x17299B63C7683A2B`
- **JHash:** `0x150E03B6`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL_HASH_KEY(uint gxtEntryHash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxtEntryHash` | `Hash` | `uint` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
It adds the localized text of the specified GXT entry name. Eg. if the argument is GET_HASH_KEY("ES_HELP"), adds "Continue". Just uses a text labels hash key
```

---

### ADD_TEXT_COMPONENT_SUBSTRING_TIME

- **命名空间:** `HUD`
- **Hash:** `0x1115F16B8AB9E8BF`
- **JHash:** `0x135B3CD0`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_SUBSTRING_TIME(int timestamp, int flags)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `timestamp` | `int` | `int` | - |
| `flags` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Adds a timer (e.g. "00:00:00:000"). The appearance of the timer depends on the flags, which needs more research.
```

---

### ADD_TEXT_COMPONENT_SUBSTRING_WEBSITE

- **命名空间:** `HUD`
- **Hash:** `0x94CF4AC034C9C986`
- **JHash:** `0xC736999E`
- **Build:** `323`

**C# 签名:**
```csharp
void ADD_TEXT_COMPONENT_SUBSTRING_WEBSITE(string website)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `website` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
This native (along with ADD_TEXT_COMPONENT_SUBSTRING_KEYBOARD_DISPLAY and ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME) do not actually filter anything. They simply add the provided text (as of 944)
```

---

### ADD_VALID_VEHICLE_HIT_HASH

- **命名空间:** `HUD`
- **Hash:** `0xE4C3B169876D33D7`
- **Build:** `1290`

**C# 签名:**
```csharp
void ADD_VALID_VEHICLE_HIT_HASH(object p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### ALLOW_DISPLAY_OF_MULTIPLAYER_CASH_TEXT

- **命名空间:** `HUD`
- **Hash:** `0xE67C6DFD386EA5E7`
- **JHash:** `0x5476B9FD`
- **Build:** `323`

**C# 签名:**
```csharp
void ALLOW_DISPLAY_OF_MULTIPLAYER_CASH_TEXT(bool allow)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `allow` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Controls whether to display 'Cash'/'Bank' next to the money balance HUD in Multiplayer (https://i.imgur.com/MiYUtNl.png)
```

---

### ALLOW_PAUSE_WHEN_NOT_IN_STATE_OF_PLAY_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xCC3FDDED67BCFC63`
- **JHash:** `0x630CD8EE`
- **Build:** `323`

**C# 签名:**
```csharp
void ALLOW_PAUSE_WHEN_NOT_IN_STATE_OF_PLAY_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Allows opening the pause menu this frame, when the player is dead.
```

---

### ALLOW_SONAR_BLIPS

- **命名空间:** `HUD`
- **Hash:** `0x60734CC207C9833C`
- **JHash:** `0xE7E1E32B`
- **Build:** `323`

**C# 签名:**
```csharp
void ALLOW_SONAR_BLIPS(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### ARE_ONLINE_POLICIES_UP_TO_DATE

- **命名空间:** `HUD`
- **Hash:** `0xF13FE2A80C05C561`
- **JHash:** `0x850690FF`
- **Build:** `323`

**C# 签名:**
```csharp
bool ARE_ONLINE_POLICIES_UP_TO_DATE()
```

**返回值:** `bool` (Native: `BOOL`)

---

### BEGIN_TEXT_COMMAND_ADD_DIRECTLY_TO_PREVIOUS_BRIEFS

- **命名空间:** `HUD`
- **Hash:** `0x23D69E0465570028`
- **JHash:** `0x0E103475`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_ADD_DIRECTLY_TO_PREVIOUS_BRIEFS(string p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### BEGIN_TEXT_COMMAND_BUSYSPINNER_ON

- **命名空间:** `HUD`
- **Hash:** `0xABA17D7CE615ADBF`
- **JHash:** `0xCB7C8994`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_BUSYSPINNER_ON(string string)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `string` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Initializes the text entry for the the text next to a loading prompt. All natives for building UI texts can be used here


e.g
void StartLoadingMessage(char *text, int spinnerType = 3)
  {
     BEGIN_TEXT_COMMAND_BUSYSPINNER_ON("STRING");
       ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
       END_TEXT_COMMAND_BUSYSPINNER_ON(spinnerType);
    }
/*OR*/
 void ShowLoadingMessage(char *text, int spinnerType = 3, int timeMs = 10000)
  {
     BEGIN_TEXT_COMMAND_BUSYSPINNER_ON("STRING");
       ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
       END_TEXT_COMMAND_BUSYSPINNER_ON(spinnerType);
        WAIT(timeMs);
     BUSYSPINNER_OFF();
 }


These are some localized strings used in the loading spinner.
"PM_WAIT"                   = Please Wait
"CELEB_WPLYRS"              = Waiting For Players.
"CELL_SPINNER2"             = Scanning storage.
"ERROR_CHECKYACHTNAME" = Registering your yacht's name. Please wait.
"ERROR_CHECKPROFANITY"   = Checking your text for profanity. Please wait.
"FM_COR_AUTOD"                        = Just spinner no text
"FM_IHELP_WAT2"                        = Waiting for other players
"FM_JIP_WAITO"                            = Game options are being set
"FMMC_DOWNLOAD"                    = Downloading
"FMMC_PLYLOAD"                         = Loading
"FMMC_STARTTRAN"                    = Launching session
"HUD_QUITTING"                           =  Quiting session
"KILL_STRIP_IDM"                         = Waiting for to accept
"MP_SPINLOADING"                      = Loading
```

---

### BEGIN_TEXT_COMMAND_CLEAR_PRINT

- **命名空间:** `HUD`
- **Hash:** `0xE124FA80A759019C`
- **JHash:** `0x550665AE`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_CLEAR_PRINT(string text)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `text` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
clears a print text command with this text
```

---

### BEGIN_TEXT_COMMAND_DISPLAY_HELP

- **命名空间:** `HUD`
- **Hash:** `0x8509B634FBE7DA11`
- **JHash:** `0xB245FC10`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_DISPLAY_HELP(string inputType)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `inputType` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Used to be known as _SET_TEXT_COMPONENT_FORMAT
```

---

### BEGIN_TEXT_COMMAND_DISPLAY_TEXT

- **命名空间:** `HUD`
- **Hash:** `0x25FBB336DF1804CB`
- **JHash:** `0x3E35563E`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_DISPLAY_TEXT(string text)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `text` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
The following were found in the decompiled script files:
STRING, TWOSTRINGS, NUMBER, PERCENTAGE, FO_TWO_NUM, ESMINDOLLA, ESDOLLA, MTPHPER_XPNO, AHD_DIST, CMOD_STAT_0, CMOD_STAT_1, CMOD_STAT_2, CMOD_STAT_3, DFLT_MNU_OPT, F3A_TRAFDEST, ES_HELP_SOC3

ESDOLLA - cash
ESMINDOLLA - cash (negative)

Used to be known as _SET_TEXT_ENTRY
```

---

### BEGIN_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING

- **命名空间:** `HUD`
- **Hash:** `0x521FB041D93DD0E4`
- **JHash:** `0x94B82066`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING(string entry)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `entry` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
int GetLineCount(char *text, float x, float y)
    {
     BEGIN_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING("STRING");
                ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
      return BEGIN_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING(x, y);
    }
```

---

### BEGIN_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT

- **命名空间:** `HUD`
- **Hash:** `0x54CE8AC98E120CAB`
- **JHash:** `0x51E7A037`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT(string text)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `text` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### BEGIN_TEXT_COMMAND_IS_MESSAGE_DISPLAYED

- **命名空间:** `HUD`
- **Hash:** `0x853648FD1063A213`
- **JHash:** `0xDD524A11`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_IS_MESSAGE_DISPLAYED(string text)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `text` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
nothin doin. 

BOOL Message(const char* text)
   {
     BEGIN_TEXT_COMMAND_IS_MESSAGE_DISPLAYED("STRING");
      ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
       return END_TEXT_COMMAND_IS_MESSAGE_DISPLAYED();
   }
```

---

### BEGIN_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED

- **命名空间:** `HUD`
- **Hash:** `0x0A24DA3A41B718F5`
- **JHash:** `0x00E20F2D`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED(string labelName)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `labelName` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
BOOL IsContextActive(char *ctx)
    {
     BEGIN_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED(ctx);
     return END_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED(0);
  }
```

---

### BEGIN_TEXT_COMMAND_OVERRIDE_BUTTON_TEXT

- **命名空间:** `HUD`
- **Hash:** `0x8F9EE5687F8EECCD`
- **JHash:** `0xBF855650`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_OVERRIDE_BUTTON_TEXT(string gxtEntry)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxtEntry` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### BEGIN_TEXT_COMMAND_PRINT

- **命名空间:** `HUD`
- **Hash:** `0xB87A37EEB7FAA67D`
- **JHash:** `0xF42C43C7`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_PRINT(string GxtEntry)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `GxtEntry` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
void ShowSubtitle(const char *text)
{
  BEGIN_TEXT_COMMAND_PRINT("STRING");
 ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
   END_TEXT_COMMAND_PRINT(2000, true);
}
```

---

### BEGIN_TEXT_COMMAND_SET_BLIP_NAME

- **命名空间:** `HUD`
- **Hash:** `0xF9113A30DE5C6670`
- **JHash:** `0xF4C211F6`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_SET_BLIP_NAME(string textLabel)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `textLabel` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Starts a text command to change the name of a blip displayed in the pause menu.
This should be paired with `END_TEXT_COMMAND_SET_BLIP_NAME`, once adding all required text components.
Example:

HUD::BEGIN_TEXT_COMMAND_SET_BLIP_NAME("STRING");
HUD::ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME("Name");
HUD::END_TEXT_COMMAND_SET_BLIP_NAME(blip);
```

---

### BEGIN_TEXT_COMMAND_THEFEED_POST

- **命名空间:** `HUD`
- **Hash:** `0x202709F4C58A0424`
- **JHash:** `0x574EE85C`
- **Build:** `323`

**C# 签名:**
```csharp
void BEGIN_TEXT_COMMAND_THEFEED_POST(string text)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `text` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Declares the entry type of a notification, for example "STRING".

int ShowNotification(char *text)
{
	BEGIN_TEXT_COMMAND_THEFEED_POST("STRING");
	ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
	return END_TEXT_COMMAND_THEFEED_POST_TICKER(1, 1);
}
```

---

### BUSYSPINNER_IS_DISPLAYING

- **命名空间:** `HUD`
- **Hash:** `0xB2A592B04648A9CB`
- **JHash:** `0x3AF34DEF`
- **Build:** `323`

**C# 签名:**
```csharp
bool BUSYSPINNER_IS_DISPLAYING()
```

**返回值:** `bool` (Native: `BOOL`)

---

### BUSYSPINNER_IS_ON

- **命名空间:** `HUD`
- **Hash:** `0xD422FCC5F239A915`
- **JHash:** `0xB8B3A5D0`
- **Build:** `323`

**C# 签名:**
```csharp
bool BUSYSPINNER_IS_ON()
```

**返回值:** `bool` (Native: `BOOL`)

---

### BUSYSPINNER_OFF

- **命名空间:** `HUD`
- **Hash:** `0x10D373323E5B9C0D`
- **JHash:** `0x94119534`
- **Build:** `323`

**C# 签名:**
```csharp
void BUSYSPINNER_OFF()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Removes the loading prompt at the bottom right of the screen.
```

---

### CAN_PHONE_BE_SEEN_ON_SCREEN

- **命名空间:** `MOBILE`
- **Hash:** `0xC4E2813898C97A4B`
- **JHash:** `0x5F978584`
- **Build:** `323`

**C# 签名:**
```csharp
bool CAN_PHONE_BE_SEEN_ON_SCREEN()
```

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
This one is weird and seems to return a TRUE state regardless of whether the phone is visible on screen or tucked away.


I can confirm the above. This function is hard-coded to always return 1.
```

---

### CELL_CAM_ACTIVATE

- **命名空间:** `MOBILE`
- **Hash:** `0xFDE8F069C542D126`
- **JHash:** `0x234C1AE9`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_ACTIVATE(bool p0, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_ACTIVATE_SELFIE_MODE

- **命名空间:** `MOBILE`
- **Hash:** `0x015C49A93E3E086E`
- **JHash:** `0x4479B304`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_ACTIVATE_SELFIE_MODE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_ACTIVATE_SHALLOW_DOF_MODE

- **命名空间:** `MOBILE`
- **Hash:** `0xA2CCBE62CD4C91A4`
- **JHash:** `0xC273BB4D`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_ACTIVATE_SHALLOW_DOF_MODE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_IS_CHAR_VISIBLE_NO_FACE_CHECK

- **命名空间:** `MOBILE`
- **Hash:** `0x439E9BC95B7E7FBE`
- **JHash:** `0xBEA88097`
- **Build:** `323`

**C# 签名:**
```csharp
bool CELL_CAM_IS_CHAR_VISIBLE_NO_FACE_CHECK(int entity)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `entity` | `Entity` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### CELL_CAM_SET_SELFIE_MODE_DISTANCE_SCALING

- **命名空间:** `MOBILE`
- **Hash:** `0xAC2890471901861C`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_SET_SELFIE_MODE_DISTANCE_SCALING(float distanceScaling)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `distanceScaling` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_SET_SELFIE_MODE_HEAD_PITCH_OFFSET

- **命名空间:** `MOBILE`
- **Hash:** `0x466DA42C89865553`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_SET_SELFIE_MODE_HEAD_PITCH_OFFSET(float pitch)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `pitch` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_SET_SELFIE_MODE_HEAD_ROLL_OFFSET

- **命名空间:** `MOBILE`
- **Hash:** `0xF1E22DC13F5EEBAD`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_SET_SELFIE_MODE_HEAD_ROLL_OFFSET(float roll)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `roll` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_SET_SELFIE_MODE_HEAD_YAW_OFFSET

- **命名空间:** `MOBILE`
- **Hash:** `0xD6ADE981781FCA09`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_SET_SELFIE_MODE_HEAD_YAW_OFFSET(float yaw)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `yaw` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_SET_SELFIE_MODE_HORZ_PAN_OFFSET

- **命名空间:** `MOBILE`
- **Hash:** `0x53F4892D18EC90A4`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_SET_SELFIE_MODE_HORZ_PAN_OFFSET(float horizontalPan)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `horizontalPan` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_SET_SELFIE_MODE_ROLL_OFFSET

- **命名空间:** `MOBILE`
- **Hash:** `0x15E69E2802C24B8D`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_SET_SELFIE_MODE_ROLL_OFFSET(float roll)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `roll` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_SET_SELFIE_MODE_SIDE_OFFSET_SCALING

- **命名空间:** `MOBILE`
- **Hash:** `0x1B0B4AEED5B9B41C`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_SET_SELFIE_MODE_SIDE_OFFSET_SCALING(float p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_CAM_SET_SELFIE_MODE_VERT_PAN_OFFSET

- **命名空间:** `MOBILE`
- **Hash:** `0x3117D84EFA60F77B`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_CAM_SET_SELFIE_MODE_VERT_PAN_OFFSET(float vertPan)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `vertPan` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### CELL_HORIZONTAL_MODE_TOGGLE

- **命名空间:** `MOBILE`
- **Hash:** `0x44E44169EF70138E`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_HORIZONTAL_MODE_TOGGLE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
if the bool "Toggle" is "true" so the phone is lean.
if the bool "Toggle" is "false" so the phone is not lean.
```

---

### CELL_SET_INPUT

- **命名空间:** `MOBILE`
- **Hash:** `0x95C9E72F3D7DEC9B`
- **Build:** `323`

**C# 签名:**
```csharp
void CELL_SET_INPUT(int direction)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `direction` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
For move the finger of player, the value of int goes 1 at 5.
```

---

### CHANGE_FAKE_MP_CASH

- **命名空间:** `HUD`
- **Hash:** `0x0772DF77852C2E30`
- **JHash:** `0xE319F90B`
- **Build:** `323`

**C# 签名:**
```csharp
void CHANGE_FAKE_MP_CASH(int cash, int bank)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `cash` | `int` | `int` | - |
| `bank` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Displays cash change notifications on HUD.
```

---

### CLEAR_ADDITIONAL_TEXT

- **命名空间:** `HUD`
- **Hash:** `0x2A179DF17CCF04CD`
- **JHash:** `0x518141E0`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_ADDITIONAL_TEXT(int p0, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### CLEAR_ALL_BLIP_ROUTES

- **命名空间:** `HUD`
- **Hash:** `0xD12882D3FF82BF11`
- **Build:** `877`

**C# 签名:**
```csharp
void CLEAR_ALL_BLIP_ROUTES()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_ALL_HELP_MESSAGES

- **命名空间:** `HUD`
- **Hash:** `0x6178F68A87A4D3A0`
- **JHash:** `0x9E5D9198`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_ALL_HELP_MESSAGES()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_BRIEF

- **命名空间:** `HUD`
- **Hash:** `0x9D292F73ADBD9313`
- **JHash:** `0x9F75A929`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_BRIEF()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_DYNAMIC_PAUSE_MENU_ERROR_MESSAGE

- **命名空间:** `HUD`
- **Hash:** `0x7792424AA0EAC32E`
- **JHash:** `0x2F9A309C`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_DYNAMIC_PAUSE_MENU_ERROR_MESSAGE()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_FAKE_CONE_ARRAY

- **命名空间:** `HUD`
- **Hash:** `0x8410C5E0CD847B9D`
- **Build:** `1290`

**C# 签名:**
```csharp
void CLEAR_FAKE_CONE_ARRAY()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_FLOATING_HELP

- **命名空间:** `HUD`
- **Hash:** `0x50085246ABD3FEFA`
- **JHash:** `0xB181F88F`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_FLOATING_HELP(int hudIndex, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudIndex` | `int` | `int` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### CLEAR_GPS_CUSTOM_ROUTE

- **命名空间:** `HUD`
- **Hash:** `0xE6DE0561D9232A64`
- **JHash:** `0xCF2E3E24`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_GPS_CUSTOM_ROUTE()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_GPS_FLAGS

- **命名空间:** `HUD`
- **Hash:** `0x21986729D6A3A830`
- **JHash:** `0x056AFCE6`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_GPS_FLAGS()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Clears the GPS flags. Only the script that originally called SET_GPS_FLAGS can clear them.

Doesn't seem like the flags are actually read by the game at all.
```

---

### CLEAR_GPS_MULTI_ROUTE

- **命名空间:** `HUD`
- **Hash:** `0x67EEDEA1B9BAFD94`
- **JHash:** `0x0D9969E4`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_GPS_MULTI_ROUTE()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Does the same as SET_GPS_MULTI_ROUTE_RENDER(false);
```

---

### CLEAR_GPS_PLAYER_WAYPOINT

- **命名空间:** `HUD`
- **Hash:** `0xFF4FB7C8CDFA3DA7`
- **JHash:** `0x0B9C7FC2`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_GPS_PLAYER_WAYPOINT()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_GPS_RACE_TRACK

- **命名空间:** `HUD`
- **Hash:** `0x7AA5B4CE533C858B`
- **JHash:** `0x40C59829`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_GPS_RACE_TRACK()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Does the same as SET_RACE_TRACK_RENDER(false);
```

---

### CLEAR_HELP

- **命名空间:** `HUD`
- **Hash:** `0x8DFCED7A656F8802`
- **JHash:** `0xE6D85741`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_HELP(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### CLEAR_PED_IN_PAUSE_MENU

- **命名空间:** `HUD`
- **Hash:** `0x5E62BE5DC58E9E06`
- **JHash:** `0x28058ACF`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_PED_IN_PAUSE_MENU()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_PRINTS

- **命名空间:** `HUD`
- **Hash:** `0xCC33FA791322B9D9`
- **JHash:** `0x216CB1C5`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_PRINTS()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_REMINDER_MESSAGE

- **命名空间:** `HUD`
- **Hash:** `0xB57D8DD645CFA2CF`
- **JHash:** `0x5BBCC934`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_REMINDER_MESSAGE()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
This native does absolutely nothing, just a nullsub
```

---

### CLEAR_SMALL_PRINTS

- **命名空间:** `HUD`
- **Hash:** `0x2CEA2839313C09AC`
- **JHash:** `0xA869A238`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_SMALL_PRINTS()
```

**返回值:** `void` (Native: `void`)

---

### CLEAR_THIS_PRINT

- **命名空间:** `HUD`
- **Hash:** `0xCF708001E1E536DD`
- **JHash:** `0x06878327`
- **Build:** `323`

**C# 签名:**
```csharp
void CLEAR_THIS_PRINT(string p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
p0: found arguments in the b617d scripts: https://pastebin.com/X5akCN7z
```

---

### CLEAR_VALID_VEHICLE_HIT_HASHES

- **命名空间:** `HUD`
- **Hash:** `0xEB81A3DADD503187`
- **Build:** `1290`

**C# 签名:**
```csharp
void CLEAR_VALID_VEHICLE_HIT_HASHES()
```

**返回值:** `void` (Native: `void`)

---

### CLOSE_MP_TEXT_CHAT

- **命名空间:** `HUD`
- **Hash:** `0x1AC8F4AD40E22127`
- **Build:** `323`

**C# 签名:**
```csharp
void CLOSE_MP_TEXT_CHAT()
```

**返回值:** `void` (Native: `void`)

---

### CLOSE_SOCIAL_CLUB_MENU

- **命名空间:** `HUD`
- **Hash:** `0xD2B32BE3FC1626C6`
- **JHash:** `0x5F86AA39`
- **Build:** `323`

**C# 签名:**
```csharp
void CLOSE_SOCIAL_CLUB_MENU()
```

**返回值:** `void` (Native: `void`)

---

### CODE_WANTS_SCRIPT_TO_TAKE_CONTROL

- **命名空间:** `HUD`
- **Hash:** `0x66E7CB63C97B7D20`
- **JHash:** `0x92DAFA78`
- **Build:** `323`

**C# 签名:**
```csharp
bool CODE_WANTS_SCRIPT_TO_TAKE_CONTROL()
```

**返回值:** `bool` (Native: `BOOL`)

---

### CREATE_FAKE_MP_GAMER_TAG

- **命名空间:** `HUD`
- **Hash:** `0xBFEFE3321A3F5015`
- **JHash:** `0xF5CD2AA4`
- **Build:** `323`

**C# 签名:**
```csharp
int CREATE_FAKE_MP_GAMER_TAG(int ped, string username, bool pointedClanTag, bool isRockstarClan, string clanTag, int clanFlag)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |
| `username` | `const char*` | `string` | - |
| `pointedClanTag` | `BOOL` | `bool` | - |
| `isRockstarClan` | `BOOL` | `bool` | - |
| `clanTag` | `const char*` | `string` | - |
| `clanFlag` | `int` | `int` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
clanFlag: takes a number 0-5
```

---

### CREATE_MOBILE_PHONE

- **命名空间:** `MOBILE`
- **Hash:** `0xA4E8E696C532FBC7`
- **JHash:** `0x5BBC5E23`
- **Build:** `323`

**C# 签名:**
```csharp
void CREATE_MOBILE_PHONE(int phoneType)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `phoneType` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Creates a mobile phone of the specified type.

Possible phone types:

0 - Default phone / Michael's phone
1 - Trevor's phone
2 - Franklin's phone
3 - Unused police phone
4 - Prologue phone

Higher values may crash your game.
```

---

### CREATE_MP_GAMER_TAG_WITH_CREW_COLOR

- **命名空间:** `HUD`
- **Hash:** `0x6DD05E9D83EFA4C9`
- **JHash:** `0xC969F2D0`
- **Build:** `323`

**C# 签名:**
```csharp
void CREATE_MP_GAMER_TAG_WITH_CREW_COLOR(int player, string username, bool pointedClanTag, bool isRockstarClan, string clanTag, int clanFlag, int r, int g, int b)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `player` | `Player` | `int` | - |
| `username` | `const char*` | `string` | - |
| `pointedClanTag` | `BOOL` | `bool` | - |
| `isRockstarClan` | `BOOL` | `bool` | - |
| `clanTag` | `const char*` | `string` | - |
| `clanFlag` | `int` | `int` | - |
| `r` | `int` | `int` | - |
| `g` | `int` | `int` | - |
| `b` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
clanFlag: takes a number 0-5
```

---

### CUSTOM_MINIMAP_CLEAR_BLIPS

- **命名空间:** `HUD`
- **Hash:** `0x2708FC083123F9FF`
- **JHash:** `0xCBEC9369`
- **Build:** `323`

**C# 签名:**
```csharp
void CUSTOM_MINIMAP_CLEAR_BLIPS()
```

**返回值:** `void` (Native: `void`)

---

### CUSTOM_MINIMAP_CREATE_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x551DF99658DB6EE8`
- **JHash:** `0x786CA0A2`
- **Build:** `323`

**C# 签名:**
```csharp
int CUSTOM_MINIMAP_CREATE_BLIP(float x, float y, float z)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `float` | `float` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
Add a BLIP_GALLERY at the specific coordinate. Used in fm_maintain_transition_players to display race track points.
```

---

### CUSTOM_MINIMAP_SET_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x5354C5BA2EA868A4`
- **JHash:** `0xE4FD20D8`
- **Build:** `323`

**C# 签名:**
```csharp
void CUSTOM_MINIMAP_SET_ACTIVE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
If toggle is true, the map is shown in full screen
If toggle is false, the map is shown in normal mode
```

---

### CUSTOM_MINIMAP_SET_BLIP_OBJECT

- **命名空间:** `HUD`
- **Hash:** `0x1EAE6DD17B7A5EFA`
- **JHash:** `0x13E7A5A9`
- **Build:** `323`

**C# 签名:**
```csharp
void CUSTOM_MINIMAP_SET_BLIP_OBJECT(int spriteId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `spriteId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets the sprite of the next BLIP_GALLERY blip, values used in the native scripts: 143 (ObjectiveBlue), 144 (ObjectiveGreen), 145 (ObjectiveRed), 146 (ObjectiveYellow).
```

---

### DELETE_WAYPOINTS_FROM_THIS_PLAYER

- **命名空间:** `HUD`
- **Hash:** `0xD8E694757BCEA8E9`
- **JHash:** `0x62BABF2C`
- **Build:** `323`

**C# 签名:**
```csharp
void DELETE_WAYPOINTS_FROM_THIS_PLAYER()
```

**返回值:** `void` (Native: `void`)

---

### DESTROY_MOBILE_PHONE

- **命名空间:** `MOBILE`
- **Hash:** `0x3BC861DF703E5097`
- **JHash:** `0x1A65037B`
- **Build:** `323`

**C# 签名:**
```csharp
void DESTROY_MOBILE_PHONE()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Destroys the currently active mobile phone.
```

---

### DISABLE_FRONTEND_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x6D3465A73092F0E6`
- **JHash:** `0xD86A029E`
- **Build:** `323`

**C# 签名:**
```csharp
void DISABLE_FRONTEND_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### DISABLE_PAUSEMENU_SPINNER

- **命名空间:** `HUD`
- **Hash:** `0x9245E81072704B8A`
- **Build:** `323`

**C# 签名:**
```csharp
void DISABLE_PAUSEMENU_SPINNER(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### DISPLAY_AMMO_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xA5E78BA2B1331C55`
- **JHash:** `0x60693CEE`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_AMMO_THIS_FRAME(bool display)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `display` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### DISPLAY_AREA_NAME

- **命名空间:** `HUD`
- **Hash:** `0x276B6CE369C33678`
- **JHash:** `0x489FDD41`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_AREA_NAME(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### DISPLAY_CASH

- **命名空间:** `HUD`
- **Hash:** `0x96DEC8D5430208B7`
- **JHash:** `0x0049DF83`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_CASH(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
"DISPLAY_CASH(false);" makes the cash amount render on the screen when appropriate
"DISPLAY_CASH(true);" disables cash amount rendering
```

---

### DISPLAY_HELP_TEXT_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x960C9FF8F616E41C`
- **JHash:** `0x18E3360A`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_HELP_TEXT_THIS_FRAME(string message, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `message` | `const char*` | `string` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
The messages are localized strings.
Examples:
"No_bus_money"
"Enter_bus"
"Tour_help"
"LETTERS_HELP2"
"Dummy"

**The bool appears to always be false (if it even is a bool, as it's represented by a zero)**
--------
p1 doesn't seem to make a difference, regardless of the state it's in. 


picture of where on the screen this is displayed? 
```

---

### DISPLAY_HUD

- **命名空间:** `HUD`
- **Hash:** `0xA6294919E56FF02A`
- **JHash:** `0xD10E4E31`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_HUD(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
If Hud should be displayed
```

---

### DISPLAY_HUD_WHEN_NOT_IN_STATE_OF_PLAY_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x7669F9E39DC17063`
- **JHash:** `0xC380AC85`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_HUD_WHEN_NOT_IN_STATE_OF_PLAY_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Enables drawing some hud components, such as help labels, this frame, when the player is dead.
```

---

### DISPLAY_HUD_WHEN_PAUSED_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x402F9ED62087E898`
- **JHash:** `0xC47AB1B0`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_HUD_WHEN_PAUSED_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### DISPLAY_PLAYER_NAME_TAGS_ON_BLIPS

- **命名空间:** `HUD`
- **Hash:** `0x82CEDC33687E1F50`
- **JHash:** `0x2F28F0A6`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_PLAYER_NAME_TAGS_ON_BLIPS(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Toggles whether or not name labels are shown on the expanded minimap next to player blips, like in GTA:O.
Doesn't need to be called every frame.
Preview: https://i.imgur.com/DfqKWfJ.png

Make sure to call SET_BLIP_CATEGORY with index 7 for this to work on the desired blip.
```

---

### DISPLAY_RADAR

- **命名空间:** `HUD`
- **Hash:** `0xA0EBB943C300E693`
- **JHash:** `0x52816BD4`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_RADAR(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
If Minimap / Radar should be displayed.
```

---

### DISPLAY_SNIPER_SCOPE_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x73115226F4814E62`
- **JHash:** `0xBC6C73CB`
- **Build:** `323`

**C# 签名:**
```csharp
void DISPLAY_SNIPER_SCOPE_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Displays the crosshair for this frame.
```

---

### DOES_BLIP_EXIST

- **命名空间:** `HUD`
- **Hash:** `0xA6DB27D19ECBB7DA`
- **JHash:** `0xAE92DD96`
- **Build:** `323`

**C# 签名:**
```csharp
bool DOES_BLIP_EXIST(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### DOES_BLIP_HAVE_GPS_ROUTE

- **命名空间:** `HUD`
- **Hash:** `0xDD2238F57B977751`
- **JHash:** `0x3E47F357`
- **Build:** `323`

**C# 签名:**
```csharp
bool DOES_BLIP_HAVE_GPS_ROUTE(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### DOES_PED_HAVE_AI_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x15B8ECF844EE67ED`
- **JHash:** `0x3BE1257F`
- **Build:** `323`

**C# 签名:**
```csharp
bool DOES_PED_HAVE_AI_BLIP(int ped)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### DOES_TEXT_BLOCK_EXIST

- **命名空间:** `HUD`
- **Hash:** `0x1C7302E725259789`
- **JHash:** `0x96F74838`
- **Build:** `323`

**C# 签名:**
```csharp
bool DOES_TEXT_BLOCK_EXIST(string gxt)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxt` | `const char*` | `string` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### DOES_TEXT_LABEL_EXIST

- **命名空间:** `HUD`
- **Hash:** `0xAC09CA973C564252`
- **JHash:** `0x6ECAE560`
- **Build:** `323`

**C# 签名:**
```csharp
bool DOES_TEXT_LABEL_EXIST(string gxt)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxt` | `const char*` | `string` | - |

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
Checks if the passed gxt name exists in the game files.
```

---

### DONT_TILT_MINIMAP_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x6D14BFDC33B34F55`
- **JHash:** `0x334EFD46`
- **Build:** `323`

**C# 签名:**
```csharp
void DONT_TILT_MINIMAP_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
When calling this, the current frame will have the players "arrow icon" be focused on the dead center of the radar.
```

---

### DONT_ZOOM_MINIMAP_WHEN_RUNNING_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x89DA85D949CE57A0`
- **Build:** `2802`

**C# 签名:**
```csharp
void DONT_ZOOM_MINIMAP_WHEN_RUNNING_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### DONT_ZOOM_MINIMAP_WHEN_SNIPING_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x55F5A5F07134DE60`
- **JHash:** `0x7A569F78`
- **Build:** `1180`

**C# 签名:**
```csharp
void DONT_ZOOM_MINIMAP_WHEN_SNIPING_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### DRAW_FRONTEND_BACKGROUND_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x211C4EF450086857`
- **JHash:** `0x801D0D86`
- **Build:** `323`

**C# 签名:**
```csharp
void DRAW_FRONTEND_BACKGROUND_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
This native does absolutely nothing, just a nullsub
```

---

### DRAW_HUD_OVER_FADE_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xBF4F34A85CA2970C`
- **JHash:** `0x317775DF`
- **Build:** `323`

**C# 签名:**
```csharp
void DRAW_HUD_OVER_FADE_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### END_TEXT_COMMAND_ADD_DIRECTLY_TO_PREVIOUS_BRIEFS

- **命名空间:** `HUD`
- **Hash:** `0xCFDBDF5AE59BA0F4`
- **JHash:** `0x2944A6C5`
- **Build:** `323`

**C# 签名:**
```csharp
void END_TEXT_COMMAND_ADD_DIRECTLY_TO_PREVIOUS_BRIEFS(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### END_TEXT_COMMAND_BUSYSPINNER_ON

- **命名空间:** `HUD`
- **Hash:** `0xBD12F8228410D9B4`
- **JHash:** `0x903F5EE4`
- **Build:** `323`

**C# 签名:**
```csharp
void END_TEXT_COMMAND_BUSYSPINNER_ON(int busySpinnerType)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `busySpinnerType` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
enum eBusySpinnerType
{
	BUSY_SPINNER_LEFT,
	BUSY_SPINNER_LEFT_2,
	BUSY_SPINNER_LEFT_3,
	BUSY_SPINNER_SAVE,
	BUSY_SPINNER_RIGHT,
};
```

---

### END_TEXT_COMMAND_CLEAR_PRINT

- **命名空间:** `HUD`
- **Hash:** `0xFCC75460ABA29378`
- **JHash:** `0x67785AF2`
- **Build:** `323`

**C# 签名:**
```csharp
void END_TEXT_COMMAND_CLEAR_PRINT()
```

**返回值:** `void` (Native: `void`)

---

### END_TEXT_COMMAND_DISPLAY_HELP

- **命名空间:** `HUD`
- **Hash:** `0x238FFE5C7B0498A6`
- **JHash:** `0xB59B530D`
- **Build:** `323`

**C# 签名:**
```csharp
void END_TEXT_COMMAND_DISPLAY_HELP(int p0, bool loop, bool beep, int shape)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |
| `loop` | `BOOL` | `bool` | - |
| `beep` | `BOOL` | `bool` | - |
| `shape` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
shape goes from -1 to 50 (may be more).
p0 is always 0.

Example:
void FloatingHelpText(const char* text)
{
    BEGIN_TEXT_COMMAND_DISPLAY_HELP("STRING");
  ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
   END_TEXT_COMMAND_DISPLAY_HELP (0, 0, 1, -1);
}

Image:
- imgbin.org/images/26209.jpg

more inputs/icons:
- https://pastebin.com/nqNYWMSB

Used to be known as _DISPLAY_HELP_TEXT_FROM_STRING_LABEL
```

---

### END_TEXT_COMMAND_DISPLAY_TEXT

- **命名空间:** `HUD`
- **Hash:** `0xCD015E5BB0D96A57`
- **JHash:** `0x6F8350CE`
- **Build:** `323`

**C# 签名:**
```csharp
void END_TEXT_COMMAND_DISPLAY_TEXT(float x, float y, int p2)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `p2` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
After applying the properties to the text (See HUD::SET_TEXT_), this will draw the text in the applied position. Also 0.0f < x, y < 1.0f, percentage of the axis.

Used to be known as _DRAW_TEXT
```

---

### END_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING

- **命名空间:** `HUD`
- **Hash:** `0x9040DFB09BE75706`
- **JHash:** `0xAA318785`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_GET_NUMBER_OF_LINES_FOR_STRING(float x, float y)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
Determines how many lines the text string will use when drawn on screen. 
Must use 0x521FB041D93DD0E4 for setting up
```

---

### END_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT

- **命名空间:** `HUD`
- **Hash:** `0x85F061DA64ED2F67`
- **JHash:** `0xD12A643A`
- **Build:** `323`

**C# 签名:**
```csharp
float END_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `float` (Native: `float`)

---

### END_TEXT_COMMAND_IS_MESSAGE_DISPLAYED

- **命名空间:** `HUD`
- **Hash:** `0x8A9BA1AB3E237613`
- **JHash:** `0x672EFB45`
- **Build:** `323`

**C# 签名:**
```csharp
bool END_TEXT_COMMAND_IS_MESSAGE_DISPLAYED()
```

**返回值:** `bool` (Native: `BOOL`)

---

### END_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED

- **命名空间:** `HUD`
- **Hash:** `0x10BDDBFC529428DD`
- **JHash:** `0xF63A13EC`
- **Build:** `323`

**C# 签名:**
```csharp
bool END_TEXT_COMMAND_IS_THIS_HELP_MESSAGE_BEING_DISPLAYED(int p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### END_TEXT_COMMAND_OVERRIDE_BUTTON_TEXT

- **命名空间:** `HUD`
- **Hash:** `0xA86911979638106F`
- **JHash:** `0x6E7FDA1C`
- **Build:** `323`

**C# 签名:**
```csharp
void END_TEXT_COMMAND_OVERRIDE_BUTTON_TEXT(int p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### END_TEXT_COMMAND_PRINT

- **命名空间:** `HUD`
- **Hash:** `0x9D77056A530643F6`
- **JHash:** `0x38F82261`
- **Build:** `323`

**C# 签名:**
```csharp
void END_TEXT_COMMAND_PRINT(int duration, bool drawImmediately)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `duration` | `int` | `int` | - |
| `drawImmediately` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Draws the subtitle at middle center of the screen.

int duration = time in milliseconds to show text on screen before disappearing

drawImmediately = If true, the text will be drawn immediately, if false, the text will be drawn after the previous subtitle has finished

Used to be known as _DRAW_SUBTITLE_TIMED
```

---

### END_TEXT_COMMAND_SET_BLIP_NAME

- **命名空间:** `HUD`
- **Hash:** `0xBC38B49BCB83BC9B`
- **JHash:** `0xE8E59820`
- **Build:** `323`

**C# 签名:**
```csharp
void END_TEXT_COMMAND_SET_BLIP_NAME(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Finalizes a text command started with BEGIN_TEXT_COMMAND_SET_BLIP_NAME, setting the name of the specified blip.
```

---

### END_TEXT_COMMAND_THEFEED_POST_AWARD

- **命名空间:** `HUD`
- **Hash:** `0xAA295B6F28BD587D`
- **JHash:** `0x02DED2B8`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_AWARD(string textureDict, string textureName, int rpBonus, int colorOverlay, string titleLabel)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `textureDict` | `const char*` | `string` | - |
| `textureName` | `const char*` | `string` | - |
| `rpBonus` | `int` | `int` | - |
| `colorOverlay` | `int` | `int` | - |
| `titleLabel` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
Shows an "award" notification above the minimap, example: https://i.imgur.com/e2DNaKX.png
Example:

HUD::BEGIN_TEXT_COMMAND_THEFEED_POST("HUNT");
HUD::END_TEXT_COMMAND_THEFEED_POST_AWARD("Hunting", "Hunting_Gold_128", 0, 109, "HUD_MED_UNLKED");
```

---

### END_TEXT_COMMAND_THEFEED_POST_CREWTAG

- **命名空间:** `HUD`
- **Hash:** `0x97C9E4E7024A8F2C`
- **JHash:** `0xA9CCEF66`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_CREWTAG(bool p0, bool p1, ref int p2, int p3, bool isLeader, bool unk0, int clanDesc, int R, int G, int B)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |
| `p1` | `BOOL` | `bool` | - |
| `p2` | `int*` | `ref int` | - |
| `p3` | `int` | `int` | - |
| `isLeader` | `BOOL` | `bool` | - |
| `unk0` | `BOOL` | `bool` | - |
| `clanDesc` | `int` | `int` | - |
| `R` | `int` | `int` | - |
| `G` | `int` | `int` | - |
| `B` | `int` | `int` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_CREWTAG_WITH_GAME_NAME

- **命名空间:** `HUD`
- **Hash:** `0x137BC35589E34E1E`
- **JHash:** `0x88B9B909`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_CREWTAG_WITH_GAME_NAME(bool p0, bool p1, ref int p2, int p3, bool isLeader, bool unk0, int clanDesc, string playerName, int R, int G, int B)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |
| `p1` | `BOOL` | `bool` | - |
| `p2` | `int*` | `ref int` | - |
| `p3` | `int` | `int` | - |
| `isLeader` | `BOOL` | `bool` | - |
| `unk0` | `BOOL` | `bool` | - |
| `clanDesc` | `int` | `int` | - |
| `playerName` | `const char*` | `string` | - |
| `R` | `int` | `int` | - |
| `G` | `int` | `int` | - |
| `B` | `int` | `int` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_CREW_RANKUP_WITH_LITERAL_FLAG

- **命名空间:** `HUD`
- **Hash:** `0x8EFCCF6EC66D85E4`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_CREW_RANKUP_WITH_LITERAL_FLAG(string p0, string p1, string p2, bool p3, bool p4)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `const char*` | `string` | - |
| `p1` | `const char*` | `string` | - |
| `p2` | `const char*` | `string` | - |
| `p3` | `BOOL` | `bool` | - |
| `p4` | `BOOL` | `bool` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT

- **命名空间:** `HUD`
- **Hash:** `0x1CCD9A37359072CF`
- **JHash:** `0xE7E3C98B`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(string txdName, string textureName, bool flash, int iconType, string sender, string subject)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `txdName` | `const char*` | `string` | - |
| `textureName` | `const char*` | `string` | - |
| `flash` | `BOOL` | `bool` | - |
| `iconType` | `int` | `int` | - |
| `sender` | `const char*` | `string` | - |
| `subject` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_DICT.

List of picNames: https://pastebin.com/XdpJVbHz


flash is a bool for fading in.
iconTypes:
1 : Chat Box
2 : Email
3 : Add Friend Request
4 : Nothing
5 : Nothing
6 : Nothing
7 : Right Jumping Arrow
8 : RP Icon
9 : $ Icon

"sender" is the very top header. This can be any old string.
"subject" is the header under the sender.
```

---

### END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_SUBTITLE_LABEL

- **命名空间:** `HUD`
- **Hash:** `0xC6F580E4C94926AC`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_SUBTITLE_LABEL(string txdName, string textureName, bool flash, int iconType, string sender, string subject)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `txdName` | `const char*` | `string` | - |
| `textureName` | `const char*` | `string` | - |
| `flash` | `BOOL` | `bool` | - |
| `iconType` | `int` | `int` | - |
| `sender` | `const char*` | `string` | - |
| `subject` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_DICT.

Needs more research.

Only one type of usage in the scripts:

HUD::END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_SUBTITLE_LABEL("CHAR_ACTING_UP", "CHAR_ACTING_UP", 0, 0, "DI_FEED_CHAR", a_0);
```

---

### END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_TU

- **命名空间:** `HUD`
- **Hash:** `0x1E6611149DB3DB6B`
- **JHash:** `0x0EB382B7`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_TU(string txdName, string textureName, bool flash, int iconType, string sender, string subject, float duration)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `txdName` | `const char*` | `string` | - |
| `textureName` | `const char*` | `string` | - |
| `flash` | `BOOL` | `bool` | - |
| `iconType` | `int` | `int` | - |
| `sender` | `const char*` | `string` | - |
| `subject` | `const char*` | `string` | - |
| `duration` | `float` | `float` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_DICT.

NOTE: 'duration' is a multiplier, so 1.0 is normal, 2.0 is twice as long (very slow), and 0.5 is half as long.

Example, only occurrence in the scripts:
v_8 = HUD::END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_TU("CHAR_SOCIAL_CLUB", "CHAR_SOCIAL_CLUB", 0, 0, &v_9, "", a_5);
```

---

### END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_WITH_CREW_TAG

- **命名空间:** `HUD`
- **Hash:** `0x5CBF7BADE20DB93E`
- **JHash:** `0x3E807FE3`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_WITH_CREW_TAG(string txdName, string textureName, bool flash, int iconType, string sender, string subject, float duration, string clanTag)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `txdName` | `const char*` | `string` | - |
| `textureName` | `const char*` | `string` | - |
| `flash` | `BOOL` | `bool` | - |
| `iconType` | `int` | `int` | - |
| `sender` | `const char*` | `string` | - |
| `subject` | `const char*` | `string` | - |
| `duration` | `float` | `float` | - |
| `clanTag` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_DICT.

List of picNames https://pastebin.com/XdpJVbHz

flash is a bool for fading in.
iconTypes:
1 : Chat Box
2 : Email
3 : Add Friend Request
4 : Nothing
5 : Nothing
6 : Nothing
7 : Right Jumping Arrow
8 : RP Icon
9 : $ Icon

"sender" is the very top header. This can be any old string.
"subject" is the header under the sender.
"duration" is a multiplier, so 1.0 is normal, 2.0 is twice as long (very slow), and 0.5 is half as long.
"clanTag" shows a crew tag in the "sender" header, after the text. You need to use 3 underscores as padding. Maximum length of this field seems to be 7. (e.g. "MK" becomes "___MK", "ACE" becomes "___ACE", etc.)
```

---

### END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_WITH_CREW_TAG_AND_ADDITIONAL_ICON

- **命名空间:** `HUD`
- **Hash:** `0x531B84E7DA981FB6`
- **JHash:** `0xDEB491C8`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT_WITH_CREW_TAG_AND_ADDITIONAL_ICON(string txdName, string textureName, bool flash, int iconType1, string sender, string subject, float duration, string clanTag, int iconType2, int p9)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `txdName` | `const char*` | `string` | - |
| `textureName` | `const char*` | `string` | - |
| `flash` | `BOOL` | `bool` | - |
| `iconType1` | `int` | `int` | - |
| `sender` | `const char*` | `string` | - |
| `subject` | `const char*` | `string` | - |
| `duration` | `float` | `float` | - |
| `clanTag` | `const char*` | `string` | - |
| `iconType2` | `int` | `int` | - |
| `p9` | `int` | `int` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_DICT.

List of picNames:  https://pastebin.com/XdpJVbHz

flash is a bool for fading in.
iconTypes:
1 : Chat Box
2 : Email
3 : Add Friend Request
4 : Nothing
5 : Nothing
6 : Nothing
7 : Right Jumping Arrow
8 : RP Icon
9 : $ Icon

"sender" is the very top header. This can be any old string.
"subject" is the header under the sender.
"duration" is a multiplier, so 1.0 is normal, 2.0 is twice as long (very slow), and 0.5 is half as long.
"clanTag" shows a crew tag in the "sender" header, after the text. You need to use 3 underscores as padding. Maximum length of this field seems to be 7. (e.g. "MK" becomes "___MK", "ACE" becomes "___ACE", etc.)
iconType2 is a mirror of iconType. It shows in the "subject" line, right under the original iconType.


int IconNotification(char *text, char *text2, char *Subject)
{
    BEGIN_TEXT_COMMAND_THEFEED_POST("STRING");
 ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
   _SET_NOTIFICATION_MESSAGE_CLAN_TAG_2("CHAR_SOCIAL_CLUB", "CHAR_SOCIAL_CLUB", 1, 7, text2, Subject, 1.0f, "__EXAMPLE", 7);
   return END_TEXT_COMMAND_THEFEED_POST_TICKER(1, 1);
}
```

---

### END_TEXT_COMMAND_THEFEED_POST_MPTICKER

- **命名空间:** `HUD`
- **Hash:** `0xF020C96915705B3A`
- **JHash:** `0x8E319AB8`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_MPTICKER(bool blink, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blink` | `BOOL` | `bool` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_REPLAY

- **命名空间:** `HUD`
- **Hash:** `0xD202B92CBF1D816F`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_REPLAY(int type, int image, string text)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `type` | `int` | `int` | - |
| `image` | `int` | `int` | - |
| `text` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
returns a notification handle, prints out a notification like below:
type range: 0 - 2
if you set type to 1, image goes from 0 - 39 - Xbox you can add text to

example: 
HUD::END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT(1, 20, "Who you trynna get crazy with, ese? Don't you know I'm LOCO?!");
- https://imgur.com/lGBPCz3
```

---

### END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT

- **命名空间:** `HUD`
- **Hash:** `0xDD6CB2CCE7C2735C`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT(int type, string button, string text)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `type` | `int` | `int` | - |
| `button` | `const char*` | `string` | - |
| `text` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
returns a notification handle, prints out a notification like below:
type range: 0 - 2
if you set type to 1, button accepts "~INPUT_SOMETHING~"

example:
HUD::END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT(1, "~INPUT_TALK~", "Who you trynna get crazy with, ese? Don't you know I'm LOCO?!");
- https://imgur.com/UPy0Ial


Examples from the scripts:
l_D1[1/*1*/]=HUD::END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT(1,"~INPUT_REPLAY_START_STOP_RECORDING~","");
l_D1[2/*1*/]=HUD::END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT(1,"~INPUT_SAVE_REPLAY_CLIP~","");
l_D1[1/*1*/]=HUD::END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT(1,"~INPUT_REPLAY_START_STOP_RECORDING~","");
l_D1[2/*1*/]=HUD::END_TEXT_COMMAND_THEFEED_POST_REPLAY_INPUT(1,"~INPUT_REPLAY_START_STOP_RECORDING_SECONDARY~","");

```

---

### END_TEXT_COMMAND_THEFEED_POST_STATS

- **命名空间:** `HUD`
- **Hash:** `0x2B7E9A4EAAA93C89`
- **JHash:** `0xED130FA1`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_STATS(string statTitle, int iconEnum, bool stepVal, int barValue, bool isImportant, string pictureTextureDict, string pictureTextureName)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `statTitle` | `const char*` | `string` | - |
| `iconEnum` | `int` | `int` | - |
| `stepVal` | `BOOL` | `bool` | - |
| `barValue` | `int` | `int` | - |
| `isImportant` | `BOOL` | `bool` | - |
| `pictureTextureDict` | `const char*` | `string` | - |
| `pictureTextureName` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
List of picture names: https://pastebin.com/XdpJVbHz
Example result: https://i.imgur.com/SdEZ22m.png
```

---

### END_TEXT_COMMAND_THEFEED_POST_TICKER

- **命名空间:** `HUD`
- **Hash:** `0x2ED7843F8F801023`
- **JHash:** `0x08F7AF78`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_TICKER(bool blink, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blink` | `BOOL` | `bool` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_TICKER_FORCED

- **命名空间:** `HUD`
- **Hash:** `0x44FA03975424A0EE`
- **JHash:** `0x57B8D0D4`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_TICKER_FORCED(bool blink, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blink` | `BOOL` | `bool` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_TICKER_WITH_TOKENS

- **命名空间:** `HUD`
- **Hash:** `0x378E809BF61EC840`
- **JHash:** `0x02BCAF9B`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_TICKER_WITH_TOKENS(bool blink, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blink` | `BOOL` | `bool` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_UNLOCK

- **命名空间:** `HUD`
- **Hash:** `0x33EE12743CCD6343`
- **JHash:** `0xE05E7052`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_UNLOCK(string gxtLabel1, int p1, string gxtLabel2)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxtLabel1` | `const char*` | `string` | - |
| `p1` | `int` | `int` | - |
| `gxtLabel2` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_UNLOCK_TU

- **命名空间:** `HUD`
- **Hash:** `0xC8F3AAF93D0600BF`
- **JHash:** `0x4FA43BA4`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_UNLOCK_TU(string gxtLabel1, int p1, string gxtLabel2, int p3)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxtLabel1` | `const char*` | `string` | - |
| `p1` | `int` | `int` | - |
| `gxtLabel2` | `const char*` | `string` | - |
| `p3` | `int` | `int` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_UNLOCK_TU_WITH_COLOR

- **命名空间:** `HUD`
- **Hash:** `0x7AE0589093A2E088`
- **JHash:** `0x8C90D22F`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_UNLOCK_TU_WITH_COLOR(object p0, object p1, object p2, object p3, object p4, object p5)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |
| `p1` | `Any` | `object` | - |
| `p2` | `Any` | `object` | - |
| `p3` | `Any` | `object` | - |
| `p4` | `Any` | `object` | - |
| `p5` | `Any` | `object` | - |

**返回值:** `int` (Native: `int`)

---

### END_TEXT_COMMAND_THEFEED_POST_VERSUS_TU

- **命名空间:** `HUD`
- **Hash:** `0xB6871B0555B02996`
- **JHash:** `0x5E93FBFA`
- **Build:** `323`

**C# 签名:**
```csharp
int END_TEXT_COMMAND_THEFEED_POST_VERSUS_TU(string txdName1, string textureName1, int count1, string txdName2, string textureName2, int count2, int hudColor1, int hudColor2)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `txdName1` | `const char*` | `string` | - |
| `textureName1` | `const char*` | `string` | - |
| `count1` | `int` | `int` | - |
| `txdName2` | `const char*` | `string` | - |
| `textureName2` | `const char*` | `string` | - |
| `count2` | `int` | `int` | - |
| `hudColor1` | `int` | `int` | - |
| `hudColor2` | `int` | `int` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
This function can show pictures of every texture that can be requested by REQUEST_STREAMED_TEXTURE_DICT.

List of picNames: https://pastebin.com/XdpJVbHz
HUD colors and their values: https://pastebin.com/d9aHPbXN

Shows a deathmatch score above the minimap, example: https://i.imgur.com/YmoMklG.png
```

---

### FLAG_PLAYER_CONTEXT_IN_TOURNAMENT

- **命名空间:** `HUD`
- **Hash:** `0xCEF214315D276FD1`
- **JHash:** `0xFF06772A`
- **Build:** `323`

**C# 签名:**
```csharp
void FLAG_PLAYER_CONTEXT_IN_TOURNAMENT(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### FLASH_ABILITY_BAR

- **命名空间:** `HUD`
- **Hash:** `0x02CFBA0C9E9275CE`
- **JHash:** `0x3648960D`
- **Build:** `323`

**C# 签名:**
```csharp
void FLASH_ABILITY_BAR(int millisecondsToFlash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `millisecondsToFlash` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### FLASH_MINIMAP_DISPLAY

- **命名空间:** `HUD`
- **Hash:** `0xF2DD778C22B15BDA`
- **JHash:** `0xB8359952`
- **Build:** `323`

**C# 签名:**
```csharp
void FLASH_MINIMAP_DISPLAY()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
adds a short flash to the Radar/Minimap
Usage: UI.FLASH_MINIMAP_DISPLAY
```

---

### FLASH_MINIMAP_DISPLAY_WITH_COLOR

- **命名空间:** `HUD`
- **Hash:** `0x6B1DE27EE78E6A19`
- **JHash:** `0x79A6CAF6`
- **Build:** `323`

**C# 签名:**
```csharp
void FLASH_MINIMAP_DISPLAY_WITH_COLOR(int hudColorIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudColorIndex` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### FLASH_WANTED_DISPLAY

- **命名空间:** `HUD`
- **Hash:** `0xA18AFB39081B6A1F`
- **JHash:** `0x629F866B`
- **Build:** `323`

**C# 签名:**
```csharp
void FLASH_WANTED_DISPLAY(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### FORCE_CLOSE_REPORTUGC_MENU

- **命名空间:** `HUD`
- **Hash:** `0xEE4C0E6DBC6F2C6F`
- **JHash:** `0x67649EE0`
- **Build:** `323`

**C# 签名:**
```csharp
void FORCE_CLOSE_REPORTUGC_MENU()
```

**返回值:** `void` (Native: `void`)

---

### FORCE_CLOSE_TEXT_INPUT_BOX

- **命名空间:** `HUD`
- **Hash:** `0x8817605C2BA76200`
- **Build:** `323`

**C# 签名:**
```csharp
void FORCE_CLOSE_TEXT_INPUT_BOX()
```

**返回值:** `void` (Native: `void`)

---

### FORCE_NEXT_MESSAGE_TO_PREVIOUS_BRIEFS_LIST

- **命名空间:** `HUD`
- **Hash:** `0x57D760D55F54E071`
- **JHash:** `0x9854485F`
- **Build:** `323`

**C# 签名:**
```csharp
void FORCE_NEXT_MESSAGE_TO_PREVIOUS_BRIEFS_LIST(int p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### FORCE_OFF_WANTED_STAR_FLASH

- **命名空间:** `HUD`
- **Hash:** `0xBA8D65C1C65702E5`
- **JHash:** `0x58612465`
- **Build:** `323`

**C# 签名:**
```csharp
void FORCE_OFF_WANTED_STAR_FLASH(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### FORCE_SCRIPTED_GFX_WHEN_FRONTEND_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x2162C446DFDF38FD`
- **JHash:** `0x2DFD35C7`
- **Build:** `323`

**C# 签名:**
```csharp
void FORCE_SCRIPTED_GFX_WHEN_FRONTEND_ACTIVE(string p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Not present in retail version of the game, actual definiton seems to be
_LOG_DEBUG_INFO(const char* category, const char* debugText);
```

---

### FORCE_SONAR_BLIPS_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x1121BFA1A1A522A8`
- **JHash:** `0x3F4AFB13`
- **Build:** `323`

**C# 签名:**
```csharp
bool FORCE_SONAR_BLIPS_THIS_FRAME()
```

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
Doesn't actually return anything.
```

---

### GET_AI_PED_PED_BLIP_INDEX

- **命名空间:** `HUD`
- **Hash:** `0x7CD934010E115C2C`
- **JHash:** `0x06349065`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_AI_PED_PED_BLIP_INDEX(int ped)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |

**返回值:** `int` (Native: `Blip`)

---

### GET_AI_PED_VEHICLE_BLIP_INDEX

- **命名空间:** `HUD`
- **Hash:** `0x56176892826A4FE8`
- **JHash:** `0xCA52CF43`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_AI_PED_VEHICLE_BLIP_INDEX(int ped)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |

**返回值:** `int` (Native: `Blip`)

**说明:**
```
Returns the current AI BLIP for the specified ped
```

---

### GET_BLIP_ALPHA

- **命名空间:** `HUD`
- **Hash:** `0x970F608F0EE6C885`
- **JHash:** `0x297AF6C8`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_BLIP_ALPHA(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `int`)

---

### GET_BLIP_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0xDF729E8D20CF7327`
- **JHash:** `0xDD6A1E54`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_BLIP_COLOUR(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `int`)

---

### GET_BLIP_COORDS

- **命名空间:** `HUD`
- **Hash:** `0x586AFE3FF72D996E`
- **JHash:** `0xEF6FF47B`
- **Build:** `323`

**C# 签名:**
```csharp
Vector3 GET_BLIP_COORDS(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `Vector3` (Native: `Vector3`)

---

### GET_BLIP_FADE_DIRECTION

- **命名空间:** `HUD`
- **Hash:** `0x2C173AE2BDB9385E`
- **Build:** `463`

**C# 签名:**
```csharp
int GET_BLIP_FADE_DIRECTION(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
Returns -1, 0, +1, depending on if the blip is fading out, doing nothing, or fading in respectively.
```

---

### GET_BLIP_FROM_ENTITY

- **命名空间:** `HUD`
- **Hash:** `0xBC8DBDCA2436F7E8`
- **JHash:** `0x005A2A47`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_BLIP_FROM_ENTITY(int entity)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `entity` | `Entity` | `int` | - |

**返回值:** `int` (Native: `Blip`)

**说明:**
```
Returns the Blip handle of given Entity.
```

---

### GET_BLIP_HUD_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x729B5F1EFBC0AAEE`
- **JHash:** `0xE88B4BC2`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_BLIP_HUD_COLOUR(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `int`)

---

### GET_BLIP_INFO_ID_COORD

- **命名空间:** `HUD`
- **Hash:** `0xFA7C7F0AADF25D09`
- **JHash:** `0xB7374A66`
- **Build:** `323`

**C# 签名:**
```csharp
Vector3 GET_BLIP_INFO_ID_COORD(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `Vector3` (Native: `Vector3`)

---

### GET_BLIP_INFO_ID_DISPLAY

- **命名空间:** `HUD`
- **Hash:** `0x1E314167F701DC3B`
- **JHash:** `0xD0FC19F4`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_BLIP_INFO_ID_DISPLAY(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `int`)

---

### GET_BLIP_INFO_ID_ENTITY_INDEX

- **命名空间:** `HUD`
- **Hash:** `0x4BA4E2553AFEDC2C`
- **JHash:** `0xA068C40B`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_BLIP_INFO_ID_ENTITY_INDEX(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `Entity`)

---

### GET_BLIP_INFO_ID_PICKUP_INDEX

- **命名空间:** `HUD`
- **Hash:** `0x9B6786E4C03DD382`
- **JHash:** `0x86913D37`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_BLIP_INFO_ID_PICKUP_INDEX(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `Pickup`)

**说明:**
```
This function is hard-coded to always return 0.
```

---

### GET_BLIP_INFO_ID_TYPE

- **命名空间:** `HUD`
- **Hash:** `0xBE9B0959FFD0779B`
- **JHash:** `0x501D7B4E`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_BLIP_INFO_ID_TYPE(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
Returns a value based on what the blip is attached to
1 - Vehicle
2 - Ped
3 - Object
4 - Coord
5 - unk
6 - Pickup
7 - Radius
```

---

### GET_BLIP_ROTATION

- **命名空间:** `HUD`
- **Hash:** `0x003E92BA477F9D7F`
- **Build:** `2060`

**C# 签名:**
```csharp
int GET_BLIP_ROTATION(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `int`)

---

### GET_BLIP_SPRITE

- **命名空间:** `HUD`
- **Hash:** `0x1FC877464A04FC4F`
- **JHash:** `0x72FF2E73`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_BLIP_SPRITE(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
Blips Images + IDs:
gtaxscripting.blogspot.com/2016/05/gta-v-blips-id-and-image.html
```

---

### GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME

- **命名空间:** `HUD`
- **Hash:** `0x169BD9382084C8C0`
- **JHash:** `0x34A396EE`
- **Build:** `323`

**C# 签名:**
```csharp
string GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME(string text, int position, int length)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `text` | `const char*` | `string` | - |
| `position` | `int` | `int` | - |
| `length` | `int` | `int` | - |

**返回值:** `string` (Native: `const char*`)

**说明:**
```
Returns a substring of a specified length starting at a specified position.

Example:
// Get "STRING" text from "MY_STRING"
subStr = HUD::GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME("MY_STRING", 3, 6);
```

---

### GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_BYTES

- **命名空间:** `HUD`
- **Hash:** `0xCE94AEBA5D82908A`
- **JHash:** `0xFA6373BB`
- **Build:** `323`

**C# 签名:**
```csharp
string GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_BYTES(string text, int startPosition, int endPosition)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `text` | `const char*` | `string` | - |
| `startPosition` | `int` | `int` | - |
| `endPosition` | `int` | `int` | - |

**返回值:** `string` (Native: `const char*`)

**说明:**
```
Returns a substring that is between two specified positions. The length of the string will be calculated using (endPosition - startPosition).

Example:
// Get "STRING" text from "MY_STRING"
subStr = HUD::GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_BYTES("MY_STRING", 3, 9);
// Overflows are possibly replaced with underscores (needs verification)
subStr = HUD::GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_BYTES("MY_STRING", 3, 10); // "STRING_"?
```

---

### GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_WITH_BYTE_LIMIT

- **命名空间:** `HUD`
- **Hash:** `0xB2798643312205C5`
- **JHash:** `0x0183A66C`
- **Build:** `323`

**C# 签名:**
```csharp
string GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_WITH_BYTE_LIMIT(string text, int position, int length, int maxLength)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `text` | `const char*` | `string` | - |
| `position` | `int` | `int` | - |
| `length` | `int` | `int` | - |
| `maxLength` | `int` | `int` | - |

**返回值:** `string` (Native: `const char*`)

**说明:**
```
Returns a substring of a specified length starting at a specified position. The result is guaranteed not to exceed the specified max length.

NOTE: The 'maxLength' parameter might actually be the size of the buffer that is returned. More research is needed. -CL69

Example:
// Condensed example of how Rockstar uses this function
strLen = HUD::GET_LENGTH_OF_LITERAL_STRING(MISC::GET_ONSCREEN_KEYBOARD_RESULT());
subStr = HUD::GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_WITH_BYTE_LIMIT(MISC::GET_ONSCREEN_KEYBOARD_RESULT(), 0, strLen, 63);

--

"fm_race_creator.ysc", line 85115:
// parameters modified for clarity
BOOL sub_8e5aa(char *text, int length) {
    for (i = 0; i <= (length - 2); i += 1) {
        if (!MISC::ARE_STRINGS_EQUAL(HUD::GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME_WITH_BYTE_LIMIT(text, i, i + 1, 1), " ")) {
            return FALSE;
        }
    }
    return TRUE;
}
```

---

### GET_CHARACTER_MENU_PED_FLOAT_STAT

- **命名空间:** `HUD`
- **Hash:** `0x8F08017F9D7C47BD`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_CHARACTER_MENU_PED_FLOAT_STAT(float statHash, ref float outValue, bool p2)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `statHash` | `float` | `float` | - |
| `outValue` | `float*` | `ref float` | - |
| `p2` | `BOOL` | `bool` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### GET_CHARACTER_MENU_PED_INT_STAT

- **命名空间:** `HUD`
- **Hash:** `0xCA6B2F7CE32AB653`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_CHARACTER_MENU_PED_INT_STAT(object p0, ref object p1, object p2)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |
| `p1` | `Any*` | `ref object` | - |
| `p2` | `Any` | `object` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### GET_CHARACTER_MENU_PED_MASKED_INT_STAT

- **命名空间:** `HUD`
- **Hash:** `0x24A49BEAF468DC90`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_CHARACTER_MENU_PED_MASKED_INT_STAT(uint statHash, ref object outValue, int p2, int mask, bool p4)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `statHash` | `Hash` | `uint` | - |
| `outValue` | `Any*` | `ref object` | - |
| `p2` | `int` | `int` | - |
| `mask` | `int` | `int` | - |
| `p4` | `BOOL` | `bool` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### GET_CLOSEST_BLIP_INFO_ID

- **命名空间:** `HUD`
- **Hash:** `0xD484BF71050CA1EE`
- **Build:** `1180`

**C# 签名:**
```csharp
int GET_CLOSEST_BLIP_INFO_ID(int blipSprite)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blipSprite` | `int` | `int` | - |

**返回值:** `int` (Native: `Blip`)

---

### GET_CURRENT_FRONTEND_MENU_VERSION

- **命名空间:** `HUD`
- **Hash:** `0x2309595AD6145265`
- **JHash:** `0x33D6868F`
- **Build:** `323`

**C# 签名:**
```csharp
uint GET_CURRENT_FRONTEND_MENU_VERSION()
```

**返回值:** `uint` (Native: `Hash`)

**说明:**
```
if (HUD::GET_CURRENT_FRONTEND_MENU_VERSION() == joaat("fe_menu_version_empty_no_background"))
```

---

### GET_CURRENT_WEBPAGE_ID

- **命名空间:** `HUD`
- **Hash:** `0x01A358D9128B7A86`
- **JHash:** `0xAB5B7C18`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_CURRENT_WEBPAGE_ID()
```

**返回值:** `int` (Native: `int`)

---

### GET_CURRENT_WEBSITE_ID

- **命名空间:** `HUD`
- **Hash:** `0x97D47996FC48CBAD`
- **JHash:** `0x42A55B14`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_CURRENT_WEBSITE_ID()
```

**返回值:** `int` (Native: `int`)

---

### GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID

- **命名空间:** `HUD`
- **Hash:** `0x52F0982D7FD156B6`
- **JHash:** `0x8188935F`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID()
```

**返回值:** `int` (Native: `int`)

**说明:**
```
This function is hard-coded to always return 1.
```

---

### GET_FAKE_SPECTATOR_MODE

- **命名空间:** `HUD`
- **Hash:** `0xC2D2AD9EAAE265B8`
- **Build:** `505`

**C# 签名:**
```csharp
bool GET_FAKE_SPECTATOR_MODE()
```

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
Getter for SET_FAKE_SPECTATOR_MODE
```

---

### GET_FILENAME_FOR_AUDIO_CONVERSATION

- **命名空间:** `HUD`
- **Hash:** `0x7B5280EBA9840C72`
- **JHash:** `0x95C4B5AD`
- **Build:** `323`

**C# 签名:**
```csharp
string GET_FILENAME_FOR_AUDIO_CONVERSATION(string labelName)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `labelName` | `const char*` | `string` | - |

**返回值:** `string` (Native: `const char*`)

**说明:**
```
Gets a localized string literal from a label name. Can be used for output of e.g. VEHICLE::GET_LIVERY_NAME. To check if a GXT label can be localized with this, HUD::DOES_TEXT_LABEL_EXIST can be used.
```

---

### GET_FIRST_BLIP_INFO_ID

- **命名空间:** `HUD`
- **Hash:** `0x1BEDE233E6CD2A1F`
- **JHash:** `0x64C0273D`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_FIRST_BLIP_INFO_ID(int blipSprite)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blipSprite` | `int` | `int` | - |

**返回值:** `int` (Native: `Blip`)

---

### GET_FIRST_N_CHARACTERS_OF_LITERAL_STRING

- **命名空间:** `HUD`
- **Hash:** `0x98C3CF913D895111`
- **Build:** `505`

**C# 签名:**
```csharp
string GET_FIRST_N_CHARACTERS_OF_LITERAL_STRING(string string, int length)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `string` | `const char*` | `string` | - |
| `length` | `int` | `int` | - |

**返回值:** `string` (Native: `const char*`)

---

### GET_GLOBAL_ACTIONSCRIPT_FLAG

- **命名空间:** `HUD`
- **Hash:** `0xE3B05614DCE1D014`
- **JHash:** `0xD217EE7E`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_GLOBAL_ACTIONSCRIPT_FLAG(int flagIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `flagIndex` | `int` | `int` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
Returns the ActionScript flagValue.
ActionScript flags are global flags that scaleforms use
Flags found during testing
0: Returns 1 if the web_browser keyboard is open, otherwise 0
1: Returns 1 if the player has clicked back twice on the opening page, otherwise 0 (web_browser)
2: Returns how many links the player has clicked in the web_browser scaleform, returns 0 when the browser gets closed
9: Returns the current selection on the mobile phone scaleform

There are 20 flags in total.
```

---

### GET_HUD_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x7C9C91AB74A0360F`
- **JHash:** `0x63F66A0B`
- **Build:** `323`

**C# 签名:**
```csharp
void GET_HUD_COLOUR(int hudColorIndex, ref int r, ref int g, ref int b, ref int a)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudColorIndex` | `int` | `int` | - |
| `r` | `int*` | `ref int` | - |
| `g` | `int*` | `ref int` | - |
| `b` | `int*` | `ref int` | - |
| `a` | `int*` | `ref int` | - |

**返回值:** `void` (Native: `void`)

---

### GET_HUD_COMPONENT_POSITION

- **命名空间:** `HUD`
- **Hash:** `0x223CA69A8C4417FD`
- **JHash:** `0x080DCED6`
- **Build:** `323`

**C# 签名:**
```csharp
Vector3 GET_HUD_COMPONENT_POSITION(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `Vector3` (Native: `Vector3`)

---

### GET_HUD_SCREEN_POSITION_FROM_WORLD_POSITION

- **命名空间:** `HUD`
- **Hash:** `0xF9904D11F1ACBEC3`
- **JHash:** `0xFE9A39F8`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_HUD_SCREEN_POSITION_FROM_WORLD_POSITION(float worldX, float worldY, float worldZ, ref float screenX, ref float screenY)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `worldX` | `float` | `float` | - |
| `worldY` | `float` | `float` | - |
| `worldZ` | `float` | `float` | - |
| `screenX` | `float*` | `ref float` | - |
| `screenY` | `float*` | `ref float` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
World to relative screen coords, this world to screen will keep the text on screen. Was named _GET_SCREEN_COORD_FROM_WORLD_COORD, but this conflicts with 0x34E82F05DF2974F5. As that hash actually matches GET_SCREEN_COORD_FROM_WORLD_COORD that one supercedes and this one was renamed to _GET_2D_COORD_FROM_3D_COORD
```

---

### GET_LENGTH_OF_LITERAL_STRING

- **命名空间:** `HUD`
- **Hash:** `0xF030907CCBB8A9FD`
- **JHash:** `0x99379D55`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_LENGTH_OF_LITERAL_STRING(string string)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `string` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
Returns the length of the string passed (much like strlen).
```

---

### GET_LENGTH_OF_LITERAL_STRING_IN_BYTES

- **命名空间:** `HUD`
- **Hash:** `0x43E4111189E54F0E`
- **JHash:** `0x7DBC0764`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_LENGTH_OF_LITERAL_STRING_IN_BYTES(string string)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `string` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

---

### GET_LENGTH_OF_STRING_WITH_THIS_TEXT_LABEL

- **命名空间:** `HUD`
- **Hash:** `0x801BD273D3A23F74`
- **JHash:** `0xA4CA7BE5`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_LENGTH_OF_STRING_WITH_THIS_TEXT_LABEL(string gxt)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxt` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

**说明:**
```
Returns the string length of the string from the gxt string .
```

---

### GET_MAIN_PLAYER_BLIP_ID

- **命名空间:** `HUD`
- **Hash:** `0xDCD4EC3F419D02FA`
- **JHash:** `0xAB93F020`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_MAIN_PLAYER_BLIP_ID()
```

**返回值:** `int` (Native: `Blip`)

---

### GET_MENU_LAYOUT_CHANGED_EVENT_DETAILS

- **命名空间:** `HUD`
- **Hash:** `0x7E17BE53E1AAABAF`
- **JHash:** `0x6025AA2F`
- **Build:** `323`

**C# 签名:**
```csharp
void GET_MENU_LAYOUT_CHANGED_EVENT_DETAILS(ref int lastItemMenuId, ref int selectedItemMenuId, ref int selectedItemUniqueId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `lastItemMenuId` | `int*` | `ref int` | - |
| `selectedItemMenuId` | `int*` | `ref int` | - |
| `selectedItemUniqueId` | `int*` | `ref int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
lastItemMenuId: this is the menuID of the last selected item minus 1000 (lastItem.menuID - 1000)
selectedItemMenuId: same as lastItemMenuId except for the currently selected menu item
selectedItemUniqueId: this is uniqueID of the currently selected menu item

when the pausemenu is closed:
lastItemMenuId = -1
selectedItemMenuId = -1
selectedItemUniqueId = 0

when the header gains focus:
lastItemMenuId updates as normal or 0 if the pausemenu was just opened
selectedItemMenuId becomes a unique id for the pausemenu page that focus was taken from (?) or 0 if the pausemenu was just opened
selectedItemUniqueId = -1

when focus is moved from the header to a pausemenu page:
lastItemMenuId becomes a unique id for the pausemenu page that focus was moved to (?)
selectedItemMenuId = -1
selectedItemUniqueId updates as normal
```

---

### GET_MENU_PED_BOOL_STAT

- **命名空间:** `HUD`
- **Hash:** `0x052991E59076E4E4`
- **JHash:** `0xD43BB56D`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_MENU_PED_BOOL_STAT(uint statHash, ref bool outValue)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `statHash` | `Hash` | `uint` | - |
| `outValue` | `BOOL*` | `ref bool` | - |

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
p0 was always 0xAE2602A3.
```

---

### GET_MENU_PED_FLOAT_STAT

- **命名空间:** `HUD`
- **Hash:** `0x5FBD7095FE7AE57F`
- **JHash:** `0x51972B04`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_MENU_PED_FLOAT_STAT(uint statHash, ref float outValue)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `statHash` | `Hash` | `uint` | - |
| `outValue` | `float*` | `ref float` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### GET_MENU_PED_INT_STAT

- **命名空间:** `HUD`
- **Hash:** `0xEF4CED81CEBEDC6D`
- **JHash:** `0x4370999E`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_MENU_PED_INT_STAT(object p0, ref object p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |
| `p1` | `Any*` | `ref object` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### GET_MENU_PED_MASKED_INT_STAT

- **命名空间:** `HUD`
- **Hash:** `0x90A6526CF0381030`
- **JHash:** `0xD6CC4766`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_MENU_PED_MASKED_INT_STAT(uint statHash, ref int outValue, int mask, bool p3)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `statHash` | `Hash` | `uint` | - |
| `outValue` | `int*` | `ref int` | - |
| `mask` | `int` | `int` | - |
| `p3` | `BOOL` | `bool` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### GET_MENU_TRIGGER_EVENT_DETAILS

- **命名空间:** `HUD`
- **Hash:** `0x36C1451A88A09630`
- **JHash:** `0x8543AAC8`
- **Build:** `323`

**C# 签名:**
```csharp
void GET_MENU_TRIGGER_EVENT_DETAILS(ref int lastItemMenuId, ref int selectedItemUniqueId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `lastItemMenuId` | `int*` | `ref int` | - |
| `selectedItemUniqueId` | `int*` | `ref int` | - |

**返回值:** `void` (Native: `void`)

---

### GET_MINIMAP_FOW_COORDINATE_IS_REVEALED

- **命名空间:** `HUD`
- **Hash:** `0x6E31B91145873922`
- **JHash:** `0x65B705F6`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_MINIMAP_FOW_COORDINATE_IS_REVEALED(float x, float y, float z)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `float` | `float` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### GET_MINIMAP_FOW_DISCOVERY_RATIO

- **命名空间:** `HUD`
- **Hash:** `0xE0130B41D3CF4574`
- **JHash:** `0xA4098ACC`
- **Build:** `323`

**C# 签名:**
```csharp
float GET_MINIMAP_FOW_DISCOVERY_RATIO()
```

**返回值:** `float` (Native: `float`)

---

### GET_MOBILE_PHONE_POSITION

- **命名空间:** `MOBILE`
- **Hash:** `0x584FDFDA48805B86`
- **JHash:** `0xB2E1E1A0`
- **Build:** `323`

**C# 签名:**
```csharp
void GET_MOBILE_PHONE_POSITION(ref Vector3 position)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `position` | `Vector3*` | `ref Vector3` | - |

**返回值:** `void` (Native: `void`)

---

### GET_MOBILE_PHONE_RENDER_ID

- **命名空间:** `MOBILE`
- **Hash:** `0xB4A53E05F68B6FA1`
- **JHash:** `0x88E4FECE`
- **Build:** `323`

**C# 签名:**
```csharp
void GET_MOBILE_PHONE_RENDER_ID(ref int renderId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `renderId` | `int*` | `ref int` | - |

**返回值:** `void` (Native: `void`)

---

### GET_MOBILE_PHONE_ROTATION

- **命名空间:** `MOBILE`
- **Hash:** `0x1CEFB61F193070AE`
- **JHash:** `0x17A29F23`
- **Build:** `323`

**C# 签名:**
```csharp
void GET_MOBILE_PHONE_ROTATION(ref Vector3 rotation, int p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `rotation` | `Vector3*` | `ref Vector3` | - |
| `p1` | `Vehicle` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### GET_MOUSE_EVENT

- **命名空间:** `HUD`
- **Hash:** `0x632B2940C67F4EA9`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_MOUSE_EVENT(int scaleformHandle, ref object p1, ref object p2, ref object p3)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `scaleformHandle` | `int` | `int` | - |
| `p1` | `Any*` | `ref object` | - |
| `p2` | `Any*` | `ref object` | - |
| `p3` | `Any*` | `ref object` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### GET_NAMED_RENDERTARGET_RENDER_ID

- **命名空间:** `HUD`
- **Hash:** `0x1A6478B61C6BDC3B`
- **JHash:** `0xF9D7A401`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_NAMED_RENDERTARGET_RENDER_ID(string name)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `name` | `const char*` | `string` | - |

**返回值:** `int` (Native: `int`)

---

### GET_NEW_SELECTED_MISSION_CREATOR_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x5C90988E7C8E1AF4`
- **JHash:** `0xFFD7476C`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_NEW_SELECTED_MISSION_CREATOR_BLIP()
```

**返回值:** `int` (Native: `Blip`)

---

### GET_NEXT_BLIP_INFO_ID

- **命名空间:** `HUD`
- **Hash:** `0x14F96AA50D6FBEA7`
- **JHash:** `0x9356E92F`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_NEXT_BLIP_INFO_ID(int blipSprite)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blipSprite` | `int` | `int` | - |

**返回值:** `int` (Native: `Blip`)

---

### GET_NORTH_BLID_INDEX

- **命名空间:** `HUD`
- **Hash:** `0x3F0CF9CB7E589B88`
- **Build:** `463`

**C# 签名:**
```csharp
int GET_NORTH_BLID_INDEX()
```

**返回值:** `int` (Native: `Blip`)

---

### GET_NUMBER_OF_ACTIVE_BLIPS

- **命名空间:** `HUD`
- **Hash:** `0x9A3FF3DE163034E8`
- **JHash:** `0x144020FA`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_NUMBER_OF_ACTIVE_BLIPS()
```

**返回值:** `int` (Native: `int`)

---

### GET_PAUSE_MENU_POSITION

- **命名空间:** `HUD`
- **Hash:** `0x5BFF36D6ED83E0AE`
- **Build:** `323`

**C# 签名:**
```csharp
Vector3 GET_PAUSE_MENU_POSITION()
```

**返回值:** `Vector3` (Native: `Vector3`)

---

### GET_PAUSE_MENU_STATE

- **命名空间:** `HUD`
- **Hash:** `0x272ACD84970869C5`
- **JHash:** `0x92F50134`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_PAUSE_MENU_STATE()
```

**返回值:** `int` (Native: `int`)

**说明:**
```
Returns:

0
5
10
15
20
25
30
35

```

---

### GET_PM_PLAYER_CREW_COLOR

- **命名空间:** `HUD`
- **Hash:** `0xA238192F33110615`
- **JHash:** `0x46794EB2`
- **Build:** `323`

**C# 签名:**
```csharp
bool GET_PM_PLAYER_CREW_COLOR(ref int r, ref int g, ref int b)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `r` | `int*` | `ref int` | - |
| `g` | `int*` | `ref int` | - |
| `b` | `int*` | `ref int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### GET_RENDERED_CHARACTER_HEIGHT

- **命名空间:** `HUD`
- **Hash:** `0xDB88A37483346780`
- **JHash:** `0x3330175B`
- **Build:** `323`

**C# 签名:**
```csharp
float GET_RENDERED_CHARACTER_HEIGHT(float size, int font)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `size` | `float` | `float` | - |
| `font` | `int` | `int` | - |

**返回值:** `float` (Native: `float`)

**说明:**
```
This gets the height of the FONT and not the total text. You need to get the number of lines your text uses, and get the height of a newline (I'm using a smaller value) to get the total text height.
```

---

### GET_SCREEN_CODE_WANTS_SCRIPT_TO_CONTROL

- **命名空间:** `HUD`
- **Hash:** `0x593FEAE1F73392D4`
- **JHash:** `0x22CA9F2A`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_SCREEN_CODE_WANTS_SCRIPT_TO_CONTROL()
```

**返回值:** `int` (Native: `int`)

---

### GET_STANDARD_BLIP_ENUM_ID

- **命名空间:** `HUD`
- **Hash:** `0x4A9923385BDB9DAD`
- **JHash:** `0x87871CE0`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_STANDARD_BLIP_ENUM_ID()
```

**返回值:** `int` (Native: `int`)

---

### GET_STREET_NAME_FROM_HASH_KEY

- **命名空间:** `HUD`
- **Hash:** `0xD0EF8A959B8A4CB9`
- **JHash:** `0x1E8E310C`
- **Build:** `323`

**C# 签名:**
```csharp
string GET_STREET_NAME_FROM_HASH_KEY(uint hash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hash` | `Hash` | `uint` | - |

**返回值:** `string` (Native: `const char*`)

**说明:**
```
This functions converts the hash of a street name into a readable string.

For how to get the hashes, see PATHFIND::GET_STREET_NAME_AT_COORD.
```

---

### GET_WARNING_SCREEN_MESSAGE_HASH

- **命名空间:** `HUD`
- **Hash:** `0x81DF9ABA6C83DFF9`
- **Build:** `1290`

**C# 签名:**
```csharp
uint GET_WARNING_SCREEN_MESSAGE_HASH()
```

**返回值:** `uint` (Native: `Hash`)

**说明:**
```
Has to do with the confirmation overlay (E.g. confirm exit)
```

---

### GET_WAYPOINT_BLIP_ENUM_ID

- **命名空间:** `HUD`
- **Hash:** `0x186E5D252FA50E7D`
- **JHash:** `0xB9827942`
- **Build:** `323`

**C# 签名:**
```csharp
int GET_WAYPOINT_BLIP_ENUM_ID()
```

**返回值:** `int` (Native: `int`)

---

### GET_WAYPOINT_CLEAR_ON_ARRIVAL_MODE

- **命名空间:** `HUD`
- **Hash:** `0xF46851AB8B02EF40`
- **Build:** `3717`

**C# 签名:**
```csharp
int GET_WAYPOINT_CLEAR_ON_ARRIVAL_MODE()
```

**返回值:** `int` (Native: `int`)

---

### GIVE_PED_TO_PAUSE_MENU

- **命名空间:** `HUD`
- **Hash:** `0xAC0BFBDC3BE00E14`
- **JHash:** `0x2AD2C9CE`
- **Build:** `323`

**C# 签名:**
```csharp
void GIVE_PED_TO_PAUSE_MENU(int ped, int p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |
| `p1` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
p1 is either 1 or 2 in the PC scripts.
```

---

### HAS_ADDITIONAL_TEXT_LOADED

- **命名空间:** `HUD`
- **Hash:** `0x02245FE4BED318B8`
- **JHash:** `0xB0E56045`
- **Build:** `323`

**C# 签名:**
```csharp
bool HAS_ADDITIONAL_TEXT_LOADED(int slot)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `slot` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### HAS_DIRECTOR_MODE_BEEN_LAUNCHED_BY_CODE

- **命名空间:** `HUD`
- **Hash:** `0xA277800A9EAE340E`
- **Build:** `323`

**C# 签名:**
```csharp
bool HAS_DIRECTOR_MODE_BEEN_LAUNCHED_BY_CODE()
```

**返回值:** `bool` (Native: `BOOL`)

---

### HAS_MENU_LAYOUT_CHANGED_EVENT_OCCURRED

- **命名空间:** `HUD`
- **Hash:** `0x2E22FEFA0100275E`
- **JHash:** `0x96863460`
- **Build:** `323`

**C# 签名:**
```csharp
bool HAS_MENU_LAYOUT_CHANGED_EVENT_OCCURRED()
```

**返回值:** `bool` (Native: `BOOL`)

---

### HAS_MENU_TRIGGER_EVENT_OCCURRED

- **命名空间:** `HUD`
- **Hash:** `0xF284AC67940C6812`
- **JHash:** `0x7D95AFFF`
- **Build:** `323`

**C# 签名:**
```csharp
bool HAS_MENU_TRIGGER_EVENT_OCCURRED()
```

**返回值:** `bool` (Native: `BOOL`)

---

### HAS_SCRIPT_HIDDEN_HELP_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x214CD562A939246A`
- **JHash:** `0x812CBE0E`
- **Build:** `323`

**C# 签名:**
```csharp
bool HAS_SCRIPT_HIDDEN_HELP_THIS_FRAME()
```

**返回值:** `bool` (Native: `BOOL`)

---

### HAS_THIS_ADDITIONAL_TEXT_LOADED

- **命名空间:** `HUD`
- **Hash:** `0xADBF060E2B30C5BC`
- **JHash:** `0x80A52040`
- **Build:** `323`

**C# 签名:**
```csharp
bool HAS_THIS_ADDITIONAL_TEXT_LOADED(string gxt, int slot)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxt` | `const char*` | `string` | - |
| `slot` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
Checks if the specified gxt has loaded into the passed slot.
```

---

### HIDE_HELP_TEXT_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xD46923FC481CA285`
- **JHash:** `0xF3807BED`
- **Build:** `323`

**C# 签名:**
```csharp
void HIDE_HELP_TEXT_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### HIDE_HUDMARKERS_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x243296A510B562B6`
- **Build:** `2060`

**C# 签名:**
```csharp
void HIDE_HUDMARKERS_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### HIDE_HUD_AND_RADAR_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x719FF505F097FD20`
- **JHash:** `0xB75D4AD2`
- **Build:** `323`

**C# 签名:**
```csharp
void HIDE_HUD_AND_RADAR_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Hides HUD and radar this frame and prohibits switching to other weapons (or accessing the weapon wheel)
```

---

### HIDE_HUD_COMPONENT_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x6806C51AD12B83B8`
- **JHash:** `0xDB2D0762`
- **Build:** `323`

**C# 签名:**
```csharp
void HIDE_HUD_COMPONENT_THIS_FRAME(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
This function hides various HUD (Heads-up Display) components.
Listed below are the integers and the corresponding HUD component.
- 1 : WANTED_STARS
- 2 : WEAPON_ICON
- 3 : CASH
- 4 : MP_CASH
- 5 : MP_MESSAGE
- 6 : VEHICLE_NAME
- 7 : AREA_NAME
- 8 : VEHICLE_CLASS
- 9 : STREET_NAME
- 10 : HELP_TEXT
- 11 : FLOATING_HELP_TEXT_1
- 12 : FLOATING_HELP_TEXT_2
- 13 : CASH_CHANGE
- 14 : RETICLE
- 15 : SUBTITLE_TEXT
- 16 : RADIO_STATIONS
- 17 : SAVING_GAME
- 18 : GAME_STREAM
- 19 : WEAPON_WHEEL
- 20 : WEAPON_WHEEL_STATS
- 21 : HUD_COMPONENTS
- 22 : HUD_WEAPONS

These integers also work for the `SHOW_HUD_COMPONENT_THIS_FRAME` native, but instead shows the HUD Component.
```

---

### HIDE_LOADING_ON_FADE_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x4B0311D3CDC4648F`
- **JHash:** `0x35087963`
- **Build:** `323`

**C# 签名:**
```csharp
void HIDE_LOADING_ON_FADE_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### HIDE_MINIMAP_EXTERIOR_MAP_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x5FBAE526203990C9`
- **JHash:** `0x1A4318F7`
- **Build:** `323`

**C# 签名:**
```csharp
void HIDE_MINIMAP_EXTERIOR_MAP_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### HIDE_MINIMAP_INTERIOR_MAP_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x20FE7FDFEEAD38C0`
- **JHash:** `0xCE36E3FE`
- **Build:** `323`

**C# 签名:**
```csharp
void HIDE_MINIMAP_INTERIOR_MAP_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### HIDE_NUMBER_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x532CFF637EF80148`
- **JHash:** `0x0B6D610D`
- **Build:** `323`

**C# 签名:**
```csharp
void HIDE_NUMBER_ON_BLIP(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### HIDE_SCRIPTED_HUD_COMPONENT_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xE374C498D8BADC14`
- **JHash:** `0x31ABA127`
- **Build:** `323`

**C# 签名:**
```csharp
void HIDE_SCRIPTED_HUD_COMPONENT_THIS_FRAME(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### HIDE_STREET_AND_CAR_NAMES_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xA4DEDE28B1814289`
- **JHash:** `0x52746FE1`
- **Build:** `323`

**C# 签名:**
```csharp
void HIDE_STREET_AND_CAR_NAMES_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Hides area and vehicle name HUD components for one frame.
```

---

### HUD_FORCE_SPECIAL_VEHICLE_WEAPON_WHEEL

- **命名空间:** `HUD`
- **Hash:** `0x488043841BBE156F`
- **Build:** `1011`

**C# 签名:**
```csharp
void HUD_FORCE_SPECIAL_VEHICLE_WEAPON_WHEEL()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Displays "blazer_wheels_up" and "blazer_wheels_down" "weapon" icons when switching between jetski and quadbike modes. Works only on vehicles using "VEHICLE_TYPE_AMPHIBIOUS_QUADBIKE" vehicle type. Needs to be called every time prior to switching modes, otherwise the icon will only appear when switching modes once.
```

---

### HUD_FORCE_WEAPON_WHEEL

- **命名空间:** `HUD`
- **Hash:** `0xEB354E5376BC81A7`
- **JHash:** `0x1EFFB02A`
- **Build:** `323`

**C# 签名:**
```csharp
void HUD_FORCE_WEAPON_WHEEL(bool show)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `show` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Forces the weapon wheel to show/hide.
```

---

### HUD_GET_WEAPON_WHEEL_CURRENTLY_HIGHLIGHTED

- **命名空间:** `HUD`
- **Hash:** `0xA48931185F0536FE`
- **JHash:** `0x22E9F555`
- **Build:** `323`

**C# 签名:**
```csharp
uint HUD_GET_WEAPON_WHEEL_CURRENTLY_HIGHLIGHTED()
```

**返回值:** `uint` (Native: `Hash`)

**说明:**
```
Returns the weapon hash to the selected/highlighted weapon in the wheel
```

---

### HUD_GET_WEAPON_WHEEL_TOP_SLOT

- **命名空间:** `HUD`
- **Hash:** `0xA13E93403F26C812`
- **Build:** `323`

**C# 签名:**
```csharp
uint HUD_GET_WEAPON_WHEEL_TOP_SLOT(int weaponTypeIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `weaponTypeIndex` | `int` | `int` | - |

**返回值:** `uint` (Native: `Hash`)

**说明:**
```
Returns the weapon hash active in a specific weapon wheel slotList
```

---

### HUD_SET_WEAPON_WHEEL_TOP_SLOT

- **命名空间:** `HUD`
- **Hash:** `0x72C1056D678BB7D8`
- **JHash:** `0x83B608A0`
- **Build:** `323`

**C# 签名:**
```csharp
void HUD_SET_WEAPON_WHEEL_TOP_SLOT(uint weaponHash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `weaponHash` | `Hash` | `uint` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Set the active slotIndex in the wheel weapon to the slot associated with the provided Weapon hash
```

---

### HUD_SHOWING_CHARACTER_SWITCH_SELECTION

- **命名空间:** `HUD`
- **Hash:** `0x14C9FDCC41F81F63`
- **JHash:** `0xE70D1F43`
- **Build:** `323`

**C# 签名:**
```csharp
void HUD_SHOWING_CHARACTER_SWITCH_SELECTION(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets a global that disables many weapon input tasks (shooting, aiming, etc.). Does not work with vehicle weapons, only used in selector.ysc
```

---

### HUD_SUPPRESS_WEAPON_WHEEL_RESULTS_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x0AFC4AF510774B47`
- **JHash:** `0xB26FED2B`
- **Build:** `323`

**C# 签名:**
```csharp
void HUD_SUPPRESS_WEAPON_WHEEL_RESULTS_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Calling this each frame, stops the player from receiving a weapon via the weapon wheel.
```

---

### IS_BLIP_FLASHING

- **命名空间:** `HUD`
- **Hash:** `0xA5E41FD83AD6CEF0`
- **JHash:** `0x52E111D7`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_BLIP_FLASHING(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_BLIP_ON_MINIMAP

- **命名空间:** `HUD`
- **Hash:** `0xE41CA53051197A27`
- **JHash:** `0x258CBA3A`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_BLIP_ON_MINIMAP(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_BLIP_SHORT_RANGE

- **命名空间:** `HUD`
- **Hash:** `0xDA5F8727EB75B926`
- **JHash:** `0x1226765A`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_BLIP_SHORT_RANGE(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_FLOATING_HELP_TEXT_ON_SCREEN

- **命名空间:** `HUD`
- **Hash:** `0x2432784ACA090DA4`
- **JHash:** `0x45472FD5`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_FLOATING_HELP_TEXT_ON_SCREEN(int hudIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudIndex` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_FRONTEND_READY_FOR_CONTROL

- **命名空间:** `HUD`
- **Hash:** `0x3BAB9A4E4F2FF5C7`
- **JHash:** `0xD3BF3ABD`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_FRONTEND_READY_FOR_CONTROL()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_HELP_MESSAGE_BEING_DISPLAYED

- **命名空间:** `HUD`
- **Hash:** `0x4D79439A6B55AC67`
- **JHash:** `0xA65F262A`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_HELP_MESSAGE_BEING_DISPLAYED()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_HELP_MESSAGE_FADING_OUT

- **命名空间:** `HUD`
- **Hash:** `0x327EDEEEAC55C369`
- **JHash:** `0x3E50AE92`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_HELP_MESSAGE_FADING_OUT()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_HELP_MESSAGE_ON_SCREEN

- **命名空间:** `HUD`
- **Hash:** `0xDAD37F45428801AE`
- **JHash:** `0x4B3C9CA9`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_HELP_MESSAGE_ON_SCREEN()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_HOVERING_OVER_MISSION_CREATOR_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x4167EFE0527D706E`
- **JHash:** `0xC5EB849A`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_HOVERING_OVER_MISSION_CREATOR_BLIP()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_HUD_COMPONENT_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0xBC4C9EA5391ECC0D`
- **JHash:** `0x6214631F`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_HUD_COMPONENT_ACTIVE(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
Full list of components below

HUD = 0;
HUD_WANTED_STARS = 1;
HUD_WEAPON_ICON = 2;
HUD_CASH = 3;
HUD_MP_CASH = 4;
HUD_MP_MESSAGE = 5;
HUD_VEHICLE_NAME = 6;
HUD_AREA_NAME = 7;
HUD_VEHICLE_CLASS = 8;
HUD_STREET_NAME = 9;
HUD_HELP_TEXT = 10;
HUD_FLOATING_HELP_TEXT_1 = 11;
HUD_FLOATING_HELP_TEXT_2 = 12;
HUD_CASH_CHANGE = 13;
HUD_RETICLE = 14;
HUD_SUBTITLE_TEXT = 15;
HUD_RADIO_STATIONS = 16;
HUD_SAVING_GAME = 17;
HUD_GAME_STREAM = 18;
HUD_WEAPON_WHEEL = 19;
HUD_WEAPON_WHEEL_STATS = 20;
MAX_HUD_COMPONENTS = 21;
MAX_HUD_WEAPONS = 22;
MAX_SCRIPTED_HUD_COMPONENTS = 141;
```

---

### IS_HUD_COMPONENT_HIDDEN_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x8EDC335C943465C8`
- **Build:** `3717`

**C# 签名:**
```csharp
bool IS_HUD_COMPONENT_HIDDEN_THIS_FRAME(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_HUD_HIDDEN

- **命名空间:** `HUD`
- **Hash:** `0xA86478C6958735C5`
- **JHash:** `0x40BADA1D`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_HUD_HIDDEN()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_HUD_PREFERENCE_SWITCHED_ON

- **命名空间:** `HUD`
- **Hash:** `0x1930DFA731813EC4`
- **JHash:** `0xC3BC1B4F`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_HUD_PREFERENCE_SWITCHED_ON()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_IME_IN_PROGRESS

- **命名空间:** `HUD`
- **Hash:** `0x801879A9B4F4B2FB`
- **Build:** `372`

**C# 签名:**
```csharp
bool IS_IME_IN_PROGRESS()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_MESSAGE_BEING_DISPLAYED

- **命名空间:** `HUD`
- **Hash:** `0x7984C03AA5CC2F41`
- **JHash:** `0x6A77FE8D`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_MESSAGE_BEING_DISPLAYED()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_MINIMAP_RENDERING

- **命名空间:** `HUD`
- **Hash:** `0xAF754F20EB5CD51A`
- **JHash:** `0x9CD18314`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_MINIMAP_RENDERING()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_MISSION_CREATOR_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x26F49BF3381D933D`
- **JHash:** `0x24ACC4E9`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_MISSION_CREATOR_BLIP(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_MOUSE_ROLLED_OVER_INSTRUCTIONAL_BUTTONS

- **命名空间:** `HUD`
- **Hash:** `0x3D9ACB1EB139E702`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_MOUSE_ROLLED_OVER_INSTRUCTIONAL_BUTTONS()
```

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
Returns TRUE if mouse is hovering above instructional buttons. Works with all buttons gfx, such as popup_warning, pause_menu_instructional_buttons, instructional_buttons, etc. Note: You have to call TOGGLE_MOUSE_BUTTONS on the scaleform if you want this native to work.
```

---

### IS_MP_GAMER_TAG_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x4E929E7A5796FD26`
- **JHash:** `0x60118951`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_MP_GAMER_TAG_ACTIVE(int gamerTagId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_MP_GAMER_TAG_FREE

- **命名空间:** `HUD`
- **Hash:** `0x595B5178E412E199`
- **JHash:** `0x63959059`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_MP_GAMER_TAG_FREE(int gamerTagId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_MP_GAMER_TAG_MOVIE_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x6E0EB3EB47C8D7AA`
- **JHash:** `0xEFD2564A`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_MP_GAMER_TAG_MOVIE_ACTIVE()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_MP_TEXT_CHAT_TYPING

- **命名空间:** `HUD`
- **Hash:** `0xB118AF58B5F332A1`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_MP_TEXT_CHAT_TYPING()
```

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
Returns whether or not the text chat (MULTIPLAYER_CHAT Scaleform component) is active.
```

---

### IS_NAMED_RENDERTARGET_LINKED

- **命名空间:** `HUD`
- **Hash:** `0x113750538FA31298`
- **JHash:** `0x8B52601F`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_NAMED_RENDERTARGET_LINKED(uint modelHash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `modelHash` | `Hash` | `uint` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_NAMED_RENDERTARGET_REGISTERED

- **命名空间:** `HUD`
- **Hash:** `0x78DCDC15C9F116B4`
- **JHash:** `0x284057F5`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_NAMED_RENDERTARGET_REGISTERED(string name)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `name` | `const char*` | `string` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_NAVIGATING_MENU_CONTENT

- **命名空间:** `HUD`
- **Hash:** `0x4E3CD0EF8A489541`
- **JHash:** `0xDA7951A2`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_NAVIGATING_MENU_CONTENT()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_ONLINE_POLICIES_MENU_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x6F72CD94F7B5B68C`
- **JHash:** `0x9D4934F4`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_ONLINE_POLICIES_MENU_ACTIVE()
```

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
Returns the same as IS_SOCIAL_CLUB_ACTIVE
```

---

### IS_PAUSEMAP_IN_INTERIOR_MODE

- **命名空间:** `HUD`
- **Hash:** `0x9049FE339D5F6F6F`
- **JHash:** `0x199DED14`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_PAUSEMAP_IN_INTERIOR_MODE()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_PAUSE_MENU_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0xB0034A223497FFCB`
- **JHash:** `0xD3600591`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_PAUSE_MENU_ACTIVE()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_PAUSE_MENU_RESTARTING

- **命名空间:** `HUD`
- **Hash:** `0x1C491717107431C7`
- **JHash:** `0x3C4CF4D9`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_PAUSE_MENU_RESTARTING()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_RADAR_HIDDEN

- **命名空间:** `HUD`
- **Hash:** `0x157F93B036700462`
- **JHash:** `0x1AB3B954`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_RADAR_HIDDEN()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_RADAR_PREFERENCE_SWITCHED_ON

- **命名空间:** `HUD`
- **Hash:** `0x9EB6522EA68F22FE`
- **JHash:** `0x14AEAA28`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_RADAR_PREFERENCE_SWITCHED_ON()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_REPORTUGC_MENU_OPEN

- **命名空间:** `HUD`
- **Hash:** `0x9135584D09A3437E`
- **JHash:** `0x9D2C94FA`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_REPORTUGC_MENU_OPEN()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_SCRIPTED_HUD_COMPONENT_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0xDD100EB17A94FF65`
- **JHash:** `0x2B86F382`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_SCRIPTED_HUD_COMPONENT_ACTIVE(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_SCRIPTED_HUD_COMPONENT_HIDDEN_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x09C0403ED9A751C2`
- **JHash:** `0xE8C8E535`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_SCRIPTED_HUD_COMPONENT_HIDDEN_THIS_FRAME(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_SOCIAL_CLUB_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0xC406BE343FC4B9AF`
- **JHash:** `0xD4DA14EF`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_SOCIAL_CLUB_ACTIVE()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_STORE_PENDING_NETWORK_SHUTDOWN_TO_OPEN

- **命名空间:** `HUD`
- **Hash:** `0x2F057596F2BD0061`
- **JHash:** `0xC85C4487`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_STORE_PENDING_NETWORK_SHUTDOWN_TO_OPEN()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_STREAMING_ADDITIONAL_TEXT

- **命名空间:** `HUD`
- **Hash:** `0x8B6817B71B85EBF0`
- **JHash:** `0xF079E4EB`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_STREAMING_ADDITIONAL_TEXT(int p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_SUBTITLE_PREFERENCE_SWITCHED_ON

- **命名空间:** `HUD`
- **Hash:** `0xAD6DACA4BA53E0A4`
- **JHash:** `0x63BA19F5`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_SUBTITLE_PREFERENCE_SWITCHED_ON()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_UPDATING_MP_GAMER_TAG_NAME_AND_CREW_DETAILS

- **命名空间:** `HUD`
- **Hash:** `0xEB709A36958ABE0D`
- **JHash:** `0xF11414C4`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_UPDATING_MP_GAMER_TAG_NAME_AND_CREW_DETAILS(int gamerTagId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### IS_WARNING_MESSAGE_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0xE18B138FABC53103`
- **JHash:** `0x94C834AD`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_WARNING_MESSAGE_ACTIVE()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_WARNING_MESSAGE_READY_FOR_CONTROL

- **命名空间:** `HUD`
- **Hash:** `0xAF42195A42C63BBA`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_WARNING_MESSAGE_READY_FOR_CONTROL()
```

**返回值:** `bool` (Native: `BOOL`)

---

### IS_WAYPOINT_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x1DD1F58F493F1DA5`
- **JHash:** `0x5E4DF47B`
- **Build:** `323`

**C# 签名:**
```csharp
bool IS_WAYPOINT_ACTIVE()
```

**返回值:** `bool` (Native: `BOOL`)

---

### LINK_NAMED_RENDERTARGET

- **命名空间:** `HUD`
- **Hash:** `0xF6C09E276AEB3F2D`
- **JHash:** `0x6844C4B9`
- **Build:** `323`

**C# 签名:**
```csharp
void LINK_NAMED_RENDERTARGET(uint modelHash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `modelHash` | `Hash` | `uint` | - |

**返回值:** `void` (Native: `void`)

---

### LOCK_MINIMAP_ANGLE

- **命名空间:** `HUD`
- **Hash:** `0x299FAEBB108AE05B`
- **JHash:** `0xDEC733E4`
- **Build:** `323`

**C# 签名:**
```csharp
void LOCK_MINIMAP_ANGLE(int angle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `angle` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Locks the minimap to the specified angle in integer degrees.

angle: The angle in whole degrees. If less than 0 or greater than 360, unlocks the angle.
```

---

### LOCK_MINIMAP_POSITION

- **命名空间:** `HUD`
- **Hash:** `0x1279E861A329E73F`
- **JHash:** `0xB9632A91`
- **Build:** `323`

**C# 签名:**
```csharp
void LOCK_MINIMAP_POSITION(float x, float y)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Locks the minimap to the specified world position.
```

---

### MP_TEXT_CHAT_DISABLE

- **命名空间:** `HUD`
- **Hash:** `0x1DB21A44B09E8BA3`
- **Build:** `323`

**C# 签名:**
```csharp
void MP_TEXT_CHAT_DISABLE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Hides the chat history, closes the input box and makes it unable to be opened unless called again with FALSE.
```

---

### MP_TEXT_CHAT_IS_TEAM_JOB

- **命名空间:** `HUD`
- **Hash:** `0x7C226D5346D4D10A`
- **Build:** `372`

**C# 签名:**
```csharp
void MP_TEXT_CHAT_IS_TEAM_JOB(object p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### OPEN_ONLINE_POLICIES_MENU

- **命名空间:** `HUD`
- **Hash:** `0x805D7CBB36FD6C4C`
- **JHash:** `0x19FCBBB2`
- **Build:** `323`

**C# 签名:**
```csharp
void OPEN_ONLINE_POLICIES_MENU()
```

**返回值:** `void` (Native: `void`)

---

### OPEN_REPORTUGC_MENU

- **命名空间:** `HUD`
- **Hash:** `0x523A590C1A3CC0D3`
- **JHash:** `0x10DE5150`
- **Build:** `323`

**C# 签名:**
```csharp
void OPEN_REPORTUGC_MENU()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Shows a menu for reporting UGC content.
```

---

### OPEN_SOCIAL_CLUB_MENU

- **命名空间:** `HUD`
- **Hash:** `0x75D3691713C3B05A`
- **JHash:** `0x57218529`
- **Build:** `323`

**C# 签名:**
```csharp
void OPEN_SOCIAL_CLUB_MENU(uint menu)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `menu` | `Hash` | `uint` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Uses the `SOCIAL_CLUB2` scaleform.
menu: GALLERY, MISSIONS, CREWS, MIGRATE, PLAYLISTS, JOBS
```

---

### OVERRIDE_MP_TEXT_CHAT_COLOR

- **命名空间:** `HUD`
- **Hash:** `0xF47E567B3630DD12`
- **Build:** `678`

**C# 签名:**
```csharp
void OVERRIDE_MP_TEXT_CHAT_COLOR(int p0, int hudColor)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |
| `hudColor` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### OVERRIDE_MP_TEXT_CHAT_TEAM_STRING

- **命名空间:** `HUD`
- **Hash:** `0x6A1738B4323FE2D9`
- **Build:** `573`

**C# 签名:**
```csharp
void OVERRIDE_MP_TEXT_CHAT_TEAM_STRING(uint gxtEntryHash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxtEntryHash` | `Hash` | `uint` | - |

**返回值:** `void` (Native: `void`)

---

### PAUSE_MENUCEPTION_GO_DEEPER

- **命名空间:** `HUD`
- **Hash:** `0x77F16B447824DA6C`
- **JHash:** `0x0A89336C`
- **Build:** `323`

**C# 签名:**
```csharp
void PAUSE_MENUCEPTION_GO_DEEPER(int page)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `page` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### PAUSE_MENUCEPTION_THE_KICK

- **命名空间:** `HUD`
- **Hash:** `0xCDCA26E80FAECB8F`
- **JHash:** `0xC84BE309`
- **Build:** `323`

**C# 签名:**
```csharp
void PAUSE_MENUCEPTION_THE_KICK()
```

**返回值:** `void` (Native: `void`)

---

### PAUSE_MENU_ACTIVATE_CONTEXT

- **命名空间:** `HUD`
- **Hash:** `0xDD564BDD0472C936`
- **JHash:** `0x9FE8FD5E`
- **Build:** `323`

**C# 签名:**
```csharp
void PAUSE_MENU_ACTIVATE_CONTEXT(uint contextHash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `contextHash` | `Hash` | `uint` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Activates the specified frontend menu context.
pausemenu.xml defines some specific menu options using 'context'. Context is basically a 'condition'. 
The `*ALL*` part of the context means that whatever is being defined, will be active when any or all of those conditions after `*ALL*` are met.
The `*NONE*` part of the context section means that whatever is being defined, will NOT be active if any or all of the conditions after `*NONE*` are met.
This basically allows you to hide certain menu sections, or things like instructional buttons.
```

---

### PAUSE_MENU_DEACTIVATE_CONTEXT

- **命名空间:** `HUD`
- **Hash:** `0x444D8CF241EC25C5`
- **JHash:** `0x0029046E`
- **Build:** `323`

**C# 签名:**
```csharp
void PAUSE_MENU_DEACTIVATE_CONTEXT(uint contextHash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `contextHash` | `Hash` | `uint` | - |

**返回值:** `void` (Native: `void`)

---

### PAUSE_MENU_GET_HAIR_COLOUR_INDEX

- **命名空间:** `HUD`
- **Hash:** `0xDE03620F8703A9DF`
- **Build:** `323`

**C# 签名:**
```csharp
int PAUSE_MENU_GET_HAIR_COLOUR_INDEX()
```

**返回值:** `int` (Native: `int`)

---

### PAUSE_MENU_GET_MOUSE_CLICK_EVENT

- **命名空间:** `HUD`
- **Hash:** `0xC8E1071177A23BE5`
- **Build:** `323`

**C# 签名:**
```csharp
bool PAUSE_MENU_GET_MOUSE_CLICK_EVENT(ref object p0, ref object p1, ref object p2)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any*` | `ref object` | - |
| `p1` | `Any*` | `ref object` | - |
| `p2` | `Any*` | `ref object` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### PAUSE_MENU_GET_MOUSE_HOVER_INDEX

- **命名空间:** `HUD`
- **Hash:** `0x359AF31A4B52F5ED`
- **Build:** `323`

**C# 签名:**
```csharp
int PAUSE_MENU_GET_MOUSE_HOVER_INDEX()
```

**返回值:** `int` (Native: `int`)

---

### PAUSE_MENU_GET_MOUSE_HOVER_UNIQUE_ID

- **命名空间:** `HUD`
- **Hash:** `0x13C4B962653A5280`
- **Build:** `323`

**C# 签名:**
```csharp
int PAUSE_MENU_GET_MOUSE_HOVER_UNIQUE_ID()
```

**返回值:** `int` (Native: `int`)

---

### PAUSE_MENU_IS_CONTEXT_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x84698AB38D0C6636`
- **JHash:** `0xC51BC42F`
- **Build:** `323`

**C# 签名:**
```csharp
bool PAUSE_MENU_IS_CONTEXT_ACTIVE(uint contextHash)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `contextHash` | `Hash` | `uint` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### PAUSE_MENU_IS_CONTEXT_MENU_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x2A25ADC48F87841F`
- **JHash:** `0x016D7AF9`
- **Build:** `323`

**C# 签名:**
```csharp
bool PAUSE_MENU_IS_CONTEXT_MENU_ACTIVE()
```

**返回值:** `bool` (Native: `BOOL`)

---

### PAUSE_MENU_REDRAW_INSTRUCTIONAL_BUTTONS

- **命名空间:** `HUD`
- **Hash:** `0x4895BDEA16E7C080`
- **JHash:** `0x15B24768`
- **Build:** `323`

**C# 签名:**
```csharp
void PAUSE_MENU_REDRAW_INSTRUCTIONAL_BUTTONS(int p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### PAUSE_MENU_SET_BUSY_SPINNER

- **命名空间:** `HUD`
- **Hash:** `0xC78E239AC5B2DDB9`
- **JHash:** `0x6C67131A`
- **Build:** `323`

**C# 签名:**
```csharp
void PAUSE_MENU_SET_BUSY_SPINNER(bool p0, int position, int spinnerIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |
| `position` | `int` | `int` | - |
| `spinnerIndex` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### PAUSE_MENU_SET_WARN_ON_TAB_CHANGE

- **命名空间:** `HUD`
- **Hash:** `0xF06EBB91A81E09E3`
- **JHash:** `0x11D09737`
- **Build:** `323`

**C# 签名:**
```csharp
void PAUSE_MENU_SET_WARN_ON_TAB_CHANGE(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### PAUSE_TOGGLE_FULLSCREEN_MAP

- **命名空间:** `HUD`
- **Hash:** `0x2DE6C5E2E996F178`
- **Build:** `372`

**C# 签名:**
```csharp
void PAUSE_TOGGLE_FULLSCREEN_MAP(object p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### PRELOAD_BUSYSPINNER

- **命名空间:** `HUD`
- **Hash:** `0xC65AB383CD91DF98`
- **JHash:** `0x71077FBD`
- **Build:** `323`

**C# 签名:**
```csharp
void PRELOAD_BUSYSPINNER()
```

**返回值:** `void` (Native: `void`)

---

### PULSE_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x742D6FD43115AF73`
- **JHash:** `0x44253855`
- **Build:** `323`

**C# 签名:**
```csharp
void PULSE_BLIP(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### REFRESH_WAYPOINT

- **命名空间:** `HUD`
- **Hash:** `0x81FA173F170560D1`
- **JHash:** `0xB395D753`
- **Build:** `323`

**C# 签名:**
```csharp
void REFRESH_WAYPOINT()
```

**返回值:** `void` (Native: `void`)

---

### REGISTER_NAMED_RENDERTARGET

- **命名空间:** `HUD`
- **Hash:** `0x57D9C12635E25CE3`
- **JHash:** `0xFAE5D6F0`
- **Build:** `323`

**C# 签名:**
```csharp
bool REGISTER_NAMED_RENDERTARGET(string name, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `name` | `const char*` | `string` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### RELEASE_CONTROL_OF_FRONTEND

- **命名空间:** `HUD`
- **Hash:** `0x14621BB1DF14E2B2`
- **JHash:** `0xB9392CE7`
- **Build:** `323`

**C# 签名:**
```csharp
void RELEASE_CONTROL_OF_FRONTEND()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Enables frontend (works in custom frontends, not sure about regular pause menu) navigation keys on keyboard if they were disabled using the native below.
To disable the keys, use `0xEC9264727EEC0F28`
```

---

### RELEASE_NAMED_RENDERTARGET

- **命名空间:** `HUD`
- **Hash:** `0xE9F6FFE837354DD4`
- **JHash:** `0xD3F6C892`
- **Build:** `323`

**C# 签名:**
```csharp
bool RELEASE_NAMED_RENDERTARGET(string name)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `name` | `const char*` | `string` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### RELOAD_MAP_MENU

- **命名空间:** `HUD`
- **Hash:** `0x2916A928514C9827`
- **Build:** `573`

**C# 签名:**
```csharp
void RELOAD_MAP_MENU()
```

**返回值:** `void` (Native: `void`)

---

### REMOVE_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x86A652570E5F25DD`
- **JHash:** `0xD8C3C1CD`
- **Build:** `323`

**C# 签名:**
```csharp
void REMOVE_BLIP(Blip* blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip*` | `Blip*` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
In the C++ SDK, this seems not to work-- the blip isn't removed immediately. I use it for saving cars.

E.g.:

Ped pped = PLAYER::PLAYER_PED_ID();
Vehicle v = PED::GET_VEHICLE_PED_IS_USING(pped);
Blip b = HUD::ADD_BLIP_FOR_ENTITY(v);

works fine.
But later attempting to delete it with:

Blip b = HUD::GET_BLIP_FROM_ENTITY(v);
if (HUD::DOES_BLIP_EXIST(b)) HUD::REMOVE_BLIP(&b);

doesn't work. And yes, doesn't work without the DOES_BLIP_EXIST check either. Also, if you attach multiple blips to the same thing (say, a vehicle), and that thing disappears, the blips randomly attach to other things (in my case, a vehicle).

Thus for me, HUD::REMOVE_BLIP(&b) only works if there's one blip, (in my case) the vehicle is marked as no longer needed, you drive away from it and it eventually despawns, AND there is only one blip attached to it. I never intentionally attach multiple blips but if the user saves the car, this adds a blip. Then if they delete it, it is supposed to remove the blip, but it doesn't. Then they can immediately save it again, causing another blip to re-appear.
-------------

Passing the address of the variable instead of the value works for me.
e.g.
int blip = HUD::ADD_BLIP_FOR_ENTITY(ped);
HUD::REMOVE_BLIP(&blip);


Remove blip will currently crash your game, just artificially remove the blip by setting the sprite to a id that is 'invisible'.
```

---

### REMOVE_COP_BLIP_FROM_PED

- **命名空间:** `HUD`
- **Hash:** `0xC594B315EDF2D4AF`
- **JHash:** `0x41B0D022`
- **Build:** `323`

**C# 签名:**
```csharp
void REMOVE_COP_BLIP_FROM_PED(int ped)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Interesting fact: A hash collision for this is RESET_JETPACK_MODEL_SETTINGS
```

---

### REMOVE_FAKE_CONE_DATA

- **命名空间:** `HUD`
- **Hash:** `0x35A3CD97B2C0A6D2`
- **Build:** `1290`

**C# 签名:**
```csharp
void REMOVE_FAKE_CONE_DATA(int blip)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### REMOVE_MP_GAMER_TAG

- **命名空间:** `HUD`
- **Hash:** `0x31698AA80E0223F8`
- **JHash:** `0x3D081FE4`
- **Build:** `323`

**C# 签名:**
```csharp
void REMOVE_MP_GAMER_TAG(int gamerTagId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### REMOVE_MULTIPLAYER_BANK_CASH

- **命名空间:** `HUD`
- **Hash:** `0xC7C6789AA1CFEDD0`
- **JHash:** `0x728B4EF4`
- **Build:** `323`

**C# 签名:**
```csharp
void REMOVE_MULTIPLAYER_BANK_CASH()
```

**返回值:** `void` (Native: `void`)

---

### REMOVE_MULTIPLAYER_HUD_CASH

- **命名空间:** `HUD`
- **Hash:** `0x968F270E39141ECA`
- **JHash:** `0x07BF4A7D`
- **Build:** `323`

**C# 签名:**
```csharp
void REMOVE_MULTIPLAYER_HUD_CASH()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Removes multiplayer cash hud each frame
```

---

### REMOVE_MULTIPLAYER_WALLET_CASH

- **命名空间:** `HUD`
- **Hash:** `0x95CF81BD06EE1887`
- **JHash:** `0x7BFFE82F`
- **Build:** `323`

**C# 签名:**
```csharp
void REMOVE_MULTIPLAYER_WALLET_CASH()
```

**返回值:** `void` (Native: `void`)

---

### REMOVE_WARNING_MESSAGE_OPTION_ITEMS

- **命名空间:** `HUD`
- **Hash:** `0x6EF54AB721DC6242`
- **Build:** `323`

**C# 签名:**
```csharp
void REMOVE_WARNING_MESSAGE_OPTION_ITEMS()
```

**返回值:** `void` (Native: `void`)

---

### REPLACE_HUD_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x1CCC708F0F850613`
- **JHash:** `0x3B216749`
- **Build:** `323`

**C# 签名:**
```csharp
void REPLACE_HUD_COLOUR(int hudColorIndex, int hudColorIndex2)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudColorIndex` | `int` | `int` | - |
| `hudColorIndex2` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
makes hudColorIndex2 color into hudColorIndex color
```

---

### REPLACE_HUD_COLOUR_WITH_RGBA

- **命名空间:** `HUD`
- **Hash:** `0xF314CF4F0211894E`
- **JHash:** `0xF6E7E92B`
- **Build:** `323`

**C# 签名:**
```csharp
void REPLACE_HUD_COLOUR_WITH_RGBA(int hudColorIndex, int r, int g, int b, int a)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudColorIndex` | `int` | `int` | - |
| `r` | `int` | `int` | - |
| `g` | `int` | `int` | - |
| `b` | `int` | `int` | - |
| `a` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### REQUEST_ADDITIONAL_TEXT

- **命名空间:** `HUD`
- **Hash:** `0x71A78003C8E71424`
- **JHash:** `0x9FA9175B`
- **Build:** `323`

**C# 签名:**
```csharp
void REQUEST_ADDITIONAL_TEXT(string gxt, int slot)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxt` | `const char*` | `string` | - |
| `slot` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Request a gxt into the passed slot.
```

---

### REQUEST_ADDITIONAL_TEXT_FOR_DLC

- **命名空间:** `HUD`
- **Hash:** `0x6009F9F1AE90D8A6`
- **JHash:** `0xF4D27EBE`
- **Build:** `323`

**C# 签名:**
```csharp
void REQUEST_ADDITIONAL_TEXT_FOR_DLC(string gxt, int slot)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gxt` | `const char*` | `string` | - |
| `slot` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### RESET_GLOBAL_ACTIONSCRIPT_FLAG

- **命名空间:** `HUD`
- **Hash:** `0xB99C4E4D9499DF29`
- **JHash:** `0x4C4C10CF`
- **Build:** `323`

**C# 签名:**
```csharp
void RESET_GLOBAL_ACTIONSCRIPT_FLAG(int flagIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `flagIndex` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### RESET_HUD_COMPONENT_VALUES

- **命名空间:** `HUD`
- **Hash:** `0x450930E616475D0D`
- **JHash:** `0xD15B46DA`
- **Build:** `323`

**C# 签名:**
```csharp
void RESET_HUD_COMPONENT_VALUES(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### RESET_RETICULE_VALUES

- **命名空间:** `HUD`
- **Hash:** `0x12782CE0A636E9F0`
- **JHash:** `0xBE27AA3F`
- **Build:** `323`

**C# 签名:**
```csharp
void RESET_RETICULE_VALUES()
```

**返回值:** `void` (Native: `void`)

---

### RESTART_FRONTEND_MENU

- **命名空间:** `HUD`
- **Hash:** `0x10706DC6AD2D49C0`
- **JHash:** `0xB07DAF98`
- **Build:** `323`

**C# 签名:**
```csharp
void RESTART_FRONTEND_MENU(uint menuHash, int p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `menuHash` | `Hash` | `uint` | - |
| `p1` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Before using this native click the native above and look at the decription.

Example:
int GetHash = Function.Call<int>(Hash.GET_HASH_KEY, "fe_menu_version_corona_lobby");
Function.Call(Hash.ACTIVATE_FRONTEND_MENU, GetHash, 0, -1);
Function.Call(Hash.RESTART_FRONTEND_MENU(GetHash, -1);

This native refreshes the frontend menu.

p1 = Hash of Menu
p2 = Unknown but always works with -1.
```

---

### SCRIPT_IS_MOVING_MOBILE_PHONE_OFFSCREEN

- **命名空间:** `MOBILE`
- **Hash:** `0xF511F759238A5122`
- **JHash:** `0x29828690`
- **Build:** `323`

**C# 签名:**
```csharp
void SCRIPT_IS_MOVING_MOBILE_PHONE_OFFSCREEN(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
If bool Toggle = true so the mobile is hide to screen.
If bool Toggle = false so the mobile is show to screen.
```

---

### SETUP_FAKE_CONE_DATA

- **命名空间:** `HUD`
- **Hash:** `0xF83D0FEBE75E62C9`
- **Build:** `1290`

**C# 签名:**
```csharp
void SETUP_FAKE_CONE_DATA(int blip, float p1, float p2, float p3, float p4, float p5, float p6, object p7, int p8)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `p1` | `float` | `float` | - |
| `p2` | `float` | `float` | - |
| `p3` | `float` | `float` | - |
| `p4` | `float` | `float` | - |
| `p5` | `float` | `float` | - |
| `p6` | `float` | `float` | - |
| `p7` | `Any` | `object` | - |
| `p8` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_ABILITY_BAR_VALUE

- **命名空间:** `HUD`
- **Hash:** `0x9969599CCFF5D85E`
- **JHash:** `0x24E53FD8`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_ABILITY_BAR_VALUE(float p0, float p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `float` | `float` | - |
| `p1` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_ABILITY_BAR_VISIBILITY

- **命名空间:** `HUD`
- **Hash:** `0x1DFEDD15019315A9`
- **Build:** `1493`

**C# 签名:**
```csharp
void SET_ABILITY_BAR_VISIBILITY(bool visible)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `visible` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_ALLOW_ABILITY_BAR

- **命名空间:** `HUD`
- **Hash:** `0x889329C80FE5963C`
- **Build:** `1868`

**C# 签名:**
```csharp
void SET_ALLOW_ABILITY_BAR(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_ALLOW_COMMA_ON_TEXT_INPUT

- **命名空间:** `HUD`
- **Hash:** `0x577599CCED639CA2`
- **Build:** `505`

**C# 签名:**
```csharp
void SET_ALLOW_COMMA_ON_TEXT_INPUT(object p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### SET_ALL_MP_GAMER_TAGS_VISIBILITY

- **命名空间:** `HUD`
- **Hash:** `0xEE76FF7E6A0166B0`
- **JHash:** `0x767DED29`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_ALL_MP_GAMER_TAGS_VISIBILITY(int gamerTagId, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BIGMAP_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x231C8F89D0539D8F`
- **JHash:** `0x08EB83D2`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BIGMAP_ACTIVE(bool toggleBigMap, bool showFullMap)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggleBigMap` | `BOOL` | `bool` | - |
| `showFullMap` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Toggles the big minimap state like in GTA:Online.
```

---

### SET_BLIP_ALPHA

- **命名空间:** `HUD`
- **Hash:** `0x45FF974EEE1C8734`
- **JHash:** `0xA791FCCD`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_ALPHA(int blip, int alpha)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `alpha` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets alpha-channel for blip color.

Example:

Blip blip = HUD::ADD_BLIP_FOR_ENTITY(entity);
HUD::SET_BLIP_COLOUR(blip , 3);
HUD::SET_BLIP_ALPHA(blip , 64);

```

---

### SET_BLIP_AS_FRIENDLY

- **命名空间:** `HUD`
- **Hash:** `0x6F6F290102C02AB4`
- **JHash:** `0xF290CFD8`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_AS_FRIENDLY(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
false for enemy
true for friendly
```

---

### SET_BLIP_AS_MINIMAL_ON_EDGE

- **命名空间:** `HUD`
- **Hash:** `0x2B6D467DAB714E8D`
- **JHash:** `0xC575F0BC`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_AS_MINIMAL_ON_EDGE(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Makes a blip go small when off the minimap.
```

---

### SET_BLIP_AS_MISSION_CREATOR_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x24AC0137444F9FD5`
- **JHash:** `0x802FB686`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_AS_MISSION_CREATOR_BLIP(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_AS_SHORT_RANGE

- **命名空间:** `HUD`
- **Hash:** `0xBE8BE4FE60E27B72`
- **JHash:** `0x5C67725E`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_AS_SHORT_RANGE(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets whether or not the specified blip should only be displayed when nearby, or on the minimap.
```

---

### SET_BLIP_BRIGHT

- **命名空间:** `HUD`
- **Hash:** `0xB203913733F27884`
- **JHash:** `0x72BEE6DF`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_BRIGHT(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_CATEGORY

- **命名空间:** `HUD`
- **Hash:** `0x234CDD44D996FD9A`
- **JHash:** `0xEF72F533`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_CATEGORY(int blip, int index)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `index` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Example: https://i.imgur.com/skY6vAJ.png

Index:
1 = No distance shown in legend
2 = Distance shown in legend
7 = "Other Players" category, also shows distance in legend
10 = "Property" category
11 = "Owned Property" category

Any other value behaves like index = 1, index wraps around after 255
Blips with categories 7, 10 or 11 will all show under the specific categories listing in the map legend, regardless of sprite or name.
Legend entries:
7 = Other Players (BLIP_OTHPLYR)
10 = Property (BLIP_PROPCAT)
11 = Owned Property (BLIP_APARTCAT)

Category needs to be `7` in order for blip names to show on the expanded minimap when using DISPLAY_PLAYER_NAME_TAGS_ON_BLIPS.
```

---

### SET_BLIP_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x03D7FB09E75D6B7E`
- **JHash:** `0xBB3C5A41`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_COLOUR(int blip, int color)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `color` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
https://gtaforums.com/topic/864881-all-blip-color-ids-pictured/
```

---

### SET_BLIP_COORDS

- **命名空间:** `HUD`
- **Hash:** `0xAE2AF67E9D9AF65D`
- **JHash:** `0x680A34D4`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_COORDS(int blip, float posX, float posY, float posZ)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `posX` | `float` | `float` | - |
| `posY` | `float` | `float` | - |
| `posZ` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_DISPLAY

- **命名空间:** `HUD`
- **Hash:** `0x9029B2F3DA924928`
- **JHash:** `0x2B521F91`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_DISPLAY(int blip, int displayId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `displayId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Display Id behaviours:
0 = Doesn't show up, ever, anywhere.
1 = Doesn't show up, ever, anywhere.
2 = Shows on both main map and minimap. (Selectable on map)
3 = Shows on main map only. (Selectable on map)
4 = Shows on main map only. (Selectable on map)
5 = Shows on minimap only.
6 = Shows on both main map and minimap. (Selectable on map)
7 = Doesn't show up, ever, anywhere.
8 = Shows on both main map and minimap. (Not selectable on map)
9 = Shows on minimap only.
10 = Shows on both main map and minimap. (Not selectable on map)

Anything higher than 10 seems to be exactly the same as 10.
```

---

### SET_BLIP_EXTENDED_HEIGHT_THRESHOLD

- **命名空间:** `HUD`
- **Hash:** `0xC4278F70131BAA6D`
- **JHash:** `0x6AA6A1CC`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_EXTENDED_HEIGHT_THRESHOLD(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Must be toggled before being queued for animation
```

---

### SET_BLIP_FADE

- **命名空间:** `HUD`
- **Hash:** `0x2AEE8F8390D2298C`
- **JHash:** `0xA5999031`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_FADE(int blip, int opacity, int duration)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `opacity` | `int` | `int` | - |
| `duration` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_FLASHES

- **命名空间:** `HUD`
- **Hash:** `0xB14552383D39CE3E`
- **JHash:** `0xC0047F15`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_FLASHES(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_FLASHES_ALTERNATE

- **命名空间:** `HUD`
- **Hash:** `0x2E8D9498C56DD0D1`
- **JHash:** `0x1A81202B`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_FLASHES_ALTERNATE(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_FLASH_INTERVAL

- **命名空间:** `HUD`
- **Hash:** `0xAA51DB313C010A7E`
- **JHash:** `0xEAF67377`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_FLASH_INTERVAL(int blip, object p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `p1` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_FLASH_TIMER

- **命名空间:** `HUD`
- **Hash:** `0xD3CD6FD297AE87CC`
- **JHash:** `0x8D5DF611`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_FLASH_TIMER(int blip, int duration)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `duration` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Adds up after viewing multiple R* scripts. I believe that the duration is in miliseconds.
```

---

### SET_BLIP_HIDDEN_ON_LEGEND

- **命名空间:** `HUD`
- **Hash:** `0x54318C915D27E4CE`
- **JHash:** `0x43996428`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_HIDDEN_ON_LEGEND(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_HIGH_DETAIL

- **命名空间:** `HUD`
- **Hash:** `0xE2590BC29220CEBB`
- **JHash:** `0xD5842BFF`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_HIGH_DETAIL(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_MARKER_LONG_DISTANCE

- **命名空间:** `HUD`
- **Hash:** `0xB552929B85FC27EC`
- **Build:** `573`

**C# 签名:**
```csharp
void SET_BLIP_MARKER_LONG_DISTANCE(object p0, object p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |
| `p1` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_NAME_FROM_TEXT_FILE

- **命名空间:** `HUD`
- **Hash:** `0xEAA0FFE120D92784`
- **JHash:** `0xAC8A5461`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_NAME_FROM_TEXT_FILE(int blip, string gxtEntry)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `gxtEntry` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Doesn't work if the label text of gxtEntry is >= 80.
```

---

### SET_BLIP_NAME_TO_PLAYER_NAME

- **命名空间:** `HUD`
- **Hash:** `0x127DE7B20C60A6A3`
- **JHash:** `0x03A0B8F9`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_NAME_TO_PLAYER_NAME(int blip, int player)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `player` | `Player` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_PRIORITY

- **命名空间:** `HUD`
- **Hash:** `0xAE9FC9EF6A9FAC79`
- **JHash:** `0xCE87DA6F`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_PRIORITY(int blip, int priority)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `priority` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
See this topic for more details : gtaforums.com/topic/717612-v-scriptnative-documentation-and-research/page-35?p=1069477935
```

---

### SET_BLIP_ROTATION

- **命名空间:** `HUD`
- **Hash:** `0xF87683CDF73C3F6E`
- **JHash:** `0x6B8F44FE`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_ROTATION(int blip, int rotation)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `rotation` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
After some testing, looks like you need to use CEIL() on the rotation (vehicle/ped heading) before using it there.
```

---

### SET_BLIP_ROTATION_WITH_FLOAT

- **命名空间:** `HUD`
- **Hash:** `0xA8B6AFDAC320AC87`
- **Build:** `877`

**C# 签名:**
```csharp
void SET_BLIP_ROTATION_WITH_FLOAT(int blip, float heading)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `heading` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Does not require whole number/integer rotations.
```

---

### SET_BLIP_ROUTE

- **命名空间:** `HUD`
- **Hash:** `0x4F7D8A9BFB0B43E9`
- **JHash:** `0x3E160C90`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_ROUTE(int blip, bool enabled)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `enabled` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Enable / disable showing route for the Blip-object.
```

---

### SET_BLIP_ROUTE_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x837155CD2F63DA09`
- **JHash:** `0xDDE7C65C`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_ROUTE_COLOUR(int blip, int colour)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `colour` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_SCALE

- **命名空间:** `HUD`
- **Hash:** `0xD38744167B2FA257`
- **JHash:** `0x1E6EC434`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_SCALE(int blip, float scale)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `scale` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_SCALE_2D

- **命名空间:** `HUD`
- **Hash:** `0xCD6524439909C979`
- **Build:** `1734`

**C# 签名:**
```csharp
void SET_BLIP_SCALE_2D(int blip, float xScale, float yScale)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `xScale` | `float` | `float` | - |
| `yScale` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
See https://imgur.com/a/lLkEsMN
```

---

### SET_BLIP_SECONDARY_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x14892474891E09EB`
- **JHash:** `0xC6384D32`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_SECONDARY_COLOUR(int blip, int r, int g, int b)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `r` | `int` | `int` | - |
| `g` | `int` | `int` | - |
| `b` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Can be used to give blips any RGB colour with SET_BLIP_COLOUR(blip, 84).
```

---

### SET_BLIP_SHORT_HEIGHT_THRESHOLD

- **命名空间:** `HUD`
- **Hash:** `0x4B5B620C9B59ED34`
- **Build:** `678`

**C# 签名:**
```csharp
void SET_BLIP_SHORT_HEIGHT_THRESHOLD(object p0, object p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |
| `p1` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLIP_SHOW_CONE

- **命名空间:** `HUD`
- **Hash:** `0x13127EC3665E8EE1`
- **JHash:** `0xFF545AD8`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_SHOW_CONE(int blip, bool toggle, int hudColorIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |
| `hudColorIndex` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
As of b2189, the third parameter sets the color of the cone (before b2189 it was ignored). Note that it uses HUD colors, not blip colors.
```

---

### SET_BLIP_SPRITE

- **命名空间:** `HUD`
- **Hash:** `0xDF735600A4696DAF`
- **JHash:** `0x8DBBB0B9`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_BLIP_SPRITE(int blip, int spriteId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `spriteId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets the displayed sprite for a specific blip..

You may have your own list, but since dev-c didn't show it I was bored and started looking through scripts and functions to get a presumable almost positive list of a majority of blip IDs
https://pastebin.com/Bpj9Sfft

Blips Images + IDs:
https://gtaxscripting.blogspot.com/2016/05/gta-v-blips-id-and-image.html
```

---

### SET_BLIP_USE_HEIGHT_INDICATOR_ON_EDGE

- **命名空间:** `HUD`
- **Hash:** `0x2C9F302398E13141`
- **JHash:** `0xCA95C487`
- **Build:** `1103`

**C# 签名:**
```csharp
void SET_BLIP_USE_HEIGHT_INDICATOR_ON_EDGE(int blip, object p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `p1` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### SET_BLOCK_WANTED_FLASH

- **命名空间:** `HUD`
- **Hash:** `0xD1942374085C8469`
- **Build:** `505`

**C# 签名:**
```csharp
void SET_BLOCK_WANTED_FLASH(bool disabled)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `disabled` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_COLOUR_OF_NEXT_TEXT_COMPONENT

- **命名空间:** `HUD`
- **Hash:** `0x39BBF623FC803EAC`
- **JHash:** `0x6F1A1901`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_COLOUR_OF_NEXT_TEXT_COMPONENT(int hudColor)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudColor` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_COP_BLIP_SPRITE

- **命名空间:** `HUD`
- **Hash:** `0x9FCB3CBFB3EAD69A`
- **Build:** `1734`

**C# 签名:**
```csharp
void SET_COP_BLIP_SPRITE(int p0, float p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |
| `p1` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_COP_BLIP_SPRITE_AS_STANDARD

- **命名空间:** `HUD`
- **Hash:** `0xB7B873520C84C118`
- **Build:** `1734`

**C# 签名:**
```csharp
void SET_COP_BLIP_SPRITE_AS_STANDARD()
```

**返回值:** `void` (Native: `void`)

---

### SET_CUSTOM_MP_HUD_COLOR

- **命名空间:** `HUD`
- **Hash:** `0x2ACCB195F3CCD9DE`
- **Build:** `2545`

**C# 签名:**
```csharp
void SET_CUSTOM_MP_HUD_COLOR(int hudColorId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudColorId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_DESCRIPTION_FOR_UGC_MISSION_EIGHT_STRINGS

- **命名空间:** `HUD`
- **Hash:** `0x817B86108EB94E51`
- **JHash:** `0xD2161E77`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_DESCRIPTION_FOR_UGC_MISSION_EIGHT_STRINGS(bool p0, string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |
| `p1` | `const char*` | `string` | - |
| `p2` | `const char*` | `string` | - |
| `p3` | `const char*` | `string` | - |
| `p4` | `const char*` | `string` | - |
| `p5` | `const char*` | `string` | - |
| `p6` | `const char*` | `string` | - |
| `p7` | `const char*` | `string` | - |
| `p8` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### SET_DIRECTOR_MODE_AVAILABLE

- **命名空间:** `HUD`
- **Hash:** `0x04655F9D075D0AE5`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_DIRECTOR_MODE_AVAILABLE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_DIRECTOR_MODE_LAUNCHED_BY_SCRIPT

- **命名空间:** `HUD`
- **Hash:** `0x2632482FD6B9AB87`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_DIRECTOR_MODE_LAUNCHED_BY_SCRIPT()
```

**返回值:** `void` (Native: `void`)

---

### SET_FAKE_GPS_PLAYER_POSITION_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xA17784FCA9548D15`
- **Build:** `877`

**C# 签名:**
```csharp
void SET_FAKE_GPS_PLAYER_POSITION_THIS_FRAME(float x, float y, float z)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_FAKE_MINIMAP_MAX_ALTIMETER_HEIGHT

- **命名空间:** `HUD`
- **Hash:** `0xD201F3FF917A506D`
- **JHash:** `0x0308EDF6`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_FAKE_MINIMAP_MAX_ALTIMETER_HEIGHT(float altitude, bool p1, object p2)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `altitude` | `float` | `float` | - |
| `p1` | `BOOL` | `bool` | - |
| `p2` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Argument must be 0.0f or above 38.0f, or it will be ignored.
```

---

### SET_FAKE_PAUSEMAP_PLAYER_POSITION_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x77E2DD177910E1CF`
- **JHash:** `0x54E75C7D`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_FAKE_PAUSEMAP_PLAYER_POSITION_THIS_FRAME(float x, float y)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets the position of the arrow icon representing the player on both the minimap and world map.

Too bad this wouldn't work over the network (obviously not). Could spoof where we would be.
```

---

### SET_FAKE_SPECTATOR_MODE

- **命名空间:** `HUD`
- **Hash:** `0xCD74233600C4EA6B`
- **Build:** `505`

**C# 签名:**
```csharp
void SET_FAKE_SPECTATOR_MODE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Setter for GET_FAKE_SPECTATOR_MODE
```

---

### SET_FLOATING_HELP_TEXT_SCREEN_POSITION

- **命名空间:** `HUD`
- **Hash:** `0x7679CC1BCEBE3D4C`
- **JHash:** `0x198F32D7`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_FLOATING_HELP_TEXT_SCREEN_POSITION(int hudIndex, float x, float y)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudIndex` | `int` | `int` | - |
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_FLOATING_HELP_TEXT_STYLE

- **命名空间:** `HUD`
- **Hash:** `0x788E7FD431BD67F1`
- **JHash:** `0x97852A82`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_FLOATING_HELP_TEXT_STYLE(int hudIndex, int p1, int p2, int p3, int p4, int p5)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudIndex` | `int` | `int` | - |
| `p1` | `int` | `int` | - |
| `p2` | `int` | `int` | - |
| `p3` | `int` | `int` | - |
| `p4` | `int` | `int` | - |
| `p5` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_FLOATING_HELP_TEXT_TO_ENTITY

- **命名空间:** `HUD`
- **Hash:** `0xB094BC1DB4018240`
- **JHash:** `0x18B012B7`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_FLOATING_HELP_TEXT_TO_ENTITY(int hudIndex, int entity, float offsetX, float offsetY)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudIndex` | `int` | `int` | - |
| `entity` | `Entity` | `int` | - |
| `offsetX` | `float` | `float` | - |
| `offsetY` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_FLOATING_HELP_TEXT_WORLD_POSITION

- **命名空间:** `HUD`
- **Hash:** `0x784BA7E0ECEB4178`
- **JHash:** `0x93045157`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_FLOATING_HELP_TEXT_WORLD_POSITION(int hudIndex, float x, float y, float z)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudIndex` | `int` | `int` | - |
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_FORCE_SHOW_GPS

- **命名空间:** `HUD`
- **Hash:** `0x2790F4B17D098E26`
- **Build:** `573`

**C# 签名:**
```csharp
void SET_FORCE_SHOW_GPS(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_FRONTEND_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x745711A75AB09277`
- **JHash:** `0x81E1AD32`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_FRONTEND_ACTIVE(bool active)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `active` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_GPS_CUSTOM_ROUTE_RENDER

- **命名空间:** `HUD`
- **Hash:** `0x900086F371220B6F`
- **JHash:** `0xDA0AF00E`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_GPS_CUSTOM_ROUTE_RENDER(bool toggle, int radarThickness, int mapThickness)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |
| `radarThickness` | `int` | `int` | - |
| `mapThickness` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
radarThickness: The width of the GPS route on the radar
mapThickness: The width of the GPS route on the map
```

---

### SET_GPS_FLAGS

- **命名空间:** `HUD`
- **Hash:** `0x5B440763A4C8D15B`
- **JHash:** `0x60539BAB`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_GPS_FLAGS(int p0, float p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |
| `p1` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Only the script that originally called SET_GPS_FLAGS can set them again. Another script cannot set the flags, until the first script that called it has called CLEAR_GPS_FLAGS.

Doesn't seem like the flags are actually read by the game at all.
```

---

### SET_GPS_FLASHES

- **命名空间:** `HUD`
- **Hash:** `0x320D0E0D936A0E9B`
- **JHash:** `0xE991F733`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_GPS_FLASHES(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_GPS_MULTI_ROUTE_RENDER

- **命名空间:** `HUD`
- **Hash:** `0x3DDA37128DD1ACA8`
- **JHash:** `0xE87CBE4C`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_GPS_MULTI_ROUTE_RENDER(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_HEALTH_HUD_DISPLAY_VALUES

- **命名空间:** `HUD`
- **Hash:** `0x3F5CC444DCAAA8F2`
- **JHash:** `0x7FB6FB2A`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_HEALTH_HUD_DISPLAY_VALUES(int health, int capacity, bool wasAdded)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `health` | `int` | `int` | - |
| `capacity` | `int` | `int` | - |
| `wasAdded` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_HELP_MESSAGE_STYLE

- **命名空间:** `HUD`
- **Hash:** `0xB9C362BABECDDC7A`
- **Build:** `463`

**C# 签名:**
```csharp
void SET_HELP_MESSAGE_STYLE(int style, int hudColor, int alpha, int p3, int p4)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `style` | `int` | `int` | - |
| `hudColor` | `int` | `int` | - |
| `alpha` | `int` | `int` | - |
| `p3` | `int` | `int` | - |
| `p4` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_HUD_COMPONENT_POSITION

- **命名空间:** `HUD`
- **Hash:** `0xAABB1F56E2A17CED`
- **JHash:** `0x2F3A0D15`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_HUD_COMPONENT_POSITION(int id, float x, float y)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_INSIDE_VERY_LARGE_INTERIOR

- **命名空间:** `HUD`
- **Hash:** `0x7EC8ABA5E74B3D7A`
- **Build:** `2372`

**C# 签名:**
```csharp
void SET_INSIDE_VERY_LARGE_INTERIOR(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_INSIDE_VERY_SMALL_INTERIOR

- **命名空间:** `HUD`
- **Hash:** `0x504DFE62A1692296`
- **Build:** `1493`

**C# 签名:**
```csharp
void SET_INSIDE_VERY_SMALL_INTERIOR(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MAX_ARMOUR_HUD_DISPLAY

- **命名空间:** `HUD`
- **Hash:** `0x06A320535F5F0248`
- **JHash:** `0x827F14DE`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MAX_ARMOUR_HUD_DISPLAY(int maximumValue)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `maximumValue` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MAX_HEALTH_HUD_DISPLAY

- **命名空间:** `HUD`
- **Hash:** `0x975D66A0BC17064C`
- **JHash:** `0xF07D8CEF`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MAX_HEALTH_HUD_DISPLAY(int maximumValue)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `maximumValue` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MINIMAP_BACKGROUND_HIDDEN

- **命名空间:** `HUD`
- **Hash:** `0xB09D42557C45EBA1`
- **Build:** `3258`

**C# 签名:**
```csharp
void SET_MINIMAP_BACKGROUND_HIDDEN(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
This native does absolutely nothing on PC master builds, just a nullsub.
```

---

### SET_MINIMAP_BLOCK_WAYPOINT

- **命名空间:** `HUD`
- **Hash:** `0x58FADDED207897DC`
- **JHash:** `0xA41C3B62`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MINIMAP_BLOCK_WAYPOINT(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MINIMAP_COMPONENT

- **命名空间:** `HUD`
- **Hash:** `0x75A9A10948D1DEA6`
- **JHash:** `0x419DCDC4`
- **Build:** `323`

**C# 签名:**
```csharp
bool SET_MINIMAP_COMPONENT(int componentId, bool toggle, int overrideColor)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `componentId` | `int` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |
| `overrideColor` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
This native is used to colorize certain map components like the army base at the top of the map.
p2 appears to be always -1. If p2 is -1 then native wouldn't change the color. See https://gfycat.com/SkinnyPinkChupacabra
```

---

### SET_MINIMAP_FOW_DO_NOT_UPDATE

- **命名空间:** `HUD`
- **Hash:** `0x62E849B7EB28E770`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MINIMAP_FOW_DO_NOT_UPDATE(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MINIMAP_FOW_REVEAL_COORDINATE

- **命名空间:** `HUD`
- **Hash:** `0x0923DBF87DFF735E`
- **JHash:** `0xE010F081`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MINIMAP_FOW_REVEAL_COORDINATE(float x, float y, float z)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Up to eight coordinates may be revealed per frame
```

---

### SET_MINIMAP_GOLF_COURSE

- **命名空间:** `HUD`
- **Hash:** `0x71BDB63DBAF8DA59`
- **JHash:** `0x5133A750`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MINIMAP_GOLF_COURSE(int hole)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hole` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Not much is known so far on what it does _exactly_.
All I know for sure is that it draws the specified hole ID on the pause menu map as well as on the mini-map/radar. This native also seems to change some other things related to the pause menu map's behaviour, for example: you can no longer set waypoints, the pause menu map starts up in a 'zoomed in' state. This native does not need to be executed every tick.
You need to center the minimap manually as well as change/lock it's zoom and angle in order for it to appear correctly on the minimap.
You'll also need to use the `GOLF` scaleform in order to get the correct minmap border to show up.
Use `0x35edd5b2e3ff01c0` to reset the map when you no longer want to display any golf holes (you still need to unlock zoom, position and angle of the radar manually after calling this).
```

---

### SET_MINIMAP_GOLF_COURSE_OFF

- **命名空间:** `HUD`
- **Hash:** `0x35EDD5B2E3FF01C0`
- **JHash:** `0x20FD3E87`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MINIMAP_GOLF_COURSE_OFF()
```

**返回值:** `void` (Native: `void`)

---

### SET_MINIMAP_HIDE_FOW

- **命名空间:** `HUD`
- **Hash:** `0xF8DEE0A5600CBB93`
- **JHash:** `0xD8D77733`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MINIMAP_HIDE_FOW(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
If true, the entire map will be revealed.

FOW = Fog of War
```

---

### SET_MINIMAP_IN_PROLOGUE

- **命名空间:** `HUD`
- **Hash:** `0x9133955F1A2DA957`
- **JHash:** `0x02F5F1D1`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MINIMAP_IN_PROLOGUE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Toggles the North Yankton map
```

---

### SET_MINIMAP_IN_SPECTATOR_MODE

- **命名空间:** `HUD`
- **Hash:** `0x1A5CD7752DD28CD3`
- **JHash:** `0xD5BFCADB`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MINIMAP_IN_SPECTATOR_MODE(bool toggle, int ped)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |
| `ped` | `Ped` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MINIMAP_SONAR_SWEEP

- **命名空间:** `HUD`
- **Hash:** `0x6B50FC8749632EC1`
- **Build:** `2189`

**C# 签名:**
```csharp
void SET_MINIMAP_SONAR_SWEEP(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MISSION_NAME

- **命名空间:** `HUD`
- **Hash:** `0x5F28ECF5FC84772F`
- **JHash:** `0x68DCAE10`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MISSION_NAME(bool p0, string name)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |
| `name` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MISSION_NAME_FOR_UGC_MISSION

- **命名空间:** `HUD`
- **Hash:** `0xE45087D85F468BC2`
- **JHash:** `0x8D9A1734`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MISSION_NAME_FOR_UGC_MISSION(bool p0, string name)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |
| `name` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MOBILE_PHONE_DOF_STATE

- **命名空间:** `MOBILE`
- **Hash:** `0x375A706A5C2FD084`
- **Build:** `372`

**C# 签名:**
```csharp
void SET_MOBILE_PHONE_DOF_STATE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MOBILE_PHONE_POSITION

- **命名空间:** `MOBILE`
- **Hash:** `0x693A5C6D6734085B`
- **JHash:** `0x841800B3`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MOBILE_PHONE_POSITION(float posX, float posY, float posZ)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `posX` | `float` | `float` | - |
| `posY` | `float` | `float` | - |
| `posZ` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MOBILE_PHONE_ROTATION

- **命名空间:** `MOBILE`
- **Hash:** `0xBB779C0CA917E865`
- **JHash:** `0x209C28CF`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MOBILE_PHONE_ROTATION(float rotX, float rotY, float rotZ, object p3)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `rotX` | `float` | `float` | - |
| `rotY` | `float` | `float` | - |
| `rotZ` | `float` | `float` | - |
| `p3` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Last parameter is unknown and always zero.
```

---

### SET_MOBILE_PHONE_SCALE

- **命名空间:** `MOBILE`
- **Hash:** `0xCBDD322A73D6D932`
- **JHash:** `0x09BCF1BE`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MOBILE_PHONE_SCALE(float scale)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `scale` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
The minimum/default is 500.0f. If you plan to make it bigger set it's position as well. Also this seems to need to be called in a loop as when you close the phone the scale is reset. If not in a loop you'd need to call it everytime before you re-open the phone.
```

---

### SET_MOUSE_CURSOR_STYLE

- **命名空间:** `HUD`
- **Hash:** `0x8DB8CFFD58B62552`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MOUSE_CURSOR_STYLE(int spriteId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `spriteId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Changes the mouse cursor's sprite. 
1 = Normal
6 = Left Arrow
7 = Right Arrow
```

---

### SET_MOUSE_CURSOR_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xAAE7CE1D63167423`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MOUSE_CURSOR_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Shows the cursor on screen for one frame.
```

---

### SET_MOUSE_CURSOR_VISIBLE

- **命名空间:** `HUD`
- **Hash:** `0x98215325A695E78A`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MOUSE_CURSOR_VISIBLE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Shows/hides the frontend cursor on the pause menu or similar menus.
Clicking off and then on the game window will show it again.
```

---

### SET_MP_GAMER_TAGS_POINT_HEALTH

- **命名空间:** `HUD`
- **Hash:** `0x1563FE35E9928E67`
- **Build:** `1365`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAGS_POINT_HEALTH(int gamerTagId, int value, int maximumValue)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `value` | `int` | `int` | - |
| `maximumValue` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MP_GAMER_TAGS_SHOULD_USE_POINTS_HEALTH

- **命名空间:** `HUD`
- **Hash:** `0xD29EC58C2F6B5014`
- **Build:** `1365`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAGS_SHOULD_USE_POINTS_HEALTH(int gamerTagId, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MP_GAMER_TAGS_SHOULD_USE_VEHICLE_HEALTH

- **命名空间:** `HUD`
- **Hash:** `0xA67F9C46D612B6F1`
- **JHash:** `0xB01A5434`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAGS_SHOULD_USE_VEHICLE_HEALTH(int gamerTagId, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Displays a bunch of icons above the players name, and level, and their name twice
```

---

### SET_MP_GAMER_TAG_ALPHA

- **命名空间:** `HUD`
- **Hash:** `0xD48FE545CD46F857`
- **JHash:** `0xF4418611`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAG_ALPHA(int gamerTagId, int component, int alpha)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `component` | `int` | `int` | - |
| `alpha` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets flag's sprite transparency. 0-255.
```

---

### SET_MP_GAMER_TAG_BIG_TEXT

- **命名空间:** `HUD`
- **Hash:** `0x7B7723747CCB55B6`
- **JHash:** `0x939218AB`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAG_BIG_TEXT(int gamerTagId, string string)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `string` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MP_GAMER_TAG_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x613ED644950626AE`
- **JHash:** `0x7E3AA40A`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAG_COLOUR(int gamerTagId, int component, int hudColorIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `component` | `int` | `int` | - |
| `hudColorIndex` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets a gamer tag's component colour

gamerTagId is obtained using for example CREATE_FAKE_MP_GAMER_TAG
Ranges from 0 to 255. 0 is grey health bar, ~50 yellow, 200 purple.
```

---

### SET_MP_GAMER_TAG_HEALTH_BAR_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x3158C77A7E888AB4`
- **JHash:** `0x5777EC77`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAG_HEALTH_BAR_COLOUR(int gamerTagId, int hudColorIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `hudColorIndex` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Ranges from 0 to 255. 0 is grey health bar, ~50 yellow, 200 purple.
Should be enabled as flag (2). Has 0 opacity by default.

- This was _SET_MP_GAMER_TAG_HEALTH_BAR_COLOR,
-> Rockstar use the EU spelling of 'color' so I hashed the same name with COLOUR and it came back as the correct hash, so it has been corrected above.
```

---

### SET_MP_GAMER_TAG_NAME

- **命名空间:** `HUD`
- **Hash:** `0xDEA2B8283BAA3944`
- **JHash:** `0x627A559B`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAG_NAME(int gamerTagId, string string)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `string` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MP_GAMER_TAG_NUM_PACKAGES

- **命名空间:** `HUD`
- **Hash:** `0x9C16459B2324B2CF`
- **Build:** `877`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAG_NUM_PACKAGES(int gamerTagId, int p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `p1` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_MP_GAMER_TAG_VISIBILITY

- **命名空间:** `HUD`
- **Hash:** `0x63BB75ABEDC1F6A0`
- **JHash:** `0xD41DF479`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAG_VISIBILITY(int gamerTagId, int component, bool toggle, object p3)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `component` | `int` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |
| `p3` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
enum eMpGamerTagComponent
{
	MP_TAG_GAMER_NAME,
	MP_TAG_CREW_TAG,
	MP_TAG_HEALTH_ARMOUR,
	MP_TAG_BIG_TEXT,
	MP_TAG_AUDIO_ICON,
	MP_TAG_USING_MENU,
	MP_TAG_PASSIVE_MODE,
	MP_TAG_WANTED_STARS,
	MP_TAG_DRIVER,
	MP_TAG_CO_DRIVER,
	MP_TAG_TAGGED,
	MP_TAG_GAMER_NAME_NEARBY,
	MP_TAG_ARROW,
	MP_TAG_PACKAGES,
	MP_TAG_INV_IF_PED_FOLLOWING,
	MP_TAG_RANK_TEXT,
	MP_TAG_TYPING,
	MP_TAG_BAG_LARGE,
	MP_TAG_ARROW,
	MP_TAG_GANG_CEO,
	MP_TAG_GANG_BIKER,
	MP_TAG_BIKER_ARROW,
	MP_TAG_MC_ROLE_PRESIDENT,
	MP_TAG_MC_ROLE_VICE_PRESIDENT,
	MP_TAG_MC_ROLE_ROAD_CAPTAIN,
	MP_TAG_MC_ROLE_SARGEANT,
	MP_TAG_MC_ROLE_ENFORCER,
	MP_TAG_MC_ROLE_PROSPECT,
	MP_TAG_TRANSMITTER,
	MP_TAG_BOMB
};
```

---

### SET_MP_GAMER_TAG_WANTED_LEVEL

- **命名空间:** `HUD`
- **Hash:** `0xCF228E2AA03099C3`
- **JHash:** `0x0EBB003F`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MP_GAMER_TAG_WANTED_LEVEL(int gamerTagId, int wantedlvl)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `gamerTagId` | `int` | `int` | - |
| `wantedlvl` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
displays wanted star above head
```

---

### SET_MULTIPLAYER_BANK_CASH

- **命名空间:** `HUD`
- **Hash:** `0xDD21B55DF695CD0A`
- **JHash:** `0x2C842D03`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MULTIPLAYER_BANK_CASH()
```

**返回值:** `void` (Native: `void`)

---

### SET_MULTIPLAYER_HUD_CASH

- **命名空间:** `HUD`
- **Hash:** `0xFD1D220394BCB824`
- **JHash:** `0xA8DB435E`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MULTIPLAYER_HUD_CASH(int p0, bool p1)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |
| `p1` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
This native does absolutely nothing, just a nullsub
```

---

### SET_MULTIPLAYER_WALLET_CASH

- **命名空间:** `HUD`
- **Hash:** `0xC2D15BEF167E27BC`
- **JHash:** `0xF4F3C796`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_MULTIPLAYER_WALLET_CASH()
```

**返回值:** `void` (Native: `void`)

---

### SET_NEW_WAYPOINT

- **命名空间:** `HUD`
- **Hash:** `0xFE43368D2AA4F2FC`
- **JHash:** `0x8444E1F0`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_NEW_WAYPOINT(float x, float y)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_PAUSE_MENU_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0xDF47FC56C71569CF`
- **JHash:** `0x1DCD878E`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PAUSE_MENU_ACTIVE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_PAUSE_MENU_PED_LIGHTING

- **命名空间:** `HUD`
- **Hash:** `0x3CA6050692BC61B0`
- **JHash:** `0x127310EB`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PAUSE_MENU_PED_LIGHTING(bool state)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `state` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Toggles the light state for the pause menu ped in frontend menus.

This is used by R* in combination with `SET_PAUSE_MENU_PED_SLEEP_STATE` to toggle the "offline" or "online" state in the "friends" tab of the pause menu in GTA Online.


Example:
Lights On: https://vespura.com/hi/i/2019-04-01_16-09_540ee_1015.png
Lights Off: https://vespura.com/hi/i/2019-04-01_16-10_8b5e7_1016.png
```

---

### SET_PAUSE_MENU_PED_SLEEP_STATE

- **命名空间:** `HUD`
- **Hash:** `0xECF128344E9FF9F1`
- **JHash:** `0x8F45D327`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PAUSE_MENU_PED_SLEEP_STATE(bool state)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `state` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Toggles the pause menu ped sleep state for frontend menus.

Example: https://vespura.com/hi/i/2019-04-01_15-51_8ed38_1014.gif

`state` 0 will make the ped slowly fall asleep, 1 will slowly wake the ped up.
```

---

### SET_PED_AI_BLIP_FORCED_ON

- **命名空间:** `HUD`
- **Hash:** `0x0C4BBF625CA98C4E`
- **JHash:** `0xFFDF46F0`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PED_AI_BLIP_FORCED_ON(int ped, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_PED_AI_BLIP_GANG_ID

- **命名空间:** `HUD`
- **Hash:** `0xE52B8E7F85D39A08`
- **JHash:** `0xD8E31B1A`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PED_AI_BLIP_GANG_ID(int ped, int gangId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |
| `gangId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_PED_AI_BLIP_HAS_CONE

- **命名空间:** `HUD`
- **Hash:** `0x3EED80DFF7325CAA`
- **JHash:** `0x872C2CFB`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PED_AI_BLIP_HAS_CONE(int ped, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_PED_AI_BLIP_NOTICE_RANGE

- **命名空间:** `HUD`
- **Hash:** `0x97C65887D4B37FA9`
- **JHash:** `0xF9DC2AF7`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PED_AI_BLIP_NOTICE_RANGE(int ped, float range)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |
| `range` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_PED_AI_BLIP_SPRITE

- **命名空间:** `HUD`
- **Hash:** `0xFCFACD0DB9D7A57D`
- **Build:** `877`

**C# 签名:**
```csharp
void SET_PED_AI_BLIP_SPRITE(int ped, int spriteId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |
| `spriteId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_PED_HAS_AI_BLIP

- **命名空间:** `HUD`
- **Hash:** `0xD30C50DF888D58B5`
- **JHash:** `0x96C4C4DD`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PED_HAS_AI_BLIP(int ped, bool hasCone)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |
| `hasCone` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
This native turns on the AI blip on the specified ped. It also disappears automatically when the ped is too far or if the ped is dead. You don't need to control it with other natives.

See gtaforums.com/topic/884370-native-research-ai-blips for further information.
```

---

### SET_PED_HAS_AI_BLIP_WITH_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0xB13DCB4C6FAAD238`
- **Build:** `505`

**C# 签名:**
```csharp
void SET_PED_HAS_AI_BLIP_WITH_COLOUR(int ped, bool hasCone, int color)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `ped` | `Ped` | `int` | - |
| `hasCone` | `BOOL` | `bool` | - |
| `color` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
color: see SET_BLIP_COLOUR
```

---

### SET_PLAYER_ICON_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x7B21E0BB01E8224A`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PLAYER_ICON_COLOUR(int color)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `color` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_PLAYER_IS_IN_DIRECTOR_MODE

- **命名空间:** `HUD`
- **Hash:** `0x808519373FD336A3`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PLAYER_IS_IN_DIRECTOR_MODE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
If toggle is true, hides special ability bar / character name in the pause menu
If toggle is false, shows special ability bar / character name in the pause menu
```

---

### SET_PM_WARNINGSCREEN_ACTIVE

- **命名空间:** `HUD`
- **Hash:** `0x41350B4FC28E3941`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_PM_WARNINGSCREEN_ACTIVE(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_RACE_TRACK_RENDER

- **命名空间:** `HUD`
- **Hash:** `0x1EAC5F91BCBC5073`
- **JHash:** `0xFB9BABF5`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_RACE_TRACK_RENDER(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_RADAR_AS_EXTERIOR_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xE81B7D2A3DAB2D81`
- **JHash:** `0x39ABB10E`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_RADAR_AS_EXTERIOR_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### SET_RADAR_AS_INTERIOR_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x59E727A1C9D3E31A`
- **JHash:** `0x6F2626E1`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_RADAR_AS_INTERIOR_THIS_FRAME(uint interior, float x, float y, int z, int zoom)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `interior` | `Hash` | `uint` | - |
| `x` | `float` | `float` | - |
| `y` | `float` | `float` | - |
| `z` | `int` | `int` | - |
| `zoom` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
List of interior hashes: https://pastebin.com/1FUyXNqY
Not for every interior zoom > 0 available.
```

---

### SET_RADAR_ZOOM

- **命名空间:** `HUD`
- **Hash:** `0x096EF57A0C999BBA`
- **JHash:** `0x2A50D1A6`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_RADAR_ZOOM(int zoomLevel)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `zoomLevel` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
zoomLevel ranges from 0 to 1400 in R* Scripts
```

---

### SET_RADAR_ZOOM_PRECISE

- **命名空间:** `HUD`
- **Hash:** `0xBD12C5EEE184C337`
- **JHash:** `0xDCA3F423`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_RADAR_ZOOM_PRECISE(float zoom)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `zoom` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
zoom ranges from 0 to 90f in R* Scripts
```

---

### SET_RADAR_ZOOM_TO_BLIP

- **命名空间:** `HUD`
- **Hash:** `0xF98E4B3E56AFC7B1`
- **JHash:** `0x25EC28C0`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_RADAR_ZOOM_TO_BLIP(int blip, float zoom)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `zoom` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_RADAR_ZOOM_TO_DISTANCE

- **命名空间:** `HUD`
- **Hash:** `0xCB7CC0D58405AD41`
- **JHash:** `0x09CF1CE5`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_RADAR_ZOOM_TO_DISTANCE(float zoom)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `zoom` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_RADIUS_BLIP_EDGE

- **命名空间:** `HUD`
- **Hash:** `0x25615540D894B814`
- **JHash:** `0x40E25DB8`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_RADIUS_BLIP_EDGE(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Enabling this on a radius blip will make it outline only. See https://cdn.discordapp.com/attachments/553235301632573459/575132227935928330/unknown.png
```

---

### SET_SAVEGAME_LIST_UNIQUE_ID

- **命名空间:** `HUD`
- **Hash:** `0x0CF54F20DE43879C`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_SAVEGAME_LIST_UNIQUE_ID(object p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### SET_SCRIPT_VARIABLE_HUD_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0xD68A5FF8A3A89874`
- **JHash:** `0x0E41E45C`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_SCRIPT_VARIABLE_HUD_COLOUR(int r, int g, int b, int a)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `r` | `int` | `int` | - |
| `g` | `int` | `int` | - |
| `b` | `int` | `int` | - |
| `a` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets the color of HUD_COLOUR_SCRIPT_VARIABLE
```

---

### SET_SECOND_SCRIPT_VARIABLE_HUD_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0x16A304E6CB2BFAB9`
- **JHash:** `0x6BE3ACA8`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_SECOND_SCRIPT_VARIABLE_HUD_COLOUR(int r, int g, int b, int a)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `r` | `int` | `int` | - |
| `g` | `int` | `int` | - |
| `b` | `int` | `int` | - |
| `a` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Sets the color of HUD_COLOUR_SCRIPT_VARIABLE_2
```

---

### SET_SOCIAL_CLUB_TOUR

- **命名空间:** `HUD`
- **Hash:** `0x9E778248D6685FE0`
- **JHash:** `0x7AD67C95`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_SOCIAL_CLUB_TOUR(string name)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `name` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
HUD::SET_SOCIAL_CLUB_TOUR("Gallery");
HUD::SET_SOCIAL_CLUB_TOUR("Missions");
HUD::SET_SOCIAL_CLUB_TOUR("General");
HUD::SET_SOCIAL_CLUB_TOUR("Playlists");
```

---

### SET_TEXT_CENTRE

- **命名空间:** `HUD`
- **Hash:** `0xC02F4DBFB51D988B`
- **JHash:** `0xE26D39A1`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_CENTRE(bool align)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `align` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_TEXT_COLOUR

- **命名空间:** `HUD`
- **Hash:** `0xBE6B23FFA53FB442`
- **JHash:** `0xE54DD2C8`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_COLOUR(int red, int green, int blue, int alpha)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `red` | `int` | `int` | - |
| `green` | `int` | `int` | - |
| `blue` | `int` | `int` | - |
| `alpha` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
colors you input not same as you think?
A: for some reason its R B G A
```

---

### SET_TEXT_DROPSHADOW

- **命名空间:** `HUD`
- **Hash:** `0x465C84BC39F1C351`
- **JHash:** `0xE6587517`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_DROPSHADOW(int distance, int r, int g, int b, int a)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `distance` | `int` | `int` | - |
| `r` | `int` | `int` | - |
| `g` | `int` | `int` | - |
| `b` | `int` | `int` | - |
| `a` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
distance - shadow distance in pixels, both horizontal and vertical
r, g, b, a - color
```

---

### SET_TEXT_DROP_SHADOW

- **命名空间:** `HUD`
- **Hash:** `0x1CA3E9EAC9D93E5E`
- **JHash:** `0xE2A11511`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_DROP_SHADOW()
```

**返回值:** `void` (Native: `void`)

---

### SET_TEXT_EDGE

- **命名空间:** `HUD`
- **Hash:** `0x441603240D202FA6`
- **JHash:** `0x3F1A5DAB`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_EDGE(int p0, int r, int g, int b, int a)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |
| `r` | `int` | `int` | - |
| `g` | `int` | `int` | - |
| `b` | `int` | `int` | - |
| `a` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
This native does absolutely nothing, just a nullsub
```

---

### SET_TEXT_FONT

- **命名空间:** `HUD`
- **Hash:** `0x66E0276CC5F6B9DA`
- **JHash:** `0x80BC530D`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_FONT(int fontType)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `fontType` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
fonts that mess up your text where made for number values/misc stuff
```

---

### SET_TEXT_INPUT_BOX_ENABLED

- **命名空间:** `HUD`
- **Hash:** `0x1185A8087587322C`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_INPUT_BOX_ENABLED(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_TEXT_JUSTIFICATION

- **命名空间:** `HUD`
- **Hash:** `0x4E096588B13FFECA`
- **JHash:** `0x68CDFA60`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_JUSTIFICATION(int justifyType)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `justifyType` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Types -
0: Center-Justify
1: Left-Justify
2: Right-Justify

Right-Justify requires SET_TEXT_WRAP, otherwise it will draw to the far right of the screen
```

---

### SET_TEXT_LEADING

- **命名空间:** `HUD`
- **Hash:** `0xA50ABC31E3CDFAFF`
- **JHash:** `0x98CE21D4`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_LEADING(int p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_TEXT_LINE_HEIGHT_MULT

- **命名空间:** `HUD`
- **Hash:** `0x9F4624F76E6953D1`
- **Build:** `3095`

**C# 签名:**
```csharp
void SET_TEXT_LINE_HEIGHT_MULT(float lineHeightMult)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `lineHeightMult` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### SET_TEXT_OUTLINE

- **命名空间:** `HUD`
- **Hash:** `0x2513DFB0FB8400FE`
- **JHash:** `0xC753412F`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_OUTLINE()
```

**返回值:** `void` (Native: `void`)

---

### SET_TEXT_PROPORTIONAL

- **命名空间:** `HUD`
- **Hash:** `0x038C1F517D7FDCF8`
- **JHash:** `0xF49D8A08`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_PROPORTIONAL(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
This native does absolutely nothing, just a nullsub
```

---

### SET_TEXT_RENDER_ID

- **命名空间:** `HUD`
- **Hash:** `0x5F15302936E07111`
- **JHash:** `0xC5C3B7F3`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_RENDER_ID(int renderId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `renderId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_TEXT_RIGHT_JUSTIFY

- **命名空间:** `HUD`
- **Hash:** `0x6B3C4650BC8BEE47`
- **JHash:** `0x45B60520`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_RIGHT_JUSTIFY(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_TEXT_SCALE

- **命名空间:** `HUD`
- **Hash:** `0x07C837F9A01C34C9`
- **JHash:** `0xB6E15B23`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_SCALE(float scale, float size)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `scale` | `float` | `float` | - |
| `size` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Size range : 0F to 1.0F
p0 is unknown and doesn't seem to have an effect, yet in the game scripts it changes to 1.0F sometimes.
```

---

### SET_TEXT_WRAP

- **命名空间:** `HUD`
- **Hash:** `0x63145D9C883A1A70`
- **JHash:** `0x6F60AB54`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_TEXT_WRAP(float start, float end)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `start` | `float` | `float` | - |
| `end` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
It sets the text in a specified box and wraps the text if it exceeds the boundries. Both values are for X axis. Useful when positioning text set to center or aligned to the right.

start - left boundry on screen position (0.0 - 1.0)
end - right boundry on screen position (0.0 - 1.0)
```

---

### SET_USE_ISLAND_MAP

- **命名空间:** `HUD`
- **Hash:** `0x5E1460624D194A38`
- **Build:** `2189`

**C# 签名:**
```csharp
void SET_USE_ISLAND_MAP(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Toggles the Cayo Perico map.
```

---

### SET_USE_SET_DESTINATION_IN_PAUSE_MAP

- **命名空间:** `HUD`
- **Hash:** `0x6CDD58146A436083`
- **Build:** `573`

**C# 签名:**
```csharp
void SET_USE_SET_DESTINATION_IN_PAUSE_MAP(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SET_WARNING_MESSAGE

- **命名空间:** `HUD`
- **Hash:** `0x7B1776B3B53F8D74`
- **JHash:** `0xBE699BDE`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_WARNING_MESSAGE(string titleMsg, int flags, string promptMsg, bool p3, int p4, string p5, string p6, bool showBackground, int errorCode)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `titleMsg` | `const char*` | `string` | - |
| `flags` | `int` | `int` | - |
| `promptMsg` | `const char*` | `string` | - |
| `p3` | `BOOL` | `bool` | - |
| `p4` | `int` | `int` | - |
| `p5` | `const char*` | `string` | - |
| `p6` | `const char*` | `string` | - |
| `showBackground` | `BOOL` | `bool` | - |
| `errorCode` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
You can only use text entries. No custom text.

Example: SET_WARNING_MESSAGE("t20", 3, "adder", false, -1, 0, 0, true);
errorCode: shows an error code at the bottom left if nonzero
```

---

### SET_WARNING_MESSAGE_OPTION_HIGHLIGHT

- **命名空间:** `HUD`
- **Hash:** `0xDAF87174BE7454FF`
- **Build:** `323`

**C# 签名:**
```csharp
bool SET_WARNING_MESSAGE_OPTION_HIGHLIGHT(object p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |

**返回值:** `bool` (Native: `BOOL`)

---

### SET_WARNING_MESSAGE_OPTION_ITEMS

- **命名空间:** `HUD`
- **Hash:** `0x0C5A80A9E096D529`
- **Build:** `323`

**C# 签名:**
```csharp
bool SET_WARNING_MESSAGE_OPTION_ITEMS(int index, string name, int cash, int rp, int lvl, int colour)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `index` | `int` | `int` | - |
| `name` | `const char*` | `string` | - |
| `cash` | `int` | `int` | - |
| `rp` | `int` | `int` | - |
| `lvl` | `int` | `int` | - |
| `colour` | `int` | `int` | - |

**返回值:** `bool` (Native: `BOOL`)

**说明:**
```
Some sort of list displayed in a warning message. Yet unknown how to prevent repeating.
Param names copied from the corresponding scaleform function "SET_LIST_ROW".
Example: https://i.imgur.com/arKvOYx.png
```

---

### SET_WARNING_MESSAGE_WITH_HEADER

- **命名空间:** `HUD`
- **Hash:** `0xDC38CC1E35B6A5D7`
- **JHash:** `0x2DB9EAB5`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_WARNING_MESSAGE_WITH_HEADER(string entryHeader, string entryLine1, int instructionalKey, string entryLine2, bool p4, object p5, ref object showBackground, ref object p7, bool p8, object p9)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `entryHeader` | `const char*` | `string` | - |
| `entryLine1` | `const char*` | `string` | - |
| `instructionalKey` | `int` | `int` | - |
| `entryLine2` | `const char*` | `string` | - |
| `p4` | `BOOL` | `bool` | - |
| `p5` | `Any` | `object` | - |
| `showBackground` | `Any*` | `ref object` | - |
| `p7` | `Any*` | `ref object` | - |
| `p8` | `BOOL` | `bool` | - |
| `p9` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Shows a warning message on screen with a header.
Note: You can only use text entries. No custom text. You can recreate this easily with scaleforms.
Example: https://i.imgur.com/ITJt8bJ.png
```

---

### SET_WARNING_MESSAGE_WITH_HEADER_AND_SUBSTRING_FLAGS

- **命名空间:** `HUD`
- **Hash:** `0x701919482C74B5AB`
- **JHash:** `0x749929D3`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_WARNING_MESSAGE_WITH_HEADER_AND_SUBSTRING_FLAGS(string entryHeader, string entryLine1, int instructionalKey, string entryLine2, bool p4, object p5, object additionalIntInfo, string additionalTextInfoLine1, string additionalTextInfoLine2, bool showBackground, int errorCode)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `entryHeader` | `const char*` | `string` | - |
| `entryLine1` | `const char*` | `string` | - |
| `instructionalKey` | `int` | `int` | - |
| `entryLine2` | `const char*` | `string` | - |
| `p4` | `BOOL` | `bool` | - |
| `p5` | `Any` | `object` | - |
| `additionalIntInfo` | `Any` | `object` | - |
| `additionalTextInfoLine1` | `const char*` | `string` | - |
| `additionalTextInfoLine2` | `const char*` | `string` | - |
| `showBackground` | `BOOL` | `bool` | - |
| `errorCode` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
You can use this native for custom input, without having to use any scaleform-related natives.
The native must be called on tick.
The entryHeader must be a valid label.
For Single lines use JL_INVITE_N as entryLine1, JL_INVITE_ND for multiple.
Notes:
- additionalIntInfo: replaces first occurrence of ~1~ in provided label with an integer
- additionalTextInfoLine1: replaces first occurrence of ~a~ in provided label, with your custom text
- additionalTextInfoLine2: replaces second occurrence of ~a~ in provided label, with your custom text
- showBackground: shows black background of the warning screen
- errorCode: shows an error code at the bottom left if nonzero
Example of usage:
SET_WARNING_MESSAGE_WITH_HEADER_AND_SUBSTRING_FLAGS("ALERT", "JL_INVITE_ND", 66, "", true, -1, -1, "Testing line 1", "Testing line 2", true, 0);
Screenshot:
https://imgur.com/a/IYA7vJ8
```

---

### SET_WARNING_MESSAGE_WITH_HEADER_AND_SUBSTRING_FLAGS_EXTENDED

- **命名空间:** `HUD`
- **Hash:** `0x15803FEC3B9A872B`
- **Build:** `573`

**C# 签名:**
```csharp
void SET_WARNING_MESSAGE_WITH_HEADER_AND_SUBSTRING_FLAGS_EXTENDED(string labelTitle, string labelMessage, int p2, int p3, string labelMessage2, bool p5, int p6, int p7, string p8, string p9, bool background, int errorCode)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `labelTitle` | `const char*` | `string` | - |
| `labelMessage` | `const char*` | `string` | - |
| `p2` | `int` | `int` | - |
| `p3` | `int` | `int` | - |
| `labelMessage2` | `const char*` | `string` | - |
| `p5` | `BOOL` | `bool` | - |
| `p6` | `int` | `int` | - |
| `p7` | `int` | `int` | - |
| `p8` | `const char*` | `string` | - |
| `p9` | `const char*` | `string` | - |
| `background` | `BOOL` | `bool` | - |
| `errorCode` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
labelTitle: Label of the alert's title.
labelMsg: Label of the alert's message.
p2: This is an enum, check the description for a list.
p3: This is an enum, check the description for a list.
labelMsg2: Label of another message line
p5: usually 0
p6: usually -1
p7: usually 0
p8: unknown label
p9: unknown label
background: Set to anything other than 0 or false (even any string) and it will draw a background. Setting it to 0 or false will draw no background.
errorCode: Error code, shown at the bottom left if set to value other than 0.

instructionalKey enum list:
Buttons = {
      Empty = 0,
      Select = 1, -- (RETURN)
      Ok = 2, -- (RETURN)
      Yes = 4, -- (RETURN)
      Back = 8, -- (ESC)
      Cancel = 16, -- (ESC)
      No = 32, -- (ESC)
      RetrySpace = 64, -- (SPACE)
      Restart = 128, -- (SPACE)
      Skip = 256, -- (SPACE)
      Quit = 512, -- (ESC)
      Adjust = 1024, -- (ARROWS)
      SpaceKey = 2048, -- (SPACE)
      Share = 4096, -- (SPACE)
      SignIn = 8192, -- (SPACE)
      Continue = 16384, -- (RETURN)
      AdjustLeftRight = 32768, -- (SCROLL L/R)
      AdjustUpDown = 65536, -- (SCROLL U/D)
      Overwrite = 131072, -- (SPACE)
      SocialClubSignup = 262144, -- (RETURN)
      Confirm = 524288, -- (RETURN)
      Queue = 1048576, -- (RETURN)
      RetryReturn = 2097152, -- (RETURN)
      BackEsc = 4194304, -- (ESC)
      SocialClub = 8388608, -- (RETURN)
      Spectate = 16777216, -- (SPACE)
      OkEsc = 33554432, -- (ESC)
      CancelTransfer = 67108864, -- (ESC)
      LoadingSpinner = 134217728,
      NoReturnToGTA = 268435456, -- (ESC)
      CancelEsc = 536870912, -- (ESC)
}

Alt = {
      Empty = 0,
      No = 1, -- (SPACE)
      Host = 2, -- (ESC)
      SearchForJob = 4, -- (RETURN)
      ReturnKey = 8, -- (TURN)
      Freemode = 16, -- (ESC)
}

Example: https://i.imgur.com/TvmNF4k.png
```

---

### SET_WARNING_MESSAGE_WITH_HEADER_EXTENDED

- **命名空间:** `HUD`
- **Hash:** `0x38B55259C2E078ED`
- **Build:** `1493`

**C# 签名:**
```csharp
void SET_WARNING_MESSAGE_WITH_HEADER_EXTENDED(string entryHeader, string entryLine1, int flags, string entryLine2, bool p4, object p5, ref object p6, ref object p7, bool showBg, object p9, object p10)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `entryHeader` | `const char*` | `string` | - |
| `entryLine1` | `const char*` | `string` | - |
| `flags` | `int` | `int` | - |
| `entryLine2` | `const char*` | `string` | - |
| `p4` | `BOOL` | `bool` | - |
| `p5` | `Any` | `object` | - |
| `p6` | `Any*` | `ref object` | - |
| `p7` | `Any*` | `ref object` | - |
| `showBg` | `BOOL` | `bool` | - |
| `p9` | `Any` | `object` | - |
| `p10` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### SET_WAYPOINT_CLEAR_ON_ARRIVAL_MODE

- **命名空间:** `HUD`
- **Hash:** `0x3FFC556B62146F75`
- **Build:** `3717`

**C# 签名:**
```csharp
void SET_WAYPOINT_CLEAR_ON_ARRIVAL_MODE(int mode)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `mode` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SET_WAYPOINT_OFF

- **命名空间:** `HUD`
- **Hash:** `0xA7E4E2D361C2627F`
- **JHash:** `0xB3496E1B`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_WAYPOINT_OFF()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
This native removes the current waypoint from the map.

Example:
C#:
Function.Call(Hash.SET_WAYPOINT_OFF);

C++:
HUD::SET_WAYPOINT_OFF();
```

---

### SET_WIDESCREEN_FORMAT

- **命名空间:** `HUD`
- **Hash:** `0xC3B07BA00A83B0F1`
- **JHash:** `0xF016E08F`
- **Build:** `323`

**C# 签名:**
```csharp
void SET_WIDESCREEN_FORMAT(object p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### SHOW_ACCOUNT_PICKER

- **命名空间:** `HUD`
- **Hash:** `0x60E892BA4F5BDCA4`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_ACCOUNT_PICKER()
```

**返回值:** `void` (Native: `void`)

---

### SHOW_CONTACT_INSTRUCTIONAL_BUTTON

- **命名空间:** `HUD`
- **Hash:** `0xC772A904CDE1186F`
- **Build:** `2545`

**C# 签名:**
```csharp
void SHOW_CONTACT_INSTRUCTIONAL_BUTTON(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SHOW_CREW_INDICATOR_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0xDCFB5D4DB8BF367E`
- **JHash:** `0xABBE1E45`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_CREW_INDICATOR_ON_BLIP(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Enables or disables the blue half circle https://i.imgur.com/iZes9Ec.png around the specified blip on the left side of the blip. This is used to indicate that the player is in your crew in GTA:O. Color is changeable by using `SET_BLIP_SECONDARY_COLOUR`.
```

---

### SHOW_FOR_SALE_ICON_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x19BD6E3C0E16A8FA`
- **Build:** `2802`

**C# 签名:**
```csharp
void SHOW_FOR_SALE_ICON_ON_BLIP(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SHOW_FRIEND_INDICATOR_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x23C3EB807312F01A`
- **JHash:** `0x4C8F02B4`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_FRIEND_INDICATOR_ON_BLIP(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Highlights a blip by a half cyan circle on the right side of the blip. https://i.imgur.com/FrV9M4e.png
.Indicating that that player is a friend (in GTA:O). This color can not be changed.
To toggle the left side (crew member indicator) of the half circle around the blip, use: `SHOW_CREW_INDICATOR_ON_BLIP`
```

---

### SHOW_GOLD_TICK_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0xCAC2031EBF79B1A8`
- **Build:** `2699`

**C# 签名:**
```csharp
void SHOW_GOLD_TICK_ON_BLIP(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Adds a orange checkmark on top of a given blip handle: https://imgur.com/a/aw5OTMF
_SHOW_FRIEND_INDICATOR_ON_BLIP* - _SHOW_HEADING_INDICATOR_ON_BLIP*
```

---

### SHOW_HEADING_INDICATOR_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x5FBCA48327B914DF`
- **JHash:** `0xD1C3D71B`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_HEADING_INDICATOR_ON_BLIP(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Adds the GTA: Online player heading indicator to a blip.
```

---

### SHOW_HEIGHT_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x75A16C3DA34F1245`
- **JHash:** `0x1D99F676`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_HEIGHT_ON_BLIP(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SHOW_HUD_COMPONENT_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x0B4DF1FA60C0E664`
- **JHash:** `0x95E1546E`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_HUD_COMPONENT_THIS_FRAME(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
This function hides various HUD (Heads-up Display) components.
Listed below are the integers and the corresponding HUD component.
- 1 : WANTED_STARS
- 2 : WEAPON_ICON
- 3 : CASH
- 4 : MP_CASH
- 5 : MP_MESSAGE
- 6 : VEHICLE_NAME
- 7 : AREA_NAME
- 8 : VEHICLE_CLASS
- 9 : STREET_NAME
- 10 : HELP_TEXT
- 11 : FLOATING_HELP_TEXT_1
- 12 : FLOATING_HELP_TEXT_2
- 13 : CASH_CHANGE
- 14 : RETICLE
- 15 : SUBTITLE_TEXT
- 16 : RADIO_STATIONS
- 17 : SAVING_GAME
- 18 : GAME_STREAM
- 19 : WEAPON_WHEEL
- 20 : WEAPON_WHEEL_STATS
- 21 : HUD_COMPONENTS
- 22 : HUD_WEAPONS

These integers also work for the `HIDE_HUD_COMPONENT_THIS_FRAME` native, but instead hides the HUD Component.
```

---

### SHOW_NUMBER_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0xA3C0B359DCB848B6`
- **JHash:** `0x7BFC66C6`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_NUMBER_ON_BLIP(int blip, int number)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `number` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SHOW_OUTLINE_INDICATOR_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0xB81656BC81FE24D1`
- **JHash:** `0x8DE82C15`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_OUTLINE_INDICATOR_ON_BLIP(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Highlights a blip by a cyan color circle.

Color can be changed with SET_BLIP_SECONDARY_COLOUR
```

---

### SHOW_SCRIPTED_HUD_COMPONENT_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x4F38DCA127DAAEA2`
- **Build:** `1734`

**C# 签名:**
```csharp
void SHOW_SCRIPTED_HUD_COMPONENT_THIS_FRAME(int id)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `id` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### SHOW_START_MISSION_INSTRUCTIONAL_BUTTON

- **命名空间:** `HUD`
- **Hash:** `0xF1A6C18B35BCADE6`
- **JHash:** `0xA2CAAB4F`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_START_MISSION_INSTRUCTIONAL_BUTTON(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### SHOW_TICK_ON_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x74513EA3E505181E`
- **JHash:** `0x3DCF0092`
- **Build:** `323`

**C# 签名:**
```csharp
void SHOW_TICK_ON_BLIP(int blip, bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Adds a green checkmark on top of a blip.
```

---

### START_GPS_CUSTOM_ROUTE

- **命名空间:** `HUD`
- **Hash:** `0xDB34E8D56FC13B08`
- **JHash:** `0x7F93799B`
- **Build:** `323`

**C# 签名:**
```csharp
void START_GPS_CUSTOM_ROUTE(int hudColor, bool displayOnFoot, bool followPlayer)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudColor` | `int` | `int` | - |
| `displayOnFoot` | `BOOL` | `bool` | - |
| `followPlayer` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Starts a new GPS custom-route, allowing you to plot lines on the map.
Lines are drawn directly between points.
The GPS custom route works like the GPS multi route, except it does not follow roads.
Example result: https://i.imgur.com/BDm5pzt.png
hudColor: The HUD color of the GPS path.
displayOnFoot: Draws the path regardless if the player is in a vehicle or not.
followPlayer: Draw the path partially between the previous and next point based on the players position between them. When false, the GPS appears to not disappear after the last leg is completed.
```

---

### START_GPS_MULTI_ROUTE

- **命名空间:** `HUD`
- **Hash:** `0x3D3D15AF7BCAAF83`
- **JHash:** `0xC3DCBEDB`
- **Build:** `323`

**C# 签名:**
```csharp
void START_GPS_MULTI_ROUTE(int hudColor, bool routeFromPlayer, bool displayOnFoot)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudColor` | `int` | `int` | - |
| `routeFromPlayer` | `BOOL` | `bool` | - |
| `displayOnFoot` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Starts a new GPS multi-route, allowing you to create custom GPS paths.
GPS functions like the waypoint, except it can contain multiple points it's forced to go through.
Once the player has passed a point, the GPS will no longer force its path through it.

Works independently from the player-placed waypoint and blip routes.
Example result: https://i.imgur.com/ZZHQatX.png
hudColor: The HUD color of the GPS path.
routeFromPlayer: Makes the GPS draw a path from the player to the next point, rather than the original path from the previous point.
displayOnFoot: Draws the GPS path regardless if the player is in a vehicle or not.
```

---

### SUPPRESS_FRONTEND_RENDERING_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0xBA751764F0821256`
- **JHash:** `0x7F349900`
- **Build:** `323`

**C# 签名:**
```csharp
void SUPPRESS_FRONTEND_RENDERING_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

---

### TAKE_CONTROL_OF_FRONTEND

- **命名空间:** `HUD`
- **Hash:** `0xEC9264727EEC0F28`
- **JHash:** `0xC06B763D`
- **Build:** `323`

**C# 签名:**
```csharp
void TAKE_CONTROL_OF_FRONTEND()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Disables frontend (works in custom frontends, not sure about regular pause menu) navigation keys on keyboard. Not sure about controller. Does not disable mouse controls. No need to call this every tick.

To enable the keys again, use `0x14621BB1DF14E2B2`.
```

---

### THEFEED_AUTO_POST_GAMETIPS_OFF

- **命名空间:** `HUD`
- **Hash:** `0xADED7F5748ACAFE6`
- **JHash:** `0x1D6859CA`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_AUTO_POST_GAMETIPS_OFF()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Displays "normal" notifications again after calling `THEFEED_AUTO_POST_GAMETIPS_ON` (those that were drawn before calling this native too), though those will have a weird offset and stay on screen forever (tested with notifications created from same script).
```

---

### THEFEED_AUTO_POST_GAMETIPS_ON

- **命名空间:** `HUD`
- **Hash:** `0x56C8B608CFD49854`
- **JHash:** `0xF881AB87`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_AUTO_POST_GAMETIPS_ON()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Enables loading screen tips to be be shown (`THEFEED_SHOW`), blocks other kinds of notifications from being displayed (at least from current script). Call `THEFEED_AUTO_POST_GAMETIPS_OFF` to display those again.
```

---

### THEFEED_CLEAR_FROZEN_POST

- **命名空间:** `HUD`
- **Hash:** `0x80FE4F3AB4E1B62A`
- **JHash:** `0x3CD4307C`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_CLEAR_FROZEN_POST()
```

**返回值:** `void` (Native: `void`)

---

### THEFEED_FLUSH_QUEUE

- **命名空间:** `HUD`
- **Hash:** `0xA8FDB297A8D25FBA`
- **JHash:** `0x5205C6F5`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_FLUSH_QUEUE()
```

**返回值:** `void` (Native: `void`)

---

### THEFEED_FORCE_RENDER_OFF

- **命名空间:** `HUD`
- **Hash:** `0x583049884A2EEE3C`
- **JHash:** `0xC8BAB2F2`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_FORCE_RENDER_OFF()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Enables loading screen tips to be be shown (`THEFEED_SHOW`), blocks other kinds of notifications from being displayed (at least from current script). Call `0xADED7F5748ACAFE6` to display those again.
```

---

### THEFEED_FORCE_RENDER_ON

- **命名空间:** `HUD`
- **Hash:** `0xA13C11E1B5C06BFC`
- **JHash:** `0x520FCB6D`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_FORCE_RENDER_ON()
```

**返回值:** `void` (Native: `void`)

---

### THEFEED_FREEZE_NEXT_POST

- **命名空间:** `HUD`
- **Hash:** `0xFDEC055AB549E328`
- **JHash:** `0xAFA1148B`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_FREEZE_NEXT_POST()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Requires manual management of game stream handles (i.e., 0xBE4390CB40B3E627).
```

---

### THEFEED_GET_LAST_SHOWN_PHONE_ACTIVATABLE_FEED_ID

- **命名空间:** `HUD`
- **Hash:** `0x82352748437638CA`
- **JHash:** `0x294405D4`
- **Build:** `323`

**C# 签名:**
```csharp
int THEFEED_GET_LAST_SHOWN_PHONE_ACTIVATABLE_FEED_ID()
```

**返回值:** `int` (Native: `int`)

**说明:**
```
Returns the handle for the notification currently displayed on the screen. Name may be a hash collision, but describes the function accurately.
```

---

### THEFEED_HIDE

- **命名空间:** `HUD`
- **Hash:** `0x32888337579A5970`
- **Build:** `463`

**C# 签名:**
```csharp
void THEFEED_HIDE()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Stops loading screen tips shown by invoking `THEFEED_SHOW`
```

---

### THEFEED_HIDE_THIS_FRAME

- **命名空间:** `HUD`
- **Hash:** `0x25F87B30C382FCA7`
- **JHash:** `0x1E63088A`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_HIDE_THIS_FRAME()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Once called each frame hides all above radar notifications.
```

---

### THEFEED_IS_PAUSED

- **命名空间:** `HUD`
- **Hash:** `0xA9CBFD40B3FA3010`
- **JHash:** `0xC5223796`
- **Build:** `323`

**C# 签名:**
```csharp
bool THEFEED_IS_PAUSED()
```

**返回值:** `bool` (Native: `BOOL`)

---

### THEFEED_ONLY_SHOW_TOOLTIPS

- **命名空间:** `HUD`
- **Hash:** `0x6F1554B0CC2089FA`
- **JHash:** `0xA7C8594B`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_ONLY_SHOW_TOOLTIPS(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### THEFEED_PAUSE

- **命名空间:** `HUD`
- **Hash:** `0xFDB423997FA30340`
- **JHash:** `0x4D0449C6`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_PAUSE()
```

**返回值:** `void` (Native: `void`)

---

### THEFEED_REMOVE_ITEM

- **命名空间:** `HUD`
- **Hash:** `0xBE4390CB40B3E627`
- **JHash:** `0xECA8ACB9`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_REMOVE_ITEM(int notificationId)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `notificationId` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Removes a notification instantly instead of waiting for it to disappear
```

---

### THEFEED_REPORT_LOGO_OFF

- **命名空间:** `HUD`
- **Hash:** `0xB695E2CD0A2DA9EE`
- **JHash:** `0x4A4A40A4`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_REPORT_LOGO_OFF()
```

**返回值:** `void` (Native: `void`)

---

### THEFEED_REPORT_LOGO_ON

- **命名空间:** `HUD`
- **Hash:** `0xD4438C0564490E63`
- **JHash:** `0x709B4BCB`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_REPORT_LOGO_ON()
```

**返回值:** `void` (Native: `void`)

---

### THEFEED_RESET_ALL_PARAMETERS

- **命名空间:** `HUD`
- **Hash:** `0xFDD85225B2DEA55E`
- **JHash:** `0xA4524B23`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_RESET_ALL_PARAMETERS()
```

**返回值:** `void` (Native: `void`)

---

### THEFEED_RESUME

- **命名空间:** `HUD`
- **Hash:** `0xE1CD1E48E025E661`
- **JHash:** `0xD3F40140`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_RESUME()
```

**返回值:** `void` (Native: `void`)

---

### THEFEED_SET_BACKGROUND_COLOR_FOR_NEXT_POST

- **命名空间:** `HUD`
- **Hash:** `0x92F0DA1E27DB96DC`
- **JHash:** `0x07CE2EA4`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_SET_BACKGROUND_COLOR_FOR_NEXT_POST(int hudColorIndex)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `hudColorIndex` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
From the decompiled scripts:
HUD::THEFEED_SET_BACKGROUND_COLOR_FOR_NEXT_POST(6);
HUD::THEFEED_SET_BACKGROUND_COLOR_FOR_NEXT_POST(184);
HUD::THEFEED_SET_BACKGROUND_COLOR_FOR_NEXT_POST(190);

sets background color for the next notification
6 = red
184 = green
190 = yellow

Here is a list of some colors that can be used: https://gyazo.com/68bd384455fceb0a85a8729e48216e15
```

---

### THEFEED_SET_FLASH_DURATION_PARAMETER_FOR_NEXT_MESSAGE

- **命名空间:** `HUD`
- **Hash:** `0x17AD8C9706BDD88A`
- **JHash:** `0x24A97AF8`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_SET_FLASH_DURATION_PARAMETER_FOR_NEXT_MESSAGE(int count)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `count` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Related to notification color flashing, setting count to 0 invalidates a `THEFEED_SET_RGBA_PARAMETER_FOR_NEXT_MESSAGE` call for the target notification.
```

---

### THEFEED_SET_RGBA_PARAMETER_FOR_NEXT_MESSAGE

- **命名空间:** `HUD`
- **Hash:** `0x17430B918701C342`
- **JHash:** `0xCF14D7F2`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_SET_RGBA_PARAMETER_FOR_NEXT_MESSAGE(int red, int green, int blue, int alpha)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `red` | `int` | `int` | - |
| `green` | `int` | `int` | - |
| `blue` | `int` | `int` | - |
| `alpha` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### THEFEED_SET_SCRIPTED_MENU_HEIGHT

- **命名空间:** `HUD`
- **Hash:** `0x55598D21339CB998`
- **JHash:** `0x1DA7E41A`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_SET_SCRIPTED_MENU_HEIGHT(float pos)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `pos` | `float` | `float` | - |

**返回值:** `void` (Native: `void`)

---

### THEFEED_SET_SNAP_FEED_ITEM_POSITIONS

- **命名空间:** `HUD`
- **Hash:** `0xBAE4F9B97CD43B30`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_SET_SNAP_FEED_ITEM_POSITIONS(bool p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### THEFEED_SET_VIBRATE_PARAMETER_FOR_NEXT_MESSAGE

- **命名空间:** `HUD`
- **Hash:** `0x4A0C7C9BB10ABB36`
- **JHash:** `0x44018EDB`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_SET_VIBRATE_PARAMETER_FOR_NEXT_MESSAGE(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### THEFEED_SHOW

- **命名空间:** `HUD`
- **Hash:** `0x15CFA549788D35EF`
- **Build:** `463`

**C# 签名:**
```csharp
void THEFEED_SHOW()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Displays loading screen tips, requires `THEFEED_AUTO_POST_GAMETIPS_ON` to be called beforehand.
```

---

### THEFEED_UPDATE_ITEM_TEXTURE

- **命名空间:** `HUD`
- **Hash:** `0x317EBA71D7543F52`
- **JHash:** `0x6A3F747D`
- **Build:** `323`

**C# 签名:**
```csharp
void THEFEED_UPDATE_ITEM_TEXTURE(string txdString1, string txnString1, string txdString2, string txnString2)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `txdString1` | `const char*` | `string` | - |
| `txnString1` | `const char*` | `string` | - |
| `txdString2` | `const char*` | `string` | - |
| `txnString2` | `const char*` | `string` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Used in the native scripts to reference "GET_PEDHEADSHOT_TXD_STRING" and "CHAR_DEFAULT".
```

---

### TOGGLE_STEALTH_RADAR

- **命名空间:** `HUD`
- **Hash:** `0x6AFDFB93754950C7`
- **JHash:** `0xC68D47C4`
- **Build:** `323`

**C# 签名:**
```csharp
void TOGGLE_STEALTH_RADAR(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### TRIGGER_SONAR_BLIP

- **命名空间:** `HUD`
- **Hash:** `0x72DD432F3CDFC0EE`
- **JHash:** `0xBF25E7B2`
- **Build:** `323`

**C# 签名:**
```csharp
void TRIGGER_SONAR_BLIP(float posX, float posY, float posZ, float radius, int p4)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `posX` | `float` | `float` | - |
| `posY` | `float` | `float` | - |
| `posZ` | `float` | `float` | - |
| `radius` | `float` | `float` | - |
| `p4` | `int` | `int` | - |

**返回值:** `void` (Native: `void`)

---

### UNLOCK_MINIMAP_ANGLE

- **命名空间:** `HUD`
- **Hash:** `0x8183455E16C42E3A`
- **JHash:** `0x742043F9`
- **Build:** `323`

**C# 签名:**
```csharp
void UNLOCK_MINIMAP_ANGLE()
```

**返回值:** `void` (Native: `void`)

---

### UNLOCK_MINIMAP_POSITION

- **命名空间:** `HUD`
- **Hash:** `0x3E93E06DB8EF1F30`
- **JHash:** `0x5E8E6F54`
- **Build:** `323`

**C# 签名:**
```csharp
void UNLOCK_MINIMAP_POSITION()
```

**返回值:** `void` (Native: `void`)

---

### UPDATE_RADAR_ZOOM_TO_BLIP

- **命名空间:** `HUD`
- **Hash:** `0xD2049635DEB9C375`
- **JHash:** `0xE8D3A910`
- **Build:** `323`

**C# 签名:**
```csharp
void UPDATE_RADAR_ZOOM_TO_BLIP()
```

**返回值:** `void` (Native: `void`)

**说明:**
```
Does nothing (it's a nullsub).
```

---

### USE_FAKE_MP_CASH

- **命名空间:** `HUD`
- **Hash:** `0x170F541E1CADD1DE`
- **JHash:** `0x6253B9D7`
- **Build:** `323`

**C# 签名:**
```csharp
void USE_FAKE_MP_CASH(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Related to displaying cash on the HUD
Always called before HUD::CHANGE_FAKE_MP_CASH in decompiled scripts
```

---

### USE_VEHICLE_TARGETING_RETICULE

- **命名空间:** `HUD`
- **Hash:** `0x0C698D8F099174C7`
- **Build:** `1180`

**C# 签名:**
```csharp
void USE_VEHICLE_TARGETING_RETICULE(object p0)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `p0` | `Any` | `object` | - |

**返回值:** `void` (Native: `void`)

---

### _SET_BLIP_GPS_ROUTE_DISPLAY_DISTANCE

- **命名空间:** `HUD`
- **Hash:** `0x25D984CFB64ED6DE`
- **Build:** `3095`

**C# 签名:**
```csharp
void _SET_BLIP_GPS_ROUTE_DISPLAY_DISTANCE(int blip, int blipChangeParam46, bool blipChangeParam47)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `blip` | `Blip` | `int` | - |
| `blipChangeParam46` | `int` | `int` | - |
| `blipChangeParam47` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

**说明:**
```
Applies to new eBlipParams _BLIP_CHANGE_46* and _BLIP_CHANGE_47*
```

---

### _SET_PAUSE_EXTERIOR_RENDERING_WHILE_IN_INTERIOR

- **命名空间:** `HUD`
- **Hash:** `0x35CCE12EAECB4A51`
- **Build:** `2944`

**C# 签名:**
```csharp
void _SET_PAUSE_EXTERIOR_RENDERING_WHILE_IN_INTERIOR()
```

**返回值:** `void` (Native: `void`)

---

### _SHOW_PURCHASE_INSTRUCTIONAL_BUTTON

- **命名空间:** `HUD`
- **Hash:** `0xF6865E26067B708C`
- **Build:** `3407`

**C# 签名:**
```csharp
void _SHOW_PURCHASE_INSTRUCTIONAL_BUTTON(bool toggle)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `toggle` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---

### _USE_VEHICLE_TARGETING_RETICULE_ON_VEHICLES

- **命名空间:** `HUD`
- **Hash:** `0x1BC0EA2912708625`
- **Build:** `3095`

**C# 签名:**
```csharp
void _USE_VEHICLE_TARGETING_RETICULE_ON_VEHICLES(bool enable)
```

**参数列表:**

| 参数名 | Native 类型 | C# 类型 | 说明 |
|--------|------------|---------|------|
| `enable` | `BOOL` | `bool` | - |

**返回值:** `void` (Native: `void`)

---
