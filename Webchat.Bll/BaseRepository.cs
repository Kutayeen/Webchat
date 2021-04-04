using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Webchat.Dal;

namespace Webchat.Bll
{
    public class BaseRepository<T> where T : class, new()
    {
        protected readonly WebchatContext context = new WebchatContext();

        public string Add(T Data)
        {
            try
            {
                context.Set<T>().Add(Data);
                int result = context.SaveChanges();
                return result > 0 ?  "Success" : "Failed";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string Delete(int index)
        {
            try
            {
                var data = context.Set<T>().Find(index);
                context.Set<T>().Remove(data);
                int result = context.SaveChanges();
                return result > 0 ? "Success" : "Failed";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T Get(int index)
        {
            return context.Set<T>().Find(index);
        }

        public string Update(T Data)
        {
            try
            {
                context.Entry<T>(Data).State = EntityState.Modified;
                int result = context.SaveChanges();
                return result > 0 ? "Success" : "Failed";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().Where(filter).ToList();
        }
    }
}
