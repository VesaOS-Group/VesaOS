using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.Core
{
    public class Process
    {
        public int ProgramID;
        public ProgramType PType;
        public List<Process> ChildProcesses;

    }
}
