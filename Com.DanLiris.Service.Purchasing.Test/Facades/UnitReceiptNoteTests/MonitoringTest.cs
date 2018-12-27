﻿using Com.DanLiris.Service.Purchasing.Lib.Facades.UnitReceiptNoteFacade;
using Com.DanLiris.Service.Purchasing.Lib.Models.UnitReceiptNoteModel;
using Com.DanLiris.Service.Purchasing.Lib.Services;
using Com.DanLiris.Service.Purchasing.Test.DataUtils.UnitReceiptNoteDataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Purchasing.Test.Facades.UnitReceiptNoteTests
{
    [Collection("ServiceProviderFixture Collection")]
    public class MonitoringTest
    {
        private IServiceProvider ServiceProvider { get; set; }

        public MonitoringTest(ServiceProviderFixture fixture)
        {
            ServiceProvider = fixture.ServiceProvider;

            IdentityService identityService = (IdentityService)ServiceProvider.GetService(typeof(IdentityService));
            identityService.Username = "Unit Test";
        }

        private UnitReceiptNoteDataUtil DataUtil
        {
            get { return (UnitReceiptNoteDataUtil)ServiceProvider.GetService(typeof(UnitReceiptNoteDataUtil)); }
        }

        private UnitReceiptNoteFacade Facade
        {
            get { return (UnitReceiptNoteFacade)ServiceProvider.GetService(typeof(UnitReceiptNoteFacade)); }
        }

        [Fact]
        public async void Should_Success_Get_Report_Data()
        {
            UnitReceiptNote model = await DataUtil.GetTestData("Unit test");
            var Response = Facade.GetReport(model.URNNo, "", model.UnitId, "", "", null, null, 1, 25, "{}", 7);
            Assert.NotEqual(Response.TotalData, 0);
        }

        [Fact]
        public async void Should_Success_Get_Report_Data_Null_Parameter()
        {
            UnitReceiptNote model = await DataUtil.GetTestData("Unit test");
            var Response = Facade.GetReport("", "", "", "", "", null, null, 1, 25, "{}", 7);
            Assert.NotEqual(Response.TotalData, 0);
        }

        [Fact]
        public async void Should_Success_Get_Report_Data_Excel()
        {
            UnitReceiptNote model = await DataUtil.GetTestData("Unit test");
            var Response = Facade.GenerateExcel(model.URNNo,"", model.UnitId,  "", "", null, null, 7);
            Assert.IsType(typeof(System.IO.MemoryStream), Response);
        }

        [Fact]
        public async void Should_Success_Get_Report_Data_Excel_Null_Parameter()
        {
            UnitReceiptNote model = await DataUtil.GetTestData("Unit test");
            var Response = Facade.GenerateExcel("", "", "", "", "", null, null, 7);
            Assert.IsType(typeof(System.IO.MemoryStream), Response);
        }
    }
}
