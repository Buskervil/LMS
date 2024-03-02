using Lms.Core.Domain.Primitives;

namespace Lms.Core.Domain.Results
{
    /// <summary>
    /// Представляет результат какой-либо операции, с информацией о статусе и, возможно, с ошибкой.
    /// </summary>
    public class Result
    {
        private readonly ApiError _error;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Result"/> с указанными параметрами.
        /// </summary>
        /// <param name="isSuccess">Флаг успешно ли была выполнена операция.</param>
        /// <param name="error">Ошибка выполнения операции.</param>
        /// <exception cref="InvalidOperationException">
        ///     Когда <paramref name="isSuccess"/> имеет значение true и <paramref name="error"/> не null
        ///     Либо когда <paramref name="isSuccess"/> имеет значение false и <paramref name="error"/> null
        /// </exception>
        protected Result(bool isSuccess, ApiError error)
        {
            if (isSuccess ^ error == null)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            _error = error;
        }

        /// <summary>
        /// Получить значение, указывающее, является ли результат успешным.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Получить значение, указывающее, является ли результат неудачным.
        /// </summary>
        public bool IsFailure => IsSuccess == false;

        /// <summary>
        /// Получить ошибку.
        /// </summary>
        public ApiError Error
        {
            get
            {
                return _error;
            }
        }

        /// <summary>
        /// Создает успешный результат <see cref="Result"/>.
        /// </summary>
        /// <returns>Новый инстанс <see cref="Result"/> с установленным флагом успешности.</returns>
        public static Result Success()
        {
            return new Result(true, null);
        }

        /// <summary>
        /// Создает успешный результат <see cref="Result{TValue}"/> с указанным значением.
        /// </summary>
        /// <typeparam name="TValue">Тип результата.</typeparam>
        /// <param name="value">Инстанс результата.</param>
        /// <returns>Новый инстанс <see cref="Result{TValue}"/> с установленным флагом успешности.</returns>
        public static Result<TValue> Success<TValue>(TValue value)
        {
            return new Result<TValue>(value, true, null);
        }

        /// <summary>
        /// Создает <see cref="Result{TValue}"/> с указанным значением и указанной ошибкой.
        /// </summary>
        /// <typeparam name="TValue">Тип результата.</typeparam>
        /// <param name="value">Инстанс результата.</param>
        /// <param name="error">Ошибка, если значение является null</param>
        /// <returns>Новый инстанс <see cref="Result{TValue}"/> с указанным значением или с ошибкой.</returns>
        public static Result<TValue> Create<TValue>(TValue value, ApiError error)
            where TValue : class
        {
            return value ?? Failure<TValue>(error);
        }

        /// <summary>
        /// Создает <see cref="Result{TValue}"/> с указанным значением и указанной ошибкой.
        /// </summary>
        /// <typeparam name="TValue">Тип результата.</typeparam>
        /// <param name="value">Инстанс результата.</param>
        /// <param name="error">Ошибка, если значение является null</param>
        /// <returns>Новый инстанс <see cref="Result{TValue}"/> с указанным значением или с ошибкой.</returns>
        public static async Task<Result<TValue>> Create<TValue>(Task<TValue> value, ApiError error)
            where TValue : class
        {
            return await value ?? Failure<TValue>(error);
        }

        /// <summary>
        /// Создает <see cref="Result{TValue}"/> с указанным значением и указанной ошибкой.
        /// </summary>
        /// <typeparam name="TValue">Тип результата.</typeparam>
        /// <param name="value">Инстанс результата.</param>
        /// <param name="error">Ошибка, если значение является null</param>
        /// <returns>Новый инстанс <see cref="Result{TValue}"/> с указанным значением или с ошибкой.</returns>
        public static Result<TValue> Create<TValue>(TValue? value, ApiError error)
            where TValue : struct
        {
            return value ?? Failure<TValue>(error);
        }

        /// <summary>
        /// Создает не успешный результат <see cref="Result"/> с указанной ошибкой.
        /// </summary>
        /// <param name="error">Ошибка.</param>
        /// <returns>Инстанс <see cref="Result"/> с указанной ошибкой и установленным флагом неудачи.</returns>
        public static Result Failure(ApiError error)
        {
            return new Result(false, error);
        }

        /// <summary>
        /// Создает не успешный результат <see cref="Result{TValue}"/> с указанной ошибкой.
        /// </summary>
        /// <typeparam name="TValue">Тип результата.</typeparam>
        /// <param name="error">Ошибка.</param>
        /// <returns>Новый инстанс <see cref="Result{TValue}"/> с указанной ошибкой и установленным флагом неудачи.</returns>
        public static Result<TValue> Failure<TValue>(ApiError error)
        {
            return new Result<TValue>(default, false, error);
        }
    }

    /// <summary>
    /// Представляет результат какой-либо операции, с информацией о статусе и, возможно, со значением и ошибкой
    /// </summary>
    /// <typeparam name="TValue">Тип результата.</typeparam>
    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Result{TValueType}"/> с указанными параметрами.
        /// </summary>
        /// <param name="value">Инстанс результата.</param>
        /// <param name="isSuccess">Флаг успешно ли была выполнена операция.</param>
        /// <param name="error">Ошибка.</param>
        protected internal Result(TValue value, bool isSuccess, ApiError error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        /// <summary>
        /// Возращает значение или выкидывает исключение если значение null.
        /// </summary>
        /// <exception cref="InvalidOperationException"> когда <see cref="Result.IsFailure"/> имеет значение true.</exception>
        public TValue Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }

        public static implicit operator Result<TValue>(TValue value)
        {
            return Success(value);
        }
    }
}
