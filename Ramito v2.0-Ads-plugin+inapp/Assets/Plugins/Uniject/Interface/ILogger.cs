//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;

namespace Uniject {

    public interface ILogger {
    	void Log(string message);
        void Log(string message, params object[] formatArgs);
        void LogWarning(string message, params object[] formatArgs);
        void LogError(string message, params object[] formatArgs);
        string prefix { get; set; }
    }
}
