using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Justin.BI.ETL
{
    public interface IObservableOperation : IObservable<DataTable>, IDisposable
    {
        List<IObserver<DataTable>> Observers { get; }
        void Trigger();
    }

    public class AbstractObservableOperation : IObservableOperation
    {
        private List<IObserver<DataTable>> _observers = new List<IObserver<DataTable>>();
        public List<IObserver<DataTable>> Observers
        {
            get
            {
                return _observers;
            }
        }

        public virtual IDisposable Subscribe(IObserver<DataTable> observer)
        {
            if (observer is IOperation)
            {
                ((IOperation)observer).Observed.Add(this);
            }
            _observers.Add(observer);
            return new AnonymousDisposable(() => _observers.Remove(observer));
        }
        public virtual void Trigger()
        {
        }
        public virtual void Dispose()
        {
        }
    }


    public interface IOperation : IObservableOperation, IObserver<DataTable>
    {

        List<IObservableOperation> Observed { get; }
    }


    class AnonymousDisposable : IDisposable
    {
        Action dispose;

        public AnonymousDisposable(Action dispose)
        {
            this.dispose = dispose;
        }

        public void Dispose()
        {
            dispose();
        }
    }


    public class QueryOperation : AbstractObservableOperation
    {
        private CommandActivator _activator;
        public QueryOperation(CommandActivator activator)
        {
            _activator = activator;
        }

        public override void Trigger()
        {
            try
            {
                _activator.UseCommand(currentCommand =>
                {
                    if (_activator.Prepare != null)
                        _activator.Prepare(currentCommand, null);

                    if (_activator.IsQuery)
                    {
                        OleDbDataAdapter ada = new OleDbDataAdapter(currentCommand);
                        DataTable table = new DataTable();
                        ada.Fill(table);
                        Observers.PropagateOnNext(table);
                    }
                    else
                    {
                        currentCommand.ExecuteNonQuery();
                    }
                });
            }
            catch (Exception ex)
            {
                Observers.PropagateOnError(ex);
            }
            finally
            {
                _activator.Release();
            }

        }
    }

    public class CommandActivator
    {

        public string ConnStringName { get; set; }

        public string CommandText { get; set; }


        public bool UseTransaction { get; set; }

        private bool failOnError = true;

        public bool FailOnError
        {
            get
            {
                return this.failOnError;
            }
            set
            {
                this.failOnError = value;
            }
        }
        public bool IsQuery { get; set; }
        public OleDbConnection Connection { get; set; }
        public OleDbTransaction Transaction { get; set; }

        private bool _selfCreatedConnection;
        private bool _rolled;
        private OleDbCommand _currentCommand;
        public Action<OleDbCommand, DataTable> Prepare { get; set; }

        private void CheckConnection()
        {
            if (Connection == null)
            {
                _selfCreatedConnection = true;
                //TTOD
                Connection = new OleDbConnection(ConnStringName);
                if (UseTransaction)
                {
                    Transaction = Connection.BeginTransaction();
                }
            }
        }

        /// <summary>
        /// Provide a command and take care of cleaning up
        /// </summary>
        /// <param name="usecmd"></param>
        public void UseCommand(Action<OleDbCommand> usecmd)
        {
            CheckConnection();
            using (_currentCommand = Connection.CreateCommand())
            {
                _currentCommand.Transaction = Transaction;
                _currentCommand.CommandText = CommandText;
                usecmd(_currentCommand);
                _currentCommand = null;
            }
        }

        /// <summary>
        /// Release the plumbing (connection, transaction, etc)
        /// </summary>
        public void Rollback()
        {
            if (_selfCreatedConnection)
            {
                if (UseTransaction)
                {
                    Transaction.Rollback();
                    _rolled = true;
                }
            }
        }

        /// <summary>
        /// Release the plumbing (connection, transaction, etc)
        /// </summary>
        public void Release()
        {
            if (_selfCreatedConnection)
            {
                if (UseTransaction && !_rolled)
                {
                    Transaction.Commit();
                }
                if (Connection != null)
                {
                    Connection.Close();
                    Connection.Dispose();
                }
            }
        }
    }

    public static class Extensions
    {
        public static void PropagateOnNext(this IEnumerable<IObserver<DataTable>> observers, DataTable row)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(row);
            }
        }

        public static void PropagateOnError(this IEnumerable<IObserver<DataTable>> observers, Exception ex)
        {
            foreach (var observer in observers)
            {
                observer.OnError(ex);
            }
        }
    }
}
