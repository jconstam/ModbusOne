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

		private int ROW_HEIGHT = 20;
		private int COL_WIDTH = 43;

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

			for ( int i = 1; i < mainTable.RowCount; i++ )
			{
				Label rowLabel = new Label( );
				rowLabel.Text = string.Format( "{0:00000}", baseAddress + i );
				rowLabel.Dock = DockStyle.Fill;
				rowLabel.TextAlign = ContentAlignment.MiddleCenter;

				mainTable.Controls.Add( rowLabel, 0, i );
			}

			this.Size = new Size( ( COL_COUNT * ( COL_WIDTH + 1 ) ) + 1, ( mainTable.RowCount * ( ROW_HEIGHT + 1 ) ) + 1 );

			this.Controls.Add( mainTable );
		}
	}
}
