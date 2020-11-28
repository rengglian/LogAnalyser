using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceAnalysis.ViewModels
{
    public class SequenceAnalysisViewModel : BindableBase
    {
        public DelegateCommand OpenSequenceCommand { get; set; }
        public SequenceAnalysisViewModel()
        {

            OpenSequenceCommand = new DelegateCommand(OpenSequenceHandler);
        }

        private void OpenSequenceHandler()
        {
            throw new NotImplementedException();
        }
    }
}
