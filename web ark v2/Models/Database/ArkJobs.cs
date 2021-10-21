using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ArkWeb.Models.Database
{
    public class ArkJobs
    {
		public int Id { get; set; }
		//[Id] [int] IDENTITY(1,1) NOT NULL,
		public string Name { get; set; }
		//  [Name] [nvarchar] (100) NOT NULL,
		public int Type { get; set; }
		//[Type] [int] NOT NULL,
		public int Result { get; set; }
		//[Result] [int] NOT NULL,

		public DateTime DateJob { get; set; }
		//[DateJob] [datetime]
		//NOT NULL,

		public bool Repeating { get; set; }
		//[Repeating] [bit]
		//NOT NULL,

	}
} 
