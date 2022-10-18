using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CPAPIRPC;

public class RPC_Handler
{
    





         public void SendRPC(string method, string[] parameters)
    {
        string url = "http://localhost:8080/jsonrpc";
        string json = "{\"jsonrpc\": \"2.0\", \"method\": \"" + method + "\", \"params\": " + JsonConvert.SerializeObject(parameters) + ", \"id\": 1}";
        byte[] data = Encoding.UTF8.GetBytes(json);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = data.Length;
        using (Stream stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        JObject jObject = JObject.Parse(responseString);
        string result = jObject["result"].ToString();
    }
    
    public static string RequestServer(string password,string username,string serverip,string methodName, List<string> parameters)
    {
        // Use the values you specified in the bitcoin server command line
        string ServerIp = serverip;
        string UserName = username;
        string Password = password;

        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(ServerIp);
        webRequest.Credentials = new NetworkCredential(UserName, Password);

        webRequest.ContentType = "application/json-rpc";
        webRequest.Method = "POST";

        string responseValue = string.Empty;
        // Configure request type
        JObject joe = new JObject();
        joe.Add(new JProperty("jsonrpc", "1.0"));
        joe.Add(new JProperty("id", "1"));
        joe.Add(new JProperty("method", methodName));

        JArray props = new JArray();
        foreach (var parameter in parameters)
        {
            props.Add(parameter);
        }

        joe.Add(new JProperty("params", props));

        // serialize JSON for request
        string s = JsonConvert.SerializeObject(joe);
        byte[] byteArray = Encoding.UTF8.GetBytes(s);
        webRequest.ContentLength = byteArray.Length;
        Stream dataStream = webRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        // deserialze the response
        StreamReader sReader = null;
        WebResponse webResponse = webRequest.GetResponse();
        sReader = new StreamReader(webResponse.GetResponseStream(), true);
        responseValue = sReader.ReadToEnd();
        var data = JsonConvert.DeserializeObject(responseValue).ToString();
        if (data != null)
        {
            blog.write(" Json Deserialize - RPC Handler : "+data);
            
        }
        return data;
    }
}