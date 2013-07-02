#ifdef _MSC_VER
	#include <WinSock2.h>
	#define socklen_t int
	#pragma comment(lib, "Ws2_32.lib")
	#define strdup _strdup

	#define _FUNC_  __FUNCTION__
#else // linux
	#include <arpa/inet.h>
	#include <netinet/in.h>
	#include <sys/socket.h>
	#include <sys/types.h>
	#include <netdb.h>
	#include <unistd.h>

	#define SOCKET int
	#define INVALID_SOCKET (-1)
	#define SOCKET_ERROR (-1)
	#define closesocket close 
	#define SD_RECEIVE SHUT_RD
	#define SD_SEND    SHUT_WR
	#define SD_BOTH    SHUT_RDWR

	#define _FUNC_  __PRETTY_FUNCTION__
#endif

#include <string>
#include <vector>

#define SKIP_PRAGMA
#include "../fonline2238.h"

#define _(x) if( (ASEngine->x) < 0 ) { Log( "DevConnect : ASSERT %s %d\n", __FILE__, __LINE__ ); }
#define __ DevClient::Script::
#define OFFSETOF(type,member) ((int)offsetof(type,member))
#define memzero(ptr,size)  memset(ptr,0,size)

using namespace std;

typedef unsigned short ushort;

enum DevClientState
{
	DevConnected = 0,
	DevAuthName,
	DevAuthPassword,
	DevWelcome,
	DevWork,
	DevDisconnect
};

class DevClient
{
//
public:
	uint Id;
	string Name;
	DevClientState State;
	uint LastProcess;
	uint LastActive;

	SOCKET Sock;
	sockaddr_in From;

	void AddRef() {}
	void Release() {}

	const char* ReadSocket();
	void WriteSocket( const char* str );

	void OnConnect();
	void OnAuth();
	void OnWelcome();
	void OnWork();

	struct Script
	{
		static uint GetIp( DevClient* dev );
		static void Disconnect( DevClient* dev );
		static void SendText( DevClient* dev, ScriptString* text );
		static void SendData( DevClient* dev, ScriptArray* data );
	};
};

vector<DevClient*> Clients;

bool Initialized = false;

SOCKET DevSocket = NULL;
string DevModule; // don't you *EVER* try to initialize it
uint DevId = 0;

void DevConnectLog( const char* text );

bool DevConnectInit( ScriptString* module, uint port )
{
	// phase 1, module
	if( module && !module->c_std_str().empty() )
	{
		asIScriptModule* tmp = ASEngine->GetModule( module->c_str(), asGM_ONLY_IF_EXISTS );
		if( tmp )
			DevModule = module->c_std_str();
		else
		{
			Log( "DevConnect : missing module<%s>\n", module->c_str() );
			return( false );
		}
	}
	else
	{
		Log( "DevConnect : empty module name\n" );
		return( false );
	}

	// phase 2, port
	if( port <= 1024 || port >= 65535 )
	{
		Log( "DevConnect : invalid port<%d>\n", port );
		return( false );
	}

	#ifdef _MSC_VER
    WSADATA wsa;
    if( WSAStartup( MAKEWORD( 2, 2 ), &wsa ) )
    {
        Log( "DevConnect : WSAStartup() fail\n" );
        return( false );
    }
    #endif

	DevSocket = socket( AF_INET, SOCK_STREAM, 0 );
    if( DevSocket == INVALID_SOCKET )
    {
        Log( "DevConnect : socket() fail\n" );
        return( false );
    }
    const int   opt = 1;
    setsockopt( DevSocket, SOL_SOCKET, SO_REUSEADDR, (char*)&opt, sizeof(opt) );

	sockaddr_in sin;
    sin.sin_family = AF_INET;
    sin.sin_port = htons( (ushort)(size_t)port );
    sin.sin_addr.s_addr = INADDR_ANY;
    if( bind( DevSocket, (sockaddr*) &sin, sizeof( sin ) ) == SOCKET_ERROR )
    {
        Log( "DevConnect : bind() fail\n" );
        closesocket( DevSocket );
        return( false );
    }

	if( listen( DevSocket, SOMAXCONN ) == SOCKET_ERROR )
    {
        Log( "DevConnect : listen() fail\n" );
        closesocket( DevSocket );
        return( false );
    }

	Log( "DevConnect : module<%s> port<%d>\n", DevModule.c_str(), port );

#if 0
	// phase 3, log redirection
	FOnline->SetLogCallback( DevConnectLog, true );
#endif

	Initialized = true;
	return( true );
}

void DevConnectWork()
{
	if( !Initialized )
		return;

    timeval tv = { 0, 10 };
    fd_set  sock_set;
    FD_ZERO( &sock_set );
    FD_SET( DevSocket, &sock_set );
    if( select( DevSocket + 1, &sock_set, NULL, NULL, &tv ) > 0 )
    {
        sockaddr_in from;
        socklen_t   len = sizeof( from );
        SOCKET      sock = accept( DevSocket, (sockaddr*) &from, &len );
        if( sock != INVALID_SOCKET )
		{
			bool refuse = false;
			// no more than 1 connection per ip
			for( auto client = Clients.begin(); client != Clients.end(); ++client )
			{
				DevClient* dev = *client;
				if( dev->From.sin_addr.s_addr == from.sin_addr.s_addr )
				{
					refuse = true;
					break;
				}
			}
			if( !refuse )
			{
				DevClient* dev = new DevClient();
				dev->Id = ++DevId;
				dev->Name = "none";
				dev->State = DevConnected;
				dev->LastActive = GetTickCount();
				dev->LastProcess = GetTickCount();
				dev->Sock = sock;
				dev->From = from;

#ifdef _MSC_VER
				u_long argp = 1;
				ioctlsocket( dev->Sock, FIONBIO, &argp);
#else // linux
				int flags;
				flags = fcntl( dev->Sock, F_GETFL, 0);
				flags |= O_NONBLOCK;
				fcntl(dev->Sock, F_SETFL, flags);
#endif
				Clients.push_back( dev );
			}
			else
			{
				shutdown( sock, SD_BOTH );
				closesocket( sock );
			}
		}
	}

	for( auto client = Clients.begin(); client != Clients.end(); )
	{
		DevClient* dev = *client;
		bool disconnect = false;

		if( dev->Sock == INVALID_SOCKET || dev->State == DevDisconnect )
			disconnect = true;

		if( disconnect )
		{
			if( dev->Sock != INVALID_SOCKET )
				shutdown( dev->Sock, SD_BOTH );
			delete dev;
			client = Clients.erase( client );
		}
		else
		{
			if( GetTickCount() - dev->LastProcess >= 1000 )
			{
				dev->LastProcess = GetTickCount();

				switch( dev->State )
				{
					case DevConnected:
						dev->OnConnect();
						break;
					case DevAuthName:
					case DevAuthPassword:
						dev->OnAuth();
						break;
					case DevWelcome:
						dev->OnWelcome();
						break;
					case DevWork:
						dev->OnWork();
						break;

					default:
						break;
				}
			}

			++client;
 		}
	}
}

void DevConnectLog( const char* text )
{
	if( Clients.empty() )
		return;

	for( auto client = Clients.begin(); client != Clients.end(); ++client )
	{
		DevClient* dev = *client;
		if( dev->State == DevWork )
			dev->WriteSocket( text );
	}
}

void DevConnectLog_( ScriptString* text )
{
	if( !text )
	{
		Log( "DevConnect : DevConnectLog_() without text\n" );
		return;
	}

	if( !text->length() )
	{
		Log( "DevConnect : DevConnectLog_() without text length\n" );
		return;
	}

	if( text->length() >= 1024 )
	{
		Log( "DevConnect : DevConnectLog_() too long<%d> : %s\n", text->length(), _FUNC_ );
		return;
	}

	DevConnectLog( text->c_str() );
}

DevClient* GetDevClient( uint id )
{
	for( uint c=0, cLen=Clients.size(); c<cLen; c++ )
	{
		if( Clients[c]->Id == id )
			return( Clients[c] );
	}

	return( NULL );
}

uint GetDevClients( ScriptArray* clients )
{
	uint count = Clients.size();

	if( clients )
	{
		clients->Resize(count);

		for( uint c = 0; c<count; c++ )
		{
			*((DevClient**)(clients->At(c))) = Clients[c];
		}
	}

	return( count );
}

const char* DevClient::ReadSocket()
{
	char cmd[ 1024 ];
	memzero( cmd, sizeof( cmd ) );

	int len = recv( Sock, cmd, sizeof(cmd), 0 );
	if( len <= 0 )
	{
		if( len == 0 ) // graceful close
		{
			Log( "DevConnect : %s : socket closed\n", Name );
			State = DevDisconnect;
		}
		//Log( "len <= 0 (%d)\n", len );
		return( NULL );
	}

	LastActive = GetTickCount();
	cmd[ len ] = 0;

	char* front = cmd;
	while( *front && ( *front == ' ' || *front == '\t' || *front == '\n' || *front == '\r' ) )
		front++;
	if( front != cmd )
	{
		char* str_ = cmd;
		while( *front )
			*str_++ = *front++;
		*str_ = 0;
	}

	char* back = cmd;
	while( *back )
		back++;
	back--;
	while( back >= cmd && ( *back == ' ' || *back == '\t' || *back == '\n' || *back == '\r' ) )
		back--;
	*( back + 1 ) = 0;

 	return( strdup( cmd ));
}

void DevClient::WriteSocket( const char* str )
{
	char buf[1024];
	size_t str_len = strlen(str);
	if( str_len >= 1024 )
		str_len = 1023;
	memcpy( buf, str, str_len);
	buf[str_len] = 0;
	size_t buf_len = strlen( buf ) + 1;
	if( send( Sock, buf, buf_len, 0 ) != (int)buf_len )
	{
		Log( "DevConnect : %s : WriteSocket() fail, disconnecting\n", Name );
		State = DevDisconnect;
	}
}

void DevClient::OnConnect()
{
	int bindId = FOnline->ScriptBind( DevModule.c_str(), "bool dev_connect(uint)", true );
	if( bindId && FOnline->ScriptPrepare( bindId ))
	{
		FOnline->ScriptSetArgUInt( From.sin_addr.S_un.S_addr );
		FOnline->ScriptRunPrepared();
		if( FOnline->ScriptGetReturnedBool() )
			State = DevAuthName;
		else
			State = DevDisconnect;
	}
	else
	{
		Log( "DevConnect : cannot bind dev_connect()\n" );
		State = DevDisconnect;
	}
}

void DevClient::OnAuth()
{
	const char* auth = ReadSocket();
	if( auth == NULL || !strlen( auth ))
		return;

	if( State == DevAuthName )
	{
		if( strcmp( auth, "none" ))
		{
			// cannot login with name already in use (including not yet authenticated)
			for( auto client = Clients.begin(); client != Clients.end(); ++client )
			{
				DevClient* dev = *client;
				if( !strcmp( dev->Name.c_str(), auth ))
				{
					State = DevDisconnect;
					return;
				}
			}

			Name.assign( auth );
			State = DevAuthPassword;
		}
		else
		{
			// cannot login as "none"
			State = DevDisconnect;
		}
	}
	else if( State == DevAuthPassword )
	{
		int bindId = FOnline->ScriptBind( DevModule.c_str(), "bool dev_login(string& name, string& password)", true );
		if( bindId && FOnline->ScriptPrepare( bindId ))
		{
			ScriptString& _name = ScriptString::Create( Name.c_str() );
			ScriptString& _password = ScriptString::Create( auth );

			FOnline->ScriptSetArgObject( &_name );
			FOnline->ScriptSetArgObject( &_password );
			if( FOnline->ScriptRunPrepared() )
			{
				if( FOnline->ScriptGetReturnedBool() )
					State = DevWelcome;
				else
					State = DevDisconnect;
			}
			else
			{
				Log( "DevConnect : failed to run prepared dev_login()\n" );
				State = DevDisconnect;
			}
			_name.Release();
			_password.Release();
		}
		else
		{
			Log( "DevConnect : cannot bind dev_login()\n" );
			State = DevDisconnect;
		}
	}
}

void DevClient::OnWelcome()
{
	int bindId = FOnline->ScriptBind( DevModule.c_str(), "void dev_welcome(DevClient& dev)", true );
	if( bindId && FOnline->ScriptPrepare( bindId ))
	{
		FOnline->ScriptSetArgAddress( this );

		if( FOnline->ScriptRunPrepared() )
			State = DevWork;
		else
		{
			Log( "DevConnect : failed to run prepared dev_welcome()\n" );
			State = DevDisconnect;
		}
	}
	else
	{
		Log( "DevConnect : cannot bind dev_welcome()\n" );
		State = DevDisconnect;
	}
}

void DevClient::OnWork()
{
	const char* input = ReadSocket();
	if( !input || !strlen( input ))
		return;

	int bindId = FOnline->ScriptBind( DevModule.c_str(), "void dev_command(DevClient& dev, string& command)", true );
	if( bindId && FOnline->ScriptPrepare( bindId ))
	{
		ScriptString& _input = ScriptString::Create( input );

		FOnline->ScriptSetArgAddress( this );
		FOnline->ScriptSetArgObject( &_input );

		if( !FOnline->ScriptRunPrepared() )
		{
			Log( "DevConnect : failed to run prepared dev_command()\n" );
			State = DevDisconnect;
		}
		_input.Release();
	}
	else
	{
		Log( "DevConnect : cannot bind dev_command()\n" );
		State = DevDisconnect;
	}
}

//

uint DevClient::Script::GetIp( DevClient* dev )
{
	return( (uint)dev->From.sin_addr.S_un.S_addr );
}

void DevClient::Script::Disconnect( DevClient* dev )
{
	dev->State = DevDisconnect;
}

void DevClient::Script::SendText( DevClient* dev, ScriptString* text )
{
	if( dev->Sock == INVALID_SOCKET )
	{
		Log( "DevConnect : SendText() on invalid socket\n" );
		return;
	}

	if( dev->State == DevDisconnect )
	{
		Log( "DevConnect : SendText() on disconnected socket\n" );
		return;
	}

	if( !text )
	{
		Log( "DevConnect : SendText() without text\n" );
		return;
	}
	
	if( !text->length() )
	{
		Log( "DevConnect : SendText() without text length\n" );
		return;
	}

	if( text->length() >= 1024 )
	{
		Log( "DevConnect : SendText() too long<%d> : %s\n", text->length(), _FUNC_ );
		return;
	}

	dev->WriteSocket( text->c_str() );
}

void DevClient::Script::SendData( DevClient* dev, ScriptArray* data )
{
	if( dev->Sock == INVALID_SOCKET )
	{
		Log( "DevConnect : SendData() on invalid socket\n" );
		return;
	}

	if( dev->State == DevDisconnect )
	{
		Log( "DevConnect : SendData() on disconnected socket\n" );
		return;
	}

	if( !data )
	{
		Log( "DevConnect : SendData() without data\n" );
		return;
	}
	
	if( !data->GetSize() )
	{
		Log( "DevConnect : SendData() without data length\n" );
		return;
	}

	if( data->GetSize() >= 1024 )
	{
		Log( "DevConnect : SendData() too long<%d> : %s\n", data->GetSize(), _FUNC_ );
		return;
	}

	char tdata[1024];
	uint d = 0;
	for( uint dLen=data->GetSize(); d<dLen; d++ )
	{
		tdata[d] = *(char*)data->At(d);
	}
	tdata[d] = 0;

	dev->WriteSocket( strdup( tdata ));
}

void RegisterDevConnect()
{
	const char* devState = "DevClientState";
	const char* devClient = "DevClient";

	_( RegisterEnum( devState ));
	_( RegisterEnumValue( devState, "DevConnected", DevConnected ));
	_( RegisterEnumValue( devState, "DevAuthName", DevAuthName ));
	_( RegisterEnumValue( devState, "DevAuthPassword", DevAuthPassword ));
	_( RegisterEnumValue( devState, "DevWelcome", DevWelcome ));
	_( RegisterEnumValue( devState, "DevWork", DevWork ));
	_( RegisterEnumValue( devState, "DevDisconnect", DevDisconnect ));

	_( RegisterObjectType( devClient, 0, asOBJ_REF ));
	_( RegisterObjectBehaviour( devClient, asBEHAVE_ADDREF, "void f()", asMETHOD( DevClient, AddRef ), asCALL_THISCALL ));
	_( RegisterObjectBehaviour( devClient, asBEHAVE_RELEASE, "void f()", asMETHOD( DevClient, Release ), asCALL_THISCALL ));

	_( RegisterObjectProperty( devClient, "const uint Id", OFFSETOF( DevClient,Id )));
	_( RegisterObjectProperty( devClient, "const DevClientState State", OFFSETOF( DevClient,State )));
	_( RegisterObjectProperty( devClient, "const uint LastProcess", OFFSETOF( DevClient,LastProcess )));
	_( RegisterObjectProperty( devClient, "const uint LastActive", OFFSETOF( DevClient,LastActive )));

	_( RegisterObjectMethod( devClient, "uint GetIp()", asFUNCTION( __ GetIp ), asCALL_CDECL_OBJFIRST ));
	_( RegisterObjectMethod( devClient, "void Disconnect()", asFUNCTION( __ Disconnect ), asCALL_CDECL_OBJFIRST ));
	_( RegisterObjectMethod( devClient, "void SendText( string& text )", asFUNCTION( __ SendText ), asCALL_CDECL_OBJFIRST ));
	_( RegisterObjectMethod( devClient, "void SendData( uint8[]& data )", asFUNCTION( __ SendData ), asCALL_CDECL_OBJFIRST ));

	_( RegisterGlobalProperty( "const bool DevConnectInitialized", &Initialized ));

	_( RegisterGlobalFunction( "bool DevConnectInit(string& module, uint port)", asFUNCTION( DevConnectInit ), asCALL_CDECL ));
	_( RegisterGlobalFunction( "void DevConnectWork()", asFUNCTION( DevConnectWork ), asCALL_CDECL ));
	_( RegisterGlobalFunction( "void DevConnectLog(string& text)", asFUNCTION( DevConnectLog_ ), asCALL_CDECL ));
	_( RegisterGlobalFunction( "DevClient@ GetDevClient(uint id)", asFUNCTION( GetDevClient ), asCALL_CDECL ));
	_( RegisterGlobalFunction( "uint GetDevClients(DevClient@[]@+ clients)", asFUNCTION( GetDevClients ), asCALL_CDECL ));
}
