using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Utilities.Helpers
{
    public static class TaskHelper
    {
        #region RetryOnFault

        public static async Task<T> RetryOnFault<T>(Func<Task<T>> func, int aMaxRetries, int aRetryDelay, Action<int, Exception> exceptionFunc = null)
        {
            for (var ix = 0; ix < aMaxRetries; ix++)
            {
                var needDelay = false;
                try
                {
                    return await func().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    exceptionFunc?.Invoke(ix, ex);

                    if (ix == aMaxRetries - 1)
                        throw;

                    if (aMaxRetries > 0)
                        needDelay = true;
                }

                if (needDelay && aRetryDelay > 0)
                {
                    await Task.Delay(aRetryDelay).ConfigureAwait(false);
                }
            }
            return default;
        }

        public static async Task RetryOnFault(Func<Task> func, int aMaxRetries, int aRetryDelay, Action<int, Exception> exceptionFunc = null)
        {
            for (var ix = 0; ix < aMaxRetries; ix++)
            {
                var needDelay = false;
                try
                {
                    await func().ConfigureAwait(false);
                    return;
                }
                catch (Exception ex)
                {
                    exceptionFunc?.Invoke(ix, ex);

                    if (ix == aMaxRetries - 1)
                        throw;

                    if (aMaxRetries > 0)
                        needDelay = true;
                }

                if (needDelay && aRetryDelay > 0)
                {
                    await Task.Delay(aRetryDelay).ConfigureAwait(false);
                }
            }
        }

        public static T RetryOnFault<T>(Func<T> func, int aMaxRetries, int aRetryDelay, Action<int, Exception> exceptionFunc = null)
        {
            for (var ix = 0; ix < aMaxRetries; ix++)
            {
                var needDelay = false;
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    exceptionFunc?.Invoke(ix, ex);

                    if (ix == aMaxRetries - 1)
                        throw;

                    if (aMaxRetries > 0)
                        needDelay = true;
                }

                if (needDelay && aRetryDelay > 0)
                {
                    Thread.Sleep(aRetryDelay);
                }
            }
            return default;
        }

        public static void RetryOnFault(Action func, int aMaxRetries, int aRetryDelay, Action<int, Exception> exceptionFunc = null)
        {
            for (var ix = 0; ix < aMaxRetries; ix++)
            {
                var needDelay = false;
                try
                {
                    func();
                    return;
                }
                catch (Exception ex)
                {
                    exceptionFunc?.Invoke(ix, ex);

                    if (ix == aMaxRetries - 1)
                        throw;

                    if (aMaxRetries > 0)
                        needDelay = true;
                }

                if (needDelay && aRetryDelay > 0)
                {
                    Thread.Sleep(aRetryDelay);
                }
            }
        }

        #endregion

        #region TimeOutAfter

        public static async Task<T> TimeoutAfter<T>(Task<T> task, TimeSpan timeout)
        {
            using var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
            if (completedTask != task) throw new TimeoutException();
            timeoutCancellationTokenSource.Cancel();
            return await task.ConfigureAwait(false);  // Very important in order to propagate exceptions
        }

        public static async Task TimeoutAfter(Task task, TimeSpan timeout)
        {
            using var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
                await task.ConfigureAwait(false);  // Very important in order to propagate exceptions
            }
            else
            {
                throw new TimeoutException();
            }
        }

        public static bool TimeoutAfter(Action func, TimeSpan timeout)
        {
            var isTimeouted = !Task.Run(func).Wait(timeout);
            return isTimeouted;
        }


        #endregion

        #region FireAndForgetTask

        public static void FireAndForgetTask(Action action)
        {
            var notUsedTask = Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch
                {
                    // ignored
                }
            });
        }

        public static async Task FireAndForget(Func<Task> func)
        {
            try
            {
                await func().ConfigureAwait(false);
            }
            catch
            {
                //Ignore
            }
        }

        public static async Task<T> FireAndForget<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func().ConfigureAwait(false);
                return result;
            }
            catch
            {
                //Ignore
            }

            return default;
        }

        #endregion


        #region Intersept Method

        public static async Task<T> InterseptWithFinalyMethodAsync<T>(Func<Task<T>> func, Action<Exception> exceptionFunc, Func<Task> finalyFunc)
        {
            try
            {
                var result = await func().ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                exceptionFunc(ex);
            }
            finally
            {
                await finalyFunc().ConfigureAwait(false);
            }

            return default;
        }

        public static async Task InterseptWithFinalyMethodAsync(Func<Task> func, Action<Exception> action, Func<Task> finalyFunc)
        {
            try
            {
                await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                action(ex);
            }
            finally
            {
                await finalyFunc().ConfigureAwait(false);
            }
        }

        public static async Task<T> InterseptMethodAsync<T>(Func<Task<T>> func, Action<Exception> exceptionFunc)
        {
            try
            {
                var result = await func().ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                exceptionFunc(ex);
            }
            return default;
        }

        public static async Task InterseptMethodAsync(Func<Task> func, Action<Exception> action)
        {
            try
            {
                await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                action(ex);
            }
        }

        #endregion
    }
}