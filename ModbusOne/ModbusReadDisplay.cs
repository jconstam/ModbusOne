using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusOne
{
	public partial class ModbusReadDisplay : UserControl
	{
		private int COL_COUNT = 3;

		private int ROW_HEIGHT = 15;
		private int COL_WIDTH = 42;

		public ModbusReadDisplay( ) : this( 205, 20 )
		{
		}

		public ModbusReadDisplay( ushort baseAddress, ushort numRegisters )
		{
			InitializeComponent( );

			TableLayoutPanel mainTable = new TableLayoutPanel( );

			mainTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			mainTable.AutoSize = true;

			mainTable.Dock = DockStyle.Fill;
			mainTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

			mainTable.ColumnCount = COL_COUNT;
			for ( int column = 0; column < mainTable.ColumnCount; column++ )
			{
				mainTable.ColumnStyles.Add( new ColumnStyle( SizeType.Absolute, COL_WIDTH ) );
			}

			mainTable.RowCount = numRegisters + 1;
			for ( int row = 0; row < mainTable.RowCount; row++ )
			{
				mainTable.RowStyles.Add( new RowStyle( SizeType.Absolute, ROW_HEIGHT ) );
			}

			mainTable.Controls.Add( MakeLabel( "AddressHeadingLabel", "Addr" ), 0, 0 );
			mainTable.Controls.Add( MakeLabel( "DecHeadingLabel", "Dec" ), 1, 0 );
			mainTable.Controls.Add( MakeLabel( "HexHeadingLabel", "Hex" ), 2, 0 );

			for ( int i = 1; i < mainTable.RowCount; i++ )
			{
				int address = baseAddress + i - 1;

				mainTable.Controls.Add( MakeLabel( string.Format( "Address{0}Label", address ), string.Format( "{0}", address ) ), 0, i );
				mainTable.Controls.Add( MakeTextBox( string.Format( "Dec{0}Textbox", address ), true ), 1, i );
				mainTable.Controls.Add( MakeTextBox( string.Format( "Hex{0}Textbox", address ), true ), 2, i );
			}

			this.Size = new Size( ( COL_COUNT * ( COL_WIDTH + 1 ) ) + 1, ( mainTable.RowCount * ( ROW_HEIGHT + 1 ) ) + 1 );

			this.Controls.Add( mainTable );
		}

		private Label MakeLabel( string name, string text )
		{
			Label label = new Label( );

			label.Name = name;
			label.Text = text;
			label.Dock = DockStyle.Fill;
			label.TextAlign = ContentAlignment.MiddleCenter;
			label.Margin = new Padding( 0 );
			label.Padding = new Padding( 0 );
			label.Font = new Font( FontFamily.GenericMonospace, 8 );

			return label;
		}

		private TextBox MakeTextBox( string name, bool readOnly )
		{
			TextBox box = new TextBox( );

			box.Name = name;
			box.ReadOnly = readOnly;
			box.Enabled = !readOnly;
			box.Dock = DockStyle.Fill;
			box.TextAlign = HorizontalAlignment.Center;
			box.Margin = new Padding( 0 );
			box.Font = new Font( FontFamily.GenericMonospace, 8 );
			box.Text = "00000";
			box.BorderStyle = BorderStyle.None;

			return box;
		}
	}
}
