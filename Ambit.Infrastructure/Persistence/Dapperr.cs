﻿using Ambit.AppCore.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace Ambit.Infrastructure.Persistence
{
	public class Dapperr : IDapper
	{
		private readonly IConfiguration _config;
		private string Connectionstring = "DefaultConnection";

		public Dapperr(IConfiguration config)
		{
			_config = config;
		}
		public void Dispose()
		{

		}

		public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
		{
			throw new NotImplementedException();
		}

		public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
		{
			IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
			return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
		}

		public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
		{
			IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
			return db.Query<T>(sp, parms, commandType: commandType).ToList();
		}

		public DbConnection GetDbconnection()
		{
			return new SqlConnection(_config.GetConnectionString(Connectionstring));
		}

		public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
		{
			T result;
			IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
			try
			{
				if (db.State == ConnectionState.Closed)
					db.Open();

				var tran = db.BeginTransaction();
				try
				{
					result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
					tran.Commit();
				}
				catch (Exception ex)
				{
					tran.Rollback();
					throw ex;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (db.State == ConnectionState.Open)
					db.Close();
			}

			return result;
		}

		public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
		{
			T result;
			IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
			try
			{
				if (db.State == ConnectionState.Closed)
					db.Open();

				var tran = db.BeginTransaction();
				try
				{
					result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
					tran.Commit();
				}
				catch (Exception ex)
				{
					tran.Rollback();
					throw ex;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (db.State == ConnectionState.Open)
					db.Close();
			}

			return result;
		}
	}
}
