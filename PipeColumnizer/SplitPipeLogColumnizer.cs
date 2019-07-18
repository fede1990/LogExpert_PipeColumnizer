using System;
using System.Collections.Generic;
using System.Text;
using LogExpert;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using System.Xml;

namespace LogExpert
{

    public class SplitPipeLogColumnizer : ILogLineColumnizer
    {
 
        public string GetName()
        {
            return "Pipe's Columnizer";
        }

        public string GetDescription()
        {
            return "Logfile Format used by me.";
        }

        ///INFO
        ///example of a log line, to test the plugin, create a txt file with this and read it from LogExpert
        ///2019-07-03 07:09:21:34446|3580| 1|10|  1|(SocketServer/), CreateListenSocket() OK| | ThreadSocketServerProc

        public int GetColumnCount()
        {
            return 9;
        }

        public string[] GetColumnNames()
        {
            return new string[] { "Date", "Time", "Ticks", "PID", "#", "Level", "Thread", "Component/Method", "Message" };
        }

        public string[] SplitLine(ILogLineColumnizerCallback callback, string line)
        {
            string[] cols = new string[9] { "", "", "", "", "", "", "", "", "" };
            String[] res = Regex.Split(line, "\\|");

            if (res.Length >= 6)
            {
                String[] dateTime = Regex.Split(res[0], "\\s");
                cols[0] = dateTime[0];
                cols[1] = dateTime[1].Substring(0, 8);
                cols[2] = dateTime[1].Substring(9);
                cols[3] = res[1];

                cols[4] = res[2]; // 1
                cols[5] = res[3]; // Level
                cols[6] = res[4]; // Thread

                int n = res[5].IndexOf("),");
                string all = "";
                int i = 0;

                if (n > 0)
                {

                    cols[7] = res[5].Substring(1, n - 1);

                    cols[8] = res[5].Substring(n + 3);
                    i = 6;
                }
                else
                {
                    i = 5;
                }

                all = string.Join("|", res, i, res.Length - i);

                cols[8] += all;
            }
            else
            {
                cols[8] = line;
            }
            return cols;
        }

        public IColumnizedLogLine SplitLine(ILogLineColumnizerCallback callback, ILogLine line)
        {
            ColumnizedLogLine columnizedLogLine = new ColumnizedLogLine();
            columnizedLogLine.LogLine = line; // Add the reference to the LogLine 
            Column[] columns = Column.CreateColumns(GetColumnCount(), columnizedLogLine);
            columnizedLogLine.ColumnValues = columns.Select(a => a as IColumn).ToArray();
            String[] tmp = SplitLine(callback, line.FullLine);

            for (int i = 0; i < columns.Length; i++)
            {
                columns[i].FullValue = tmp[i];
            }
            return columnizedLogLine;
        }
        public bool IsTimeshiftImplemented()
        {
            return false;
        }

        public void SetTimeOffset(int msecOffset)
        {
            throw new NotImplementedException(); //Nothing to do here
        }

        public int GetTimeOffset()
        {
            throw new NotImplementedException(); //Nothing to do here
        }

        public DateTime GetTimestamp(ILogLineColumnizerCallback callback, string line)
        {
            throw new NotImplementedException(); //Nothing to do here
        }

        public void PushValue(ILogLineColumnizerCallback callback, int column, string value, string oldValue)
        {
            throw new NotImplementedException(); //Nothing to do here
        }
        public DateTime GetTimestamp(ILogLineColumnizerCallback callback, ILogLine line)
        {
            throw new NotImplementedException();
        }
    }
}