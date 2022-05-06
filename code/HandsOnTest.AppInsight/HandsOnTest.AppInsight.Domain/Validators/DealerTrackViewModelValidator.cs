using FluentValidation;
using HandsOnTest.AppInsight.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnTest.AppInsight.Domain.Validators
{
    public class DealerTrackViewModelValidator : AbstractValidator<DealerTrackViewModel>
    {
        public DealerTrackViewModelValidator()
        {
            RuleFor(f => f.File).SetValidator(new CsvFileValidator());

        }
    }
}
