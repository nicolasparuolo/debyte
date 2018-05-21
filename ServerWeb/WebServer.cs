﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Debyte.ServerWeb {
	class WebServer {

		private readonly HttpListener _listener = new HttpListener();
		private readonly Func<HttpListenerRequest, string> _responderMethod;

		public WebServer(IReadOnlyCollection<string> prefixes, Func<HttpListenerRequest, string> method) {

			// URI prefixes are required eg: "http://localhost:8080/debyte/"
			if (prefixes == null || prefixes.Count == 0) {
				throw new ArgumentException("URI prefixes are required");
			}

			if (method == null) {
				throw new ArgumentException("responder method required");
			}

			foreach (var s in prefixes) {
				_listener.Prefixes.Add(s);
			}

			_responderMethod = method;
			_listener.Start();
		}

		public WebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
		   : this(prefixes, method) {
		}

		public void Run() {
			ThreadPool.QueueUserWorkItem(o =>
			{
				Console.WriteLine("Webserver running...");
				try {
					while (_listener.IsListening) {
						ThreadPool.QueueUserWorkItem(c =>
						{
							var ctx = c as HttpListenerContext;
							try {
								if (ctx == null) {
									return;
								}

								var rstr = _responderMethod(ctx.Request);
								var buf = Encoding.UTF8.GetBytes(rstr);
								ctx.Response.ContentLength64 = buf.Length;
								ctx.Response.OutputStream.Write(buf, 0, buf.Length);
							}
							catch {
								// ignored
							}
							finally {
								// always close the stream
								if (ctx != null) {
									ctx.Response.OutputStream.Close();
								}
							}
						}, _listener.GetContext());
					}
				}
				catch (Exception ex) {
					// ignored
				}
			});
		}

		public void Stop() {
			_listener.Stop();
			_listener.Close();
		}


	}
}
