// Pent 2.0. 
//  Created by EL HIRACH ABDERRAZZAK on 2024-02-14. 
//  Copyright © 2024 aelhirach.me.  All rights reserved.

using System;
using System.Windows.Forms;

namespace Pente
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
