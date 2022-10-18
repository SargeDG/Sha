namespace CPAPIRPC;

public class blog
{
    public bool _isRunning = false;
    public static void write(string message)
    {
        //create new thread to write log
  
            try
            {
                //get current time
                DateTime time = DateTime.Now;
                string date = time.ToString("dd/MM/yyyy HH:mm:ss");

                //write log
                string file = "CPAPIRPC.log";
                string line = "[" + date + "] " + message;
                File.AppendAllText(file, line + Environment.NewLine);
                Console.WriteLine(line);
            }
            catch (Exception e)
            {
                //write exception to console
                Console.WriteLine(e.ToString());
            }

    

        
        
    }
}