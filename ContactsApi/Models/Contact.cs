﻿using System;
namespace ContactsApi.Models
{
	public class Contact
	{

		public Guid ID { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public long Phone { get; set; }
		public string Address { get; set; }
	}
}

