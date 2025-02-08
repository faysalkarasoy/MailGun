Imports RestSharp
Imports RestSharp.Authenticators

Public Class EmailSinifi

    Public Sub Gonder(Baslik As String, Icerik As String)
        Const MAILGUN_ApiKey As String = "api_key"
        Const MAILGUN_Domain As String = "email.com"
        Const MAILGUN_DisplayName As String = "display_name"
        Const MAILGUN_Email As String = "info@email.com"

        Try
            Dim options As New RestClientOptions("https://api.mailgun.net/v3") With {
                .Authenticator = New HttpBasicAuthenticator("api", MAILGUN_ApiKey)
            }

            Dim Client As New RestClient(options)
            Dim Request As New RestRequest($"{MAILGUN_Domain}/messages", Method.Post)

            Request.AddParameter("from", $"{MAILGUN_DisplayName} <{MAILGUN_Email}>")
            Request.AddParameter("to", "alici@mail.com")
            Request.AddParameter("cc", "cc_alici@mail.com")
            Request.AddParameter("subject", Baslik)
            Request.AddParameter("html", $"<html><body>{Icerik}</body></html>")

            ' API çağrısını gerçekleştir ve sonucu al
            Dim response As RestResponse = Client.Execute(Request)

            ' Yanıtı kontrol et
            If response.StatusCode <> System.Net.HttpStatusCode.OK Then
                Console.WriteLine("Mail gönderme başarısız: " & response.Content)
            Else
                Console.WriteLine("Mail başarıyla gönderildi!")
            End If
        Catch ex As Exception
            Console.WriteLine("Hata: " & ex.Message)
        End Try
    End Sub
	
End Class
