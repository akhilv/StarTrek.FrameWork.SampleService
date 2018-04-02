using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.DTO;

namespace StarTrek.FrameWork.SampleService.DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        private const string GetSampleOrder = "GetSampleOrder";
        private const string Sql = "CreateSampleOrder";

        private readonly ISqlConfiguration _sqlConfiguration;
        public OrderRepository(ISqlConfiguration sqlConfiguration)
        {
            _sqlConfiguration = sqlConfiguration;
        }

        public async Task<IEnumerable<OrderInformation>> GetOrderInformation(string id)
        {
            IDbConnection connection = null;
            IEnumerable<OrderInformation> response = null;
            try
            {
                connection = new SqlConnection(_sqlConfiguration.ConnectionString);

                var parameter = new DynamicParameters();
                parameter.Add("@OrderId", id, DbType.String, ParameterDirection.Input);

                response = await connection.QueryAsync<OrderInformation>(GetSampleOrder, parameter, null,
                    _sqlConfiguration.CommandTimeOut, CommandType.StoredProcedure);

            }
            finally
            {
                connection?.Dispose();
            }

            return response;


        }

        public async Task<OrderInformation> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            IDbConnection connection = null;
            OrderInformation response = null;
            try
            {
                connection = new SqlConnection(_sqlConfiguration.ConnectionString);

                var parameter = new DynamicParameters();
                parameter.Add("@OrderRef", Guid.Parse(createOrderRequest.OrderRef), DbType.Guid, ParameterDirection.Input);
                parameter.Add("@CustomerId", createOrderRequest.CustomerId, DbType.String, ParameterDirection.Input);
                parameter.Add("@Price", createOrderRequest.Price, DbType.Decimal,  ParameterDirection.Input);
                parameter.Add("@Currency", createOrderRequest.Currency, DbType.String, ParameterDirection.Input);

                var result = await connection.QueryAsync<OrderInformation>(Sql, parameter, null, _sqlConfiguration.CommandTimeOut, CommandType.StoredProcedure);
                response = result?.First();
            }
            finally
            {
                connection?.Dispose();
            }
            return response;
        }

        public Task<OrderInformation> UpdateOrder(UpdateOrderRequest updateOrderRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteOrder(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}