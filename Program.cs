namespace PasswordReset
{
	class PasswordReset
	{
		static void Main(string[] args)
		{
			Console.WriteLine("To Reset Your Password Enter Your Email: ");
			string email = Console.ReadLine();

			if(IsValidEmail(email))
			{
				SendResetEmail(email);
				Console.WriteLine("Tried to send an email reset link");
			}
            else
            {
				Console.WriteLine("Invalid Email Address.");
            }
        }

		static bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;	
			}
		}

		static void SendResetEmail(string email)
		{
			var message = new MimeKit.MimeMessage();

			message.From.Add(new MimeKit.MailboxAddress("TROTA", "trota@mail.com"));
			message.To.Add(new MimeKit.MailboxAddress("",email));

			message.Subject = "Password Reset";
			message.Body = new MimeKit.TextPart("plain")
			{
				Text = "Here is your password reset link: "
			};

			using (var client = new MailKit.Net.Smtp.SmtpClient())
			{
				try
				{
					client.Connect("smtp.example.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
					client.Authenticate("Username", "Password");
					client.Send(message);
				}
				catch(Exception ex)
				{
					Console.WriteLine($"An Error Occured While Sending Email: {ex.Message}");

				}
				finally
				{
					client.Disconnect(true);
				}

			}
		}
	}
}

