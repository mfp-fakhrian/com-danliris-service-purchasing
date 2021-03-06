﻿using Com.DanLiris.Service.Purchasing.Lib.Facades.BankExpenditureNoteFacades;
using Com.DanLiris.Service.Purchasing.Lib.Models.BankExpenditureNoteModel;
using Com.DanLiris.Service.Purchasing.Lib.Models.Expedition;
using Com.DanLiris.Service.Purchasing.Lib.Services;
using Com.DanLiris.Service.Purchasing.Test.DataUtils.ExpeditionDataUtil;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Purchasing.Test.DataUtils.BankExpenditureNoteDataUtils
{
    public class BankExpenditureNoteDataUtil
    {
        private readonly BankExpenditureNoteFacade Facade;
        private readonly PurchasingDocumentAcceptanceDataUtil pdaDataUtil;
        public BankExpenditureNoteDataUtil(BankExpenditureNoteFacade Facade, PurchasingDocumentAcceptanceDataUtil pdaDataUtil)
        {
            this.Facade = Facade;
            this.pdaDataUtil = pdaDataUtil;
        }

        public BankExpenditureNoteDetailModel GetNewDetailSpinningData()
        {
            PurchasingDocumentExpedition purchasingDocumentExpedition = Task.Run(() => this.pdaDataUtil.GetCashierTestData()).Result;

            List<BankExpenditureNoteItemModel> Items = new List<BankExpenditureNoteItemModel>();
            foreach (var item in purchasingDocumentExpedition.Items)
            {
                BankExpenditureNoteItemModel Item = new BankExpenditureNoteItemModel
                {
                    Price = item.Price,
                    ProductCode = item.ProductCode,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitCode = item.UnitCode,
                    UnitId = item.UnitId,
                    UnitName = item.UnitName,
                    Uom = item.Uom
                };

                Items.Add(Item);
            }

            return new BankExpenditureNoteDetailModel()
            {
                Id = 0,
                UnitPaymentOrderId = purchasingDocumentExpedition.Id,
                UnitPaymentOrderNo = purchasingDocumentExpedition.UnitPaymentOrderNo,
                DivisionCode = purchasingDocumentExpedition.DivisionCode,
                DivisionName = "SPINNING",
                Currency = purchasingDocumentExpedition.Currency,
                DueDate = purchasingDocumentExpedition.DueDate,
                InvoiceNo = purchasingDocumentExpedition.InvoiceNo,
                SupplierCode = purchasingDocumentExpedition.SupplierCode,
                SupplierName = purchasingDocumentExpedition.SupplierName,
                TotalPaid = purchasingDocumentExpedition.TotalPaid,
                UPODate = purchasingDocumentExpedition.UPODate,
                Vat = purchasingDocumentExpedition.Vat,
                Items = Items
            };
        }

        public BankExpenditureNoteDetailModel GetNewDetailWeavingData()
        {
            PurchasingDocumentExpedition purchasingDocumentExpedition = Task.Run(() => this.pdaDataUtil.GetCashierTestData()).Result;

            List<BankExpenditureNoteItemModel> Items = new List<BankExpenditureNoteItemModel>();
            foreach (var item in purchasingDocumentExpedition.Items)
            {
                BankExpenditureNoteItemModel Item = new BankExpenditureNoteItemModel
                {
                    Price = item.Price,
                    ProductCode = item.ProductCode,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitCode = item.UnitCode,
                    UnitId = item.UnitId,
                    UnitName = item.UnitName,
                    Uom = item.Uom
                };

                Items.Add(Item);
            }

            return new BankExpenditureNoteDetailModel()
            {
                Id = 0,
                UnitPaymentOrderId = purchasingDocumentExpedition.Id,
                UnitPaymentOrderNo = purchasingDocumentExpedition.UnitPaymentOrderNo,
                DivisionCode = purchasingDocumentExpedition.DivisionCode,
                DivisionName = "WEAVING",
                Currency = purchasingDocumentExpedition.Currency,
                DueDate = purchasingDocumentExpedition.DueDate,
                InvoiceNo = purchasingDocumentExpedition.InvoiceNo,
                SupplierCode = purchasingDocumentExpedition.SupplierCode,
                SupplierName = purchasingDocumentExpedition.SupplierName,
                TotalPaid = purchasingDocumentExpedition.TotalPaid,
                UPODate = purchasingDocumentExpedition.UPODate,
                Vat = purchasingDocumentExpedition.Vat,
                Items = Items
            };
        }

        public BankExpenditureNoteDetailModel GetNewDetailFinishingPrintingData()
        {
            PurchasingDocumentExpedition purchasingDocumentExpedition = Task.Run(() => this.pdaDataUtil.GetCashierTestData()).Result;

            List<BankExpenditureNoteItemModel> Items = new List<BankExpenditureNoteItemModel>();
            foreach (var item in purchasingDocumentExpedition.Items)
            {
                BankExpenditureNoteItemModel Item = new BankExpenditureNoteItemModel
                {
                    Price = item.Price,
                    ProductCode = item.ProductCode,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitCode = item.UnitCode,
                    UnitId = item.UnitId,
                    UnitName = item.UnitName,
                    Uom = item.Uom
                };

                Items.Add(Item);
            }

            return new BankExpenditureNoteDetailModel()
            {
                Id = 0,
                UnitPaymentOrderId = purchasingDocumentExpedition.Id,
                UnitPaymentOrderNo = purchasingDocumentExpedition.UnitPaymentOrderNo,
                DivisionCode = purchasingDocumentExpedition.DivisionCode,
                DivisionName = "FINISHING & PRINTING",
                Currency = purchasingDocumentExpedition.Currency,
                DueDate = purchasingDocumentExpedition.DueDate,
                InvoiceNo = purchasingDocumentExpedition.InvoiceNo,
                SupplierCode = purchasingDocumentExpedition.SupplierCode,
                SupplierName = purchasingDocumentExpedition.SupplierName,
                TotalPaid = purchasingDocumentExpedition.TotalPaid,
                UPODate = purchasingDocumentExpedition.UPODate,
                Vat = purchasingDocumentExpedition.Vat,
                Items = Items
            };
        }

        public BankExpenditureNoteDetailModel GetNewDetailGarmentData()
        {
            PurchasingDocumentExpedition purchasingDocumentExpedition = Task.Run(() => this.pdaDataUtil.GetCashierTestData()).Result;

            List<BankExpenditureNoteItemModel> Items = new List<BankExpenditureNoteItemModel>();
            foreach (var item in purchasingDocumentExpedition.Items)
            {
                BankExpenditureNoteItemModel Item = new BankExpenditureNoteItemModel
                {
                    Price = item.Price,
                    ProductCode = item.ProductCode,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitCode = item.UnitCode,
                    UnitId = item.UnitId,
                    UnitName = item.UnitName,
                    Uom = item.Uom
                };

                Items.Add(Item);
            }

            return new BankExpenditureNoteDetailModel()
            {
                Id = 0,
                UnitPaymentOrderId = purchasingDocumentExpedition.Id,
                UnitPaymentOrderNo = purchasingDocumentExpedition.UnitPaymentOrderNo,
                DivisionCode = purchasingDocumentExpedition.DivisionCode,
                DivisionName = "GARMENT",
                Currency = purchasingDocumentExpedition.Currency,
                DueDate = purchasingDocumentExpedition.DueDate,
                InvoiceNo = purchasingDocumentExpedition.InvoiceNo,
                SupplierCode = purchasingDocumentExpedition.SupplierCode,
                SupplierName = purchasingDocumentExpedition.SupplierName,
                TotalPaid = purchasingDocumentExpedition.TotalPaid,
                UPODate = purchasingDocumentExpedition.UPODate,
                Vat = purchasingDocumentExpedition.Vat,
                Items = Items
            };
        }

        public BankExpenditureNoteModel GetNewData()
        {
            PurchasingDocumentExpedition purchasingDocumentExpedition1 = Task.Run(() => this.pdaDataUtil.GetCashierTestData()).Result;
            PurchasingDocumentExpedition purchasingDocumentExpedition2 = Task.Run(() => this.pdaDataUtil.GetCashierTestData()).Result;

            List<BankExpenditureNoteDetailModel> Details = new List<BankExpenditureNoteDetailModel>()
            {
                GetNewDetailSpinningData(),
                GetNewDetailWeavingData(),
                GetNewDetailFinishingPrintingData(),
                GetNewDetailGarmentData()
            };

            BankExpenditureNoteModel TestData = new BankExpenditureNoteModel()
            {
                BankAccountNumber = "100020003000",
                BankAccountCOA = "BankAccountCOA",
                BankAccountName = "BankAccountName",
                BankCode = "BankCode",
                BankId = "BankId",
                BankName = "BankName",
                BankCurrencyCode = "CurrencyCode",
                BankCurrencyId = "CurrencyId",
                BankCurrencyRate = "1",
                GrandTotal = 120,
                BGCheckNumber = "BGNo",
                SupplierImport = false,
                Details = Details,
            };

            return TestData;
        }

        public BankExpenditureNoteModel GetImportData()
        {
            PurchasingDocumentExpedition purchasingDocumentExpedition1 = Task.Run(() => this.pdaDataUtil.GetCashierTestData()).Result;
            PurchasingDocumentExpedition purchasingDocumentExpedition2 = Task.Run(() => this.pdaDataUtil.GetCashierTestData()).Result;

            List<BankExpenditureNoteDetailModel> Details = new List<BankExpenditureNoteDetailModel>()
            {
                GetNewDetailSpinningData(),
                GetNewDetailWeavingData(),
                GetNewDetailFinishingPrintingData(),
                GetNewDetailGarmentData()
            };

            BankExpenditureNoteModel TestData = new BankExpenditureNoteModel()
            {
                BankAccountNumber = "100020003000",
                BankAccountCOA = "BankAccountCOA",
                BankAccountName = "BankAccountName",
                BankCode = "BankCode",
                BankId = "BankId",
                BankName = "BankName",
                BankCurrencyCode = "CurrencyCode",
                BankCurrencyId = "CurrencyId",
                BankCurrencyRate = "1",
                GrandTotal = 120,
                BGCheckNumber = "BGNo",
                SupplierImport = true,
                Details = Details,
            };

            return TestData;
        }

        public async Task<BankExpenditureNoteModel> GetTestData()
        {
            IdentityService identityService = new IdentityService()
            {
                Token = "Token",
                Username = "Unit Test"
            };
            BankExpenditureNoteModel model = GetNewData();
            await Facade.Create(model, identityService);
            return await Facade.ReadById((int)model.Id);
        }
    }
}
