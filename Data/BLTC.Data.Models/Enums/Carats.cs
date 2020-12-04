namespace BLTC.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum Carats
    {
        Undefined = 0,
        [Display(Name = "8 (33.3%)")]
        Eight = 333,
        [Display(Name = "9 (37.5%)")]
        Nine = 375,
        [Display(Name = "10 (41.7%)")]
        Ten = 417,
        [Display(Name = "14 (58.5%)")]
        Fourteen = 585,
        [Display(Name = "15 (62.5%)")]
        Fifteen = 625,
        [Display(Name = "18 (75.0%)")]
        Eigteen = 750,
        [Display(Name = "20 (83.4%)")]
        Twenty = 834,
        [Display(Name = "21.6 (90.0%)")]
        TwentyOne = 900,
        [Display(Name = "22 (91.6%)")]
        TwentyTwo = 916,
        [Display(Name = "23 (95.83%)")]
        TwentyThree = 958,
        [Display(Name = "24 (99.9%)")]
        TwentyFour = 999,
    }
}
