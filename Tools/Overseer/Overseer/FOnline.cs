using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace FOnline
{
    /// <summary>
    /// Copyleft 1999 by Borg Locutus (dystopia@iname.com)
    /// See http://140.112.31.133/~Borg/f2/
    /// Minor modifications by Wipe/Rotators
    /// </summary>
    class FalloutFRM
    {
        private static void Load( Stream input, ref List<Bitmap> result )
        {
            byte[] buf;
            int chunkBegin, ptr, fileEnd;
            int file_siz, i, frame_num;
            int width, height, aligned_width;
            MemoryStream memStream;

            file_siz = (int)input.Length;
            buf = new byte[file_siz];
            input.Read( buf, 0, file_siz );
            input.Close();

            chunkBegin = 0x3e;
            fileEnd = file_siz;

            frame_num = 0;
            do
            {
                width = buf[chunkBegin] * 256 + buf[chunkBegin + 1];
                height = buf[chunkBegin + 2] * 256 + buf[chunkBegin + 3];
                aligned_width = (4 - width % 4) % 4;
                file_siz = (buf[chunkBegin + 5] * 256 + buf[chunkBegin + 6]) * 256 + buf[chunkBegin + 7];
                frame_num++;

                memStream = new MemoryStream( bmpHeader.Length + height * (width + aligned_width) );
                memStream.Write( bmpHeader, 0, bmpHeader.Length );

                ptr = chunkBegin + 12 + width * (height - 1);
                for( i = 0 ; i < height ; i++ )
                {
                    memStream.Write( buf, ptr, width );
                    memStream.Write( zeros, 0, aligned_width );
                    ptr -= width;
                }

                memStream.Seek( 18L, SeekOrigin.Begin );
                memStream.Write( BitConverter.GetBytes( width ), 0, 4 );
                memStream.Write( BitConverter.GetBytes( height ), 0, 4 );
                result.Add( new Bitmap( memStream ) );
                memStream.Close();
                chunkBegin += (12 + file_siz);
            }
            while( fileEnd - chunkBegin > 12 );
        }

        public static List<Bitmap> Load( string filename )
        {
            if( File.Exists( filename ) )
            {
                FileStream file = null;
                try
                {
                    file = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.Read );
                }
                catch
                {
                    return (null);
                }
                List<Bitmap> bmpList = new List<Bitmap>();
                Load( file, ref bmpList );
                if( bmpList.Count == 0 )
                    return (null);
                return (bmpList);
            }
            else
                return (null);
        }

        public static List<Bitmap> Load( byte[] buffer )
        {
            if( buffer.Length > 0 )
            {
                List<Bitmap> bmpList = new List<Bitmap>();
                Load( new MemoryStream( buffer ), ref bmpList );
                if( bmpList.Count == 0 )
                    return (null);

                return (bmpList);
            }
            else
            {
                return (null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">path to .frm file</param>
        /// <param name="index">if too low/high, it's changed to 1</param>
        /// <returns>converted image</returns>
        public static Bitmap Load( string filename, int index )
        {
            List<Bitmap> bmpList = Load( filename );
            if( bmpList == null || bmpList.Count == 0 )
                return (null);
            else if( index <= 0 || index > bmpList.Count )
                index = 1;

            return (new Bitmap( bmpList[index - 1] ));
        }

        #region Bytes
        private static byte[] zeros = { 0, 0, 0, 0 };

        private static byte[] bmpHeader =
            {
                0x42,0x4D,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x36,0x04,0x00,0x00,0x28,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x08,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,
                0x00,0x00,0x00,0x01,0x00,0x00,0x0B,0x00,0x0B,0x00,0x41,0x40,0x91,0x00,0x00,0xA2,
                0x00,0x00,0x00,0xA1,0xA2,0x00,0xD1,0xD0,0xD1,0x00,0xC9,0xC7,0xC9,0x00,0xBC,0xBB,
                0xBC,0x00,0xB0,0xAE,0xB0,0x00,0xA3,0xA1,0xA3,0x00,0x99,0x97,0x99,0x00,0x8B,0x89,
                0x8B,0x00,0x7C,0x7A,0x7C,0x00,0x70,0x6E,0x70,0x00,0x5F,0x5D,0x5F,0x00,0x4D,0x4A,
                0x4D,0x00,0x42,0x40,0x42,0x00,0xF3,0xF2,0xFE,0x00,0xE5,0xE5,0xF3,0x00,0xD6,0xD6,
                0xE8,0x00,0xC8,0xC8,0xE0,0x00,0xBC,0xBC,0xD4,0x00,0xAF,0xAF,0xC9,0x00,0xA1,0xA2,
                0xBF,0x00,0x94,0x94,0xB3,0x00,0x85,0x86,0xA6,0x00,0x7A,0x7B,0x9C,0x00,0x6A,0x6B,
                0x8E,0x00,0x5E,0x5E,0x80,0x00,0x50,0x50,0x74,0x00,0x45,0x46,0x64,0x00,0x36,0x36,
                0x51,0x00,0x2B,0x29,0x42,0x00,0xFE,0xF2,0xF2,0x00,0xF3,0xE4,0xE5,0x00,0xE8,0xD6,
                0xD7,0x00,0xE0,0xC8,0xC8,0x00,0xD4,0xBC,0xBC,0x00,0xC9,0xAF,0xAF,0x00,0xBF,0xA1,
                0xA2,0x00,0xB3,0x94,0x94,0x00,0xA6,0x85,0x86,0x00,0x9C,0x7A,0x7B,0x00,0x8E,0x6A,
                0x6B,0x00,0x80,0x5E,0x5E,0x00,0x74,0x50,0x50,0x00,0x64,0x46,0x48,0x00,0x51,0x34,
                0x36,0x00,0x42,0x29,0x29,0x00,0xF6,0xC8,0xFD,0x00,0xC3,0x86,0xD6,0x00,0x86,0x46,
                0x8D,0x00,0x6F,0x31,0x73,0x00,0x5A,0x22,0x5E,0x00,0x45,0x29,0x4B,0x00,0x45,0x16,
                0x46,0x00,0x36,0x20,0x3D,0x00,0xEE,0xF8,0xF8,0x00,0xD7,0xEE,0xEF,0x00,0x90,0xCF,
                0xD9,0x00,0x73,0xC3,0xD2,0x00,0x51,0xB7,0xCC,0x00,0x55,0xA8,0xBF,0x00,0x46,0x9C,
                0xAF,0x00,0x36,0x8D,0x9F,0x00,0x29,0x80,0x91,0x00,0x1A,0x6E,0x7F,0x00,0x11,0x5E,
                0x70,0x00,0x00,0x4B,0x5A,0x00,0x00,0x37,0x41,0x00,0xD4,0xF5,0xDC,0x00,0xA9,0xE5,
                0xCB,0x00,0x94,0xCE,0xB5,0x00,0x83,0xB6,0x9B,0x00,0x6E,0x9B,0x81,0x00,0x5B,0x7F,
                0x67,0x00,0x40,0x5E,0x4A,0x00,0x77,0x86,0x94,0x00,0x58,0x6F,0x7B,0x00,0x41,0x55,
                0x5F,0x00,0x77,0x9B,0x8E,0x00,0x42,0x9B,0x95,0x00,0x4B,0x8D,0x94,0x00,0x48,0x86,
                0x85,0x00,0x46,0x6B,0x73,0x00,0x41,0x55,0x5F,0x00,0xB8,0xC5,0xB9,0x00,0x9A,0xB1,
                0x9B,0x00,0x80,0x9F,0x7E,0x00,0x66,0x8C,0x67,0x00,0x7F,0x7E,0x5E,0x00,0x6E,0x73,
                0x55,0x00,0x62,0x6B,0x4B,0x00,0x50,0x62,0x41,0x00,0x48,0x56,0x3C,0x00,0x36,0x4B,
                0x30,0x00,0x28,0x41,0x29,0x00,0x37,0x55,0x34,0x00,0x22,0x46,0x28,0x00,0x11,0x3C,
                0x18,0x00,0x00,0x30,0x11,0x00,0x00,0x22,0x0E,0x00,0xB8,0xB8,0xAC,0x00,0xB5,0xB2,
                0x9B,0x00,0xB3,0xA9,0x8B,0x00,0xAF,0x9F,0x77,0x00,0xAC,0x90,0x67,0x00,0xAC,0x7F,
                0x55,0x00,0x9F,0x73,0x51,0x00,0x90,0x6B,0x4A,0x00,0x83,0x5E,0x42,0x00,0x73,0x55,
                0x3A,0x00,0x67,0x4B,0x37,0x00,0xBF,0xBF,0xB9,0x00,0x8D,0x6F,0x5E,0x00,0x80,0x7F,
                0x78,0x00,0xA5,0x8D,0x7E,0x00,0x77,0x67,0x5D,0x00,0xD1,0xD0,0xD1,0x00,0xB5,0xBF,
                0xC4,0x00,0xA0,0xB0,0xBC,0x00,0x85,0x9F,0xB2,0x00,0x72,0x8C,0xA9,0x00,0x5A,0x80,
                0x9F,0x00,0x48,0x6F,0x94,0x00,0x30,0x61,0x8A,0x00,0x1A,0x54,0x7F,0x00,0xDB,0xDB,
                0xFD,0x00,0xC9,0xC8,0xFE,0x00,0xB6,0xB5,0xFE,0x00,0x9F,0x9E,0xFD,0x00,0x8A,0x89,
                0xFD,0x00,0x6F,0x6F,0xFE,0x00,0x6B,0x6B,0xF4,0x00,0x66,0x64,0xE2,0x00,0x5C,0x5C,
                0xD0,0x00,0x55,0x54,0xC0,0x00,0x4B,0x4D,0xAD,0x00,0x44,0x44,0x9C,0x00,0x3D,0x3C,
                0x89,0x00,0x33,0x31,0x71,0x00,0x28,0x28,0x5C,0x00,0xDB,0xE9,0xFC,0x00,0xB9,0xD5,
                0xFB,0x00,0xA9,0xCB,0xFA,0x00,0x96,0xC0,0xF9,0x00,0x81,0xB5,0xFA,0x00,0x6D,0xAC,
                0xF8,0x00,0x58,0xA1,0xF7,0x00,0x3E,0x97,0xF6,0x00,0x0B,0x91,0xE7,0x00,0x00,0x87,
                0xD4,0x00,0x00,0x77,0xBF,0x00,0x00,0x6B,0xA5,0x00,0x07,0x5A,0x8E,0x00,0x07,0x46,
                0x73,0x00,0x00,0x36,0x54,0x00,0xC7,0xDF,0xF8,0x00,0x9C,0xC8,0xE5,0x00,0x8A,0xBC,
                0xD9,0x00,0x7B,0xAF,0xD0,0x00,0x6B,0xA2,0xC4,0x00,0x5B,0x98,0xB9,0x00,0x4B,0x8A,
                0xAB,0x00,0x3A,0x7F,0x9F,0x00,0x2E,0x73,0x94,0x00,0x1A,0x67,0x86,0x00,0x11,0x5A,
                0x77,0x00,0x00,0x4B,0x68,0x00,0x00,0x41,0x5A,0x00,0xD9,0xE9,0xF9,0x00,0xB6,0xD9,
                0xEF,0x00,0x9F,0xC5,0xE1,0x00,0x8B,0xAF,0xD6,0x00,0x72,0x98,0xC8,0x00,0x5E,0x82,
                0xBB,0x00,0x50,0x73,0xAF,0x00,0x40,0x62,0xA5,0x00,0x36,0x50,0x9A,0x00,0x2E,0x42,
                0x91,0x00,0x28,0x3A,0x7F,0x00,0x20,0x31,0x69,0x00,0x20,0x29,0x5A,0x00,0xE7,0xF0,
                0xFD,0x00,0xD0,0xE2,0xFA,0x00,0xBC,0xD4,0xF8,0x00,0xA5,0xC8,0xF5,0x00,0x91,0xBC,
                0xF5,0x00,0x82,0xB2,0xF5,0x00,0x7B,0xA2,0xE5,0x00,0x6F,0x94,0xD4,0x00,0x66,0x86,
                0xC2,0x00,0x5E,0x77,0xAE,0x00,0x55,0x67,0x9B,0x00,0x45,0x55,0x85,0x00,0x3A,0x46,
                0x6F,0x00,0x30,0x36,0x5E,0x00,0x8A,0xED,0x8A,0x00,0x30,0xB5,0x2E,0x00,0x00,0xBF,
                0x00,0x00,0x70,0x77,0x77,0x00,0x00,0x91,0x00,0x00,0xA6,0xAC,0xAC,0x00,0x3D,0x3A,
                0x3D,0x00,0x5D,0x77,0x8C,0x00,0x40,0x4B,0x54,0x00,0x87,0x94,0xAD,0x00,0x4B,0x5E,
                0x6F,0x00,0x24,0x20,0x24,0x00,0x64,0x61,0x64,0x00,0x92,0x98,0x91,0x00,0x9C,0xA5,
                0x9A,0x00,0xA9,0xB2,0xA8,0x00,0xB1,0xBF,0xB2,0x00,0x85,0x8D,0x7E,0x00,0x8C,0x94,
                0x85,0x00,0x57,0xEA,0x56,0x00,0x36,0xDF,0x58,0x00,0x29,0xCB,0x5A,0x00,0x30,0xB3,
                0x56,0x00,0x36,0x98,0x4B,0x00,0xFE,0xFC,0xFE,0x00,0xE1,0xF2,0xF4,0x00,0xA8,0xCE,
                0xDF,0x00,0x77,0x9F,0xB5,0x00,0x64,0x7F,0x8E,0x00,0x48,0x67,0x78,0x00,0x3A,0x4B,
                0x58,0x00,0x22,0x29,0x36,0x00,0x0B,0x00,0x0B,0x00,0x36,0xA2,0x4B,0x00,0x00,0x91,
                0x00,0x00,0x11,0x95,0x1A,0x00,0x24,0x9B,0x36,0x00,0x91,0x8C,0x8C,0x00,0x9F,0x8A,
                0x87,0x00,0xAC,0x8D,0x7B,0x00,0xBC,0xAF,0x00,0x00,0xFD,0xCE,0x8D,0x00,0x66,0x64,
                0xE2,0x00,0x5B,0x58,0xC9,0x00,0x26,0x51,0xAB,0x00,0x36,0x92,0xF9,0x00,0x31,0x75,
                0xF3,0x00,0x3C,0x3C,0x8B,0x00,0x4F,0x4F,0xB2,0x00,0x3C,0x3C,0x8B,0x00,0x2B,0x2B,
                0x60,0x00,0x2B,0x2B,0x60,0x00,0x4B,0x5E,0x6F,0x00,0x45,0x5A,0x68,0x00,0x45,0x55,
                0x61,0x00,0x41,0x50,0x58,0x00,0x40,0x4B,0x54,0x00,0x4B,0x62,0x77,0x00,0x53,0x54,
                0xBD,0x00,0x0B,0x00,0x0B,0x00
            };
        #endregion
    }

    // broken

    /* public class FOnlineBag
    {
        enum FOnlineBagItemLocation
        {
            Inventory,
            ActiveHand,
            SecondHand,
            Armor
        }

        public readonly int Id;
        public List<Tuple<string, int, int, FOnlineBagItemLocation>> Items { get { return (items); } }
        public List<Tuple<string, int, int, FOnlineBagItemLocation>> items = new List<Tuple<string, int, int, FOnlineBagItemLocation>>();

        private FOnlineBag( int id )
        {
            Id = id;
        }

        private void AddItem( string pid, int min, int max = 0, FOnlineBagItemLocation location = FOnlineBagItemLocation.Inventory )
        {
            int tmp = -1;
            if( pid == null || pid.Length == 0 ||
                (int.TryParse( pid, out tmp ) && tmp <= 0) )
                throw new ArgumentOutOfRangeException( "pid" );

            if( min <= 0 )
                throw new ArgumentOutOfRangeException( "min <= 0" );

            if( max <= 0 )
                max = min;
            else if( max < min )
                throw new ArgumentException( "max < min" );

            items.Add( new Tuple<string, int, int, FOnlineBagItemLocation>( pid, min, max, location ) );
        }

        private void AddItem( string line )
        {
            FOnlineBagItemLocation location = FOnlineBagItemLocation.Inventory;

            string[] item = line.Split( new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries );
            if( item.Length >= 1 )
            {
                string[] pid_location = item[0].Split( new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries );
                if( pid_location.Length > 1 )
                {
                    if( pid_location[1].ToLower() == "m" )
                        location = FOnlineBagItemLocation.ActiveHand;
                    else if( pid_location[1].ToLower() == "e" )
                        location = FOnlineBagItemLocation.SecondHand;
                    else if( pid_location[1].ToLower() == "a" )
                        location = FOnlineBagItemLocation.Armor;
                    else
                        throw new InvalidDataException( "location" );
                }
            }
            else
            {
                throw new Exception( "item length > 2" );
            }

            if( item.Length == 2 )
            {
            }
        }

        public static List<FOnlineBag> LoadList( string filename )
        {
            if( File.Exists( filename ) )
            {
                string[] lines = File.ReadAllLines( filename );

                Dictionary<string, string> bags = new Dictionary<string, string>();
                Dictionary<string, string> defines = new Dictionary<string, string>();

                Match match = null;
                string reAlias = "^(.*?)[\t ]*=[\t ]*(.*?)$";
                string reBag = "^[\t ]*bag_([0-9]+)$";
                foreach( string line in lines )
                {
                    if( line.Length == 0 || Regex.Match( line, "^[\t ]*#" ).Success )
                        continue;

                    match = Regex.Match( line, reAlias );
                    if( match.Success )
                    {
                        string bag = match.Groups[1].Value;
                        string def = match.Groups[2].Value;

                        match = Regex.Match( bag, reBag );
                        if( match.Success )
                        {
                            if( !bags.ContainsKey( bag ) )
                                bags.Add( bag, def );
                            else
                                bags[bag] = def;
                        }
                        else
                        {
                            if( !defines.ContainsKey( bag ) )
                                defines.Add( bag, def );
                            else
                                defines[bag] = def;
                        }
                    }
                }
                List<FOnlineBag> result = new List<FOnlineBag>();

                string reBagId = "^bag_([0-9]+)$";
                string tmp;
                foreach( KeyValuePair<string, string> bagLine in bags )
                {
                    match = Regex.Match( bagLine.Key, reBagId );
                    if( match.Success )
                    {
                        int bagId = -1;
                        if( int.TryParse( match.Groups[1].Value, out bagId ) )
                        {
                            FOnlineBag bag =  new FOnlineBag( bagId );
                            foreach( string item_define in bagLine.Value.Split( new char[]{' '}, StringSplitOptions.RemoveEmptyEntries ) )
                            {
                                if( defines.ContainsKey( item_define ) &&
                                    defines.TryGetValue( item_define, out tmp )
                                {
                                    foreach( string item in tmp.Split( new char[]{' '} ))
                                    {

                                    }
                                }
                                else
                                {
                                    bag.AddItem( item_define );
                                }
                            }
                        }
                    }
                }

                return (result);
            }
            else
                return (null);
        }
    } */

    public class FOnlineMap
    {
        // TODO: Header, Tiles, Roofs, Object - make *truly* read only
        // ?:
        // <@Atom> class Objects : List<Dictionary<string, string>>
        // <@Atom> {
        // <@Atom> public override string this[string key]

        /// <summary>
        /// Map filename, without path.
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// Map header informations.
        /// </summary>
        public Dictionary<string, string> Header { get { return (header); } }
        private Dictionary<string, string> header = new Dictionary<string, string>();

        /// <summary>
        /// Tiles used on map.
        /// </summary>
        public List<Tuple<int, int, string>> Tiles { get { return (tiles); } }
        private List<Tuple<int, int, string>> tiles = new List<Tuple<int, int, string>>();

        /// <summary>
        /// Roofs used on map.
        /// </summary>
        public List<Tuple<int, int, string>> Roofs { get { return (roofs); } }
        private List<Tuple<int, int, string>> roofs = new List<Tuple<int, int, string>>();

        /// <summary>
        /// List of object on map.
        /// </summary>
        public List<Dictionary<string, string>> Objects { get { return (objects); } }
        private List<Dictionary<string, string>> objects = new List<Dictionary<string, string>>();

        /// <summary>
        /// [internal]
        /// Create map object
        /// </summary>
        /// <param name="filename">map filename</param>
        private FOnlineMap( string filename )
        {
            FileName = filename;
        }

        /// <summary>
        /// Read and parse .fomap file
        /// </summary>
        /// <param name="filename">path to file</param>
        /// <param name="readHeader">if false, header Data won't be saved</param>
        /// <param name="readTiles">if false, tiles list won't be saved</param>
        /// <param name="readObjects">if false, mapObject list won't be saved</param>
        /// <returns>Newly loaded map, null if file don't exists</returns>
        public static FOnlineMap Load( string filename, bool readHeader = false, bool readTiles = false, bool readObjects = false )
        {
            if( File.Exists( filename ) )
            {
                string[] lines = File.ReadAllLines( filename );

                FOnlineMap fomap = new FOnlineMap( filename );

                bool inHeader = false, inTiles = false, inObjects = false;
                string reVarVal = "^(.*?)[\t ]+(.*?)$";
                string reTile = "^(tile|roof)[\t ]+([0-9]+)[\t ]+([0-9]+)[\t ]+(.*?)$";
                Match match = null;
                foreach( string line in lines )
                {
                    if( line.Length == 0 )
                        continue;

                    // TODO: make it regexp
                    if( line == "[Header]" )
                    {
                        inHeader = true;
                        inTiles = false;
                        inObjects = false;
                        continue;
                    }
                    else if( line == "[Tiles]" )
                    {
                        inHeader = false;
                        inTiles = true;
                        inObjects = false;
                        continue;
                    }
                    else if( line == "[Objects]" )
                    {
                        inHeader = false;
                        inTiles = false;
                        inObjects = true;
                        continue;
                    }

                    if( inHeader && readHeader )
                    {
                        match = Regex.Match( line, reVarVal );
                        if( match.Success )
                        {
                            fomap.header.Add( match.Groups[1].Value, match.Groups[2].Value );
                        }
                    }
                    else if( inTiles && readTiles )
                    {
                        match = Regex.Match( line, reTile );
                        if( match.Success )
                        {
                            int x, y;
                            if( int.TryParse( match.Groups[2].Value, out x ) &&
                                int.TryParse( match.Groups[3].Value, out y ) )
                            {
                                if( match.Groups[1].Value == "tile" )
                                {
                                    fomap.tiles.Add( new Tuple<int, int, string>( x, y, match.Groups[4].Value ) );
                                }
                                else if( match.Groups[1].Value == "roof" )
                                {
                                    fomap.roofs.Add( new Tuple<int, int, string>( x, y, match.Groups[4].Value ) );
                                }
                            }
                        }
                    }
                    else if( inObjects && readObjects )
                    {
                        match = Regex.Match( line, reVarVal );

                        if( match.Success )
                        {
                            if( fomap.objects.Count == 0 || match.Groups[1].Value.ToUpper() == "MAPOBJTYPE" )
                            {
                                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                                dictionary.Add( match.Groups[1].Value, match.Groups[2].Value );
                                fomap.objects.Add( dictionary );
                            }
                            else
                            {
                                fomap.objects[fomap.objects.Count - 1].Add( match.Groups[1].Value, match.Groups[2].Value );
                            }
                        }
                    }
                }

                return (fomap);
            }
            else
                return (null);
        }

        public static FOnlineMap LoadHeader( string filename )
        {
            return (Load( filename, true ));
        }

        public static FOnlineMap LoadTiles( string filename )
        {
            return (Load( filename, false, true ));
        }

        public static FOnlineMap LoadObjects( string filename )
        {
            return (Load( filename, false, false, true ));
        }
    }
}
