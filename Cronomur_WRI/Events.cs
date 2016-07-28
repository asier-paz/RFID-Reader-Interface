using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cronomur_WRI
{
	public class Events
	{
		private ListBox list;

		public Events(ListBox list)
		{
			this.list = list;
		}

		public void add(string msg)
		{
			ListBoxItem itm = new ListBoxItem();
			itm.Content = "[" + System.DateTime.Now.ToString("HH:mm:ss.fff") + "] " + msg;
			list.Items.Add(itm);

			list.SelectedIndex = list.Items.Count - 1;
			list.ScrollIntoView(list.SelectedItem);
		}

		public ListBox getListBox() { return list; }
	}
}
