namespace vimac.ProcessingCommands
{
    public interface IProcessingCommand
    {
        bool ShouldProcess(string[] argumentFlags);
        void Execute(ProcessingOptions processingOptions);
    }
}
