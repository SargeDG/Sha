using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace CPAPIRPC;

public class API_Handlerz
{
    public int reqcount = 0;
    public bool _isRunning = false;
    public void _runAPI()
    {
        _isRunning = true;
        Thread _thread = new Thread(new ThreadStart(() =>
        {
         
            try
            {
                // create http listener and check for get requests for urls starting with /api/ and return some json data
                HttpListener _listener = new HttpListener();
                _listener.Prefixes.Add("http://*:1777/api/");
                _listener.Start();
                blog.write("API Handler Started");
                while (_isRunning)
                {
                    
                    HttpListenerContext _context = _listener.GetContext();
                    HttpListenerRequest _request = _context.Request;
                    if (_request != null)
                    {
                        reqcount++;
                    }
                    HttpListenerResponse _response = _context.Response;
                    //logging
                    blog.write( "Request URI: "+_request.Url.ToString());
                    blog.write("User Agent: "+_request.UserAgent);
                    blog.write("UserHostAddress: "+_request.UserHostAddress);
                    blog.write("Http Method: "+_request.HttpMethod);
                    blog.write("Request IsLocal: "+_request.IsLocal);
                    blog.write("Request Headers: "+_request.Headers);
                  //  blog.write("Request Cookies: "+_request.Cookies);
                    
                    
                    string _responseString = "";

                    // check if the request is a get request
                    if (_request.HttpMethod == "GET")
                    {
                        // check if the url is /api/users
                        if (_request.RawUrl == "/api/users")
                        {
                            // get the users from the database
                            List<string> _users = new List<string>();
                            _users.Add("user1");
                            _users.Add("user2");
                            _users.Add("user3");
                            _users.Add("user4");

                            // serialize the users to json
                            _responseString = JsonConvert.SerializeObject(_users);
                        }
                        // check if the url is /api/user/{id}
                        else if (_request.RawUrl.StartsWith("/api/user/"))
                        {
                            // get the id from the url
                            int _id = int.Parse(_request.RawUrl.Replace("/api/user/", ""));

                            // get the user from the database
                            List<int> _uId = new List<int>();
                            _uId.Add(1);
                            _uId.Add(2);
                            _uId.Add(3);
                            _uId.Add(4);
                            List<string> _users = new List<string>();
                            _users.Add("user1");
                            _users.Add("user2");
                            _users.Add("user3");
                            _users.Add("user4");
                            
                            // create dictonary with uId and users
                            Dictionary<int, string> _user = new Dictionary<int, string>();
                            for (int i = 0; i < _uId.Count; i++)
                            {
                                _user.Add(_uId[i], _users[i]);
                            }
                            

                            foreach (var idizzle in _uId)
                            {
                                if (idizzle == _id)
                                {
                                   // get record from dictonary
                                    _responseString = JsonConvert.SerializeObject(_user[idizzle]);
                                    blog.write(_responseString);
                                }
                                else
                                {
                                    // get first record from dictonary
                                    _responseString = JsonConvert.SerializeObject(_user[_uId[0]]);
                                    blog.write(_responseString);
                                }
                            }
                                
                            
                            

                            
                        }
                    }

                    // set the content type and write the response
                    _response.ContentType = "application/json";
                    byte[] _buffer = Encoding.UTF8.GetBytes(_responseString);
                    _response.ContentLength64 = _buffer.Length;
                    Stream _outputStream = _response.OutputStream;
                    _outputStream.Write(_buffer, 0, _buffer.Length);
                    _outputStream.Close();
                }
            }
            catch (Exception ex)
            {
              blog.write(ex.ToString());
            }
                
        }));
        
        //start thread
        _thread.Start();
        
        if (_isRunning == false)
        {
            _thread.Abort(); 
            blog.write("API Handler Stopped");
        }
    }


    public class __Users
    {
        public string username { get; set; }
        public int id { get; set; }
        
        
    }
}
