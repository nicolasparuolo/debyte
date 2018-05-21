using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;
using Debyte.Database.Types;

namespace Debyte.Database {
	class DatabaseManager{

		//Connessione
		private readonly DatabaseConnection databaseConnection = null;
		//Singleton
		private static DatabaseManager _instance = null;
		//Collezioni
		private static HashSet<DatabaseController> registered;
		private static Queue<DBRequest> queue;
		//Variabili

		protected DatabaseManager() {
			databaseConnection = new DatabaseConnection();
			queue = new Queue<DBRequest>();
			registered = new HashSet<DatabaseController>();
		}

		public static DatabaseManager GetInstance(DatabaseController databaseController) {
			if (_instance == null)
				_instance = new DatabaseManager();
			try {
				Register(databaseController);
			} catch (Exception e) { throw e; }
			return _instance;
		}

		public static void Register(DatabaseController toRegister) {
			try {
				registered.Add(toRegister);
			}
			catch (Exception e) {
				throw e;
			}
		}

		public void Unregister(DatabaseController toUnregister) {
			try {
				registered.Remove(toUnregister);
			}
			catch (Exception e) {
				throw e;
			}
		}

		public bool AskNonQuery(DBRequest request) {

			//Controllo
			if (!registered.Contains(request.Controller))
				throw new Exception("Il Controller non è registrato");
			if (queue.Contains(request))
				throw new Exception("La richiesta è già in corso");

			//Aggiunta alla coda
			queue.Enqueue(request);

			//Attesa
			bool first = queue.Peek().Equals(request);
			while (!first) {
				first = queue.Peek().Equals(request);
			}

			//Esecuzione
			bool result;
			try {
				result = ExecuteNonQuery(request);
			}
			catch (Exception e) {
				throw e;
			}
			//Comunque vada, la richiesta viene tolta dalla fila
			finally {
				queue.Dequeue();
			}
			return result;


		}

		private bool ExecuteNonQuery(DBRequest request) {

			//Controllo
			if (!registered.Contains(request.Controller))
				throw new Exception("Il Controller non è registrato all'inizio dell'esecuzione");

			//Inizio
			DB2Transaction transaction = databaseConnection.GetTransaction();
			DB2Command command = databaseConnection.GetCommand();
			command.CommandText = request.Command;
			command.Transaction = transaction;

			//Esecuzione
			try {
				command.ExecuteNonQuery();
			}
			catch (Exception e) {
				throw new Exception("Errore: " + e.Message);
			}

			//Controllo
			if (!registered.Contains(request.Controller)) {
				transaction.Rollback();
				throw new Exception("Il Controller non è registrato alla fine dell'esecuzione");
			}

			//Fine
			transaction.Commit();
			return true;
		}

		public DBReaderController AskQuery(DBRequest request)  {

			//Controllo
			if (!registered.Contains(request.Controller))
				throw new Exception("Il Controller non è registrato");
			if (queue.Contains(request))
				throw new Exception("La richiesta è già in corso");

			//Aggiunta alla coda
			queue.Enqueue(request);

			//Attesa
			bool first = queue.Peek().Equals(request);
			while(!first) {
				first = queue.Peek().Equals(request);
			}

			//Esecuzione
			DBReaderController reader;
			try {
				reader = ExecuteQuery(request);
			}
			catch (Exception e) {
				throw e;
			}
			//Comunque vada, la richiesta viene tolta dalla fila
			finally {
				queue.Dequeue();
			}
			return reader;

		}

		private DBReaderController ExecuteQuery(DBRequest request) {

			//Controllo
			if (!registered.Contains(request.Controller))
				throw new Exception("Il Controller non è registrato all'inizio dell'esecuzione");

			//Inizio
			DBReaderController result;
			DB2DataReader reader;
			DB2Transaction transaction = databaseConnection.GetTransaction();
			DB2Command command = databaseConnection.GetCommand();
			command.CommandText = request.Command;
			command.Transaction = transaction;

			//Esecuzione
			try {
				reader = command.ExecuteReader();
			}
			catch (Exception e) {
				throw new Exception("Errore in lettura: "+e.Message);
			}

			//Controllo
			if (!registered.Contains(request.Controller)) {
				transaction.Rollback();
				throw new Exception("Il Controller non è registrato alla fine dell'esecuzione");
			}

			//Fine
			transaction.Commit();
			return result = new DBReaderController(reader);

		}
	}
}
