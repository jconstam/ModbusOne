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
	public partial class DataDisplayControlBase : UserControl
	{
		public int Index
		{
			get;
			set;
		}

		public DataDisplayControlBase( string name, int index, int width, int height )
		{
			InitializeComponent( );

			Name = name;
			Index = index;
			Size = new Size( width, height );
			Padding = new Padding( 0 );
			Margin = new Padding( 0 );
			Dock = DockStyle.Fill;
		}

		public virtual void SetValue( ushort[] value )
		{
			throw new NotImplementedException( "SetValue not implemented in base class" );
		}
	}
}
