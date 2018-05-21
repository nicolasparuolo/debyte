using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debyte.Database {
	interface ICommandBuilder {

		string BuildCommand(params string[] p);
	}
}
