using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
	/// <summary>
	/// Interaction logic for TimeEditor.xaml
	/// </summary>
	public partial class TimeEditor : UserControl
	{
		public TimeEditor()
		{
			InitializeComponent();
		}

		public DateTime Value
		{
			get { return (DateTime)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(DateTime), typeof(TimeEditor), new UIPropertyMetadata(DateTime.Now, new PropertyChangedCallback(OnValueChanged)));

		private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			TimeEditor control = obj as TimeEditor;
			control.Years = ((DateTime)e.NewValue).Year;
			control.Months = ((DateTime)e.NewValue).Month;
			control.Days = ((DateTime)e.NewValue).Day;
			control.Hours = ((DateTime)e.NewValue).Hour;
			control.Minutes = ((DateTime)e.NewValue).Minute;
		}

		public static readonly DependencyProperty YearsProperty = DependencyProperty.Register("Years", typeof(int), typeof(TimeEditor), new UIPropertyMetadata(DateTime.Now.Year, new PropertyChangedCallback(OnTimeChanged)));

		public int Years
		{
			get { return (int)GetValue(YearsProperty); }
			set { SetValue(YearsProperty, value); }
		}

		public static readonly DependencyProperty MonthsProperty = DependencyProperty.Register("Months", typeof(int), typeof(TimeEditor), new UIPropertyMetadata(DateTime.Now.Month, new PropertyChangedCallback(OnTimeChanged)));

		public int Months
		{
			get { return (int)GetValue(MonthsProperty); }
			set { SetValue(MonthsProperty, value); }
		}

		public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days", typeof(int), typeof(TimeEditor), new UIPropertyMetadata(DateTime.Now.Day, new PropertyChangedCallback(OnTimeChanged)));

		public int Days
		{
			get { return (int)GetValue(DaysProperty); }
			set { SetValue(DaysProperty, value); }
		}

		public int Hours
		{
			get { return (int)GetValue(HoursProperty); }
			set { SetValue(HoursProperty, value); }
		}

		public static readonly DependencyProperty HoursProperty = DependencyProperty.Register("Hours", typeof(int), typeof(TimeEditor), new UIPropertyMetadata(DateTime.Now.Hour, new PropertyChangedCallback(OnTimeChanged)));

		public int Minutes
		{
			get { return (int)GetValue(MinutesProperty); }
			set { SetValue(MinutesProperty, value); }
		}

		public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register("Minutes", typeof(int), typeof(TimeEditor), new UIPropertyMetadata(DateTime.Now.Minute, new PropertyChangedCallback(OnTimeChanged)));

		private static void OnTimeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			TimeEditor control = obj as TimeEditor;
			if (control.Hours > 23)
			{
				control.Hours = 23;
			}
			if (control.Minutes > 59)
			{
				control.Minutes = 59;
			}
			control.Value = new DateTime(control.Years, control.Months, control.Days, control.Hours, control.Minutes, 0);
		}

		private void Down(object sender, KeyEventArgs args)
		{
			switch (((Grid)sender).Name)
			{
				case "min":
					if (args.Key == Key.Up)
					{
						this.Minutes++;
						args.Handled = true;
					}
					if (args.Key == Key.Down)
					{
						this.Minutes--;
						args.Handled = true;
					}
					break;
				case "hour":
					if (args.Key == Key.Up)
					{
						this.Hours++;
						args.Handled = true;
					}
					if (args.Key == Key.Down)
					{
						this.Hours--;
						args.Handled = true;
					}
					break;
			}
		}
	}
}


