﻿namespace BipEmulator.Host
{
    public struct SymbolSet
    {
        public static (int, int) GetResource(int symbolSet, char value)
        {
            if (symbolSet > 15)
                return (-1, - 1);

            var offsetSymbols = 0;
            for (var i = 0; i < symbolSet; i++)
                offsetSymbols += SymbolSetLengths[i];

            for (var i = 0; i < SymbolSetLengths[symbolSet]; i++)
                if (Preset[offsetSymbols + i].Value == value)
                    return (Preset[offsetSymbols + i].ResId, Preset[offsetSymbols + i].Width);
            return (-1, -1);
        }

        private static int[] SymbolSetLengths = { 10, 10, 14, 15, 10, 75, 75, 11, 16, 12, 16, 16, 20, 11, 23, 14 };

        private static Symbol[] Preset = new Symbol[]
        {
            // 0 10
            new Symbol ('0', 11, 28),
            new Symbol ('1', 11, 29),
            new Symbol ('2', 11, 30),
            new Symbol ('3', 11, 31),
            new Symbol ('4', 11, 32),
            new Symbol ('5', 11, 33),
            new Symbol ('6', 11, 34),
            new Symbol ('7', 11, 35),
            new Symbol ('8', 11, 36),
            new Symbol ('9', 11, 37),
            // 1 10
            new Symbol ('0', 8, 170),
            new Symbol ('1', 8, 171),
            new Symbol ('2', 8, 172),
            new Symbol ('3', 8, 173),
            new Symbol ('4', 8, 174),
            new Symbol ('5', 8, 175),
            new Symbol ('6', 8, 176),
            new Symbol ('7', 8, 177),
            new Symbol ('8', 8, 178),
            new Symbol ('9', 8, 179),
            // 2 14
            new Symbol ((char)0x22, 10, 582),
            new Symbol ((char)0x27, 4, 583),
            new Symbol ('.', 4, 580),
            new Symbol ('0', 14, 584),
            new Symbol ('1', 14, 585),
            new Symbol ('2', 14, 586),
            new Symbol ('3', 14, 587),
            new Symbol ('4', 14, 588),
            new Symbol ('5', 14, 589),
            new Symbol ('6', 14, 590),
            new Symbol ('7', 14, 591),
            new Symbol ('8', 14, 592),
            new Symbol ('9', 14, 593),
            new Symbol (':', 4, 581),
            // 3 15
            new Symbol ((char)0xb0, 12, 594),
            new Symbol ('-', 13, 595),
            new Symbol ('.', 6, 596),
            new Symbol ('/', 12, 597),
            new Symbol ('0', 17, 599),
            new Symbol ('1', 17, 600),
            new Symbol ('2', 17, 601),
            new Symbol ('3', 17, 602),
            new Symbol ('4', 17, 603),
            new Symbol ('5', 17, 604),
            new Symbol ('6', 17, 605),
            new Symbol ('7', 17, 606),
            new Symbol ('8', 17, 607),
            new Symbol ('9', 17, 608),
            new Symbol (':', 6, 598),
            // 4 10
            new Symbol ('0', 17, 609),
            new Symbol ('1', 17, 610),
            new Symbol ('2', 17, 611),
            new Symbol ('3', 17, 612),
            new Symbol ('4', 17, 613),
            new Symbol ('5', 17, 614),
            new Symbol ('6', 17, 615),
            new Symbol ('7', 17, 616),
            new Symbol ('8', 17, 617),
            new Symbol ('9', 17, 618),
            // 5 75
            new Symbol ('0', 9, 619),
            new Symbol ('1', 9, 620),
            new Symbol ('2', 9, 621),
            new Symbol ('3', 9, 622),
            new Symbol ('4', 9, 623),
            new Symbol ('5', 9, 624),
            new Symbol ('6', 9, 625),
            new Symbol ('7', 9, 626),
            new Symbol ('8', 9, 627),
            new Symbol ('9', 9, 628),
            new Symbol ('-', 6, 629),
            new Symbol ('.', 2, 630),
            new Symbol ('/', 6, 631),
            new Symbol (':', 2, 632),
            new Symbol ('A', 15, 633),
            new Symbol ('B', 10, 634),
            new Symbol ('C', 11, 635),
            new Symbol ('D', 11, 636),
            new Symbol ('E', 10, 637),
            new Symbol ('F', 10, 638),
            new Symbol ('G', 12, 639),
            new Symbol ('H', 11, 640),
            new Symbol ('I', 6, 641),
            new Symbol ('J', 7, 642),
            new Symbol ('K', 12, 643),
            new Symbol ('L', 10, 644),
            new Symbol ('M', 14, 645),
            new Symbol ('N', 13, 646),
            new Symbol ('O', 13, 647),
            new Symbol ('P', 12, 648),
            new Symbol ('Q', 13, 649),
            new Symbol ('R', 11, 650),
            new Symbol ('S', 10, 651),
            new Symbol ('T', 12, 652),
            new Symbol ('U', 12, 653),
            new Symbol ('V', 14, 654),
            new Symbol ('W', 14, 655),
            new Symbol ('X', 15, 656),
            new Symbol ('Y', 14, 657),
            new Symbol ('Z', 13, 658),
            new Symbol ('a', 9, 659),
            new Symbol ('b', 9, 660),
            new Symbol ('c', 8, 661),
            new Symbol ('d', 9, 662),
            new Symbol ('e', 9, 663),
            new Symbol ('f', 7, 664),
            new Symbol ('g', 9, 665),
            new Symbol ('h', 8, 666),
            new Symbol ('i', 2, 667),
            new Symbol ('j', 5, 668),
            new Symbol ('k', 9, 669),
            new Symbol ('l', 2, 670),
            new Symbol ('m', 14, 671),
            new Symbol ('n', 9, 672),
            new Symbol ('o', 9, 673),
            new Symbol ('p', 9, 674),
            new Symbol ('q', 9, 675),
            new Symbol ('r', 7, 676),
            new Symbol ('s', 8, 677),
            new Symbol ('t', 7, 678),
            new Symbol ('u', 9, 679),
            new Symbol ('v', 9, 680),
            new Symbol ('w', 14, 681),
            new Symbol ('x', 9, 682),
            new Symbol ('y', 9, 683),
            new Symbol ('z', 9, 684),
            new Symbol ((char)0xa5, 8, 975),
            new Symbol ('#', 12, 978),
            new Symbol ((char)0x24, 8, 979),
            new Symbol ('%', 14, 980),
            new Symbol ('(', 7, 981),
            new Symbol (')', 7, 982),
            new Symbol ('@', 14, 983),
            new Symbol (' ', 1, 977),
            new Symbol ('?', 13, 976),
            // 6 75
            new Symbol ('0', 12, 685),
            new Symbol ('1', 12, 686),
            new Symbol ('2', 12, 687),
            new Symbol ('3', 12, 688),
            new Symbol ('4', 12, 689),
            new Symbol ('5', 12, 690),
            new Symbol ('6', 12, 691),
            new Symbol ('7', 12, 692),
            new Symbol ('8', 12, 693),
            new Symbol ('9', 12, 694),
            new Symbol ('-', 7, 695),
            new Symbol ('.', 3, 696),
            new Symbol ('/', 9, 697),
            new Symbol (':', 3, 698),
            new Symbol ('A', 16, 699),
            new Symbol ('B', 15, 700),
            new Symbol ('C', 16, 701),
            new Symbol ('D', 16, 702),
            new Symbol ('E', 13, 703),
            new Symbol ('F', 13, 704),
            new Symbol ('G', 16, 705),
            new Symbol ('H', 15, 706),
            new Symbol ('I', 9, 707),
            new Symbol ('J', 9, 708),
            new Symbol ('K', 17, 709),
            new Symbol ('L', 12, 710),
            new Symbol ('M', 19, 711),
            new Symbol ('N', 16, 712),
            new Symbol ('O', 17, 713),
            new Symbol ('P', 14, 714),
            new Symbol ('Q', 17, 715),
            new Symbol ('R', 15, 716),
            new Symbol ('S', 14, 717),
            new Symbol ('T', 15, 718),
            new Symbol ('U', 15, 719),
            new Symbol ('V', 20, 720),
            new Symbol ('W', 19, 721),
            new Symbol ('X', 18, 722),
            new Symbol ('Y', 19, 723),
            new Symbol ('Z', 17, 724),
            new Symbol ('a', 13, 725),
            new Symbol ('b', 12, 726),
            new Symbol ('c', 12, 727),
            new Symbol ('d', 12, 728),
            new Symbol ('e', 13, 729),
            new Symbol ('f', 9, 730),
            new Symbol ('g', 12, 731),
            new Symbol ('h', 12, 732),
            new Symbol ('i', 3, 733),
            new Symbol ('j', 7, 734),
            new Symbol ('k', 14, 735),
            new Symbol ('l', 3, 736),
            new Symbol ('m', 21, 737),
            new Symbol ('n', 12, 738),
            new Symbol ('o', 13, 739),
            new Symbol ('p', 13, 740),
            new Symbol ('q', 13, 741),
            new Symbol ('r', 9, 742),
            new Symbol ('s', 12, 743),
            new Symbol ('t', 9, 744),
            new Symbol ('u', 12, 745),
            new Symbol ('v', 16, 746),
            new Symbol ('w', 21, 747),
            new Symbol ('x', 15, 748),
            new Symbol ('y', 14, 749),
            new Symbol ('z', 12, 750),
            new Symbol ((char)0xa5, 11, 984),
            new Symbol ('#', 12, 987),
            new Symbol ((char)0x24, 12, 988),
            new Symbol ('%', 16, 989),
            new Symbol ('(', 8, 990),
            new Symbol (')', 8, 991),
            new Symbol ('@', 16, 992),
            new Symbol (' ', 1, 986),
            new Symbol ('?', 17, 985),
            // 7 11
            new Symbol ('0', 20, 751),
            new Symbol ('1', 20, 752),
            new Symbol ('2', 20, 753),
            new Symbol ('3', 20, 754),
            new Symbol ('4', 20, 755),
            new Symbol ('5', 20, 756),
            new Symbol ('6', 20, 757),
            new Symbol ('7', 20, 758),
            new Symbol ('8', 20, 759),
            new Symbol ('9', 20, 760),
            new Symbol (':', 5, 761),
            // 8 16
            new Symbol ('0', 12, 762),
            new Symbol ('1', 10, 763),
            new Symbol ('2', 12, 764),
            new Symbol ('3', 12, 765),
            new Symbol ('4', 14, 766),
            new Symbol ('5', 11, 767),
            new Symbol ('6', 12, 768),
            new Symbol ('7', 12, 769),
            new Symbol ('8', 12, 770),
            new Symbol ('9', 12, 771),
            new Symbol (':', 4, 772),
            new Symbol ((char)0x22, 10, 773),
            new Symbol ((char)0x27, 4, 774),
            new Symbol ('-', 11, 775),
            new Symbol ('.', 4, 776),
            new Symbol ((char)0xb0, 9, 777),
            // 9 12
            new Symbol ('0', 18, 778),
            new Symbol ('1', 18, 779),
            new Symbol ('2', 18, 780),
            new Symbol ('3', 18, 781),
            new Symbol ('4', 18, 782),
            new Symbol ('5', 18, 783),
            new Symbol ('6', 18, 784),
            new Symbol ('7', 18, 785),
            new Symbol ('8', 18, 786),
            new Symbol ('9', 18, 787),
            new Symbol (':', 6, 788),
            new Symbol ('/', 12, 789),
            // 10 16
            new Symbol ('0', 10, 790),
            new Symbol ('1', 10, 791),
            new Symbol ('2', 10, 792),
            new Symbol ('3', 10, 793),
            new Symbol ('4', 10, 794),
            new Symbol ('5', 10, 795),
            new Symbol ('6', 10, 796),
            new Symbol ('7', 10, 797),
            new Symbol ('8', 10, 798),
            new Symbol ('9', 10, 799),
            new Symbol (':', 2, 800),
            new Symbol ((char)0x22, 6, 801),
            new Symbol ((char)0x27, 2, 802),
            new Symbol ('-', 10, 803),
            new Symbol ('.', 2, 804),
            new Symbol ('/', 9, 805),
            // 11 16
            new Symbol ('0', 9, 806),
            new Symbol ('1', 9, 807),
            new Symbol ('2', 9, 808),
            new Symbol ('3', 9, 809),
            new Symbol ('4', 9, 810),
            new Symbol ('5', 9, 811),
            new Symbol ('6', 9, 812),
            new Symbol ('7', 9, 813),
            new Symbol ('8', 9, 814),
            new Symbol ('9', 9, 815),
            new Symbol (':', 7, 816),
            new Symbol ('-', 5, 817),
            new Symbol ('~', 10, 818),
            new Symbol ((char)0xb0, 4, 819),
            new Symbol ((char)0, 0, 0),
            new Symbol ((char)0, 0, 0),
            // 12 20
            new Symbol ('0', 8, 820),
            new Symbol ('1', 6, 821),
            new Symbol ('2', 8, 822),
            new Symbol ('3', 8, 823),
            new Symbol ('4', 8, 824),
            new Symbol ('5', 8, 825),
            new Symbol ('6', 8, 826),
            new Symbol ('7', 8, 827),
            new Symbol ('8', 8, 828),
            new Symbol ('9', 8, 829),
            new Symbol ('/', 8, 830),
            new Symbol ((char)0x4e00, 12, 831),
            new Symbol ((char)0x4e8c, 12, 832),
            new Symbol ((char)0x4e09, 12, 833),
            new Symbol ((char)0x4e94, 12, 834),
            new Symbol ((char)0x56db, 12, 835),
            new Symbol ((char)0x516d, 12, 836),
            new Symbol ((char)0x65e5, 12, 837),
            new Symbol ((char)0x5468, 12, 838),
            new Symbol ((char)0x9031, 15, 839),
            // 13 11
            new Symbol ('0', 6, 840),
            new Symbol ('1', 5, 841),
            new Symbol ('2', 6, 842),
            new Symbol ('3', 6, 843),
            new Symbol ('4', 6, 844),
            new Symbol ('5', 6, 845),
            new Symbol ('6', 6, 846),
            new Symbol ('7', 6, 847),
            new Symbol ('8', 6, 848),
            new Symbol ('9', 6, 849),
            new Symbol ('/', 6, 850),
            // 14 23
            new Symbol ('0', 7, 851),
            new Symbol ('1', 7, 852),
            new Symbol ('2', 7, 853),
            new Symbol ('3', 7, 854),
            new Symbol ('4', 7, 855),
            new Symbol ('5', 7, 856),
            new Symbol ('6', 7, 857),
            new Symbol ('7', 7, 858),
            new Symbol ('8', 7, 859),
            new Symbol ('9', 7, 860),
            new Symbol (':', 1, 861),
            new Symbol ('-', 7, 863),
            new Symbol ('/', 6, 864),
            new Symbol (' ', 1, 862),
            new Symbol ((char)0x4e00, 11, 865),
            new Symbol ((char)0x4e8c, 11, 866),
            new Symbol ((char)0x4e09, 11, 867),
            new Symbol ((char)0x4e94, 11, 868),
            new Symbol ((char)0x56db, 11, 869),
            new Symbol ((char)0x516d, 11, 870),
            new Symbol ((char)0x65e5, 11, 871),
            new Symbol ((char)0x5468, 11, 872),
            new Symbol ((char)0x9031, 14, 873),
            // 15 14
            new Symbol ('0', 7, 874),
            new Symbol ('1', 7, 875),
            new Symbol ('2', 7, 876),
            new Symbol ('3', 7, 877),
            new Symbol ('4', 7, 878),
            new Symbol ('5', 7, 879),
            new Symbol ('6', 7, 880),
            new Symbol ('7', 7, 881),
            new Symbol ('8', 7, 882),
            new Symbol ('9', 7, 883),
            new Symbol ('-', 7, 884),
            new Symbol ('/', 7, 885),
            new Symbol ('~', 7, 886),
            new Symbol ((char)0x2103, 7, 887),
        };
    }

    public struct Symbol
    {
        public char Value { get; set; }
        public int Width { get; set; }
        public int ResId { get; set; }

        public Symbol(char value, int width, int resId)
        {
            Value = value;
            Width = width;
            ResId = resId;
        }
    }
}
