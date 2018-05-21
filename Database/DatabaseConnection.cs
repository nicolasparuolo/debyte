using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace Debyte.Database {
	class DatabaseConnection: IDisposable {

		private readonly string connectString = "Database=DEBYTE;UserID=DB2admin;Password=debyte;Server=localhost:50000";
		private readonly DB2Connection _dB2Connection;
		

		public DatabaseConnection() {
			_dB2Connection = new DB2Connection(connectString);
			_dB2Connection.Open();
		}

		public bool IsOpen() {
			return _dB2Connection.IsOpen;
		}

		public DB2Command GetCommand() {
			return _dB2Connection.CreateCommand();
		}

		public DB2Transaction GetTransaction() {
			return _dB2Connection.BeginTransaction();
		}

		public void Dispose() {
			_dB2Connection.Close();
		}
	}
}
