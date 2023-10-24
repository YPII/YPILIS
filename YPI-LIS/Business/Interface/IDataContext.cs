using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface IDataContext : IDisposable
	{
		IQueryable<T> Query<T>() where T : class;
		void Insert<T>(T item) where T : class;
		void Delete<T>(T item) where T : class;
		int ExecuteCommand(string command, object[] parameters);
		void SubmitChanges();
		void Refresh(object entity);
		IQueryable<T> ExecuteQuery<T>(string command, object[] parameters) where T : class;
	}
}
