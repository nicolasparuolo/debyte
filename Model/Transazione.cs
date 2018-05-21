using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debyte.Model {
	public abstract class Transazione {

		private Int32 _id;
		private Utente _mittente;
		private Utente _destinatario;
		private Decimal _importo;
		private Valuta _valuta;
		private String _descrizione;
		private DateTime _timestamp;
		private char _stato;

		protected Transazione(int id, Utente mittente, Utente destinatario, decimal importo, Valuta valuta, string descrizione, DateTime timestamp, char stato) {
			Id = id;
			Mittente = mittente;
			Destinatario = destinatario;
			Importo = importo;
			Valuta = valuta;
			Descrizione = descrizione;
			Timestamp = timestamp;
			Stato = stato;
		}

		public int Id { get => _id; set => _id = value; }
		public decimal Importo { get => _importo; set => _importo = value; }
		public string Descrizione { get => _descrizione; set => _descrizione = value; }
		public DateTime Timestamp { get => _timestamp; set => _timestamp = value; }
		public char Stato { get => _stato; set => _stato = value; }
		internal Utente Mittente { get => _mittente; set => _mittente = value; }
		internal Utente Destinatario { get => _destinatario; set => _destinatario = value; }
		internal Valuta Valuta { get => _valuta; set => _valuta = value; }

		public override String ToString() {
			return base.ToString();
		}

		public override bool Equals(object obj) {
			bool result;
			Transazione temp;
			try {
				temp = (Transazione)obj;
			} catch (Exception e) {
				throw e;
			}
			result = this.Id.Equals(temp.Id);
			return result;		
		}
	}
}
