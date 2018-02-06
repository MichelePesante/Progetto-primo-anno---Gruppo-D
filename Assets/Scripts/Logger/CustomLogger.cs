using System;

public static class CustomLogger {

	public static LoggerType Type = LoggerType.ScreenLogger;

	public static string currentLogString = string.Empty;

	public static void Log (string message, params Object[] args) {
		switch (Type) {
		case LoggerType.UnityLogger:
			UnityEngine.Debug.LogFormat (message, args);
			break;
		case LoggerType.ScreenLogger:
			currentLogString = String.Format (message, args) + Environment.NewLine + currentLogString;
			break;
		default:
			UnityEngine.Debug.LogFormat (message, args);
			break;
		}
	}
}

public enum LoggerType {
	UnityLogger,
	ScreenLogger
}