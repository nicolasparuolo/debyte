using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debyte.Model {
	public class TransazioneSaldo : Transazione {

		private readonly string _tipo = "SA";

		protected TransazioneSaldo(int id, Utente mittente, Utente destinatario, decimal importo, Valuta valuta, string descrizione, DateTime timestamp, char stato)
		: base(id, mittente, destinatario, importo, valuta, descrizione, timestamp, stato) {
		}

		public override String ToString() {
			return base.ToString();
		}
	}
}
