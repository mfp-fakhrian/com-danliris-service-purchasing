﻿using System;
using System.Collections.Generic;

namespace Com.DanLiris.Service.Purchasing.Lib.Enums
{
    public static class COAGenerator
    {
        public const string HUTANG_USAHA_LOKAL = "3010";
        public const string HUTANG_USAHA_IMPOR = "3020";

        public const string HUTANG_USAHA_OPERASIONAL = "01";
        public const string HUTANG_USAHA_INVESTASI = "02";

        public const string DIVISI_SPINNING = "1";
        public const string UNIT_SPINNING1 = "01";
        public const string UNIT_SPINNING2 = "02";
        public const string UNIT_SPINNING3 = "03";
        public const string UNIT_SPINNINGMS = "04";

        public const string DIVISI_WEAVING = "2";
        public const string UNIT_WEAVING1 = "01";
        public const string UNIT_WEAVING2 = "02";

        public const string DIVISI_FINISHING_PRINTING = "3";
        public const string UNIT_FINISHING = "01";
        public const string UNIT_PRINTING = "02";

        public const string DIVISI_GARMENT = "4";
        public const string UNI_CENTRAL1A = "01";
        public const string UNI_CENTRAL1B = "02";
        public const string UNI_CENTRAL2A = "03";
        public const string UNI_CENTRAL2B = "04";
        public const string UNI_CENTRAL2C = "05";

        public const string PEMBELIAN_BAHAN_BAKU = "5901";
        public const string PEMBELIAN_BARANG_JADI = "5902";
        public const string PEMBELIAN_BAHAN_PEMBANTU = "5903";
        public const string PEMBELIAN_BAHAN_EMBALASE = "5904";
        public const string PEMBELIAN_BARANG_DAGANGAN = "5906";

        public const string PPH23_YMH = "3330";
        public const string PPH_FINAL = "3331";
        public const string PPH21_YMH = "3340";
        public const string PPH26_YMH = "3350";

        public const string PERSEDIAAN_BAHAN_BAKU = "1403";
        public const string PERSEDIAAN_BARANG_JADI = "1401";

        public static string GetDebtCOA(bool isImport, string division, string unitCode)
        {
            var result = "";

            if (isImport)
                result += HUTANG_USAHA_IMPOR + "." + HUTANG_USAHA_OPERASIONAL;
            else
                result += HUTANG_USAHA_LOKAL + "." + HUTANG_USAHA_OPERASIONAL;

            result += "." + GetDivisionAndUnitCOACode(division, unitCode);

            return result;
        }

        public static string GetDivisionAndUnitCOACode(string division, string unitCode)
        {
            var result = "";
            switch (division.ToUpper().Replace(" ", ""))
            {
                case "SPINNING":
                    result = DIVISI_SPINNING;
                    switch (unitCode)
                    {
                        case "S1":
                            result += "." + UNIT_SPINNING1;
                            break;
                        case "S2":
                            result += "." + UNIT_SPINNING2;
                            break;
                        case "S3":
                            result += "." + UNIT_SPINNING3;
                            break;
                        case "S4":
                            result += "." + UNIT_SPINNINGMS;
                            break;
                        default:
                            result += ".00";
                            break;
                    }
                    break;
                case "WEAVING":
                    result = DIVISI_WEAVING;
                    switch (unitCode)
                    {
                        case "KK":
                            result += "." + UNIT_WEAVING2;
                            break;
                        case "E":
                            result += "." + UNIT_WEAVING1;
                            break;
                        default:
                            result += ".00";
                            break;
                    }
                    break;
                case "FINISHING&PRINTING":
                    result = DIVISI_FINISHING_PRINTING;
                    switch (unitCode)
                    {
                        case "F1":
                            result += "." + UNIT_FINISHING;
                            break;
                        case "F2":
                            result += "." + UNIT_PRINTING;
                            break;
                        default:
                            result += ".00";
                            break;
                    }
                    break;
                case "GARMENT":
                    result = DIVISI_GARMENT;
                    switch (unitCode)
                    {
                        case "C1A":
                            result += "." + UNIT_FINISHING;
                            break;
                        case "C1B":
                            result += "." + UNIT_PRINTING;
                            break;
                        case "C2A":
                            result += "." + UNIT_FINISHING;
                            break;
                        case "C2B":
                            result += "." + UNIT_PRINTING;
                            break;
                        case "C2C":
                            result += "." + UNIT_PRINTING;
                            break;
                        default:
                            result += ".00";
                            break;
                    }
                    break;
                default:
                    result = "0.00";
                    break;
            }

            return result;
        }

        public static string GetPurchasingCOA(string division, string unitCode, string category)
        {
            var result = "";

            switch (category.ToString().ToUpper().Replace(" ", ""))
            {
                case "EMBALAGE":
                    result += PEMBELIAN_BAHAN_EMBALASE + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
                case "BAHANBAKU":
                    result += PEMBELIAN_BAHAN_BAKU + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
                case "BAHANPEMBANTU":
                    result += PEMBELIAN_BAHAN_PEMBANTU + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
                case "BARANGJADI":
                    result += PEMBELIAN_BARANG_JADI + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
            }
            return result;
        }

        public static string GetStockCOA(string division, string unitCode, string category)
        {
            var result = "";

            switch (category.ToString().ToUpper().Replace(" ", ""))
            {
                case "BAHANBAKU":
                    result += PERSEDIAAN_BAHAN_BAKU + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
                case "BARANGJADI":
                    result += PERSEDIAAN_BARANG_JADI + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
            }
            return result;
        }

        public static string GetIncomeTaxCOA(string incomeTaxArticle, string division, string unitCode)
        {
            var result = "";
            switch (incomeTaxArticle.ToString().ToUpper().Replace(" ", ""))
            {
                case "FINAL":
                    result = PPH_FINAL + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
                case "PASAL21":
                    result = PPH21_YMH + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
                case "PASAL23":
                    result = PPH23_YMH + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
                case "PASAL26":
                    result = PPH26_YMH + ".00." + GetDivisionAndUnitCOACode(division, unitCode);
                    break;
            }

            return result;
        }
    }

    public class JournalTransaction
    {
        public string Description { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string ReferenceNo { get; set; }
        public List<JournalTransactionItem> Items { get; set; }
    }

    public class JournalTransactionItem
    {
        public COA COA { get; set; }
        public string Remark { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
    }

    public class COA
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
