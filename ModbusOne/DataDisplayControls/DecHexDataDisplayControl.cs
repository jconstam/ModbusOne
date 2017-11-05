using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusOne.DataDisplayControls
{
	public partial class DecHexDataDisplayControl : DataDisplayControlBase
	{
		private static int BOX_HEIGHT = 17;
		private static int BOX_WIDTH = 84;

		private TextBox dataBox;

		public DecHexDataDisplayControl( ) : this( "DecHexDataDisplayControl", 0, BOX_WIDTH, BOX_HEIGHT )
        {
		}

		public DecHexDataDisplayControl( string name, int index, int width, int height ) : base( name, index, width, height )
		{
			Name = name;
			dataBox = MakeTextBox( );

            this.Controls.Add( dataBox );
		}

		public override void SetValue( ushort[ ] value )
		{
			dataBox.Text = string.Format( "{0:D5} ({0:X4})", value[ Index ] );
        }

		private TextBox MakeTextBox( )
		{
			TextBox box = new TextBox( );
			
			box.ReadOnly = true;
			box.Enabled = false;
			box.Dock = DockStyle.Fill;
			box.Size = new Size( BOX_WIDTH, BOX_HEIGHT );
			box.TextAlign = HorizontalAlignment.Center;
			box.Margin = new Padding( 0 );
			box.Font = new Font( FontFamily.GenericMonospace, 8 );
			box.Text = "00000 (0000)";
			box.BorderStyle = BorderStyle.None;

			return box;
		}
	}
}
