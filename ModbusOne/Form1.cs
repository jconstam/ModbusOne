using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusOne
{
	public partial class Form1 : Form
	{
		public Form1( )
		{
			InitializeComponent( );

			ModbusReadDisplay readDisplay1 = new ModbusReadDisplay( 1001, 5 );

			this.Controls.Add( readDisplay1 );

			readDisplay1.Data = new ushort[ ] { 5, 10, 15, 20, 25 };
		}
	}
}
