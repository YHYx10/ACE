using System.ComponentModel.DataAnnotations;

namespace ServerGo.Casino.Business
{
    public enum CashBoxState
    {
        [Display(Name= "Under the threat of seizure")]
        UnderThreat,
        
        [Display(Name= "Safe")]
        Safe
    }
}