using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ModbusOne.DataDisplayControls;

namespace ModbusOne
{
	public partial class ModbusReadDisplay : UserControl
	{
		private int COL_COUNT = 2;

		private int ROW_HEIGHT = 17;
		private int LABEL_COL_WIDTH = 42;
		private int DATA_COL_WIDTH = 86;

		private ushort baseAddress;
		private TableLayoutPanel mainTable;
		private ushort[ ] data;

		public ushort[ ] Data
		{
			get
			{
				return data;
			}
			set
			{
				if ( value.Length != data.Length )
				{
					if ( value.Length == 0 )
					{
						return;
					}
					else
					{
						throw new Exception( string.Format( "Invalid length. Expected {0}, got {1}.", data.Length, value.Length ) );
					}
				}

				Array.Copy( value, data, value.Length);

				SetValues( );
			}
		}

		public ModbusReadDisplay( ) : this( 205, 20 )
		{
		}

		public ModbusReadDisplay( ushort baseAddress, ushort numRegisters )
		{
			InitializeComponent( );

			this.baseAddress = baseAddress;
			SetupControls( baseAddress, numRegisters );

			data = new ushort[ numRegisters ];
		}

		private void SetValues( )
		{
			foreach ( Control control in mainTable.Controls )
			{
				if ( control is DataDisplayControlBase )
				{
					( ( DataDisplayControlBase ) control ).SetValue( data );
				}
			}
		}

		private void SetupControls( ushort baseAddress, ushort numRegisters )
		{
			mainTable = new TableLayoutPanel( );

			mainTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			mainTable.AutoSize = true;

			mainTable.Dock = DockStyle.Fill;
			mainTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

			mainTable.ColumnCount = COL_COUNT;
			mainTable.ColumnStyles.Add( new ColumnStyle( SizeType.Absolute, LABEL_COL_WIDTH ) );
			for ( int column = 1; column < mainTable.ColumnCount; column++ )
			{
				mainTable.ColumnStyles.Add( new ColumnStyle( SizeType.Absolute, DATA_COL_WIDTH ) );
			}

			mainTable.RowCount = numRegisters + 1;
			for ( int row = 0; row < mainTable.RowCount; row++ )
			{
				mainTable.RowStyles.Add( new RowStyle( SizeType.Absolute, ROW_HEIGHT ) );
			}

			mainTable.Controls.Add( MakeLabel( "AddressHeadingLabel", "Addr" ), 0, 0 );
			mainTable.Controls.Add( MakeLabel( "DecHeadingLabel", "Data" ), 1, 0 );

			for ( int i = 1; i < mainTable.RowCount; i++ )
			{
				ushort address = ( ushort ) ( baseAddress + i - 1 );

				mainTable.Controls.Add( MakeLabel( string.Format( "Address{0}Label", address ), string.Format( "{0}", address ) ), 0, i );
				mainTable.Controls.Add( new DecHexDataDisplayControl( string.Format( "Data{0}", i - 1 ), i - 1, DATA_COL_WIDTH, ROW_HEIGHT ) );
			}

			this.Size = new Size( ( LABEL_COL_WIDTH + 1 ) + ( DATA_COL_WIDTH + 1 ) + 1, ( mainTable.RowCount * ( ROW_HEIGHT + 1 ) ) + 1 );

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
	}
}
