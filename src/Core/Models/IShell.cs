using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sheller.Implementations.Executables;

namespace Sheller.Models
{
    /// <summary>
    /// The interface for defining a shell.
    /// </summary>
    public interface IShell
    {
        Task<ICommandResult> ExecuteCommandAsync(string executable, IEnumerable<string> arguments);

        IShell Clone();

        /// <summary>
        /// Adds an environment variable (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="key">The environment variable key.</param>
        /// <param name="value">The environment variable value.</param>
        /// <returns>A `new` instance of type <see cref="IShell"/> with the environment variable passed in this call.</returns>
        IShell WithEnvironmentVariable(string key, string value);
        /// <summary>
        /// Adds a list of environment variables (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="variables">The list of key value pairs of environment variables.</param>
        /// <returns>A `new` instance of type <see cref="IShell"/> with the environment variables passed in this call.</returns>
        IShell WithEnvironmentVariables(IEnumerable<KeyValuePair<string, string>> variables);
        /// <summary>
        /// Adds a list of environment variables (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="variables">The list of tuple of environment variables.</param>
        /// <returns>A `new` instance of type <see cref="IShell"/> with the environment variables passed in this call.</returns>
        IShell WithEnvironmentVariables(IEnumerable<(string, string)> variables);

        /// <summary>
        /// Adds a standard output handler (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="standardOutputHandler">An <see cref="Action"/> that handles a new line in the standard output of the executable.</param>
        /// <returns>A `new` instance of type <see cref="IShell"/> with the standard output handler passed to this call.</returns>
        IShell WithStandardOutputHandler(Action<string> standardOutputHandler);
        /// <summary>
        /// Adds an error output handler (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="standardErrorHandler">An <see cref="Action"/> that handles a new line in the standard error of the executable.</param>
        /// <returns>A `new` instance of type <see cref="IShell"/> with the standard error handler passed to this call.</returns>
        IShell WithStandardErrorHandler(Action<string> standardErrorHandler);

        /// <summary>
        /// Adds an executable and switches to the executable context.
        /// </summary>
        /// <typeparam name="TExecutable">The type of the executable to use.</typeparam>
        /// <returns>An instance of <typeparamref name="TExecutable"/> passed to this call.</returns>
        TExecutable UseExecutable<TExecutable>() where TExecutable : Executable<TExecutable>, new();
        /// <summary>
        /// Adds an executable and switches to the executable context.
        /// </summary>
        /// <param name="exe">The name or path of the executable.</param>
        /// <returns>An instance of <see cref="GenericExe"/> which represents the executable name or path passed to this call.</returns>
        GenericExe UseExecutable(string exe);
        
        /// <summary>
        /// Builds the arguments that should be passed to the shell based on the shell's type.
        /// </summary>
        /// <param name="executableCommand">The name or path of the executable for which to build the command.</param>
        /// <returns>A <see cref="string"/> which represents the command argument that should be passed to this shell.</returns>
        string GetCommandArgument(string executableCommand);
    }

    /// <summary>
    /// The interface for defining a shell.
    /// </summary>
    /// <typeparam name="TShell">The type of the shell class implementing this interface.  This allows the base class to return `new` instances for daisy chaining.</typeparam>
    public interface IShell<out TShell> : IShell where TShell : IShell<TShell>
    {
        new TShell Clone();

        /// <summary>
        /// Adds an environment variable (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="key">The environment variable key.</param>
        /// <param name="value">The environment variable value.</param>
        /// <returns>A `new` instance of <typeparamref name="TShell"/> with the environment variable passed in this call.</returns>
        new TShell WithEnvironmentVariable(string key, string value);
        /// <summary>
        /// Adds a list of environment variables (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="variables">The list of key value pairs of environment variables.</param>
        /// <returns>A `new` instance of <typeparamref name="TShell"/> with the environment variables passed in this call.</returns>
        new TShell WithEnvironmentVariables(IEnumerable<KeyValuePair<string, string>> variables);
        /// <summary>
        /// Adds a list of environment variables (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="variables">The list of tuple of environment variables.</param>
        /// <returns>A `new` instance of <typeparamref name="TShell"/> with the environment variables passed in this call.</returns>
        new TShell WithEnvironmentVariables(IEnumerable<(string, string)> variables);

        /// <summary>
        /// Adds a standard output handler (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="standardOutputHandler">An <see cref="Action"/> that handles a new line in the standard output of the executable.</param>
        /// <returns>A `new` instance of <typeparamref name="TShell"/> with the standard output handler passed to this call.</returns>
        new TShell WithStandardOutputHandler(Action<string> standardOutputHandler);
        /// <summary>
        /// Adds an error output handler (of which there may be many) to the shell context and returns a `new` context instance.
        /// </summary>
        /// <param name="standardErrorHandler">An <see cref="Action"/> that handles a new line in the standard error of the executable.</param>
        /// <returns>A `new` instance of <typeparamref name="TShell"/> with the standard error handler passed to this call.</returns>
        new TShell WithStandardErrorHandler(Action<string> standardErrorHandler);
    }
}