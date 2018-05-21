using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace Debyte.Database.Types {
	class DBReaderController {

		private readonly DB2DataReader _reader;
		public DB2DataReader Reader => _reader;

		public DBReaderController(DB2DataReader reader) {
			_reader = reader;
		}
	}
}
