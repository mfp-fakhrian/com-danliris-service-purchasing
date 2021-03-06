﻿using AutoMapper;
using Com.DanLiris.Service.Purchasing.Lib;
using Com.DanLiris.Service.Purchasing.Lib.Facades.GarmentDeliveryOrderFacades;
using Com.DanLiris.Service.Purchasing.Lib.Facades.GarmentExternalPurchaseOrderFacades;
using Com.DanLiris.Service.Purchasing.Lib.Facades.GarmentInternalPurchaseOrderFacades;
using Com.DanLiris.Service.Purchasing.Lib.Facades.GarmentPurchaseRequestFacades;
using Com.DanLiris.Service.Purchasing.Lib.Facades.GarmentUnitReceiptNoteFacades;
using Com.DanLiris.Service.Purchasing.Lib.Interfaces;
using Com.DanLiris.Service.Purchasing.Lib.Models.GarmentUnitReceiptNoteModel;
using Com.DanLiris.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels;
using Com.DanLiris.Service.Purchasing.Lib.Services;
using Com.DanLiris.Service.Purchasing.Lib.ViewModels.NewIntegrationViewModel;
using Com.DanLiris.Service.Purchasing.Test.DataUtils.GarmentDeliveryOrderDataUtils;
using Com.DanLiris.Service.Purchasing.Test.DataUtils.GarmentExternalPurchaseOrderDataUtils;
using Com.DanLiris.Service.Purchasing.Test.DataUtils.GarmentInternalPurchaseOrderDataUtils;
using Com.DanLiris.Service.Purchasing.Test.DataUtils.GarmentPurchaseRequestDataUtils;
using Com.DanLiris.Service.Purchasing.Test.DataUtils.GarmentUnitReceiptNoteDataUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;
using Com.DanLiris.Service.Purchasing.Lib.Facades.MonitoringUnitReceiptFacades;
using Com.DanLiris.Service.Purchasing.Lib.Models.GarmentDeliveryOrderModel;

namespace Com.DanLiris.Service.Purchasing.Test.Facades.GarmentUnitReceiptNoteFacadeTests
{
    public class GarmentUnitReceiptNoteFacadeTest
    {
        private const string ENTITY = "GarmentUnitReceiptNote";

        private const string USERNAME = "Unit Test";
		private IServiceProvider ServiceProvider { get; set; }

		private IServiceProvider GetServiceProvider() {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponseMessage.Content = new StringContent("{\"apiVersion\":\"1.0\",\"statusCode\":200,\"message\":\"Ok\",\"data\":[{\"Id\":7,\"code\":\"USD\",\"rate\":13700.0,\"date\":\"2018/10/20\"}],\"info\":{\"count\":1,\"page\":1,\"size\":1,\"total\":2,\"order\":{\"date\":\"desc\"},\"select\":[\"Id\",\"code\",\"rate\",\"date\"]}}");

            var httpClientService = new Mock<IHttpClientService>();
            httpClientService
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);

            var mapper = new Mock<IMapper>();
            mapper
                .Setup(x => x.Map<GarmentUnitReceiptNoteViewModel>(It.IsAny<GarmentUnitReceiptNote>()))
                .Returns(new GarmentUnitReceiptNoteViewModel {
                    Id = 1,
                    DOId = 1,
                    Supplier = new SupplierViewModel(),
                    Unit = new UnitViewModel(),
                    Items = new List<GarmentUnitReceiptNoteItemViewModel>
                    {
                        new GarmentUnitReceiptNoteItemViewModel {
                            Product = new GarmentProductViewModel(),
                            Uom = new UomViewModel()
                        }
                    }
                });

            var mockGarmentDeliveryOrderFacade = new Mock<IGarmentDeliveryOrderFacade>();
            mockGarmentDeliveryOrderFacade
                .Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new GarmentDeliveryOrder());

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(new IdentityService { Username = "Username" });
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientService.Object);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IMapper)))
                .Returns(mapper.Object);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IGarmentDeliveryOrderFacade)))
                .Returns(mockGarmentDeliveryOrderFacade.Object);


            return serviceProviderMock.Object;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private PurchasingDbContext _dbContext(string testName)
        {
            DbContextOptionsBuilder<PurchasingDbContext> optionsBuilder = new DbContextOptionsBuilder<PurchasingDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            PurchasingDbContext dbContext = new PurchasingDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private GarmentUnitReceiptNoteDataUtil dataUtil(GarmentUnitReceiptNoteFacade facade, string testName)
        {
            var garmentPurchaseRequestFacade = new GarmentPurchaseRequestFacade(_dbContext(testName));
            var garmentPurchaseRequestDataUtil = new GarmentPurchaseRequestDataUtil(garmentPurchaseRequestFacade);

            var garmentInternalPurchaseOrderFacade = new GarmentInternalPurchaseOrderFacade(_dbContext(testName));
            var garmentInternalPurchaseOrderDataUtil = new GarmentInternalPurchaseOrderDataUtil(garmentInternalPurchaseOrderFacade, garmentPurchaseRequestDataUtil);

            var garmentExternalPurchaseOrderFacade = new GarmentExternalPurchaseOrderFacade(GetServiceProvider(), _dbContext(testName));
            var garmentExternalPurchaseOrderDataUtil = new GarmentExternalPurchaseOrderDataUtil(garmentExternalPurchaseOrderFacade, garmentInternalPurchaseOrderDataUtil);

            var garmentDeliveryOrderFacade = new GarmentDeliveryOrderFacade(GetServiceProvider(), _dbContext(testName));
            var garmentDeliveryOrderDataUtil = new GarmentDeliveryOrderDataUtil(garmentDeliveryOrderFacade, garmentExternalPurchaseOrderDataUtil);

            return new GarmentUnitReceiptNoteDataUtil(facade, garmentDeliveryOrderDataUtil);
        }

		private GarmentDeliveryOrderDataUtil dataUtilDO(GarmentDeliveryOrderFacade facade, string testName)
		{
			var garmentPurchaseRequestFacade = new GarmentPurchaseRequestFacade(_dbContext(testName));
			var garmentPurchaseRequestDataUtil = new GarmentPurchaseRequestDataUtil(garmentPurchaseRequestFacade);

			var garmentInternalPurchaseOrderFacade = new GarmentInternalPurchaseOrderFacade(_dbContext(testName));
			var garmentInternalPurchaseOrderDataUtil = new GarmentInternalPurchaseOrderDataUtil(garmentInternalPurchaseOrderFacade, garmentPurchaseRequestDataUtil);

			var garmentExternalPurchaseOrderFacade = new GarmentExternalPurchaseOrderFacade(ServiceProvider, _dbContext(testName));
			var garmentExternalPurchaseOrderDataUtil = new GarmentExternalPurchaseOrderDataUtil(garmentExternalPurchaseOrderFacade, garmentInternalPurchaseOrderDataUtil);

			return new GarmentDeliveryOrderDataUtil(facade, garmentExternalPurchaseOrderDataUtil);
		}
		[Fact]
        public async void Should_Success_Get_All_Data()
        {
            var facade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
            var data = await dataUtil(facade, GetCurrentMethod()).GetTestDataWithStorage();
            var Response = facade.Read();
            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public async void Should_Success_Get_Data_By_Id()
        {
            var facade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
            var data = await dataUtil(facade, GetCurrentMethod()).GetTestDataWithStorage();
            var Response = facade.ReadById((int)data.Id);
            Assert.NotEqual(Response.Id, 0);
        }

        [Fact]
        public async void Should_Success_Generate_Pdf()
        {
            var facade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
            var data = await dataUtil(facade, GetCurrentMethod()).GetTestDataWithStorage();
            var dataViewModel = facade.ReadById((int)data.Id);
            var Response = facade.GeneratePdf(dataViewModel);
            Assert.IsType<MemoryStream>(Response);
        }

        [Fact]
        public async void Should_Success_Create_Data()
        {
            var facade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
            var data = dataUtil(facade, GetCurrentMethod()).GetNewDataWithStorage();
            var Response = await facade.Create(data);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void Should_Error_Create_Data_Null_Items()
        {
            var facade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
            var data = dataUtil(facade, GetCurrentMethod()).GetNewDataWithStorage();
            data.Items = null;
            Exception e = await Assert.ThrowsAsync<Exception>(async () => await facade.Create(data));
            Assert.NotNull(e.Message);
        }

        [Fact]
        public async void Should_Success_Update_Data()
        {
            var dbContext = _dbContext(GetCurrentMethod());
            var facade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), dbContext);

            var dataUtil = this.dataUtil(facade, GetCurrentMethod());
            var data = await dataUtil.GetTestDataWithStorage();

            dataUtil.SetDataWithStorage(data);
            var ResponseUpdateStorage = await facade.Update((int)data.Id, data);
            Assert.NotEqual(ResponseUpdateStorage, 0);

            // Create Storage based on UnitId that contain longTick on create DataUtil
            dataUtil.SetDataWithStorage(data, data.UnitId);
            var ResponseRestoreStorage = await facade.Update((int)data.Id, data);
            Assert.NotEqual(ResponseRestoreStorage, 0);

            data.IsStorage = false;
            var ResponseDeleteStorage = await facade.Update((int)data.Id, data);
            Assert.NotEqual(ResponseDeleteStorage, 0);

            dataUtil.SetDataWithStorage(data);
            var ResponseAddStorage = await facade.Update((int)data.Id, data);
            Assert.NotEqual(ResponseAddStorage, 0);
        }

        [Fact]
        public async void Should_Error_Update_Data_Null_Items()
        {
            var dbContext = _dbContext(GetCurrentMethod());
            var facade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), dbContext);

            var data = await dataUtil(facade, GetCurrentMethod()).GetTestDataWithStorage();
            dbContext.Entry(data).State = EntityState.Detached;
            data.Items = null;

            Exception e = await Assert.ThrowsAsync<Exception>(async () => await facade.Update((int)data.Id, data));
            Assert.NotNull(e.Message);
        }

        [Fact]
        public async void Should_Success_Delete_Data()
        {
            var facade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
            var data = await dataUtil(facade, GetCurrentMethod()).GetTestDataWithStorage();

            var Response = await facade.Delete((int)data.Id);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void Should_Error_Delete_Data_Invalid_Id()
        {
            var facade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
            var data = await dataUtil(facade, GetCurrentMethod()).GetTestDataWithStorage();

            Exception e = await Assert.ThrowsAsync<Exception>(async () => await facade.Delete(0));
            Assert.NotNull(e.Message);
        }

        [Fact]
        public void Should_Success_Validate_Data()
        {
            GarmentUnitReceiptNoteViewModel viewModel = new GarmentUnitReceiptNoteViewModel { IsStorage = true };
            Assert.True(viewModel.Validate(null).Count() > 0);

            GarmentUnitReceiptNoteViewModel viewModelCheckDeliveryOrder = new GarmentUnitReceiptNoteViewModel {
                Supplier = new SupplierViewModel { Id = 1 },
                Unit = new UnitViewModel { Id = "1" },
            };
            Assert.True(viewModelCheckDeliveryOrder.Validate(null).Count() > 0);

            GarmentUnitReceiptNoteViewModel viewModelCheckItemsCount = new GarmentUnitReceiptNoteViewModel { DOId = 1 };
            Assert.True(viewModelCheckItemsCount.Validate(null).Count() > 0);

            GarmentUnitReceiptNoteViewModel viewModelCheckItems = new GarmentUnitReceiptNoteViewModel {
                DOId = 1,
                Items = new List<GarmentUnitReceiptNoteItemViewModel>
                {
                    new GarmentUnitReceiptNoteItemViewModel()
                }
            };
            Assert.True(viewModelCheckItems.Validate(null).Count() > 0);

            GarmentUnitReceiptNoteViewModel viewModelCheckItemsConvertion = new GarmentUnitReceiptNoteViewModel
            {
                DOId = 1,
                Items = new List<GarmentUnitReceiptNoteItemViewModel>
                {
                    new GarmentUnitReceiptNoteItemViewModel
                    {
                        Uom = new UomViewModel
                        {
                            Id = "1"
                        },
                        SmallUom = new UomViewModel
                        {
                            Id = "1"
                        },
                        Conversion = 10
                    }
                }
            };
            Assert.True(viewModelCheckItemsConvertion.Validate(null).Count() > 0);
        }
		//monitoring
		[Fact]
		public async void Should_Success_Get_Report_Data()
		{
			GarmentDeliveryOrderFacade facade = new GarmentDeliveryOrderFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
			var data = dataUtilDO(facade, GetCurrentMethod()).GetNewData();
			await facade.Create(data, USERNAME);
			var uFacade= new GarmentUnitReceiptNoteFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
			var datas = dataUtil(uFacade, GetCurrentMethod()).GetNewDataWithStorage();
			var Responses = await uFacade.Create(datas);
			Assert.NotEqual(Responses, 0);
			var Facade = new MonitoringUnitReceiptAllFacade(ServiceProvider, _dbContext(GetCurrentMethod()));
			var Response = Facade.GetReport(null, null, null, null, null, null, null, null, 1, 25, "{}", 7);
			Assert.NotEqual(Response.Item2, 0);

			var Response1 = Facade.GetReport(null, null, null, null, null, null, null, null, 1, 25, "{}", 7);
			Assert.NotNull(Response1.Item1);


		}

		[Fact]
		public async void Should_Success_Get_Report_Excel()
		{
			GarmentDeliveryOrderFacade facade = new GarmentDeliveryOrderFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
			var data = dataUtilDO(facade, GetCurrentMethod()).GetNewData();
			await facade.Create(data, USERNAME);
			var uFacade = new GarmentUnitReceiptNoteFacade(GetServiceProvider(), _dbContext(GetCurrentMethod()));
			var datas = dataUtil(uFacade, GetCurrentMethod()).GetNewDataWithStorage();
			var Responses = await uFacade.Create(datas);
			Assert.NotEqual(Responses, 0);
			var Facade = new MonitoringUnitReceiptAllFacade(ServiceProvider, _dbContext(GetCurrentMethod()));
			var Response = Facade.GetReport(null, null, null, null, null, null, null, null, 1, 25, "{}", 7);
			Assert.NotEqual(Response.Item2, 0);

			var Response1 = Facade.GenerateExcel(null, null, null, null, null, null, null, null, 1, 25, "{}", 7);
			Assert.IsType(typeof(System.IO.MemoryStream), Response1);
		}

	}
}
