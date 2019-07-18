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

        public int GetColumnCount()
        {
            return 7;
        }

        public string[] GetColumnNames()
        {
            return new string[] { "Date", "Time", "PID", "#", "Level", "Thread", "Message" };
        }

        public string[] SplitLine(ILogLineColumnizerCallback callback, string line)
        {
            string[] cols = new string[7] { "", "", "", "", "", "", ""};

            String[] res = Regex.Split(line, "\\|");

            if (res.Length >= 5)
            {
                String[] dateTime = Regex.Split(res[0], "\\s");
                cols[0] = dateTime[0];
                cols[1] = dateTime[1];
                cols[2] = res[1];

                cols[3] = res[2]; // 1
                cols[4] = res[3]; // Level
                cols[5] = res[4]; // Thread
                string all = string.Join("|", res, 5, res.Length-5);
                cols[6] = all;

            }
            else
            {
                cols[6] = line;
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