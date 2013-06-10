using System.Collections.Generic;

namespace KWizCom.ResourceTranslator
{
    public class TranslationBatchJob
    {
        public string OutputPath;
        public List<ResourceFile> ResourceFiles;
        public string StatusMessage;
        public TranslationJobStatus Status;
    }
}
