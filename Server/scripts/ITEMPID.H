#ifndef _ITEMPID_H_DEFINED
#define _ITEMPID_H_DEFINED

/******************************************************************
***************       Armor                         ***************
******************************************************************/

//Light
#define PID_LEATHER_JACKET                  (74)
#define PID_LEATHER_ARMOR                   (1)
#define PID_LEATHER_ARMOR_MK_II             (379)
#define PID_CURED_LEATHER_ARMOR             (265)

//Medium
#define PID_METAL_ARMOR                     (2)
#define PID_METAL_ARMOR_MK_II               (380)
#define PID_TESLA_ARMOR                     (240)
#define PID_COMBAT_ARMOR                    (17)
#define PID_COMBAT_ARMOR_MK_II              (381)
#define PID_BROTHERHOOD_COMBAT_ARMOR        (239)
#define PID_NCR_ARMOR                       (9654)
#define PID_ENVIROMENTAL_ARMOR              (751) // Test
#define PID_ENCLAVE_COMBAT_ARMOR            (752)
#define PID_VAULT_13_SUIT                   (753)
#define PID_VAULT_SUIT                      (754)
#define PID_WORN_ROBE                       (755)
#define PID_BLUE_ROBE                       (756)
#define PID_RED_ROBE                        (757)
#define PID_GRAY_ROBE                       (758)
#define PID_MOBSTER_BLUE                    (759)
#define PID_MOBSTER_GRAY                    (760)
#define PID_CLOTHES_POLICE                  (761)
#define PID_CLOTHES_GENERIC1                (762)
#define PID_CLOTHES_GENERIC2                (763)
#define PID_CLOTHES_SCIENTIST1              (764)
#define PID_CLOTHES_SCIENTIST2              (765)
#define PID_CLOTHES_ELEGANT_SUIT            (766)
#define PID_CLOTHES_RAGS                    (767)
#define PID_CLOTHES_DWARF                   (768)
#define PID_CLOTHES_RED_SHIRT               (769)
#define PID_CLOTHES_DOCTOR                  (770)
#define PID_CLOTHES_CHILD                   (771)
#define PID_CLOTHES_GRAY_SHIRT              (772)
#define PID_CLOTHES_FAT1                    (773)
#define PID_CLOTHES_BOXER                   (774)
#define PID_SF_COMBAT_JACKET                (775)
#define PID_MORDINO_METAL_JACKET            (776)
#define PID_CLOTHES_GENERIC3                (777)
#define PID_NPC_SKIN                        (799)

//Heavy
#define PID_POWERED_ARMOR                   (3)
#define PID_HARDENED_POWER_ARMOR            (232)
#define PID_ADVANCED_POWER_ARMOR            (348)
#define PID_ADVANCED_POWER_ARMOR_MK2        (349)

//Robes
#define PID_PURPLE_ROBE                     (113)
#define PID_KEEPBRIGE_ROBE                  (524) // PID_BRIDGEKEEPERS_ROBE
#define PID_SAND_ROBE                       (750) // Has same stats as PID_PURPLE_ROBE

//Helmets
#define PID_STRAWHAT                        (719)
#define PID_ARMIN_CAP                       (720)
#define PID_POWER_HELMET                    (721)
#define PID_MOTORCYCLE_HELMET               (722)

//New armors
#define PID_TIBBETS_PRISON                  (723)
#define PID_VAULT_SUIT2                     (724)

//More helmets
#define PID_COMBAT_HELMET                  (725)
#define PID_COMBAT_HELMET_MK_II            (726)
#define PID_BROTHERHOOD_HELMET             (727)
#define PID_LEATHER_JACKET_HELMET          (728)
#define PID_COMBAT_LEATHER_JACKET_HELMET   (729)
#define PID_LEATHER_ARMOR_HELMET           (730)
#define PID_LEATHER_ARMOR_HELMET_MK2       (731)
#define PID_METAL_HELMET                   (732)
#define PID_METAL_HELMET_MK2               (733)
#define PID_TESLA_HELMET                   (734)
#define PID_APA_HELMET                     (735)
#define PID_APA_HELMET_MK2                 (736)
#define PID_HARDENED_POWER_HELMET          (737)
#define PID_ENCLAVE_COMBAT_HELMET          (738)
#define PID_ENCLAVE_COMBAT_HELMET_MK2      (739)

//TODO:
//#define PID_BLACK_ROBE                      (xxx)
//#define PID_BLUE_трико                      (xxx)
//#define PID_RED_трико                       (xxx)
//#define PID_GREEN_трико                     (xxx)

// PID 719 to 749 are free for helmet objects!
// Use PID 750 to 799 for new armor objects!
// Check FOOBJ.MSG to see what's still free.

/******************************************************************
***************       Weapons                       ***************
******************************************************************/

//Small Guns
    //Pistols
#define PID_ZIP_GUN                         (300)
#define PID_9MM_MAUSER                      (122)
#define PID_10MM_PISTOL                     (8)
#define PID_14MM_PISTOL                     (22)
#define PID_DESERT_EAGLE                    (18)
#define PID_DESERT_EAGLE_EXT_MAG            (404)
#define PID_223_PISTOL                      (241)
#define PID_44_MAGNUM_REVOLVER              (313)
#define PID_44_MAGNUM_SPEEDLOADER           (398)
#define PID_NEEDLER_PISTOL                  (388)  // HN Needler
#define PID_PK12_GAUSS_PISTOL               (394)  // 2mm EC
    //Rifles
#define PID_HUNTING_RIFLE                   (10)
#define PID_SCOPED_HUNTING_RIFLE            (287)
#define PID_SPRINGER_RIFLE                  (299)
#define PID_ASSAULT_RIFLE                   (23)
#define PID_ASSAULT_RIFLE_EXT_MAG           (405)
#define PID_SNIPER_RIFLE                    (143)
#define PID_FN_FAL                          (351)
#define PID_FN_FAL_NIGHT_SCOPE              (403)
#define PID_FN_FAL_HPFA                     (500)
#define PID_RED_RYDER_BB_GUN                (161)
#define PID_RED_RYDER_LE_BB_GUN             (162)
#define PID_JONNY_BB_GUN                    (261)
#define PID_INDEPENDENT                     (353)
#define PID_M72_GAUSS_RIFLE                 (392)  // 2mm EC
    //Shotguns
#define PID_SHOTGUN                         (94)
#define PID_SAWED_OFF_SHOTGUN               (385)  // 12 ga.
#define PID_COMBAT_SHOTGUN                  (242)
#define PID_HK_CAWS                         (268)
#define PID_PANCOR_JACKHAMMER               (354)
    //Pistol-machine gun
#define PID_10MM_SMG                        (9)
#define PID_HK_P90C                         (296)
#define PID_TOMMY_GUN                       (283)
#define PID_HK_G11                          (352)
#define PID_HK_G11E                         (391)  // 4.7mm Caseless
#define PID_GREASE_GUN                      (332)

//Big guns
#define PID_FLAMER                          (11)
#define PID_IMPROVED_FLAMETHROWER           (400)
#define PID_ROCKET_LAUNCHER                 (13)
#define PID_MINIGUN                         (12)
#define PID_AVENGER_MINIGUN                 (389)  // 5mm JHP
#define PID_VINDICATOR_MINIGUN              (395)  // 4.7mm Caseless
#define PID_BOZAR                           (350)
#define PID_LIGHT_SUPPORT_WEAPON            (355)
#define PID_M60                             (387)  // 7.62

//Energo
    //Laser
#define PID_LASER_PISTOL                    (16)
#define PID_MAGNETO_LASER_PISTOL            (402)
#define PID_SOLAR_SCORCHER                  (390)  // No ammo
#define PID_LASER_RIFLE                     (118)
#define PID_LASER_RIFLE_EXT_CAP             (401)
#define PID_GATLING_LASER                   (28)
#define PID_ALIEN_LASER_PISTOL              (120)
#define PID_DUAL_LASER_CANNON               (9659) // Vault City Gun Turret
    //Plasma
#define PID_PLASMA_PISTOL                   (24)
#define PID_PLASMA_PISTOL_EXT_CART          (406)
#define PID_PLASMA_RIFLE                    (15)
#define PID_TURBO_PLASMA_RIFLE              (233)
    //Pulse
#define PID_YK32_PULSE_PISTOL               (396)  // Small Energy
#define PID_YK42B_PULSE_RIFLE               (397)  // Micro Fusion

//Throwing
    //Grenade
#define PID_MOLOTOV_COCKTAIL                (159) //Grouped
#define PID_FRAG_GRENADE                    (25) //Grouped
#define PID_PLASMA_GRENADE                  (26) //Grouped
#define PID_PULSE_GRENADE                   (27) //Grouped
    //Other
#define PID_FLARE                           (79) //Grouped
#define PID_ACTIVE_FLARE                    (205) //Active
#define PID_FLARE_GREEN                     (9721) //Grouped
#define PID_ACTIVE_FLARE_GREEN              (9722) //Active
#define PID_FLARE_WHITE                     (9723) //Grouped
#define PID_ACTIVE_FLARE_WHITE              (9724) //Active
#define PID_FLARE_YELLOW                    (9725) //Grouped
#define PID_ACTIVE_FLARE_YELLOW             (9726) //Active

#define PID_PLANT_SPIKE                     (365) //Grouped
#define PID_THROWING_KNIFE                  (45) //Grouped
#define PID_ROCK                            (19) //Grouped
#define PID_FOOLS_GOLD                      (423) //Grouped
#define PID_GOLD_NUGGET                     (572) //Grouped

#define PID_URANIUM_ORE                     (426) //Grouped - USED FOR MICRO FUSION CELL CRAFTING!!
#define PID_REFINED_ORE                     (486) //Grouped - USED FOR MICRO FUSION CELL CRAFTING!!
#define PID_DYNACORD                        (1834) //Grouped
#define PID_FAKE_FRAG_GRENADE               (9674) //Grouped
#define PID_FAKE_PLASMA_GRENADE             (9675) //Grouped
#define PID_FAKE_SUPER_SLEDGE               (9676) //Grouped
#define PID_FAKE_SPEAR                      (9916) //Grouped
#define PID_FAKE_THROWING_KNIFE             (9917) //Grouped

//Melee
    //Cutting
#define PID_KNIFE                           (4)
#define PID_COMBAT_KNIFE                    (236)
#define PID_LIL_JESUS_WEAPON                (517)
#define PID_SHIV                            (383)
#define PID_SWITCHBLADE                     (319)
#define PID_WAKIZASHI_BLADE                 (522)
    //Pricking
#define PID_SPEAR                           (7)
#define PID_SHARP_SPEAR                     (280)
#define PID_SHARPENED_POLE                  (320)
    //Shock
#define PID_CLUB                            (5)
#define PID_CROWBAR                         (20)
#define PID_WRENCH                          (384)
#define PID_SLEDGEHAMMER                    (6)
#define PID_LOUISVILLE_SLUGGER              (386)
#define PID_SUPER_SLEDGE                    (115)
    //Electric
#define PID_CATTLE_PROD                     (160)
#define PID_SUPER_CATTLE_PROD               (399)
#define PID_RIPPER                          (116)

    // Plasma
#define PID_PLASMA_TORCH                    (610)

//Unarmed
#define PID_BOXING_GLOVES                   (292)
#define PID_PLATED_BOXING_GLOVES            (293)
#define PID_RING_BOXING_GLOVES              (496)
#define PID_RING_PLATED_BOXING_GLOVES       (497)
#define PID_POWER_FIST                      (235)
#define PID_BRASS_KNUCKLES                  (21)
#define PID_SPIKED_KNUCKLES                 (234)
#define PID_MEGA_POWER_FIST                 (407)

//Special
#define PID_ROBO_ROCKET_LAUNCHER            (270)
#define PID_PHAZER                          (393)  // Small Energy
#define PID_DEATHCLAW_CLAW_1                (371)
#define PID_DEATHCLAW_CLAW_2                (372)
#define PID_FIRE_GECKO_FLAME_WEAPON         (427)
#define PID_FLAME_BREATH                    (427)
#define PID_WEAK_FLAME_BREATH               (616)
#define PID_SPECIAL_BOXER_WEAPON            (489)
#define PID_GUN_TURRET_WEAPON               (498)
#define PID_EYEBALL_FIST_1                  (290)
#define PID_EYEBALL_FIST_2                  (291)
#define PID_DUAL_MINIGUN                    (518)
#define PID_HEAVY_DUAL_MINIGUN              (520)
#define PID_END_BOSS_KIFE                   (530)
#define PID_END_BOSS_PLASMA_GUN             (531)
#define PID_HOLY_HAND_GRENADE               (421) // Grenade

/******************************************************************
***************       Ammo                          ***************
******************************************************************/

//Bullets
#define PID_4_7MM_CASELESS                  (359)
#define PID_5MM_JHP                         (35)
#define PID_5MM_AP                          (36)
#define PID_7_62MM_AMMO                     (363)
#define PID_9MM_AMMO                        (360)
#define PID_9MM_BALL                        (121)
#define PID_10MM_JHP                        (29)
#define PID_10MM_AP                         (30)
#define PID_14MM_AP                         (33)
#define PID_44_MAGNUM_JHP                   (31)
#define PID_44_FMJ_MAGNUM                   (111)
#define PID_44_MAGNUM_FMJ           (111) // to be name-consistent
#define PID_45_CALIBER_AMMO                 (357)
#define PID_223_FMJ                         (34)
#define PID_22_AMMO                         (850)
#define PID_SHOTGUN_SHELLS                  (95)
#define PID_2MM_EC_AMMO                     (358)

//Other
#define PID_EXPLOSIVE_ROCKET                (14)
#define PID_ROCKET_AP                       (37)
#define PID_FLAMETHROWER_FUEL               (32)
#define PID_FLAMETHROWER_FUEL_MK_II         (382)
#define PID_SMALL_ENERGY_CELL               (38)
#define PID_SEC                             (38) // alias
#define PID_MICRO_FUSION_CELL               (39)
#define PID_MFC                             (39) // alias
#define PID_BBS                             (163)
#define PID_HN_NEEDLER_CARTRIDGE            (361)
#define PID_HN_AP_NEEDLER_CARTRIDGE         (362)

//Special
#define PID_ROBO_ROCKET_AMMO                (274)

/******************************************************************
***************       Drugs                         ***************
******************************************************************/

//Preparations
#define PID_STIMPAK                         (40)
#define PID_RADAWAY                         (48)
#define PID_ANTIDOTE                        (49)
#define PID_RAD_X                           (109)
#define PID_SUPER_STIMPAK                   (144)
#define PID_JET_ANTIDOTE                    (260)
#define PID_HEALING_POWDER                  (273)
#define PID_HYPO                            (525)
#define PID_WEAK_HEALING_POWDER             (9655)

//Alcohol
#define PID_NUKA_COLA                       (106)
#define PID_BEER                            (124)
#define PID_BOOZE                           (125)
#define PID_GAMMA_GULP_BEER                 (310)
#define PID_ROENTGEN_RUM                    (311)
#define PID_ROT_GUT                         (469)

//Drug
#define PID_MENTATS                         (53)
#define PID_BUFFOUT                         (87)
#define PID_PSYCHO                          (110)
#define PID_JET                             (259)

//Other
#define PID_MUTATED_FRUIT                   (71)
#define PID_IGUANA_ON_A_STICK               (81)
#define PID_MEAT_ON_A_STICK                 (103)
#define PID_COOKIE                          (378)

//Special
#define PID_HYPO_POISON                     (334)
#define PID_MUTATED_TOE                     (473)
#define PID_KITTY_SEX_DRUG_AGILITY          (480)  // + 1 agility for 1 hr
#define PID_KITTY_SEX_DRUG_INTELLIGENCE     (481)  // + 1 iq for 1 hr
#define PID_KITTY_SEX_DRUG_STRENGTH         (482)  // + 1 strength for 1 hr
#define PID_MONUMENT_CHUNCK                 (424)

/******************************************************************
***************       Container                     ***************
******************************************************************/

#define PID_FRIDGE                          (42)
#define PID_ICE_CHEST_LEFT                  (43)
#define PID_ICE_CHEST_RIGHT                 (44)
#define PID_BAG                             (46)
#define PID_BACKPACK                        (90)
#define PID_BROWN_BAG                       (93)
#define PID_FOOTLOCKER_CLEAN_LEFT           (128)
#define PID_FOOTLOCKER_RUSTY_LEFT           (129)
#define PID_FOOTLOCKER_CLEAN_RIGHT          (130)
#define PID_FOOTLOCKER_RUSTY_RIGHT          (131)
#define PID_LOCKER_CLEAN_LEFT               (132)
#define PID_LOCKER_RUSTY_LEFT               (133)
#define PID_LOCKER_CLEAN_RIGHT              (134)
#define PID_LOCKER_RUSTY_RIGHT              (135)
#define PID_WALL_LOCKER_CLEAN_LEFT          (136)
#define PID_WALL_LOCKER_CLEAN_RIGHT         (137)
#define PID_WALL_LOCKER_RUSTY_LEFT          (138)
#define PID_WALL_LOCKER_RUSTY_RIGHT         (139)
#define PID_CONTAINER_WOOD_CRATE            (180)
#define PID_VAULT_DWELLER_BONES             (211)
#define PID_SMALL_POT                       (243)
#define PID_TALL_POT                        (244)
#define PID_CHEST                           (245)
#define PID_LEFT_ARROYO_BOOKCASE            (246)
#define PID_RIGHT_ARROYO_BOOKCASE           (247)
#define PID_OLIVE_POT                       (248)
#define PID_FLOWER_POT                      (249)
#define PID_HUMAN_BONES                     (250)
#define PID_CRASHED_VERTI_BIRD              (330)

#define PID_GRAVESITE_V                     (344)
#define PID_GRAVESITE_H                     (345)

#define PID_LG_LT_AMMO_CRATE                (367)
#define PID_SM_LT_AMMO_CRATE                (368)
#define PID_LG_RT_AMMO_CRATE                (369)
#define PID_SM_RT_AMMO_CRATE                (370)

#define PID_HIDDEN_CONTAINER                (467)

//From Misc
#define PID_ALIEN_SIDE                      (107)
#define PID_ALIEN_FORWARD                   (108)
#define PID_STONE_HEAD                      (425)
#define PID_WAGON_RED                       (434)
#define PID_WAGON_GREY                      (435)
#define PID_CAR_TRUNK                       (455)
#define PID_WALL_SAFE                       (501)
#define PID_FLOOR_SAFE                      (502)
#define PID_POOL_TABLE_1                    (510)
#define PID_POOL_TABLE_2                    (511)
#define PID_POOL_TABLE_3                    (512)
#define PID_POOL_TABLE_4                    (513)
#define PID_POOL_TABLE_5                    (514)
#define PID_POOL_TABLE_6                    (515)
#define PID_POOR_BOX                        (521)

//New
#define PID_BOOKCASE_0                      (60)
#define PID_BOOKCASE_1                      (61)
#define PID_BOOKCASE_2                      (62)
#define PID_BOOKCASE_3                      (63)
#define PID_BOOKCASE_4                      (64)
#define PID_BOOKCASE_5                      (65)
#define PID_DESK_0                          (66)
#define PID_DESK_1                          (67)
#define PID_DRESSER_0                       (68)
#define PID_DRESSER_1                       (69)
#define PID_DRESSER_2                       (70)
#define PID_BOOKSELF_0                      (145)
#define PID_BOOKSELF_1                      (146)
#define PID_BOOKSELF_2                      (147)
#define PID_BOOKSELF_3                      (149)
#define PID_SHELVES_0                       (151)
#define PID_SHELVES_1                       (152)
#define PID_SHELVES_2                       (153)
#define PID_SHELVES_3                       (155)
#define PID_WORKBENCH                       (157)
#define PID_TOOL_BOARD                      (158)
#define PID_IGUANA_STAND                    (165)
#define PID_TABLE_0                         (166)
#define PID_TABLE_1                         (167)
#define PID_STUFF_0                         (168)
#define PID_STUFF_1                         (169)
#define PID_STUFF_2                         (170)
#define PID_STUFF_3                         (171)
#define PID_STUFF_4                         (172)
#define PID_STUFF_5                         (173)
#define PID_STUFF_6                         (174)
#define PID_STUFF_7                         (175)
#define PID_STUFF_8                         (176)
#define PID_STUFF_9                         (177)
#define PID_STUFF_10                        (178)
#define PID_STUFF_11                        (179)
#define PID_DESK_3                          (181)
#define PID_DESK_4                          (182)
#define PID_DESK_5                          (183)
#define PID_DESK_6                          (184)
#define PID_DESK_7                          (185)
#define PID_DESK_8                          (186)
#define PID_DESK_9                          (187)
#define PID_LOCKER_7                        (188)
#define PID_LOCKER_8                        (189)
#define PID_BOX_0                           (197)
#define PID_BOX_1                           (198)
#define PID_BOX_2                           (199)
#define PID_BOX_3                           (200)
#define PID_BOX_4                           (201)
#define PID_BOX_5                           (202)
#define PID_BOX_6                           (203)
#define PID_BOX_7                           (204)
#define PID_REMAINSOF_GIZMO             (213)
#define PID_DESK_10                     (214)
#define PID_DEAD_REDSHIRT_0                 (526)
#define PID_DEAD_REDSHIRT_1                 (527)
#define PID_DEAD_REDSHIRT_2                 (528)
#define PID_MINING_MACHINE          (529)
#define PID_TOBACCO_BENCH           (9636)
#define PID_CAMPFIRE                (9913)
#define PID_ADVANCED_BENCH          (9914)

//Car bags
#define PID_HUMMER_BAG                      (801)
#define PID_VERTIBIRD_BAG                   (809)
#define PID_HIGHWAYMAN_BAG                  (817)
#define PID_BUGGY_BAG                       (818)
#define PID_SCOUT_BAG                       (819)
#define PID_CARAVAN_WHITE_BAG           (603)
#define PID_CARAVAN_ORANGE_BAG          (604)
#define PID_BOAT_BAG                        (823)
#define PID_POLICE_BAG                          (9747)
#define PID_MOTORCYCLE_BAG                      (9926)
#define PID_FIRETRUCK_BAG                       (9928)
#define PID_TRUCK_BAG                       (9930)
#define PID_BARGE_BAG                       (9933)

/******************************************************************
***************       Keys                          ***************
******************************************************************/

//From Misc
#define PID_KEY                             (82)
#define PID_KEYS                            (83)
#define PID_RED_PASS_KEY                    (96)
#define PID_BLUE_PASS_KEY                   (97)
#define PID_NUKE_KEY                        (105)
#define PID_YELLOW_PASS_KEY                 (223)
#define PID_TEMPLE_KEY                      (438)
#define PID_JAIL_KEY                        (456) //PID_CELL_DOOR_KEY

/******************************************************************
***************       Misc.                         ***************
******************************************************************/

//Money
#define PID_BOTTLE_CAPS                     (41)
#define PID_MORNING_STAR_MINE               (420)
#define PID_KOKOWEEF_MINE_SCRIP             (494)
#define PID_REAL_BOTTLE_CAPS                (519)
#define PID_DOLLAR                          (9715)

//Basic parts
#define PID_PUMP_PARTS                      (98) //Trash
#define PID_JUNK                            (PID_PUMP_PARTS)
#define PID_SCORPION_TAIL                   (92)
#define PID_ROPE                            (127)
#define PID_BROC_FLOWER                     (271)
#define PID_XANDER_ROOT                     (272)
#define PID_GECKO_PELT                      (276)
#define PID_GOLDEN_GECKO_PELT               (277)
#define PID_FLINT                           (278)
#define PID_MEAT_JERKY                      (284)
#define PID_RADSCORPION_PARTS               (285)
#define PID_FIREWOOD                        (286)
#define PID_HYPODERMIC_NEEDLE               (318)
#define PID_EMPTY_JET                       (416)
#define PID_BOTTLE_GLASS                    (542) //Empty glass bottle
#define PID_BOTTLE_EMPTY                    (532) //Пустая бутылка
#define PID_PART_OF_ROPE                    (534) //Кусок веревки
#define PID_METAL_TRASH                     (475) //Металлический мусор
#define PID_GUNPOWDER                       (535) //Коробка пороха
#define PID_METAL_ORE                       (536) //Железная руда
#define PID_MINERAL                         (537) //Минерал
#define PID_TUBE                            (538) //Металлическая труба
#define PID_STEEL                           (50) //Сталь
#define PID_BRAHMIN_SKIN                    (449) //Шкура брамина
#define PID_MEAT                            (539) //Мясо
#define PID_CIGARETTES                      (541) //Сигареты
#define PID_SCORPION_EGG                    (9911)  

//Self-special parts
#define PID_MOTOR                           (89)
#define PID_MOTIVATOR                       (229)
#define PID_PLASMA_TRANSFORMER              (307)
#define PID_MINE_PART                       (419)
#define PID_EXPLOSIVE_SWITCH                (454)
#define PID_ROBOT_MOTIVATOR                 (364)
#define PID_EXCAVATOR_CHIP                  (422)
#define PID_NAVCOM_PARTS                    (479)
#define PID_K9_MOTIVATOR                    (488)

//Special parts
#define PID_WATER_CHIP                      (55)
#define PID_CAR_FUEL_CELL_CONTROLLER        (253)   //For car
#define PID_CAR_FUEL_INJECTION              (254)   //For car
#define PID_HY_MAG_PART                     (258)
#define PID_ROBOT_PARTS                     (269)
#define PID_COMPUTER_VOICE_MODULE           (356)
#define PID_V15_COMPUTER_PART               (377)
#define PID_TANKER_METALLBOX            (705)   //Stranded tanker quest
#define PID_TANKER_PUMPPART         (706)   //Stranded tanker quest and Junktown Power House
#define PID_CARAVAN_BOX             (9626)  // Box for "caravan unloading" job
#define PID_Q_LA_WAREHOUSE_LETTER               (9729)  // LA Warehouse Quest item
#define PID_Q_GUNRUNNER_BOX         (9741)  // Gun Runner Caravan quest item
#define PID_Q_WESTIN_HOLO           (9909)  // Westin Ranch Quest 2 Holodisk
#define PID_Q_GUNRUNNER_COURIER_BOX     (9912)  // Gun Runners Courier quest item

//Body parts
#define PID_TANGLERS_HAND                   (114)
#define PID_BONES                           (251)   //PID_ANNA_BONES
#define PID_DIXON_EYE                       (281)
#define PID_CLIFTON_EYE                     (282)
#define PID_CYBERNETIC_BRAIN                (321)
#define PID_HUMAN_BRAIN                     (322)
#define PID_CHIMP_BRAIN                     (323)
#define PID_ABNORMAL_BRAIN                  (324)
#define PID_GOLD_TOOTH                      (429)
#define PID_PLAYERS_EAR                     (484)
#define PID_MASTICATORS_EAR                 (485)
#define PID_DECOMPOSING_BODY                (507)   //PID_FN_FAL_HPFA

//Plans
#define PID_DR_HENRY_PAPERS                 (340)
#define PID_VERTIBIRD_PLANS                 (446)

//Chips
#define PID_MEM_CHIP_BLUE                   (503) //PID_BLUE_MEMORY_MODULE
#define PID_MEM_CHIP_GREEN                  (504) //PID_GREEN_MEMORY_MODULE
#define PID_MEM_CHIP_RED                    (505) //PID_RED_MEMORY_MODULE
#define PID_MEM_CHIP_YELLOW                 (506) //PID_YELLOW_MEMORY_MODULE
#define PID_DERMAL_PIP_BOY_DISK             (499)
#define PID_PIP_BOY_MEDICAL_ENHANCER        (516)

//Tools
    //Tech
#define PID_MULTI_TOOL                      (75)  // +25 repair
#define PID_SUPER_TOOL_KIT                  (308) // +50 repair
#define PID_OIL_CAN                         (412) // 100% repair
#define PID_LOCKPICKS                       (84)  // +25 lockpick simple
#define PID_EXP_LOCKPICK_SET                (410) // +50 lockpick simple
#define PID_ELECTRONIC_LOCKPICKS            (77)  // +25 lockpick electronic
#define PID_ELEC_LOCKPICK_MKII              (411) // +50 lockpick electronic
#define PID_LOCK                            (549)

    //Medical
#define PID_FIRST_AID_KIT                   (47)  // +25 first aid
#define PID_FIELD_MEDIC_KIT                 (408) // +50 first aid
#define PID_DOCTORS_BAG                     (91)  // +25 doctor
#define PID_PARAMEDICS_BAG                  (409) // +50 doctor
#define PID_MEDICAL_SUPPLIES                (428)
#define PID_BIO_GEL                         (440)
    //Other
#define PID_SHOVEL                          (289) //Лопата
#define PID_PLANK                           (297) //Лом
#define PID_AXE                             (543) //Топор
    //Special
#define PID_GEIGER_COUNTER                  (52)
#define PID_ACTIVE_GEIGER_COUNTER           (207) //Active
#define PID_STEALTH_BOY                     (54)
#define PID_ACTIVE_STEALTH_BOY              (210) //Active
#define PID_MOTION_SENSOR                   (59)
#define PID_ACTIVE_MOTION_SENSOR            (208) //Active
#define PID_WATER_BAG                       (605)

//Books
#define PID_BIG_BOOK_OF_SCIENCE             (73)
#define PID_DEANS_ELECTRONICS               (76)
#define PID_FIRST_AID_BOOK                  (80)
#define PID_SCOUT_HANDBOOK                  (86)
#define PID_GUNS_AND_BULLETS                (102)
#define PID_CATS_PAW                        (225)
#define PID_TECHNICAL_MANUAL                (228)
#define PID_CHEMISTRY_MANUAL                (237)
#define PID_CATS_PAW_ISSUE_5                (331)
#define PID_BECKY_BOOK                      (471)

//Dolls
#define PID_SMALL_STATUETTE                 (224)
#define PID_MR_NIXON_DOLL                   (491)
#define PID_BLOW_UP_DOLL                    (508)
#define PID_POPPED_BLOW_UP_DOLL             (509)

//Other
#define PID_LIGHTER                         (101) //Zippo
#define PID_FLOWER                          (117)
#define PID_WATER_FLASK                     (126)
#define PID_BOX_OF_NOODLES                  (226)
#define PID_FROZEN_DINNER                   (227)
#define PID_RUBBER_BOOTS                    (262)
#define PID_CHEEZY_POOFS                    (295)
#define PID_BLUE_CONDOM                     (314)
#define PID_GREEN_CONDOM                    (315)
#define PID_RED_CONDOM                      (316)
#define PID_COSMETIC_CASE                   (317)
#define PID_DICE                            (325) //Play dice
#define PID_LOADED_DICE                     (326) //Play dice
#define PID_DECK_OF_CARDS                   (436) //Play cards
#define PID_MARKED_DECK_OF_CARDS            (437) //Play cards

//Special
#define PID_BRIEFCASE                       (72)
#define PID_MUTAGENIC_SYRUM                 (329) //Иньекция от мутации
#define PID_HEART_PILLS                     (333) //Таблетки от серца
#define PID_SPECTACLES                      (415) //Eye glasses
#define PID_OXYGEN_TANK                     (417)
#define PID_POISON_TANK                     (418)
#define PID_HOWITZER_SHELL                  (430) //Ammo of turret in SAD
#define PID_MIRROR_SHADES                   (433) //Eye glasses
#define PID_POCKET_LINT                     (439) //Trash
#define PID_BALL_GAG                        (470) //Кляп
#define PID_DAISIES                         (474) //Горшок с цветами
#define PID_BOTTLE_FULL                     (533) //Полная бутылка
#define PID_GOGGLES                         (570)

#define PID_DOG_TAGS                        (56)
#define PID_ELECTRONIC_BUG                  (57)
#define PID_FUZZY_PAINTING                  (78)
#define PID_GOLD_LOCKET                     (99)
#define PID_TAPE_RECORDER                   (104)
#define PID_URN                             (112)
#define PID_NECKLACE                        (119) //Ожерелье
#define PID_PSYCHIC_NULLIFIER               (123)
#define PID_BLACK_COC_BADGE                 (141) //Habbology
#define PID_RED_COC_BADGE                   (142) //Habbology
#define PID_BARTER_TANDI                    (212)
#define PID_ANNA_GOLD_LOCKET                (252)
#define PID_DAY_PASS                        (255) //Vault-City
#define PID_FAKE_CITIZENSHIP                (256) //Vault-City
#define PID_CORNELIUS_GOLD_WATCH            (257)
#define PID_VIC_RADIO                       (266)
#define PID_VIC_WATER_FLASK                 (267)
#define PID_TROPHY_OF_RECOGNITION           (275)
#define PID_NEURAL_INTERFACE                (279)
#define PID_CLIPBOARD                       (301)
#define PID_DECK_OF_TRAGIC_CARDS            (304) //Play cards
#define PID_TALISMAN                        (309)
#define PID_SLAG_MESSAGE                    (263)
#define PID_PART_REQUISITION_FORM           (312)
#define PID_EASTER_EGG                      (327)
#define PID_MAGIC_8_BALL                    (328)
#define PID_MOORE_BAD_BRIEFCASE             (335)
#define PID_MOORE_GOOD_BRIEFCASE            (336)
#define PID_PRESIDENTIAL_PASS               (341) //Ncr
#define PID_RANGER_PIN                      (342) //Ncr
#define PID_RANGER_MAP                      (343)
#define PID_GECK                            (366)
#define PID_STABLES_ID_BADGE                (413) //Пропуск в канюшни
#define PID_RAMIREZ_BOX_CLOSED              (431)
#define PID_RAMIREZ_BOX_OPEN                (432)
#define PID_BLONDIE_DOG_TAG                 (441) //Бирка
#define PID_ANGEL_EYES_DOG_TAG              (442) //Бирка
#define PID_TUCO_DOG_TAG                    (443) //Бирка
#define PID_RAIDERS_MAP                     (444)
#define PID_SHERIFF_BADGE                   (445) //Значек шерифа
#define PID_ACCOUNT_BOOK                    (448) //Книга по разборкам в Нью-Рено
#define PID_TORN_PAPER_1                    (450) //Part1 password
#define PID_TORN_PAPER_2                    (451) //Part2 password
#define PID_TORN_PAPER_3                    (452) //Part3 password
#define PID_PASSWORD_PAPER                  (453) //All password
#define PID_ELRON_FIELD_REP                 (457) //Отчет Хаббологов
#define PID_SMITTY_MEAL                     (468) //Еда Смитти
#define PID_ENLIGHTENED_ONE_LETTER          (476) //Доклад
#define PID_FALLOUT_2_HINTBOOK              (483)
#define PID_MOUSE_SKIN                      (540) //Шкура мыши
#define PID_MEAT_BAG                (546) //a fucking bag of meat
#define PID_METZGERS_CASE           (547) //a case for metzger you moron!
#define PID_MORDINOS_CASE           (548) //mordino joining quest case


//Special keys
#define PID_ACCESS_CARD                     (140)
#define PID_SECURITY_CARD                   (221)
#define PID_TRAPPER_TOWN_KEY                (298)
#define PID_YELLOW_REACTOR_KEYCARD          (305)
#define PID_RED_REACTOR_KEYCARD             (306)
#define PID_V15_KEYCARD                     (373)
#define PID_VAULT_13_SHACK_KEY              (414)
#define PID_PRES_ACCESS_KEY                 (495)
#define PID_TANKER_FOB                      (492)

//Holodisks
#define PID_BROTHERHOOD_TAPE                (164)
#define PID_DISK_FEV                        (190)
#define PID_DISK_SECURITY                   (191)
#define PID_DISK_ALPHA_EXPERIMENT           (192)
#define PID_DISK_DELTA_EXPERIMENT           (193)
#define PID_DISK_VREES_EXPERIMENT           (194)
#define PID_DISK_MUTANT_TRANSMISSIONS       (196)
#define PID_DISK_BROTHERHOOD_HISTORY        (215)
#define PID_DISK_MAXSON_HISTORY             (216)
#define PID_DISK_MAXSON_JOURNAL             (217)
#define PID_DISK_VAULT_RECORDS              (230)
#define PID_DISK_MILITARY_BASE_SEC_CODE     (231)
#define PID_DISK_REGULATOR_TRANSMISSIONS    (238)
#define PID_HOLODISK_FAKE_V13               (294)
#define PID_GECKO_DATA_DISK                 (302)
#define PID_REACTOR_DATA_DISK               (303)
#define PID_LYNETTE_HOLO                    (337)
#define PID_WESTIN_HOLO                     (338)
#define PID_SPY_HOLO                        (339)
#define PID_BISHOPS_HOLODISK                (447)
#define PID_ENCLAVE_HOLODISK_5              (458)
#define PID_ENCLAVE_HOLODISK_1              (459)
#define PID_ENCLAVE_HOLODISK_2              (460)
#define PID_ENCLAVE_HOLODISK_3              (461)
#define PID_ENCLAVE_HOLODISK_4              (462)
#define PID_EVACUATION_HOLODISK             (463)
#define PID_EXPERIMENT_HOLODISK             (464)
#define PID_MEDICAL_HOLODISK                (465)
#define PID_PASSWORD_HOLODISK               (466)
#define PID_ELRON_MEMBER_HOLO               (472)
#define PID_BROADCAST_HOLODISK              (477)
#define PID_SIERRA_MISSION_HOLODISK         (478)
#define PID_NCR_HISTORY_HOLODISK            (490)
#define PID_ELRON_TEACH_HOLO                (493)

//Other, Inventory pic n/a
#define PID_WATCH                           (88)
#define PID_MISC_BOOKCASE_0                 (148)
#define PID_MISC_BOOKCASE_1                 (150)
#define PID_MISC_SHELVES_0                  (154)
#define PID_MISC_SHELVES_1                  (156)
#define PID_DISK_BROTHERHOOD_HONOR_CODE     (195)
#define PID_BARTER_LIGHT_HEALING            (218)
#define PID_BARTER_MEDIUM_HEALING           (219)
#define PID_BARTER_HEAVY_HEALING            (220)
#define PID_SMITH_COOL_ITEM                 (264)
#define PID_CAR_FUEL_CELL                   (288)

#define PID_POWER_SERVOS                    (707) // Super Powerfist
#define PID_ADV_SUPERBURN                   (708) // Advanced Flamerfuel
#define PID_DETONATOR_PARTS                 (709) // Used for dynamite
#define PID_EXTENDED_MAGAZINE               (710) // Used for bigger Assault Rifle Mag (and maybe Desert Eagle Mag)
#define PID_EXT_CAPACITOR_BATTERY           (711) // Laser Rifle
#define PID_PLASMA_BOLT_CHAMBER             (712) // Turbo Plasmarifle
#define PID_IMP_CAPACITOR_BATTERY           (713) // Super Cattle Prod
#define PID_MFT_SYSTEM                      (714) // Improved Laserpistol
#define PID_HEATERCHAMBER                   (715) // Improved Flamer
#define PID_SCOPE                           (716) // All weapons with scope
#define PID_SPEED_LOADER                    (717) // Revolver
#define PID_LASER_POINTER                   (718) // FNFAL with laser pointer, etc

//Traps, active see in Misc2
#define PID_DYNAMITE                        (51) //Has timer
#define PID_PLASTIC_EXPLOSIVES              (85)
#define PID_MINE                            (544)
// add bear trap and alarm trigger

// Blueprints, weapons and explosives, ST_WEAPON_BLUEPRINTS
#define PID_BLUEPRINT_14MM_PISTOL           (9750)
#define PID_BLUEPRINT_NEEDLER_PISTOL        (9751)
#define PID_BLUEPRINT_223_PISTOL            (9752)
#define PID_BLUEPRINT_ASSAULT_RIFLE         (9753)
#define PID_BLUEPRINT_FN_FAL                (9754)
#define PID_BLUEPRINT_SNIPER_RIFLE          (9755)
#define PID_BLUEPRINT_P90                   (9756)
#define PID_BLUEPRINT_HK_CAWS               (9757)
#define PID_BLUEPRINT_PANCOR_JACKHAMMER     (9758)
#define PID_BLUEPRINT_PLASMA_PISTOL         (9759)
#define PID_BLUEPRINT_LASER_RIFLE           (9760)
#define PID_BLUEPRINT_PLASMA_RIFLE          (9761)
#define PID_BLUEPRINT_MEGA_POWER_FIST       (9762)
#define PID_BLUEPRINT_SUPER_CATTLE_PROD     (9763)
#define PID_BLUEPRINT_RIPPER                (9764)
#define PID_BLUEPRINT_ROCKET_LAUNCHER       (9765)
#define PID_BLUEPRINT_LIGHT_SUPPORT_WEAPON  (9766)
#define PID_BLUEPRINT_MINIGUN               (9767)
#define PID_BLUEPRINT_PLASMA_GRENADE        (9768)
#define PID_BLUEPRINT_DYNAMITE              (9769)
#define PID_BLUEPRINT_PLASTIC_EXPLOSIVES    (9770)
#define PID_BLUEPRINT_SUPER_SLEDGE          (9771)
#define PID_BLUEPRINT_IMPROVED_FLAMETHROWER (9772)

// Blueprints, armours, ST_ARMOR_BLUEPRINTS
#define PID_BLUEPRINT_METAL_ARMOR_MK_II     (9780)
#define PID_BLUEPRINT_TESLA_ARMOR           (9781)
#define PID_BLUEPRINT_COMBAT_ARMOR          (9782)
#define PID_BLUEPRINT_METAL_HELMET_MK2      (9810)
#define PID_BLUEPRINT_TESLA_HELMET          (9811)
#define PID_BLUEPRINT_COMBAT_HELMET         (9812)

// Blueprints, misc, ST_MISC_BLUEPRINTS
#define PID_BLUEPRINT_SUPER_STIMPAK         (9790)
#define PID_BLUEPRINT_STIMPAK               (9791)
#define PID_BLUEPRINT_JET                   (9792)
#define PID_BLUEPRINT_BUFFOUT               (9793)
#define PID_BLUEPRINT_MENTATS               (9794)
#define PID_BLUEPRINT_PSYCHO                (9795)
#define PID_BLUEPRINT_RADAWAY               (9796)
#define PID_BLUEPRINT_RADX                  (9797)

// Random Blueprints
#define PID_BLUEPRINT_RANDOM_1              (9800) // low
#define PID_BLUEPRINT_RANDOM_2              (9801) // med
#define PID_BLUEPRINT_RANDOM_3              (9802) // high

// Random Blueprints by type ane level
#define PID_BLUEPRINT_ARMOR_1              (11004)
#define PID_BLUEPRINT_ARMOR_2              (11005)
#define PID_BLUEPRINT_ARMOR_3              (11006)
#define PID_BLUEPRINT_DRUG_1               (11007)
#define PID_BLUEPRINT_DRUG_2               (11008)
#define PID_BLUEPRINT_DRUG_3               (11009)
#define PID_BLUEPRINT_WEAPON_1             (11010)
#define PID_BLUEPRINT_WEAPON_2             (11011)
#define PID_BLUEPRINT_WEAPON_3             (11012)
#define PID_BLUEPRINT_AMMO_1               (11013)
#define PID_BLUEPRINT_AMMO_2               (11014)
#define PID_BLUEPRINT_AMMO_3               (11015)

/******************************************************************
***************       Misc2                         ***************
******************************************************************/

//Special
#define PID_HOLODISK                        (58)
#define PID_RADIO                           (100)
#define PID_MEMO                            (487) //PID_MEMO_FROM_FRANCIS
#define PID_MAP                             (523) //PID_SURVEY_MAP
#define PID_TICKET                          (564) //Used for train
#define PID_DOGTAGS                         (9601)

// tanker
#define PID_TANKER_STEAM                    (615)
#define PID_TANKER_HOLE                     (703)
#define PID_TANKER_ROPE_HOLE                (704)

//Cars
#define PID_HUMMER                          (800) //Car
#define PID_BUGGY                           (802) //Car
#define PID_SCOUT                           (805) //Car
#define PID_VERTIBIRD                       (808) //Big Car
#define PID_HIGHWAYMAN                      (816) //Car
#define PID_RUSTY_HIGHWAYMAN                (550) //Car
#define PID_CARAVAN_WHITE                   (558)
#define PID_CARAVAN_ORANGE                  (559)
#define PID_COCKROACH                       (560)
#define PID_WRECKED_CAR                     (563)
#define PID_BOAT                            (824) //Car
#define PID_TARDIS                          (9673)
#define PID_TRAIN                           (9688)
#define PID_TRAIN2                          (9689)
#define PID_POLICE_CAR                      (9746)
#define PID_MOTORCYCLE                      (9925)
#define PID_FIRETRUCK                       (9927)
#define PID_TRUCK                       (9929)
#define PID_BARGE                       (9931)

//Traps
#define PID_ACTIVE_DYNAMITE                 (206)
#define PID_TOGGLE_SWITCH                   (222)
#define PID_ACTIVE_PLASTIC_EXPLOSIVE        (209)
#define PID_ACTIVE_MINE                     (545)

// Manifacture, Still
#define PID_STILL_B_BROKEN                  (810)
#define PID_STILL_S_BROKEN                  (811)
#define PID_STILL_B                         (812) // Beer still
#define PID_STILL_B_ACTIVE                  (813)
#define PID_STILL_S                         (814) // Booz (Schlager) still
#define PID_STILL_S_ACTIVE                  (815)


/******************************************************************
***************       Doors                          **************
******************************************************************/
#define PID_VAULT_CITY_GATE                 (3337)
#define PID_CORRUGATED_GATE                 (3818)
#define PID_ACCESS_GATE_1                   (3999)
#define PID_ACCESS_GATE_2                   (4000)

/******************************************************************
***************       FACTIONS MOD              ***************
******************************************************************/

#define PID_OLD_SHOVEL                      (600)

#define PID_QUESTIONNAIRE                   (1800)  // Quest item: VC
#define PID_ELECTRONIC_PARTS                (1801)  // Resources
#define PID_ALLOYS                          (1802)  // "
#define PID_BRAHMIN_HIDE                    (1803)  // "
#define PID_METAL_PARTS                     (1804)  // "
#define PID_DAY_PASS2                       (1805)  // VC
#define PID_BOUNTY_DOG_TAG                  (1806)  // Bounties
#define PID_LOST_HILLS_DISK                 (1807)  // BoS
#define PID_BOS_DISK                        (1808)  // BoS
#define PID_PROCESSED_BOS_DISK              (1809)  // BoS
#define PID_BARREL_JUNK                     (1810)  // Production
#define PID_PLANT_TOBACCO                   (1812)  // "
#define PID_PLANT_FIBER                     (1817)  // "
#define PID_STILL_ROTGUT                    (1819)  // "
#define PID_TREE1_FIREWOOD                  (1820)  // "
#define PID_TREE2_FIREWOOD                  (1821)  // "
#define PID_TREE3_FIREWOOD                  (1822)  // "
#define PID_TABLE_BRAHMIN                   (1823)  // "
#define PID_FIBER                           (1824)  // "
#define PID_PROD_BROC                       (1825)  // "
#define PID_PROD_XANDER             (1826)  // "
#define PID_PROD_FLINT              (1827)  // "
#define PID_BRAHMINSHIT             (9627)
#define PID_ALLOYS2                 (9628)
#define PID_CHEMICALS               (9629)
#define PID_FIREGECKO_PELT          (9630)
#define PID_GUNPOWDER2              (9631)
#define PID_FIBRE2              (9632)
#define PID_ORE2                (9633)
#define PID_MINERALS2               (9634)
#define PID_TOBACCO                 (9635)
#define PID_METAL_PARTS2                        (9653)  // "
#define PID_CAR_REPLACEMENT_PARTS       (9681)
#define PID_ELECTRONIC_PARTS2           (9918)
#define PID_CHEMICALS2              (9919)
#define PID_PLANT_APPLE_TREE            (9910)
#define PID_PLANT_BARLEY            (9920)
#define PID_BARLEY              (9921)

// New traps
#define PID_BEAR_TRAP                       (9850)
#define PID_BEAR_TRAP_SPRUNG                (9851)

// Containers for sale
#define PID_CONTAINER_ICE_CHEST_R       (1828)
#define PID_CONTAINER_ICE_CHEST_L       (1829)
#define PID_CONTAINER_FOOTLOCKER_R      (1830)
#define PID_CONTAINER_FOOTLOCKER_L      (1831)
#define PID_CONTAINER_AMMOBOX_R         (1832)
#define PID_CONTAINER_AMMOBOX_L         (1833)

// map with prospect location marked
#define PID_PROSPECT_MAP                (1850)

// brahmin's cakes
#define PID_BRAHMIN_SHIT_1              (1854)
#define PID_BRAHMIN_SHIT_2              (1855)
#define PID_BRAHMIN_SHIT_3              (1856)
#define PID_BRAHMIN_SHIT_4              (1857)

// barf?
#define PID_BARF_1                      (1858)
#define PID_BARF_2                      (1859)

#define PID_CARAVAN_SPAWNPOINT          (1860)

#define PID_PRIMITIVE_TOOL              (1863)
#define PID_WORKBENCH_RAIDERS           (1864)
#define PID_AMMO_FACILITY               (1865)
#define PID_MED_MACHINE                 (4608)
#define PID_WORKBENCH_PRIMITIVE         (571)

// Other
#define PID_WANTED_SIGN                 (3228)
#define PID_UNVISIBLE_BLOCK             (820)
#define PID_RESPAWN_POINT               (821)
#define PID_TRAP                        (822)
#define PID_MFC_MACHINE                 (3167)
#define PID_NUKA_VENDING_MACHINE        (2194)

#define PID_BED1                        (3854)
#define PID_BED2                        (3855)
#define PID_TENT                        (3856)
#define PID_MAP_TO_LOCATION             (3857)

#define PID_WAYPOINT                    (1851)
#define PID_ITEM_SPAWNING_HEX           (1867)
#define PID_LEAFLET_CADILLAC            (1866)

// Blocker
#define PID_BLOCK_BARREL1               (9670)
#define PID_BLOCK_BARREL2               (9671)
#define PID_BLOCK_BARREL3               (9672)


// Treasure Hunt
#define PID_TREASURE1                   (553)
#define PID_TREASURE2                   (554)
#define PID_TREASURE3                   (555)
#define PID_TREASURE4                   (556)
#define PID_TREASURE5                   (557)

#define PID_BRAHMIN_HIDE_TROPHY         (9932)

#define PID_WATERBAG                    (605)
#define PID_WATERPIPE                   (606)
#define PID_WATERBAG_EMPTY              (609)

// Quest items
#define PID_Q_CORS_BROS_DEAD_BODY       (9924)

/******************************************************************
***************       Effects                       ***************
******************************************************************/

//Explode
#define PID_EXPLODE_FIRE_BIG                (4027)
#define PID_EXPLODE_FIRE_SMALL              (4028)
#define PID_EXPLODE_PLASMA                  (4029)
#define PID_EXPLODE_EMP                     (4008)
#define PID_EXPLODE_ROCKET                  (4011)
//Flying
#define PID_FLYING_ROCKET                   (4001)
#define PID_FLYING_PLASMA_BALL              (4002)
#define PID_FLYING_KNIFE                    (4006)
#define PID_FLYING_SPEAR                    (4007)
#define PID_FLYING_LASER_BLAST              (4009)
#define PID_FLYING_PLASMA_BLAST             (4010)
#define PID_FLYING_ELECTRICITY_BOLT         (4013)

#define PID_TANKER_ROPE                     (9624)
#define PID_SHAFT_WITH_ROPE                 (9677)

#define PID_FORCEFIELD_YELLOW_1             (9678)
#define PID_FORCEFIELD_YELLOW_2             (9679)



// 
#define PID_TRIGGER                         (825) // Custom trigger item, see triggers.fos
#define PID_MHEX                            (826) // Spawned by multihex items
                                                  

// Special item pids (for engine)
#define PID_SPECIAL_SCEN_LIGHT                 (2141) // Light Source
#define PID_SPECIAL_SCEN_LIGHT_STOP            (4592)
#define PID_SPECIAL_SCEN_BLOCK                 (2067) // Secret Blocking Hex
#define PID_SPECIAL_SCEN_IBLOCK                (2344) // Block Hex Auto Inviso
#define PID_SPECIAL_SCEN_TRIGGER               (3852)
#define PID_SPECIAL_WALL_BLOCK_LIGHT           (5621) // Wall s.t. with light
#define PID_SPECIAL_WALL_BLOCK                 (5622) // Wall s.t.
#define PID_SPECIAL_GRID_EXITGRID              (2049) // Exit Grid Map Marker
#define PID_SPECIAL_GRID_ENTIRE                (3853)
#define PID_SPECIAL_MISC_SCRBLOCK              (4012) // Scroll block
#define PID_SPECIAL_MISC_GRID_MAP #(pid)        ((pid)>=4016&&(pid)<=4023)
#define PID_SPECIAL_MISC_GRID_GM #(pid)         ((pid)>=4031&&(pid)<=4046 )

/******************************************************************
***************       Movable Containers            ***************
******************************************************************/
#define PID_MOVABLE_FRIDGE                  (650)

#define PID_MOVABLE_AMMO_BOX_L1             (652)
#define PID_MOVABLE_AMMO_BOX_L2             (653)
#define PID_MOVABLE_AMMO_BOX_S1             (654)
#define PID_MOVABLE_AMMO_BOX_S2             (655)

#define PID_MOVABLE_SAFE                    (656)
#define PID_MOVABLE_POOR_BOX                (658)
#define PID_MOVABLE_CRATE                   (660)
#define PID_MOVABLE_CHEST                   (662)

#define PID_MOVABLE_FOOTLOCKER1             (664)
#define PID_MOVABLE_FOOTLOCKER2             (665)
#define PID_MOVABLE_LOCKER1                 (666)
#define PID_MOVABLE_LOCKER2                 (667)
#define PID_MOVABLE_WOODLOCKER1             (668)
#define PID_MOVABLE_WOODLOCKER2             (669)


/******************************************************************
***************              Spots (21)             ***************
******************************************************************/
#define PID_SPOT_GENERIC                    (21000)
#define PID_SPOT_MOB_WAVE                   (21001)
#define PID_SPOT_SHUFFLER                   (21002)

// 21100 - 21355 is reserved for entry spots (target hexes of transfers)
#define PID_SPOT_ENTRY_BEGIN                (21100)
#define PID_SPOT_ENTRY_0                    (21100)
#define PID_SPOT_ENTRY_1                    (21101)
#define PID_SPOT_ENTRY_2                    (21102)
#define PID_SPOT_ENTRY_3                    (21103)
#define PID_SPOT_ENTRY_4                    (21104)
#define PID_SPOT_ENTRY_5                    (21105)
#define PID_SPOT_ENTRY_6                    (21106)
#define PID_SPOT_ENTRY_7                    (21107)
#define PID_SPOT_ENTRY_8                    (21108)
#define PID_SPOT_ENTRY_9                    (21109)
#define PID_SPOT_ENTRY_10                   (21110)
#define PID_SPOT_ENTRY_11                   (21111)
#define PID_SPOT_ENTRY_12                   (21112)
#define PID_SPOT_ENTRY_13                   (21113)
#define PID_SPOT_ENTRY_14                   (21114)
#define PID_SPOT_ENTRY_15                   (21115)
#define PID_SPOT_ENTRY_END                  (21355)

/******************************************************************
***************            Map Objects (22)         ***************
******************************************************************/
#define PID_VEIN_IRON                       (22000)
#define PID_VEIN_IRON2                      (22001)
#define PID_VEIN_MINERALS                   (22002)
#define PID_VEIN_MINERALS2                  (22003)
#define PID_VEIN_URANIUM                    (22004)
#define PID_VEIN_GOLD                       (22005)
#define PID_PILE_CHEMICALS                  (22006)
#define PID_PILE_CHEMICALS2                 (22007)

/******************************************************************
***************         Transfer Objects (23)       ***************
******************************************************************/


/******************************************************************
***************              Triggers (24)          ***************
******************************************************************/



/*
PID_BONES
PID_MEMO
PID_MAP
*/

/*
#define PID_FLYING_ROCKET                    //(83886081)1
#define PID_FLYING_PLASMA_BALL               //(83886082)2
#define PID_FLYING_KNIFE                     //(83886086)6
#define PID_FLYING_SPEAR                     //(83886087)7
#define PID_FLYING_LASER_BLAST               //(83886089)9
#define PID_FLYING_PLASMA_BLAST              //(83886090)A
#define PID_FLYING_ELECTRICITY_BOLT          //(83886091)B
*/

#endif  // _ITEMPID_H_DEFINED
