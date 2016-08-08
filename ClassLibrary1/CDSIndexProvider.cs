﻿using ClassLibrary1.Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMLib.Bootstrapping.Interpolation;
using OMLib.Pricing;
using QLNet;
using ClassLibrary1.Commons;
using ClassLibrary1.Models;
namespace ClassLibrary1
{
    public class CDSIndexProvider
    {
        public static YieldTermStructure ISDA_USD_20140213 { get; set; }
           
          //  DateTime TRADE_DATE = new DateTime(2014, 2, 13);
            public static int[] INDEX_TENORS = new int[] { 36, 60, 84, 120};
            public static int[] CDS_TENORS = new int[] { 6, 12, 24, 36, 48, 60, 84, 120};
            public static double CDX_NA_HY_21_RECOVERY_RATE = 0.3;
            public static double CDX_NA_HY_21_COUPON = 0.05;
            //Market data on 13-Feb-2014  
            //Market data on 13-Feb-2014  
            public static double[] CDX_NA_HY_20140213_PRICES = new double[] { 1.0756, 1.0762, 1.0571, 1.0652 };
            public static double[] CDX_NA_HY_20140213_RECOVERY_RATES = new double[] {0.4, 0.4942, 0.4, 0.3929, 0.4922, 0.3976, 0.395, 0.4, 0.3525, 0.37, 0.3929, 0.4, 0.3283, 0.1521, 0.179, 0.4268, 0.4,
                0.425, 0.4, 0.3513, 0.4, 0.4, 0.3625, 0.4, 0.4, 0.4, 0.4, 0.4, 0.4, 0.3973, 0.3, 0.4, 0.314, 0.4, 0.4004, 0.4, 0.4, 0.4, 0.4, 0.3983, 0.2075, 0.2, 0.3733, 0.3944, 0.3983, 0.4, 0.4, 0.4, 0.3929,
                0.3975, 0.4055, 0.2971, 0.4464, 0.2483, 0.3725, 0.426, 0.374, 0.4, 0.3983, 0.3271, 0.38, 0.4, 0.4, 0.4, 0.4, 0.33, 0.4, 0.4, 0.4, 0.3336, 0.3843, 0.3881, 0.2767, 0.4, 0.2157, 0.4, 0.4, 0.4,
                0.5162, 0.4, 0.4054, 0.3857, 0.5063, 0.4, 0.3857, 0.4, 0.3586, 0.3257, 0.399, 0.06, 0.4, 0.2717, 0.3964, 0.4, 0.4, 0.3914, 0.4 };
            public static double[,] CDX_NA_HY_20140213_PAR_SPREADS = new double[97, 8] { {0.0019, 0.0028, 0.0054, 0.0089, 0.0138, 0.0187, 0.0238, 0.0262 },
            {0.0038, 0.0051, 0.0094, 0.0141, 0.0185, 0.0226, 0.027, 0.0287 }, {0.0089, 0.0147, 0.0354, 0.0514, 0.0586, 0.065, 0.0692, 0.0709 },
            {0.0038, 0.0062, 0.0108, 0.0187, 0.027, 0.0344, 0.0421, 0.0449 }, {0.0041, 0.0058, 0.0083, 0.0103, 0.0124, 0.0149, 0.0186, 0.0211 },
            {0.0075, 0.009, 0.0205, 0.0307, 0.0403, 0.049, 0.0569, 0.0587 }, {0.0039, 0.0056, 0.0096, 0.0172, 0.0266, 0.0345, 0.0419, 0.0438 },
            {0.0021, 0.0025, 0.0048, 0.0081, 0.012, 0.0163, 0.0228, 0.0245 }, {0.0024, 0.0032, 0.0062, 0.0114, 0.0174, 0.0235, 0.031, 0.0331 },
            {0.0028, 0.0038, 0.0077, 0.0135, 0.0214, 0.0287, 0.0387, 0.041 }, {0.0011, 0.0014, 0.0027, 0.0046, 0.0079, 0.0108, 0.0163, 0.0184 },
            {0.0029, 0.004, 0.0081, 0.0158, 0.0234, 0.0319, 0.0382, 0.0412 }, {0.0029, 0.0037, 0.0118, 0.0241, 0.0386, 0.0497, 0.0588, 0.061 },
            {0.0453, 0.0506, 0.065, 0.0789, 0.0898, 0.0979, 0.1069, 0.1023 }, {0.1687, 0.1836, 0.2099, 0.227, 0.2349, 0.2445, 0.2397, 0.2314 },
            {0.0036, 0.006, 0.009, 0.012, 0.0153, 0.0194, 0.0245, 0.026 },
            {0.0029, 0.0043, 0.0083, 0.0152, 0.0228, 0.0264, 0.0315, 0.0335 }, {0.004, 0.0065, 0.0096, 0.0127, 0.0149, 0.017, 0.0202, 0.0226 },
            {0.0027, 0.0036, 0.0069, 0.0113, 0.0171, 0.0229, 0.0305, 0.0328 }, {0.0027, 0.0046, 0.0097, 0.0152, 0.023, 0.0301, 0.039, 0.0412 },
            {0.0017, 0.0025, 0.0064, 0.0128, 0.0209, 0.0279, 0.0318, 0.0334 }, {0.0034, 0.0044, 0.0086, 0.0146, 0.0216, 0.0291, 0.0365, 0.0383 },
            {0.0018, 0.0022, 0.004, 0.0068, 0.0106, 0.0147, 0.0193, 0.021 }, {0.0033, 0.0046, 0.0099, 0.0187, 0.0275, 0.0358, 0.0457, 0.0505 },
            {0.0016, 0.0022, 0.0044, 0.0073, 0.0113, 0.0153, 0.0215, 0.0241 }, {0.002, 0.0039, 0.0072, 0.0107, 0.0154, 0.0202, 0.028, 0.0306 },
            {double.NaN, 0.0021, 0.006, 0.0121, 0.0183, 0.0232, 0.03, 0.0319 }, {0.0029, 0.0039, 0.0084, 0.0142, 0.0213, 0.029, 0.0369, 0.0395 },
            {0.0019, 0.0024, 0.0057, 0.0115, 0.0185, 0.0264, 0.0371, 0.0394 }, {0.0024, 0.0032, 0.0068, 0.0112, 0.0172, 0.0233, 0.0301, 0.0319 },
            {0.0046, 0.0069, 0.0166, 0.0277, 0.0391, 0.0495, 0.0576, 0.06 }, {0.0032, 0.0042, 0.0084, 0.0149, 0.0226, 0.0309, 0.0407, 0.0429 },
            {0.0025, 0.0034, 0.0071, 0.0146, 0.0226, 0.0297, 0.0365, 0.0382 }, {0.0094, 0.0127, 0.029, 0.0423, 0.0525, 0.0614, 0.0654, 0.0668 },
            {0.0018, 0.0024, 0.0048, 0.0079, 0.0122, 0.0177, 0.025, 0.0272 }, {0.002, 0.0031, 0.0059, 0.0107, 0.0175, 0.0233, 0.0335, 0.0364 },
            {0.0026, 0.0037, 0.0067, 0.0101, 0.0142, 0.0191, 0.0265, 0.0282 }, {0.0005, 0.0007, 0.0011, 0.0018, 0.0027, 0.0037, 0.0059, 0.0073 },
            {0.0007, 0.0015, 0.0033, 0.0059, 0.009, 0.0135, 0.0198, 0.0223 }, {0.0008, 0.0011, 0.003, 0.0052, 0.0076, 0.0106, 0.0151, 0.0171 },
            {0.0047, 0.0071, 0.0164, 0.0232, 0.0327, 0.0411, 0.051, 0.0534 }, {0.0049, 0.0078, 0.0111, 0.0162, 0.0234, 0.0294, 0.0358, 0.0361 },
            {0.1594, 0.1763, 0.1821, 0.1765, 0.1674, 0.1633, 0.1579, 0.1519 }, {0.004, 0.0052, 0.01, 0.0186, 0.0295, 0.0382, 0.0466, 0.0486 },
            {0.0028, 0.0043, 0.0095, 0.0155, 0.0227, 0.0294, 0.0386, 0.0402 }, {0.0015, 0.0019, 0.0048, 0.008, 0.0118, 0.0164, 0.0215, 0.0234 },
            {0.002, 0.003, 0.0061, 0.0095, 0.014, 0.0187, 0.0241, 0.0263 }, {0.0026, 0.0034, 0.0069, 0.0105, 0.0155, 0.0201, 0.0286, 0.0294 },
            {0.0019, 0.0029, 0.006, 0.0101, 0.0159, 0.0219, 0.0293, 0.0319 }, {0.0018, 0.0024, 0.0054, 0.0098, 0.015, 0.0213, 0.0266, 0.0289 },
            {0.0012, 0.0015, 0.004, 0.007, 0.0104, 0.0142, 0.0211, 0.0229 }, {0.0028, 0.0034, 0.0068, 0.0119, 0.0185, 0.0251, 0.0338, 0.0364 },
            {0.0553, 0.065, 0.0727, 0.0769, 0.0811, 0.0842, 0.0858, 0.0858 }, {0.0043, 0.0067, 0.0171, 0.0292, 0.042, 0.052, 0.0599, 0.0619 },
            {0.0028, 0.0037, 0.0078, 0.0135, 0.0217, 0.0288, 0.039, 0.0414 }, {0.0044, 0.006, 0.0112, 0.0167, 0.0223, 0.0278, 0.0349, 0.0376 },
            {0.0022, 0.0031, 0.0068, 0.0131, 0.0192, 0.0253, 0.0354, 0.0375 }, {0.0013, 0.0018, 0.0044, 0.0083, 0.0124, 0.0168, 0.0241, 0.0257 },
            {0.0009, 0.0016, 0.003, 0.0054, 0.0086, 0.0125, 0.0162, 0.0174 }, {0.0091, 0.0131, 0.0322, 0.0507, 0.0632, 0.0727, 0.0785, 0.0787 },
            {0.001, 0.0017, 0.0031, 0.0059, 0.009, 0.0127, 0.0179, 0.0203 }, {0.0011, 0.0017, 0.0033, 0.0068, 0.0106, 0.0147, 0.021, 0.0234 },
            {0.0013, 0.0017, 0.0041, 0.0071, 0.0113, 0.0154, 0.0224, 0.0243 }, {0.001, 0.0014, 0.0035, 0.0058, 0.009, 0.0123, 0.0163, 0.0192 },
            {0.0025, 0.0034, 0.0098, 0.0184, 0.0263, 0.0336, 0.0413, 0.0428 }, {0.0036, 0.0058, 0.0118, 0.0183, 0.0241, 0.0297, 0.0345, 0.0372 },
            {0.0018, 0.0022, 0.0046, 0.0079, 0.012, 0.0162, 0.0233, 0.0248 }, {0.0041, 0.0052, 0.0086, 0.016, 0.023, 0.0304, 0.0346, 0.036 },
            {0.0016, 0.0023, 0.0039, 0.0071, 0.0117, 0.0168, 0.0213, 0.0211 }, {0.0023, 0.0033, 0.0075, 0.0131, 0.0201, 0.0265, 0.0336, 0.0356 },
            {0.0014, 0.0022, 0.0044, 0.0075, 0.0119, 0.0164, 0.024, 0.0257 }, {0.0051, 0.007, 0.0124, 0.0181, 0.0236, 0.0293, 0.0362, 0.0386 },
            {0.1214, 0.1466, 0.1708, 0.1862, 0.1825, 0.1808, 0.1724, 0.1645 }, {0.0024, 0.0031, 0.0061, 0.0106, 0.0162, 0.0211, 0.0284, 0.0299 },
            {0.0028, 0.0036, 0.0069, 0.0118, 0.0187, 0.0249, 0.032, 0.0348 }, {0.0016, 0.0021, 0.0041, 0.0062, 0.009, 0.0132, 0.0203, 0.0232 },
            {0.0012, 0.0015, 0.0037, 0.0072, 0.0107, 0.0147, 0.0214, 0.0233 }, {0.002, 0.0034, 0.0068, 0.0118, 0.018, 0.0241, 0.0315, 0.0335 },
            {0.0041, 0.0051, 0.0098, 0.0156, 0.0215, 0.0279, 0.036, 0.0395 }, {0.0029, 0.004, 0.0084, 0.0145, 0.0213, 0.0281, 0.0345, 0.0371 },
            {0.0846, 0.0986, 0.1181, 0.1229, 0.1239, 0.1262, 0.1257, 0.1221 }, {0.0026, 0.0035, 0.0068, 0.0112, 0.0164, 0.0217, 0.0318, 0.0333 },
            {0.0051, 0.0073, 0.0124, 0.0192, 0.0248, 0.0304, 0.0358, 0.0378 }, {0.0046, 0.0059, 0.0109, 0.0164, 0.0235, 0.0297, 0.0384, 0.0418 },
            {0.0053, 0.0068, 0.0138, 0.0236, 0.0335, 0.0427, 0.0507, 0.053 }, {0.0029, 0.0044, 0.0085, 0.0145, 0.0211, 0.0291, 0.0363, 0.0386 },
            {0.0661, 0.0946, 0.1263, 0.1364, 0.1385, 0.1409, 0.1389, 0.1352 }, {0.0026, 0.0038, 0.0083, 0.014, 0.02, 0.0258, 0.0339, 0.0367 },
            {0.0028, 0.0036, 0.0069, 0.0118, 0.0174, 0.0236, 0.0287, 0.0303 }, {4.7657, 4.3612, 3.8436, 3.5247, 3.3097, 3.1568, 2.9315, 2.711 },
            {0.0014, 0.0022, 0.0041, 0.006, 0.0093, 0.0119, 0.0154, 0.0167 }, {0.0031, 0.0042, 0.0078, 0.0122, 0.018, 0.025, 0.0296, 0.0316 },
            {0.0016, 0.0028, 0.0059, 0.0095, 0.0145, 0.0197, 0.0271, 0.028 }, {0.0011, 0.0016, 0.003, 0.0059, 0.0094, 0.013, 0.0191, 0.0212 },
            {0.0032, 0.0048, 0.0093, 0.0166, 0.0258, 0.034, 0.0415, 0.0434 }, {0.0032, 0.0047, 0.0099, 0.0205, 0.0315, 0.0414, 0.0501, 0.0529 },
            {0.0023, 0.0033, 0.0083, 0.0152, 0.0238, 0.0309, 0.0368, 0.0384 } };

      public static double CDX_NA_HY_26_RECOVERY_RATE = 0.3;
        public static double CDX_NA_HY_26_COUPON = 0.05;
        public static int[] CDX_Tenor = new int[] { 36, 60, 84, 120 };
        public static int[] CDX_HY_TENORS = new int[] { 6, 12, 24, 36, 48, 60, 84, 120,180,240,360 };
        //Market data on 13-Feb-2014  
        //Market data on 13-Feb-2014  
        public static double[] CDX_NA_HY_20160725_PRICES = new double[] {1.045584693,1.044241262,1.03617517,1.042492283};
        public static double[] CDX_NA_HY_20160725_RECOVERY_RATES = new double[] {/*Already default 0,*/ 0.4,0.4,0.175,0.39,0.41,0.4,0.3975,0.4,0.39875,0.28,0.389,0.4,0.4,0.33,
            0.325,0.394444,0.06775,0.111571,0.4,0.4,0.4,0.4,0.4,0.4,0.4,0.4,0.4,0.4,0.4,0.4,0.4,0.4,0.35,0.4,0.38,0.396667,0.4,0.400009,
            0.405833,0.4,0.4,0.20125,0.069938,0.5135,0.3675,0.398571,0.4,0.4,0.398,0.3925,0.421275,0.256667,0.394,0.4,0.413571,0.4,0.39875,
            0.327453,0.4,0.4,0.4,0.4,0.396667,0.4,0.4,0.4,0.345,0.4,0.394375,0.292819,0.287741,0.4,0.205,0.4,0.378929,0.43,0.394995,0.398333,
            0.4,0.4,0.3625,0.4,0.4,0.4,0.4,0.3875,0.3275,0.4,0.4,0.4,0.375,0.4,0.4,0.4,0.399091,0.325,0.283333,0.4,0.4};
        public static double[,] CDX_NA_HY_20160725_PAR_SPREADS = new double[99, 11] { { 0.0024, 0.0034, 0.0064, 0.0106, 0.0156, 0.0212, 0.0268, 0.0298, 0.0318, 0.0326, 0.0323 }, {0.0052,0.0070,0.0109,0.0142,0.0169,0.0198,0.0234,0.0253,0.0267,0.0273,0.0279} , {0.0072,0.0094,0.0291,0.0447,0.0641,0.0770,0.0847,0.0878,double.NaN,double.NaN,double.NaN
} , {0.0011,0.0016,0.0025,0.0032,0.0041,0.0049,0.0077,0.0092,0.0099,0.0103,0.0107} , {0.0068,0.0089,0.0134,0.0177,0.0211,0.0244,0.0292,0.0314,0.0324,0.0329,0.0330} , {0.0111,0.0137,0.0291,0.0393,0.0458,0.0533,0.0569,0.0606,double.NaN,double.NaN,double.NaN} , {0.0047,0.0054,0.0096,0.0163,0.0237,0.0311,0.0404,0.0434,0.0458,0.0468,0.0467} , {0.0020,0.0026,0.0046,0.0066,0.0084,0.0103,0.0155,0.0176,0.0193,0.0197,0.0203} , {0.0030,0.0042,0.0110,0.0191,0.0293,0.0398,0.0474,0.0493,0.0505,0.0510,0.0509} , {0.0113,0.0250,0.0398,0.0547,0.0666,0.0714,0.0851,0.0829,double.NaN,double.NaN,double.NaN} , {0.0026,0.0035,0.0074,0.0143,0.0218,0.0294,0.0371,0.0397,0.0418,0.0427,0.0431} , {0.0012,0.0015,0.0034,0.0050,0.0076,0.0103,0.0145,0.0170,0.0187,0.0196,0.0205} , {0.0190,0.0248,0.0369,0.0429,0.0517,0.0575,0.0631,0.0652,double.NaN,double.NaN,double.NaN} , {double.NaN,double.NaN,0.0087,0.0129,0.0206,0.0279,0.0356,0.0384,double.NaN,double.NaN,double.NaN} , {0.0076,0.0108,0.0285,0.0424,0.0555,0.0643,0.0777,0.0764,double.NaN,double.NaN,double.NaN} , {0.0019,0.0026,0.0057,0.0092,0.0136,0.0187,0.0259,0.0279,0.0289,0.0293,0.0297} , {0.1984,0.1939,0.1876,0.1803,0.1894,0.1835,0.1597,0.1402,0.1241,0.1157,0.1079} , {0.0518,0.0995,0.1203,0.1242,0.1389,0.1433,0.1318,0.1237,double.NaN,double.NaN,double.NaN} , {0.0022,0.0030,0.0048,0.0075,0.0116,0.0161,0.0196,0.0230,0.0235,0.0237,0.0239} , {0.0057,0.0073,0.0104,0.0136,0.0163,0.0195,0.0228,0.0253,0.0266,0.0273,0.0278} , {0.0025,0.0036,0.0080,0.0142,0.0224,0.0306,0.0371,0.0391,0.0404,0.0407,0.0410} , {0.0036,0.0051,0.0109,0.0184,0.0262,0.0331,0.0385,0.0406,0.0422,0.0429,0.0434} , {0.0054,0.0072,0.0110,0.0168,0.0245,0.0328,double.NaN,double.NaN,double.NaN,double.NaN,double.NaN} , {0.0033,0.0050,0.0113,0.0191,0.0237,0.0289,0.0345,0.0363,0.0375,0.0381,0.0385} , {0.0095,0.0139,0.0267,0.0498,0.0670,0.0793,0.0810,0.0806,double.NaN,double.NaN,double.NaN} , {0.0067,0.0067,0.0067,0.0084,0.0103,0.0113,double.NaN,double.NaN,double.NaN,double.NaN,double.NaN} , {0.0063,0.0071,0.0114,0.0187,0.0274,0.0353,0.0428,0.0471,0.0497,0.0502,0.0513} , {0.0012,0.0013,0.0030,0.0055,0.0075,0.0103,0.0158,0.0175,0.0184,0.0189,0.0192} , {0.0028,0.0045,0.0099,0.0175,0.0245,0.0317,0.0387,0.0412,0.0438,0.0448,0.0456} , {0.0006,0.0010,0.0016,0.0022,0.0033,0.0045,0.0110,0.0136,0.0149,0.0153,0.0154} , {0.0037,0.0047,0.0094,0.0171,0.0259,0.0353,0.0438,0.0462,0.0479,0.0485,0.0488} , {0.0039,0.0043,0.0066,0.0095,0.0140,0.0185,0.0250,0.0276,0.0280,0.0283,0.0281} , {0.0244,0.0265,0.0350,0.0416,0.0469,0.0508,0.0563,0.0588,0.0591,0.0588,0.0584} , {0.0023,0.0024,0.0038,0.0053,0.0079,0.0104,0.0107,0.0113,double.NaN,double.NaN,double.NaN} , {0.0303,0.0267,0.0266,0.0284,0.0304,0.0324,0.0311,0.0295,0.0269,0.0255,0.0239} , {0.0035,0.0054,0.0108,0.0201,0.0294,0.0397,0.0469,0.0490,0.0512,0.0520,0.0511} , {0.0057,0.0063,0.0105,0.0170,0.0255,0.0343,0.0438,0.0452,0.0518,0.0529,0.0532} , {0.0044,0.0062,0.0176,0.0293,0.0423,0.0533,0.0614,0.0640,0.0656,0.0663,0.0670} , {0.0153,0.0287,0.0561,0.0746,0.0825,0.0886,0.0948,0.0971,double.NaN,double.NaN,double.NaN} , {0.0024,0.0032,0.0053,0.0088,0.0135,0.0189,0.0267,0.0283,0.0293,0.0298,0.0293} , {0.0024,0.0044,0.0096,0.0122,0.0141,0.0186,0.0224,0.0251,0.0270,0.0279,0.0287} , {0.0573,0.0763,0.1212,0.1693,0.1682,0.1653,0.1569,0.1442,double.NaN,double.NaN,double.NaN} , {0.2594,0.2787,0.3680,0.3808,0.3636,0.3542,0.3233,0.2935,0.2588,0.2377,0.2178} , {0.0133,0.0184,0.0273,0.0351,0.0416,0.0468,0.0534,0.0552,0.0560,0.0562,0.0563} , {0.0078,0.0111,0.0204,0.0405,0.0523,0.0606,0.0689,0.0711,double.NaN,double.NaN,double.NaN} , {0.0024,0.0038,0.0103,0.0173,0.0238,0.0309,0.0394,0.0407,0.0417,0.0421,0.0420} , {0.0017,0.0022,0.0051,0.0095,0.0139,0.0191,0.0239,0.0263,0.0273,0.0280,0.0286} , {0.0020,0.0025,0.0062,0.0097,0.0131,0.0171,0.0236,0.0258,0.0268,0.0268,0.0271} , {0.0018,0.0025,0.0054,0.0093,0.0143,0.0199,0.0257,0.0277,0.0283,0.0287,0.0279} , {0.0018,0.0022,0.0040,0.0067,0.0100,0.0135,0.0191,0.0214,0.0221,0.0224,0.0223} , {0.2597,0.2787,0.2047,0.1819,0.1669,0.1593,0.1480,0.1378,double.NaN,double.NaN,double.NaN} , {0.0191,0.0239,0.0449,0.0712,0.0866,0.0953,0.1039,0.1000,double.NaN,double.NaN,double.NaN} , {0.0045,0.0078,0.0181,0.0332,0.0453,0.0552,0.0643,0.0680,0.0706,0.0715,0.0717} , {0.0016,0.0024,0.0042,0.0062,0.0089,0.0120,0.0166,0.0174,0.0181,0.0185,0.0188} , {0.0048,0.0065,0.0112,0.0160,0.0208,0.0257,0.0317,0.0341,0.0356,0.0370,0.0373} , {0.0073,0.0121,0.0221,0.0309,0.0405,0.0484,0.0562,0.0603,double.NaN,double.NaN,double.NaN} , {0.0017,0.0022,0.0037,0.0065,0.0087,0.0120,0.0156,0.0171,0.0181,0.0185,0.0184} , {0.0030,0.0039,0.0079,0.0141,0.0203,0.0269,0.0341,0.0372,0.0395,0.0405,0.0393} , {0.0009,0.0011,0.0014,0.0024,0.0037,0.0053,0.0093,0.0109,0.0119,0.0140,0.0138} , {0.0009,0.0011,0.0021,0.0038,0.0057,0.0081,0.0147,0.0176,0.0200,0.0211,0.0200} , {0.0028,0.0037,0.0077,0.0126,0.0201,0.0265,0.0332,0.0365,0.0384,0.0396,0.0400} , {0.0044,0.0070,0.0130,0.0177,0.0249,0.0320,0.0391,0.0419,0.0440,0.0448,0.0447} , {0.0111,0.0161,0.0261,0.0404,0.0479,0.0538,0.0653,0.0678,0.0665,0.0668,0.0669} , {0.0015,0.0021,0.0036,0.0066,0.0104,0.0146,0.0213,0.0232,0.0247,0.0268,0.0251} , {0.0186,0.0379,0.0910,0.1065,0.1165,0.1280,0.1320,0.1330,0.1336,0.1338,0.1339} , {0.0015,0.0022,0.0037,0.0062,0.0099,0.0136,0.0180,0.0158,double.NaN,double.NaN,double.NaN} , {0.0019,0.0022,0.0050,0.0088,0.0136,0.0190,0.0256,0.0272,0.0313,0.0317,0.0303} , {0.0026,0.0032,0.0058,0.0096,0.0148,0.0199,0.0267,0.0303,0.0322,0.0324,0.0316} , {0.0049,0.0063,0.0116,0.0169,0.0222,0.0274,0.0326,0.0343,0.0350,0.0352,0.0354} , {0.0039,0.0062,0.0107,0.0157,0.0211,0.0263,0.0336,0.0353,0.0364,0.0369,0.0373} , {0.0185,0.0181,0.0385,0.0630,0.0826,0.0926,0.0984,0.0945,0.0897,0.0859,0.0809} , {0.0029,0.0051,0.0078,0.0139,0.0182,0.0235,0.0243,0.0268,0.0269,0.0265,0.0257} , {0.0013,0.0016,0.0027,0.0045,0.0064,0.0092,0.0190,0.0218,double.NaN,double.NaN,double.NaN} , {0.0011,0.0013,0.0030,0.0053,0.0079,0.0109,0.0165,0.0191,0.0228,0.0234,0.0232} , {0.1862,0.1972,0.1969,0.2034,0.2090,0.2092,0.2113,0.2093,double.NaN,double.NaN,double.NaN} , {0.0119,0.0152,0.0264,0.0395,0.0455,0.0515,0.0576,0.0588,double.NaN,double.NaN,double.NaN} , {0.0212,0.0229,0.0415,0.0532,0.0612,0.0680,0.0733,0.0757,double.NaN,double.NaN,double.NaN} , {0.0050,0.0072,0.0170,0.0383,0.0569,0.0674,0.0738,0.0761,double.NaN,double.NaN,double.NaN} , {0.0020,0.0028,0.0058,0.0111,0.0167,0.0230,0.0303,0.0323,0.0336,0.0342,0.0345} , {0.0123,0.0194,0.0319,0.0461,0.0605,0.0680,0.0746,0.0764,0.0751,0.0730,0.0704} , {0.0073,0.0124,0.0239,0.0422,0.0558,0.0674,0.0714,0.0726,double.NaN,double.NaN,double.NaN} , {0.0011,0.0016,0.0040,0.0068,0.0108,0.0155,0.0218,0.0247,0.0271,0.0279,0.0287} , {0.0040,0.0114,0.0174,0.0231,0.0269,0.0326,0.0368,0.0392,0.0369,0.0368,0.0367} , {0.0071,0.0091,0.0149,0.0290,0.0409,0.0525,0.0597,0.0614,0.0627,0.0633,0.0635} , {double.NaN,double.NaN,double.NaN,0.0188,0.0215,0.0257,double.NaN,double.NaN,double.NaN,double.NaN,double.NaN} , {0.0042,0.0059,0.0112,0.0192,0.0285,0.0372,0.0428,0.0447,0.0459,0.0464,0.0464} , {0.0383,0.0507,0.0777,0.0926,0.0973,0.1008,0.1025,0.0978,double.NaN,double.NaN,double.NaN} , {0.0038,0.0073,0.0135,0.0223,0.0295,0.0325,0.0367,0.0394,0.0414,0.0426,0.0444} , {0.0037,0.0045,0.0084,0.0145,0.0213,0.0290,0.0371,0.0390,0.0409,0.0432,0.0422} , {0.0010,0.0014,0.0026,0.0041,0.0061,0.0082,0.0117,0.0134,0.0147,0.0152,0.0155} , {0.0145,0.0178,0.0353,0.0452,0.0572,0.0688,0.0751,0.0789,0.0817,0.0831,0.0849} , {0.0019,0.0030,0.0059,0.0119,0.0198,0.0274,0.0347,0.0360,0.0371,0.0376,0.0380} , {0.0369,0.0598,0.0685,0.0708,0.0725,0.0756,double.NaN,double.NaN,double.NaN,double.NaN,double.NaN} , {0.0005,0.0007,0.0015,0.0031,0.0049,0.0069,0.0124,0.0136,0.0139,0.0141,0.0142} , {0.0098,0.0112,0.0228,0.0489,0.0665,0.0758,0.0791,0.0775,double.NaN,double.NaN,double.NaN} , {double.NaN,0.0142,0.0229,0.0336,0.0475,0.0565,0.0632,0.0585,double.NaN,double.NaN,double.NaN} , {0.0046,0.0080,0.0240,0.0445,0.0643,0.0771,0.0855,0.0840,double.NaN,double.NaN,double.NaN} , {0.0050,0.0077,0.0146,0.0231,0.0308,0.0385,0.0448,0.0467,0.0480,0.0485,0.0489} , {0.0018,0.0022,0.0055,0.0091,0.0134,0.0183,0.0253,0.0279,0.0299,0.0309,0.0316 }};

        public static PiecewiseconstantHazardRate[] getCDX_NA_HY_20140213_CreditCurves()
        {
            DateTime tradeDate = new DateTime(2014, 2, 13);
            return buildCreditCurves(tradeDate, CDX_NA_HY_20140213_PAR_SPREADS, CDX_NA_HY_20140213_RECOVERY_RATES, CDS_TENORS, ISDA_USD_20140213);
        }
        public static PiecewiseconstantHazardRate[] getCDX_NA_HY_20160725_CreditCurves()
        {
            DateTime tradeDate = new DateTime(2016, 7, 25);
            return buildCreditCurves(tradeDate, CDX_NA_HY_20160725_PAR_SPREADS, CDX_NA_HY_20160725_RECOVERY_RATES, CDX_HY_TENORS, ISDA_USD_20140213);
        }

        public static PiecewiseconstantHazardRate[] buildCreditCurves(DateTime tradeDate, double[,] parSpreads, double[] recoveryRates, int[] tenors,
          YieldTermStructure yieldCurve)
        {
            CdsAnalyticFactory factory = new CdsAnalyticFactory(0.0);
            CDS[] pillarCDS = factory.makeImmCds(tradeDate, CDX_HY_TENORS);
            int indexSize = parSpreads.GetLength(0);
            PiecewiseconstantHazardRate[] creditCurves = new PiecewiseconstantHazardRate[indexSize];

            //this section of code is hugely wasteful. If we do this for real (i.e. not just in a test), must improve  
            
            for (int i = 0; i < indexSize; i++)
            {
                int m = parSpreads.GetLength(1);
                double[] spreads = new double[m];
                for (int j = 0; j < m; j++)
                {
                    spreads[j] = parSpreads[i, j];
                }
                int nPillars = spreads.Length;
                CDS[] tempCDS = new CDS[nPillars];
                double[] tempSpreads = new double[nPillars];
                int count = 0;
                for (int j = 0; j < nPillars; j++)
                {
                    if (!double.IsNaN(parSpreads[i,j]))
                    {
                        tempCDS[count] = pillarCDS[j].withRecoveryRate(recoveryRates[i]);
                        tempSpreads[count] = spreads[j];
                        count++;
                    }
                }

                CDS[] calCDS = null;
                double[] calSpreads = null;
                if (count == nPillars)
                {
                    calCDS = tempCDS;
                    calSpreads = tempSpreads;
                }
                else
                {
                    calCDS = new CDS[count];
                    calSpreads = new double[count];
                    Array.Copy(tempCDS, 0, calCDS, 0, count);
                    Array.Copy(tempSpreads, 0, calSpreads, 0, count);
                }

                CreditCurveCalibrator calibrator = new CreditCurveCalibrator(calCDS, yieldCurve);
                creditCurves[i] = calibrator.calibrate(calSpreads);
               
            }
            return creditCurves;
        }
        public YieldTermStructure makeUSDCurve(DateTime tradeDate, double[] rates)
        {
            DateTime spotDate = tradeDate.AddDays(2);
            InterestRateCurve IRC = new InterestRateCurve();
            YieldTermStructure yt = new YieldTermStructure(tradeDate);

            yt = IRC.calculation2(tradeDate, rates.ToList());
            return yt;
        }
    }
}