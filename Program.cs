using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Debyte.ServerWeb;

namespace Debyte {
	class Program {
		public static string SendResponse(HttpListenerRequest request) {
			return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
		}
		static void Main(string[] args) {

			var ws = new WebServer(SendResponse, "http://localhost:8080/debyte/");
			ws.Run();
			Console.WriteLine("A simple webserver. Press a key to quit.");
			Console.ReadKey();
			ws.Stop();

		}
	}
}
