

namespace Domain.ListaCompras.Common.Results
{

    public abstract class Result
    {

        protected Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; private set; }

        public string Message { get; private set; }
    }

    /// <summary>
    /// Transporta o resultado de uma operação sem exigir exceção para falhas previstas:
    /// <c>Success</c> guarda o dado retornado; <c>Error</c> marca a falha e guarda
    /// a mensagem que será interpretada pela camada chamadora.
    /// </summary>
    public class ResultData<T> : Result
    {

        private ResultData(
            T? data,
            bool isSuccess = true,
            string message = "")
            : base(isSuccess, message)
        {
            Data = data;
        }

        public T? Data { get; set; }

        public static ResultData<T> Success(T? data)
            => new(data);

        public static ResultData<T> Error(string message)
            => new(default, false, message);
    }
}
