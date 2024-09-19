using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Servise.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email input)
		{
			var client = new SmtpClient("stmp.gmail.com",587);
			client.EnableSsl = true;

			client.Credentials = new NetworkCredential("heba.m.abdelmotal@gmail.com", "euah yami kswv kaju");
			client.Send("heba.m.abdelmotal@gmail.com",input.To,input.Subject,input.Body);



		}
	}
}
