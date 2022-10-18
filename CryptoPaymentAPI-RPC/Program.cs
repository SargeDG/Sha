
// create a class

using CPAPIRPC;

class MyClass {
  // create a  main method 
  public static void Main(String[] args)
  {
      API_Handlerz _API = new API_Handlerz();
      _API._runAPI();
      var _IsRunning = true;
        while (_IsRunning)
        {
          
           // display time in console without creating a new line
            Console.Write("\r" + _API.reqcount + " Requests Recv'd  - ");
            Thread.Sleep(1000);
           
        }
      
  }
  
}