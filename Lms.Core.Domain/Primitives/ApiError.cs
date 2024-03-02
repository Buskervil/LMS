using System.Net;

namespace Lms.Core.Domain.Primitives
{
    /// <summary>
    /// Предоставляет ошибку какой-либо операции, с информацией возвращаемого кода и текста ошибки
    /// </summary>
    public sealed class ApiError
    {
        private readonly HttpStatusCode _code;
        private readonly string _message;

        /// <summary>
        /// Иницилизирует новый инстанс <see cref="Error"/> класса.
        /// </summary>
        /// <param name="code">Код ошибки</param>
        /// <param name="message">Текст ошибки</param>
        public ApiError(HttpStatusCode code, string message)
        {
            _code = code;
            _message = string.Intern(message);
        }

        /// <summary>
        /// Получить код ошибки
        /// </summary>
        public HttpStatusCode Code
        {
            get
            {
                return _code;
            }
        }

        /// <summary>
        /// Получить текст ошибки
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
        }

        public static ApiError BadRequest(string errorMessage)
        {
            return new ApiError(HttpStatusCode.BadRequest, errorMessage);
        }
        
        public static ApiError Forbidden(string errorMessage)
        {
            return new ApiError(HttpStatusCode.Forbidden, errorMessage);
        }

        public static ApiError NotFound(string errorMessage)
        {
            return new ApiError(HttpStatusCode.NotFound, errorMessage);
        }

        public static ApiError InternalServerError(string errorMessage)
        {
            return new ApiError(HttpStatusCode.InternalServerError, errorMessage);
        }
    }
}
