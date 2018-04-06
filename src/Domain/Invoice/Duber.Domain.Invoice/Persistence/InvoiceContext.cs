﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Duber.Infrastructure.DDD;
using Duber.Infrastructure.Extensions;
using MediatR;

namespace Duber.Domain.Invoice.Persistence
{
    public class InvoiceContext : IInvoiceContext
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        private readonly IMediator _mediator;

        public InvoiceContext(string connectionString, IMediator mediator)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException(nameof(connectionString));

            _connectionString = connectionString;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _connection = GetOpenConnection();
        }

        public async Task<int> ExecuteAsync<T>(T entity, string sql, object parameters = null, int? timeOut = null, CommandType? commandType = null)
            where T : Entity, IAggregateRoot
        {
            using (_connection = GetOpenConnection())
            {
                var result = await _connection.ExecuteAsync(sql, parameters, null, timeOut, commandType);

                // ensures that all events are dispatched after the entity is saved successfully.
                await _mediator.DispatchDomainEventsAsync(entity);
                return result;
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, int? timeOut = null, CommandType? commandType = null)
            where T : Entity, IAggregateRoot
        {
            using (_connection = GetOpenConnection())
            {
                return await _connection.QueryAsync<T>(sql, parameters, null, timeOut, commandType);
            }
        }

        private IDbConnection GetOpenConnection()
        {
            if (_connection == null)
            {
                return new SqlConnection(_connectionString);
            }

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            return _connection;
        }
    }
}
