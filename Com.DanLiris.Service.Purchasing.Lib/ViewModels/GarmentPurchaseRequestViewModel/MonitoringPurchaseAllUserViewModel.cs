﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.Purchasing.Lib.ViewModels.GarmentPurchaseRequestViewModel
{
	public class MonitoringPurchaseAllUserViewModel
	{
		public long id { get; set; }
		public int index { get; set; }
		public string prNo { get; set; }
		public string prDate { get; set; }
		public string poextNo { get; set; }
		public string poExtDate { get; set; }
		public string unitName { get; set; }
		public string useInternalPO { get; set; }
		public string poSerialNumber { get; set; }
		public string ro { get; set; }
		public string article { get; set; }
		public string shipmentDate { get; set; }
		public string productCode { get; set; }
		public string productName { get; set; }
		public string buyerName { get; set; }
		public string buyerCode { get; set; }
		public string prProductRemark { get; set; }
		public string poProductRemark { get; set; }
		public double poDealQty { get; set; }
		public string poDealUomUnit { get; set; }
		public double prBudgetPrice { get; set; }
		public double poPricePerDealUnit { get; set; }
		public double totalNominalPO { get; set; }
		public string poCurrencyCode { get; set; }
		public double poCurrencyRate { get; set; }
		public double totalNominalRp { get; set; }
		public string supplierCode { get; set; }
		public string supplierName { get; set; }
		public string epoNo { get; set; }
		public string ipoDate { get; set; }
		public string poDate { get; set; }
		public string useVat { get; set; }
		public string useIncomeTax { get; set; }
		public string deliveryDate { get; set; }
		public string invoiceNo { get; set; }
		public string invoiceDate { get; set; }
		public string incomeTaxNo { get; set; }
		public string incomeTaxDate { get; set; }
		public string incomeTaxtRate { get; set; }
		public string incomeTaxRate { get; set; }
		public decimal incomeTaxtValue { get; set; }
		public string incomeTaxType { get; set; }
		public string vatNo { get; set; }
		public string vatDate { get; set; }
		public decimal vatValue { get; set; }
		public long dodetailId { get; set; }
		public string doNo { get; set; }
		public string doDate { get; set; }
		public string arrivalDate { get; set; }
		public double doQty { get; set; }
		public double remainingDOQty { get; set; }
		public string doUomUnit { get; set; }
		public string bcNo { get; set; }
		public string bcDate { get; set; }
		public string receiptNo { get; set; }
		public string receiptDate { get; set; }
		public string receiptQty { get; set; }
		public string receiptUomUnit { get; set; }
		public string internNo { get; set; }
		public string internDate { get; set; }
		public decimal internTotal { get; set; }
		public string maturityDate { get; set; }
		public string correctionNoteNo { get; set; }
		public string correctionDate { get; set; }
		public decimal  correctionTotal { get; set; }
		public string  valueCorrection { get; set; }
		public string correctionRemark { get; set; }
		public string username { get; set; }
		public string status { get; set; }
	}
}
