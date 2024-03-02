using Core.Domain.Primitives;

namespace Core.Domain.Results
{
    /// <summary>
    /// Предоставляет доступ к методам расширения класса <see cref="Result"/> и <see cref="Result{TValue}"/>.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Метод гарантирует, то, что <paramref name="predicate"/> будет выполнен только если результат был успешен.
        /// </summary>
        /// <typeparam name="TValue">Тип результата.</typeparam>
        /// <param name="result">Прошлый результат.</param>
        /// <param name="predicate">Предикат который как-то работает с прошлым результатом.</param>
        /// <param name="error">Какая ошибка должна будть вызвана если предикат вернул false.</param>
        /// <returns>Инстанс результата</returns>
        public static Result<TValue> Ensure<TValue>(
            this Result<TValue> result,
            Func<TValue, bool> predicate,
            ApiError error)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return predicate(result.Value) ? Result.Success(result.Value) : Result.Failure<TValue>(error);
        }

        /// <summary>
        /// Метод гарантирует, то, что <paramref name="predicate"/> будет выполнен только если результат был успешен.
        /// </summary>
        /// <typeparam name="TValue">Тип результата.</typeparam>
        /// <param name="resultTask">Прошлый результат.</param>
        /// <param name="predicate">Предикат который как-то работает с прошлым результатом.</param>
        /// <param name="error">Какая ошибка должна будть вызвана если предикат вернул false.</param>
        /// <returns>Инстанс результата</returns>
        public static async Task<Result<TValue>> Ensure<TValue>(
            this Task<Result<TValue>> resultTask,
            Func<TValue, bool> predicate,
            ApiError error)
        {
            var result = await resultTask;

            if (result.IsFailure)
            {
                return result;
            }

            return predicate(result.Value) ? Result.Success(result.Value) : Result.Failure<TValue>(error);
        }

        /// <summary>
        /// Метод гарантирует, то, что <paramref name="predicate"/> будет выполнен только если результат был успешен.
        /// </summary>
        /// <typeparam name="TValue">Тип результата.</typeparam>
        /// <param name="result">Прошлый результат.</param>
        /// <param name="predicate">Предикат который как-то работает с прошлым результатом.</param>
        /// <param name="error">Какая ошибка должна будть вызвана если предикат вернул false.</param>
        /// <returns>Инстанс результата</returns>
        public static async Task<Result<TValue>> Ensure<TValue>(
            this Result<TValue> result,
            Func<TValue, Task<bool>> predicate,
            ApiError error)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return await predicate(result.Value) ? Result.Success(result.Value) : Result.Failure<TValue>(error);
        }

        /// <summary>
        /// Вызывает функцию зависимости от результата.
        /// </summary>
        /// <typeparam name="T">Тип результата.</typeparam>
        /// <param name="resultTask">Прошлый резльтат</param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailure"></param>
        /// <returns></returns>
        public static async Task<T> Match<T>(
            this Task<Result> resultTask,
            Func<T> onSuccess,
            Func<ApiError, T> onFailure)
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }

        public static T Match<T>(
            this Result result,
            Func<T> onSuccess,
            Func<ApiError, T> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result.Error);
        }

        public static async Task<T> Match<TValue, T>(
            this Task<Result<TValue>> resultTask,
            Func<TValue, T> onSuccess,
            Func<ApiError, T> onFailure)
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }

        public static T Match<TValue, T>(
            this Result<TValue> result,
            Func<TValue, T> onSuccess,
            Func<ApiError, T> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
        }

        public static Result<T> Map<T>(this Result result, Func<Result<T>> func)
        {
            return result.IsSuccess ? func() : Result.Failure<T>(result.Error);
        }

        public static Result<T> Map<TValue, T>(this Result<TValue> result, Func<TValue, T> func)
        {
            return result.IsSuccess ? Result.Success(func(result.Value)) : Result.Failure<T>(result.Error);
        }

        public static async Task<Result<T>> Map<TValue, T>(
            this Task<Result<TValue>> resultTask,
            Func<TValue, T> func)
        {
            var result = await resultTask;

            return result.IsSuccess ? Result.Success(func(result.Value)) : Result.Failure<T>(result.Error);
        }

        public static async Task<Result> Bind<TValue>(
            this Result<TValue> result,
            Func<TValue, Task<Result>> func)
        {
            return result.IsSuccess ? await func(result.Value) : Result.Failure(result.Error);
        }

        public static Result Bind<TValue>(
            this Result<TValue> result,
            Func<TValue, Result> func)
        {
            return result.IsSuccess ? func(result.Value) : Result.Failure(result.Error);
        }

        public static async Task<Result<T>> Bind<TValue, T>(this Result<TValue> result, Func<TValue, Task<Result<T>>> func)
        {
            return result.IsSuccess ? await func(result.Value) : Result.Failure<T>(result.Error);
        }

        public static async Task<Result<T>> Bind<TValue, T>(this Result<TValue> result, Func<TValue, Task<T>> func)
            where T : class
        {
            return result.IsSuccess ? await func(result.Value) : Result.Failure<T>(result.Error);
        }

        public static async Task<Result<T>> Bind<TValue, T>(
            this Result<TValue> result,
            Func<TValue, Task<T>> func,
            ApiError error)
            where T : class
        {
            if (result.IsFailure)
            {
                return Result.Failure<T>(result.Error);
            }

            var value = await func(result.Value);

            return value == null ? Result.Failure<T>(error) : Result.Success(value);
        }

        public static async Task<Result<T>> Bind<TValue, T>(
            this Task<Result<TValue>> resultTask,
            Func<TValue, Task<T>> func)
        {
            var result = await resultTask;

            return result.IsSuccess ? Result.Success(await func(result.Value)) : Result.Failure<T>(result.Error);
        }

        public static async Task<Result> Bind<TValue>(
            this Task<Result<TValue>> resultTask,
            Func<TValue, Task<Result>> func)
        {
            var result = await resultTask;

            return result.IsSuccess ? await func(result.Value) : Result.Failure(result.Error);
        }

        public static async Task<Result<T>> BindScalar<TValue, T>(
            this Result<TValue> result,
            Func<TValue, Task<T>> func)
            where T : struct
        {
            return result.IsSuccess ? await func(result.Value) : Result.Failure<T>(result.Error);
        }

        public static async Task<Result> Tap<TValue>(
            this Task<Result<TValue>> resultTask,
            Action<TValue> action)
        {
            var result = await resultTask;

            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result.IsSuccess ? Result.Success() : Result.Failure(result.Error);
        }
    }
}