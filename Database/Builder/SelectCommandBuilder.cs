using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debyte.Database.Builder {
	public class SelectCommandBuilder : ICommandBuilder {

		public string BuildCommand(params string[] p) {
			try {
				switch (p[0]) {
					case "DebitoConContatto":
						return DebitoConContatto(p[1], p[2]); //UTENTE, CONTATTO
					case "ContoUtente":
						return ContoUtente(p[1]); //UTENTE
					case "GruppiUtente":
						return GruppiUtente(p[1]); //UTENTE
					case "ContattiInGruppo":
						return ContattiInGruppo(p[1], p[2]); //UTENTE, GRUPPO
					case "DatiUtente":
						return DatiUtente(p[1]); //UTENTE
					case "UtentiConPP":
						return UtentiConPP(); //niente
					case "MaxID":
						return MaxID(p[1]); //TABLE
					case "UtentePerNome":
						return UtentePerNome(p[1]); //NOME
					case "TransazioniEliminate":
						return TransazioniEliminate(p[1]); //UTENTE
					default:
						throw new Exception("Parametri passati non validi!");
				}
			} //try
			catch (Exception e) {
				throw e;
			} //catch
		}

		private string TransazioniEliminate(string utente) {
			try {
				SQLStringChecker.CheckString(utente);
			}
			catch (Exception e) {
				throw e;
			} //Controllo argomenti
			string result = "";
			result += ("SELECT * ");
			result += ("FROM Transazioni_Eliminate TM, Transazioni_Eliminate TD ");
			result += ("WHERE TM.Mittente = " + utente + " ");
			result += ("AND TM.Destinatario = " + utente + " ");
			return result;
		}

		private string UtentePerNome(string nome) {
			try {
				SQLStringChecker.CheckString(nome);
			}
			catch (Exception e) {
				throw e;
			} //Controllo argomenti
			string result = "";
			result += ("SELECT * ");
			result += ("FROM Utenti ");
			result += ("WHERE Nome LIKE " + nome + " ");
			return result;
		}

		//Recupero ID Massimo (Utenti o Gruppi)
		private string MaxID(string table) {
		//Non necessita controlli perchè 'table' è scelto da un insieme finito di possibilita'
			string result = "";
			result += ("SELECT MAX(ID) ");
			result += ("FROM "+table+" ");
			return result;
		}

		//Recupero dati utenti con riferimento PayPal
		private string UtentiConPP() {
			string result = "";
			result += ("SELECT * ");
			result += ("FROM Utenti ");
			result += ("WHERE PP_Riferimento IS NOT NULL ");
			return result;
		}

		//Recupera dati utente
		private string DatiUtente(string utente) {
			try {
				SQLStringChecker.CheckString(utente);
			}
			catch (Exception e) {
				throw e;
			} //Controllo argomenti
			string result = "";
			result += ("SELECT Utente, Nome ");
			result += ("FROM Utenti ");
			result += ("WHERE Utente = " + utente + " ");
			return result;
		}

		//Restituisce i contatti di un gruppo richiesto dall'utente
		private string ContattiInGruppo(string utente, string gruppo_id) {
			try {
				SQLStringChecker.CheckString(utente);
				SQLStringChecker.CheckString(gruppo_id);
			} catch (Exception e) {
				throw e;
			} //Controllo argomenti
			string result = "";
			result += ("SELECT Utente ");
			result += ("FROM GruppiUtenti ");
			result += ("WHERE ID = "+ gruppo_id +" ");
			result += ("AND Utente <> " + utente + " ");
			return result;
		}

		//Restituisce i gruppi a cui appartiene/e' invitato l'utente
		private string GruppiUtente(string utente) {
			try {
				SQLStringChecker.CheckString(utente);
			}
			catch (Exception e) {
				throw e;
			} //Controllo argomenti
			string result = "";
			result += ("SELECT * ");
			result += ("FROM Gruppi ");
			result += ("WHERE ID IN ( SELECT ID ");
			result += ("FROM GruppiUtenti ");
			result += ("WHERE Utente = " + utente +" )");
			return result;
		}

		//Restituisce la query per il conto dell'utente
		private string ContoUtente(string utente) {
			try {
				SQLStringChecker.CheckString(utente);
			}
			catch (Exception e) {
				throw e;
			} //Controllo argomenti
			string result = "";
			result += ("SELECT * ");
			result += ("FROM Transazioni TM, Transazioni TD ");
			result += ("WHERE TM.Mittente = " + utente + " ");
			result += ("AND TM.Destinatario = "+ utente + " ");
			return result;
		}

		//Restituisce l'intero debito che un utente ha con un contatto
		private string DebitoConContatto(string utente, string contatto) {
			try {
				SQLStringChecker.CheckString(utente);
				SQLStringChecker.CheckString(contatto);
			}
			catch (Exception e) {
				throw e;
			} //Controllo argomenti
			string result = "";
			result += ("SELECT * ");
			result += ("FROM Transazioni TM, Transazioni TD ");
			result += ("WHERE TM.Mittente = "+utente+" ");
			result += ("AND TM.Destinatario = " +contatto + " ");
			result += ("AND TD.Mittente = "+ contatto + " ");
			result += ("AND TD.Destinatario = " + utente + " ");
			return result;
		}
	}
}
