using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TempGraphic
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            this.CreateImage( "" );
        }

        void CreateImage( string text )
        {
            string[] lines = text.Split( new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries );
            if( lines.Length > 0 && lines.Length < 4 )
            {
                Font font = new Font( "Tahoma", 8, FontStyle.Bold );
                Bitmap bmp = new Bitmap( 1, 1 );
                int w = 0;
                using( Graphics g = Graphics.FromImage( bmp ) )
                {
                    
                    foreach( string line in lines )
                    {
                        SizeF tmpsize = g.MeasureString( line, font, 90 );
                        if( tmpsize.Width > w )
                            w = (int)tmpsize.Width;
                    }
                }
                bmp = new Bitmap( w+10, 25 + ((lines.Length - 1) * (lines.Length > 1 ? 15 : 0)) );
                using( Graphics g = Graphics.FromImage( bmp ))
                {
                    for( int x = 1; x < bmp.Width; x++ )
                    {
                        for( int y = 1; y < bmp.Height; y++ )
                        {
                            if( (x == 1 || y == 1) || (x == bmp.Width - 1 || y == bmp.Height - 1) )
                                bmp.SetPixel( x, y, Color.White );
                            else
                                bmp.SetPixel( x, y, Color.FromArgb( 104, 36, 96 ) );
                        }
                    }
                    int l = 0;
                    foreach( string line in lines )
                    {
                        g.DrawString( line, font, Brushes.White, 5, 5 + (l * 15) );
                        l++;
                    }

                }
                this.pictureBox1.Image = (Image)bmp;
                this.button1.Enabled = true;
            }
            else
            {
                this.pictureBox1.Image = null;
                this.button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged( object sender, EventArgs e )
        {
            TextBox self = (TextBox)sender;

            this.CreateImage( self.Text );
        }

        private void button1_Click( object sender, EventArgs e )
        {
            if( this.pictureBox1 != null )
            {
                DialogResult result = this.saveFileDialog1.ShowDialog();

                if( result == DialogResult.OK )
                {
                    this.pictureBox1.Image.Save( this.saveFileDialog1.FileName, ImageFormat.Png );
                }
            }
        }
    }
}
