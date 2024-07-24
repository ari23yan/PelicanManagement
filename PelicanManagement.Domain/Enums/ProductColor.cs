using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Enums
{
    public enum ProductColor
    {
        [Display(Name = "قرمز")]
        Red,
        [Display(Name = "سبز")]
        Green,
        [Display(Name = "آبی")]
        Blue,
        [Display(Name = "زرد")]
        Yellow,
        [Display(Name = "سیاه")]
        Black,
        [Display(Name = "سفید")]
        White,
        [Display(Name = "نارنجی")]
        Orange,
        [Display(Name = "بنفش")]
        Purple,
        [Display(Name = "صورتی")]
        Pink,
        [Display(Name = "قهوه‌ای")]
        Brown,
        [Display(Name = "خاکستری")]
        Gray,
        [Display(Name = "فیروزه‌ای")]
        Cyan,
        [Display(Name = "قرمز زرد")]
        Magenta,
        [Display(Name = "فیروزه‌ای روشن")]
        Teal,
        [Display(Name = "قهوه‌ای تیره")]
        Maroon,
        [Display(Name = "زیتونی")]
        Olive,
        [Display(Name = "نیوی")]
        Navy,
        [Display(Name = "نیلی")]
        Indigo,
        [Display(Name = "فیروزه‌ای روشن")]
        Turquoise,
        [Display(Name = "بنفش مایل به آبی")]
        Violet,
        [Display(Name = "لاوندر")]
        Lavender,
        [Display(Name = "هلویی")]
        Peach,
        [Display(Name = "طلایی")]
        Gold,
        [Display(Name = "نقره‌ای")]
        Silver,
        [Display(Name = "بژ")]
        Beige,
        [Display(Name = "کرمی")]
        Cream,
        [Display(Name = "آبی آسمانی")]
        SkyBlue,
        [Display(Name = "خاکستری مایل به آبی")]
        Slate,
        [Display(Name = "زغالی")]
        Charcoal,
        [Display(Name = "کهربایی")]
        Amber,
        [Display(Name = "لیلاک")]
        Lilac,
        [Display(Name = "خرمایی")]
        Mustard,
        [Display(Name = "آبی کمرنگ")]
        Periwinkle,
        [Display(Name = "نارنجی زرد")]
        Tangerine,
        [Display(Name = "زرد زعفرانی")]
        Turmeric,
        [Display(Name = "آتشین")]
        Vermilion,
        [Display(Name = "آبی دریایی")]
        Aquamarine,
        [Display(Name = "کبالتی")]
        Cobalt,
        [Display(Name = "قرمز تیره")]
        Crimson,
        [Display(Name = "صورتی روشن")]
        Fuchsia,
        [Display(Name = "سبز زیتونی")]
        OliveGreen,
        [Display(Name = "آبی طلایی")]
        PeacockBlue,
        [Display(Name = "سبز صورتی")]
        PineGreen,
        [Display(Name = "قرمز مایل به کرم")]
        CoralRed,
        [Display(Name = "خاکی")]
        Taupe,
        [Display(Name = "ارکید")]
        Orchid,
        [Display(Name = "پیازی")]
        Pistachio,
        [Display(Name = "آبی فولادی")]
        SteelBlue,
        [Display(Name = "بادامی")]
        Eggplant,
        [Display(Name = "آبی پادشاهی")]
        RoyalBlue,
        [Display(Name = "سبز نعنایی")]
        MintGreen,
        [Display(Name = "آبی تیره")]
        TealBlue,
        [Display(Name = "بنفش تیره")]
        PlumPurple,
        [Display(Name = "گلابی گرد")]
        DustyRose,
        [Display(Name = "قرمز شرابی")]
        WineRed,
        [Display(Name = "آبی نیمه شب")]
        MidnightBlue,
        [Display(Name = "سبز جنگلی")]
        ForestGreen,
        [Display(Name = "قرمز گوجه فرنگی")]
        TomatoRed,
        [Display(Name = "زرد لیمویی")]
        LemonYellow,
        [Display(Name = "آبی کمرنگ")]
        BabyBlue,
        [Display(Name = "بنفش لاوندر")]
        LavenderPurple,
        [Display(Name = "صورتی کودکانه")]
        BabyPink,
        [Display(Name = "سبز جنگلی")]
        DesertSand,
        [Display(Name = "آبی بنفش")]
        SkyGray,
        [Display(Name = "آبی اقیانوس")]
        OceanBlue,
        [Display(Name = "زرد آفتابی")]
        SunflowerYellow,
        [Display(Name = "بنفش تیره")]
        DeepPurple,
        [Display(Name = "قهوه‌ای تیره")]
        DarkBrown,
        [Display(Name = "سبز کودکانه")]
        BabyGreen,
        [Display(Name = "قرمز گلخانه ای")]
        BrickRed,
        [Display(Name = "آبی الکتریکی")]
        ElectricBlue,
        [Display(Name = "قهوه‌ای شنی")]
        SandyBrown,
        [Display(Name = "بنفش کودکانه")]
        BabyPurple,
        [Display(Name = "سفید خامه‌ای")]
        CreamyWhite,
        [Display(Name = "صورتی توت فرنگی")]
        RaspberryPink,
        [Display(Name = "قهوه‌ای قهوه‌ای")]
        CoffeeBrown,
        [Display(Name = "سبز نئون")]
        NeonGreen,
        [Display(Name = "آبی تیره")]
        DarkBlue,
        [Display(Name = "سبز دریایی")]
        SeaGreen,
        [Display(Name = "سبز تیره")]
        DarkGreen,
        [Display(Name = "آبی روشن")]
        LightBlue,
        [Display(Name = "سبز روشن")]
        LightGreen,
        [Display(Name = "صورتی نئون")]
        NeonPink,
        [Display(Name = "زرد نئون")]
        NeonYellow,
        [Display(Name = "نارنجی نئون")]
        NeonOrange,
        [Display(Name = "آبی نئون")]
        NeonBlue,
        [Display(Name = "بنفش نئون")]
        NeonPurple,
        [Display(Name = "قرمز نئون")]
        NeonRed,
        [Display(Name = "سبز زرد نئون")]
        NeonGreenYellow,
        [Display(Name = "سبز آبی نئون")]
        NeonGreenBlue,
        [Display(Name = "سبز نارنجی نئون")]
        NeonGreenOrange,
        [Display(Name = "زرد نارنجی نئون")]
        NeonYellowOrange,
        [Display(Name = "زرد آبی نئون")]
        NeonYellowBlue,
        [Display(Name = "نارنجی آبی نئون")]
        NeonOrangeBlue,
        [Display(Name = "صورتی بنفش نئون")]
        NeonPinkPurple,
        [Display(Name = "صورتی آبی نئون")]
        NeonPinkBlue,
        [Display(Name = "صورتی نارنجی نئون")]
        NeonPinkOrange,
        [Display(Name = "بنفش آبی نئون")]
        NeonPurpleBlue,
        [Display(Name = "بنفش نارنجی نئون")]
        NeonPurpleOrange,
        [Display(Name = "بنفش آبی نئون")]
        NeonBlueOrange,
        [Display(Name = "سبز زرد نارنجی نئون")]
        NeonGreenYellowOrange,
        [Display(Name = "سبز زرد نارنجی آبی نئون")]
        NeonGreenYellowOrangeBlue,
        [Display(Name = "سبز آبی نارنجی آبی نئون")]
        NeonGreenBlueOrangeBlue,
        [Display(Name = "سبز آبی نارنجی بنفش نئون")]
        NeonGreenBlueOrangePurple,
        [Display(Name = "سبز بنفش نارنجی نئون")]
        NeonGreenPurpleOrange,
        [Display(Name = "سبز بنفش نارنجی آبی نئون")]
        NeonGreenPurpleOrangeBlue,
        [Display(Name = "سبز بنفش آبی نارنجی نئون")]
        NeonGreenPurpleBlueOrange,
        [Display(Name = "سبز بنفش آبی نارنجی بنفش نئون")]
        NeonGreenPurpleBlueOrangePurple,
        [Display(Name = "سبز بنفش آبی نارنجی بنفش آبی نئون")]
        NeonGreenPurpleBlueOrangePurpleBlue,
        [Display(Name = "سبز بنفش آبی نارنجی بنفش آبی زرد نئون")]
        NeonGreenPurpleBlueOrangePurpleBlueYellow,
    }
}
