/**
 * The `node:child_process` module provides the ability to spawn subprocesses.
 * @see [source](https://github.com/nodejs/node/blob/v24.x/lib/child_process.js)
 */
declare module "child_process" {
    import { EventEmitter } from "events";
    import { Readable, Writable } from "stream";

    /**
     * Instances of `ChildProcess` represent spawned child processes.
     *
     * Instances of `ChildProcess` are not intended to be created directly. Rather,
     * use the `spawn()`, `exec()`, `execFile()`, or `fork()` methods to create
     * instances of `ChildProcess`.
     * @since v2.2.0
     */
    class ChildProcess extends EventEmitter {
        /**
         * A `Writable Stream` that represents the child process's `stdin`.
         *
         * If the child was spawned with `stdio[0]` set to anything other than `'pipe'`,
         * then this will be `null`.
         * @since v0.1.90
         */
        stdin: Writable | null;

        /**
         * A `Readable Stream` that represents the child process's `stdout`.
         *
         * If the child was spawned with `stdio[1]` set to anything other than `'pipe'`,
         * then this will be `null`.
         * @since v0.1.90
         */
        stdout: Readable | null;

        /**
         * A `Readable Stream` that represents the child process's `stderr`.
         *
         * If the child was spawned with `stdio[2]` set to anything other than `'pipe'`,
         * then this will be `null`.
         * @since v0.1.90
         */
        stderr: Readable | null;

        /**
         * The `subprocess.killed` property indicates whether the child process
         * successfully received a signal from `subprocess.kill()`.
         * @since v0.5.10
         */
        readonly killed: boolean;

        /**
         * Returns the process identifier (PID) of the child process.
         * @since v0.1.90
         */
        readonly pid: number;

        /**
         * The `subprocess.connected` property indicates whether it is still possible to
         * send and receive messages from a child process.
         * @since v0.7.2
         */
        readonly connected: boolean;

        /**
         * Indicates whether the child process is referenced (parent will wait for it).
         * @since v0.7.10
         */
        readonly referenced: boolean;

        /**
         * The `subprocess.exitCode` property indicates the exit code of the child process.
         * If the child process is still running, the field will be `null`.
         */
        readonly exitCode: number | null;

        /**
         * The `subprocess.signalCode` property indicates the signal received by
         * the child process if any, else `null`.
         */
        readonly signalCode: string | null;

        /**
         * The full list of command-line arguments the child process was launched with.
         */
        readonly spawnargs: string[];

        /**
         * The executable file name of the child process that is launched.
         */
        readonly spawnfile: string;

        /**
         * The `subprocess.kill()` method sends a signal to the child process.
         * @param signal The signal to send (default: 'SIGTERM')
         * @returns True if the signal was sent successfully
         * @since v0.1.90
         */
        kill(signal?: string): boolean;

        /**
         * Closes the IPC channel between parent and child.
         * @since v0.7.2
         */
        disconnect(): void;

        /**
         * When an IPC channel exists, the send() method sends messages to the child process.
         * @param message The message to send
         * @param sendHandle Optional handle to send
         * @param options Optional send options
         * @param callback Optional callback when message is sent
         * @returns True if message was queued successfully
         * @since v0.5.9
         */
        send(
            message: any,
            sendHandle?: any,
            options?: any,
            callback?: (error: Error | null) => void
        ): boolean;

        /**
         * Calling `subprocess.ref()` after making a call to `subprocess.unref()` will
         * restore the removed reference count for the child process.
         * @since v0.7.10
         */
        ref(): void;

        /**
         * Calling `subprocess.unref()` will allow the parent process to exit
         * independently of the child.
         * @since v0.7.10
         */
        unref(): void;

        /**
         * @event close Emitted when the stdio streams have been closed.
         */
        on(event: "close", listener: (code: number | null, signal: string | null) => void): this;

        /**
         * @event disconnect Emitted after calling `subprocess.disconnect()`.
         */
        on(event: "disconnect", listener: () => void): this;

        /**
         * @event error Emitted when the process could not be spawned or killed.
         */
        on(event: "error", listener: (err: Error) => void): this;

        /**
         * @event exit Emitted when the child process ends.
         */
        on(event: "exit", listener: (code: number | null, signal: string | null) => void): this;

        /**
         * @event message Emitted when the child process sends a message using `process.send()`.
         */
        on(event: "message", listener: (message: any, sendHandle: any) => void): this;

        /**
         * @event spawn Emitted once the child process has spawned successfully.
         */
        on(event: "spawn", listener: () => void): this;
    }

    /**
     * Options for exec, spawn, and related methods.
     */
    interface ExecOptions {
        /**
         * Current working directory of the child process.
         */
        cwd?: string;

        /**
         * Environment variables to pass to the child process.
         */
        env?: Record<string, string>;

        /**
         * Encoding to use for string output (default: 'buffer' returns Buffer).
         */
        encoding?: string | null;

        /**
         * Shell to execute the command with (default: '/bin/sh' on Unix, 'cmd.exe' on Windows).
         */
        shell?: string;

        /**
         * Timeout in milliseconds (default: 0 = no timeout).
         */
        timeout?: number;

        /**
         * Largest amount of data in bytes allowed on stdout or stderr (default: 1024*1024).
         */
        maxBuffer?: number;

        /**
         * Signal to use to kill the process (default: 'SIGTERM').
         */
        killSignal?: string;

        /**
         * Hide the subprocess console window on Windows (default: false).
         */
        windowsHide?: boolean;

        /**
         * No quoting or escaping of arguments on Windows (default: false).
         */
        windowsVerbatimArguments?: boolean;

        /**
         * Prepare child to run independently of its parent process (Unix only).
         */
        detached?: boolean;

        /**
         * User identity of the process (Unix only).
         */
        uid?: number;

        /**
         * Group identity of the process (Unix only).
         */
        gid?: number;

        /**
         * Explicitly set the value of argv[0] sent to the child process.
         */
        argv0?: string;

        /**
         * stdio configuration ('pipe', 'inherit', 'ignore').
         */
        stdio?: string;

        /**
         * Input to be sent to stdin (for sync methods).
         */
        input?: string;
    }

    /**
     * Return type for spawnSync and related synchronous child process methods.
     */
    interface SpawnSyncReturns<T> {
        /**
         * The process ID of the spawned child process.
         */
        pid: number;

        /**
         * Array containing the results from stdio output.
         */
        output: Array<T | null>;

        /**
         * The contents of stdout.
         */
        stdout: T;

        /**
         * The contents of stderr.
         */
        stderr: T;

        /**
         * The exit code of the subprocess, or null if the subprocess terminated due to a signal.
         */
        status: number | null;

        /**
         * The signal used to kill the subprocess, or null.
         */
        signal: string | null;

        /**
         * Error object if the child process failed or timed out.
         */
        error?: Error;
    }

    /**
     * The `child_process.execSync()` method blocks until the child process exits.
     *
     * **Never pass unsanitized user input to this function. Any input containing shell**
     * **metacharacters may be used to trigger arbitrary command execution.**
     * @param command The command to run.
     * @param options Options object.
     * @returns The stdout from the command.
     * @since v0.11.12
     */
    function execSync(command: string): Buffer;
    function execSync(command: string, options: ExecOptions & { encoding: "buffer" | null }): Buffer;
    function execSync(command: string, options: ExecOptions & { encoding: BufferEncoding }): string;
    function execSync(command: string, options?: ExecOptions): string | Buffer;

    /**
     * The `child_process.exec()` spawns a shell and runs a command asynchronously.
     *
     * **Never pass unsanitized user input to this function. Any input containing shell**
     * **metacharacters may be used to trigger arbitrary command execution.**
     * @param command The command to run.
     * @param options Options object.
     * @param callback Called with the output when process terminates.
     * @since v0.1.90
     */
    function exec(
        command: string,
        callback?: (error: Error | null, stdout: string, stderr: string) => void
    ): void;
    function exec(
        command: string,
        options: ExecOptions | null,
        callback?: (error: Error | null, stdout: string, stderr: string) => void
    ): void;

    /**
     * The `child_process.spawnSync()` method blocks until the child process exits.
     *
     * **If the `shell` option is enabled, do not pass unsanitized user input to this**
     * **function. Any input containing shell metacharacters may be used to trigger**
     * **arbitrary command execution.**
     * @param command The command to run.
     * @param args List of string arguments.
     * @param options Options object.
     * @returns SpawnSyncReturns object containing pid, output, stdout, stderr, status, signal.
     * @since v0.11.12
     */
    function spawnSync(command: string): SpawnSyncReturns<Buffer>;
    function spawnSync(command: string, args?: string[]): SpawnSyncReturns<Buffer>;
    function spawnSync(
        command: string,
        args: string[] | undefined,
        options: ExecOptions & { encoding: "buffer" | null }
    ): SpawnSyncReturns<Buffer>;
    function spawnSync(
        command: string,
        args: string[] | undefined,
        options: ExecOptions & { encoding: BufferEncoding }
    ): SpawnSyncReturns<string>;
    function spawnSync(command: string, args?: string[], options?: ExecOptions): SpawnSyncReturns<string | Buffer>;

    /**
     * The `child_process.spawn()` method spawns a new process asynchronously.
     * @param command The command to run.
     * @param args List of string arguments.
     * @param options Options object.
     * @returns ChildProcess instance.
     * @since v0.1.90
     */
    function spawn(command: string, args?: string[], options?: ExecOptions): ChildProcess;

    /**
     * The `child_process.execFileSync()` is similar to `execSync()` except that it
     * does not spawn a shell by default.
     *
     * **If the `shell` option is enabled, do not pass unsanitized user input to this**
     * **function. Any input containing shell metacharacters may be used to trigger**
     * **arbitrary command execution.**
     * @param file The name or path of the executable file to run.
     * @param args List of string arguments.
     * @param options Options object.
     * @returns The stdout from the command.
     * @since v0.11.12
     */
    function execFileSync(file: string): Buffer;
    function execFileSync(file: string, args?: string[]): Buffer;
    function execFileSync(file: string, args: string[] | undefined, options: ExecOptions & { encoding: "buffer" | null }): Buffer;
    function execFileSync(file: string, args: string[] | undefined, options: ExecOptions & { encoding: BufferEncoding }): string;
    function execFileSync(file: string, args?: string[], options?: ExecOptions): string | Buffer;

    /**
     * The `child_process.execFile()` function is similar to `exec()` except that it
     * does not spawn a shell by default.
     * @param file The name or path of the executable file to run.
     * @param args List of string arguments.
     * @param options Options object.
     * @param callback Called with the output when process terminates.
     * @since v0.1.91
     */
    function execFile(
        file: string,
        args: string[] | undefined | null,
        options: ExecOptions | undefined | null,
        callback?: (error: Error | null, stdout: string, stderr: string) => void
    ): void;

    /**
     * The `child_process.fork()` method is a special case of `spawn()` used
     * specifically to spawn new Node.js processes.
     * @param modulePath The module to run in the child.
     * @param args List of string arguments.
     * @param options Options object.
     * @returns ChildProcess instance with IPC channel.
     * @since v0.5.0
     */
    function fork(modulePath: string, args?: string[], options?: ExecOptions): ChildProcess;
}

declare module "node:child_process" {
    export * from "child_process";
}
