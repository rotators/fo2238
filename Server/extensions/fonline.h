#ifndef __FONLINE__
#define __FONLINE__

//
// FOnline engine structures, for native working
// Last update 20.10.2012
// Server version 504, MSVS, GCC
// Default calling convention - cdecl
//

// Detect operating system
#if defined ( _WIN32 ) || defined ( _WIN64 )
# define FO_WINDOWS
#elif defined ( __linux__ )
# define FO_LINUX
#else
# error "Unknown operating system."
#endif

// Detect compiler
#if defined ( __GNUC__ )
# define FO_GCC
#elif defined ( _MSC_VER ) && !defined ( __MWERKS__ )
# define FO_MSVC
#else
# error "Unknown compiler."
#endif

// Detect CPU
#if ( defined ( FO_MSVC ) && defined ( _M_IX86 ) ) || ( defined ( FO_GCC ) && !defined ( __LP64__ ) )
# define FO_X86
#elif ( defined ( FO_MSVC ) && defined ( _M_X64 ) ) || ( defined ( FO_GCC ) && defined ( __LP64__ ) )
# define FO_X64
#else
# error "Unknown CPU."
#endif

// Detect target
#if defined ( __SERVER )
# define TARGET_NAME                SERVER
#elif defined ( __CLIENT )
# define TARGET_NAME                CLIENT
#elif defined ( __MAPPER )
# define TARGET_NAME                MAPPER
#else
# error __SERVER / __CLIENT / __MAPPER any of this must be defined
#endif

// Platform specific options
#define NDEBUG
#ifdef FO_MSVC
# define _WINDOWS
# define _MBCS
# define _CRT_SECURE_NO_WARNINGS
# define _CRT_SECURE_NO_DEPRECATE
# define _HAS_ITERATOR_DEBUGGING    0
# define _SECURE_SCL                0
# define _HAS_EXCEPTIONS            0
#endif

#ifdef FO_WINDOWS
# ifdef FO_MSVC
#  define EXPORT                    extern "C" __declspec( dllexport )
#  define EXPORT_UNINITIALIZED      extern "C" __declspec( dllexport ) extern
# else // FO_GCC
#  define EXPORT                    extern "C" __attribute__( ( dllexport ) )
#  define EXPORT_UNINITIALIZED      extern "C" __attribute__( ( dllexport ) ) extern
# endif
#else
# define EXPORT                     extern "C" __attribute__( ( visibility( "default" ) ) )
# define EXPORT_UNINITIALIZED       extern "C" __attribute__( ( visibility( "default" ) ) )
#endif

// STL
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <string>
#include <vector>
#include <set>
#include <map>
using namespace std;

// AngelScript
#include "AngelScript/angelscript.h"
EXPORT_UNINITIALIZED asIScriptEngine* ASEngine;

// AngelScript add-ons
#define FONLINE_DLL
#include "AngelScript/scriptstring.h"
#include "AngelScript/scriptarray.h"
#include "AngelScript/scriptfile.h"
#include "AngelScript/scriptdictionary.h"
#include "AngelScript/scriptany.h"
#include "AngelScript/scriptmath.h"
#undef FONLINE_DLL

// FOnline types
struct GameOptions;
struct Mutex;
struct Spinlock;
struct SyncObj;
struct CritterType;
struct ProtoItem;
struct GameVar;
struct TemplateVar;
struct NpcPlane;
struct GlobalMapGroup;
struct Item;
struct CritterTimeEvent;
struct Critter;
struct Client;
struct Npc;
struct CritterCl;
struct MapObject;
struct MapEntire;
struct SceneryToClient;
struct ProtoMap;
struct Map;
struct ProtoLocation;
struct Location;
struct Field;
struct SpriteInfo;
struct Sprite;

#ifdef __SERVER
typedef Critter                               CritterMutual;
#else
typedef CritterCl                             CritterMutual;
#endif

typedef char                                  int8;
typedef unsigned char                         uint8;
typedef short                                 int16;
typedef unsigned short                        uint16;
typedef unsigned int                          uint;
#if defined ( FO_MSVC )
typedef unsigned __int64                      uint64;
typedef __int64                               int64;
#elif defined ( FO_GCC )
# include <inttypes.h>
typedef uint64_t                              uint64;
typedef int64_t                               int64;
#endif

typedef pair< int, int >                      IntPair;
typedef pair< uint, uint >                    UintPair;
typedef pair< uint16, uint16 >                Uint16Pair;

typedef vector< uint >                        UintVec;
typedef vector< uint >::const_iterator        UintVecIt;
typedef vector< uint16 >                      Uint16Vec;
typedef vector< uint16 >::const_iterator      Uint16VecIt;
typedef vector< int >                         IntVec;
typedef vector< int >::const_iterator         IntVecIt;
typedef vector< UintPair >                    UintPairVec;
typedef vector< Uint16Pair >                  Uint16PairVec;
typedef set< int >                            IntSet;
typedef set< int >::const_iterator            IntSetIt;
typedef set< uint >                           UintSet;
typedef set< uint >::const_iterator           UintSetIt;
typedef map< int, int >                       IntMap;
typedef set< int, int >::const_iterator       IntMapIt;

typedef vector< NpcPlane* >                   NpcPlaneVec;
typedef vector< NpcPlane* >::const_iterator   NpcPlaneVecIt;
typedef vector< Critter* >                    CrVec;
typedef vector< Critter* >::const_iterator    CrVecIt;
typedef map< uint, Critter* >                 CrMap;
typedef map< uint, Critter* >::const_iterator CrMapIt;
typedef vector< CritterCl* >                  CrClVec;
typedef vector< CritterCl* >::const_iterator  CrClVecIt;
typedef vector< Client* >                     ClVec;
typedef vector< Client* >::const_iterator     ClVecIt;
typedef vector< Npc* >                        PcVec;
typedef vector< Npc* >::const_iterator        PcVecIt;
typedef vector< Item* >                       ItemVec;
typedef vector< Item* >::const_iterator       ItemVecIt;
typedef vector< MapObject* >                  MapObjectVec;
typedef vector< MapObject* >::const_iterator  MapObjectVecIt;
typedef vector< Map* >                        MapVec;
typedef vector< Map* >::const_iterator        MapVecIt;
typedef vector< Location* >                   LocVec;
typedef vector< Location* >::const_iterator   LocVecIt;

// Generic
EXPORT_UNINITIALIZED void              ( * Log )( const char* frmt, ... );
EXPORT_UNINITIALIZED asIScriptContext* ( *ScriptGetActiveContext )( );
EXPORT_UNINITIALIZED const char* (ScriptGetLibraryOptions) ( );
EXPORT_UNINITIALIZED const char* (ScriptGetLibraryVersion) ( );

#define FONLINE_DLL_ENTRY( isCompiler )               \
    GameOptions * FOnline;                            \
    asIScriptEngine* ASEngine;                        \
    EXPORT void TARGET_NAME() {}                      \
    void ( * Log )( const char* frmt, ... );          \
    asIScriptContext* ( *ScriptGetActiveContext )( ); \
    const char* (ScriptGetLibraryOptions) ( );        \
    const char* (ScriptGetLibraryVersion) ( );        \
    EXPORT void DllMainEx( bool isCompiler )
// FONLINE_DLL_ENTRY

#define STATIC_ASSERT( a )               { static int arr[ ( a ) ? 1 : -1 ]; }
#define BIN__N( x )                      ( x ) | x >> 3 | x >> 6 | x >> 9
#define BIN__B( x )                      ( x ) & 0xf | ( x ) >> 12 & 0xf0
#define BIN8( v )                        ( BIN__B( BIN__N( 0x ## v ) ) )
#define BIN16( bin16, bin8 )             ( ( BIN8( bin16 ) << 8 ) | ( BIN8( bin8 ) ) )

#define FLAG( x, y )                     ( ( ( x ) & ( y ) ) != 0 )
#define CLAMP( x, low, high )            ( ( ( x ) > ( high ) ) ? ( high ) : ( ( ( x ) < ( low ) ) ? ( low ) : ( x ) ) )
#define SQRT3T2_FLOAT                ( 3.4641016151f )
#define SQRT3_FLOAT                  ( 1.732050807568877f )
#define RAD2DEG                      ( 57.29577951f )
#define CONVERT_GRAMM( x )               ( ( x ) * 453 )

#define LEXEMS_SIZE                  ( 128 )
#define MAX_HOLO_INFO                ( 250 )
#define SCORES_MAX                   ( 50 )
#define MAX_NPC_BAGS_PACKS           ( 20 )
#define MAX_ENEMY_STACK              ( 30 )
#define MAX_NPC_BAGS                 ( 50 )
#define MAX_STORED_LOCATIONS         ( 1000 )
#define GM_ZONES_FOG_SIZE            ( 2500 )
#define MAX_SCRIPT_NAME              ( 64 )
#define MAPOBJ_SCRIPT_NAME           ( 25 )
#define MAPOBJ_CRITTER_PARAMS        ( 40 )
#define MAX_PARAMETERS_ARRAYS        ( 100 )
#define MAX_NAME                     ( 30 )
#define PASS_HASH_SIZE               ( 32 )
#define MAX_STORED_IP                ( 20 )
#define MAX_HEX_OFFSET               ( 50 )
#define AP_DIVIDER                   ( 100 )
#define MAX_CRIT_TYPES               ( 1000 )
#define EFFECT_TEXTURES              ( 10 )
#define EFFECT_SCRIPT_VALUES         ( 10 )
#define CRITTER_USER_DATA_SIZE       ( 400 )

// Vars
#define VAR_CALC_QUEST( tid, val )       ( ( tid ) * 1000 + ( val ) )
#define VAR_GLOBAL                   ( 0 )
#define VAR_LOCAL                    ( 1 )
#define VAR_UNICUM                   ( 2 )
#define VAR_LOCAL_LOCATION           ( 3 )
#define VAR_LOCAL_MAP                ( 4 )
#define VAR_LOCAL_ITEM               ( 5 )
#define VAR_FLAG_QUEST               ( 0x1 )
#define VAR_FLAG_RANDOM              ( 0x2 )
#define VAR_FLAG_NO_CHECK            ( 0x4 )

// Items
#define PROTO_ITEM_USER_DATA_SIZE    ( 500 )
#define ITEM_MAX_BLOCK_LINES         ( 50 )
#define ITEM_MAX_CHILDS              ( 5 )
#define ITEM_MAX_CHILD_LINES         ( 6 )
#define ITEM_MAX_SCRIPT_VALUES       ( 10 )
#define USE_PRIMARY                  ( 0 )
#define USE_SECONDARY                ( 1 )
#define USE_THIRD                    ( 2 )
#define USE_RELOAD                   ( 3 )
#define USE_USE                      ( 4 )
#define MAX_USES                     ( 3 )
#define USE_NONE                     ( 15 )

// Parameters
#define MAX_PARAMS                   ( 1000 )
#define SKILL_OFFSET( skill )            ( ( skill ) + ( FOnline->AbsoluteOffsets ? 0 : SKILL_BEGIN ) )
#define PERK_OFFSET( perk )              ( ( perk )  + ( FOnline->AbsoluteOffsets ? 0 : PERK_BEGIN ) )
#define TB_BATTLE_TIMEOUT            ( 100000000 )
#define TB_BATTLE_TIMEOUT_CHECK( to )    ( ( to ) > 10000000 )
#define SKILL_BEGIN                  ( FOnline->SkillBegin )
#define SKILL_END                    ( FOnline->SkillEnd )
#define TIMEOUT_BEGIN                ( FOnline->TimeoutBegin )
#define TIMEOUT_END                  ( FOnline->TimeoutEnd )
#define KILL_BEGIN                   ( FOnline->KillBegin )
#define KILL_END                     ( FOnline->KillEnd )
#define PERK_BEGIN                   ( FOnline->PerkBegin )
#define PERK_END                     ( FOnline->PerkEnd )
#define ADDICTION_BEGIN              ( FOnline->AddictionBegin )
#define ADDICTION_END                ( FOnline->AddictionEnd )
#define KARMA_BEGIN                  ( FOnline->KarmaBegin )
#define KARMA_END                    ( FOnline->KarmaEnd )
#define DAMAGE_BEGIN                 ( FOnline->DamageBegin )
#define DAMAGE_END                   ( FOnline->DamageEnd )
#define TRAIT_BEGIN                  ( FOnline->TraitBegin )
#define TRAIT_END                    ( FOnline->TraitEnd )
#define REPUTATION_BEGIN             ( FOnline->ReputationBegin )
#define REPUTATION_END               ( FOnline->ReputationEnd )

// Events
#define MAP_LOOP_FUNC_MAX            ( 5 )
#define MAP_MAX_DATA                 ( 100 )

// Sprites cutting
#define SPRITE_CUT_HORIZONTAL        ( 1 )
#define SPRITE_CUT_VERTICAL          ( 2 )

// Map blocks
#define FH_BLOCK                     BIN8( 00000001 )
#define FH_NOTRAKE                   BIN8( 00000010 )
#define FH_WALL                      BIN8( 00000100 )
#define FH_SCEN                      BIN8( 00001000 )
#define FH_SCEN_GRID                 BIN8( 00010000 )
#define FH_TRIGGER                   BIN8( 00100000 )
#define FH_CRITTER                   BIN8( 00000001 )
#define FH_DEAD_CRITTER              BIN8( 00000010 )
#define FH_ITEM                      BIN8( 00000100 )
#define FH_BLOCK_ITEM                BIN8( 00010000 )
#define FH_NRAKE_ITEM                BIN8( 00100000 )
#define FH_WALK_ITEM                 BIN8( 01000000 )
#define FH_GAG_ITEM                  BIN8( 10000000 )
#define FH_NOWAY                     BIN16( 00010001, 00000001 )
#define FH_NOSHOOT                   BIN16( 00100000, 00000010 )

// GameOptions::ChangeLang
#define CHANGE_LANG_CTRL_SHIFT       ( 0 )
#define CHANGE_LANG_ALT_SHIFT        ( 1 )
// GameOptions::IndicatorType
#define INDICATOR_LINES              ( 0 )
#define INDICATOR_NUMBERS            ( 1 )
#define INDICATOR_BOTH               ( 2 )
// GameOptions::Zoom
#define MIN_ZOOM                     ( 0.2f )
#define MAX_ZOOM                     ( 10.0f )

struct GameOptions
{
    const uint16       YearStart;
    const uint         YearStartFTLo;
    const uint         YearStartFTHi;
    const uint16       Year;
    const uint16       Month;
    const uint16       Day;
    const uint16       Hour;
    const uint16       Minute;
    const uint16       Second;
    const uint         FullSecondStart;
    const uint         FullSecond;
    const uint16       TimeMultiplier;
    const uint         GameTimeTick;

    const bool         DisableTcpNagle;
    const bool         DisableZlibCompression;
    const uint         FloodSize;
    const bool         NoAnswerShuffle;
    const bool         DialogDemandRecheck;
    const uint         FixBoyDefaultExperience;
    const uint         SneakDivider;
    const uint         LevelCap;
    const bool         LevelCapAddExperience;
    const uint         LookNormal;
    const uint         LookMinimum;
    const uint         GlobalMapMaxGroupCount;
    const uint         CritterIdleTick;
    const uint         TurnBasedTick;
    const int          DeadHitPoints;
    const uint         Breaktime;
    const uint         TimeoutTransfer;
    const uint         TimeoutBattle;
    const uint         ApRegeneration;
    const uint         RtApCostCritterWalk;
    const uint         RtApCostCritterRun;
    const uint         RtApCostMoveItemContainer;
    const uint         RtApCostMoveItemInventory;
    const uint         RtApCostPickItem;
    const uint         RtApCostDropItem;
    const uint         RtApCostReloadWeapon;
    const uint         RtApCostPickCritter;
    const uint         RtApCostUseItem;
    const uint         RtApCostUseSkill;
    const bool         RtAlwaysRun;
    const uint         TbApCostCritterMove;
    const uint         TbApCostMoveItemContainer;
    const uint         TbApCostMoveItemInventory;
    const uint         TbApCostPickItem;
    const uint         TbApCostDropItem;
    const uint         TbApCostReloadWeapon;
    const uint         TbApCostPickCritter;
    const uint         TbApCostUseItem;
    const uint         TbApCostUseSkill;
    const bool         TbAlwaysRun;
    const uint         ApCostAimEyes;
    const uint         ApCostAimHead;
    const uint         ApCostAimGroin;
    const uint         ApCostAimTorso;
    const uint         ApCostAimArms;
    const uint         ApCostAimLegs;
    const bool         RunOnCombat;
    const bool         RunOnTransfer;
    const uint         GlobalMapWidth;
    const uint         GlobalMapHeight;
    const uint         GlobalMapZoneLength;
    const uint         GlobalMapMoveTime;
    const uint         BagRefreshTime;
    const uint         AttackAnimationsMinDist;
    const uint         WhisperDist;
    const uint         ShoutDist;
    const int          LookChecks;
    const uint         LookDir[ 5 ];
    const uint         LookSneakDir[ 5 ];
    const uint         LookWeight;
    const bool         CustomItemCost;
    const uint         RegistrationTimeout;
    const uint         AccountPlayTime;
    const bool         LoggingVars;
    const uint         ScriptRunSuspendTimeout;
    const uint         ScriptRunMessageTimeout;
    const uint         TalkDistance;
    const uint         NpcMaxTalkers;
    const uint         MinNameLength;
    const uint         MaxNameLength;
    const uint         DlgTalkMinTime;
    const uint         DlgBarterMinTime;
    const uint         MinimumOfflineTime;

    const int          StartSpecialPoints;
    const int          StartTagSkillPoints;
    const int          SkillMaxValue;
    const int          SkillModAdd2;
    const int          SkillModAdd3;
    const int          SkillModAdd4;
    const int          SkillModAdd5;
    const int          SkillModAdd6;

    const bool         AbsoluteOffsets;
    const uint         SkillBegin;
    const uint         SkillEnd;
    const uint         TimeoutBegin;
    const uint         TimeoutEnd;
    const uint         KillBegin;
    const uint         KillEnd;
    const uint         PerkBegin;
    const uint         PerkEnd;
    const uint         AddictionBegin;
    const uint         AddictionEnd;
    const uint         KarmaBegin;
    const uint         KarmaEnd;
    const uint         DamageBegin;
    const uint         DamageEnd;
    const uint         TraitBegin;
    const uint         TraitEnd;
    const uint         ReputationBegin;
    const uint         ReputationEnd;

    const int          ReputationLoved;
    const int          ReputationLiked;
    const int          ReputationAccepted;
    const int          ReputationNeutral;
    const int          ReputationAntipathy;
    const int          ReputationHated;

    const bool         MapHexagonal;
    const int          MapHexWidth;
    const int          MapHexHeight;
    const int          MapHexLineHeight;
    const int          MapTileOffsX;
    const int          MapTileOffsY;
    const int          MapRoofOffsX;
    const int          MapRoofOffsY;
    const int          MapRoofSkipSize;
    const float        MapCameraAngle;
    const bool         MapSmoothPath;
    const ScriptString MapDataPrefix;

    // Client and Mapper
    const bool         Quit;
    const bool         OpenGLDebug;
    const bool         AssimpLogging;
    const int          MouseX;
    const int          MouseY;
    const int          ScrOx;
    const int          ScrOy;
    const bool         ShowTile;
    const bool         ShowRoof;
    const bool         ShowItem;
    const bool         ShowScen;
    const bool         ShowWall;
    const bool         ShowCrit;
    const bool         ShowFast;
    const bool         ShowPlayerNames;
    const bool         ShowNpcNames;
    const bool         ShowCritId;
    const bool         ScrollKeybLeft;
    const bool         ScrollKeybRight;
    const bool         ScrollKeybUp;
    const bool         ScrollKeybDown;
    const bool         ScrollMouseLeft;
    const bool         ScrollMouseRight;
    const bool         ScrollMouseUp;
    const bool         ScrollMouseDown;
    const bool         ShowGroups;
    const bool         HelpInfo;
    const bool         DebugInfo;
    const bool         DebugNet;
    const bool         DebugSprites;
    const bool         FullScreen;
    const bool         VSync;
    const int          FlushVal;
    const int          BaseTexture;
    const int          Light;
    const ScriptString Host;
    const uint         Port;
    const uint         ProxyType;
    const ScriptString ProxyHost;
    const uint         ProxyPort;
    const ScriptString ProxyUser;
    const ScriptString ProxyPass;
    const ScriptString Name;
    const int          ScrollDelay;
    const uint         ScrollStep;
    const bool         ScrollCheck;
    const ScriptString FoDataPath;
    const int          FixedFPS;
    const bool         MsgboxInvert;
    const int          ChangeLang;
    const uint8        DefaultCombatMode;
    const bool         MessNotify;
    const bool         SoundNotify;
    const bool         AlwaysOnTop;
    const uint         TextDelay;
    const uint         DamageHitDelay;
    const int          ScreenWidth;
    const int          ScreenHeight;
    const int          MultiSampling;
    const bool         MouseScroll;
    const int          IndicatorType;
    const uint         DoubleClickTime;
    const uint8        RoofAlpha;
    const bool         HideCursor;
    const bool         DisableLMenu;
    const bool         DisableMouseEvents;
    const bool         DisableKeyboardEvents;
    const bool         HidePassword;
    const ScriptString PlayerOffAppendix;
    const int          CombatMessagesType;
    const bool         DisableDrawScreens;
    const uint         Animation3dSmoothTime;
    const uint         Animation3dFPS;
    const int          RunModMul;
    const int          RunModDiv;
    const int          RunModAdd;
    const bool         MapZooming;
    const float        SpritesZoom;
    const float        SpritesZoomMax;
    const float        SpritesZoomMin;
    const float        EffectValues[ EFFECT_SCRIPT_VALUES ];
    const bool         AlwaysRun;
    const int          AlwaysRunMoveDist;
    const int          AlwaysRunUseDist;
    const ScriptString KeyboardRemap;
    const uint         CritterFidgetTime;
    const uint         Anim2CombatBegin;
    const uint         Anim2CombatIdle;
    const uint         Anim2CombatEnd;
    const uint         RainTick;
    const int16        RainSpeedX;
    const int16        RainSpeedY;

    // Mapper
    const ScriptString ClientPath;
    const ScriptString ServerPath;
    const bool         ShowCorners;
    const bool         ShowCuttedSprites;
    const bool         ShowDrawOrder;
    const bool         SplitTilesCollection;

    // Engine data
    void               ( * CritterChangeParameter )( Critter& cr, uint index );                            // Call for correct changing critter parameter
    CritterType*       CritterTypes;                                                                       // Array of critter types, maximum is MAX_CRIT_TYPES

    Field*             ClientMap;                                                                          // Array of client map hexes, accessing - ClientMap[ hexY * ClientMapWidth + hexX ]
    uint8*             ClientMapLight;                                                                     // Hex light, accessing - ClientMapLight[ hexY * ClientMapWidth * 3 + hexX * 3 {+ 0(R), 1(G), 2(B)} ]
    uint               ClientMapWidth;                                                                     // Map width
    uint               ClientMapHeight;                                                                    // Map height

    Sprite**           ( *GetDrawingSprites )( uint & count );                                             // Array of currently drawing sprites, tree is sorted
    SpriteInfo*        ( *GetSpriteInfo )(uint sprId);                                                     // Sprite information
    uint               ( * GetSpriteColor )( uint sprId, int x, int y, bool affectZoom );                  // Color of pixel on sprite
    bool               ( * IsSpriteHit )( Sprite* sprite, int x, int y, bool checkEgg );                   // Is position hitting sprite

    const char*        ( *GetNameByHash )(uint hash);                                                      // Get name of file by hash
    uint               ( * GetHashByName )( const char* name );                                            // Get hash of file name

    bool               ( * ScriptLoadModule )( const char* moduleName );
    uint               ( * ScriptBind )( const char* moduleName, const char* funcDecl, bool temporaryId ); // Returning bindId
    bool               ( * ScriptPrepare )( uint bindId );
    void               ( * ScriptSetArgInt8 )( int8 value );
    void               ( * ScriptSetArgInt16 )( int16 value );
    void               ( * ScriptSetArgInt )( int value );
    void               ( * ScriptSetArgInt64 )( int64 value );
    void               ( * ScriptSetArgUInt8 )( uint8 value );
    void               ( * ScriptSetArgUInt16 )( uint16 value );
    void               ( * ScriptSetArgUInt )( uint value );
    void               ( * ScriptSetArgUInt64 )( uint64 value );
    void               ( * ScriptSetArgBool )( bool value );
    void               ( * ScriptSetArgFloat )( float value );
    void               ( * ScriptSetArgDouble )( double value );
    void               ( * ScriptSetArgObject )( void* value );
    void               ( * ScriptSetArgAddress )( void* value );
    bool               ( * ScriptRunPrepared )();
    int8               ( * ScriptGetReturnedInt8 )();
    int16              ( * ScriptGetReturnedInt16 )();
    int                ( * ScriptGetReturnedInt )();
    int64              ( * ScriptGetReturnedInt64 )();
    uint8              ( * ScriptGetReturnedUInt8 )();
    uint16             ( * ScriptGetReturnedUInt16 )();
    uint               ( * ScriptGetReturnedUInt )();
    uint64             ( * ScriptGetReturnedUInt64 )();
    bool               ( * ScriptGetReturnedBool )();
    float              ( * ScriptGetReturnedFloat )();
    double             ( * ScriptGetReturnedDouble )();
    void*              ( *ScriptGetReturnedObject )( );
    void*              ( *ScriptGetReturnedAddress )( );

    int                ( * Random )( int minimum, int maximumInclusive );
    uint               ( * GetTick )();

    // Callbacks
    uint               ( * GetUseApCost )( CritterMutual& cr, Item& item, uint8 mode );
    uint               ( * GetAttackDistantion )( CritterMutual& cr, Item& item, uint8 mode );
    void               ( * GetRainOffset )( int16* ox, int16* oy );
};
EXPORT_UNINITIALIZED GameOptions* FOnline;

struct Mutex
{
    const int Locker[ 6 ];      // CRITICAL_SECTION, include Windows.h
};

struct Spinlock
{
    const long Locker;
};

struct SyncObj
{
    const void* CurMngr;
};

struct CritterType
{
    const bool Enabled;
    const char Name[ 64 ];
    const char SoundName[ 64 ];
    const uint Alias;
    const uint Multihex;
    const int  AnimType;

    const bool CanWalk;
    const bool CanRun;
    const bool CanAim;
    const bool CanArmor;
    const bool CanRotate;

    const bool Anim1[ 37 ];   // A..Z 0..9
};

struct ProtoItem
{
    // Internal data
    const uint16 ProtoId;
    const int    Type;
    const uint   PicMap;
    const uint   PicInv;
    const uint   Flags;
    const bool   Stackable;
    const bool   Deteriorable;
    const bool   GroundLevel;
    const int    Corner;
    const int    Dir;
    const uint8  Slot;
    const uint   Weight;
    const uint   Volume;
    const uint   Cost;
    const uint   StartCount;
    const uint8  SoundId;
    const uint8  Material;
    const uint8  LightFlags;
    const uint8  LightDistance;
    const int8   LightIntensity;
    const uint   LightColor;
    const bool   DisableEgg;
    const uint16 AnimWaitBase;
    const uint16 AnimWaitRndMin;
    const uint16 AnimWaitRndMax;
    const uint8  AnimStay[ 2 ];
    const uint8  AnimShow[ 2 ];
    const uint8  AnimHide[ 2 ];
    const int16  OffsetX;
    const int16  OffsetY;
    const uint8  SpriteCut;
    const int8   DrawOrderOffsetHexY;
    const uint16 RadioChannel;
    const uint16 RadioFlags;
    const uint8  RadioBroadcastSend;
    const uint8  RadioBroadcastRecv;
    const uint8  IndicatorStart;
    const uint8  IndicatorMax;
    const uint   HolodiskNum;
    const int    StartValue[ ITEM_MAX_SCRIPT_VALUES ];
    const uint8  BlockLines[ ITEM_MAX_BLOCK_LINES ];
    const uint16 ChildPids[ ITEM_MAX_CHILDS ];
    const uint8  ChildLines[ ITEM_MAX_CHILDS ][ ITEM_MAX_CHILD_LINES ];

    // User data, binded with 'bindfield' pragma
    // Common
    const int    MagicPower;
    const uint8  Unused[ 96 ];
    // Armor, offset 100
    const uint   Armor_CrTypeMale;
    const uint   Armor_CrTypeFemale;
    const int    Armor_AC;
    const uint   Armor_Perk;
    const int    Armor_DRNormal;
    const int    Armor_DRLaser;
    const int    Armor_DRFire;
    const int    Armor_DRPlasma;
    const int    Armor_DRElectr;
    const int    Armor_DREmp;
    const int    Armor_DRExplode;
    const int    Armor_DTNormal;
    const int    Armor_DTLaser;
    const int    Armor_DTFire;
    const int    Armor_DTPlasma;
    const int    Armor_DTElectr;
    const int    Armor_DTEmp;
    const int    Armor_DTExplode;
    const uint8  Armor_Unused[ 28 ];
    // Weapon, offset 200
    const int    Weapon_DmgType[ 3 ];
    const uint   Weapon_Anim2[ 3 ];
    const int    Weapon_DmgMin[ 3 ];
    const int    Weapon_DmgMax[ 3 ];
    const uint16 Weapon_Effect[ 3 ];
    const bool   Weapon_Remove[ 3 ];
    const uint   Weapon_ReloadAp;
    const int    Weapon_UnarmedCriticalBonus;
    const uint   Weapon_CriticalFailture;
    const bool   Weapon_UnarmedArmorPiercing;
    const uint8  Weapon_Unused[ 27 ];
    // Ammo, offset 300
    const int    Ammo_AcMod;
    const int    Ammo_DrMod;
    const uint   Ammo_DmgMult;
    const uint   Ammo_DmgDiv;
    // Other
    const uint8  UnusedEnd[ 184 ];

    // Type specific data
    const bool   Weapon_IsUnarmed;
    const int    Weapon_UnarmedTree;
    const int    Weapon_UnarmedPriority;
    const int    Weapon_UnarmedMinAgility;
    const int    Weapon_UnarmedMinUnarmed;
    const int    Weapon_UnarmedMinLevel;
    const uint   Weapon_Anim1;
    const uint   Weapon_MaxAmmoCount;
    const int    Weapon_Caliber;
    const uint16 Weapon_DefaultAmmoPid;
    const int    Weapon_MinStrength;
    const int    Weapon_Perk;
    const uint   Weapon_ActiveUses;
    const int    Weapon_Skill[ MAX_USES ];
    const uint   Weapon_PicUse[ MAX_USES ];
    const uint   Weapon_MaxDist[ MAX_USES ];
    const uint   Weapon_Round[ MAX_USES ];
    const uint   Weapon_ApCost[ MAX_USES ];
    const bool   Weapon_Aim[ MAX_USES ];
    const uint8  Weapon_SoundId[ MAX_USES ];
    const int    Ammo_Caliber;
    const bool   Door_NoBlockMove;
    const bool   Door_NoBlockShoot;
    const bool   Door_NoBlockLight;
    const uint   Container_Volume;
    const bool   Container_CannotPickUp;
    const bool   Container_MagicHandsGrnd;
    const bool   Container_Changeble;
    const uint16 Locker_Condition;
    const int    Grid_Type;
    const uint   Car_Speed;
    const uint   Car_Passability;
    const uint   Car_DeteriorationRate;
    const uint   Car_CrittersCapacity;
    const uint   Car_TankVolume;
    const uint   Car_MaxDeterioration;
    const uint   Car_FuelConsumption;
    const uint   Car_Entrance;
    const uint   Car_MovementType;

    bool IsItem()      const { return Type != ITEM_TYPE_GENERIC && Type != ITEM_TYPE_WALL; }
    bool IsScen()      const { return Type == ITEM_TYPE_GENERIC; }
    bool IsWall()      const { return Type == ITEM_TYPE_WALL; }
    bool IsArmor()     const { return Type == ITEM_TYPE_ARMOR; }
    bool IsDrug()      const { return Type == ITEM_TYPE_DRUG; }
    bool IsWeapon()    const { return Type == ITEM_TYPE_WEAPON; }
    bool IsAmmo()      const { return Type == ITEM_TYPE_AMMO; }
    bool IsMisc()      const { return Type == ITEM_TYPE_MISC; }
    bool IsKey()       const { return Type == ITEM_TYPE_KEY; }
    bool IsContainer() const { return Type == ITEM_TYPE_CONTAINER; }
    bool IsDoor()      const { return Type == ITEM_TYPE_DOOR; }
    bool IsGrid()      const { return Type == ITEM_TYPE_GRID; }
    bool IsGeneric()   const { return Type == ITEM_TYPE_GENERIC; }
    bool IsCar()       const { return Type == ITEM_TYPE_CAR; }
    bool LockerIsChangeble() const
    {
        if( IsDoor() ) return true;
        if( IsContainer() ) return Container_Changeble;
        return false;
    }
    bool IsCanPickUp() { return FLAG( Flags, ITEM_CAN_PICKUP ); }
};

struct TemplateVar
{
    const int    Type;
    const uint16 TempId;
    const string Name;
    const string Desc;
    const int    StartVal;
    const int    MinVal;
    const int    MaxVal;
    const uint   Flags;

    bool IsQuest()     const { return FLAG( Flags, VAR_FLAG_QUEST ); }
    bool IsRandom()    const { return FLAG( Flags, VAR_FLAG_RANDOM ); }
    bool IsNoBorders() const { return FLAG( Flags, VAR_FLAG_NO_CHECK ); }
};

struct GameVar
{
    const uint         MasterId;
    const uint         SlaveId;
    const int          VarValue;
    const TemplateVar* VarTemplate;
    const uint         QuestVarIndex;
    const uint16       Type;
    const int16        RefCount;
    const SyncObj      Sync;

    int    GetValue()    const { return VarValue; }
    int    GetMin()      const { return VarTemplate->MinVal; }
    int    GetMax()      const { return VarTemplate->MaxVal; }
    bool   IsQuest()     const { return VarTemplate->IsQuest(); }
    uint   GetQuestStr() const { return VAR_CALC_QUEST( VarTemplate->TempId, VarValue ); }
    uint64 GetUid()      const { return ( ( (uint64) SlaveId ) << 32 ) | ( (uint64) MasterId ); }
};

struct NpcPlane
{
    const int       Type;
    const uint      Priority;
    const int       Identifier;
    const uint      IdentifierExt;
    const NpcPlane* ChildPlane;
    const bool      IsMove;

    union
    {
        struct
        {
            bool IsRun;
            uint WaitSecond;
            int  ScriptBindId;
        } const Misc;

        struct
        {
            bool   IsRun;
            uint   TargId;
            int    MinHp;
            bool   IsGag;
            uint16 GagHexX, GagHexY;
            uint16 LastHexX, LastHexY;
        } const Attack;

        struct
        {
            bool   IsRun;
            uint16 HexX;
            uint16 HexY;
            uint8  Dir;
            uint   Cut;
        } const Walk;

        struct
        {
            bool   IsRun;
            uint16 HexX;
            uint16 HexY;
            uint16 Pid;
            uint   UseItemId;
            bool   ToOpen;
        } const Pick;

        struct
        {
            uint Buffer[ 8 ];
        } const Buffer;
    };

    struct
    {
        const uint   PathNum;
        const uint   Iter;
        const bool   IsRun;
        const uint   TargId;
        const uint16 HexX;
        const uint16 HexY;
        const uint   Cut;
        const uint   Trace;
    } Move;

    const bool      Assigned;
    const int       RefCounter;

    const NpcPlane* GetCurPlane()           const { return ChildPlane ? ChildPlane->GetCurPlane() : this; }
    bool            IsSelfOrHas( int type ) const { return Type == type || ( ChildPlane ? ChildPlane->IsSelfOrHas( type ) : false ); }
    uint            GetChildIndex( NpcPlane* child ) const
    {
        uint index = 0;
        for( const NpcPlane* child_ = this; child_; index++ )
        {
            if( child_ == child ) break;
            else child_ = child_->ChildPlane;
        }
        return index;
    }
    uint GetChildsCount() const
    {
        uint            count = 0;
        const NpcPlane* child = ChildPlane;
        for( ; child; count++, child = child->ChildPlane ) ;
        return count;
    }
};

struct Item
{
    const uint       Id;
    const ProtoItem* Proto;
    const int        From;
    const uint8      Accessory;
    const bool       ViewPlaceOnMap;
    const int16      Reserved0;

    union
    {
        struct
        {
            uint   MapId;
            uint16 HexX;
            uint16 HexY;
        } const AccHex;

        struct
        {
            uint  Id;
            uint8 Slot;
        } const AccCritter;

        struct
        {
            uint ContainerId;
            uint StackId;
        } const AccContainer;

        const char AccBuffer[ 8 ];
    };

    struct _Data
    {
        const uint16 SortValue;
        const uint8  Info;
        const uint8  Indicator;
        const uint   PicMapHash;
        const uint   PicInvHash;
        const uint16 AnimWaitBase;
        const uint8  AnimStay[ 2 ];
        const uint8  AnimShow[ 2 ];
        const uint8  AnimHide[ 2 ];
        const uint   Flags;
        uint8  Rate;
        const int8   LightIntensity;
        const uint8  LightDistance;
        const uint8  LightFlags;
        const uint   LightColor;
        const uint16 ScriptId;
        const int16  TrapValue;
        const uint   Count;
        const uint   Cost;
        const int    ScriptValues[ ITEM_MAX_SCRIPT_VALUES ];
        const uint8  BrokenFlags;
        const uint8  BrokenCount;
        const uint16 Deterioration;
        const uint16 AmmoPid;
        const uint16 AmmoCount;
        const uint   LockerId;
        const uint16 LockerCondition;
        const uint16 LockerComplexity;
        const uint   HolodiskNumber;
        const uint16 RadioChannel;
        const uint16 RadioFlags;
        const uint8  RadioBroadcastSend;
        const uint8  RadioBroadcastRecv;
        const uint16 Charge;
        const int16  OffsetX;
        const int16  OffsetY;
        const int16  Dir;
        const char   Reserved[ 2 ];
    } Data;

    const int16        RefCounter;
    const bool         IsNotValid;

    #ifdef __SERVER
    const int          FuncId[ ITEM_EVENT_MAX ];
    const Critter*     ViewByCritter;
    const ItemVec*     ChildItems;
    const char*        Lexems;
    const SyncObj      Sync;
    #endif

    #ifdef __CLIENT
    const ScriptString Lexems;
    #endif

    uint   GetId()       const { return Id; }
    uint16 GetProtoId()  const { return Proto->ProtoId; }
    uint   GetInfo()     const { return Proto->ProtoId * 100 + Data.Info; }
    uint   GetPicMap()   const { return Data.PicMapHash ? Data.PicMapHash : Proto->PicMap; }
    uint   GetPicInv()   const { return Data.PicInvHash ? Data.PicInvHash : Proto->PicInv; }
    uint8  GetType()     const { return Proto->Type; }
    bool   IsStackable() const { return Proto->Stackable; }

    bool IsPassed()           const { return FLAG( Data.Flags, ITEM_NO_BLOCK ) && FLAG( Data.Flags, ITEM_SHOOT_THRU ); }
    bool IsRaked()            const { return FLAG( Data.Flags, ITEM_SHOOT_THRU ); }
    bool IsFlat()             const { return FLAG( Data.Flags, ITEM_FLAT ); }
    bool IsHidden()           const { return FLAG( Data.Flags, ITEM_HIDDEN ); }
    bool IsCanTalk()          const { return FLAG( Data.Flags, ITEM_CAN_TALK ); }
    bool IsCanUse()           const { return FLAG( Data.Flags, ITEM_CAN_USE ); }
    bool IsCanUseOnSmth()     const { return FLAG( Data.Flags, ITEM_CAN_USE_ON_SMTH ); }
    bool IsHasTimer()         const { return FLAG( Data.Flags, ITEM_HAS_TIMER ); }
    bool IsBadItem()          const { return FLAG( Data.Flags, ITEM_BAD_ITEM ); }
    bool IsTwoHands()         const { return FLAG( Data.Flags, ITEM_TWO_HANDS ); }
    bool IsBigGun()           const { return FLAG( Data.Flags, ITEM_BIG_GUN ); }
    bool IsNoHighlight()      const { return FLAG( Data.Flags, ITEM_NO_HIGHLIGHT ); }
    bool IsShowAnim()         const { return FLAG( Data.Flags, ITEM_SHOW_ANIM ); }
    bool IsShowAnimExt()      const { return FLAG( Data.Flags, ITEM_SHOW_ANIM_EXT ); }
    bool IsLightThru()        const { return FLAG( Data.Flags, ITEM_LIGHT_THRU ); }
    bool IsAlwaysView()       const { return FLAG( Data.Flags, ITEM_ALWAYS_VIEW ); }
    bool IsGeck()             const { return FLAG( Data.Flags, ITEM_GECK ); }
    bool IsNoLightInfluence() const { return FLAG( Data.Flags, ITEM_NO_LIGHT_INFLUENCE ); }
    bool IsNoLoot()           const { return FLAG( Data.Flags, ITEM_NO_LOOT ); }
    bool IsNoSteal()          const { return FLAG( Data.Flags, ITEM_NO_STEAL ); }
    bool IsCanPickUp()        const { return FLAG( Data.Flags, ITEM_CAN_PICKUP ); }

    bool IsDeteriorable()   const { return Proto->Deteriorable; }
    bool IsBroken()         const { return FLAG( Data.BrokenFlags, BI_BROKEN ); }
    bool IsNoResc()         const { return FLAG( Data.BrokenFlags, BI_NOTRESC ); }
    bool IsService()        const { return FLAG( Data.BrokenFlags, BI_SERVICE ); }
    bool IsServiceExt()     const { return FLAG( Data.BrokenFlags, BI_SERVICE_EXT ); }
    bool IsEternal()        const { return FLAG( Data.BrokenFlags, BI_ETERNAL ); }
    int  GetBrokenCount()   const { return Data.BrokenCount; }
    int  GetDeterioration() const { return Data.Deterioration; }
    int  GetDeteriorationProc() const
    {
        int val = GetDeterioration() * 100 / MAX_DETERIORATION;
        return CLAMP( val, 0, 100 );
    }

    uint GetCount()  const { return IsStackable() ? Data.Count : 1; }
    uint GetVolume() const { return GetCount() * Proto->Volume; }
    uint GetWeight() const { return GetCount() * Proto->Weight; }

    // Armor
    bool IsArmor() const { return GetType() == ITEM_TYPE_ARMOR; }

    // Weapon
    bool IsWeapon()                  const { return GetType() == ITEM_TYPE_WEAPON; }
    bool WeapIsEmpty()               const { return !Data.AmmoCount; }
    bool WeapIsFull()                const { return Data.AmmoCount >= Proto->Weapon_MaxAmmoCount; }
    uint WeapGetAmmoCount()          const { return Data.AmmoCount; }
    uint WeapGetAmmoPid()            const { return Data.AmmoPid; }
    uint WeapGetMaxAmmoCount()       const { return Proto->Weapon_MaxAmmoCount; }
    int  WeapGetAmmoCaliber()        const { return Proto->Weapon_Caliber; }
    int  WeapGetNeedStrength()       const { return Proto->Weapon_MinStrength; }
    bool WeapIsUseAviable( int use ) const { return use >= USE_PRIMARY && use <= USE_THIRD ? ( ( ( Proto->Weapon_ActiveUses >> use ) & 1 ) != 0 ) : false; }
    bool WeapIsCanAim( int use )     const { return use >= 0 && use < MAX_USES && Proto->Weapon_Aim[ use ]; }

    // Container
    bool IsContainer()          const { return Proto->IsContainer(); }
    bool ContIsCannotPickUp()   const { return Proto->Container_CannotPickUp; }
    bool ContIsMagicHandsGrnd() const { return Proto->Container_MagicHandsGrnd; }
    bool ContIsChangeble()      const { return Proto->Container_Changeble; }

    // Door
    bool IsDoor() const { return GetType() == ITEM_TYPE_DOOR; }

    // Locker
    bool IsHasLocker()       const { return IsDoor() || IsContainer(); }
    uint LockerDoorId()      const { return Data.LockerId; }
    bool LockerIsOpen()      const { return FLAG( Data.LockerCondition, LOCKER_ISOPEN ); }
    bool LockerIsClose()     const { return !LockerIsOpen(); }
    bool LockerIsChangeble() const { return Proto->LockerIsChangeble(); }
    int  LockerComplexity()  const { return Data.LockerComplexity; }

    // Ammo
    bool IsAmmo()         const { return Proto->IsAmmo(); }
    int  AmmoGetCaliber() const { return Proto->Ammo_Caliber; }

    // Key
    bool IsKey()     const { return Proto->IsKey(); }
    uint KeyDoorId() const { return Data.LockerId; }

    // Drug
    bool IsDrug() const { return Proto->IsDrug(); }

    // Misc
    bool IsMisc() const { return Proto->IsMisc(); }

    // Colorize
    bool  IsColorize() const { return FLAG( Data.Flags, ITEM_COLORIZE ); }
    uint  GetColor()   const { return ( Data.LightColor ? Data.LightColor : Proto->LightColor ) & 0xFFFFFF; }
    uint8 GetAlpha()   const { return ( Data.LightColor ? Data.LightColor : Proto->LightColor ) >> 24; }

    // Light
    bool IsLight()           const { return FLAG( Data.Flags, ITEM_LIGHT ); }
    int  LightGetIntensity() const { return Data.LightIntensity ? Data.LightIntensity : Proto->LightIntensity; }
    int  LightGetDistance()  const { return Data.LightDistance ? Data.LightDistance : Proto->LightDistance; }
    int  LightGetFlags()     const { return Data.LightFlags ? Data.LightFlags : Proto->LightFlags; }
    uint LightGetColor()     const { return ( Data.LightColor ? Data.LightColor : Proto->LightColor ) & 0xFFFFFF; }

    // Car
    bool IsCar() const { return Proto->IsCar(); }

    // Trap
    bool IsTrap()       const { return FLAG( Data.Flags, ITEM_TRAP ); }
    int  TrapGetValue() const { return Data.TrapValue; }
};

struct GlobalMapGroup
{
    const CrVec    Group;
    const Critter* Rule;
    const uint     CarId;
    const float    CurX, CurY;
    const float    ToX, ToY;
    const float    Speed;
    const bool     IsSetMove;
    const uint     TimeCanFollow;
    const bool     IsMultiply;
    const uint     ProcessLastTick;
    const uint     EncounterDescriptor;
    const uint     EncounterTick;
    const bool     EncounterForce;
};

struct CritterTimeEvent
{
    const uint FuncNum;
    const uint Rate;
    const uint NextTime;
    const int  Identifier;
};
typedef vector< CritterTimeEvent >                 CritterTimeEventVec;
typedef vector< CritterTimeEvent >::const_iterator CritterTimeEventVecIt;

struct Critter
{
    const uint   Id;
    const uint16 HexX;
    const uint16 HexY;
    const uint16 WorldX;
    const uint16 WorldY;
    const uint   BaseType;
    const uint8  Dir;
    const uint8  Cond;
    const uint8  ReservedCE;
    const uint8  Reserved0;
    const uint   ScriptId;
    const uint   ShowCritterDist1;
    const uint   ShowCritterDist2;
    const uint   ShowCritterDist3;
    const uint16 Reserved00;
    const int16  Multihex;
    const uint   GlobalGroupUid;
    const uint16 LastHexX;
    const uint16 LastHexY;
    const uint   Reserved1[ 4 ];
    const uint   MapId;
    const uint16 MapPid;
    const uint16 Reserved2;
    const int    Params[ MAX_PARAMS ];
    const uint   Anim1Life;
    const uint   Anim1Knockout;
    const uint   Anim1Dead;
    const uint   Anim2Life;
    const uint   Anim2Knockout;
    const uint   Anim2Dead;
    const uint   Anim2KnockoutEnd;
    const uint   Reserved3[ 3 ];
    const char   Lexems[ LEXEMS_SIZE ];
    const uint   Reserved4[ 8 ];
    const bool   ClientToDelete;
    const uint8  Reserved5;
    const uint16 Reserved6;
    const uint   Temp;
    const uint16 Reserved8;
    const uint16 HoloInfoCount;
    const uint   HoloInfo[ MAX_HOLO_INFO ];
    const uint   Reserved9[ 10 ];
    const int    Scores[ SCORES_MAX ];

    // Binded with pragma bindfield
    const uint   GlobalMapMoveCounter;
    const uint8  UserData[ CRITTER_USER_DATA_SIZE - sizeof( uint ) ];

    // Npc data
    const uint   HomeMap;
    const uint16 HomeX;
    const uint16 HomeY;
    const uint8  HomeDir;
    const uint8  Reserved11;
    const uint16 ProtoId;
    const uint   Reserved12;
    const uint   Reserved13;
    const uint   Reserved14;
    const uint   Reserved15;
    const bool   IsDataExt;
    const uint8  Reserved16;
    const uint16 Reserved17;
    const uint   Reserved18[ 8 ];
    const uint16 FavoriteItemPid[ 4 ];
    const uint   Reserved19[ 10 ];
    const uint   EnemyStackCount;
    const uint   EnemyStack[ MAX_ENEMY_STACK ];
    const uint   Reserved20[ 5 ];
    const uint8  BagCurrentSet[ MAX_NPC_BAGS_PACKS ];
    const int16  BagRefreshTime;
    const uint8  Reserved21;
    const uint8  BagSize;
    struct
    {
        const uint ItemPid;
        const uint MinCnt;
        const uint MaxCnt;
        const uint ItemSlot;
    } const Bag[ MAX_NPC_BAGS ];
    const uint Reserved22[ 100 ];

    // Ext data
    struct
    {
        const uint   Reserved23[ 10 ];
        const uint8  GlobalMapFog[ GM_ZONES_FOG_SIZE ];
        const uint16 Reserved24;
        const uint16 LocationsCount;
        const uint   LocationsId[ MAX_STORED_LOCATIONS ];
        const uint   Reserved25[ 40 ];
        const uint   PlayIp[ MAX_STORED_IP ];       // 0 - registration ip
        const uint16 PlayPort[ MAX_STORED_IP ];
        const uint   CurrentIp;
        const uint   Reserved26[ 29 ];
    }* const DataExt;

    const SyncObj      Sync;
    const bool         CritterIsNpc;
    const uint         Flags;
    const ScriptString NameStr;

    struct
    {
        const bool   IsAlloc;
        const uint8* Data;
        const uint   Width;
        const uint   Height;
        const uint   WidthB;
    } const GMapFog;

    const bool                IsRuning;
    const uint                PrevHexTick;
    const uint16              PrevHexX, PrevHexY;
    const int                 LockMapTransfers;
    const Critter*            ThisPtr[ MAX_PARAMETERS_ARRAYS ];
    const uint                AllowedToDownloadMap;
    const bool                ParamsIsChanged[ MAX_PARAMS ];
    const IntVec              ParamsChanged;
    const int                 ParamLocked;
    const CrVec               VisCr;
    const CrVec               VisCrSelf;
    const CrMap               VisCrMap;
    const CrMap               VisCrSelfMap;
    const UintSet             VisCr1, VisCr2, VisCr3;
    const UintSet             VisItem;
    const Spinlock            VisItemLocker;
    const uint                ViewMapId;
    const uint16              ViewMapPid, ViewMapLook, ViewMapHx, ViewMapHy;
    const uint8               ViewMapDir;
    const uint                ViewMapLocId, ViewMapLocEnt;

    const GlobalMapGroup*     GroupSelf;
    const GlobalMapGroup*     GroupMove;

    const ItemVec             InvItems;
    const Item*               DefItemSlotHand;
    const Item*               DefItemSlotArmor;
    const Item*               ItemSlotMain;
    const Item*               ItemSlotExt;
    const Item*               ItemSlotArmor;
    const int                 FuncId[ CRITTER_EVENT_MAX ];
    const uint                KnockoutAp;
    const uint                NextIntellectCachingTick;
    const uint16              IntellectCacheValue;
    const uint                LookCacheValue;
    const uint                StartBreakTime;
    const uint                BreakTime;
    const uint                WaitEndTick;
    const int                 DisableSend;
    const uint                AccessContainerId;
    const uint                ItemTransferCount;
    const uint                TryingGoHomeTick;

    const CritterTimeEventVec CrTimeEvents;

    const uint                GlobalIdleNextTick;
    const uint                ApRegenerationTick;
    const bool                IsNotValid;
    const int                 RefCounter;

    uint                      GetItemsWeight() const
    {
        uint res = 0;
        for( ItemVecIt it = InvItems.begin(), end = InvItems.end(); it != end; ++it )
        {
            const Item* item = *it;
            if( !item->IsHidden() ) res += item->GetWeight();
        }
        return res;
    }

    uint GetItemsVolume() const
    {
        uint res = 0;
        for( ItemVecIt it = InvItems.begin(), end = InvItems.end(); it != end; ++it )
        {
            const Item* item = *it;
            if( !item->IsHidden() ) res += item->GetVolume();
        }
        return res;
    }
};

struct Client: Critter
{
    const char  Name[ MAX_NAME + 1 ];
    const char  PassHash[ PASS_HASH_SIZE ];
    const uint8 Access;
    const uint  LanguageMsg;
};

struct Npc: Critter
{
    const uint        NextRefreshBagTick;
    const NpcPlaneVec AiPlanes;
    const uint        Reserved;
};

struct CritterCl
{
    const uint          Id;
    const uint16        Pid;
    const uint16        HexX, HexY;
    const uint8         Dir;
    const int           Params[ MAX_PARAMS ];
    const uint          NameColor;
    const uint          ContourColor;
    const Uint16Vec     LastHexX, LastHexY;
    const uint8         Cond;
    const uint          Anim1Life;
    const uint          Anim1Knockout;
    const uint          Anim1Dead;
    const uint          Anim2Life;
    const uint          Anim2Knockout;
    const uint          Anim2Dead;
    const uint          Flags;
    const uint          BaseType, BaseTypeAlias;
    const uint          ApRegenerationTick;
    const int16         Multihex;

    const ScriptString  Name;
    const ScriptString  NameOnHead;
    const ScriptString  Lexems;
    const ScriptString  Avatar;
    const char          PasswordReg[ MAX_NAME + 1 ];

    const ItemVec       InvItems;
    const Item*         DefItemSlotHand;
    const Item*         DefItemSlotArmor;
    const Item*         ItemSlotMain;
    const Item*         ItemSlotExt;
    const Item*         ItemSlotArmor;

    const CritterCl*    ThisPtr[ MAX_PARAMETERS_ARRAYS ];
    const bool          ParamsIsChanged[ MAX_PARAMS ];
    const IntVec        ParamsChanged;
    const int           ParamLocked;

    const bool          IsRuning;
    const Uint16PairVec MoveSteps;
};

struct MapObject
{
    const uint8  MapObjType;
    const uint16 ProtoId;
    const uint16 MapX;
    const uint16 MapY;
    const int16  Dir;

    const uint   UID;
    const uint   ContainerUID;
    const uint   ParentUID;
    const uint   ParentChildIndex;

    const uint   LightRGB;
    const uint8  LightDay;
    const uint8  LightDirOff;
    const uint8  LightDistance;
    const int8   LightIntensity;

    const char   ScriptName[ MAPOBJ_SCRIPT_NAME + 1 ];
    const char   FuncName[ MAPOBJ_SCRIPT_NAME + 1 ];

    const uint   Reserved[ 7 ];
    const int    UserData[ 10 ];

    union
    {
        struct
        {
            uint8 Cond;
            uint  Anim1;
            uint  Anim2;
            int16 ParamIndex[ MAPOBJ_CRITTER_PARAMS ];
            int   ParamValue[ MAPOBJ_CRITTER_PARAMS ];
        } const MCritter;

        struct
        {
            int16  OffsetX;
            int16  OffsetY;
            uint8  AnimStayBegin;
            uint8  AnimStayEnd;
            uint16 AnimWait;
            uint8  InfoOffset;
            uint   PicMapHash;
            uint   PicInvHash;

            uint   Count;
            uint8  ItemSlot;

            uint8  BrokenFlags;
            uint8  BrokenCount;
            uint16 Deterioration;

            uint16 AmmoPid;
            uint   AmmoCount;

            uint   LockerDoorId;
            uint16 LockerCondition;
            uint16 LockerComplexity;

            int16  TrapValue;

            int    Val[ 10 ];
        } const MItem;

        struct
        {
            int16  OffsetX;
            int16  OffsetY;
            uint8  AnimStayBegin;
            uint8  AnimStayEnd;
            uint16 AnimWait;
            uint8  InfoOffset;
            uint   PicMapHash;
            uint   PicInvHash;

            bool   CanUse;
            bool   CanTalk;
            uint   TriggerNum;

            uint8  ParamsCount;
            int    Param[ 5 ];

            uint16 ToMapPid;
            uint   ToEntire;
            uint8  ToDir;
        } const MScenery;
    };
};

struct MapEntire
{
    const uint   Number;
    const uint16 HexX;
    const uint16 HexY;
    const uint8  Dir;
};
typedef vector< MapEntire > EntiresVec;

struct SceneryToClient
{
    const uint16 ProtoId;
    const uint8  Flags;
    const uint8  Reserved0;
    const uint16 MapX;
    const uint16 MapY;
    const int16  OffsetX;
    const int16  OffsetY;
    const uint   LightColor;
    const uint8  LightDistance;
    const uint8  LightFlags;
    const int8   LightIntensity;
    const uint8  InfoOffset;
    const uint8  AnimStayBegin;
    const uint8  AnimStayEnd;
    const uint16 AnimWait;
    const uint   PicMapHash;
    const int16  Dir;
    const uint16 Reserved1;
};
typedef vector< SceneryToClient > SceneryToClientVec;

struct ProtoMap
{
    struct
    {
        const uint   Version;
        const uint16 MaxHexX, MaxHexY;
        const int    WorkHexX, WorkHexY;
        const char   ScriptModule[ MAX_SCRIPT_NAME + 1 ];
        const char   ScriptFunc[ MAX_SCRIPT_NAME + 1 ];
        const int    Time;
        const bool   NoLogOut;
        const int    DayTime[ 4 ];
        const uint8  DayColor[ 12 ];

        // Deprecated
        const uint16 HeaderSize;
        const bool   Packed;
        const uint   UnpackedDataLen;
    } const Header;

    const MapObjectVec MObjects;
    const uint         LastObjectUID;

    struct Tile
    {
        const uint   NameHash;
        const uint16 HexX, HexY;
        const int8   OffsX, OffsY;
        const uint8  Layer;
        const bool   IsRoof;
        #ifdef __MAPPER
        const bool   IsSelected;
        #endif
    };
    typedef vector< Tile >    TileVec;
    const TileVec    Tiles;

    #ifdef __MAPPER
    typedef vector< TileVec > TileVecVec;
    const TileVecVec TilesField;
    const TileVecVec RoofsField;

    const TileVec&   GetTiles( uint16 hx, uint16 hy, bool is_roof ) const
    {
        uint index = hy * Header.MaxHexX + hx;
        return is_roof ? RoofsField[ index ] : TilesField[ index ];
    }
    #endif

    #ifdef __SERVER
    const SceneryToClientVec WallsToSend;
    const SceneryToClientVec SceneriesToSend;
    const uint               HashTiles;
    const uint               HashWalls;
    const uint               HashScen;

    const MapObjectVec       CrittersVec;
    const MapObjectVec       ItemsVec;
    const MapObjectVec       SceneriesVec;
    const MapObjectVec       GridsVec;
    const uint8*             HexFlags;
    #endif

    const EntiresVec         MapEntires;

    const int                PathType;
    const string             Name;
    const uint16             Pid;
};

struct Map
{
    const SyncObj   Sync;
    const Mutex     DataLocker;
    const uint8*    HexFlags;
    const CrVec     MapCritters;
    const ClVec     MapPlayers;
    const PcVec     MapNpcs;
    const ItemVec   HexItems;
    const Location* MapLocation;

    struct
    {
        const uint   MapId;
        const uint16 MapPid;
        const uint8  MapRain;
        const bool   IsTurnBasedAviable;
        const int    MapTime;
        const uint   ScriptId;
        const int    MapDayTime[ 4 ];
        const uint8  MapDayColor[ 12 ];
        const uint   Reserved[ 20 ];
        const int    UserData[ MAP_MAX_DATA ];
    } const Data;

    const ProtoMap* Proto;

    const bool      NeedProcess;
    const uint      FuncId[ MAP_EVENT_MAX ];
    const uint      LoopEnabled[ MAP_LOOP_FUNC_MAX ];
    const uint      LoopLastTick[ MAP_LOOP_FUNC_MAX ];
    const uint      LoopWaitTick[ MAP_LOOP_FUNC_MAX ];

    const bool      IsTurnBasedOn;
    const uint      TurnBasedEndTick;
    const int       TurnSequenceCur;
    const UintVec   TurnSequence;
    const bool      IsTurnBasedTimeout;
    const uint      TurnBasedBeginSecond;
    const bool      NeedEndTurnBased;
    const uint      TurnBasedRound;
    const uint      TurnBasedTurn;
    const uint      TurnBasedWholeTurn;

    const bool      IsNotValid;
    const int16     RefCounter;

    uint16 GetMaxHexX() const { return Proto->Header.MaxHexX; }
    uint16 GetMaxHexY() const { return Proto->Header.MaxHexY; }

    #ifdef __SERVER
    bool   IsHexTrigger( uint16 hx, uint16 hy ) const { return FLAG( Proto->HexFlags[ hy * GetMaxHexX() + hx ], FH_TRIGGER ); }
    bool   IsHexTrap( uint16 hx, uint16 hy )    const { return FLAG( HexFlags[ hy * GetMaxHexX() + hx ], FH_WALK_ITEM ); }
    bool   IsHexCritter( uint16 hx, uint16 hy ) const { return FLAG( HexFlags[ hy * GetMaxHexX() + hx ], FH_CRITTER | FH_DEAD_CRITTER ); }
    bool   IsHexGag( uint16 hx, uint16 hy )     const { return FLAG( HexFlags[ hy * GetMaxHexX() + hx ], FH_GAG_ITEM ); }
    uint16 GetHexFlags( uint16 hx, uint16 hy )  const { return ( HexFlags[ hy * GetMaxHexX() + hx ] << 8 ) | Proto->HexFlags[ hy * GetMaxHexX() + hx ]; }
    bool   IsHexPassed( uint16 hx, uint16 hy )  const { return !FLAG( GetHexFlags( hx, hy ), FH_NOWAY ); }
    bool   IsHexRaked( uint16 hx, uint16 hy )   const { return !FLAG( GetHexFlags( hx, hy ), FH_NOSHOOT ); }
    #endif
};

struct ProtoLocation
{
    const bool        IsInit;
    const uint16      LocPid;
    const string      Name;

    const uint        MaxPlayers;
    const Uint16Vec   ProtoMapPids;
    const Uint16Vec   AutomapsPids;
    const UintPairVec Entrance;
    const int         ScriptBindId;

    const uint16      Radius;
    const bool        Visible;
    const bool        AutoGarbage;
    const bool        GeckVisible;
};

struct Location
{
    const SyncObj Sync;
    const MapVec  LocMaps;

    struct
    {
        const uint   LocId;
        const uint16 LocPid;
        const uint16 WX;
        const uint16 WY;
        const uint16 Radius;
        const bool   Visible;
        const bool   GeckVisible;
        const bool   AutoGarbage;
        const bool   ToGarbage;
        const uint   Color;
        const uint   Reserved3[ 59 ];
    } const Data;

    const ProtoLocation* Proto;
    const int            GeckCount;

    const bool           IsNotValid;
    const int16          RefCounter;

    bool IsToGarbage() const { return Data.AutoGarbage || Data.ToGarbage; }
    bool IsVisible()   const { return Data.Visible || ( Data.GeckVisible && GeckCount > 0 ); }
};

struct Field
{
    struct Tile
    {
        const void* Anim;
        const int16 OffsX;
        const int16 OffsY;
        const uint8 Layer;
    };
    typedef vector< Tile > TileVec;

    const CritterCl* Crit;
    const CrClVec    DeadCrits;
    const int        ScrX;
    const int        ScrY;
    const TileVec    Tiles;
    const TileVec    Roofs;
    const ItemVec    Items;
    const int16      RoofNum;
    const bool       ScrollBlock;
    const bool       IsWall;
    const bool       IsWallSAI;
    const bool       IsWallTransp;
    const bool       IsScen;
    const bool       IsExitGrid;
    const bool       IsNotPassed;
    const bool       IsNotRaked;
    const uint8      Corner;
    const bool       IsNoLight;
    const uint8      LightValues[ 3 ];
    const bool       IsMultihex;
};

struct SpriteInfo
{
    const void*  Surface;
    const float  SurfaceUV[ 4 ];
    const uint16 Width;
    const uint16 Height;
    const int16  OffsX;
    const int16  OffsY;
    const void*  Effect;
    const void*  Anim3d;     // If Anim3d != NULL than this is pure 3d animation
};

struct Sprite
{
    // Ordering
    const int     DrawOrderType;     // 0..4 - flat, 5..9 - normal
    const uint    DrawOrderPos;
    const uint    TreeIndex;

    // Sprite information, pass to GetSpriteInfo
    const uint    SprId;
    const uint*   PSprId;     // If PSprId == NULL than used SprId

    // Positions
    const int     HexX, HexY;
    const int     ScrX, ScrY;
    const int16*  OffsX, * OffsY;

    // Cutting
    const int     CutType;     // See Sprites cutting
    const Sprite* Parent, * Child;
    const float   CutX, CutW, CutTexL, CutTexR;

    // Other
    const uint8*  Alpha;
    const uint8*  Light;
    const int     EggType;
    const int     ContourType;
    const uint    ContourColor;
    const uint    Color;
    const uint    FlashMask;
    const bool*   ValidCallback;
    const bool    Valid;     // If Valid == false than this sprite not valid

    #ifdef __MAPPER
    const int     CutOyL, CutOyR;
    #endif

    uint const GetSprId()
    {
        return PSprId ? *PSprId : SprId;
    }

    SpriteInfo* const GetSprInfo()
    {
        return FOnline->GetSpriteInfo( PSprId ? *PSprId : SprId );
    }

    void const GetPos( int& x, int& y )
    {
        SpriteInfo* si = GetSprInfo();
        x = (int) ( (float) ( ScrX - si->Width / 2 + si->OffsX + ( OffsX ? *OffsX : 0 ) + FOnline->ScrOx ) / FOnline->SpritesZoom );
        y = (int) ( (float) ( ScrY - si->Height    + si->OffsY + ( OffsY ? *OffsY : 0 ) + FOnline->ScrOy ) / FOnline->SpritesZoom );
    }
};


inline Field* GetField( uint hexX, uint hexY )
{
    if( !FOnline->ClientMap || hexX >= FOnline->ClientMapWidth || hexY >= FOnline->ClientMapHeight )
        return NULL;
    return &FOnline->ClientMap[ hexY * FOnline->ClientMapWidth + hexX ];
}

inline uint GetFieldLight( uint hexX, uint hexY )
{
    if( !FOnline->ClientMapLight || hexX >= FOnline->ClientMapWidth || hexY >= FOnline->ClientMapHeight )
        return 0;
    uint r = FOnline->ClientMapLight[ hexY * FOnline->ClientMapWidth * 3 + hexX * 3 + 0 ];
    uint g = FOnline->ClientMapLight[ hexY * FOnline->ClientMapWidth * 3 + hexX * 3 + 1 ];
    uint b = FOnline->ClientMapLight[ hexY * FOnline->ClientMapWidth * 3 + hexX * 3 + 2 ];
    uint rgb = ( r << 16 ) | ( g << 8 ) | ( b );
    return rgb;
}

inline int GetDirection( int x1, int y1, int x2, int y2 )
{
    if( FOnline->MapHexagonal )
    {
        float hx = (float) x1;
        float hy = (float) y1;
        float tx = (float) x2;
        float ty = (float) y2;
        float nx = 3 * ( tx - hx );
        float ny = ( ty - hy ) * SQRT3T2_FLOAT - ( float(x2 & 1) - float(x1 & 1) ) * SQRT3_FLOAT;
        float dir = 180.0f + RAD2DEG* atan2f( ny, nx );

        if( dir >= 60.0f  && dir < 120.0f )
            return 5;
        if( dir >= 120.0f && dir < 180.0f )
            return 4;
        if( dir >= 180.0f && dir < 240.0f )
            return 3;
        if( dir >= 240.0f && dir < 300.0f )
            return 2;
        if( dir >= 300.0f )
            return 1;
        return 0;
    }
    else
    {
        float dir = 180.0f + RAD2DEG* atan2( (float) ( x2 - x1 ), (float) ( y2 - y1 ) );

        if( dir >= 22.5f  && dir <  67.5f )
            return 7;
        if( dir >= 67.5f  && dir < 112.5f )
            return 0;
        if( dir >= 112.5f && dir < 157.5f )
            return 1;
        if( dir >= 157.5f && dir < 202.5f )
            return 2;
        if( dir >= 202.5f && dir < 247.5f )
            return 3;
        if( dir >= 247.5f && dir < 292.5f )
            return 4;
        if( dir >= 292.5f && dir < 337.5f )
            return 5;
        return 6;
    }
}

inline int GetDistantion( int x1, int y1, int x2, int y2 )
{
    if( FOnline->MapHexagonal )
    {
        int dx = ( x1 > x2 ? x1 - x2 : x2 - x1 );
        if( x1 % 2 == 0 )
        {
            if( y2 <= y1 )
            {
                int rx = y1 - y2 - dx / 2;
                return dx + ( rx > 0 ? rx : 0 );
            }
            else
            {
                int rx = y2 - y1 - ( dx + 1 ) / 2;
                return dx + ( rx > 0 ? rx : 0 );
            }
        }
        else
        {
            if( y2 >= y1 )
            {
                int rx = y2 - y1 - dx / 2;
                return dx + ( rx > 0 ? rx : 0 );
            }
            else
            {
                int rx = y1 - y2 - ( dx + 1 ) / 2;
                return dx + ( rx > 0 ? rx : 0 );
            }
        }
    }
    else
    {
        int dx = abs( x2 - x1 );
        int dy = abs( y2 - y1 );
        return max( dx, dy );
    }
}


inline void static_asserts()
{
    STATIC_ASSERT( sizeof( char )        == 1 );
    STATIC_ASSERT( sizeof( int8 )        == 1 );
    STATIC_ASSERT( sizeof( int16 )       == 2 );
    STATIC_ASSERT( sizeof( int )         == 4 );
    STATIC_ASSERT( sizeof( int64 )       == 8 );
    STATIC_ASSERT( sizeof( uint8 )       == 1 );
    STATIC_ASSERT( sizeof( uint16 )      == 2 );
    STATIC_ASSERT( sizeof( uint )        == 4 );
    STATIC_ASSERT( sizeof( uint64 )      == 8 );
    STATIC_ASSERT( sizeof( bool )        == 1 );

    #if defined ( _M_IX86 )
    STATIC_ASSERT( sizeof( string )       == 24   );
    STATIC_ASSERT( sizeof( IntVec )       == 12   );
    STATIC_ASSERT( sizeof( IntMap )       == 24   );
    STATIC_ASSERT( sizeof( IntSet )       == 24   );
    STATIC_ASSERT( sizeof( IntPair )      == 8    );
    STATIC_ASSERT( sizeof( ProtoItem )    == 908  );
    STATIC_ASSERT( sizeof( Mutex )        == 24   );
    STATIC_ASSERT( sizeof( GameOptions )  == 1332 );
    STATIC_ASSERT( sizeof( SpriteInfo )   == 36   );
    STATIC_ASSERT( sizeof( Field )        == 76   );
    # ifdef __MAPPER
    STATIC_ASSERT( sizeof( Sprite )       == 116  );
    # else
    STATIC_ASSERT( sizeof( Sprite )       == 108  );
    # endif

    STATIC_ASSERT( offsetof( TemplateVar, Flags )              == 68   );
    STATIC_ASSERT( offsetof( NpcPlane, RefCounter )            == 88   );
    STATIC_ASSERT( offsetof( GlobalMapGroup, EncounterForce )  == 64   );
    STATIC_ASSERT( offsetof( Item, IsNotValid )                == 146  );
    STATIC_ASSERT( offsetof( CritterTimeEvent, Identifier )    == 12   );
    STATIC_ASSERT( offsetof( Critter, RefCounter )             == 9388 );
    STATIC_ASSERT( offsetof( Client, LanguageMsg )             == 9456 );
    STATIC_ASSERT( offsetof( Npc, Reserved )                   == 9408 );
    STATIC_ASSERT( offsetof( CritterCl, MoveSteps )            == 5704 );
    STATIC_ASSERT( offsetof( MapEntire, Dir )                  == 8    );
    STATIC_ASSERT( offsetof( SceneryToClient, Reserved1 )      == 30   );
    STATIC_ASSERT( offsetof( Map, RefCounter )                 == 774  );
    STATIC_ASSERT( offsetof( ProtoLocation, GeckVisible )      == 76   );
    STATIC_ASSERT( offsetof( Location, RefCounter )            == 282  );

    # ifdef __SERVER
    STATIC_ASSERT( offsetof( ProtoMap, HexFlags )              == 304  );
    # endif
    #endif // defined(_M_IX86)
}

#endif     // __FONLINE__
