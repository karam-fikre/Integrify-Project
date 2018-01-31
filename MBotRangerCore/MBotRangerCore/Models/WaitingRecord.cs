using System;

namespace MBotRangerCore.Models
{
	public class WaitingRecord
	{
		public string UserName { get; set; } // The users name, email or whatever unique string of charachters.
		public DateTime DateTimeLastActive { get; set; } // When the user is introduced to the list, so we can calculate inacctivity time.
	}
}