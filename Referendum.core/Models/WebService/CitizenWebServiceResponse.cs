using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.core.Models
{
	public class CitizenWebServiceResponse
	{
		public string opaque { get; set; }
		public string last_name { get; set; }
		public string first_name { get; set; }
		public string SSN { get; set; }
		public string time {get;set;}
		
	}
}
