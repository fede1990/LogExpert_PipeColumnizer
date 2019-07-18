# LogExpert_PipeColumnizer
Plugin for LogExpert to split log lines by pipe

Short summary:

1.   You have to provide a DLL written in some .NET language. 

2.   You have to implement the ILogLineColumnizer interface exported by the ColumnizerLib DLL. 

3.   Put the DLL into a subdirectory of LogExpert called 'plugins'

I recommend to keep the end of the class name with "Columnizer" in order to be loaded by the LogExpert PluginLoader and be visible in the appropriate menu.
