using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Commons
{
	public class GeneralLocalizableEntity
	{
		public string Localize(string textAr, string textEN)
		{
			CultureInfo CultureInfo = Thread.CurrentThread.CurrentCulture;
			if (CultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
				return textAr;
			return textEN;
		}
	}
}
