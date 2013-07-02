using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Overseer
{
    static class Overseer
    {
        public static Version Version = new Version( Application.ProductVersion );
        public static Config Config = null;
        private static frmMain formMain = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );

            Directory.SetCurrentDirectory( Path.GetDirectoryName( Application.ExecutablePath ) );
            Config = new Config( Application.ProductName );

            if( Config.IsFirstTime() )
            {
                Config.SetDefaultValues( false );
                Config.Save();
            }
            Config.Load();

            formMain = new frmMain();

            // !! form in closed constructor in case of errors
            if( formMain != null && !formMain.IsDisposed )
                Application.Run( formMain );
        }
    }

    // yay
    public static class Extensions
    {
        public enum BackgroundWorker : int
        {
            SendProgressMax = -2,
            SendProgressText = -1,
            SendResult = 0,
            SendProgress = 1
        }

        public static void SendProgressMax( this System.ComponentModel.BackgroundWorker worker, int max )
        {
            worker.ReportProgress( (int)BackgroundWorker.SendProgressMax, (int)max );
        }

        public static void SendProgressText( this System.ComponentModel.BackgroundWorker worker, string text )
        {
            worker.ReportProgress( (int)BackgroundWorker.SendProgressText, (string)text );
        }

        public static void SendResult( this System.ComponentModel.BackgroundWorker worker, string data )
        {
            worker.ReportProgress( (int)BackgroundWorker.SendResult, (string)data );
        }

        public static void SendProgress( this System.ComponentModel.BackgroundWorker worker, int count = 1 )
        {
            worker.ReportProgress( (int)BackgroundWorker.SendProgress, (int)count );
        }

        public static TKey FindKeyByValue<TKey, TValue>( this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TValue value )
        {
            if( dictionary == null )
                throw new ArgumentNullException( "dictionary" );

            foreach( KeyValuePair<TKey, TValue> pair in dictionary )
            {
                if( value.Equals( pair.Value ) )
                    return (pair.Key);
            }

            return (default( TKey ));
        }

        public static bool HaveExtension( this string filename, string extension )
        {
            if( filename == null || filename.Length == 0 )
                return (false);

            int index = filename.LastIndexOf( '.' );
            if( index <= 0 || filename.Remove( 0, index ).ToLower() != ("." + extension).ToLower() )
                return (false);

            return (true);
        }

        public static bool IsWantedObject( this List<Tuple<int,int>> list, int type, int pid )
        {
            if( list.Count == 0 )
                return (false);

            foreach( Tuple<int, int> element in list )
            {
                if( (element.Item1 < 0 || element.Item1 == type) &&
                    element.Item2 == pid )
                    return (true);
            }

            return (false);
        }
    }
}
