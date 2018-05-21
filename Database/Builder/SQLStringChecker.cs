using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debyte.Database.Builder {
	public static class SQLStringChecker {
		public static void CheckString(string s) {
			
			//Controllo generico per Bob Tables
			if (s.Contains("SELECT")
				|| s.Contains("UPDATE")
				|| s.Contains("INSERT")
				|| s.Contains("DELETE")
				|| s.Contains("ALTER")
				|| s.Contains("CREATE")
				|| s.Contains("DROP")
				|| s.Contains("(")
				|| s.Contains(")")
				|| s.Contains(";")
				|| s.Contains("*")) {

				throw new Exception("Stringa" + s + "non valida! Potenziale Bob Tables!");
			}

			//Controllo argomenti di tipo stringa
			//Se non è un intero, allora si entra nel catch
			try {
				int i = Int32.Parse(s);
			}
			//Non è un intero
			catch (FormatException e) {

				//Per essere valido nella query la stringa deve essere racchiusa tra apici		
				if (!s.StartsWith("'") || !s.EndsWith("'"))
					throw new Exception("Stringa" + s + "non valida!Deve essere racchiusa tra apici poichè non è un intero!");

			}
		}

	}
}
