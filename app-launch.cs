using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Diagnostics;


ProcessStartInfo s_CountInfo = new ProcessStartInfo();
Process s_Count;
s_CountInfo.FileName = @"C:\Windows\notepad.exe";
s_CountInfo.Arguments = @"";
s_CountInfo.CreateNoWindow = false;
s_CountInfo.UseShellExecute = false;
s_Count = Process.Start(pythonInfo);
s_Count.WaitForExit();
s_Count.Close();
