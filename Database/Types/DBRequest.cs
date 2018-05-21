using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace Debyte.Database.Types {
	class DBRequest {

		private readonly DatabaseController _controller;
		private string _command;

		internal DatabaseController Controller => _controller;

		public string Command { get => _command; set => _command = value; }

		public DBRequest(DatabaseController controller, string command) {
			_controller = controller;
			_command = command;
		}

		public DBRequest(DatabaseController controller): this(controller, null) {
		//Niente di che
		}
	}
}
