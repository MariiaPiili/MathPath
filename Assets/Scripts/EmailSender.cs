using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class EmailSender : MonoBehaviour
{
    

    public void SendEmail(string to, string subject, string body)
    {
        StartCoroutine(SendEmailCoroutine(to, subject, body));
    }

    private IEnumerator SendEmailCoroutine(string to, string subject, string body)
    {
        string url = "https://api.sendgrid.com/v3/mail/send";

        string jsonData = $@"
        {{
            ""personalizations"": [
                {{
                    ""to"": [{{""email"": ""{to}""}}],
                    ""subject"": ""{subject}""
                }}
            ],
            ""from"": {{""email"": ""mathpathproject@gmail.com""}},
            ""content"": [
                {{
                    ""type"": ""text/plain"",
                    ""value"": ""{body}""
                }}
            ]
        }}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + sendGridApiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Email sent successfully!");
        }
        else
        {
            Debug.LogError("Failed to send email: " + request.error);
        }
    }
}

