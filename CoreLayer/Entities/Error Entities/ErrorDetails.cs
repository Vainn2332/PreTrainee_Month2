using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace PreTrainee_Month2.CoreLayer.Entities.Error_Entities
{
    public class ErrorDetails//класс для обработки исключений в ExceptionHandlerMiddleware
    {
        public string ExceptionMessage { get; set; }
        public string ExceptionInfo { get; set; }
        public int StatusCode { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
