using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debyte.Database {
	class DatabaseController : IDisposable {

		private DatabaseManager manager;
		
		public void Dispose() {
			manager.Unregister(this);
		}
	}
}
