using Newtonsoft.Json;

namespace HealthTrackerSolution
{
    internal class ErrorResponseData
    {
        
        public int StatusCode { get; internal set; }
        public string Message { get; internal set; }
        public string Path { get; internal set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}